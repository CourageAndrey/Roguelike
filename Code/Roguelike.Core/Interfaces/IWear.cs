namespace Roguelike.Core.Interfaces
{
	public interface IWear : IItem
	{
		WearSlot SuitableSlot
		{ get; }

		event EventHandler<IWear, IAlive> Equipped;

		event EventHandler<IWear, IAlive> Unequipped;

		void RaiseEquipped(IAlive who);

		void RaiseUnequipped(IAlive who);
	}

	public enum WearSlot
	{
		Head,
		UpperBody,
		LowerBody,
		Cover,
		Hands,
		Foots,
		Jewelry,
	}
}