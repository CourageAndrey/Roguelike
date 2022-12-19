﻿using System.Drawing;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Horse : Alive
	{
		#region Properties

		public override Color SkinColor
		{ get; }

		#endregion

		public Horse(Balance balance, bool sexIsMale, Time birthDate, Color skinColor)
			: base(balance, sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
			AddAspects(new Transport(this));
		}

		protected override ActionResult DoImplementation()
		{
			var game = this.GetGame();
			var balance = game.Balance;

			if (State.IsThirsty)
			{
				var waterSource = CurrentCell.Objects.Select<IObject, WaterSource>().FirstOrDefault();
				if (waterSource != null)
				{
					return waterSource.GetAspect<WaterSource>().Drink(this);
				}
				else
				{
					var waterCell = CurrentCell.FindClosiest(cell => cell.Objects.Any(o => o.Is<WaterSource>()), balance.Distance.AiRange);
					waterSource = waterCell != null
						? waterCell.Objects.Select<IObject, WaterSource>().First()
						: null;

					var nextStep = waterSource != null
						? Ai.CalculateRoute(this.GetRegion(), this.GetPosition(), waterSource.CurrentCell.Position).Skip(1).FirstOrDefault()
						: null;
					if (nextStep != null)
					{
						return this.TryMove(this.GetPosition().GetDirection(nextStep));
					}
					else
					{
						return ActionResult.Wait(this);
					}
				}
			}
			else if (State.IsHungry)
			{
				if (CurrentCell.Background == CellBackground.Grass)
				{
					return Eat(ItemFactory.CreateGrass(State.GetFoodToFull()));
				}
				else
				{
					Cell grassCell = CurrentCell.FindClosiest(cell => cell.Background == CellBackground.Grass, balance.Distance.AiRange);

					var nextStep = grassCell != null
						? Ai.CalculateRoute(this.GetRegion(), this.GetPosition(), grassCell.Position).Skip(1).FirstOrDefault()
						: null;
					if (nextStep != null)
					{
						return this.TryMove(this.GetPosition().GetDirection(nextStep));
					}
					else
					{
						return ActionResult.Wait(this);
					}
				}
			}
			else
			{
				return new ActionResult(Time.FromTicks(balance.Time, 60 * 60 * 1000), string.Empty);
			}
		}

		public override Body CreateBody()
		{
			return Body.CreateAnimal();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Horse;
		}
	}
}
