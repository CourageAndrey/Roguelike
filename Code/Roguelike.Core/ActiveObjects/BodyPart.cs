using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public class BodyPart
	{
		#region Properties

		public Body Body
		{ get; }

		public IReadOnlyList<BodyPart> Parts
		{ get; }

		public bool IsVital
		{ get; }

		private readonly Func<Language, string> getName;

		public double Weight
		{ get; }

		#endregion

		#region Constructors

		internal BodyPart(Body body, double weight, Func<Language, string> getName, bool isVital = false)
			: this(body, weight, getName, new BodyPart[0], isVital)
		{ }

		internal BodyPart(Body body, double weight, Func<Language, string> getName, IList<BodyPart> parts, bool isVital = false)
		{
			Body = body;
			Weight = weight;
			this.getName = getName;
			Parts = new ReadOnlyCollection<BodyPart>(parts);
			IsVital = isVital;
		}

		#endregion

		public string GetName(Language language)
		{
			return getName(language);
		}
	}
}