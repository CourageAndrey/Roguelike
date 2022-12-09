using System;
using System.Collections.Generic;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Objects
{
	public class Hero : Humanoid, IHero
	{
		#region Properties

		public Camera Camera
		{ get { return this.GetAspect<Camera>(); } }

		#endregion

		public Hero(Balance balance, Race race, bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory, string name)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name)
		{
			AddAspects(new Camera(this, () => Properties.Perception));
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = this.GetGame();
			game.Defeat();
			return base.Die(reason);
		}
	}
}
