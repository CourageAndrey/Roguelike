using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class Door : Object, IInteractive
	{
		#region Properties

		public bool IsOpened
		{
			get { return !isClosed; }
			set
			{
				isClosed = !value;
				CurrentCell.RefreshView();
			}
		}

		public bool IsClosed
		{
			get { return isClosed; }
			set
			{
				isClosed = value;
				CurrentCell.RefreshView();
			}
		}

		public override bool IsSolid
		{ get { return isClosed; } }

		private bool isClosed;

		#endregion

		#region Implementation of IInteractive

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.InteractionOpenDoor, IsClosed, a =>
				{
					IsOpened = true;
					return new ActionResult(
						Time.FromTicks(balance, balance.ActionLongevityOpenCloseDoor),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormatOpenDoor, a, CurrentCell.Position));
				}),
				new Interaction(language.InteractionCloseDoor, IsOpened && CurrentCell.Objects.FirstOrDefault(o => o.IsSolid) == null, a =>
				{
					IsClosed = true;
					return new ActionResult(
						Time.FromTicks(balance, balance.ActionLongevityOpenCloseDoor),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormatCloseDoor, a, CurrentCell.Position));
				}),
			};
		}

		#endregion
	}
}
