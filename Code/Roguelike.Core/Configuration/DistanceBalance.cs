namespace Roguelike.Core.Configuration
{
	public class DistanceBalance
	{
		#region Properties

		public int AiRange
		{ get; set; }

		public int HeroInitialView
		{ get; set; }

		#endregion

		public static DistanceBalance CreateDefault()
		{
			return new DistanceBalance
			{
				AiRange = 30,
				HeroInitialView = 15,
			};
		}
	}
}
