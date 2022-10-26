using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

	public static class DirectionHelper
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

		public static readonly IList<Direction> AllDirections = new ReadOnlyCollection<Direction>(Enum.GetValues(typeof(Direction)).OfType<Direction>().ToList());
	}
}
