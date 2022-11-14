using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public abstract class Object : IObject, IHasOwner
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

			CurrentCell.RefreshView(true);
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

		public abstract string GetDescription(Language language, IAlive forWhom);

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
				var camera = CurrentCell.Region.World?.Hero?.Camera;
				bool visibleDestination; // we need to track this in order not to refresh screen twice on object move
				bool refreshOnRemove =	!IsSolid ||
										cell != null && (
										camera?.VisibleCells.TryGetValue(cell, out visibleDestination) != true ||
										!visibleDestination);
				CurrentCell.RemoveObject(this, refreshOnRemove);
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
