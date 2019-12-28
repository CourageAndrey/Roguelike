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
		public static string GetName(this Direction direction, Language language)
		{
			switch (direction)
			{
				case Direction.None:
					return language.DirectionNone;
				case Direction.Left:
					return language.DirectionLeft;
				case Direction.Right:
					return language.DirectionRight;
				case Direction.Up:
					return language.DirectionUp;
				case Direction.Down:
					return language.DirectionDown;
				case Direction.UpLeft:
					return language.DirectionUpLeft;
				case Direction.UpRight:
					return language.DirectionUpRight;
				case Direction.DownLeft:
					return language.DirectionDownLeft;
				case Direction.DownRight:
					return language.DirectionDownRight;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}
		}

		public static readonly IList<Direction> AllDirections = new ReadOnlyCollection<Direction>(Enum.GetValues(typeof(Direction)).OfType<Direction>().ToList());
	}
}
