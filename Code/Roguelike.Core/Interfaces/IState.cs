using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IState : IDescriptive
	{
		bool IsHungry
		{ get; }

		bool IsBloated
		{ get; }

		bool IsThirsty
		{ get; }

		bool IsTired
		{ get; }

		bool IsFallingAsleep
		{ get; }

		bool IsLosingBlood
		{ get; }

		bool IsConfused
		{ get; }

		bool IsFrozen
		{ get; }

		bool IsSunburned
		{ get; }

		bool IsFireBurned
		{ get; }

		bool IsAcidBurned
		{ get; }

		bool IsLightningBurned
		{ get; }

		bool IsScared
		{ get; }

		bool IsDrunk
		{ get; }

		bool HasHangover
		{ get; }

		bool IsPoisoned
		{ get; }

		bool IsDirty
		{ get; }

		bool IsSick
		{ get; }

		Activity Activity
		{ get; }

		event EventHandler<IState> Changed;
#warning Need to subscribe this event in order to track hero's state

		void SetActivity(Activity activity);

		void EatDrink(Nutricious nutricious, Language language);

		void PassTime(Time span, Language language);
	}
}
