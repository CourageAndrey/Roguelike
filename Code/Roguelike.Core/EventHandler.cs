namespace Roguelike.Core
{
	public delegate void EventHandler<in SenderT, in EventArgsT>(SenderT sender, EventArgsT args);
}
