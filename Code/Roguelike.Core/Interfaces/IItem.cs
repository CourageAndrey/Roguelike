namespace Roguelike.Core.Interfaces
{
	public interface IItem : IMassy, IDescriptive, IAspectHolder, IColorful
	{
		ItemType Type
		{ get; }

		Material Material
		{ get; }

		event EventHandler<IItem, IAlive> Picked;

		event EventHandler<IItem, IAlive> Dropped;

		void RaisePicked(IAlive who);

		void RaiseDropped(IAlive who);
	}
}
