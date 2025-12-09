using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	/// <summary>
	/// Localization strings for weather system.
	/// </summary>
	[XmlType]
	public class LanguageWeather
	{
		#region Precipitation Types

		[XmlElement]
		public string NoPrecipitation
		{ get; set; }

		[XmlElement]
		public string LightRain
		{ get; set; }

		[XmlElement]
		public string HeavyRain
		{ get; set; }

		[XmlElement]
		public string LightSnow
		{ get; set; }

		[XmlElement]
		public string HeavySnow
		{ get; set; }

		[XmlElement]
		public string Thunderstorm
		{ get; set; }

		#endregion

		#region Temperature Descriptors

		[XmlElement]
		public string Freezing
		{ get; set; }

		[XmlElement]
		public string Cold
		{ get; set; }

		[XmlElement]
		public string Cool
		{ get; set; }

		[XmlElement]
		public string Comfortable
		{ get; set; }

		[XmlElement]
		public string Warm
		{ get; set; }

		[XmlElement]
		public string Hot
		{ get; set; }

		[XmlElement]
		public string Scorching
		{ get; set; }

		#endregion

		#region Wind Strength

		[XmlElement]
		public string Calm
		{ get; set; }

		[XmlElement]
		public string Breezy
		{ get; set; }

		[XmlElement]
		public string Windy
		{ get; set; }

		[XmlElement]
		public string StrongWinds
		{ get; set; }

		[XmlElement]
		public string Gale
		{ get; set; }

		#endregion

		#region Sun Visibility

		[XmlElement]
		public string BrightSunshine
		{ get; set; }

		[XmlElement]
		public string PartlyCloudy
		{ get; set; }

		[XmlElement]
		public string Overcast
		{ get; set; }

		[XmlElement]
		public string DarkClouds
		{ get; set; }

		#endregion

		#region Status Messages

		[XmlElement]
		public string GettingWet
		{ get; set; }

		[XmlElement]
		public string FeelingCold
		{ get; set; }

		[XmlElement]
		public string FingersNumb
		{ get; set; }

		[XmlElement]
		public string FeelingHot
		{ get; set; }

		[XmlElement]
		public string Overheating
		{ get; set; }

		[XmlElement]
		public string ClothingDrying
		{ get; set; }

		[XmlElement]
		public string StruckByLightning
		{ get; set; }

		#endregion

		#region Item Condition Messages

		[XmlElement]
		public string PapersSoaked
		{ get; set; }

		[XmlElement]
		public string ItemRusting
		{ get; set; }

		[XmlElement]
		public string ItemRusted
		{ get; set; }

		#endregion

		#region Weather Display

		[XmlElement]
		public string Temperature
		{ get; set; }

		[XmlElement]
		public string Wind
		{ get; set; }

		[XmlElement]
		public string Precipitation
		{ get; set; }

		[XmlElement]
		public string SunVisibility
		{ get; set; }

		#endregion

		public static LanguageWeather CreateDefault()
		{
			return new LanguageWeather
			{
				// Precipitation Types
				NoPrecipitation = "Clear",
				LightRain = "Light Rain",
				HeavyRain = "Heavy Rain",
				LightSnow = "Light Snow",
				HeavySnow = "Heavy Snow",
				Thunderstorm = "Thunderstorm",

				// Temperature Descriptors
				Freezing = "Freezing",
				Cold = "Cold",
				Cool = "Cool",
				Comfortable = "Comfortable",
				Warm = "Warm",
				Hot = "Hot",
				Scorching = "Scorching",

				// Wind Strength
				Calm = "Calm",
				Breezy = "Breezy",
				Windy = "Windy",
				StrongWinds = "Strong Winds",
				Gale = "Gale",

				// Sun Visibility
				BrightSunshine = "Bright Sunshine",
				PartlyCloudy = "Partly Cloudy",
				Overcast = "Overcast",
				DarkClouds = "Dark Clouds",

				// Status Messages
				GettingWet = "You are getting wet",
				FeelingCold = "You feel cold",
				FingersNumb = "Your fingers are numb with frostbite",
				FeelingHot = "You feel hot",
				Overheating = "You are overheating",
				ClothingDrying = "Your clothing is drying",
				StruckByLightning = "You were struck by lightning!",

				// Item Condition Messages
				PapersSoaked = "Your papers are soaked",
				ItemRusting = "Your {0} is rusting",
				ItemRusted = "Your {0} has rusted",

				// Weather Display
				Temperature = "Temperature",
				Wind = "Wind",
				Precipitation = "Precipitation",
				SunVisibility = "Sun",
			};
		}
	}
}
