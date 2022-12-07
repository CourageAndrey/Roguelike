using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core
{
	public abstract class Active : Object, IActive
	{
		public Time? NextActionTime
		{ get; internal set; }

		public abstract ActionResult Do();

		public ActionResult TryMove(Direction direction)
		{
			var world = this.GetWorld();
			var game = world.Game;
			var balance = game.Balance;

			var oldPosition = this.GetPosition();
			var newCell = this.GetRegion().GetCell(this.GetPosition().GetNeighboor(direction));
			if (newCell != null)
			{
				double distance = this.GetPosition() != null
					? newCell.Position.GetDistance(this.GetPosition())
					: 0;
				var language = game.Language.LogActionFormats;

				var alive = this as IAlive;
				IAlive target = null;
				if (alive?.IsAgressive == true && alive?.WeaponToFight?.GetAspect<Weapon>()?.IsRange == false && (target = newCell.Objects.OfType<IAlive>().FirstOrDefault()) != null)
				{
					return alive.Attack(target);
				}
				else
				{
					Activity newActivity = (this as IAlive)?.IsAgressive == false
						? Activity.Walks
						: null;

					if (!IsSolid || newCell.IsTransparent)
					{
						MoveTo(newCell);

						return new ActionResult(
							Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Step * distance)),
							string.Format(CultureInfo.InvariantCulture, language.Move, GetDescription(game.Language, game.Hero), oldPosition, newCell.Position),
							newActivity);
					}
					else
					{
						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
							string.Format(CultureInfo.InvariantCulture, language.MoveDisabled, GetDescription(game.Language, game.Hero), oldPosition, newCell.Position),
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
