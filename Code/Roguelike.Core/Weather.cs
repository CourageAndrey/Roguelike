using System;
using System.Threading;

namespace Roguelike.Core
{
	public class Weather
	{
		#region Properties

		public Region Region
		{ get; }

		public double Temperature
		{ get; private set; }

		public Direction WindDirection
		{ get; private set; }

		public double WindSpeed
		{ get; private set; }

		public double VisibilityBonus
		{ get; private set; }

		public bool Precipitation
		{ get; private set; }

		public Time NextChangeTime
		{ get; private set; }
#warning Weather also has to be updated with time.

		public event EventHandler Changed;
#warning Subscribe on weather change.

		#endregion

		public Weather(Region region)
		{
			Region = region;
			Change();
		}

		internal void Change()
		{
#warning Change weather in some way.
			Temperature = 20;
			WindDirection = Direction.None;
			WindSpeed = 0;
			VisibilityBonus = 1;
			Precipitation = false;

			var seed = new Random(DateTime.Now.Millisecond);
			var balance = Region.World.Game.Balance;
			NextChangeTime = Region.World.Time.AddTicks(seed.Next((int)(balance.TicksInDay / 48), (int)(balance.TicksInDay * 3)));

			Volatile.Read(ref Changed)?.Invoke(this, EventArgs.Empty);
		}
	}
}
