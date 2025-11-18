using System.Collections.Generic;

namespace Roguelike.Console
{
	public static class TextWrapper
	{
		public static List<string> WrapIfNecessary(this ICollection<string> lines, int displayWidth)
		{
			var wrappedLines = new List<string>();

			foreach (string line in lines)
			{
				string rest = line;
				while (rest.Length > displayWidth)
				{
					string part = rest.Remove(displayWidth);
					int breakPos = part.LastIndexOf(' ');

					if (breakPos >= 0)
					{
						part = part.Remove(breakPos);
						breakPos++;
					}
					else
					{
						breakPos = displayWidth;
					}

					wrappedLines.Add(part);
					rest = rest.Substring(breakPos);
				}
				wrappedLines.Add(rest);
			}

			return wrappedLines;
		}
	}
}
