using System.Collections.ObjectModel;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Mechanics
{
	[Flags]
	public enum Direction
	{
		None = 0,
		Left = 1,
		Right = 2,
		Up = 4,
		Down = 8,
		UpLeft = Up | Left,
		UpRight = Up | Right,
		DownLeft = Down | Left,
		DownRight = Down | Right,
	}

	public static class Directions
	{
		public static string GetName(this Direction direction, LanguageDirections language)
		{
			switch (direction)
			{
				case Direction.None:
					return language.None;
				case Direction.Left:
					return language.Left;
				case Direction.Right:
					return language.Right;
				case Direction.Up:
					return language.Up;
				case Direction.Down:
					return language.Down;
				case Direction.UpLeft:
					return language.UpLeft;
				case Direction.UpRight:
					return language.UpRight;
				case Direction.DownLeft:
					return language.DownLeft;
				case Direction.DownRight:
					return language.DownRight;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}
		}

		public static readonly IReadOnlyList<Direction> AllPossible = new ReadOnlyCollection<Direction>(new[]
		{
			Direction.None,
			Direction.Left,
			Direction.Right,
			Direction.Up,
			Direction.Down,
			Direction.UpLeft,
			Direction.UpRight,
			Direction.DownLeft,
			Direction.DownRight,
		});

		public static readonly IReadOnlyList<Direction> All8 = new ReadOnlyCollection<Direction>(new[]
		{
			Direction.Left,
			Direction.Right,
			Direction.Up,
			Direction.Down,
			Direction.UpLeft,
			Direction.UpRight,
			Direction.DownLeft,
			Direction.DownRight,
		});

		public static readonly IReadOnlyList<Direction> All4 = new ReadOnlyCollection<Direction>(new[]
		{
			Direction.Left,
			Direction.Right,
			Direction.Up,
			Direction.Down,
		});

		public static Direction GetRandom(this IReadOnlyList<Direction> list, Random seed)
		{
			return list[seed.Next(0, list.Count - 1)];
		}
	}
}
