namespace Roguelike.Core.Configuration
{
	public class WorldSizeBalance
	{
		#region Properties

		public int RegionsCount
		{ get; set; }

		public int RegionXdimension
		{ get; set; }

		public int RegionYdimension
		{ get; set; }

		public int RegionZdimension
		{ get; set; }

		#endregion

		public static WorldSizeBalance CreateDefault()
		{
			return new WorldSizeBalance
			{
				RegionsCount = 1,
				RegionXdimension = 128,
				RegionYdimension = 64,
				RegionZdimension = 1,
			};
		}
	}
}
