using System.Collections.Generic;
using System.Linq;

using Roguelike.Core;
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
		{ get { return _typeMapping[Object.Items.First().Type]; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.White; } }

		#endregion

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
