namespace Roguelike.Core.Interfaces
{
	public interface IDoor : IObject
	{
		bool IsOpened
		{ get; }

		bool IsClosed
		{ get; }

		void Open();

		void Close();
	}
}
