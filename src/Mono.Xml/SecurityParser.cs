using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	public class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		private SecurityElement root;

		private SecurityElement current;

		private Stack stack;

		public SecurityParser()
		{
			this.stack = new Stack();
		}

		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		public SecurityElement ToXml()
		{
			return this.root;
		}

		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		public void OnProcessingInstruction(string name, string text)
		{
		}

		public void OnIgnorableWhitespace(string s)
		{
		}

		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				SecurityElement securityElement2 = (SecurityElement)this.stack.Peek();
				securityElement2.AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), attrs.GetValue(i));
			}
		}

		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		public void OnChars(string ch)
		{
			this.current.set_Text(ch);
		}

		public void OnEndParsing(SmallXmlParser parser)
		{
		}
	}
}
