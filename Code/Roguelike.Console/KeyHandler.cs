using System;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	internal delegate ActionResult KeyHandlerDelegate(
		Language language,
		ConsoleUi ui,
		Game game,
		World world,
		Hero hero);

	internal class KeyHandler
	{
		private readonly KeyHandlerDelegate _ctrlAltShiftHandler;
		private readonly KeyHandlerDelegate _ctrlAltHandler;
		private readonly KeyHandlerDelegate _ctrlShiftHandler;
		private readonly KeyHandlerDelegate _ctrlHandler;
		private readonly KeyHandlerDelegate _altShiftHandler;
		private readonly KeyHandlerDelegate _altHandler;
		private readonly KeyHandlerDelegate _shiftHandler;
		private readonly KeyHandlerDelegate _handler;

		public KeyHandler(
			KeyHandlerDelegate ctrlAltShiftHandler,
			KeyHandlerDelegate ctrlAltHandler,
			KeyHandlerDelegate ctrlShiftHandler,
			KeyHandlerDelegate ctrlHandler,
			KeyHandlerDelegate altShiftHandler,
			KeyHandlerDelegate altHandler,
			KeyHandlerDelegate shiftHandler,
			KeyHandlerDelegate handler)
		{
			_ctrlAltShiftHandler = ctrlAltShiftHandler;
			_ctrlAltHandler = ctrlAltHandler;
			_ctrlShiftHandler = ctrlShiftHandler;
			_ctrlHandler = ctrlHandler;
			_altShiftHandler = altShiftHandler;
			_altHandler = altHandler;
			_shiftHandler = shiftHandler;
			_handler = handler;
		}

		public KeyHandler(KeyHandlerDelegate handler)
			: this(null, null, null, null, null, null, null, handler)
		{ }

		public static KeyHandler Shift(KeyHandlerDelegate withoutHandler, KeyHandlerDelegate withHandler)
		{
			return new KeyHandler(null, null, null, null, null, null, withHandler, withoutHandler);
		}

		public static KeyHandler Ctrl(KeyHandlerDelegate withoutHandler, KeyHandlerDelegate withHandler)
		{
			return new KeyHandler(null, null, null, null, null, withHandler, null, withoutHandler);
		}

		public static KeyHandler Alt(KeyHandlerDelegate withoutHandler, KeyHandlerDelegate withHandler)
		{
			return new KeyHandler(null, null, null, withHandler, null, null, null, withoutHandler);
		}

		public KeyHandlerDelegate GetHandler(ConsoleKeyInfo key)
		{
			if (DoesInclude(key.Modifiers, ConsoleModifiers.Control))
			{
				if (DoesInclude(key.Modifiers, ConsoleModifiers.Alt))
				{
					if (DoesInclude(key.Modifiers, ConsoleModifiers.Shift))
					{
						return _ctrlAltShiftHandler;
					}
					else
					{
						return _ctrlAltHandler;
					}
				}
				else
				{
					if (DoesInclude(key.Modifiers, ConsoleModifiers.Shift))
					{
						return _ctrlShiftHandler;
					}
					else
					{
						return _ctrlHandler;
					}
				}
			}
			else
			{
				if (DoesInclude(key.Modifiers, ConsoleModifiers.Alt))
				{
					if (DoesInclude(key.Modifiers, ConsoleModifiers.Shift))
					{
						return _altShiftHandler;
					}
					else
					{
						return _altHandler;
					}
				}
				else
				{
					if (DoesInclude(key.Modifiers, ConsoleModifiers.Shift))
					{
						return _shiftHandler;
					}
					else
					{
						return _handler;
					}
				}
			}
		}

		private static bool DoesInclude(ConsoleModifiers value, ConsoleModifiers flag)
		{
			return (value & flag) != 0;
		}
	}
}
