using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roguelike.Objects.ActiveCharacters;
using Roguelike.Worlds;

namespace Roguelike
{
  /// <summary>
  /// Игра.
  /// </summary>
  public class Game
  {
    #region Свойства

    /// <summary>
    /// Мир.
    /// </summary>
    public readonly World World;

    /// <summary>
    /// Герой текущего мира игры.
    /// </summary>
    public Hero Hero
		{ get { return World.Hero; } }

		/// <summary>
		/// Журнал событий для вывода на экран.
		/// </summary>
		public string Log
		{
			get
			{
				var logString = new StringBuilder();
				foreach (var line in log.Reverse())
				{
					logString.AppendLine(line);
				}
				return logString.ToString();
			}
		}
		
		/// <summary>
		/// Запись в журнал.
		/// </summary>
		/// <param name="message">сообщение</param>
		public void WriteLog(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				if (log.Count == Balance.MaxLogSize)
				{
					log.Dequeue();
				}
				log.Enqueue(message);
			}
		}

		// очередь сообщений журнала событий
		private readonly Queue<string> log = new Queue<string>(Balance.MaxLogSize);

    #endregion

    #region Singleton

    /// <summary>
    /// Текущий экземпляр.
    /// </summary>
    public static Game Current
    { get; private set; }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="world">мир</param>
    private Game(World world)
    {
      World = world;
    }
    
    /// <summary>
    /// Создание новой игры.
    /// </summary>
    /// <param name="settings">настройки будущей игры</param>
    public static void CreateNew(StartSettings settings)
    {
      Current = new Game(new World(settings));
    }

    /// <summary>
    /// Загрузка игры.
    /// </summary>
    /// <param name="fileName">путь к файлу</param>
    public static void Load(string fileName)
    {
      Current = new Game(World.Load(fileName));
    }

    /// <summary>
    /// Сохранение игры.
    /// </summary>
    /// <param name="fileName">путь к файлу</param>
    public void Save(string fileName)
    {
      World.Save(fileName);
    }

    #endregion

    /// <summary>
    /// Выполнить консольную команду.
    /// </summary>
    /// <param name="command">строка команды</param>
    public StringBuilder ConsoleRun(string command)
    {
      var result = new StringBuilder();

      try
      {
        // разбивка команды на строковые кусочки
        var tokens = command.Split();

        // вывод непостредственных характеристик героя
        if (tokens[0] == "?")
          result.Append(Hero.Properties.GetText());
        // вывод модицированных характеристик героя
        else if (tokens[0] == "@")
        {
          result.AppendLine("Модифицированные характеристики персонажа:");
          result.Append(Hero.Properties.GetText());
        }
        // увеличение значения характеристики
        else if (tokens[0] == "!")
          result.AppendLine(Hero.Properties.UpdateFeatureModify(tokens[1], Convert.ToDouble(tokens[2])));
        // установка значения характеристики
        else if (tokens[0] == "=")
          result.AppendLine(Hero.Properties.UpdateFeatureSet(tokens[1], Convert.ToDouble(tokens[2])));
        // вывод значения скорости
        else if (tokens[0] == ".")
          result.AppendLine(string.Format("Все действия героя занимают времени {0:N2}% от обычного.", Hero.Speed * 100));
        // ошибка разбора
        else
          throw new NotSupportedException("Данная команда не известна!");
      }
      catch (Exception ex)
      {
        result.AppendLine("Введите корректную команду! Предыдущая вызвала ошибку: " + ex.Message);
      }

      return result;
    }
  }
}
