using System;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Objects
{
	public class Transport : IObjectAspect
	{
		#region Properties

		public IObject Object
		{ get; }

		public IHumanoid Rider
		{
			get { return _rider; }
			set
			{
				if (_rider == null && value != null)
				{
					_rider = value;
					_rider.Transport = this;
					Object.MoveTo(null);
				}
				else if (_rider != null && value == null)
				{
					var riderCell = _rider.CurrentCell;
					var cells = riderCell.Region.GetCellsAroundPoint(riderCell.Position);
					cells.Remove(Direction.None);
					var horseCell = cells.Values.First(c => c.IsTransparent);

					Object.MoveTo(horseCell);

					_rider.Transport = null;
					_rider = null;
				}
				else
				{
					throw new InvalidOperationException("Impossible to change rider on horse.");
				}
			}
		}

		private IHumanoid _rider;

		#endregion

		public Transport(IObject o)
		{
			Object = o;
		}
	}
}
