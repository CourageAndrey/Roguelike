﻿using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Ring : Wear, IJewelry
	{
		#region Properties

		public override decimal Weight
		{ get { return 0.02m; } }

		public override ItemType Type
		{ get { return ItemType.Wear; } }

		public override Material Material
		{ get { return Material.Metal; } }

		#endregion

		public Ring()
			: base(Color.Aquamarine)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Ring;
		}
	}
}