using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Log : Item
	{
		#region Properties

		public override Color Color
		{ get { return Color.Brown; } }

		public override double Weight
		{ get { return 0.5; } }

		public override ItemType Type
		{ get { return ItemType.Tool; } }

		#endregion

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Log;
		}
	}
}
