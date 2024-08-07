namespace Roguelike.Core
{
	public abstract class CellEnvironment
	{
		#region Properties

		public abstract Weather Weather
		{ get; }

		#endregion
	}

	public class InteriorCellEnvironment : CellEnvironment
	{
		#region Properties

		public override Weather Weather
		{ get { return _weather; } }

		private readonly Weather _weather;

		#endregion

		public InteriorCellEnvironment(Weather weather)
		{
			_weather = weather;
		}
	}

	public class ExteriorCellEnvironment : CellEnvironment
	{
		#region Properties

		public override Weather Weather
		{ get { return _region.Weather; } }

		private readonly Region _region;

		#endregion

		public ExteriorCellEnvironment(Region region)
		{
			_region = region;
		}
	}
}
