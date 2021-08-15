﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.StaticObjects;
using Roguelike.WpfClient.ViewModels;

namespace Roguelike.WpfClient
{
	internal static class ConsoleUiHelper
	{
		#region Models

		public static ObjectViewModel GetModel(Cell cell)
		{
			var objectToDisplay = cell.GetTopVisibleObject();
			if (objectToDisplay == null) return BackgroundViewModel.All[cell.Background];

			Func<Core.Object, ObjectViewModel> modelCreator = null;
			var objectType = objectToDisplay.GetType();
			while (objectType != null)
			{
				if (modelCreators.TryGetValue(objectType, out modelCreator))
				{
					break;
				}
				else
				{
					objectType = objectType.BaseType;
				}
			}
			if (modelCreator != null)
			{
				return modelCreator(objectToDisplay);
			}
			else
			{
				throw new NotSupportedException("Impossible to display object of type " + objectToDisplay.GetType().FullName);
			}
		}

		private static readonly IDictionary<Type, Func<Core.Object, ObjectViewModel>> modelCreators = new Dictionary<Type, Func<Core.Object, ObjectViewModel>>
		{
			{ typeof(Humanoid), o => new HumanoidViewModel((Humanoid) o) },
			{ typeof(Animal), o => new AnimalViewModel((Animal) o) },
			{ typeof(Wall), o => new WallViewModel((Wall) o) },
			{ typeof(Door), o => new DoorViewModel((Door) o) },
			{ typeof(Pool), o => new PoolViewModel((Pool) o) },
			{ typeof(Tree), o => new TreeViewModel((Tree) o) },
			{ typeof(Fire), o => new FireViewModel((Fire) o) },
			{ typeof(Bed), o => new BedViewModel((Bed) o) },
			{ typeof(Corpse), o => new CorpseViewModel((Corpse) o) },
			{ typeof(Stump), o => new StumpViewModel((Stump) o) },
		};

		#endregion

		#region Routed events

		public static void RemoveRoutedEventHandlers(this UIElement element, RoutedEvent routedEvent)
		{
			var eventHandlersStoreProperty = typeof(UIElement).GetProperty("EventHandlersStore", BindingFlags.Instance | BindingFlags.NonPublic);
			object eventHandlersStore = eventHandlersStoreProperty.GetValue(element, null);
			if (eventHandlersStore == null) return;

			var getRoutedEventHandlers = eventHandlersStore.GetType().GetMethod(
				"GetRoutedEventHandlers",
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			var routedEventHandlers = (RoutedEventHandlerInfo[]) getRoutedEventHandlers.Invoke(
				eventHandlersStore,
				new object[] { routedEvent });

			foreach (var routedEventHandler in routedEventHandlers)
			{
				element.RemoveHandler(routedEvent, routedEventHandler.Handler);
			}
		}

		#endregion
	}
}
