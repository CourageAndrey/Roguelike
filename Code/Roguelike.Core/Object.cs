using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public class Object : IHasOwner
	{
		#region Properties

		public Cell CurrentCell
		{ get; private set; }

		#region Physical properties

		/// <summary>
		/// Is this possible to go through this object.
		/// </summary>
		public virtual bool IsSolid
		{ get { return true; } }

		#endregion

		#region Ownership

		public Object Owner
		{ get; internal set; }

		#endregion

		#endregion

		#region Movement

		public bool TryMoveTo(Cell cell)
		{
			if (!IsSolid || cell.Objects.All(o => !o.IsSolid))
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

		protected virtual void HandleCellChanged(Cell from, Cell to)
		{ }

		#endregion
	}
}
