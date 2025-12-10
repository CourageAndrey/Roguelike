using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Door : Mechanics.Object, IDoor
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

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Door;
		}
	}
}
