using System;

namespace ProtoBuf
{
	public static class BclHelpers
	{
		[Flags]
		public enum NetObjectOptions : byte
		{
			None = 0,
			AsReference = 1,
			DynamicType = 2,
			UseConstructor = 4,
			LateSet = 8
		}

		private const int FieldTimeSpanValue = 1;

		private const int FieldTimeSpanScale = 2;

		private const int FieldDecimalLow = 1;

		private const int FieldDecimalHigh = 2;

		private const int FieldDecimalSignScale = 3;

		private const int FieldGuidLow = 1;

		private const int FieldGuidHigh = 2;

		private const int FieldExistingObjectKey = 1;

		private const int FieldNewObjectKey = 2;

		private const int FieldExistingTypeKey = 3;

		private const int FieldNewTypeKey = 4;

		private const int FieldTypeName = 8;

		private const int FieldObject = 10;

		internal static readonly DateTime EpochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public static object GetUninitializedObject(Type type)
		{
			throw new NotSupportedException("Constructor-skipping is not supported on this platform");
		}

		public static void WriteTimeSpan(TimeSpan timeSpan, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			switch (dest.WireType)
			{
			case WireType.Fixed64:
				ProtoWriter.WriteInt64(timeSpan.get_Ticks(), dest);
				break;
			case WireType.String:
			case WireType.StartGroup:
			{
				long num = timeSpan.get_Ticks();
				TimeSpanScale timeSpanScale;
				if (timeSpan == TimeSpan.MaxValue)
				{
					num = 1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (timeSpan == TimeSpan.MinValue)
				{
					num = -1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (num % 864000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Days;
					num /= 864000000000L;
				}
				else if (num % 36000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Hours;
					num /= 36000000000L;
				}
				else if (num % 600000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Minutes;
					num /= 600000000L;
				}
				else if (num % 10000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Seconds;
					num /= 10000000L;
				}
				else if (num % 10000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Milliseconds;
					num /= 10000L;
				}
				else
				{
					timeSpanScale = TimeSpanScale.Ticks;
				}
				SubItemToken token = ProtoWriter.StartSubItem(null, dest);
				if (num != 0L)
				{
					ProtoWriter.WriteFieldHeader(1, WireType.SignedVariant, dest);
					ProtoWriter.WriteInt64(num, dest);
				}
				if (timeSpanScale != TimeSpanScale.Days)
				{
					ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)timeSpanScale, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
				break;
			}
			default:
				throw new ProtoException("Unexpected wire-type: " + dest.WireType.ToString());
			}
		}

		public static TimeSpan ReadTimeSpan(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return TimeSpan.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromTicks(num);
		}

		public static DateTime ReadDateTime(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return DateTime.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return DateTime.MaxValue;
			}
			return BclHelpers.EpochOrigin.AddTicks(num);
		}

		public static void WriteDateTime(DateTime value, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			TimeSpan timeSpan;
			if (wireType != WireType.String && wireType != WireType.StartGroup)
			{
				timeSpan = value - BclHelpers.EpochOrigin;
			}
			else if (value == DateTime.MaxValue)
			{
				timeSpan = TimeSpan.MaxValue;
			}
			else if (value == DateTime.MinValue)
			{
				timeSpan = TimeSpan.MinValue;
			}
			else
			{
				timeSpan = value - BclHelpers.EpochOrigin;
			}
			BclHelpers.WriteTimeSpan(timeSpan, dest);
		}

		private static long ReadTimeSpanTicks(ProtoReader source)
		{
			switch (source.WireType)
			{
			case WireType.Fixed64:
				return source.ReadInt64();
			case WireType.String:
			case WireType.StartGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				TimeSpanScale timeSpanScale = TimeSpanScale.Days;
				long num = 0L;
				int num2;
				while ((num2 = source.ReadFieldHeader()) > 0)
				{
					int num3 = num2;
					if (num3 != 1)
					{
						if (num3 != 2)
						{
							source.SkipField();
						}
						else
						{
							timeSpanScale = (TimeSpanScale)source.ReadInt32();
						}
					}
					else
					{
						source.Assert(WireType.SignedVariant);
						num = source.ReadInt64();
					}
				}
				ProtoReader.EndSubItem(token, source);
				TimeSpanScale timeSpanScale2 = timeSpanScale;
				switch (timeSpanScale2)
				{
				case TimeSpanScale.Days:
					return num * 864000000000L;
				case TimeSpanScale.Hours:
					return num * 36000000000L;
				case TimeSpanScale.Minutes:
					return num * 600000000L;
				case TimeSpanScale.Seconds:
					return num * 10000000L;
				case TimeSpanScale.Milliseconds:
					return num * 10000L;
				case TimeSpanScale.Ticks:
					return num;
				default:
				{
					if (timeSpanScale2 != TimeSpanScale.MinMax)
					{
						throw new ProtoException("Unknown timescale: " + timeSpanScale.ToString());
					}
					long num4 = num;
					if (num4 >= -1L && num4 <= 1L)
					{
						switch ((int)(num4 - -1L))
						{
						case 0:
							return -9223372036854775808L;
						case 2:
							return 9223372036854775807L;
						}
					}
					throw new ProtoException("Unknown min/max value: " + num.ToString());
				}
				}
				break;
			}
			default:
				throw new ProtoException("Unexpected wire-type: " + source.WireType.ToString());
			}
		}

		public static decimal ReadDecimal(ProtoReader reader)
		{
			ulong num = 0uL;
			uint num2 = 0u;
			uint num3 = 0u;
			SubItemToken token = ProtoReader.StartSubItem(reader);
			int num4;
			while ((num4 = reader.ReadFieldHeader()) > 0)
			{
				switch (num4)
				{
				case 1:
					num = reader.ReadUInt64();
					break;
				case 2:
					num2 = reader.ReadUInt32();
					break;
				case 3:
					num3 = reader.ReadUInt32();
					break;
				default:
					reader.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, reader);
			if (num == 0uL && num2 == 0u)
			{
				return 0m;
			}
			int num5 = (int)(num & (ulong)-1);
			int num6 = (int)(num >> 32 & (ulong)-1);
			int num7 = (int)num2;
			bool flag = (num3 & 1u) == 1u;
			byte b = (byte)((num3 & 510u) >> 1);
			return new decimal(num5, num6, num7, flag, b);
		}

		public static void WriteDecimal(decimal value, ProtoWriter writer)
		{
			int[] bits = decimal.GetBits(value);
			ulong num = (ulong)((ulong)((long)bits[1]) << 32);
			ulong num2 = (ulong)((long)bits[0] & (long)((ulong)-1));
			ulong num3 = num | num2;
			uint num4 = (uint)bits[2];
			uint num5 = (uint)((bits[3] >> 15 & 510) | (bits[3] >> 31 & 1));
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			if (num3 != 0uL)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
				ProtoWriter.WriteUInt64(num3, writer);
			}
			if (num4 != 0u)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num4, writer);
			}
			if (num5 != 0u)
			{
				ProtoWriter.WriteFieldHeader(3, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num5, writer);
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		public static void WriteGuid(Guid value, ProtoWriter dest)
		{
			byte[] data = value.ToByteArray();
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != Guid.Empty)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 0, 8, dest);
				ProtoWriter.WriteFieldHeader(2, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 8, 8, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		public static Guid ReadGuid(ProtoReader source)
		{
			ulong num = 0uL;
			ulong num2 = 0uL;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				int num4 = num3;
				if (num4 != 1)
				{
					if (num4 != 2)
					{
						source.SkipField();
					}
					else
					{
						num2 = source.ReadUInt64();
					}
				}
				else
				{
					num = source.ReadUInt64();
				}
			}
			ProtoReader.EndSubItem(token, source);
			if (num == 0uL && num2 == 0uL)
			{
				return Guid.Empty;
			}
			uint num5 = (uint)(num >> 32);
			uint num6 = (uint)num;
			uint num7 = (uint)(num2 >> 32);
			uint num8 = (uint)num2;
			return new Guid((int)num6, (short)num5, (short)(num5 >> 16), (byte)num8, (byte)(num8 >> 8), (byte)(num8 >> 16), (byte)(num8 >> 24), (byte)num7, (byte)(num7 >> 8), (byte)(num7 >> 16), (byte)(num7 >> 24));
		}

		public static object ReadNetObject(object value, ProtoReader source, int key, Type type, BclHelpers.NetObjectOptions options)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num = -1;
			int num2 = -1;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				switch (num3)
				{
				case 1:
				{
					int key2 = source.ReadInt32();
					value = source.NetCache.GetKeyedObject(key2);
					continue;
				}
				case 2:
					num = source.ReadInt32();
					continue;
				case 3:
				{
					int key2 = source.ReadInt32();
					type = (Type)source.NetCache.GetKeyedObject(key2);
					key = source.GetTypeKey(ref type);
					continue;
				}
				case 4:
					num2 = source.ReadInt32();
					continue;
				case 8:
				{
					string text = source.ReadString();
					type = source.DeserializeType(text);
					if (type == null)
					{
						throw new ProtoException("Unable to resolve type: " + text + " (you can use the TypeModel.DynamicTypeFormatting event to provide a custom mapping)");
					}
					if (type == typeof(string))
					{
						key = -1;
					}
					else
					{
						key = source.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.get_Name());
						}
					}
					continue;
				}
				case 10:
				{
					bool flag = type == typeof(string);
					bool flag2 = value == null;
					bool flag3 = flag2 && (flag || (byte)(options & BclHelpers.NetObjectOptions.LateSet) != 0);
					if (num >= 0 && !flag3)
					{
						if (value == null)
						{
							source.TrapNextObject(num);
						}
						else
						{
							source.NetCache.SetKeyedObject(num, value);
						}
						if (num2 >= 0)
						{
							source.NetCache.SetKeyedObject(num2, type);
						}
					}
					object obj = value;
					if (flag)
					{
						value = source.ReadString();
					}
					else
					{
						value = ProtoReader.ReadTypedObject(obj, key, source, type);
					}
					if (num >= 0)
					{
						if (flag2 && !flag3)
						{
							obj = source.NetCache.GetKeyedObject(num);
						}
						if (flag3)
						{
							source.NetCache.SetKeyedObject(num, value);
							if (num2 >= 0)
							{
								source.NetCache.SetKeyedObject(num2, type);
							}
						}
					}
					if (num >= 0 && !flag3 && !object.ReferenceEquals(obj, value))
					{
						throw new ProtoException("A reference-tracked object changed reference during deserialization");
					}
					if (num < 0 && num2 >= 0)
					{
						source.NetCache.SetKeyedObject(num2, type);
					}
					continue;
				}
				}
				source.SkipField();
			}
			if (num >= 0 && (byte)(options & BclHelpers.NetObjectOptions.AsReference) == 0)
			{
				throw new ProtoException("Object key in input stream, but reference-tracking was not expected");
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		public static void WriteNetObject(object value, ProtoWriter dest, int key, BclHelpers.NetObjectOptions options)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			bool flag = (byte)(options & BclHelpers.NetObjectOptions.DynamicType) != 0;
			bool flag2 = (byte)(options & BclHelpers.NetObjectOptions.AsReference) != 0;
			WireType wireType = dest.WireType;
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			bool flag3 = true;
			if (flag2)
			{
				bool flag4;
				int value2 = dest.NetCache.AddObjectKey(value, out flag4);
				ProtoWriter.WriteFieldHeader((!flag4) ? 2 : 1, WireType.Variant, dest);
				ProtoWriter.WriteInt32(value2, dest);
				if (flag4)
				{
					flag3 = false;
				}
			}
			if (flag3)
			{
				if (flag)
				{
					Type type = value.GetType();
					if (!(value is string))
					{
						key = dest.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.get_Name());
						}
					}
					bool flag5;
					int value3 = dest.NetCache.AddObjectKey(type, out flag5);
					ProtoWriter.WriteFieldHeader((!flag5) ? 4 : 3, WireType.Variant, dest);
					ProtoWriter.WriteInt32(value3, dest);
					if (!flag5)
					{
						ProtoWriter.WriteFieldHeader(8, WireType.String, dest);
						ProtoWriter.WriteString(dest.SerializeType(type), dest);
					}
				}
				ProtoWriter.WriteFieldHeader(10, wireType, dest);
				if (value is string)
				{
					ProtoWriter.WriteString((string)value, dest);
				}
				else
				{
					ProtoWriter.WriteObject(value, key, dest);
				}
			}
			ProtoWriter.EndSubItem(token, dest);
		}
	}
}
