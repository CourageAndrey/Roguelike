using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Roguelike.Core.Localization;

namespace Roguelike.Core
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

		/// <summary>
		/// Gets the opposite direction.
		/// </summary>
		public static Direction GetOpposite(this Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
					return Direction.Right;
				case Direction.Right:
					return Direction.Left;
				case Direction.Up:
					return Direction.Down;
				case Direction.Down:
					return Direction.Up;
				case Direction.UpLeft:
					return Direction.DownRight;
				case Direction.UpRight:
					return Direction.DownLeft;
				case Direction.DownLeft:
					return Direction.UpRight;
				case Direction.DownRight:
					return Direction.UpLeft;
				case Direction.None:
					return Direction.None;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction));
			}
		}

		/// <summary>
		/// Converts direction to a unit vector for wind calculations.
		/// Returns (dx, dy) where positive X is right, positive Y is down.
		/// </summary>
		public static (int dx, int dy) ToVector(this Direction direction)
		{
			int dx = 0;
			int dy = 0;

			if ((direction & Direction.Left) != 0)
				dx = -1;
			if ((direction & Direction.Right) != 0)
				dx = 1;
			if ((direction & Direction.Up) != 0)
				dy = -1;
			if ((direction & Direction.Down) != 0)
				dy = 1;

			return (dx, dy);
		}

		/// <summary>
		/// Creates a direction from a vector.
		/// </summary>
		public static Direction FromVector(int dx, int dy)
		{
			Direction result = Direction.None;

			if (dx < 0)
				result |= Direction.Left;
			else if (dx > 0)
				result |= Direction.Right;

			if (dy < 0)
				result |= Direction.Up;
			else if (dy > 0)
				result |= Direction.Down;

			return result;
		}
	}
}
