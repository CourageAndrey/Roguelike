using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Properties : IAspect
	{
		#region Properties

		public int Strength
		{ get; }

		public int Endurance
		{ get; }

		public int Reaction
		{ get; }

		public int Perception
		{ get; }

		public int Intelligence
		{ get; }

		public int Willpower
		{ get; }

		public int Luck
		{ get; }

		#endregion

		public Properties(
			int strength,
			int endurance,
			int reaction,
			int perception,
			int intelligence,
			int willpower,
			int luck)
		{
			Strength = strength;
			Endurance = endurance;
			Reaction = reaction;
			Perception = perception;
			Intelligence = intelligence;
			Willpower = willpower;
			Luck = luck;
		}

		public override string ToString()
		{
			return $"STR={Strength} END={Endurance} REA={Reaction} PER={Perception} INT={Intelligence} WIL={Willpower}";
		}

		public static Properties Empty()
		{
			return new Properties(0, 0, 0, 0, 0, 0, 0);
		}

		public Properties Merge(Properties? other)
		{
			if (other == null)
			{
				other = Empty();
			}

			return new Properties(
				Strength + other.Strength,
				Endurance + other.Endurance,
				Reaction + other.Reaction,
				Perception + other.Perception,
				Intelligence + other.Intelligence,
				Willpower + other.Willpower,
				Luck + other.Luck);
		}
	}
}
