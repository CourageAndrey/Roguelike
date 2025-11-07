using System.Collections.Generic;

namespace Roguelike.Core
{
	public abstract class Place
	{
		public ICollection<Cell> Cells
		{ get; }

		protected Place(IEnumerable<Cell> cells)
		{
			Cells = new HashSet<Cell>(cells);
		}
	}
}
