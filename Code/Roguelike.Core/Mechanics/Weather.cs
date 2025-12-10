namespace Roguelike.Core.Mechanics
{
	public enum Precipitation
	{
		No,
		Rain,
		Storm,
		Snow,
		Hail,
		Dust,
	}

	public class Weather
	{
		#region Properties

		public Region Region
		{ get; }

		public double Temperature
		{ get; private set; } // C degrees

		public Direction WindDirection
		{ get; private set; }

		public double WindSpeed
		{ get; private set; } // cells of arrow shift

		public double VisibilityBonus
		{ get; private set; } // % of visibility range

		public Precipitation Precipitation
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
			Precipitation = Precipitation.No;

			var seed = new Random(DateTime.Now.Millisecond);
			var balance = Region.World.Time.Balance;
			NextChangeTime = Region.World.Time.AddTicks(seed.Next((int)(balance.TicksInDay / 48), (int)(balance.TicksInDay * 3)));

			Volatile.Read(ref Changed)?.Invoke(this, EventArgs.Empty);
		}
	}
}
