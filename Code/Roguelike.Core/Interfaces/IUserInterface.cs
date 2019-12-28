using System.Collections.Generic;
using System.Text;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Interfaces
{
	public interface IUserInterface
	{
		void ShowMessage(string title, StringBuilder text);
		bool TrySelectItem(Game game, string question, IEnumerable<ListItem> items, out ListItem selectedItem);
		bool TrySelectItems(Game game, string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems);
		void ShowCharacter(Game game, Humanoid humanoid);
	}
}
