﻿using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Skirt : Wear, ILowerBodyWear
	{
		#region Properties

		public override double Weight
		{ get { return 1; } }

		#endregion

		public Skirt(Color clothColor)
			: base(clothColor)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Skirt;
		}
	}
}
