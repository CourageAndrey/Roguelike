using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Ownership : IAspect
	{
		#region Properties

		private readonly IObject _holder;

		public IObject Owner
		{ get; private set; }

		public event ValueChangedEventHandler<IObject, IObject> OwnerChanged;

		#endregion

		public Ownership(IObject holder)
		{
			_holder = holder;
		}

		public void OwnBy(IObject newOwner)
		{
			var oldOwner = Owner;
			Owner = newOwner;

			var handler = Volatile.Read(ref OwnerChanged);
			if (handler != null)
			{
				handler(_holder, oldOwner, newOwner);
			}
		}
	}
}
