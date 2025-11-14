using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageRace
	{
		#region Properties

		[XmlElement]
		public string Name
		{ get; set; }

		[XmlElement]
		public NameLists Names
		{ get; set; }

		[XmlArray]
		public string[] Surnames
		{ get; set; }

		[XmlElement]
		public string DefaultSettlementName
		{ get; set; }

		#endregion

		public static LanguageRace CreatePlainsman()
		{
			return new LanguageRace
			{
				Name = "Plainsman",
				Names = new NameLists
				{
					Male = new[]
					{
						"John",
						"Bill",
						"Bob",
						"Tom",
						"Edward",
						"Harry",
						"Jack",
						"Charles",
						"George",
						"Henry",
						"Louis",
						"Oscar",
					},
					Female = new[]
					{
						"Emma",
						"Alice",
						"Berta",
						"Ella",
						"Charlotte",
						"Amelia",
						"Lillian",
						"Eleanor",
						"Lucy",
						"Juliet",
					}
				},
				Surnames = new[]
				{
					"Black",
					"White",
					"Green",
					"Brown",
					"Evans",
					"Stone",
					"Roberts",
					"Mills",
					"Lewis",
					"Morgan",
					"Florence",
					"Campbell",
					"Bronte",
					"Bell",
					"Adams",
				},
				DefaultSettlementName = "Citytown village"
			};
		}

		public static LanguageRace CreateNomad()
		{
			return new LanguageRace
			{
				Name = "Nomad",
				Names = new NameLists
				{
					Male = new[]
					{
						"Abbas",
						"Abu",
						"Aziz",
						"Ali",
						"Valid",
						"Jabir",
						"Zahir",
						"Imran",
						"Ibrahim",
						"Karim",
						"Maimun",
						"Mubarak",
					},
					Female = new[]
					{
						"Aisha",
						"Basma",
						"Jamila",
						"Zainab",
						"Zuhra",
						"Leila",
						"Mariam",
					}
				},
				Surnames = new[]
				{
					"Hagan",
					"Hulgana",
					"Shona",
					"Naran",
					"Oktaj",
				},
				DefaultSettlementName = "Qaryat al-madina al-balad"
			};
		}

		public static LanguageRace CreateHighlander()
		{
			return new LanguageRace
			{
				Name = "Highlander",
				Names = new NameLists
				{
					Male = new[]
					{
						"Breasal",
						"Brian",
						"Conall",
						"Conan",
						"Kenneth",
						"Lorcan",
						"Niall",
						"Rian",
					},
					Female = new[]
					{
						"Eithne",
						"Deirdre",
						"Sorcha",
					}
				},
				Surnames = new[]
				{
					"McCartney",
					"O'Sullivan",
					"O'Connor",
					"O'Neill",
					"O'Reilly",
					"Walsh",
				},
				DefaultSettlementName = "Bailebhaile sráidbhaile"
			};
		}

		public static LanguageRace CreateJungleman()
		{
			return new LanguageRace
			{
				Name = "Jungleman",
				Names = new NameLists
				{
					Male = new[]
					{
						"Amokstly",
						"Zolin",
						"Ikstly",
						"Kitlaly",
						"Koatle",
					},
					Female = new[]
					{
						"Zelcine",
						"Iskacine",
						"Papan",
						"Tlako",
					}
				},
				Surnames = new[]
				{
					"Atl",
					"Ake",
					"kojotl",
				},
				DefaultSettlementName = "Altépetl-calpolli ranchito"
			};
		}

		public static LanguageRace CreateNordman()
		{
			return new LanguageRace
			{
				Name = "Nordman",
				Names = new NameLists
				{
					Male = new[]
					{
						"Oscar",
						"Liam",
						"Axel",
						"Noah",
						"Nils",
						"Arvid",
						"Theodor",
						"Olle",
						"Erik",
						"Viggo",
						"Ebbe",
						"Elton",
						"Otto",
					},
					Female = new[]
					{
						"Elsa",
						"Agnes",
						"Olivia",
						"Julia",
						"Ebba",
						"Linnea",
						"Freja",
						"Astrid",
						"Signe",
						"Tyra",
						"Tuva",
						"Tilde",
					}
				},
				Surnames = new[]
				{
					"Andersson",
					"Johansson",
					"Karlsson",
					"Nilsson",
					"Eriksson",
					"Pettersson",
					"Lindberg",
					"Lindgren",
					"Bergström",
					"Fredriksson",
					"Björk",
				},
				DefaultSettlementName = "Bystad landsby"
			};
		}
	}

	[XmlType]
	public class NameLists
	{
		[XmlArray]
		public string[] Male
		{ get; set; }

		[XmlArray]
		public string[] Female
		{ get; set; }
	}
}
