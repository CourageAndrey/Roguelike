using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Aspects
{
	public class Body : IAspect, IVariableMassy
	{
		#region Properties

		public ICollection<BodyPart> Parts
		{ get; }

		public decimal Weight
		{ get { return Parts.Sum(part => part.Weight); } }

		public event ValueChangedEventHandler<IMassy, decimal> WeightChanged;

		protected void RaiseWeightChanged(decimal oldWeight, decimal newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		#endregion

		private Body(IEnumerable<BodyPart> parts)
		{
			var collection = new EventCollection<BodyPart>(parts);
			collection.ItemAdded += (sender, args) =>
			{
				args.Item.WeightChanged += onPartWeightChanged;
				onPartWeightChanged(args.Item, 0, args.Item.Weight);
			};
			collection.ItemRemoved += (sender, args) =>
			{
				args.Item.WeightChanged -= onPartWeightChanged;
				onPartWeightChanged(args.Item, args.Item.Weight, 0);
			};

			Parts = collection;

			foreach (var part in collection)
			{
				part.WeightChanged += onPartWeightChanged;
			}
		}

		private void onPartWeightChanged(IMassy part, decimal oldPartWeight, decimal newPartWeight)
		{
			decimal newWeight = Weight;
			decimal oldWeight = newWeight - newPartWeight + oldPartWeight;
			RaiseWeightChanged(oldWeight, newWeight);
		}

		#region Body constructors

		public static Body CreateHumanoid()
		{
			var parts = new List<BodyPart>();
			var body = new Body(parts);
			parts.AddRange(new[]
			{
				createUsualHead(body, 32),
				createUsualBody(body),
				createArm(body, 5),
				createArm(body, 5),
				createLeg(body, 5),
				createLeg(body, 5),
				createSkin(body),
			});
			return body;
		}

		public static Body CreateAnimal()
		{
			var parts = new List<BodyPart>();
			var body = new Body(parts);
			parts.AddRange(new[]
			{
				createUsualHead(body, 20),
				createUsualBody(body),
				createLeg(body, 5),
				createLeg(body, 5),
				createLeg(body, 5),
				createLeg(body, 5),
				createTail(body),
				createSkin(body),
			});
			return body;
		}

		private static List<IBodyPart> createFingers(Body body, int count)
		{
			var result = new List<IBodyPart>();
			for (int i = 0; i < count; i++)
			{
				result.Add(new BodyPart(body, 0.01m, language => language.Finger));
			}
			return result;
		}

		private static BodyPart createWrist(Body body, int fingersCount)
		{
			return new BodyPart(body, 0.25m, language => language.Wrist, createFingers(body, fingersCount));
		}

		private static BodyPart createFoot(Body body, int fingersCount)
		{
			return new BodyPart(body, 0.5m, language => language.Foot, createFingers(body, fingersCount));
		}

		private static BodyPart createArm(Body body, int fingersCount)
		{
			return new BodyPart(body, 5, language => language.Arm, new[]
			{
				new BodyPart(body, 2, language => language.Shoulder),
				new BodyPart(body, 0.5m, language => language.Elbow),
				new BodyPart(body, 1.5m, language => language.Forearm),
				createWrist(body, fingersCount),
			});
		}

		private static BodyPart createLeg(Body body, int fingersCount)
		{
			return new BodyPart(body, 10, language => language.Leg, new[]
			{
				new BodyPart(body, 5, language => language.Haunch),
				new BodyPart(body, 0.75m, language => language.Knee),
				new BodyPart(body, 3, language => language.Shin),
				createFoot(body, fingersCount),
			});
		}

		private static BodyPart createUsualBody(Body body)
		{
			return new BodyPart(body, 40, language => language.Body, new[]
			{
				new BodyPart(body, 2, language => language.Ribs),
				new BodyPart(body, 1, language => language.Heart, true),
				new BodyPart(body, 1, language => language.Lung),
				new BodyPart(body, 1, language => language.Lung),
				new BodyPart(body, 2, language => language.Liver, true),
				new BodyPart(body, 1, language => language.Stomach),
				new BodyPart(body, 0.3m, language => language.Kidney),
				new BodyPart(body, 0.3m, language => language.Kidney),
			}, true);
		}

		private static BodyPart createUsualHead(Body body, int teeth)
		{
			var parts = new List<IBodyPart>
			{
				new BodyPart(body, 1, language => language.Skull),
				new BodyPart(body, 1.5m, language => language.Brain, true),
				new BodyPart(body, 0.25m, language => language.Hairs),
				new BodyPart(body, 0.05m, language => language.Ear),
				new BodyPart(body, 0.05m, language => language.Ear),
				new BodyPart(body, 0.015m, language => language.Eye),
				new BodyPart(body, 0.015m, language => language.Eye),
				new BodyPart(body, 0.02m, language => language.Mouth),
				new BodyPart(body, 0.05m, language => language.Tongue),
				new BodyPart(body, 0.1m, language => language.Throat),
			};
			for (int i = 0; i < teeth; i++)
			{
				parts.Add(new BodyPart(body, 0.005m, language => language.Tooth));
			}
			return new BodyPart(body, 5, language => language.Head, parts, true);
		}

		private static BodyPart createSkin(Body body)
		{
			return new BodyPart(body, 5, language => language.Skin);
		}

		private static BodyPart createTail(Body body)
		{
			return new BodyPart(body, 5, language => language.Tail);
		}

		#endregion
	}
}
