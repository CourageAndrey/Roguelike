using System.Globalization;

using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IObject : IDescriptive, IAspectHolder, ILogSource
	{
		Cell? CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		void MoveTo(Cell? cell);

		event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		event ValueChangedEventHandler<IObject, Cell?> CellChanged;
	}

	public static class ObjectExtensions
	{
		public static Vector? GetPosition(this IObject obj)
		{
			return obj.CurrentCell?.Position;
		}

		public static Region? GetRegion(this IObject obj)
		{
			return obj.CurrentCell?.Region;
		}

		public static World? GetWorld(this IObject obj)
		{
			return obj.GetRegion()?.World;
		}

		public static Game? GetGame(this IObject obj)
		{
			return obj.GetWorld()?.Game;
		}

		public static IHero? GetHero(this IObject obj)
		{
			return obj.GetWorld()?.Hero;
		}

		public static ActionResult TryMove(this IObject obj, Direction direction)
		{
			var world = obj.GetWorld();
			var game = world.Game;
			var balance = world.Balance;

			var oldPosition = obj.GetPosition();
			var newCell = oldPosition != null
				? obj.GetRegion().GetCell(oldPosition.GetNeighbor(direction))
				: null;
			if (newCell != null)
			{
				double distance = newCell.Position.GetDistance(oldPosition);
				var language = game.Language.LogActionFormats;

				var alive = obj as IAlive;
				IAlive? target = null;
				bool canAttack =	alive?.Fighter?.IsAggressive == true &&
									alive?.Fighter?.WeaponToFight?.GetAspect<Weapon>()?.IsRange == false &&
									(target = newCell.Objects.OfType<IAlive>().FirstOrDefault()) != null;

				if (canAttack)
				{
					return alive?.Thief?.IsSneaking == true
						? alive.Fighter.Backstab(target)
						: alive.Fighter.Attack(target);
				}
				else
				{
					Activity? newActivity = alive?.Fighter?.IsAggressive == false
						? (alive?.Thief?.IsSneaking == true ? Activity.Sneaks : Activity.Walks)
						: null;

					if (!obj.IsSolid || newCell.IsTransparent)
					{
						obj.MoveTo(newCell);

						return new ActionResult(
							Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Step * distance)),
							string.Format(
								CultureInfo.InvariantCulture,
								(alive?.Thief?.IsSneaking == true ? language.Sneak : language.Move),
								obj.GetDescription(game.Language, game.Hero),
								oldPosition,
								newCell.Position),
							newActivity);
					}
					else
					{
						var humanoid = obj as IHumanoid;
						return humanoid != null && alive?.Thief?.IsSneaking == true
							? game.UserInterface.BeginPickpocket(game, humanoid)
							: new ActionResult(
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
