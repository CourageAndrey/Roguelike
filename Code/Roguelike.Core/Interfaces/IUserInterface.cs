using System.Collections.Generic;
using System.Text;

namespace Roguelike.Core.Interfaces
{
	public interface IUserInterface
	{
		void ShowMessage(string title, StringBuilder text);
		bool TrySelectItem(Game game, string question, IEnumerable<ListItem> items, out ListItem selectedItem);
		bool TrySelectItems(Game game, string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems);
		void ShowCharacter(Game game, IHumanoid humanoid);
		ActionResult BeginChat(Game game, IHumanoid humanoid);
		ActionResult BeginTrade(Game game, IHumanoid humanoid);
		ActionResult BeginPickpocket(Game game, IHumanoid humanoid);
		Cell SelectShootingTarget(Game game);
		void AnimateShoot(Direction direction, ICollection<Cell> path, IMissile missile);
	}
}
