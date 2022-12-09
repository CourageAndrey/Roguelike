using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Items;

namespace Roguelike.Core.Interfaces
{
	public interface IObject : IDescriptive, IAspectHolder
	{
		Cell CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		void MoveTo(Cell cell);

		event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		event ValueChangedEventHandler<IObject, Cell> CellChanged;

		void WriteToLog(ICollection<string> messages);

		event EventHandler<IObject, ICollection<string>> OnLogMessage;
	}

	public static class ObjectExtensions
	{
		public static Vector GetPosition(this IObject obj)
		{
			return obj.CurrentCell?.Position;
		}

		public static Region GetRegion(this IObject obj)
		{
			return obj.CurrentCell?.Region;
		}

		public static World GetWorld(this IObject obj)
		{
			return obj.GetRegion()?.World;
		}

		public static Game GetGame(this IObject obj)
		{
			return obj.GetWorld()?.Game;
		}

		public static Hero GetHero(this IObject obj)
		{
			return obj.GetWorld()?.Hero;
		}

		internal static void WriteToLog(this IObject obj, string message)
		{
			obj.WriteToLog(new[] { message });
		}

		public static ActionResult TryMove(this IObject obj, Direction direction)
		{
			var world = obj.GetWorld();
			var game = world.Game;
			var balance = game.Balance;

			var oldPosition = obj.GetPosition();
			var newCell = oldPosition != null
				? obj.GetRegion().GetCell(oldPosition.GetNeighboor(direction))
				: null;
			if (newCell != null)
			{
				double distance = newCell.Position.GetDistance(oldPosition);
				var language = game.Language.LogActionFormats;

				var alive = obj as IAlive;
				IAlive target = null;
				if (alive?.IsAgressive == true && alive?.WeaponToFight?.GetAspect<Weapon>()?.IsRange == false && (target = newCell.Objects.OfType<IAlive>().FirstOrDefault()) != null)
				{
					return alive.Attack(target);
				}
				else
				{
					Activity newActivity = (obj as IAlive)?.IsAgressive == false
						? Activity.Walks
						: null;

					if (!obj.IsSolid || newCell.IsTransparent)
					{
						obj.MoveTo(newCell);

						return new ActionResult(
							Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Step * distance)),
							string.Format(CultureInfo.InvariantCulture, language.Move, obj.GetDescription(game.Language, game.Hero), oldPosition, newCell.Position),
							newActivity);
					}
					else
					{
						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
							string.Format(CultureInfo.InvariantCulture, language.MoveDisabled, obj.GetDescription(game.Language, game.Hero), oldPosition, newCell.Position),
							newActivity);
					}
				}
			}
			else
			{
				return ActionResult.GetEmpty(balance);
			}
		}
	}
}
