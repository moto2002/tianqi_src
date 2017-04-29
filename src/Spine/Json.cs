using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Spine
{
	public static class Json
	{
		private sealed class Parser : IDisposable
		{
			private enum TOKEN
			{
				NONE,
				CURLY_OPEN,
				CURLY_CLOSE,
				SQUARED_OPEN,
				SQUARED_CLOSE,
				COLON,
				COMMA,
				STRING,
				NUMBER,
				TRUE,
				FALSE,
				NULL
			}

			private const string WHITE_SPACE = " \t\n\r";

			private const string WORD_BREAK = " \t\n\r{}[],:\"";

			private TextReader json;

			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (" \t\n\r{}[],:\"".IndexOf(this.PeekChar) == -1)
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					char c = peekChar;
					switch (c)
					{
					case '"':
						return Json.Parser.TOKEN.STRING;
					case '#':
					case '$':
					case '%':
					case '&':
					case '\'':
					case '(':
					case ')':
					case '*':
					case '+':
					case '.':
					case '/':
						IL_8F:
						switch (c)
						{
						case '[':
							return Json.Parser.TOKEN.SQUARED_OPEN;
						case '\\':
						{
							IL_A4:
							switch (c)
							{
							case '{':
								return Json.Parser.TOKEN.CURLY_OPEN;
							case '}':
								this.json.Read();
								return Json.Parser.TOKEN.CURLY_CLOSE;
							}
							string nextWord = this.NextWord;
							string text = nextWord;
							if (text != null)
							{
								if (Json.Parser.<>f__switch$map1 == null)
								{
									Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
									dictionary.Add("false", 0);
									dictionary.Add("true", 1);
									dictionary.Add("null", 2);
									Json.Parser.<>f__switch$map1 = dictionary;
								}
								int num;
								if (Json.Parser.<>f__switch$map1.TryGetValue(text, ref num))
								{
									switch (num)
									{
									case 0:
										return Json.Parser.TOKEN.FALSE;
									case 1:
										return Json.Parser.TOKEN.TRUE;
									case 2:
										return Json.Parser.TOKEN.NULL;
									}
								}
							}
							return Json.Parser.TOKEN.NONE;
						}
						case ']':
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						goto IL_A4;
					case ',':
						this.json.Read();
						return Json.Parser.TOKEN.COMMA;
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						return Json.Parser.TOKEN.NUMBER;
					case ':':
						return Json.Parser.TOKEN.COLON;
					}
					goto IL_8F;
				}
			}

			private Parser(TextReader reader)
			{
				this.json = reader;
			}

			public static object Parse(TextReader reader)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(reader))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				while (true)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case Json.Parser.TOKEN.NONE:
						goto IL_37;
					case Json.Parser.TOKEN.CURLY_OPEN:
					{
						IL_2B:
						if (nextToken == Json.Parser.TOKEN.COMMA)
						{
							continue;
						}
						string text = this.ParseString();
						if (text == null)
						{
							goto Block_2;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto Block_3;
						}
						this.json.Read();
						dictionary.set_Item(text, this.ParseValue());
						continue;
					}
					case Json.Parser.TOKEN.CURLY_CLOSE:
						return dictionary;
					}
					goto IL_2B;
				}
				IL_37:
				return null;
				Block_2:
				return null;
				Block_3:
				return null;
			}

			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					Json.Parser.TOKEN tOKEN = nextToken;
					switch (tOKEN)
					{
					case Json.Parser.TOKEN.SQUARED_CLOSE:
						flag = false;
						continue;
					case Json.Parser.TOKEN.COLON:
						IL_38:
						if (tOKEN != Json.Parser.TOKEN.NONE)
						{
							object obj = this.ParseByToken(nextToken);
							list.Add(obj);
							continue;
						}
						return null;
					case Json.Parser.TOKEN.COMMA:
						continue;
					}
					goto IL_38;
				}
				return list;
			}

			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char nextChar = this.NextChar;
					char c = nextChar;
					if (c != '"')
					{
						if (c != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else
						{
							if (this.json.Peek() != -1)
							{
								nextChar = this.NextChar;
								char c2 = nextChar;
								switch (c2)
								{
								case 'n':
									stringBuilder.Append('\n');
									continue;
								case 'o':
								case 'p':
								case 'q':
								case 's':
									IL_A5:
									if (c2 == '"' || c2 == '/' || c2 == '\\')
									{
										stringBuilder.Append(nextChar);
										continue;
									}
									if (c2 == 'b')
									{
										stringBuilder.Append('\b');
										continue;
									}
									if (c2 != 'f')
									{
										continue;
									}
									stringBuilder.Append('\f');
									continue;
								case 'r':
									stringBuilder.Append('\r');
									continue;
								case 't':
									stringBuilder.Append('\t');
									continue;
								case 'u':
								{
									StringBuilder stringBuilder2 = new StringBuilder();
									for (int i = 0; i < 4; i++)
									{
										stringBuilder2.Append(this.NextChar);
									}
									stringBuilder.Append((char)Convert.ToInt32(stringBuilder2.ToString(), 16));
									continue;
								}
								}
								goto IL_A5;
							}
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				float num;
				float.TryParse(nextWord, 167, CultureInfo.get_InvariantCulture(), ref num);
				return num;
			}

			private void EatWhitespace()
			{
				while (" \t\n\r".IndexOf(this.PeekChar) != -1)
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}
		}

		private sealed class Serializer
		{
			private StringBuilder builder;

			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			private void SerializeValue(object value)
			{
				string str;
				IList anArray;
				IDictionary obj;
				if (value == null)
				{
					this.builder.Append("null");
				}
				else if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
				}
				else if (value is bool)
				{
					this.builder.Append(value.ToString().ToLower());
				}
				else if ((anArray = (value as IList)) != null)
				{
					this.SerializeArray(anArray);
				}
				else if ((obj = (value as IDictionary)) != null)
				{
					this.SerializeObject(obj);
				}
				else if (value is char)
				{
					this.SerializeString(value.ToString());
				}
				else
				{
					this.SerializeOther(value);
				}
			}

			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				IEnumerator enumerator = obj.get_Keys().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object current = enumerator.get_Current();
						if (!flag)
						{
							this.builder.Append(',');
						}
						this.SerializeString(current.ToString());
						this.builder.Append(':');
						this.SerializeValue(obj.get_Item(current));
						flag = false;
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.builder.Append('}');
			}

			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.get_Count(); i++)
				{
					object value = anArray.get_Item(i);
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(value);
					flag = false;
				}
				this.builder.Append(']');
			}

			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					char c = array[i];
					char c2 = c;
					switch (c2)
					{
					case '\b':
						this.builder.Append("\\b");
						goto IL_149;
					case '\t':
						this.builder.Append("\\t");
						goto IL_149;
					case '\n':
						this.builder.Append("\\n");
						goto IL_149;
					case '\v':
						IL_44:
						if (c2 == '"')
						{
							this.builder.Append("\\\"");
							goto IL_149;
						}
						if (c2 != '\\')
						{
							int num = Convert.ToInt32(c);
							if (num >= 32 && num <= 126)
							{
								this.builder.Append(c);
							}
							else
							{
								this.builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
							}
							goto IL_149;
						}
						this.builder.Append("\\\\");
						goto IL_149;
					case '\f':
						this.builder.Append("\\f");
						goto IL_149;
					case '\r':
						this.builder.Append("\\r");
						goto IL_149;
					}
					goto IL_44;
					IL_149:;
				}
				this.builder.Append('"');
			}

			private void SerializeOther(object value)
			{
				if (value is float || value is int || value is uint || value is long || value is float || value is sbyte || value is byte || value is short || value is ushort || value is ulong || value is decimal)
				{
					this.builder.Append(value.ToString());
				}
				else
				{
					this.SerializeString(value.ToString());
				}
			}
		}

		public static object Deserialize(TextReader json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}
	}
}
