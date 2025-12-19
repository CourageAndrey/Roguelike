using System.Globalization;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Aspects
{
	public class Fighter : AspectWithHolder<IAlive>, IVariableMassy
	{
		#region Properties

		public IItem WeaponToFight
		{ get; private set; }
#warning Take shield and also left/right hands into account

		public bool IsAggressive
		{ get; private set; }

		public decimal Weight
		{ get { return WeaponToFight.Weight; } }

		public event ValueChangedEventHandler<IAlive, bool>? AggressiveChanged;

		public event ValueChangedEventHandler<IAlive, IItem>? WeaponChanged;

		public event ValueChangedEventHandler<IMassy, decimal>? WeightChanged;

		#endregion

		public Fighter(IAlive holder)
			: base(holder)
		{
			WeaponToFight = new Unarmed(holder);
		}

		public ActionResult ChangeAggressive(bool aggressive)
		{
			int time;
			string logMessage;
			var game = Holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.World.Balance;
			Activity? newActivity = null;

			if (IsAggressive != aggressive)
			{
				IsAggressive = aggressive;

				RaiseAggressiveChanged(!aggressive, aggressive);

				if (aggressive)
				{
					WeaponToFight.GetAspect<Weapon>().RaisePreparedForBattle(Holder);
					newActivity = Activity.Guards;
				}
				else
				{
					WeaponToFight.GetAspect<Weapon>().RaiseStoppedBattle(Holder);
					newActivity = Activity.Stands;
				}

				time = balance.ActionLongevity.ChangeAggressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAggressive ? language.StartFight : language.StopFight,
					Holder.GetDescription(game.Language, game.Hero),
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeFightModeDisabled, Holder.GetDescription(game.Language, game.Hero));
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage, newActivity);
		}

		public ActionResult ChangeWeapon(IItem weapon)
		{
			int time;
			string logMessage;
			var game = Holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.World.Balance;

			var oldWeapon = WeaponToFight;
			if (oldWeapon != weapon)
			{
				WeaponToFight = weapon;

				if (!(oldWeapon is Unarmed))
				{
					Holder.Inventory.Items.Add(oldWeapon);
				}
				if (!(weapon is Unarmed))
				{
					Holder.Inventory.Items.Remove(weapon);
				}

				RaiseWeaponChanged(oldWeapon, weapon);

				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					language.ChangeWeapon,
					Holder.GetDescription(game.Language, game.Hero),
					oldWeapon,
					weapon);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeWeaponDisabled, Holder.GetDescription(game.Language, game.Hero));
			}

			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage);
		}

		public ActionResult Backstab(IAlive target)
		{
			var game = Holder.GetGame();
			var language = game.Language;
			var balance = game.World.Balance;

			target.Die(language.DeathReasons.Backstabbed);
			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.Backstab),
				string.Format(
					CultureInfo.InvariantCulture,
					language.LogActionFormats.Backstab,
					target.GetDescription(language, game.Hero),
					Holder.GetDescription(language, game.Hero),
					WeaponToFight.GetDescription(language, game.Hero)));
		}

		public ActionResult? Attack(IAlive target)
		{
			if (WeaponToFight.GetAspect<Weapon>().IsRange) return null;

			var world = Holder.GetWorld();
			var game = world.Game;
			var balance = world.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.Player.BaseHitPossibility;
			hitPossibility += (Holder.Properties.Reaction - target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, Holder.GetDescription(game.Language, game.Hero)));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Attack, Holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		public ActionResult? Shoot(Cell target)
		{
			if (!WeaponToFight.GetAspect<Weapon>().IsRange) return null;

			var position = Holder.CurrentCell!.Position;
			var region = Holder.CurrentCell.Region;
			var world = region.World;
			var game = world.Game;
			var balance = world.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			var missileType = WeaponToFight.GetAspect<RangeWeapon>().Type;
			var missile = Holder.Inventory.Items
				.Select<IItem, Missile>()
				.FirstOrDefault(m => m.GetAspect<Missile>().Type == missileType);
			if (missile == null)
			{
				return new ActionResult(
					Time.FromTicks(balance.Time, 0),
					language.LogActionFormats.NoMissiles);
			}

			if (!position.Equals(target.Position))
			{
				var direction = position.GetDirection(target.Position);

				// take all obstacles into account
				var track = new List<Cell>();
				int dx = target.Position.X - position.X,
					dy = target.Position.Y - position.Y;
				double	distance = Math.Sqrt(dx * dx + dy * dy),
						sx = dx / distance,
						sy = dy / distance,
						x = position.X,
						y = position.Y;
				while (distance > 1)
				{
					x += sx;
					y += sy;
					var cell = region.GetCell(
						(int) Math.Round(x, MidpointRounding.AwayFromZero),
						(int) Math.Round(y, MidpointRounding.AwayFromZero),
#warning Z is ignored here.
						position.Z);
					if (!track.Contains(cell))
					{
						track.Add(cell);
					}
					distance--;
				}
				track.Remove(target);

				if (track.Count > 0)
				{
					// animate missile fly
					game.UserInterface.AnimateShoot(direction, track, missile);
				}
			}

			// calculate damage
			var aim = target.Objects.OfType<IAlive>().FirstOrDefault();
			if (aim != null)
			{
				int hitPossibility = balance.Player.BaseHitPossibility;
				hitPossibility += (Holder.Properties.Perception - aim.Properties.Reaction) * 10;
				if (random.Next(0, 100) < hitPossibility)
				{
					aim.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, Holder.GetDescription(game.Language, game.Hero)));
				}
			}

			// remove missile
			Holder.Inventory.RemoveOneItem(missile);

			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.Shoot),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Shoot, Holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		private void RaiseAggressiveChanged(bool oldAggressive, bool newAggressive)
		{
			var handler = Volatile.Read(ref AggressiveChanged);
			if (handler != null)
			{
				handler(Holder, oldAggressive, newAggressive);
			}
		}

		private void RaiseWeaponChanged(IItem oldWeapon, IItem newWeapon)
		{
			var weaponHandler = Volatile.Read(ref WeaponChanged);
			if (weaponHandler != null)
			{
				weaponHandler(Holder, oldWeapon, newWeapon);
			}

			var weightChanged = Volatile.Read(ref WeightChanged);
			if (weightChanged != null)
			{
				weightChanged(Holder, oldWeapon.Weight, newWeapon.Weight);
			}
		}
	}
}
