using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Core;
using Roguelike.Core.Objects;
using Roguelike.Console.ViewModels;

namespace Roguelike.Console
{
	internal static class ConsoleUiHelper
	{
		#region Models

		public static ObjectViewModel GetModel(this Cell cell)
		{
			var objectToDisplay = cell.GetTopVisibleObject();
			if (objectToDisplay == null) return BackgroundViewModel.All[cell.Background];

			Func<Core.Object, ObjectViewModel> modelCreator = null;
			var objectType = objectToDisplay.GetType();
			while (objectType != null)
			{
				if (modelCreators.TryGetValue(objectType, out modelCreator))
				{
					break;
				}
				else
				{
					objectType = objectType.BaseType;
				}
			}
			if (modelCreator != null)
			{
				return modelCreator(objectToDisplay);
			}
			else
			{
				throw new NotSupportedException("Impossible to display object of type " + objectToDisplay.GetType().FullName);
			}
		}

		private static readonly IDictionary<Type, Func<Core.Object, ObjectViewModel>> modelCreators = new Dictionary<Type, Func<Core.Object, ObjectViewModel>>
		{
			{ typeof(Humanoid), o => new HumanoidViewModel((Humanoid) o) },
			{ typeof(Dog), o => new DogViewModel((Dog) o) },
			{ typeof(Horse), o => new HorseViewModel((Horse) o) },
			{ typeof(Wall), o => new WallViewModel((Wall) o) },
			{ typeof(Door), o => new DoorViewModel((Door) o) },
			{ typeof(Pool), o => new PoolViewModel((Pool) o) },
			{ typeof(Tree), o => new TreeViewModel((Tree) o) },
			{ typeof(Fire), o => new FireViewModel((Fire) o) },
			{ typeof(Bed), o => new BedViewModel((Bed) o) },
			{ typeof(Corpse), o => new CorpseViewModel((Corpse) o) },
			{ typeof(Stump), o => new StumpViewModel((Stump) o) },
			{ typeof(ItemsPile), o => new ItemsPileViewModel((ItemsPile) o) },
		};

		public const string Aim = "+";

		#endregion

		public static ConsoleColor ToGrayScale(this ConsoleColor color)
		{
			switch (color)
			{
				case ConsoleColor.DarkBlue:
				case ConsoleColor.DarkGreen:
				case ConsoleColor.DarkCyan:
				case ConsoleColor.DarkRed:
				case ConsoleColor.DarkMagenta:
				case ConsoleColor.DarkYellow:
					return ConsoleColor.DarkGray;
				case ConsoleColor.Blue:
				case ConsoleColor.Green:
				case ConsoleColor.Cyan:
				case ConsoleColor.Red:
				case ConsoleColor.Magenta:
					return ConsoleColor.Gray;
				case ConsoleColor.Yellow:
					return ConsoleColor.White;
				// case ConsoleColor.Black:
				// case ConsoleColor.White:
				// case ConsoleColor.Gray:
				// case ConsoleColor.DarkGray:
				default:
					return color;
			}
		}

		public static ConsoleColor ToConsole(this Color color)
		{
			int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; // Bright bit
			index |= (color.R > 64) ? 4 : 0; // Red bit
			index |= (color.G > 64) ? 2 : 0; // Green bit
			index |= (color.B > 64) ? 1 : 0; // Blue bit
			return (ConsoleColor) index;
		}

		public static string GetMissile(this Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
				case Direction.Right:
					return "-";
				case Direction.Up:
				case Direction.Down:
					return "|";
				case Direction.UpLeft:
				case Direction.DownRight:
					return "\\";
				case Direction.UpRight:
				case Direction.DownLeft:
					return "/";
				default:
					return null;
			}
		}
	}
}
