using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Body : IBody
	{
		#region Properties

		public ICollection<BodyPart> Parts
		{ get; }

		public double Weight
		{ get { return Parts.Sum(part => part.Weight); } }

		#endregion

		private Body(ICollection<BodyPart> parts)
		{
			Parts = parts;
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
				result.Add(new BodyPart(body, 0.01, language => language.BodyPartFinger));
			}
			return result;
		}

		private static BodyPart createWrist(Body body, int fingersCount)
		{
			return new BodyPart(body, 0.25, language => language.BodyPartWrist, createFingers(body, fingersCount));
		}

		private static BodyPart createFoot(Body body, int fingersCount)
		{
			return new BodyPart(body, 0.5, language => language.BodyPartFoot, createFingers(body, fingersCount));
		}

		private static BodyPart createArm(Body body, int fingersCount)
		{
			return new BodyPart(body, 5, language => language.BodyPartArm, new[]
			{
				new BodyPart(body, 2, language => language.BodyPartShoulder),
				new BodyPart(body, 0.5, language => language.BodyPartElbow),
				new BodyPart(body, 1.5, language => language.BodyPartForearm),
				createWrist(body, fingersCount),
			});
		}

		private static BodyPart createLeg(Body body, int fingersCount)
		{
			return new BodyPart(body, 10, language => language.BodyPartLeg, new[]
			{
				new BodyPart(body, 5, language => language.BodyPartHaunch),
				new BodyPart(body, 0.75, language => language.BodyPartKnee),
				new BodyPart(body, 3, language => language.BodyPartShin),
				createFoot(body, fingersCount),
			});
		}

		private static BodyPart createUsualBody(Body body)
		{
			return new BodyPart(body, 40, language => language.BodyPartBody, new[]
			{
				new BodyPart(body, 2, language => language.BodyPartRibs),
				new BodyPart(body, 1, language => language.BodyPartHeart, true),
				new BodyPart(body, 1, language => language.BodyPartLung),
				new BodyPart(body, 1, language => language.BodyPartLung),
				new BodyPart(body, 2, language => language.BodyPartLiver, true),
				new BodyPart(body, 1, language => language.BodyPartStomach),
				new BodyPart(body, 0.3, language => language.BodyPartKidney),
				new BodyPart(body, 0.3, language => language.BodyPartKidney),
			}, true);
		}

		private static BodyPart createUsualHead(Body body, int teeth)
		{
			var parts = new List<IBodyPart>
			{
				new BodyPart(body, 1, language => language.BodyPartSkull),
				new BodyPart(body, 1.5, language => language.BodyPartBrain, true),
				new BodyPart(body, 0.25, language => language.BodyPartHairs),
				new BodyPart(body, 0.05, language => language.BodyPartEar),
				new BodyPart(body, 0.05, language => language.BodyPartEar),
				new BodyPart(body, 0.015, language => language.BodyPartEye),
				new BodyPart(body, 0.015, language => language.BodyPartEye),
				new BodyPart(body, 0.02, language => language.BodyPartMouth),
				new BodyPart(body, 0.05, language => language.BodyPartTongue),
				new BodyPart(body, 0.1, language => language.BodyPartThroat),
			};
			for (int i = 0; i < teeth; i++)
			{
				parts.Add(new BodyPart(body, 0.005, language => language.BodyPartTooth));
			}
			return new BodyPart(body, 5, language => language.BodyPartHead, parts, true);
		}

		private static BodyPart createSkin(Body body)
		{
			return new BodyPart(body, 5, language => language.BodyPartSkin);
		}

		private static BodyPart createTail(Body body)
		{
			return new BodyPart(body, 5, language => language.BodyPartTail);
		}

		#endregion
	}
}
