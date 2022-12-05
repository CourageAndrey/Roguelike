using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Tree : Object, ITree
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		#region Implementation of IInteractive

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = this.GetGame();
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.Interactions.ChopTree, (actor as IAlive)?.Inventory.Any(item => item.Is<TreeChopper>()) == true, a =>
				{
					var inventory = ((IAlive) a).Inventory;
					foreach (var log in Chop())
					{
						inventory.Add(log);
					}
					return new ActionResult(
						Time.FromTicks(balance.Time, balance.ActionLongevity.ChopTree),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.ChopTree, a.GetDescription(game.Language, game.Hero), this.GetPosition()),
						Activity.ChopsTree);
				}),
			};
		}

		#endregion

		public ICollection<IItem> Chop()
		{
			CurrentCell.AddObject(new Stump());
			CurrentCell.RemoveObject(this);
			return new IItem[] { ItemFactory.CreateLog() };
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Tree;
		}
	}
}
