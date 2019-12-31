using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Win32;

using Roguelike.Core;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.WpfClient
{
	public partial class MenuWindow
	{
		public MenuWindow()
		{
			InitializeComponent();

			((StackPanel) Content).Children.OfType<Button>().First().Focus();
		}

		public Game Game
		{ get; private set; }

		private readonly IUserInterface userInterface = new WpfInterface();

		private void newGameClick(object sender, RoutedEventArgs e)
		{
			Game = new Game(userInterface, (Language) listBoxLanguages.SelectedItem);
			DialogResult = true;
		}

		private void loadSaveClick(object sender, RoutedEventArgs e)
		{
			var language = (Language) listBoxLanguages.SelectedItem;
			var dialog = new OpenFileDialog
			{
				Title = language.UiMainSelectSave,
				Filter = language.UiMainSavesFilter,
			};
			if (dialog.ShowDialog() == true)
			{
				using (var reader = new XmlTextReader(dialog.FileName))
				{
					var saveSerializer = new XmlSerializer(typeof(Save));
					Game.Load((Save) saveSerializer.Deserialize(reader), new WpfInterface(), (Language)listBoxLanguages.SelectedItem);
					DialogResult = true;
				}
			}
		}

#warning Move this code to the right place (implement save)
		/*
		public void Save(string fileName)
		{
			var save = new Save { World = new Version_1(World) };
			var xmlDocument = new XmlDocument();
			using (StringWriter stringWriter = new StringWriter())
			{
				saveSerializer.Serialize(stringWriter, save);
				xmlDocument.LoadXml(stringWriter.ToString());
			}
			if (xmlDocument.DocumentElement != null)
			{
				xmlDocument.DocumentElement.RemoveAttribute("xmlns:xsd");
				xmlDocument.DocumentElement.RemoveAttribute("xmlns:xsi");
			}
			xmlDocument.Save(fileName);
		}
		 */

		private void viewHelpClick(object sender, RoutedEventArgs e)
		{
			var language = (Language) listBoxLanguages.SelectedItem;
			userInterface.ShowMessage(language.HelpTitle, new StringBuilder(language.HelpText));
		}

		private void exitClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void menuWindowLoaded(object sender, RoutedEventArgs e)
		{
			List<Language> availableLanguages;
			Language selectedLanguage = null;

			var defaultLanguage = Core.Localization.Language.CreateDefault();
			string startupPath = AppDomain.CurrentDomain.BaseDirectory;
			var languageFolder = new DirectoryInfo(Path.Combine(startupPath, "Localization"));
			if (languageFolder.Exists)
			{
				var files = languageFolder.GetFiles("*.xml");
				if (files.Length == 0)
				{
					selectedLanguage = defaultLanguage;
					availableLanguages = new List<Language>();
				}
				else
				{
					var languageSerializer = new XmlSerializer(typeof(Language));
					availableLanguages = new List<Language>();
					foreach (var file in files)
					{
						using (var reader = new XmlTextReader(file.FullName))
						{
							availableLanguages.Add((Language) languageSerializer.Deserialize(reader));
						}
					}

					var configFile = Path.Combine(startupPath, "Settings.txt");
					if (File.Exists(configFile))
					{
						string lastSelectedLanguage = File.ReadAllText(configFile);
						selectedLanguage = availableLanguages.FirstOrDefault(l => l.Name == lastSelectedLanguage);
					}
					if (selectedLanguage == null)
					{
						selectedLanguage = defaultLanguage;
					}
				}
			}
			else
			{
				selectedLanguage = defaultLanguage;
				availableLanguages = new List<Language>();
				languageFolder.Create();
			}
			availableLanguages.Insert(0, defaultLanguage);

			listBoxLanguages.ItemsSource = availableLanguages;
			listBoxLanguages.SelectedItem = selectedLanguage;
		}

		private void onClosed(object sender, EventArgs e)
		{
			if (DialogResult == true)
			{
				string configFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.txt");
				File.WriteAllText(configFullPath, ((Language) listBoxLanguages.SelectedItem).Name);
			}
		}

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}
	}
}
