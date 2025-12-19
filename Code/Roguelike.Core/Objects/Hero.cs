using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;
using Roguelike.Core.Places;
using Roguelike.Core.RolePlaying;

namespace Roguelike.Core.Objects
{
	public class Hero : Humanoid, IHero
	{
		#region Properties

		public Camera Camera
		{ get { return this.GetAspect<Camera>(); } }

		#endregion

		public Hero(Region region, Balance balance, Time now, HeroStartSettings startSettings)
			: base(
				balance,
				startSettings.Race,
				startSettings.SexIsMale,
				now.AddYears(- (int) startSettings.Age).AddDays(1),
				startSettings.Name,
				startSettings.Profession,
				startSettings.HairColor,
				startSettings.Haircut,
				GetFakeSettlement(region))
		{
			AddAspects(new Camera(this, GetEffectiveVisibleDistance, GetMaxVisibleDistance));
		}

		private decimal GetEffectiveVisibleDistance()
		{
			// base distance
			decimal baseDistance = GetMaxVisibleDistance(),
					distance = baseDistance;

			// take daytime into account
			var world = this.GetWorld();
			var time = world.Time.DayPart;
			var balance = world.Balance.Distance;
			if (time == DayPart.Night)
			{
				distance *= balance.NightVisibilityPercent;
			}
			else if (time == DayPart.Evening || time == DayPart.Morning)
			{
				distance *= balance.TwilightVisibilityPercent;
			}
			else
			{
				distance *= 100;
			}
			distance /= 100;

			var lightSource = Fighter.WeaponToFight?.TryGetAspect<LightSource>();
			if (lightSource != null)
			{
				distance = Math.Min(baseDistance, distance + lightSource.Power);
			}

			var weather = this.GetRegion().Weather;
			distance *= (decimal) weather.VisibilityBonus;

			return Math.Max(1, distance);
		}

		private decimal GetMaxVisibleDistance()
		{
			return Properties.Perception;
		}

		private static Settlement GetFakeSettlement(Region region)
		{
#warning Make existing
			var cell = region.GetCell(0, 0, 0);
			new Door().MoveTo(cell);
			var house = new House(new[] { cell });
			return new Settlement(new[] { house }, l => string.Empty);
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = this.GetGame();
			game.Defeat();
			return base.Die(reason);
		}
	}

	public class HeroStartSettings
	{
		#region Properties

		public Race Race
		{ get; set; }

		public bool SexIsMale
		{ get; set; }

		public uint Age
		{ get; set; }

		public string Name
		{ get; set; }

		public Profession Profession
		{ get; set; }

		public Color HairColor
		{ get; set; }

		public Haircut Haircut
		{ get; set; }

		#endregion
	}
}
