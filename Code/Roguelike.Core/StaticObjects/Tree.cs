using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.StaticObjects
{
	public class Tree : Object, IInteractive
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		#region Implementation of IInteractive

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.InteractionChopTree, (actor as Humanoid)?.Inventory.OfType<Hatchet>().Any() == true, a =>
				{
					(a as Humanoid).Inventory.TryAdd(new Log());
					CurrentCell.AddObject(new Stump());
					CurrentCell.RemoveObject(this);
					return new ActionResult(
						Time.FromTicks(balance, balance.ActionLongevityChopTree),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormatChopTree, a, CurrentCell.Position));
				}),
			};
		}

		#endregion
	}
}
