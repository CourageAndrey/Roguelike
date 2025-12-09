namespace Roguelike.Core.Configuration
{
	/// <summary>
	/// Configuration for weather system balance parameters.
	/// </summary>
	public class WeatherBalance
	{
		#region Temperature

		/// <summary>
		/// Base temperature for spring season (°C).
		/// </summary>
		public double SpringBaseTemperature
		{ get; set; }

		/// <summary>
		/// Base temperature for summer season (°C).
		/// </summary>
		public double SummerBaseTemperature
		{ get; set; }

		/// <summary>
		/// Base temperature for autumn season (°C).
		/// </summary>
		public double AutumnBaseTemperature
		{ get; set; }

		/// <summary>
		/// Base temperature for winter season (°C).
		/// </summary>
		public double WinterBaseTemperature
		{ get; set; }

		/// <summary>
		/// Temperature variation range for time of day (±°C).
		/// </summary>
		public double DailyTemperatureVariation
		{ get; set; }

		/// <summary>
		/// Indoor temperature heat transfer rate (0.0-1.0).
		/// </summary>
		public double IndoorHeatTransferRate
		{ get; set; }

		/// <summary>
		/// Temperature increase per fire source (°C).
		/// </summary>
		public double FireHeatingEffect
		{ get; set; }

		/// <summary>
		/// Fire heating effective radius in cells.
		/// </summary>
		public int FireHeatingRadius
		{ get; set; }

		#endregion

		#region Temperature Effects

		/// <summary>
		/// Time in ticks before cold exposure causes catching cold risk.
		/// </summary>
		public uint ColdExposureTimeThreshold
		{ get; set; }

		/// <summary>
		/// Time in ticks before cold exposure causes frostbite damage.
		/// </summary>
		public uint FrostbiteTimeThreshold
		{ get; set; }

		/// <summary>
		/// Time in ticks before heat exposure causes heat exhaustion.
		/// </summary>
		public uint HeatExhaustionTimeThreshold
		{ get; set; }

		/// <summary>
		/// Time in ticks before heat exposure causes overheating death risk.
		/// </summary>
		public uint OverheatingTimeThreshold
		{ get; set; }

		#endregion

		#region Precipitation

		/// <summary>
		/// Time in ticks to accumulate wetness from light rain.
		/// </summary>
		public uint LightRainWetnessRate
		{ get; set; }

		/// <summary>
		/// Time in ticks to accumulate wetness from heavy rain.
		/// </summary>
		public uint HeavyRainWetnessRate
		{ get; set; }

		/// <summary>
		/// Time in ticks to dry clothing indoors.
		/// </summary>
		public uint IndoorDryingRate
		{ get; set; }

		/// <summary>
		/// Time in ticks to dry clothing near fire.
		/// </summary>
		public uint FireDryingRate
		{ get; set; }

		/// <summary>
		/// Base probability of catching sickness when wet (0.0-1.0).
		/// </summary>
		public double WetSicknessProbability
		{ get; set; }

		/// <summary>
		/// Time in ticks for metal items to accumulate rust damage.
		/// </summary>
		public uint RustAccumulationRate
		{ get; set; }

		/// <summary>
		/// Time in ticks for paper items to accumulate water damage.
		/// </summary>
		public uint PaperDamageRate
		{ get; set; }

		/// <summary>
		/// Base lightning strike probability during thunderstorm (0.0-1.0).
		/// </summary>
		public double BaseLightningProbability
		{ get; set; }

		/// <summary>
		/// Lightning probability multiplier for carrying metal items.
		/// </summary>
		public double MetalLightningMultiplier
		{ get; set; }

		/// <summary>
		/// Lightning probability multiplier per elevation level.
		/// </summary>
		public double ElevationLightningMultiplier
		{ get; set; }

		#endregion

		#region Wind

		/// <summary>
		/// Wind speed threshold for projectile deflection (cells per turn).
		/// </summary>
		public double WindDeflectionThreshold
		{ get; set; }

		/// <summary>
		/// Maximum projectile deflection in cells per wind speed unit.
		/// </summary>
		public double MaxProjectileDeflection
		{ get; set; }

		/// <summary>
		/// Object weight threshold for wind movement (kg).
		/// </summary>
		public double WindMovementWeightThreshold
		{ get; set; }

		/// <summary>
		/// Wind speed threshold for character movement reduction.
		/// </summary>
		public double StrongWindThreshold
		{ get; set; }

		/// <summary>
		/// Movement speed reduction multiplier in strong winds (0.0-1.0).
		/// </summary>
		public double WindMovementPenalty
		{ get; set; }

		#endregion

		#region Visibility

		/// <summary>
		/// Vision range multiplier for clear sunny conditions (1.0 = normal).
		/// </summary>
		public double ClearVisibilityMultiplier
		{ get; set; }

		/// <summary>
		/// Vision range multiplier for cloudy conditions (0.0-1.0).
		/// </summary>
		public double CloudyVisibilityMultiplier
		{ get; set; }

		/// <summary>
		/// Vision range multiplier for storm conditions (0.0-1.0).
		/// </summary>
		public double StormVisibilityMultiplier
		{ get; set; }

		#endregion

		#region Weather Transitions

		/// <summary>
		/// Minimum time in ticks between weather changes.
		/// </summary>
		public uint MinWeatherChangeDuration
		{ get; set; }

		/// <summary>
		/// Maximum time in ticks between weather changes.
		/// </summary>
		public uint MaxWeatherChangeDuration
		{ get; set; }

		#endregion

		public static WeatherBalance CreateDefault()
		{
			return new WeatherBalance
			{
				// Temperature
				SpringBaseTemperature = 15.0,
				SummerBaseTemperature = 25.0,
				AutumnBaseTemperature = 10.0,
				WinterBaseTemperature = -5.0,
				DailyTemperatureVariation = 10.0,
				IndoorHeatTransferRate = 0.1,
				FireHeatingEffect = 15.0,
				FireHeatingRadius = 5,

				// Temperature Effects
				ColdExposureTimeThreshold = 300000, // 5 minutes
				FrostbiteTimeThreshold = 600000, // 10 minutes
				HeatExhaustionTimeThreshold = 600000, // 10 minutes
				OverheatingTimeThreshold = 1200000, // 20 minutes

				// Precipitation
				LightRainWetnessRate = 180000, // 3 minutes
				HeavyRainWetnessRate = 60000, // 1 minute
				IndoorDryingRate = 600000, // 10 minutes
				FireDryingRate = 180000, // 3 minutes
				WetSicknessProbability = 0.01,
				RustAccumulationRate = 3600000, // 1 hour
				PaperDamageRate = 300000, // 5 minutes
				BaseLightningProbability = 0.001,
				MetalLightningMultiplier = 5.0,
				ElevationLightningMultiplier = 1.5,

				// Wind
				WindDeflectionThreshold = 3.0,
				MaxProjectileDeflection = 2.0,
				WindMovementWeightThreshold = 5.0,
				StrongWindThreshold = 5.0,
				WindMovementPenalty = 0.5,

				// Visibility
				ClearVisibilityMultiplier = 1.0,
				CloudyVisibilityMultiplier = 0.8,
				StormVisibilityMultiplier = 0.5,

				// Weather Transitions
				MinWeatherChangeDuration = 900000, // 15 minutes
				MaxWeatherChangeDuration = 3600000, // 60 minutes
			};
		}
	}
}
