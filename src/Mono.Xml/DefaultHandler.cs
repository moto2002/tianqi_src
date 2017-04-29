using System;

namespace Mono.Xml
{
	internal class DefaultHandler : SmallXmlParser.IContentHandler
	{
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
		}

		public void OnEndElement(string name)
		{
		}

		public void OnChars(string s)
		{
		}

		public void OnIgnorableWhitespace(string s)
		{
		}

		public void OnProcessingInstruction(string name, string text)
		{
		}
	}
}
