using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Saves;

namespace Roguelike.Core
{
	public class Game
	{
		#region Properties

		public Balance Balance
		{ get; }

		public World World
		{ get; }

		public Language Language
		{ get; }

		public Hero Hero
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
				foreach (var line in log.Reverse())
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
				if (log.Count == Balance.MaxLogSize)
				{
					log.Dequeue();
				}
				log.Enqueue(message);
			}
		}

		private readonly Queue<string> log;

		#endregion

		#region Constructors

		private Game(Balance balance, World world, Language language, IUserInterface userInterface)
		{
			Balance = balance;
			State = GameState.Initialize;
			log = new Queue<string>(balance.MaxLogSize);
			World = world ?? new World(this);
			Language = language;
			UserInterface = userInterface;
		}

		public Game(IUserInterface userInterface, Language language)
			: this(Balance.CreateDefault(), null, language, userInterface)
		{ }

		public static Game Load(Save save, IUserInterface userInterface, Language language)
		{
			var world = save.World;
			while (world.CanBeUpdated)
			{
				world = world.UpdateToNewestVersion();
			}
			var balance = Balance.CreateDefault();
			var game = new Game(balance, world.Load(), language, userInterface);
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

		public event Action<Game, GameState> StateChanged;

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
