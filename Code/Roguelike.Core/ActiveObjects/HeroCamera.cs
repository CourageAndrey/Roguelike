using System.Collections.Generic;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class HeroCamera : ICamera
	{
		#region Properties

		private readonly Hero _hero;

		public Cell Cell
		{ get { return _hero.CurrentCell; } }

		public double Distance
		{
			get
			{
#warning Implement visibility length depending of hero senses.
				return 10;
			}
		}

		public ICollection<Cell> MapMemory
		{ get; } = new HashSet<Cell>();

		public event EventHandler<ICamera> Changed;

		public void Refresh()
		{
			var handler = Volatile.Read(ref Changed);
			if (handler != null)
			{
				handler(this);
			}
		}

		#endregion

		public HeroCamera(Hero hero)
		{
			_hero = hero;
		}
	}
}
