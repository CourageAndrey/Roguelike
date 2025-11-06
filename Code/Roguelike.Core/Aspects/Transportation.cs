using System;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Transport : IAspect
	{
		#region Properties

		private readonly IObject _holder;

		public IObject? Rider
		{
			get { return _rider; }
			set
			{
				if (_rider == null && value != null)
				{
					_rider = value;
					_rider.GetAspect<Rider>().Transport = this;
					_holder.MoveTo(null);
				}
				else if (_rider != null && value == null)
				{
					var riderCell = _rider.CurrentCell;
					var cells = riderCell.Region.GetCellsAroundPoint(riderCell.Position);
					cells.Remove(Direction.None);
					var horseCell = cells.Values.First(c => c.IsTransparent);

					_holder.MoveTo(horseCell);

					_rider.GetAspect<Rider>().Transport = null;
					_rider = null;
				}
				else
				{
					throw new InvalidOperationException("Impossible to change rider on horse.");
				}
			}
		}

		private IObject? _rider;

		#endregion

		public Transport(IObject holder)
		{
			_holder = holder;
		}
	}

	public class Rider : IAspect
	{
		#region Properties

		private readonly IObject _holder;

		public Transport? Transport
		{
			get { return _transport; }
			set
			{
				_transport = value;
				_holder.CurrentCell.RefreshView(false);
			}
		}

		private Transport? _transport;

		#endregion

		public Rider(IObject holder)
		{
			_holder = holder;
		}
	}

	public static class RidingHelper
	{
		public static ActionResult? Ride(this IObject rider, IObject transport)
		{
			var riderAspect = rider.TryGetAspect<Rider>();
			if (riderAspect == null) throw new InvalidOperationException("Object is not a rider.");

			if (riderAspect.Transport != null) throw new InvalidOperationException("Object has already ridden something or somebody.");

			var transportAspect = transport?.TryGetAspect<Transport>();
			if (transportAspect != null)
			{
				transportAspect.Rider = rider;

				var game = rider.GetGame();
				var language = game.Language;
				return new ActionResult(
					Time.FromTicks(game.World.Balance.Time, game.World.Balance.ActionLongevity.RideHorse),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.RideHorse, rider.GetDescription(language, game.Hero)));
			}
			else
			{
				return null;
			}
		}

		public static ActionResult? Unride(this IObject rider)
		{
			var riderAspect = rider.TryGetAspect<Rider>();
			if (riderAspect == null) throw new InvalidOperationException("Object is not a rider.");

			if (riderAspect.Transport == null) throw new InvalidOperationException("Object doesn't ride anything or anybody.");

			var riderCell = rider.CurrentCell;
			var cellsAround = riderCell.Region.GetCellsAroundPoint(riderCell.Position);
			cellsAround.Remove(Direction.None);

			if (cellsAround.Any(cell => cell.Value.IsTransparent))
			{
				riderAspect.Transport.Rider = null;
				riderAspect.Transport = null;

				var game = rider.GetGame();
				var language = game.Language;
				return new ActionResult(
					Time.FromTicks(game.World.Balance.Time, game.World.Balance.ActionLongevity.RideHorse),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.RideHorse, rider.GetDescription(language, game.Hero)));
			}
			else
			{
				return null;
			}
		}
	}
}
