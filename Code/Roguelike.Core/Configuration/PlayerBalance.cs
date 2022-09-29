namespace Roguelike.Core.Configuration
{
	public class PlayerBalance
	{
		#region Properties

		public int BaseHitPossibility
		{ get; set; }

		public int ReflexesAverage
		{ get; set; }

		public double ReflexesRate
		{ get; set; }

		#endregion

		public static PlayerBalance CreateDefault()
		{
			return new PlayerBalance
			{
				BaseHitPossibility = 50,
				ReflexesAverage = 30,
				ReflexesRate = 0.015,
			};
		}
	}
}
