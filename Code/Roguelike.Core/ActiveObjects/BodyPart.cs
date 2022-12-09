using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public class BodyPart : IBodyPart
	{
		#region Properties

		public IBody Body
		{ get; }

		public IReadOnlyCollection<IBodyPart> Parts
		{ get; }

		public bool IsVital
		{ get; }

		private readonly Func<LanguageBodyParts, string> getName;

		public decimal Weight
		{ get; }

		public event ValueChangedEventHandler<IMassy, decimal> WeightChanged;

		#endregion

		#region Constructors

		internal BodyPart(IBody body, decimal weight, Func<LanguageBodyParts, string> getName, bool isVital = false)
			: this(body, weight, getName, new IBodyPart[0], isVital)
		{ }

		internal BodyPart(IBody body, decimal weight, Func<LanguageBodyParts, string> getName, IList<IBodyPart> parts, bool isVital = false)
		{
			Body = body;
			Weight = weight;
			this.getName = getName;
			IsVital = isVital;

			Parts = new ReadOnlyCollection<IBodyPart>(parts);
			foreach (var part in parts)
			{
				part.WeightChanged += onPartWeightChanged;
			}
		}

		#endregion

		protected void RaiseWeightChanged(decimal oldWeight, decimal newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		private void onPartWeightChanged(IMassy part, decimal oldPartWeight, decimal newPartWeight)
		{
			decimal newWeight = Weight;
			decimal oldWeigth = newWeight - newPartWeight + oldPartWeight;
			RaiseWeightChanged(oldWeigth, newWeight);
		}

		public string GetName(LanguageBodyParts language)
		{
			return getName(language);
		}
	}
}