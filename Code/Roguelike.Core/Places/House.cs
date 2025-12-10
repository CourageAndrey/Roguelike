using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Places
{
	public class House : Place, IAspectHolder
	{
		public IReadOnlyCollection<IAspect> Aspects
		{ get { return _aspects; } }

		private readonly Door _door;

		private readonly List<IAspect> _aspects;

		public House(IEnumerable<Cell> cells)
			: base(cells)
		{
			_aspects = new List<IAspect>();
			_door = cells.SelectMany(cell => cell.Objects.OfType<Door>()).Single();
		}

		public void Settle(params IAlive[] owners)
		{
			foreach (var owner in owners)
			{
				var ownership = new Ownership(_door);
				ownership.OwnBy(owner);
				_aspects.Add(ownership);
			}
		}
	}
}