namespace Roguelike.Core.Chat
{
	public class Attitude
	{
		private readonly string _description = "-";

		public static readonly Attitude Neutral = new Attitude();

		public override string ToString()
		{
			return _description;
		}
	}
}
