using System.Collections.Generic;
using System.Text;

using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IUserInterface
	{
		void ShowMessage(string title, StringBuilder text);
		bool TrySelectItem(string question, IEnumerable<ListItem> items, out ListItem selectedItem);
		bool TrySelectItems(string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems);
		void ShowCharacter(Game game, IHumanoid humanoid);
		void ShowInventory(Game game, IHumanoid humanoid);
		ActionResult ShowEquipment(Game game, Manequin manequin);
		ActionResult BeginChat(Game game, IHumanoid humanoid);
		ActionResult BeginTrade(Game game, IHumanoid humanoid);
		ActionResult BeginPickpocket(Game game, IHumanoid humanoid);
		Cell SelectShootingTarget(Game game);
		void AnimateShoot(Direction direction, ICollection<Cell> path, IItem missile);
	}

	public static class UiExtensions
	{
		public static bool? AskForYesNoCancel(this IUserInterface ui, string question, Game game)
		{
			var language = game.Language.Ui.Common;
			var cases = new List<ListItem>
			{
				new ListItem<bool>(true, language.Yes),
				new ListItem<bool>(false, language.No),
			};

			ListItem selected;
			if (ui.TrySelectItem(question, cases, out selected))
			{
				return ((ListItem<bool>) selected).Value;
			}
			else
			{
				return null;
			}
		}
	}
}
