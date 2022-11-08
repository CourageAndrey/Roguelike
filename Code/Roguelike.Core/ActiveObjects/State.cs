using System.Collections.Generic;
using System.Text;
using System.Threading;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public class State : IState
	{
		#region Properties

		private readonly Balance _balance;
		private readonly IAlive _owner;
		private readonly ICollection<IDisease> _diseases;
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
		private int _waterLevel;
		private int _foodLevel;

		public bool IsHungry
		{
			get { return _foodLevel < _balance.Food.HungerLevel; }
		}

		public bool IsBloated
		{
			get { return _foodLevel > _balance.Food.BloatedLevel; }
		}

		public bool IsThirsty
		{
			get { return _waterLevel < _balance.Food.ThirstLevel; }
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
		{ get { return _diseases.Count > 0; } }

		public Activity Activity
		{ get; private set; }

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
			Balance balance,
			IAlive owner,
			IEnumerable<IDisease> diseases = null,
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
			int waterLevel = 0,
			int foodLevel = 0)
		{
			_balance = balance;
			_owner = owner;

			_diseases = new List<IDisease>(diseases ?? new IDisease[0]);

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
			_waterLevel = waterLevel;
			_foodLevel = foodLevel;

			Activity = Activity.Stands;
		}

		public string GetDescription(LanguageState language, IAlive forWhom)
		{
			var result = new StringBuilder();

			string activity = Activity.GetDescription(language.Activities);
			if (!string.IsNullOrEmpty(activity))
			{
				result.Append($"{language.Activity} : activity");
			}

			if (IsSick)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsSick} ({string.Join(", ", _diseases)})");
			}

			if (_isDirty)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsDirty}");
			}

			if (_isPoisoned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsPoisoned}");
			}

			if (_hasHangover)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.HasHangover}");
			}

			if (_isDrunk)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsDrunk}");
			}

			if (_isScared)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsScared}");
			}

			if (_isLightningBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsLightningBurned}");
			}

			if (_isAcidBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsAcidBurned}");
			}

			if (_isFireBurned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsFireBurned}");
			}

			if (_isSunburned)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsSunburned}");
			}

			if (_isFrozen)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsFrozen}");
			}

			if (_isConfused)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsConfused}");
			}

			if (_isLosingBlood)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsLosingBlood}");
			}

			if (_isFallingAsleep)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsFallingAsleep}");
			}

			if (_isTired)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsTired}");
			}

			if (IsThirsty)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsThirsty}");
			}

			if (IsBloated)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsBloated}");
			}

			if (IsHungry)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append($"{language.IsHungry}");
			}

			if (result.Length == 0)
			{
				result.Append("-");
			}

			return result.ToString();
		}

		public void SetActivity(Activity activity)
		{
			Activity = activity;
		}

		public void EatDrink(IFood food, LanguageDeathReasons deathReasons)
		{
			_foodLevel += food.Nutricity;
			_waterLevel += food.Water;
			RaiseChanged();
		}

		public void PassTime(Time span, LanguageDeathReasons deathReasons)
		{
			_foodLevel -= (int)(span.TotalTicks / _balance.Food.TicksToChangeLevel);
			_waterLevel -= (int)(span.TotalTicks / _balance.Food.TicksToChangeLevel);

			RaiseChanged();

			if (_foodLevel < _balance.Food.HungerDeathLevel)
			{
				_owner.Die(deathReasons.Hunger);
			}
			if (_waterLevel < _balance.Food.ThirstDeathLevel)
			{
				_owner.Die(deathReasons.Thirst);
			}
		}

		public override string ToString()
		{
			var result = new StringBuilder();

			if (IsSick)
			{
				//if (result.Length > 0)
				//{
				//	result.Append(", ");
				//}
				result.Append($"Sick ({string.Join(", ", _diseases)})");
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

			if (IsThirsty)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Thirsty");
			}

			if (IsBloated)
			{
				if (result.Length > 0)
				{
					result.Append(", ");
				}
				result.Append("Bloated");
			}

			if (IsHungry)
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
