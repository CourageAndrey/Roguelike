using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Roguelike.Console;

namespace Roguelike.Tests
{
	[TestFixture]
	public class TextWrapperTest
	{
		[Test]
		public void WrapIfNecessary_ShortLine_ReturnsUnchanged()
		{
			// Arrange
			var lines = new List<string> { "Short line" };
			int displayWidth = 50;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual("Short line", result.Single());
		}

		[Test]
		public void WrapIfNecessary_ExactWidth_ReturnsUnchanged()
		{
			// Arrange
			var lines = new List<string> { "Exact" };
			int displayWidth = 5;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual("Exact", result.Single());
		}

		[Test]
		public void WrapIfNecessary_LongLineWithSpaces_BreaksAtWord()
		{
			// Arrange
			var lines = new List<string> { "This is a very long line that needs wrapping" };
			int displayWidth = 20;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual(result.Count, 3);
			Assert.AreEqual(result[0], "This is a very long");
			Assert.AreEqual(result[1], "line that needs");
			Assert.AreEqual(result[2], "wrapping");
		}

		[Test]
		public void WrapIfNecessary_LongLineNoSpaces_BreaksMidWord()
		{
			// Arrange
			var lines = new List<string> { "Thisisaverylongwordwithoutanyspaces" };
			int displayWidth = 10;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual(result.Count, 4);
			Assert.AreEqual(result[0], "Thisisaver");
			Assert.AreEqual(result[1], "ylongwordw");
			Assert.AreEqual(result[2], "ithoutanys");
			Assert.AreEqual(result[3], "paces");
		}

		[Test]
		public void WrapIfNecessary_EmptyLine_ReturnsEmptyLine()
		{
			// Arrange
			var lines = new List<string> { "" };
			int displayWidth = 50;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual("", result.Single());
		}

		[Test]
		public void WrapIfNecessary_SingleSpace_ReturnsSpace()
		{
			// Arrange
			var lines = new List<string> { " " };
			int displayWidth = 10;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual(" ", result.Single());
		}

		[Test]
		public void WrapIfNecessary_EmptyCollection_ReturnsEmptyList()
		{
			// Arrange
			var lines = new List<string>();
			int displayWidth = 10;

			// Act
			var result = lines.WrapIfNecessary(displayWidth);

			// Assert
			Assert.AreEqual(0, result.Count);
		}
	}
}
