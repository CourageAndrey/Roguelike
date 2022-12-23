using System;
using System.Collections.Generic;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Haircut
	{
		private readonly Func<LanguageHaircuts, string> _getName;

		private Haircut(Func<LanguageHaircuts, string> getName)
		{
			_getName = getName;
		}

		public string GetName(LanguageHaircuts language)
		{
			return _getName.Invoke(language);
		}

		#region List

		public static readonly Haircut Bald = new Haircut(l => l.Bald);
		public static readonly Haircut LongHairs = new Haircut(l => l.LongHairs);
		public static readonly Haircut ShortHairs = new Haircut(l => l.ShortHairs);

		public static readonly IReadOnlyCollection<Haircut> All = new[]
		{
			Bald,
			LongHairs,
			ShortHairs,
		};

		#endregion
	}
}
