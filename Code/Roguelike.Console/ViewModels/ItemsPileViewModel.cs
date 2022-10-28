using System.Collections.Generic;
using System.Linq;

using Roguelike.Core;
using Roguelike.Core.Interfaces;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Console.ViewModels
{
	internal class ItemsPileViewModel : ObjectViewModel<ItemsPile>
	{
		public ItemsPileViewModel(ItemsPile o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return _typeMapping[getTopObject().Type]; } }

		public override System.ConsoleColor Foreground
		{ get { return getTopObject().Color.ToConsole(); } }

		#endregion

		private IItem getTopObject()
		{
			return Object.Items.First();
		}

		private static readonly IDictionary<ItemType, string> _typeMapping = new Dictionary<ItemType, string>
		{
			{ ItemType.Weapon, "(" },
			{ ItemType.Wear, "[" },
			{ ItemType.Food, "%" },
			{ ItemType.Tool, "]" },
			{ ItemType.Paper, "\"" },
		};
	}
}
