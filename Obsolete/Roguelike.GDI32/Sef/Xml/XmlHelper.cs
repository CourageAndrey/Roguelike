using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Sef.Xml
{
	public static class XmlHelper
	{
		#region DOM

		#region Constants

		public const String UsedXmlVersion = "1.0";

		public const String UsedXmlEncoding = "UTF-8";

		#endregion

		#region Document

		public static XmlDocument CreateNewDocument(String rootName)
		{
			var document = CreateNewDocument();
			document.AppendChild(document.CreateElement(rootName));
			return document;
		}

		public static XmlDocument CreateNewDocument()
		{
			var document = new XmlDocument();
			document.AppendChild(document.CreateXmlDeclaration(UsedXmlVersion, UsedXmlEncoding, null));
			return document;
		}

		public static XmlElement GetRoot(this XmlDocument document)
		{
			return document.DocumentElement;
		}

		#endregion

		#region Child elements

		public static IList<XmlElement> SelectChildren(this XmlElement element, String name = null)
		{
			var elements = element.ChildNodes.OfType<XmlElement>();
			return (String.IsNullOrEmpty(name) ? elements : elements.Where(e => (e.LocalName == name))).ToList();
		}

		#endregion

		#region Text

		private static readonly Char[] trimmedChars = { ' ', '\r', '\n', '\t' };

		public static String GetElementText(this XmlElement element, Boolean trim)
		{
			foreach (var textNode in element.ChildNodes.OfType<XmlText>())
			{
				return trim ? textNode.Value.Trim(trimmedChars) : textNode.Value;
			}
			return null;
		}

		public static void SetElementText(this XmlElement element, XmlDocument document, String text)
		{
			element.AppendChild(document.CreateTextNode(text));
		}

		#endregion

		#region Attributes

		public static void SetAttribute<T>(this XmlElement element, String attribute, T value)
		{
			element.SetAttribute(attribute, (string) Convert.ChangeType(value, typeof(string)));
		}

		public static T GetAttribute<T>(this XmlElement element, String attribute)
		{
			return (T) Convert.ChangeType(element.GetAttribute(attribute), typeof(T));
		}

		#endregion

		#region Images

		public static Image LoadImage(this XmlElement element)
		{
			return Image.FromStream(new MemoryStream(Convert.FromBase64String(element.InnerText.discardWhiteSpaces())));
		}

		public static void SaveImage(this XmlElement element, Image image, ImageFormat imageFormat)
		{
			var stream = new MemoryStream();
			image.Save(stream, imageFormat);
			element.InnerText = Convert.ToBase64String(stream.ToArray());
		}

		private static String discardWhiteSpaces(this String text)
		{
			if (!String.IsNullOrEmpty(text))
			{
				var result = new Char[text.Count(c => !Char.IsWhiteSpace(c))];
				Int32 index = 0;
				foreach (var c in text)
				{
					if (!Char.IsWhiteSpace(c))
					{
						result[index++] = c;
					}
				}
				return new String(result);
			}
			else
			{
				return text;
			}
		}

		#endregion

		#endregion

		#region Simple attributes

		#region Serializers cache

		private static readonly Dictionary<Type, XmlSerializer> serializers = new Dictionary<Type, XmlSerializer>();

		private static readonly Object serializersLock = new Object();

		public static XmlSerializer AcquireSerializer<T>()
		{
			return AcquireSerializer(typeof(T));
		}

		public static XmlSerializer AcquireSerializer(Type type)
		{
			lock (serializersLock)
			{
				XmlSerializer serializer;
				if (!serializers.TryGetValue(type, out serializer))
				{
					serializer = serializers[type] = new XmlSerializer(type);
				}
				return serializer;
			}
		}

		#endregion

		#region Serialization

		public static XmlDocument SerializeToDocument(this Object entity)
		{
			var serializer = AcquireSerializer(entity.GetType());
			var document = new XmlDocument();
			using (var writer = new StringWriter())
			{
				serializer.Serialize(writer, entity);
				document.LoadXml(writer.ToString());
			}
			if (document.DocumentElement != null)
			{
				document.DocumentElement.RemoveAttribute("xmlns:xsd");
				document.DocumentElement.RemoveAttribute("xmlns:xsi");
			}
			return document;
		}

		public static XmlElement SerializeToElement(this Object entity)
		{
			return entity.SerializeToDocument().DocumentElement;
		}

		public static void SerializeToFile(this Object entity, String fileName)
		{
			entity.SerializeToDocument().Save(fileName);
		}

		#endregion

        #region Deserialization

		public static T DeserializeFromStream<T>(this XmlReader reader)
		{
			return (T) AcquireSerializer<T>().Deserialize(reader);
		}

		public static T Deserialize<T>(this Byte[] bytes)
		{
			using (var stream = new MemoryStream(bytes))
			{
				using (var reader = XmlReader.Create(stream))
				{
					return reader.DeserializeFromStream<T>();
				}
			}
		}

		public static T DeserializeFromFile<T>(this String file)
		{
			using (var xmlFile = new XmlTextReader(file))
			{
				return DeserializeFromStream<T>(xmlFile);
			}
		}

		public static T DeserializeFromText<T>(this String xml)
		{
			using (var stringReader = new StringReader(xml))
			{
				using (var xmlStringReader = new XmlTextReader(stringReader))
				{
					return xmlStringReader.DeserializeFromStream<T>();
				}
			}
		}

		#endregion

		#endregion
	}
}
