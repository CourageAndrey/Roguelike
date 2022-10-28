using System;
using System.Drawing;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	internal class Book : Item, IPaper
	{
		#region Properties

		public override double Weight
		{ get { return 1; } }

		public override ItemType Type
		{ get { return ItemType.Paper; } }

		public override Color Color
		{ get; }

		private readonly Func<LanguageBooks, string> _getTitle;
		private readonly Func<LanguageBooks, string> _getText;

		public Book(Color coverColor, Func<LanguageBooks, string> getTitle, Func<LanguageBooks, string> getText)
		{
			_getTitle = getTitle;
			_getText = getText;
		}

		#endregion

		public string GetTitle(LanguageBooks language)
		{
			return _getTitle(language);
		}

		public string GetText(LanguageBooks language)
		{
			return _getText(language);
		}
	}
}
