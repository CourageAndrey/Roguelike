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

		public HashSet<Cell> MapMemory
		{ get; } = new HashSet<Cell>();

		public event Action<ICamera> Changed;

		protected override void HandleCellChanged(Cell from, Cell to)
		{
			if (from == null || to == null) return; // check of insert and remove Hero object

			RefreshCamera();
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

		public Hero(bool sexIsMale, Time age, Properties properties, IInventory inventory, string name)
			: base(sexIsMale, age, properties, inventory, name)
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
	}
}
