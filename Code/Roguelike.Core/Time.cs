﻿using System;

using Roguelike.Core.Configuration;

namespace Roguelike.Core
{
	public enum Season
	{
		Winter,
		Spring,
		Summer,
		Autumn,
	}

	public enum DayPart
	{
		Morning,
		Noon,
		Evening,
		Night,
	}

	public readonly struct Time : IEquatable<Time>, IComparable<Time>
	{
		#region Properties

		internal TimeBalance Balance
		{ get; }

		public int Year
		{ get; }

		public byte Month
		{ get; }

		public byte Week
		{ get; }

		public byte Day
		{ get; }

		public short MonthDay
		{ get { return (short) (Week * Balance.DaysInWeek + Day); } }

		public Season Season
		{
			get
			{
				switch (Month)
				{
					case 0:
					case 1:
					case 11:
						return Season.Winter;
					case 2:
					case 3:
					case 4:
						return Season.Spring;
					case 5:
					case 6:
					case 7:
						return Season.Summer;
					case 8:
					case 9:
					case 10:
						return Season.Autumn;
					default:
						throw new NotSupportedException("Season is broken");
				}
			}
		}

		public DayPart DayPart
		{
			get
			{
				double ticksInHour = (double)Balance.TicksInDay / 24;
				double hoursNow = Ticks / ticksInHour;
				if (hoursNow >= 3 && hoursNow < 9)
				{
					return DayPart.Morning;
				}
				else if (hoursNow >= 9 && hoursNow < 15)
				{
					return DayPart.Noon;
				}
				else if (hoursNow >= 15 && hoursNow < 21)
				{
					return DayPart.Evening;
				}
				else
				{
					return DayPart.Night;
				}
			}
		}

		public uint Ticks
		{ get; }

		public long TotalTicks
		{
			get
			{
				long ticks = Year;
				ticks = ticks * Balance.MonthInYear + Month;
				ticks = ticks * Balance.WeeksInMonth + Week;
				ticks = ticks * Balance.DaysInWeek + Day;
				ticks = ticks * Balance.TicksInDay + Ticks;
				return ticks;
			}
		}

		#endregion

		#region Constructors

		public Time(TimeBalance balance, int year, byte month, byte week, byte day, uint ticks)
		{
			Balance = balance;

			Year = year;
			Month = month;
			Week = week;
			Day = day;
			Ticks = ticks;

			if (Month >= balance.MonthInYear) throw new ArgumentOutOfRangeException(nameof(Month), balance.MonthInYear + " MAX");
			if (Week >= balance.WeeksInMonth) throw new ArgumentOutOfRangeException(nameof(Week), balance.WeeksInMonth + " MAX");
			if (Day >= balance.DaysInWeek) throw new ArgumentOutOfRangeException(nameof(Day), balance.DaysInWeek + " MAX");
			if (Ticks >= balance.TicksInDay) throw new ArgumentOutOfRangeException(nameof(Ticks), balance.TicksInDay + " MAX");
		}

		public Time(TimeBalance balance, int year, byte month, byte week, byte day)
			: this(balance, year, month, week, day, 0)
		{ }

		public Time(TimeBalance balance)
			: this(balance, 0, 0, 0, 0, 0)
		{ }

		public static Time FromYears(TimeBalance balance, int value)
		{
			return new Time(balance).AddYears(value);
		}

		public static Time FromMonths(TimeBalance balance, int value)
		{
			return new Time(balance).AddMonths(value);
		}

		public static Time FromWeeks(TimeBalance balance, int value)
		{
			return new Time(balance).AddWeeks(value);
		}

		public static Time FromDays(TimeBalance balance, int value)
		{
			return new Time(balance).AddDays(value);
		}

		public static Time FromTicks(TimeBalance balance, long value)
		{
			return new Time(balance).AddTicks(value);
		}

		#endregion

		#region Comparison

		private static void EnsureSameBalance(TimeBalance balance1, TimeBalance balance2)
		{
			if (balance1 != balance2)
			{
				throw new InvalidOperationException("Impossible to compare date & time with different scale.");
			}
		}

		public bool Equals(Time other)
		{
			EnsureSameBalance(Balance, other.Balance);

			return	Year == other.Year &&
					Month == other.Month &&
					Week == other.Week &&
					Day == other.Day &&
					Ticks == other.Ticks;
		}

		public int CompareTo(Time other)
		{
			EnsureSameBalance(Balance, other.Balance);

			int result = Year.CompareTo(other.Year);
			if (result == 0)
			{
				result = Month.CompareTo(other.Month);
			}
			if (result == 0)
			{
				result = Week.CompareTo(other.Week);
			}
			if (result == 0)
			{
				result = Day.CompareTo(other.Day);
			}
			if (result == 0)
			{
				result = Ticks.CompareTo(other.Ticks);
			}
			return result;
		}

		public static bool operator >(Time a, Time b)
		{
			return a.CompareTo(b) > 0;
		}

		public static bool operator <(Time a, Time b)
		{
			return a.CompareTo(b) < 0;
		}

		public static bool operator >=(Time a, Time b)
		{
			return a.CompareTo(b) >= 0;
		}

		public static bool operator <=(Time a, Time b)
		{
			return a.CompareTo(b) <= 0;
		}

		public static bool operator ==(Time a, Time b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Time a, Time b)
		{
			return !a.Equals(b);
		}

		public override bool Equals(object obj)
		{
			return base.Equals((Time) obj);
		}

		public override int GetHashCode()
		{
			return (Year ^ Month ^ Week ^ Day ^ Ticks).GetHashCode();
		}

		#endregion

		#region Mathematics

		public Time AddYears(int delta)
		{
			if (delta == 0) return this;

			return new Time(
				Balance,
				Year + delta,
				Month,
				Week,
				Day,
				Ticks);
		}

		public Time AddMonths(int delta)
		{
			if (delta == 0) return this;

			int thisLevelValue = Month + delta;
			int nextLevelDelta;
			int multiplier = Balance.MonthInYear;

			if (thisLevelValue < 0)
			{
				nextLevelDelta = - (int) Math.Ceiling((double) Math.Abs(thisLevelValue) / multiplier);
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else if (thisLevelValue >= multiplier)
			{
				nextLevelDelta = thisLevelValue / multiplier;
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else
			{
				nextLevelDelta = 0;
			}

			return new Time(
				Balance,
				Year,
				(byte) thisLevelValue,
				Week,
				Day,
				Ticks).AddYears(nextLevelDelta);
		}

		public Time AddWeeks(int delta)
		{
			if (delta == 0) return this;

			int thisLevelValue = Week + delta;
			int nextLevelDelta;
			int multiplier = Balance.WeeksInMonth;

			if (thisLevelValue < 0)
			{
				nextLevelDelta = - (int) Math.Ceiling((double) Math.Abs(thisLevelValue) / multiplier);
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else if (thisLevelValue >= multiplier)
			{
				nextLevelDelta = thisLevelValue / multiplier;
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else
			{
				nextLevelDelta = 0;
			}

			return new Time(
				Balance,
				Year,
				Month,
				(byte) thisLevelValue,
				Day,
				Ticks).AddMonths(nextLevelDelta);
		}

		public Time AddDays(int delta)
		{
			if (delta == 0) return this;

			int thisLevelValue = Day + delta;
			int nextLevelDelta;
			int multiplier = Balance.DaysInWeek;

			if (thisLevelValue < 0)
			{
				nextLevelDelta = - (int) Math.Ceiling((double) Math.Abs(thisLevelValue) / multiplier);
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else if (thisLevelValue >= multiplier)
			{
				nextLevelDelta = thisLevelValue / multiplier;
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else
			{
				nextLevelDelta = 0;
			}

			return new Time(
				Balance,
				Year,
				Month,
				Week,
				(byte) thisLevelValue,
				Ticks).AddWeeks(nextLevelDelta);
		}

		public Time AddTicks(long delta)
		{
			if (delta == 0) return this;

			long thisLevelValue = Ticks + delta;
			long nextLevelDelta;
			uint multiplier = Balance.TicksInDay;

			if (thisLevelValue < 0)
			{
				nextLevelDelta = - (int) Math.Ceiling((double) Math.Abs(thisLevelValue) / multiplier);
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else if (thisLevelValue >= multiplier)
			{
				nextLevelDelta = thisLevelValue / multiplier;
				thisLevelValue -= nextLevelDelta * multiplier;
			}
			else
			{
				nextLevelDelta = 0;
			}

			return new Time(
				Balance,
				Year,
				Month,
				Week,
				Day,
				(uint) thisLevelValue).AddDays((int) nextLevelDelta);
		}

		public Time Scale(double rate)
		{
			return FromTicks(Balance, (long) Math.Min(Math.Max(Math.Floor(TotalTicks * rate), long.MinValue), long.MaxValue));
		}

		public Time Date()
		{
			return new Time(
				Balance,
				Year,
				Month,
				Week,
				Day,
				0);
		}

		public Time DayTime()
		{
			return new Time(
				Balance,
				0,
				0,
				0,
				0,
				Ticks);
		}

		public static Time operator +(Time a, Time b)
		{
			return a
				.AddYears(b.Year)
				.AddMonths(b.Month)
				.AddWeeks(b.Week)
				.AddDays(b.Day)
				.AddTicks(b.Ticks);
		}

		public static Time operator -(Time a, Time b)
		{
			return a
				.AddYears(-b.Year)
				.AddMonths(-b.Month)
				.AddWeeks(-b.Week)
				.AddDays(-b.Day)
				.AddTicks(-b.Ticks);
		}

		public static Time operator *(Time time, double rate)
		{
			return time.Scale(rate);
		}

		public static Time operator /(Time time, double rate)
		{
			return time.Scale(1 / rate);
		}

		#endregion

		public override string ToString()
		{
			return $"DATE {Year}.{Month + 1}.{MonthDay + 1} TIME {Ticks}";
		}
	}
}
