using System;
using System.Threading;

namespace Roguelike.Core
{
	public enum Precipitation
	{
		No,
		LightRain,
		HeavyRain,
		LightSnow,
		HeavySnow,
		Thunderstorm,
	}

	public enum SunVisibility
	{
		BrightSunshine,
		PartlyCloudy,
		Overcast,
		DarkClouds,
	}

	public class Weather
	{
		#region Properties

		public Region Region
		{ get; }

		public double Temperature
		{ get; private set; } // C degrees

		public Direction WindDirection
		{ get; private set; }

		public double WindSpeed
		{ get; private set; } // cells of arrow shift

		public double VisibilityBonus
		{ get; private set; } // % of visibility range

		public Precipitation Precipitation
		{ get; private set; }

		public SunVisibility SunVisibility
		{ get; private set; }

		public Time NextChangeTime
		{ get; private set; }

		public event EventHandler Changed;

		#endregion

		public Weather(Region region)
		{
			Region = region;
			Change();
		}

		internal void Change()
		{
			var balance = Region.World.Balance.Weather;
			var time = Region.World.Time;
			var seed = new Random(DateTime.Now.Millisecond);

			// Calculate temperature based on season and time of day
			Temperature = CalculateTemperature(time, balance, seed);

			// Determine precipitation
			Precipitation = CalculatePrecipitation(seed, Temperature);

			// Determine sun visibility based on precipitation
			SunVisibility = CalculateSunVisibility(Precipitation, seed);

			// Calculate wind
			WindDirection = Directions.All8.GetRandom(seed);
			WindSpeed = seed.NextDouble() * 10.0; // 0-10 cells per turn

			// Calculate visibility bonus based on sun and precipitation
			VisibilityBonus = CalculateVisibilityBonus(SunVisibility, Precipitation, balance);

			// Schedule next weather change
			var timeBalance = Region.World.Time.Balance;
			var minDuration = (int)balance.MinWeatherChangeDuration;
			var maxDuration = (int)balance.MaxWeatherChangeDuration;
			NextChangeTime = Region.World.Time.AddTicks((uint)seed.Next(minDuration, maxDuration));

			Volatile.Read(ref Changed)?.Invoke(this, EventArgs.Empty);
		}

		private double CalculateTemperature(Time time, Configuration.WeatherBalance balance, Random seed)
		{
			// Base temperature by season
			double baseTemp = time.Season switch
			{
				Season.Winter => balance.WinterBaseTemperature,
				Season.Spring => balance.SpringBaseTemperature,
				Season.Summer => balance.SummerBaseTemperature,
				Season.Autumn => balance.AutumnBaseTemperature,
				_ => balance.SpringBaseTemperature,
			};

			// Time of day variation (coldest at night, warmest at noon)
			double timeVariation = time.DayPart switch
			{
				DayPart.Night => -balance.DailyTemperatureVariation,
				DayPart.Morning => -balance.DailyTemperatureVariation / 2,
				DayPart.Noon => balance.DailyTemperatureVariation / 2,
				DayPart.Evening => 0,
				_ => 0,
			};

			// Add some random variation
			double randomVariation = (seed.NextDouble() - 0.5) * balance.DailyTemperatureVariation * 0.2;

			return baseTemp + timeVariation + randomVariation;
		}

		private Precipitation CalculatePrecipitation(Random seed, double temperature)
		{
			// 30% chance of precipitation
			if (seed.NextDouble() > 0.3)
				return Precipitation.No;

			// Below freezing: snow, above: rain
			if (temperature < 0)
			{
				// 70% light snow, 20% heavy snow, 10% no snow
				double snowRoll = seed.NextDouble();
				if (snowRoll < 0.7)
					return Precipitation.LightSnow;
				else if (snowRoll < 0.9)
					return Precipitation.HeavySnow;
				else
					return Precipitation.No;
			}
			else
			{
				// 60% light rain, 25% heavy rain, 15% thunderstorm
				double rainRoll = seed.NextDouble();
				if (rainRoll < 0.6)
					return Precipitation.LightRain;
				else if (rainRoll < 0.85)
					return Precipitation.HeavyRain;
				else
					return Precipitation.Thunderstorm;
			}
		}

		private SunVisibility CalculateSunVisibility(Precipitation precipitation, Random seed)
		{
			return precipitation switch
			{
				Precipitation.No => seed.NextDouble() < 0.7 ? SunVisibility.BrightSunshine : SunVisibility.PartlyCloudy,
				Precipitation.LightRain => SunVisibility.Overcast,
				Precipitation.LightSnow => SunVisibility.Overcast,
				Precipitation.HeavyRain => SunVisibility.DarkClouds,
				Precipitation.HeavySnow => SunVisibility.DarkClouds,
				Precipitation.Thunderstorm => SunVisibility.DarkClouds,
				_ => SunVisibility.PartlyCloudy,
			};
		}

		private double CalculateVisibilityBonus(SunVisibility sun, Precipitation precipitation, Configuration.WeatherBalance balance)
		{
			double sunMultiplier = sun switch
			{
				SunVisibility.BrightSunshine => balance.ClearVisibilityMultiplier,
				SunVisibility.PartlyCloudy => balance.CloudyVisibilityMultiplier,
				SunVisibility.Overcast => balance.CloudyVisibilityMultiplier,
				SunVisibility.DarkClouds => balance.StormVisibilityMultiplier,
				_ => 1.0,
			};

			// Heavy precipitation further reduces visibility
			if (precipitation == Precipitation.HeavyRain || precipitation == Precipitation.HeavySnow || precipitation == Precipitation.Thunderstorm)
			{
				sunMultiplier *= balance.StormVisibilityMultiplier;
			}

			return sunMultiplier;
		}
	}
}
