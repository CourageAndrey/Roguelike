namespace Roguelike.Core.Interfaces
{
	public interface IFood : IItem
	{
		int Nutricity
		{ get; }

		int Water
		{ get; }
	}
}
