namespace Roguelike.Core.Configuration
{
	public class DressTimeBalance
	{
		#region Properties

		public int HeadWear
		{ get; set; }

		public int UpperBodyWear
		{ get; set; }

		public int LowerBodyWear
		{ get; set; }

		public int CoverWear
		{ get; set; }

		public int HandsWear
		{ get; set; }

		public int FootsWear
		{ get; set; }

		public int Jewelry
		{ get; set; }

		#endregion

		public static DressTimeBalance CreateDefault()
		{
			return new DressTimeBalance
			{
				HeadWear = 2 * 1000,
				UpperBodyWear = 2 * 60 * 1000,
				LowerBodyWear = 45 * 1000,
				CoverWear = 30 * 1000,
				HandsWear = 20 * 1000,
				FootsWear = 60 * 1000,
				Jewelry = 15 * 1000,
			};
		}
	}
}
