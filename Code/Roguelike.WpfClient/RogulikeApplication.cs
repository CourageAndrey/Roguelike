using System;
using System.Windows;
using System.Windows.Threading;

namespace Roguelike.WpfClient
{
	internal class RogulikeApplication : Application
	{
		public RogulikeApplication()
		{
			DispatcherUnhandledException += dispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += dispatcherAppDomainException;

			this.MainWindow = new MainWindow();
			this.ShutdownMode = ShutdownMode.OnMainWindowClose;
		}

		#region Exception handling

		private static void dispatcherAppDomainException(object sender, UnhandledExceptionEventArgs e)
		{
#warning Implement exception handling.
		}

		private void dispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
#warning Implement exception handling.
		}

		#endregion

		[STAThread]
		private static void Main()
		{
			var application = new RogulikeApplication();
			var menu = new MenuWindow();
			if (menu.ShowDialog() == true)
			{
				((MainWindow) application.MainWindow).Game = menu.Game;
				application.MainWindow.Show();
				application.Run();
			}
		}
	}
}
