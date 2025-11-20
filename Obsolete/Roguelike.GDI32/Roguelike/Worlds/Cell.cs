using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Roguelike.Objects;

using Sef.Common;

namespace Roguelike.Worlds
{
	/// <summary>
	/// Ячейка карты.
	/// </summary>
	public class Cell
	{
		#region Свойства

		/// <summary>
		/// Координаты.
		/// </summary>
		public Point Position
		{ get; private set; }

		/// <summary>
		/// Фон.
		/// </summary>
		public CellBackground Background
		{ get; private set; }

		/// <summary>
		/// Список объектов.
		/// </summary>
		public IList<WorldObject> Objects
		{ get { return objects; } }

		/// <summary>
		/// "Прозрачность".
		/// </summary>
		/// <remarks>определяет, можно ли пройти в эту ячейку</remarks>
		public bool CanGoThrough
		{ get { return Objects.All(o => o.CanGoThrough); } }

		private readonly IList<WorldObject> objects;

		#endregion

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="position">координаты</param>
		/// <param name="background">фон</param>
		public Cell(Point position, CellBackground background)
		{
			Position = position;
			Background = background;
			objects = new EventList<WorldObject>(
				added: (list, item) => item.SetPosition(Position),
				removed: (list, item) => item.SetPosition(Default.Position));
		}

		/// <summary>
		/// Фикс, исправляющий фон ячейки на требуемый.
		/// </summary>
		/// <param name="background">новый фон</param>
		internal void SetBackground(CellBackground background)
		{
			Background = background;
		}

		/// <summary>
		/// # Пустая клетка #
		/// </summary>
		/// <remarks>значение клетки по умолчанию</remarks>
		public static readonly Cell Default = new Cell(new Point(), CellBackground.Default);
	}
}
