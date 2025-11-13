using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Ownership : AspectWithHolder<IObject>
	{
		#region Properties

		public IObject Owner
		{ get; private set; }

		public event ValueChangedEventHandler<IObject, IObject>? OwnerChanged;

		#endregion

		public Ownership(IObject holder)
			: base(holder)
		{ }

		public void OwnBy(IObject newOwner)
		{
			var oldOwner = Owner;
			Owner = newOwner;

			var handler = Volatile.Read(ref OwnerChanged);
			if (handler != null)
			{
				handler(Holder, oldOwner, newOwner);
			}
		}
	}
}
