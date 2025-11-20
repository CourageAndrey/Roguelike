using System.Collections.Generic;
using System.Linq;

using Roguelike.Objects;

namespace Roguelike.Items
{
  /// <summary>
  /// Некоторый инвентарь - список предметов.
  /// </summary>
  public abstract class Inventory
  {
		#region Свойства

		/// <summary>
		/// Наименование.
		/// </summary>
		public string Name
		{ get; private set; }

		/// <summary>
		/// Общий список вещей.
		/// </summary>
		public IEnumerable<Item> AllItems
		{ get { return Items; } }

		/// <summary>
		/// Максимальное количество предметов.
		/// </summary>
		public int? MaxCount
		{ get; private set; }

		/// <summary>
		/// Максимальный переносимый вес.
		/// </summary>
		public double? MaxWeight
		{ get; private set; }

    /// <summary>
    /// Общий вес переносимых вещей.
    /// </summary>
    public double TotalWeight
		{ get { return Items.Sum(item => item.Weight); } }

		/// <summary>
		/// Полный список вещей.
		/// </summary>
		protected readonly List<Item> Items = new List<Item>();

    #endregion

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="name">наименование</param>
		/// <param name="maxCount">максимальное количество предметов</param>
		/// <param name="maxWeight">максимальный переносимый вес</param>
	  protected Inventory(string name, int? maxCount = null, double? maxWeight = null)
		{
			Name = name;
			MaxCount = maxCount;
			MaxWeight = maxWeight;
		}

    #region Работа со списком

		/// <summary>
		/// Добавить новую вещь.
		/// </summary>
		/// <param name="item">вещь</param>
		/// <returns><b>true</b>, если вещь удалось добавить, иначе - <b>false</b></returns>
    public bool Add(Item item)
    {
			if ((MaxCount == null || Items.Count < MaxCount.Value) &&
					(MaxWeight == null || (TotalWeight + item.Weight) <= MaxWeight.Value) &&
					CanAdd(item))
	    {
		    Items.Add(item);
		    return true;
	    }
			else
	    {
		    return false;
	    }
    }

    /// <summary>
    /// Удалить вещь.
    /// </summary>
    /// <param name="item">вещь</param>
    public void Remove(Item item)
    {
      Items.Remove(item);
    }

    #endregion

	  /// <summary>
	  /// Проверка, можно ли положить вещь в инвентарь.
	  /// </summary>
	  /// <param name="item">предмет</param>
	  /// <returns><b>true</b>, если да, иначе - <b>false</b></returns>
	  protected abstract bool CanAdd(Item item);
	}

	/// <summary>
	/// Обычная сумка.
	/// </summary>
	public sealed class Bag : Inventory
	{
		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="maxCount">максимальное количество предметов</param>
		/// <param name="maxWeight">максимальный переносимый вес</param>
		public Bag(int? maxCount = null, double? maxWeight = null)
			: base("сумка", maxCount, maxWeight)
		{ }

		/// <summary>
		/// Проверка, можно ли положить вещь в инвентарь.
		/// </summary>
		/// <param name="item">предмет</param>
		/// <returns><b>true</b>, если да, иначе - <b>false</b></returns>
		protected override bool CanAdd(Item item)
		{
			return true;
		}
	}

#warning Класс-колчан, в который можно будет закладывать стрелы, и пояс для зелий, и ошелёк для денег, тубус для свитков и письменных приложений.
}
