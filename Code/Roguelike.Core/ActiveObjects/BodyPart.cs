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

		private readonly Func<Language, string> getName;

		public double Weight
		{ get; }

		public event ValueChangedEventHandler<IRequireGravitation, double> WeightChanged;

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
			IsVital = isVital;

			Parts = new ReadOnlyCollection<IBodyPart>(parts);
			foreach (var part in parts)
			{
				part.WeightChanged += onPartWeightChanged;
			}
		}

		#endregion

		protected void RaiseWeightChanged(double oldWeight, double newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		private void onPartWeightChanged(IRequireGravitation part, double oldPartWeight, double newPartWeight)
		{
			double newWeight = Weight;
			double oldWeigth = newWeight - newPartWeight + oldPartWeight;
			RaiseWeightChanged(oldWeigth, newWeight);
		}

		public string GetName(Language language)
		{
			return getName(language);
		}
	}
}