using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Activity
	{
		private readonly Func<LanguageActivities, string> _getDescription;

		private Activity(Func<LanguageActivities, string> getDescription)
		{
			if (getDescription != null)
			{
				_getDescription = getDescription;
			}
			else
			{
				throw new ArgumentNullException(nameof(getDescription));
			}
		}

		public string GetDescription(LanguageActivities language)
		{
			return _getDescription(language);
		}

		#region List

		public static readonly Activity Stands = new Activity(language => language.Stands);
		public static readonly Activity Guards = new Activity(language => language.Guards);
		public static readonly Activity Walks = new Activity(language => language.Walks);
		public static readonly Activity Fights = new Activity(language => language.Fights);
		public static readonly Activity ChopsTree = new Activity(language => language.ChopsTree);
		public static readonly Activity Dresses = new Activity(language => language.Dresses);

		#endregion
	}
}
