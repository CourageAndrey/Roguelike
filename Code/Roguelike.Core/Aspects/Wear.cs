using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Wear : IAspect
	{
		public WearSlot SuitableSlot
		{ get; }

		public event EventHandler<Wear, IAlive>? Equipped;

		public event EventHandler<Wear, IAlive>? Unequipped;

		public Wear(WearSlot suitableSlot)
		{
			SuitableSlot = suitableSlot;
		}

		public void RaiseEquipped(IAlive who)
		{
			var handler = Volatile.Read(ref Equipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		public void RaiseUnequipped(IAlive who)
		{
			var handler = Volatile.Read(ref Unequipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}
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