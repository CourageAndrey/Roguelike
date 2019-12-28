using System;

using Roguelike.Core.Configuration;

namespace Roguelike.Core
{
	public class Time : IEquatable<Time>, IComparable<Time>
	{
		#region Properties

		public Balance Balance
		{ get; }

		public int Year
		{ get; }

		public byte Month
		{ get; }

		public byte Week
		{ get; }

		public byte Day
		{ get; }

		internal uint Ticks
		{ get; }

		#endregion

		#region Constructors

		public Time(Balance balance, int year, byte month, byte week, byte day, uint ticks)
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

		public Time(Balance balance, int year, byte month, byte week, byte day)
			: this(balance, year, month, week, day, 0)
		{ }

		public Time(Balance balance)
			: this(balance, 0, 0, 0, 0, 0)
		{ }

		public static Time FromYears(Balance balance, int value)
		{
			return new Time(balance).AddYears(value);
		}

		public static Time FromMonths(Balance balance, int value)
		{
			return new Time(balance).AddMonths(value);
		}

		public static Time FromWeeks(Balance balance, int value)
		{
			return new Time(balance).AddWeeks(value);
		}

		public static Time FromDays(Balance balance, int value)
		{
			return new Time(balance).AddDays(value);
		}

		public static Time FromTicks(Balance balance, int value)
		{
			return new Time(balance).AddTicks(value);
		}

		#endregion

		#region Comparison

		public bool Equals(Time other)
		{
			return Year == other.Year &&
				   Month == other.Month &&
				   Week == other.Week &&
				   Day == other.Day &&
				   Ticks == other.Ticks;
		}

		public int CompareTo(Time other)
		{
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

		#endregion

		#region Mathematics

		public Time AddYears(int delta)
		{
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
			int nextLevelDelta = (Month + delta) / Balance.MonthInYear;
			int thisLevelValue = (Month + delta) % Balance.MonthInYear;
			var result = this;
			if (nextLevelDelta != 0)
			{
				result = result.AddYears(nextLevelDelta);
			}
			return new Time(
				Balance,
				result.Year,
				(byte)thisLevelValue,
				result.Week,
				result.Day,
				result.Ticks);
		}

		public Time AddWeeks(int delta)
		{
			int nextLevelDelta = (Week + delta) / Balance.WeeksInMonth;
			int thisLevelValue = (Week + delta) % Balance.WeeksInMonth;
			var result = this;
			if (nextLevelDelta != 0)
			{
				result = result.AddMonths(nextLevelDelta);
			}
			return new Time(
				Balance,
				result.Year,
				result.Month,
				(byte)thisLevelValue,
				result.Day,
				result.Ticks);
		}

		public Time AddDays(int delta)
		{
			int nextLevelDelta = (Day + delta) / Balance.DaysInWeek;
			int thisLevelValue = (Day + delta) % Balance.DaysInWeek;
			var result = this;
			if (nextLevelDelta != 0)
			{
				result = result.AddWeeks(nextLevelDelta);
			}
			return new Time(
				Balance,
				result.Year,
				result.Month,
				result.Week,
				(byte)thisLevelValue,
				result.Ticks);
		}

		public Time AddTicks(long delta)
		{
			int nextLevelDelta = (int)((Ticks + delta) / Balance.TicksInDay);
			long thisLevelValue = (Ticks + delta) % Balance.TicksInDay;
			var result = this;
			if (nextLevelDelta != 0)
			{
				result = result.AddDays(nextLevelDelta);
			}
			return new Time(
				Balance,
				result.Year,
				result.Month,
				result.Week,
				result.Day,
				(uint)thisLevelValue);
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

		#endregion
	}
}
