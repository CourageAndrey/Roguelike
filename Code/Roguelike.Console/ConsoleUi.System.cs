using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		[SupportedOSPlatform("windows")]
		private static void AdjustWindowsConsole(int width, int height)
		{
			IntPtr handle = GetConsoleWindow();
			if (handle != IntPtr.Zero)
			{
				LockConsoleSize(handle, width, height);
				CenterConsoleWindow(handle);
			}
		}

		[SupportedOSPlatform("windows")]
		private static void LockConsoleSize(IntPtr handle, int width, int height)
		{
			System.Console.SetBufferSize(width, height);
			System.Console.SetWindowSize(width, height);

			IntPtr sysMenu = GetSystemMenu(handle, false);
			DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
			DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
		}

		[SupportedOSPlatform("windows")]
		private static void CenterConsoleWindow(IntPtr handle)
		{
			if (GetWindowRect(handle, out var rect))
			{
				int windowWidth = rect.Right - rect.Left;
				int windowHeight = rect.Bottom - rect.Top;

				int screenWidth = GetSystemMetrics(SM_CXSCREEN);
				int screenHeight = GetSystemMetrics(SM_CYSCREEN);

				int x = (screenWidth - windowWidth) / 2;
				int y = (screenHeight - windowHeight) / 2;

				SetWindowPos(handle, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
			}
		}

		[SupportedOSPlatform("windows")]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		[SupportedOSPlatform("windows")]
		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[SupportedOSPlatform("windows")]
		[DllImport("user32.dll")]
		private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		[SupportedOSPlatform("windows")]
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[SupportedOSPlatform("windows")]
		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int nIndex);

		[SupportedOSPlatform("windows")]
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[SupportedOSPlatform("windows")]
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern bool WriteConsoleOutput(
			IntPtr hConsoleOutput,
			CHAR_INFO[] lpBuffer,
			COORD dwBufferSize,
			COORD dwBufferCoord,
			ref SMALL_RECT lpWriteRegion);

		[SupportedOSPlatform("windows")]
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct COORD
		{
			public short X;
			public short Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct SMALL_RECT
		{
			public short Left;
			public short Top;
			public short Right;
			public short Bottom;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct CHAR_INFO
		{
			[FieldOffset(0)]
			public char UnicodeChar;
			[FieldOffset(0)]
			public byte AsciiChar;
			[FieldOffset(2)]
			public ushort Attributes;
		}

		private const uint SC_MAXIMIZE = 0xF030;
		private const uint SC_SIZE = 0xF000;
		private const uint MF_BYCOMMAND = 0x00000000;
		private const int SM_CXSCREEN = 0;
		private const int SM_CYSCREEN = 1;
		private const uint SWP_NOSIZE = 0x0001;
		private const uint SWP_NOZORDER = 0x0004;
		private const int STD_OUTPUT_HANDLE = -11;

		private static ushort MakeColorAttribute(ConsoleColor foreground, ConsoleColor background)
		{
			return (ushort)((int)foreground | ((int)background << 4));
		}
	}
}