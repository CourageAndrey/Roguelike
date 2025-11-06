using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;
using Roguelike.Core.Saves;

namespace Roguelike.Core
{
	public class Game
	{
		#region Properties

		public World World
		{ get; }

		public Language Language
		{ get; }

		public IHero Hero
		{ get { return World.Hero; } }

		public IUserInterface UserInterface
		{ get; }

		public GameState State
		{ get; private set; }

		#endregion

		#region Event log

		public string Log
		{
			get
			{
				var logString = new StringBuilder();
				foreach (var line in _log.Reverse())
				{
					logString.AppendLine(line);
				}
				return logString.ToString();
			}
		}

		public void WriteLog(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				if (_log.Count == World.Balance.MaxLogSize)
				{
					_log.Dequeue();
				}
				_log.Enqueue(message);
			}
		}

		private readonly Queue<string> _log;

		#endregion

		#region Constructors

		private Game(World world, Language language, IUserInterface userInterface)
		{
			State = GameState.Initialize;
			_log = new Queue<string>(world.Balance.MaxLogSize);
			Language = language;
			World = world;
			World.Game = this;
			UserInterface = userInterface;
		}

		public Game(IUserInterface userInterface, Language language, HeroStartSettings heroStartSettings)
			: this(new World(Balance.CreateDefault(), language, heroStartSettings), language, userInterface)
		{ }

		public static Game Load(Save save, IUserInterface userInterface, Language language)
		{
			var world = save.World;
			while (world.CanBeUpdated)
			{
				world = world.UpdateToNewestVersion();
			}
			var game = new Game(world.Load(), language, userInterface);
			return game;
		}

		public Save Save()
		{
			return new Save { World = new Version_1(World) };
		}

		#endregion

		#region State management

		public void Start()
		{
			foreach (var region in World.Regions)
			{
				region.Start();
			}
			changeState(GameState.Active);
		}

		public void Win()
		{
			changeState(GameState.Win);
		}

		public void Defeat()
		{
			changeState(GameState.Defeat);
		}

		public event Action<Game, GameState>? StateChanged;

		private void changeState(GameState state)
		{
			State = state;
			var handler = Volatile.Read(ref StateChanged);
			if (handler != null)
			{
				handler(this, State);
			}
		}

		#endregion
	}

	public enum GameState
	{
		Initialize,
		Active,
		Win,
		Defeat,
	}
}
