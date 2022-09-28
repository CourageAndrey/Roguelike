using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Properties : IProperties
	{
		#region Properties

		public uint Strength
		{ get; }

		public uint Endurance
		{ get; }

		public uint Reaction
		{ get; }

		public uint Perception
		{ get; }

		public uint Intelligence
		{ get; }

		public uint Willpower
		{ get; }

		#endregion

		public Properties(
			uint strength,
			uint endurance,
			uint reaction,
			uint perception,
			uint intelligence,
			uint willpower)
		{
			Strength = strength;
			Endurance = endurance;
			Reaction = reaction;
			Perception = perception;
			Intelligence = intelligence;
			Willpower = willpower;
		}
	}
}
