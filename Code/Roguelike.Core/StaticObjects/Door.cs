using System;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class Door : Object, IDoor
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

		public void Open()
		{
			if (!_isClosed) throw new InvalidOperationException("Impossible to open already opened door.");

			_isClosed = false;
			RaiseIsSolidChanged(false, true);
		}

		public void Close()
		{
			if (_isClosed) throw new InvalidOperationException("Impossible to close already closed door.");

			_isClosed = true;
			RaiseIsSolidChanged(false, true);
		}
	}
}
