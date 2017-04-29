using System;

namespace ProtoBuf.Serializers
{
	internal sealed class EnumSerializer : IProtoSerializer
	{
		public struct EnumPair
		{
			public readonly object RawValue;

			public readonly Enum TypedValue;

			public readonly int WireValue;

			public EnumPair(int wireValue, object raw, Type type)
			{
				this.WireValue = wireValue;
				this.RawValue = raw;
				this.TypedValue = (Enum)Enum.ToObject(type, raw);
			}
		}

		private readonly Type enumType;

		private readonly EnumSerializer.EnumPair[] map;

		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		public Type ExpectedType
		{
			get
			{
				return this.enumType;
			}
		}

		public EnumSerializer(Type enumType, EnumSerializer.EnumPair[] map)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			this.enumType = enumType;
			this.map = map;
			if (map != null)
			{
				for (int i = 1; i < map.Length; i++)
				{
					for (int j = 0; j < i; j++)
					{
						if (map[i].WireValue == map[j].WireValue && !object.Equals(map[i].RawValue, map[j].RawValue))
						{
							throw new ProtoException("Multiple enums with wire-value " + map[i].WireValue.ToString());
						}
						if (object.Equals(map[i].RawValue, map[j].RawValue) && map[i].WireValue != map[j].WireValue)
						{
							throw new ProtoException("Multiple enums with deserialized-value " + map[i].RawValue);
						}
					}
				}
			}
		}

		private ProtoTypeCode GetTypeCode()
		{
			Type underlyingType = Helpers.GetUnderlyingType(this.enumType);
			if (underlyingType == null)
			{
				underlyingType = this.enumType;
			}
			return Helpers.GetTypeCode(underlyingType);
		}

		private int EnumToWire(object value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return (int)((sbyte)value);
			case ProtoTypeCode.Byte:
				return (int)((byte)value);
			case ProtoTypeCode.Int16:
				return (int)((short)value);
			case ProtoTypeCode.UInt16:
				return (int)((ushort)value);
			case ProtoTypeCode.Int32:
				return (int)value;
			case ProtoTypeCode.UInt32:
				return (int)((uint)value);
			case ProtoTypeCode.Int64:
				return (int)((long)value);
			case ProtoTypeCode.UInt64:
				return (int)((ulong)value);
			default:
				throw new InvalidOperationException();
			}
		}

		private object WireToEnum(int value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return Enum.ToObject(this.enumType, (sbyte)value);
			case ProtoTypeCode.Byte:
				return Enum.ToObject(this.enumType, (byte)value);
			case ProtoTypeCode.Int16:
				return Enum.ToObject(this.enumType, (short)value);
			case ProtoTypeCode.UInt16:
				return Enum.ToObject(this.enumType, (ushort)value);
			case ProtoTypeCode.Int32:
				return Enum.ToObject(this.enumType, value);
			case ProtoTypeCode.UInt32:
				return Enum.ToObject(this.enumType, (uint)value);
			case ProtoTypeCode.Int64:
				return Enum.ToObject(this.enumType, (long)value);
			case ProtoTypeCode.UInt64:
				return Enum.ToObject(this.enumType, (ulong)((long)value));
			default:
				throw new InvalidOperationException();
			}
		}

		public object Read(object value, ProtoReader source)
		{
			int num = source.ReadInt32();
			if (this.map == null)
			{
				return this.WireToEnum(num);
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (this.map[i].WireValue == num)
				{
					return this.map[i].TypedValue;
				}
			}
			source.ThrowEnumException(this.ExpectedType, num);
			return null;
		}

		public void Write(object value, ProtoWriter dest)
		{
			if (this.map == null)
			{
				ProtoWriter.WriteInt32(this.EnumToWire(value), dest);
			}
			else
			{
				for (int i = 0; i < this.map.Length; i++)
				{
					if (object.Equals(this.map[i].TypedValue, value))
					{
						ProtoWriter.WriteInt32(this.map[i].WireValue, dest);
						return;
					}
				}
				ProtoWriter.ThrowEnumException(dest, value);
			}
		}
	}
}
