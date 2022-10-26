using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Horse : Alive, ITransport
	{
		#region Properties

		public IHumanoid Rider
		{
			get { return _rider; }
			set
			{
				if (_rider == null && value != null)
				{
					_rider = value;
					_rider.Transport = this;
					MoveTo(null);
				}
				else if (_rider != null && value == null)
				{
					var riderCell = _rider.CurrentCell;
					var cells = riderCell.Region.GetCellsAroundPoint(riderCell.Position);
					cells.Remove(Direction.None);
					var horseCell = cells.Values.First(c => c.IsTransparent);

					MoveTo(horseCell);

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

		public Horse(bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory)
			: base(sexIsMale, birthDate, properties, inventory)
		{ }

		protected override ActionResult DoImplementation()
		{
#warning Horse is too passive.
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			return new ActionResult(Time.FromYears(balance.Time, 1), string.Empty);
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateAnimal();
		}
	}
}
