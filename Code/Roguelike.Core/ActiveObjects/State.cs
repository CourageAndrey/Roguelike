using System.Text;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class State : IState
	{
		#region Properties

		private readonly bool _isSick;
		private readonly bool _isDirty;
		private readonly bool _isPoisoned;
		private readonly bool _hasHangover;
		private readonly bool _isDrunk;
		private readonly bool _isScared;
		private readonly bool _isLightningBurned;
		private readonly bool _isAcidBurned;
		private readonly bool _isFireBurned;
		private readonly bool _isSunburned;
		private readonly bool _isFrozen;
		private readonly bool _isConfused;
		private readonly bool _isLosingBlood;
		private readonly bool _isFallingAsleep;
		private readonly bool _isTired;
		private readonly bool _isThirsty;
		private readonly bool _isBloated;
		private readonly bool _isHungry;

		public bool IsHungry
		{
			get { return _isHungry; }
		}

		public bool IsBloated
		{
			get { return _isBloated; }
		}

		public bool IsThirsty
		{
			get { return _isThirsty; }
		}

		public bool IsTired
		{
			get { return _isTired; }
		}

		public bool IsFallingAsleep
		{
			get { return _isFallingAsleep; }
		}

		public bool IsLosingBlood
		{
			get { return _isLosingBlood; }
		}

		public bool IsConfused
		{
			get { return _isConfused; }
		}

		public bool IsFrozen
		{
			get { return _isFrozen; }
		}

		public bool IsSunburned
		{
			get { return _isSunburned; }
		}

		public bool IsFireBurned
		{
			get { return _isFireBurned; }
		}

		public bool IsAcidBurned
		{
			get { return _isAcidBurned; }
		}

		public bool IsLightningBurned
		{
			get { return _isLightningBurned; }
		}

		public bool IsScared
		{
			get { return _isScared; }
		}

		public bool IsDrunk
		{
			get { return _isDrunk; }
		}

		public bool HasHangover
		{
			get { return _hasHangover; }
		}

		public bool IsPoisoned
		{
			get { return _isPoisoned; }
		}

		public bool IsDirty
		{
			get { return _isDirty; }
		}

		public bool IsSick
		{
			get { return _isSick; }
		}

		public event EventHandler<IState> Changed;

		protected void RaiseChanged()
		{
			var handler = Volatile.Read(ref Changed);
			if (handler != null)
			{
				handler(this);
			}
		}

		#endregion

		public State(
			bool isSick = false,
			bool isDirty = false,
			bool isPoisoned = false,
			bool hasHangover = false,
			bool isDrunk = false,
			bool isScared = false,
			bool isLightningBurned = false,
			bool isAcidBurned = false,
			bool isFireBurned = false,
			bool isSunburned = false,
			bool isFrozen = false,
			bool isConfused = false,
			bool isLosingBlood = false,
			bool isFallingAsleep = false,
			bool isTired = false,
			bool isThirsty = false,
			bool isBloated = false,
			bool isHungry = false)
		{
			_isSick = isSick;
			_isDirty = isDirty;
			_isPoisoned = isPoisoned;
			_hasHangover = hasHangover;
			_isDrunk = isDrunk;
			_isScared = isScared;
			_isLightningBurned = isLightningBurned;
			_isAcidBurned = isAcidBurned;
			_isFireBurned = isFireBurned;
			_isSunburned = isSunburned;
			_isFrozen = isFrozen;
			_isConfused = isConfused;
			_isLosingBlood = isLosingBlood;
			_isFallingAsleep = isFallingAsleep;
			_isTired = isTired;
			_isThirsty = isThirsty;
			_isBloated = isBloated;
			_isHungry = isHungry;
		}

		public override string ToString()
		{
#warning localize
			var result = new StringBuilder();

			if (_isSick)
			{
				//if (result.Length > 0)
				//{
				//	result.Append(", ");
				//}
				result.Append("Sick");
			}

			if (_isDirty)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Dirty");
			}

			if (_isPoisoned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Poisoned");
			}

			if (_hasHangover)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Hangover");
			}

			if (_isDrunk)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Drunk");
			}

			if (_isScared)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Scared");
			}

			if (_isLightningBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Lightning Burned");
			}

			if (_isAcidBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Acid Burned");
			}

			if (_isFireBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Fire Burned");
			}

			if (_isSunburned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Sunburned");
			}

			if (_isFrozen)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Frozen");
			}

			if (_isConfused)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Confused");
			}

			if (_isLosingBlood)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Losing Blood");
			}

			if (_isFallingAsleep)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Falling Asleep");
			}

			if (_isTired)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Tired");
			}

			if (_isThirsty)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Thirsty");
			}

			if (_isBloated)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Bloated");
			}

			if (_isHungry)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Hungry");
			}

			return result.ToString();
		}
	}
}
