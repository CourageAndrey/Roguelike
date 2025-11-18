using System;
using System.Text;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		internal static readonly Encoding DefaultEncoding = Encoding.UTF8;
		internal const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
		internal const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
		internal const ConsoleColor HeaderForegroundColor = ConsoleColor.Blue;
		internal const ConsoleColor HighlightForegroundColor = ConsoleColor.Yellow;
		internal const ConsoleColor DisabledForegroundColor = ConsoleColor.Gray;
	}
}