using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Roguelike.GUI;
using Roguelike.Objects;
using Roguelike.Objects.ActiveCharacters;
using Roguelike.Objects.Interfaces;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Мир.
  /// </summary>
  public partial class World
  {
    #region Свойства

    // массив ячеек карты
    private readonly Cell[,] cells;
    // границы мира
    private readonly Rectangle bounds;
    // элементали (отвечают за стихийный урон, но не являются объектами мира)
    private readonly IEnumerable<IActiveObject> elementals;

    /// <summary>
    /// Размер мира.
    /// </summary>
    public Size Size
    { get { return bounds.Size; } }
    
    /// <summary>
    /// Герой текущего мира.
    /// </summary>
    public Hero Hero
    { get; private set; }

    /// <summary>
    /// Последний действоваший субъект.
    /// </summary>
    internal IActiveObject LastActor
    { get; private set; }

    /// <summary>
    /// Текущие дата/время мира.
    /// </summary>
    public DateTime TimeStamp
    { get; private set; }

    /// <summary>
    /// Генератор случайных чисел.
    /// </summary>
    public Random God
    { get; private set; }
    
    /// <summary>
    /// Текущий мир.
    /// </summary>
    /// <remarks>реализация шаблона Singleton</remarks>
    public static World Instance
    { get; private set; }

    #endregion

    #region Кэшированные данные

		// кэш видимых на экране объектов
    private List<WorldObject> visibleObjectsCache;
    // список актёров на экране, отсортированный по времени следующего действия
    private List<IActiveObject> actorsSortedCache;
    // флаг: все видимые объекты помещены в кэш
    private bool objectsCached;

    #endregion

		#region Выборка ячеек

		/// <summary>
		/// Получение клетки в точке.
		/// </summary>
		/// <param name="position">координаты точки</param>
		/// <returns>клетка по заданным координатам</returns>
		public Cell GetCell(Point position)
		{
			return bounds.Contains(position)
				? cells[position.X, position.Y]
				: Cell.Default;
		}

    /// <summary>
    /// Получение списка ячеек, находящихся вокруг точки.
    /// </summary>
    /// <param name="position">координаты точки</param>
    /// <returns>таблица направление-ячейка</returns>
    public Dictionary<Direction, Cell> GetCellsAround(Point position)
    {
      var result = new Dictionary<Direction, Cell>();
      foreach (var direction in Vector.AroundDirections8)
      {
				result.Add(direction, GetCell(position.Strafe(direction)));
      }
      return result;
    }

    /// <summary>
    /// Поиск списка ячеек рядом с точкой.
    /// </summary>
    /// <param name="position">точка</param>
    /// <param name="distance">расстояние</param>
    /// <returns>список ячеек</returns>
    public List<Cell> GetCellsNear(Point position, int distance)
    {
      var result = new List<Cell>();
      int distanceSquare = distance * distance;
	    for (int x = -distance; x < distance; x++)
	    {
				for (int y = -distance; y < distance; y++)
				{
					var searchPosition = position.Strafe(x, y);
					if (position.GetDistanceSquare(searchPosition) <= distanceSquare)
					{
						result.Add(GetCell(position.Strafe(x, y)));
					}
				}
	    }
      return result;
    }

		/// <summary>
		/// Поиск списка ячеек рядом в некотором прямоугольнике.
		/// </summary>
		/// <param name="rect">границы</param>
		/// <returns>список ячеек</returns>
		public List<Cell> GetCellsInRect(Rectangle rect)
		{
			var result = new List<Cell>();
			for (int x = rect.Left; x <= rect.Right; x++)
			{
				for (int y = rect.Top; y <= rect.Bottom; y++)
				{
					result.Add(GetCell(new Point(x, y)));
				}
			}
			return result;
		}
		
#warning Три метода ниже - полный шлак - смотри коммент после них
		/// <summary>
    /// Получение единственного объекта нужного типа в точке.
    /// </summary>
    /// <typeparam name="T">тип объектов</typeparam>
    /// <param name="position">координаты точки</param>
    /// <returns>объект</returns>
    public T GetObjectSingle<T>(Point position)
    {
      return GetCell(position).Objects.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Выбор объектов среди окружающих.
    /// </summary>
    /// <typeparam name="T">тип объекта</typeparam>
    /// <param name="targets">список объектов с указанием направления</param>
    /// <param name="question">вопрос выбора</param>
    /// <param name="emptyListWarning">строка, если объектов этого типа нет</param>
    /// <param name="cancelMessage">ответ по умолчанию</param>
    /// <returns>выбранный объект либо null</returns>
    public T SelectObjectFromAround<T>(
      Dictionary<Direction, T> targets,
      string question,
      string emptyListWarning,
      string cancelMessage)
      where T : WorldObject
    {
      // если объектов нет - вывести предупреждение
      if (targets.Count == 0)
      {
        MessageBox.Show(emptyListWarning, string.Empty);
        return null;
      }

      // если объект один - вернуть его
      if (targets.Count == 1)
        return targets.Values.First();

      // выбор из списка
      var choises = new List<ChoiseObject>();
      foreach (var obj in targets)
				choises.Add(new Choise<T>("в направлении " + DirectionHelper.GetDescription(obj.Key), obj.Value));
      var choise = SelectionDialog.Show(question, choises);
      return (choise != CancelChoise.Value)
        ? ((Choise<T>) choise).Value
        : null;
    }
    
    #endregion

    #region Выполнение шага

    /// <summary>
    /// Первоначальное выполнение действий.
    /// </summary>
    /// <remarks>Устанавливает время следующего действия для объектов и стихий мира.</remarks>
    public void StartLife()
    {
      foreach (var elemental in elementals)
        elemental.NextActionTime = TimeStamp;

	    for (int x = 0; x < Size.Width; x++)
	    {
		    for (int y = 0; y < Size.Height; y++)
		    {
					foreach (var o in cells[x, y].Objects.OfType<IActiveObject>())
			    {
						o.NextActionTime = TimeStamp;
			    }
		    }
	    }
    }

    /// <summary>
    /// Выполнение одного шага мира.
    /// </summary>
    public void DoOneStep()
    {
      // поиск следующего действующего лица
      var nextActor = GetPossibleActors().FirstOrDefault();
      LastActor = nextActor;
      if (nextActor == null)
        return;

      // выполнение действия
      var actionResult = nextActor.Do();
      nextActor.NextActionTime = nextActor.NextActionTime.AddSeconds(actionResult.Longevity);
      if ((nextActor.NextActionTime > TimeStamp) && (nextActor.NextActionTime != DateTime.MaxValue) && (nextActor.NextActionTime != DateTime.MinValue))
        TimeStamp = nextActor.NextActionTime;
      var realActor = nextActor as WorldObject;
      if (realActor != null)
        foreach (var line in actionResult.LogMessages)
          Game.Current.WriteLog(line);
      
      // отработка голода
      var actor = nextActor as Actor;
      if (actor != null)
      {
        string hunger = actor.Body.ParseHunger(actor);
        if (actor.Body.Destroyed)
          actor.Die("скончался от голода");
        if (!string.IsNullOrEmpty(hunger))
#if !DEBUG
          if (obj is Hero)
#endif
					Game.Current.WriteLog(string.Format("{0} страдает от голода: {1}", actor.Description, hunger));
      }

      // подготовка дополнительного кэширования
      actorsSortedCache.Remove(LastActor);
      if (LastActor == Hero)
        objectsCached = false;
    }

    private void recacheIdNeed()
    {
      // если кэширование уже выполнено - нет необходимости выполнять его повторно
      if (objectsCached)
        return;

      // поиск всех видимых на экране объектов
	    visibleObjectsCache = new List<WorldObject>();
			foreach (var cell in GetCellsInRect(new Rectangle(
				Hero.Position.X - Balance.AiDistance,
				Hero.Position.Y - Balance.AiDistance,
				Balance.AiDistance*2,
				Balance.AiDistance*2)))
	    {
		    visibleObjectsCache.AddRange(cell.Objects);
	    }

      // поиск всех активных среди видимых
      actorsSortedCache = visibleObjectsCache.OfType<IActiveObject>().ToList();

      // добавление стихий
      actorsSortedCache.AddRange(elementals);

      // сортировка активных, начиная с тех, кто будет первым ходить
      actorsSortedCache.Sort((a1, a2) => a1.NextActionTime.CompareTo(a2.NextActionTime));

      objectsCached = true;
    }

    /// <summary>
    /// Получение списка видимых объектов.
    /// </summary>
    /// <returns>visibleObjectsCache</returns>
    internal List<WorldObject> GetVisibleObjects()
    {
      recacheIdNeed();
      return visibleObjectsCache;
    }

    /// <summary>
    /// Получение списка действующих лиц.
    /// </summary>
    /// <returns>actorsSortedCache</returns>
    internal List<IActiveObject> GetPossibleActors()
    {
      recacheIdNeed();
      return actorsSortedCache;
    }

    #endregion

    /// <summary>
    /// Смена положения в пространстве.
    /// </summary>
    /// <param name="o">объект</param>
    /// <param name="v">новое положение</param>
    public void Teleport(WorldObject o, Point v)
    {
	    cells[o.Position.X, o.Position.Y].Objects.Remove(o);
			cells[v.X, v.Y].Objects.Add(o);
    }

    #region Работа со списком объектов

    /// <summary>
    /// Корректное удаление объекта из мира.
    /// </summary>
    /// <param name="o">объект</param>
    public void RemoveObject(WorldObject o)
    {
			cells[o.Position.X, o.Position.Y].Objects.Remove(o);
    }

    /// <summary>
    /// Добавление нового объекта.
    /// </summary>
    /// <param name="o">объект</param>
    /// <param name="position">положение в пространстве</param>
    public void AddNewObject(WorldObject o, Point position)
    {
			cells[position.X, position.Y].Objects.Add(o);
    }

    #endregion

    #region Окончание игры

    /// <summary>
    /// Окончить игру.
    /// </summary>
    /// <param name="endMessage">причина завершения</param>
    public static void EndGame(string endMessage)
    {
      MessageBox.Show(endMessage, "игра окончена");
      Instance.GameOver = true;
    }

    /// <summary>
    /// Флаг, что игра окончена.
    /// </summary>
    internal bool GameOver
    { get; private set; }

    #endregion
  }
}
