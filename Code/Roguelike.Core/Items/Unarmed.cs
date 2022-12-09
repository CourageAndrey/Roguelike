﻿using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Unarmed : Item
	{
		#region Properties

		public IAlive Fighter
		{ get; }

		#endregion

		public Unarmed(IAlive fighter)
			: base(
				(language, alive) => language.Items.Unarmed,
				() => 0,
				ItemType.Weapon,
				default(Color),
				Material.Skin,
				new Weapon(false))
		{
			Fighter = fighter;
		}
	}
}
