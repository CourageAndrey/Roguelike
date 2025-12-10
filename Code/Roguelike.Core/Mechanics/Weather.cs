namespace Roguelike.Core.Mechanics
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

		public bool IsInterior
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

		public event EventHandler? Changed;

		#endregion

		public Weather(Region region, bool isInterior)
		{
			Region = region;
			IsInterior = isInterior;
			Change();
		}

		internal void Change()
		{
#warning If IsInterior=TRUE need to change separately
			var balance = Region.World.Balance.Weather;
			var time = Region.World.Time;
			var seed = new Random(DateTime.Now.Millisecond);

			Temperature = CalculateTemperature(time, balance, seed);
			Precipitation = CalculatePrecipitation(seed, Temperature);
			SunVisibility = CalculateSunVisibility(Precipitation, seed);
			WindDirection = Directions.All8.GetRandom(seed);
			WindSpeed = seed.NextDouble() * 10.0; // 0-10 cells per turn
			VisibilityBonus = CalculateVisibilityBonus(SunVisibility, Precipitation, balance);

			var minDuration = (int)balance.MinWeatherChangeDuration;
			var maxDuration = (int)balance.MaxWeatherChangeDuration;
			NextChangeTime = Region.World.Time.AddTicks((uint)seed.Next(minDuration, maxDuration));

			var handler = Volatile.Read(ref Changed);
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		private double CalculateTemperature(Time time, Configuration.WeatherBalance balance, Random seed)
		{
			double baseSeasonTemperature = time.Season switch
			{
				Season.Winter => balance.WinterBaseTemperature,
				Season.Spring => balance.SpringBaseTemperature,
				Season.Summer => balance.SummerBaseTemperature,
				Season.Autumn => balance.AutumnBaseTemperature,
				_ => balance.SpringBaseTemperature,
			};

			double daytimeVariation = time.DayPart switch
			{
				DayPart.Night => -balance.DailyTemperatureVariation,
				DayPart.Morning => -balance.DailyTemperatureVariation / 2,
				DayPart.Noon => balance.DailyTemperatureVariation / 2,
				DayPart.Evening => 0,
				_ => 0,
			};

			double randomVariation = (seed.NextDouble() - 0.5) * balance.DailyTemperatureVariation * 0.2;

			return baseSeasonTemperature + daytimeVariation + randomVariation;
		}

		private Precipitation CalculatePrecipitation(Random seed, double temperature)
		{
#warning Make all percents here depending on Balance and climate.
			// 30% chance of precipitation
			if (seed.NextDouble() > 0.3) return Precipitation.No;

			// Below freezing: snow, above: rain
			if (temperature < 0)
			{
				// 70% light snow, 20% heavy snow, 10% no snow
				double snowRoll = seed.NextDouble();
				if (snowRoll < 0.7)
				{
					return Precipitation.LightSnow;
				}
				else if (snowRoll < 0.9)
				{
					return Precipitation.HeavySnow;
				}
				else
				{
					return Precipitation.No;
				}
			}
			else
			{
				// 60% light rain, 25% heavy rain, 15% thunderstorm
				double rainRoll = seed.NextDouble();
				if (rainRoll < 0.6)
				{
					return Precipitation.LightRain;
				}
				else if (rainRoll < 0.85)
				{
					return Precipitation.HeavyRain;
				}
				else
				{
					return Precipitation.Thunderstorm;
				}
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
			if (precipitation == Precipitation.HeavyRain ||
				precipitation == Precipitation.HeavySnow ||
				precipitation == Precipitation.Thunderstorm)
			{
				sunMultiplier *= balance.StormVisibilityMultiplier;
			}

			return sunMultiplier;
		}
	}
}
