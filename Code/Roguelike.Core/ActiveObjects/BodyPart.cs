using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public class BodyPart : IBodyPart
	{
		#region Properties

		public IBody Body
		{ get; }

		public IReadOnlyList<IBodyPart> Parts
		{ get; }

		public bool IsVital
		{ get; }

		private readonly Func<Language, string> getName;

		public double Weight
		{ get; }

		#endregion

		#region Constructors

		internal BodyPart(IBody body, double weight, Func<Language, string> getName, bool isVital = false)
			: this(body, weight, getName, new IBodyPart[0], isVital)
		{ }

		internal BodyPart(IBody body, double weight, Func<Language, string> getName, IList<IBodyPart> parts, bool isVital = false)
		{
			Body = body;
			Weight = weight;
			this.getName = getName;
			Parts = new ReadOnlyCollection<IBodyPart>(parts);
			IsVital = isVital;
		}

		#endregion

		public string GetName(Language language)
		{
			return getName(language);
		}
	}
}