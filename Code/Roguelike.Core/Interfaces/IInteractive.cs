using System;
using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IInteractive
	{
		List<Interaction> GetAvailableInteractions(Object actor);
	}

	public class Interaction
	{
		public string Name { get; }
		public bool IsAvailable { get; }
		private readonly Func<Object, ActionResult> handler;

		internal Interaction(string name, bool isAvailable, Func<Object, ActionResult> handler)
		{
			if (handler == null) throw new ArgumentNullException("handler");
			Name = name;
			IsAvailable = isAvailable;
			this.handler = handler;
		}

		public ActionResult Perform(Object actor)
		{
			return handler(actor);
		}
	}
}
