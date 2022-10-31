using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Trousers : Wear, ILowerBodyWear
	{
		#region Properties

		public override decimal Weight
		{ get { return 1; } }

		#endregion

		public Trousers(Color clothColor)
			: base(clothColor)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Trousers;
		}
	}
}
