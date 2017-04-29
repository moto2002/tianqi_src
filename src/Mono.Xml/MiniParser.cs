using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Mono.Xml
{
	internal class MiniParser
	{
		public interface IReader
		{
			int Read();
		}

		public interface IAttrList
		{
			int Length
			{
				get;
			}

			bool IsEmpty
			{
				get;
			}

			string[] Names
			{
				get;
			}

			string[] Values
			{
				get;
			}

			string GetName(int i);

			string GetValue(int i);

			string GetValue(string name);

			void ChangeValue(string name, string newValue);
		}

		public interface IMutableAttrList : MiniParser.IAttrList
		{
			void Clear();

			void Add(string name, string value);

			void CopyFrom(MiniParser.IAttrList attrs);

			void Remove(int i);

			void Remove(string name);
		}

		public interface IHandler
		{
			void OnStartParsing(MiniParser parser);

			void OnStartElement(string name, MiniParser.IAttrList attrs);

			void OnEndElement(string name);

			void OnChars(string ch);

			void OnEndParsing(MiniParser parser);
		}

		public class HandlerAdapter : MiniParser.IHandler
		{
			public void OnStartParsing(MiniParser parser)
			{
			}

			public void OnStartElement(string name, MiniParser.IAttrList attrs)
			{
			}

			public void OnEndElement(string name)
			{
			}

			public void OnChars(string ch)
			{
			}

			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		private enum CharKind : byte
		{
			LEFT_BR,
			RIGHT_BR,
			SLASH,
			PI_MARK,
			EQ,
			AMP,
			SQUOTE,
			DQUOTE,
			BANG,
			LEFT_SQBR,
			SPACE,
			RIGHT_SQBR,
			TAB,
			CR,
			EOL,
			CHARS,
			UNKNOWN = 31
		}

		private enum ActionCode : byte
		{
			START_ELEM,
			END_ELEM,
			END_NAME,
			SET_ATTR_NAME,
			SET_ATTR_VAL,
			SEND_CHARS,
			START_CDATA,
			END_CDATA,
			ERROR,
			STATE_CHANGE,
			FLUSH_CHARS_STATE_CHANGE,
			ACC_CHARS_STATE_CHANGE,
			ACC_CDATA,
			PROC_CHAR_REF,
			UNKNOWN = 15
		}

		public class AttrListImpl : MiniParser.IAttrList, MiniParser.IMutableAttrList
		{
			protected ArrayList names;

			protected ArrayList values;

			public int Length
			{
				get
				{
					return this.names.get_Count();
				}
			}

			public bool IsEmpty
			{
				get
				{
					return this.Length != 0;
				}
			}

			public string[] Names
			{
				get
				{
					return this.names.ToArray(typeof(string)) as string[];
				}
			}

			public string[] Values
			{
				get
				{
					return this.values.ToArray(typeof(string)) as string[];
				}
			}

			public AttrListImpl() : this(0)
			{
			}

			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					this.names = new ArrayList();
					this.values = new ArrayList();
				}
				else
				{
					this.names = new ArrayList(initialCapacity);
					this.values = new ArrayList(initialCapacity);
				}
			}

			public AttrListImpl(MiniParser.IAttrList attrs) : this((attrs == null) ? 0 : attrs.Length)
			{
				if (attrs != null)
				{
					this.CopyFrom(attrs);
				}
			}

			public string GetName(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.names.get_Item(i) as string);
				}
				return result;
			}

			public string GetValue(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.values.get_Item(i) as string);
				}
				return result;
			}

			public string GetValue(string name)
			{
				return this.GetValue(this.names.IndexOf(name));
			}

			public void ChangeValue(string name, string newValue)
			{
				int num = this.names.IndexOf(name);
				if (num >= 0 && num < this.Length)
				{
					this.values.set_Item(num, newValue);
				}
			}

			public void Clear()
			{
				this.names.Clear();
				this.values.Clear();
			}

			public void Add(string name, string value)
			{
				this.names.Add(name);
				this.values.Add(value);
			}

			public void Remove(int i)
			{
				if (i >= 0)
				{
					this.names.RemoveAt(i);
					this.values.RemoveAt(i);
				}
			}

			public void Remove(string name)
			{
				this.Remove(this.names.IndexOf(name));
			}

			public void CopyFrom(MiniParser.IAttrList attrs)
			{
				if (attrs != null && this == attrs)
				{
					this.Clear();
					int length = attrs.Length;
					for (int i = 0; i < length; i++)
					{
						this.Add(attrs.GetName(i), attrs.GetValue(i));
					}
				}
			}
		}

		public class XMLError : Exception
		{
			protected string descr;

			protected int line;

			protected int column;

			public int Line
			{
				get
				{
					return this.line;
				}
			}

			public int Column
			{
				get
				{
					return this.column;
				}
			}

			public XMLError() : this("Unknown")
			{
			}

			public XMLError(string descr) : this(descr, -1, -1)
			{
			}

			public XMLError(string descr, int line, int column) : base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			public override string ToString()
			{
				return string.Format("{0} @ (line = {1}, col = {2})", this.descr, this.line, this.column);
			}
		}

		private static readonly int INPUT_RANGE = 13;

		private static readonly ushort[] tbl = new ushort[]
		{
			2305,
			43264,
			63616,
			10368,
			6272,
			14464,
			18560,
			22656,
			26752,
			34944,
			39040,
			47232,
			30848,
			2177,
			10498,
			6277,
			14595,
			18561,
			22657,
			26753,
			35088,
			39041,
			43137,
			47233,
			30849,
			64004,
			4352,
			43266,
			64258,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			14597,
			2307,
			10499,
			6403,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			64260,
			8710,
			4615,
			41480,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			6400,
			2307,
			10499,
			14595,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			6400,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			43137,
			47233,
			30849,
			63617,
			2561,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			43783,
			48138,
			31754,
			64522,
			64265,
			8198,
			4103,
			43272,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			64265,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			2317,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			44042,
			48138,
			31754,
			64522,
			26894,
			30991,
			43275,
			2180,
			10372,
			6276,
			14468,
			18564,
			22660,
			34948,
			39044,
			47236,
			63620,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			63618,
			9474,
			35088,
			2182,
			6278,
			14470,
			18566,
			22662,
			26758,
			39046,
			43142,
			47238,
			30854,
			63622,
			25617,
			23822,
			2830,
			11022,
			6926,
			15118,
			19214,
			35598,
			39694,
			43790,
			47886,
			31502,
			64270,
			29713,
			23823,
			2831,
			11023,
			6927,
			15119,
			19215,
			27407,
			35599,
			39695,
			43791,
			47887,
			64271,
			38418,
			6400,
			1555,
			9747,
			13843,
			17939,
			22035,
			26131,
			34323,
			42515,
			46611,
			30227,
			62995,
			8198,
			4103,
			43281,
			64265,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			46858,
			3090,
			11282,
			7186,
			15378,
			19474,
			23570,
			27666,
			35858,
			39954,
			44050,
			31762,
			64530,
			3091,
			11283,
			7187,
			15379,
			19475,
			23571,
			27667,
			35859,
			39955,
			44051,
			48147,
			31763,
			64531,
			65535,
			65535
		};

		protected static string[] errors = new string[]
		{
			"Expected element",
			"Invalid character in tag",
			"No '='",
			"Invalid character entity",
			"Invalid attr value",
			"Empty tag",
			"No end tag",
			"Bad entity ref"
		};

		protected int line;

		protected int col;

		protected int[] twoCharBuff;

		protected bool splitCData;

		public MiniParser()
		{
			this.twoCharBuff = new int[2];
			this.splitCData = false;
			this.Reset();
		}

		public void Reset()
		{
			this.line = 0;
			this.col = 0;
		}

		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.get_Length())
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str.get_Chars(i) != sb.get_Chars(sbStart + i))
				{
					return false;
				}
			}
			return true;
		}

		protected void FatalErr(string descr)
		{
			throw new MiniParser.XMLError(descr, this.line, this.col);
		}

		protected static int Xlat(int charCode, int state)
		{
			int num = state * MiniParser.INPUT_RANGE;
			int num2 = Math.Min(MiniParser.tbl.Length - num, MiniParser.INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = MiniParser.tbl[num];
				if (charCode == num3 >> 12)
				{
					return (int)(num3 & 4095);
				}
				num++;
			}
			return 4095;
		}

		public void Parse(MiniParser.IReader reader, MiniParser.IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new MiniParser.HandlerAdapter();
			}
			MiniParser.AttrListImpl attrListImpl = new MiniParser.AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			this.line = 1;
			this.col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			while (true)
			{
				this.col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 15;
				if (num4 != 13)
				{
					if (num4 == 12)
					{
						num4 = 10;
					}
					if (num4 == 14)
					{
						this.col = 0;
						this.line++;
						num4 = 10;
					}
					int num5 = MiniParser.Xlat(num4, num2);
					num2 = (num5 & 255);
					if (num != 10 || (num2 != 14 && num2 != 15))
					{
						num5 >>= 8;
						if (num2 >= 128)
						{
							if (num2 == 255)
							{
								this.FatalErr("State dispatch error.");
							}
							else
							{
								this.FatalErr(MiniParser.errors[num2 ^ 128]);
							}
						}
						switch (num5)
						{
						case 0:
							goto IL_19F;
						case 1:
						{
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							string text3 = null;
							if (stack.get_Count() == 0 || text2 != (text3 = (stack.Pop() as string)))
							{
								if (text3 == null)
								{
									this.FatalErr("Tag stack underflow");
								}
								else
								{
									this.FatalErr(string.Format("Expected end tag '{0}' but found '{1}'", text2, text3));
								}
							}
							handler.OnEndElement(text2);
							break;
						}
						case 2:
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							if (num == 47 || num == 62)
							{
								goto IL_19F;
							}
							break;
						case 3:
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							break;
						case 4:
							if (text == null)
							{
								this.FatalErr("Internal error.");
							}
							attrListImpl.Add(text, stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							text = null;
							break;
						case 5:
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							break;
						case 6:
						{
							string text4 = "CDATA[";
							flag2 = false;
							flag3 = false;
							if (num == 45)
							{
								num = reader.Read();
								if (num != 45)
								{
									this.FatalErr("Invalid comment");
								}
								this.col++;
								flag2 = true;
								this.twoCharBuff[0] = -1;
								this.twoCharBuff[1] = -1;
							}
							else if (num != 91)
							{
								flag3 = true;
								num3 = 0;
							}
							else
							{
								for (int i = 0; i < text4.get_Length(); i++)
								{
									if (reader.Read() != (int)text4.get_Chars(i))
									{
										this.col += i + 1;
										break;
									}
								}
								this.col += text4.get_Length();
								flag = true;
							}
							break;
						}
						case 7:
						{
							int num6 = 0;
							num = 93;
							while (num == 93)
							{
								num = reader.Read();
								num6++;
							}
							if (num != 62)
							{
								for (int j = 0; j < num6; j++)
								{
									stringBuilder.Append(']');
								}
								stringBuilder.Append((char)num);
								num2 = 18;
							}
							else
							{
								for (int k = 0; k < num6 - 2; k++)
								{
									stringBuilder.Append(']');
								}
								flag = false;
							}
							this.col += num6;
							break;
						}
						case 8:
							this.FatalErr(string.Format("Error {0}", num2));
							break;
						case 9:
							break;
						case 10:
							stringBuilder = new StringBuilder();
							if (num != 60)
							{
								goto IL_465;
							}
							break;
						case 11:
							goto IL_465;
						case 12:
							if (flag2)
							{
								if (num == 62 && this.twoCharBuff[0] == 45 && this.twoCharBuff[1] == 45)
								{
									flag2 = false;
									num2 = 0;
								}
								else
								{
									this.twoCharBuff[0] = this.twoCharBuff[1];
									this.twoCharBuff[1] = num;
								}
							}
							else if (flag3)
							{
								if (num == 60 || num == 62)
								{
									num3 ^= 1;
								}
								if (num == 62 && num3 != 0)
								{
									flag3 = false;
									num2 = 0;
								}
							}
							else
							{
								if (this.splitCData && stringBuilder.get_Length() > 0 && flag)
								{
									handler.OnChars(stringBuilder.ToString());
									stringBuilder = new StringBuilder();
								}
								flag = false;
								stringBuilder.Append((char)num);
							}
							break;
						case 13:
						{
							num = reader.Read();
							int num7 = this.col + 1;
							if (num == 35)
							{
								int num8 = 10;
								int num9 = 0;
								int num10 = 0;
								num = reader.Read();
								num7++;
								if (num == 120)
								{
									num = reader.Read();
									num7++;
									num8 = 16;
								}
								NumberStyles numberStyles = (num8 != 16) ? 7 : 515;
								while (true)
								{
									int num11 = -1;
									if (char.IsNumber((char)num))
									{
										goto IL_5D9;
									}
									if ("abcdef".IndexOf(char.ToLower((char)num)) != -1)
									{
										goto Block_43;
									}
									IL_5F9:
									if (num11 == -1)
									{
										break;
									}
									num9 *= num8;
									num9 += num11;
									num10++;
									num = reader.Read();
									num7++;
									continue;
									Block_43:
									try
									{
										IL_5D9:
										num11 = int.Parse(new string((char)num, 1), numberStyles);
									}
									catch (FormatException)
									{
										num11 = -1;
									}
									goto IL_5F9;
								}
								if (num == 59 && num10 > 0)
								{
									stringBuilder.Append((char)num9);
								}
								else
								{
									this.FatalErr("Bad char ref");
								}
							}
							else
							{
								string text5 = "aglmopqstu";
								string text6 = "&'\"><";
								int num12 = 0;
								int num13 = 15;
								int num14 = 0;
								int length = stringBuilder.get_Length();
								while (true)
								{
									if (num12 != 15)
									{
										num12 = (text5.IndexOf((char)num) & 15);
									}
									if (num12 == 15)
									{
										this.FatalErr(MiniParser.errors[7]);
									}
									stringBuilder.Append((char)num);
									int num15 = (int)"Ｕ㾏侏ཟｸ⊙ｏ".get_Chars(num12);
									int num16 = num15 >> 4 & 15;
									int num17 = num15 & 15;
									int num18 = num15 >> 12;
									int num19 = num15 >> 8 & 15;
									num = reader.Read();
									num7++;
									num12 = 15;
									if (num16 != 15 && num == (int)text5.get_Chars(num16))
									{
										if (num18 < 14)
										{
											num13 = num18;
										}
										num14 = 12;
									}
									else if (num17 != 15 && num == (int)text5.get_Chars(num17))
									{
										if (num19 < 14)
										{
											num13 = num19;
										}
										num14 = 8;
									}
									else if (num == 59)
									{
										if (num13 != 15 && num14 != 0 && (num15 >> num14 & 15) == 14)
										{
											break;
										}
										continue;
									}
									num12 = 0;
								}
								int num20 = num7 - this.col - 1;
								if (num20 > 0 && num20 < 5 && (MiniParser.StrEquals("amp", stringBuilder, length, num20) || MiniParser.StrEquals("apos", stringBuilder, length, num20) || MiniParser.StrEquals("quot", stringBuilder, length, num20) || MiniParser.StrEquals("lt", stringBuilder, length, num20) || MiniParser.StrEquals("gt", stringBuilder, length, num20)))
								{
									stringBuilder.set_Length(length);
									stringBuilder.Append(text6.get_Chars(num13));
								}
								else
								{
									this.FatalErr(MiniParser.errors[7]);
								}
							}
							this.col = num7;
							break;
						}
						default:
							this.FatalErr(string.Format("Unexpected action code - {0}.", num5));
							break;
						}
						continue;
						IL_19F:
						handler.OnStartElement(text2, attrListImpl);
						if (num != 47)
						{
							stack.Push(text2);
						}
						else
						{
							handler.OnEndElement(text2);
						}
						attrListImpl.Clear();
						continue;
						IL_465:
						stringBuilder.Append((char)num);
					}
				}
			}
			if (num2 != 0)
			{
				this.FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}
	}
}
