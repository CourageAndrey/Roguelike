using System;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Configuration;
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

		public override Color SkinColor
		{ get; }

		#endregion

		public Horse(Balance balance, bool sexIsMale, Time birthDate, Color skinColor)
			: base(balance, sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
		}

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
