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
				new Interaction(language.Interactions.ChopTree, (actor as Humanoid)?.Inventory.OfType<Hatchet>().Any() == true, a =>
				{
					(a as Humanoid).Inventory.Add(new Log());
					CurrentCell.AddObject(new Stump());
					CurrentCell.RemoveObject(this);
					return new ActionResult(
						Time.FromTicks(balance.Time, balance.ActionLongevity.ChopTree),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.ChopTree, a, CurrentCell.Position));
				}),
			};
		}

		#endregion
	}
}
