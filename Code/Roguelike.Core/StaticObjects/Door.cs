using System;
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
		{ get { return !_isClosed; } }

		public bool IsClosed
		{ get { return _isClosed; } }

		public override bool IsSolid
		{ get { return _isClosed; } }

		private bool _isClosed;

		#endregion

		#region Implementation of IInteractive

		public void Open()
		{
			if (!_isClosed) throw new InvalidOperationException("Impossible to open already opened door.");

			_isClosed = false;
			CurrentCell.RefreshView(true);
		}

		public void Close()
		{
			if (_isClosed) throw new InvalidOperationException("Impossible to close already closed door.");

			_isClosed = true;
			CurrentCell.RefreshView(true);
		}

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.InteractionOpenDoor, IsClosed, a =>
				{
					Open();
					return new ActionResult(
						Time.FromTicks(balance.Time, balance.ActionLongevity.OpenCloseDoor),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormatOpenDoor, a, CurrentCell.Position));
				}),
				new Interaction(language.InteractionCloseDoor, IsOpened && CurrentCell.Objects.FirstOrDefault(o => o.IsSolid) == null, a =>
				{
					Close();
					return new ActionResult(
						Time.FromTicks(balance.Time, balance.ActionLongevity.OpenCloseDoor),
						string.Format(CultureInfo.InvariantCulture, language.LogActionFormatCloseDoor, a, CurrentCell.Position));
				}),
			};
		}

		#endregion
	}
}
