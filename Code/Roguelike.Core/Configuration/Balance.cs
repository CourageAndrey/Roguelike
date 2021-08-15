namespace Roguelike.Core.Configuration
{
	public class Balance
	{
		#region Properties

		#region Action longevities

		public int ActionLongevityNull
		{ get; set; }

		public int ActionLongevityStep
		{ get; set; }

		public int ActionLongevityDisabled
		{ get; set; }

		public int ActionLongevityOpenCloseDoor
		{ get; set; }

		public int ActionLongevityChopTree
		{ get; set; }

		public int ActionLongevityChangeWeapon
		{ get; set; }

		public int ActionLongevityWait
		{ get; set; }

		#endregion

		#region Time settings

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

		public int MaxLogSize
		{ get; set; }

		#region World size

		public int DefaultRegionsCount
		{ get; set; }

		public int DefaultRegionXdimension
		{ get; set; }

		public int DefaultRegionYdimension
		{ get; set; }

		public int DefaultRegionZdimension
		{ get; set; }

		#endregion

		#region Distances

		public int AiDistance
		{ get; set; }

		public int HeroInitialViewDistance
		{ get; set; }

		#endregion

		#endregion

		public static Balance CreateDefault()
		{
			return new Balance
			{
				ActionLongevityNull = 0,
				ActionLongevityStep = 1000,
				ActionLongevityDisabled = 1000,
				ActionLongevityOpenCloseDoor = 2000,
				ActionLongevityChopTree = 24 * 60 * 60 * 1000,
				ActionLongevityChangeWeapon = 1000,
				ActionLongevityWait = 500,

				BeginYear = 1400,
				MonthInYear = 12,
				SeasonCount = 4,
				WeeksInMonth = 4,
				DaysInWeek = 7,
				TicksInDay = 24 * 60 * 60 * 1000,
				DaytimeCount = 4,

				MaxLogSize = 512,

				DefaultRegionsCount = 1,
				DefaultRegionXdimension = 128,
				DefaultRegionYdimension = 64,
				DefaultRegionZdimension = 1,

				AiDistance = 30,
				HeroInitialViewDistance = 15,
			};
		}
	}
}
