namespace Roguelike.Core.Configuration
{
	public class TimeBalance
	{
		#region Properties

		public int BeginYear
		{ get; set; }

		public int MonthInYear
		{ get; set; }

		public int SeasonCount
		{ get; set; }

		public int WeeksInMonth
		{ get; set; }

		public int DaysInWeek
		{ get; set; }

		public uint TicksInDay
		{ get; set; }

		public int DaytimeCount
		{ get; set; }

		#endregion

		public static TimeBalance CreateDefault()
		{
			return new TimeBalance
			{
				BeginYear = 1400,
				MonthInYear = 12,
				SeasonCount = 4,
				WeeksInMonth = 4,
				DaysInWeek = 7,
				TicksInDay = 24 * 60 * 60 * 1000,
				DaytimeCount = 4,
			};
		}
	}
}
