using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public class Object : IObject, IHasOwner
	{
		#region Properties

		public Cell CurrentCell
		{ get; private set; }

		public event ValueChangedEventHandler<IObject, Cell> CellChanged;

		#region Physical properties

		/// <summary>
		/// Is this possible to go through this object.
		/// </summary>
		public virtual bool IsSolid
		{ get { return true; } }

		public event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		protected void RaiseIsSolidChanged(bool oldSolid, bool newSolid)
		{
			var handler = Volatile.Read(ref IsSolidChanged);
			if (handler != null)
			{
				handler(this, oldSolid, newSolid);
			}
		}

		#endregion

		#region Ownership

		public IObject Owner
		{ get; internal set; }

		public event ValueChangedEventHandler<IObject, IObject> OwnerChanged;

		protected void RaiseOwnerChanged(IObject oldOwner, IObject newOwner)
		{
			var handler = Volatile.Read(ref OwnerChanged);
			if (handler != null)
			{
				handler(this, oldOwner, newOwner);
			}
		}

		#endregion

		#endregion

		#region Movement

		public bool TryMoveTo(Cell cell)
		{
			if (!IsSolid || cell.IsTransparent)
			{
				MoveTo(cell);
				return true;
			}
			else
			{
				return false;
			}
		}

		internal void MoveTo(Cell cell)
		{
			var oldCell = CurrentCell;
			if (CurrentCell != null)
			{
				CurrentCell.RemoveObject(this);
			}
			if (cell != null)
			{
				CurrentCell = cell;
				CurrentCell.AddObject(this);
			}
			else
			{
				CurrentCell = null;
			}
			HandleCellChanged(oldCell, cell);
		}

		protected virtual void HandleCellChanged(Cell oldCell, Cell newCell)
		{
			var handler = Volatile.Read(ref CellChanged);
			if (handler != null)
			{
				handler(this, oldCell, newCell);
			}
		}

		#endregion
	}
}
