using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageBodyParts
	{
		#region Properties

		[XmlElement]
		public string Arm
		{ get; set; }
		[XmlElement]
		public string Leg
		{ get; set; }
		[XmlElement]
		public string Tail
		{ get; set; }
		[XmlElement]
		public string Wing
		{ get; set; }
		[XmlElement]
		public string Body
		{ get; set; }
		[XmlElement]
		public string Tentacle
		{ get; set; }
		[XmlElement]
		public string Head
		{ get; set; }
		[XmlElement]
		public string Face
		{ get; set; }
		[XmlElement]
		public string Eye
		{ get; set; }
		[XmlElement]
		public string Ear
		{ get; set; }
		[XmlElement]
		public string Nose
		{ get; set; }
		[XmlElement]
		public string Hoove
		{ get; set; }
		[XmlElement]
		public string Horn
		{ get; set; }
		[XmlElement]
		public string Finger
		{ get; set; }
		[XmlElement]
		public string Wrist
		{ get; set; }
		[XmlElement]
		public string Foot
		{ get; set; }
		[XmlElement]
		public string Shoulder
		{ get; set; }
		[XmlElement]
		public string Elbow
		{ get; set; }
		[XmlElement]
		public string Forearm
		{ get; set; }
		[XmlElement]
		public string Haunch
		{ get; set; }
		[XmlElement]
		public string Knee
		{ get; set; }
		[XmlElement]
		public string Shin
		{ get; set; }
		[XmlElement]
		public string Ribs
		{ get; set; }
		[XmlElement]
		public string Heart
		{ get; set; }
		[XmlElement]
		public string Lung
		{ get; set; }
		[XmlElement]
		public string Liver
		{ get; set; }
		[XmlElement]
		public string Stomach
		{ get; set; }
		[XmlElement]
		public string Kidney
		{ get; set; }
		[XmlElement]
		public string Skull
		{ get; set; }
		[XmlElement]
		public string Brain
		{ get; set; }
		[XmlElement]
		public string Hairs
		{ get; set; }
		[XmlElement]
		public string Mouth
		{ get; set; }
		[XmlElement]
		public string Tongue
		{ get; set; }
		[XmlElement]
		public string Throat
		{ get; set; }
		[XmlElement]
		public string Tooth
		{ get; set; }
		[XmlElement]
		public string Skin
		{ get; set; }

		#endregion

		public static LanguageBodyParts CreateDefault()
		{
			return new LanguageBodyParts
			{
				Arm = "Arm",
				Leg = "Leg",
				Tail = "Tail",
				Wing = "Wing",
				Body = "Body",
				Tentacle = "Tentacle",
				Head = "Head",
				Face = "Face",
				Eye = "Eye",
				Ear = "Ear",
				Nose = "Nose",
				Hoove = "Hoove",
				Horn = "Horn",
				Finger = "Finger",
				Wrist = "Wrist",
				Foot = "Foot",
				Shoulder = "Shoulder",
				Elbow = "Elbow",
				Forearm = "Forearm",
				Haunch = "Haunch",
				Knee = "Knee",
				Shin = "Shin",
				Ribs = "Ribs",
				Heart = "Heart",
				Lung = "Lung",
				Liver = "Liver",
				Stomach = "Stomach",
				Kidney = "Kidney",
				Skull = "Skull",
				Brain = "Brain",
				Hairs = "Hairs",
				Mouth = "Mouth",
				Tongue = "Tongue",
				Throat = "Throat",
				Tooth = "Tooth",
				Skin = "Skin",
			};
		}
	}
}
