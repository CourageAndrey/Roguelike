namespace Roguelike.Core.Interfaces
{
	interface IHasOwner
	{
		IObject Owner
		{ get; }

		event ValueChangedEventHandler<IObject, IObject> OwnerChanged;
	}
}
