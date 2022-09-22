using System;

using NUnit.Framework;

using Roguelike.Core;
using Roguelike.Core.Configuration;

namespace Roguelike.Tests
{
	public class TimeTest
	{
		private static readonly TimeBalance _balance = TimeBalance.CreateDefault();

		#region Constructors

		[Test]
		public void PossibleCreateCorrectDate()
		{
			// arrange
			int correctYear = 0;
			byte correctMonth = (byte) (_balance.MonthInYear - 1);
			byte correctWeek = (byte) (_balance.WeeksInMonth - 1);
			byte correctDay = (byte) (_balance.DaysInWeek - 1);
			uint correctTicks = _balance.TicksInDay - 1;

			// act && assert
			Assert.DoesNotThrow(() => new Time(_balance, 0, 0, 0, 0, 0));
			Assert.DoesNotThrow(() => new Time(_balance, correctYear, correctMonth, correctWeek, correctDay, correctTicks));
			Assert.DoesNotThrow(() => new Time(_balance, 0, 0, 0, 0));
			Assert.DoesNotThrow(() => new Time(_balance, correctYear, correctMonth, correctWeek, correctDay));
			Assert.DoesNotThrow(() => new Time(_balance));
		}

		[Test]
		public void ImpossibleCreateWrongDate()
		{
			// act && assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, 1, 1, _balance.TicksInDay));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, 1, 1, _balance.TicksInDay + 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, 1, (byte) _balance.DaysInWeek));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, 1, (byte) (_balance.DaysInWeek + 1)));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, (byte) _balance.WeeksInMonth, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, 1, (byte) (_balance.WeeksInMonth + 1), 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, (byte) _balance.MonthInYear, 1, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => new Time(_balance, 1, (byte) (_balance.MonthInYear + 1), 1, 1));
		}

		[Test]
		public void CheckFromYears()
		{
			// act
			var time = Time.FromYears(_balance, 3);

			// assert
			Assert.AreEqual(3, time.Year);
			Assert.AreEqual(0, time.Month);
			Assert.AreEqual(0, time.Week);
			Assert.AreEqual(0, time.Day);
			Assert.AreEqual(0, time.Ticks);
		}

		[Test]
		public void CheckFromMonths()
		{
			// act
			var time = Time.FromMonths(_balance, 3);

			// assert
			Assert.AreEqual(0, time.Year);
			Assert.AreEqual(3, time.Month);
			Assert.AreEqual(0, time.Week);
			Assert.AreEqual(0, time.Day);
			Assert.AreEqual(0, time.Ticks);
		}

		[Test]
		public void CheckFromWeeks()
		{
			// act
			var time = Time.FromWeeks(_balance, 3);

			// assert
			Assert.AreEqual(0, time.Year);
			Assert.AreEqual(0, time.Month);
			Assert.AreEqual(3, time.Week);
			Assert.AreEqual(0, time.Day);
			Assert.AreEqual(0, time.Ticks);
		}

		[Test]
		public void CheckFromDays()
		{
			// act
			var time = Time.FromDays(_balance, 3);

			// assert
			Assert.AreEqual(0, time.Year);
			Assert.AreEqual(0, time.Month);
			Assert.AreEqual(0, time.Week);
			Assert.AreEqual(3, time.Day);
			Assert.AreEqual(0, time.Ticks);
		}

		[Test]
		public void CheckFromTicks()
		{
			// act
			var time = Time.FromTicks(_balance, 3);

			// assert
			Assert.AreEqual(0, time.Year);
			Assert.AreEqual(0, time.Month);
			Assert.AreEqual(0, time.Week);
			Assert.AreEqual(0, time.Day);
			Assert.AreEqual(3, time.Ticks);
		}

		#endregion

		#region Comparison

		[Test]
		public void EqualsIsTrueOnlyIfAllFieldsAreEqual()
		{
			// act && assert
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2).Equals(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 1, 2, 2, 2, 2).Equals(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 1, 2, 2, 2).Equals(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 1, 2, 2).Equals(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 1, 2).Equals(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 1).Equals(new Time(_balance, 2, 2, 2, 2, 2)));

			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 1, 2, 2, 2, 2).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 1, 2, 2, 2).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 1, 2, 2).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 1, 2).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 1).Equals((object) new Time(_balance, 2, 2, 2, 2, 2)));
		}

		[Test]
		public void CompareMethodsWorkCorrect()
		{
			// act && assert
			Assert.AreEqual(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Less(0, new Time(_balance, 3, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Less(0, new Time(_balance, 2, 3, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Less(0, new Time(_balance, 2, 2, 3, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Less(0, new Time(_balance, 2, 2, 2, 3, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Less(0, new Time(_balance, 2, 2, 2, 2, 3).CompareTo(new Time(_balance, 2, 2, 2, 2, 2)));
			Assert.Greater(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 3, 2, 2, 2, 2)));
			Assert.Greater(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 3, 2, 2, 2)));
			Assert.Greater(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 3, 2, 2)));
			Assert.Greater(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 3, 2)));
			Assert.Greater(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 2, 2, 2, 2, 3)));
			Assert.Less(0, new Time(_balance, 2, 2, 2, 2, 2).CompareTo(new Time(_balance, 1, 3, 3, 3, 3)));
		}

		[Test]
		public void CompareOperatorsWorkCorrect()
		{
			// act && assert
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) > new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsFalse(new Time(_balance, 1, 1, 1, 1, 1) > new Time(_balance, 2, 2, 2, 2, 2));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) > new Time(_balance, 2, 2, 2, 2, 2));

			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) < new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsTrue(new Time(_balance, 1, 1, 1, 1, 1) < new Time(_balance, 2, 2, 2, 2, 2));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) < new Time(_balance, 2, 2, 2, 2, 2));

			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) >= new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsFalse(new Time(_balance, 1, 1, 1, 1, 1) >= new Time(_balance, 2, 2, 2, 2, 2));
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) >= new Time(_balance, 2, 2, 2, 2, 2));

			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) <= new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsTrue(new Time(_balance, 1, 1, 1, 1, 1) <= new Time(_balance, 2, 2, 2, 2, 2));
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) <= new Time(_balance, 2, 2, 2, 2, 2));

			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) == new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) == new Time(_balance, 2, 2, 2, 2, 2));
			Assert.IsTrue(new Time(_balance, 2, 2, 2, 2, 2) != new Time(_balance, 1, 1, 1, 1, 1));
			Assert.IsFalse(new Time(_balance, 2, 2, 2, 2, 2) != new Time(_balance, 2, 2, 2, 2, 2));
		}

		[Test]
		public void ImpossibleToCompareDifferentScaledTime()
		{
			// arrange
			var otherBalance = new TimeBalance
			{
				BeginYear = 1000,
				MonthInYear = 11,
				SeasonCount = 8,
				WeeksInMonth = 22,
				DaysInWeek = 11,
				TicksInDay = 10 * 10 * 10 * 100,
				DaytimeCount = 4,
			};

			var time = new Time(_balance, 1, 2, 3, 4, 5);
			var otherTime = new Time(otherBalance, 1, 2, 3, 4, 5);

			// act && assert
			Assert.Throws<InvalidOperationException>(() => time.CompareTo(otherTime));
			Assert.Throws<InvalidOperationException>(() => time.Equals(otherTime));
		}

		#endregion

		#region Mathematics

		[Test]
		public void DateWorksCorrect()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var dateOnly = time.Date();

			// assert
			Assert.AreEqual(1, dateOnly.Year);
			Assert.AreEqual(2, dateOnly.Month);
			Assert.AreEqual(3, dateOnly.Week);
			Assert.AreEqual(4, dateOnly.Day);
			Assert.AreEqual(0, dateOnly.Ticks);
		}

		[Test]
		public void DayTimeWorksCorrect()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var timeOnly = time.DayTime();

			// assert
			Assert.AreEqual(0, timeOnly.Year);
			Assert.AreEqual(0, timeOnly.Month);
			Assert.AreEqual(0, timeOnly.Week);
			Assert.AreEqual(0, timeOnly.Day);
			Assert.AreEqual(5, timeOnly.Ticks);
		}

		[Test]
		public void AddYearsJustChangesYears()
		{
			// arrange
			var time = new Time(_balance, 123456789, 2, 3, 4, 5);

			// act
			var sameTime = time.AddYears(0);
			var increasedTime = time.AddYears(876543211);
			var decreasedTime = time.AddYears(-123456789);
			var negativeTime = time.AddYears(-123456790);

			// assert
			Assert.AreEqual(time, sameTime);
			Assert.AreEqual(new Time(_balance, 1000000000, 2, 3, 4, 5), increasedTime);
			Assert.AreEqual(new Time(_balance, 0, 2, 3, 4, 5), decreasedTime);
			Assert.AreEqual(new Time(_balance, -1, 2, 3, 4, 5), negativeTime);
		}

		[Test]
		public void AddZeroMonthsReturnsTheSame()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddMonths(0);

			// assert
			Assert.AreEqual(time, newTime);
		}

		[Test]
		public void AddMonthsWithoutSwitchingYear()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddMonths(2);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 4, 3, 4, 5), newTime);
		}

		[Test]
		public void AddMonthsWithSwitchingYear()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddMonths(23);

			// assert
			Assert.AreEqual(new Time(_balance, 3, 1, 3, 4, 5), newTime);
		}

		[Test]
		public void SubMonthsWithoutSwitchingYear()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddMonths(-1);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 1, 3, 4, 5), newTime);
		}

		[Test]
		public void SubMonthsWithSwitchingYear()
		{
			// arrange
			var time = new Time(_balance, 5, 2, 3, 4, 5);

			// act
			var newTime = time.AddMonths(-27);

			// assert
			Assert.AreEqual(new Time(_balance, 2, 11, 3, 4, 5), newTime);
		}

		[Test]
		public void AddWeeksWithoutSwitchingMonth()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 1, 4, 5);

			// act
			var newTime = time.AddWeeks(2);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 4, 5), newTime);
		}

		[Test]
		public void AddWeeksWithSwitchingMonth()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddWeeks(2);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 3, 1, 4, 5), newTime);
		}

		[Test]
		public void SubWeeksWithoutSwitchingMonth()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddWeeks(-1);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 2, 4, 5), newTime);
		}

		[Test]
		public void SubWeeksWithSwitchingMonth()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddWeeks(-4);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 1, 3, 4, 5), newTime);
		}

		[Test]
		public void AddDaysWithoutSwitchingWeek()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddDays(2);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 6, 5), newTime);
		}

		[Test]
		public void AddDaysWithSwitchingWeek()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 2, 4, 5);

			// act
			var newTime = time.AddDays(4);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 1, 5), newTime);
		}

		[Test]
		public void SubDaysWithoutSwitchingWeek()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddDays(-3);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 1, 5), newTime);
		}

		[Test]
		public void SubDaysWithSwitchingWeek()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddDays(-6);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 2, 5, 5), newTime);
		}

		[Test]
		public void AddTicksWithoutSwitchingDay()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddTicks(10);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 4, 15), newTime);
		}

		[Test]
		public void AddTicksWithSwitchingDay()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddTicks(1 + _balance.TicksInDay * 2);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 6, 6), newTime);
		}

		[Test]
		public void SubTicksWithoutSwitchingDay()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddTicks(-5);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 4, 0), newTime);
		}

		[Test]
		public void SubTicksWithSwitchingDay()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddTicks(-5 - _balance.TicksInDay);

			// assert
			Assert.AreEqual(new Time(_balance, 1, 2, 3, 3, 0), newTime);
		}

		[Test]
		public void AddTicksWithSwitchingYear()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			long increment = 1;
			increment += 1 * _balance.TicksInDay;
			increment += 1 * _balance.TicksInDay * _balance.DaysInWeek * _balance.WeeksInMonth;
			increment += 10 * _balance.TicksInDay * _balance.DaysInWeek * _balance.WeeksInMonth * _balance.MonthInYear;

			// act
			var newTime = time.AddTicks(increment);

			// assert
			Assert.AreEqual(new Time(_balance, 11, 3, 3, 5, 6), newTime);
		}

		[Test]
		public void AddZeroTicksReturnsTheSame()
		{
			// arrange
			var time = new Time(_balance, 1, 2, 3, 4, 5);

			// act
			var newTime = time.AddTicks(0);

			// assert
			Assert.AreEqual(time, newTime);
		}

		[Test]
		public void PlusJustAdds()
		{
			// arrange
			var time1 = new Time(_balance, 12, 1, 1, 1, 1);
			var time2 = new Time(_balance, 21, 2, 2, 2, 2);

			// act
			var result = time1 + time2;

			// assert
			Assert.AreEqual(new Time(_balance, 33, 3, 3, 3, 3), result);
		}

		[Test]
		public void MinusJustSubtracts()
		{
			// arrange
			var time1 = new Time(_balance, 33, 3, 3, 3, 3);
			var time2 = new Time(_balance, 22, 2, 2, 2, 2);

			// act
			var result = time1 - time2;

			// assert
			Assert.AreEqual(new Time(_balance, 11, 1, 1, 1, 1), result);
		}

		#endregion

		[Test]
		public void ToStringLooksUnderstandable()
		{
			// arrange
			var time = new Time(_balance, 1234, 3, 1, 1, 100);

			// act
			string result = time.ToString();

			// assert
			Assert.AreEqual("DATE 1234.4.9 TIME 100", result);
		}

		[Test]
		public void MontDayIzAlsoZeroBased()
		{
			// arrange
			var time = new Time(_balance, 1234, 3, 1, 1, 100);

			// act & assert
			Assert.AreEqual(8, time.MonthDay);
		}
	}
}