using System;
using System.Collections.Generic;

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

		protected override void HandleCellChanged(Cell oldCell, Cell newCell)
		{
			base.HandleCellChanged(oldCell, newCell);

			if (oldCell != null && newCell != null) // check of insert and remove Hero object
			{
				Camera.Refresh();
			}
		}

		public Hero(bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(sexIsMale, birthDate, properties, inventory, name)
		{
			Camera = new HeroCamera(this);
		}

		public override ActionResult Do()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = CurrentCell.Region.World.Game;
			game.Defeat();
			return base.Die(reason);
		}

		public override List<Interaction> GetAvailableInteractions(Object actor)
		{
			return new List<Interaction>(); // Because it is not possible to interact with yourself.
		}
	}
}
