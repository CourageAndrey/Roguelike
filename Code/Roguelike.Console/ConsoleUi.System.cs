using System;
using System.Runtime.InteropServices;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		private static void LockConsoleSize(int width, int height)
		{
			System.Console.SetBufferSize(width, height);
			System.Console.SetWindowSize(width, height);

			IntPtr handle = GetConsoleWindow();
			IntPtr sysMenu = GetSystemMenu(handle, false);
			DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
			DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
		}

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll")]
		private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		private const uint SC_MAXIMIZE = 0xF030;
		private const uint SC_SIZE = 0xF000;
		private const uint MF_BYCOMMAND = 0x00000000;
	}
}