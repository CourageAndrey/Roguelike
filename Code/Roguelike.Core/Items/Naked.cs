using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Naked : Item
	{
		#region Properties

		public IHumanoid Owner
		{ get; }

		#endregion

		public Naked(IHumanoid owner)
			: base(
				(language, alive) => string.Empty,
				() => 0,
				ItemType.Wear,
				owner.SkinColor,
				Material.Skin,
				new Wear(default(WearSlot)))
		{
			Owner = owner;
		}
	}
}
