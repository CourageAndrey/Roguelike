using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;

namespace Roguelike.WpfClient
{
	internal class WpfInterface : IUserInterface
	{
		public void ShowMessage(string title, StringBuilder text)
		{
			new MessageWindow
			{
				Title = title,
				Text = text.ToString(),
			}.ShowDialog();
		}

		public bool TrySelectItem(Game game, string question, IEnumerable<ListItem> items, out ListItem selectedItem)
		{
			var itemsList = items.ToList();
			if (itemsList.Count(i => i.IsAvailable) == 0)
			{
				selectedItem = null;
				return false;
			}

			var dialog = new ChoiceWindow
			{
				Title = question,
				Items = itemsList,
				Game = game,
			};
			if (dialog.ShowDialog() == true)
			{
				selectedItem = dialog.SelectedItem;
				return true;
			}
			else
			{
				selectedItem = null;
				return false;
			}
		}

		public bool TrySelectItems(Game game, string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems)
		{
			var dialog = new MultiChoiceWindow
			{
				Title = question,
				Items = items,
				Game = game,
			};
			if (dialog.ShowDialog() == true)
			{
				selectedItems = dialog.SelectedItems;
				return true;
			}
			else
			{
				selectedItems = new ListItem[0];
				return false;
			}
		}

		public void ShowCharacter(Game game, Humanoid humanoid)
		{
			new CharacherWindow
			{
				GameLanguage = game.Language,
			}.ShowDialog();
		}
	}
}
