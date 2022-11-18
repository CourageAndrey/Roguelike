using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public class Hero : Humanoid, IHero
	{
		#region Properties

		public ICamera Camera
		{ get; }

		#endregion

		public Hero(Balance balance, Race race, bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name, IObjectAspect[] aspects = null)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name, aspects)
		{
			Camera = new HeroCamera(this);
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = CurrentCell.Region.World.Game;
			game.Defeat();
			return base.Die(reason);
		}
	}
}
