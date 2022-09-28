using System;
using System.Collections.Generic;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public class Hero : Humanoid, ICamera
	{
		#region Properties



		#endregion

		#region Implementation of ICamera

		Cell ICamera.Cell
		{ get { return CurrentCell; } }

		public double Distance
		{
			get
			{
#warning Implement visibility length depending of senses.
				return 10;
			}
		}

		public ICollection<Cell> MapMemory
		{ get; } = new HashSet<Cell>();

		public event EventHandler<ICamera> Changed;

		protected override void HandleCellChanged(Cell oldCell, Cell newCell)
		{
			base.HandleCellChanged(oldCell, newCell);

			if (oldCell != null && newCell != null) // check of insert and remove Hero object
			{
				RefreshCamera();
			}
		}

		public void RefreshCamera()
		{
			var handler = Volatile.Read(ref Changed);
			if (handler != null)
			{
				handler(this);
			}
		}

		#endregion

		public Hero(bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(sexIsMale, birthDate, properties, inventory, name)
		{ }

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
