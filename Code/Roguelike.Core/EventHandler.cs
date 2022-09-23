namespace Roguelike.Core
{
	public delegate void EventHandler<in SenderT>(SenderT sender);

	public delegate void EventHandler<in SenderT, in EventArgsT>(SenderT sender, EventArgsT args);

	public delegate void ValueChangedEventHandler<in SenderT, in ValueT>(SenderT sender, ValueT oldValue, ValueT newValue);
}
