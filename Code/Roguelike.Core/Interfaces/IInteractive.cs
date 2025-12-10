using Roguelike.Core.Mechanics;
using System;
using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IInteractive
	{
		List<Interaction> GetAvailableInteractions(IObject actor);
	}

	public class Interaction
	{
		public string Name { get; }
		public bool IsAvailable { get; }
		private readonly Func<IObject, ActionResult> handler;

		internal Interaction(string name, bool isAvailable, Func<IObject, ActionResult> handler)
		{
			if (handler == null) throw new ArgumentNullException("handler");
			Name = name;
			IsAvailable = isAvailable;
			this.handler = handler;
		}

		public ActionResult Perform(IObject actor)
		{
			return handler(actor);
		}
	}
}
