﻿using System;
using System.Collections.Generic;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Profession
	{
		#region Properties

		private readonly Func<LanguageProfessions, string> _getName;

		public bool IsSurname
		{ get; }

		#endregion

		private Profession(Func<LanguageProfessions, string> nameGetter, bool isSurname = false)
		{
			_getName = nameGetter;
			IsSurname = isSurname;
		}

		public string GetName(LanguageProfessions language)
		{
			return _getName(language);
		}

		#region List

		public static readonly Profession Miller		= new Profession(language => language.Miller, true);
		public static readonly Profession Baker			= new Profession(language => language.Baker, true);
		public static readonly Profession Weaver		= new Profession(language => language.Weaver, true);
		public static readonly Profession Tailor		= new Profession(language => language.Tailor, true);
		public static readonly Profession Hunter		= new Profession(language => language.Hunter, true);
		public static readonly Profession Fisher		= new Profession(language => language.Fisher, true);
		public static readonly Profession Tanner		= new Profession(language => language.Tanner);
		public static readonly Profession Miner			= new Profession(language => language.Miner);
		public static readonly Profession Smith			= new Profession(language => language.Smith, true);
		public static readonly Profession Farmer		= new Profession(language => language.Farmer);
		public static readonly Profession WoodCutter	= new Profession(language => language.WoodCutter);
		public static readonly Profession Soldier		= new Profession(language => language.Soldier);
		public static readonly Profession Bandit		= new Profession(language => language.Bandit);
		public static readonly Profession Healer		= new Profession(language => language.Healer);
		public static readonly Profession Monk			= new Profession(language => language.Monk);
		public static readonly Profession Merchant		= new Profession(language => language.Merchant);
		public static readonly Profession Clerk			= new Profession(language => language.Clerk);
		public static readonly Profession Cook			= new Profession(language => language.Cook);
		public static readonly Profession Everyman		= new Profession(language => language.Everyman);

		public static readonly IReadOnlyList<Profession> All = new[]
		{
			Miller,
			Baker,
			Weaver,
			Tailor,
			Hunter,
			Fisher,
			Tanner,
			Miner,
			Smith,
			Farmer,
			WoodCutter,
			Soldier,
			Bandit,
			Healer,
			Monk,
			Merchant,
			Clerk,
			Cook,
			Everyman,
		};

		#endregion
	}
}
