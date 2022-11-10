using System.Drawing;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public abstract class Wear : Item, IWear
	{
		#region Properties

		public abstract WearSlot SuitableSlot
		{ get; }

		public override ItemType Type
		{ get { return ItemType.Wear; } }

		public override Color Color
		{ get; }

		public event EventHandler<IWear, IAlive> Equipped;

		public event EventHandler<IWear, IAlive> Unequipped;

		#endregion

		protected Wear(Color clothColor)
		{
			Color = clothColor;
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
}
