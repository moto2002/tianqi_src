using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProtoBuf
{
	public sealed class ProtoReader : IDisposable
	{
		internal const int TO_EOF = -1;

		private const long Int64Msb = -9223372036854775808L;

		private const int Int32Msb = -2147483648;

		private Stream source;

		private byte[] ioBuffer;

		private TypeModel model;

		private int fieldNumber;

		private int depth;

		private int dataRemaining;

		private int ioIndex;

		private int position;

		private int available;

		private int blockEnd;

		private WireType wireType;

		private bool isFixedLength;

		private bool internStrings;

		private NetObjectCache netCache;

		private uint trapCount;

		private SerializationContext context;

		private Dictionary<string, string> stringInterner;

		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		private static readonly byte[] EmptyBlob = new byte[0];

		[ThreadStatic]
		private static ProtoReader lastReader;

		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		public WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		public bool InternStrings
		{
			get
			{
				return this.internStrings;
			}
			set
			{
				this.internStrings = value;
			}
		}

		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		public int Position
		{
			get
			{
				return this.position;
			}
		}

		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		public ProtoReader(Stream source, TypeModel model, SerializationContext context)
		{
			ProtoReader.Init(this, source, model, context, -1);
		}

		public ProtoReader(Stream source, TypeModel model, SerializationContext context, int length)
		{
			ProtoReader.Init(this, source, model, context, length);
		}

		private static void Init(ProtoReader reader, Stream source, TypeModel model, SerializationContext context, int length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!source.get_CanRead())
			{
				throw new ArgumentException("Cannot read from stream", "source");
			}
			reader.source = source;
			reader.ioBuffer = BufferPool.GetBuffer();
			reader.model = model;
			bool flag = length >= 0;
			reader.isFixedLength = flag;
			reader.dataRemaining = ((!flag) ? 0 : length);
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			reader.context = context;
			reader.position = (reader.available = (reader.depth = (reader.fieldNumber = (reader.ioIndex = 0))));
			reader.blockEnd = 2147483647;
			reader.internStrings = true;
			reader.wireType = WireType.None;
			reader.trapCount = 1u;
			if (reader.netCache == null)
			{
				reader.netCache = new NetObjectCache();
			}
		}

		public void Dispose()
		{
			this.source = null;
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
			if (this.stringInterner != null)
			{
				this.stringInterner.Clear();
			}
			if (this.netCache != null)
			{
				this.netCache.Clear();
			}
		}

		internal int TryReadUInt32VariantWithoutMoving(bool trimNegative, out uint value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0u;
				return 0;
			}
			int num = this.ioIndex;
			value = (uint)this.ioBuffer[num++];
			if ((value & 128u) == 0u)
			{
				return 1;
			}
			value &= 127u;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			uint num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 7;
			if ((num2 & 128u) == 0u)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 14;
			if ((num2 & 128u) == 0u)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 21;
			if ((num2 & 128u) == 0u)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num];
			value |= num2 << 28;
			if ((num2 & 240u) == 0u)
			{
				return 5;
			}
			if (trimNegative && (num2 & 240u) == 240u && this.available >= 10 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[num + 1] == 1)
			{
				return 10;
			}
			throw ProtoReader.AddErrorData(new OverflowException(), this);
		}

		private uint ReadUInt32Variant(bool trimNegative)
		{
			uint result;
			int num = this.TryReadUInt32VariantWithoutMoving(trimNegative, out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		private bool TryReadUInt32Variant(out uint value)
		{
			int num = this.TryReadUInt32VariantWithoutMoving(false, out value);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return true;
			}
			return false;
		}

		public uint ReadUInt32()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
				return this.ReadUInt32Variant(false);
			case WireType.Fixed64:
			{
				ulong num = this.ReadUInt64();
				return checked((uint)num);
			}
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.position += 4;
				this.available -= 4;
				return (uint)((int)this.ioBuffer[this.ioIndex++] | (int)this.ioBuffer[this.ioIndex++] << 8 | (int)this.ioBuffer[this.ioIndex++] << 16 | (int)this.ioBuffer[this.ioIndex++] << 24);
			}
			throw this.CreateWireTypeException();
		}

		internal void Ensure(int count, bool strict)
		{
			if (count > this.ioBuffer.Length)
			{
				BufferPool.ResizeAndFlushLeft(ref this.ioBuffer, count, this.ioIndex, this.available);
				this.ioIndex = 0;
			}
			else if (this.ioIndex + count >= this.ioBuffer.Length)
			{
				Helpers.BlockCopy(this.ioBuffer, this.ioIndex, this.ioBuffer, 0, this.available);
				this.ioIndex = 0;
			}
			count -= this.available;
			int num = this.ioIndex + this.available;
			int num2 = this.ioBuffer.Length - num;
			if (this.isFixedLength && this.dataRemaining < num2)
			{
				num2 = this.dataRemaining;
			}
			int num3;
			while (count > 0 && num2 > 0 && (num3 = this.source.Read(this.ioBuffer, num, num2)) > 0)
			{
				this.available += num3;
				count -= num3;
				num2 -= num3;
				num += num3;
				if (this.isFixedLength)
				{
					this.dataRemaining -= num3;
				}
			}
			if (strict && count > 0)
			{
				throw ProtoReader.EoF(this);
			}
		}

		public short ReadInt16()
		{
			return checked((short)this.ReadInt32());
		}

		public ushort ReadUInt16()
		{
			return checked((ushort)this.ReadUInt32());
		}

		public byte ReadByte()
		{
			return checked((byte)this.ReadUInt32());
		}

		public sbyte ReadSByte()
		{
			return checked((sbyte)this.ReadInt32());
		}

		public int ReadInt32()
		{
			WireType wireType = this.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				return (int)this.ReadUInt32Variant(true);
			case WireType.Fixed64:
			{
				long num = this.ReadInt64();
				return checked((int)num);
			}
			case WireType.String:
			case WireType.StartGroup:
			case WireType.EndGroup:
				IL_25:
				if (wireType != WireType.SignedVariant)
				{
					throw this.CreateWireTypeException();
				}
				return ProtoReader.Zag(this.ReadUInt32Variant(true));
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.position += 4;
				this.available -= 4;
				return (int)this.ioBuffer[this.ioIndex++] | (int)this.ioBuffer[this.ioIndex++] << 8 | (int)this.ioBuffer[this.ioIndex++] << 16 | (int)this.ioBuffer[this.ioIndex++] << 24;
			}
			goto IL_25;
		}

		private static int Zag(uint ziggedValue)
		{
			return (int)(-(ziggedValue & 1u) ^ (uint)((int)ziggedValue >> 1 & 2147483647));
		}

		private static long Zag(ulong ziggedValue)
		{
			return (long)(-(long)(ziggedValue & 1uL) ^ (ziggedValue >> 1 & 9223372036854775807uL));
		}

		public long ReadInt64()
		{
			WireType wireType = this.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				return (long)this.ReadUInt64Variant();
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position += 8;
				this.available -= 8;
				return (long)this.ioBuffer[this.ioIndex++] | (long)this.ioBuffer[this.ioIndex++] << 8 | (long)this.ioBuffer[this.ioIndex++] << 16 | (long)this.ioBuffer[this.ioIndex++] << 24 | (long)this.ioBuffer[this.ioIndex++] << 32 | (long)this.ioBuffer[this.ioIndex++] << 40 | (long)this.ioBuffer[this.ioIndex++] << 48 | (long)this.ioBuffer[this.ioIndex++] << 56;
			case WireType.String:
			case WireType.StartGroup:
			case WireType.EndGroup:
				IL_25:
				if (wireType != WireType.SignedVariant)
				{
					throw this.CreateWireTypeException();
				}
				return ProtoReader.Zag(this.ReadUInt64Variant());
			case WireType.Fixed32:
				return (long)this.ReadInt32();
			}
			goto IL_25;
		}

		private int TryReadUInt64VariantWithoutMoving(out ulong value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0uL;
				return 0;
			}
			int num = this.ioIndex;
			value = (ulong)this.ioBuffer[num++];
			if ((value & 128uL) == 0uL)
			{
				return 1;
			}
			value &= 127uL;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			ulong num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 7;
			if ((num2 & 128uL) == 0uL)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 14;
			if ((num2 & 128uL) == 0uL)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 21;
			if ((num2 & 128uL) == 0uL)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 28;
			if ((num2 & 128uL) == 0uL)
			{
				return 5;
			}
			if (this.available == 5)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 35;
			if ((num2 & 128uL) == 0uL)
			{
				return 6;
			}
			if (this.available == 6)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 42;
			if ((num2 & 128uL) == 0uL)
			{
				return 7;
			}
			if (this.available == 7)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 49;
			if ((num2 & 128uL) == 0uL)
			{
				return 8;
			}
			if (this.available == 8)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127uL) << 56;
			if ((num2 & 128uL) == 0uL)
			{
				return 9;
			}
			if (this.available == 9)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num];
			value |= num2 << 63;
			if ((num2 & 18446744073709551614uL) != 0uL)
			{
				throw ProtoReader.AddErrorData(new OverflowException(), this);
			}
			return 10;
		}

		private ulong ReadUInt64Variant()
		{
			ulong result;
			int num = this.TryReadUInt64VariantWithoutMoving(out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		private string Intern(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.get_Length() == 0)
			{
				return string.Empty;
			}
			string text;
			if (this.stringInterner == null)
			{
				this.stringInterner = new Dictionary<string, string>();
				this.stringInterner.Add(value, value);
			}
			else if (this.stringInterner.TryGetValue(value, ref text))
			{
				value = text;
			}
			else
			{
				this.stringInterner.Add(value, value);
			}
			return value;
		}

		public string ReadString()
		{
			if (this.wireType != WireType.String)
			{
				throw this.CreateWireTypeException();
			}
			int num = (int)this.ReadUInt32Variant(false);
			if (num == 0)
			{
				return string.Empty;
			}
			if (this.available < num)
			{
				this.Ensure(num, true);
			}
			string text = ProtoReader.encoding.GetString(this.ioBuffer, this.ioIndex, num);
			if (this.internStrings)
			{
				text = this.Intern(text);
			}
			this.available -= num;
			this.position += num;
			this.ioIndex += num;
			return text;
		}

		public void ThrowEnumException(Type type, int value)
		{
			string text = (type != null) ? type.get_FullName() : "<null>";
			throw ProtoReader.AddErrorData(new ProtoException("No " + text + " enum is mapped to the wire-value " + value.ToString()), this);
		}

		private Exception CreateWireTypeException()
		{
			return this.CreateException("Invalid wire-type; this usually means you have over-written a file without truncating or setting the length; see http://stackoverflow.com/q/2152978/23354");
		}

		private Exception CreateException(string message)
		{
			return ProtoReader.AddErrorData(new ProtoException(message), this);
		}

		public double ReadDouble()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Fixed64)
			{
				long num = this.ReadInt64();
				return BitConverter.ToDouble(BitConverter.GetBytes(num), 0);
			}
			if (wireType != WireType.Fixed32)
			{
				throw this.CreateWireTypeException();
			}
			return (double)this.ReadSingle();
		}

		public static object ReadObject(object value, int key, ProtoReader reader)
		{
			return ProtoReader.ReadTypedObject(value, key, reader, null);
		}

		internal static object ReadTypedObject(object value, int key, ProtoReader reader, Type type)
		{
			if (reader.model == null)
			{
				throw ProtoReader.AddErrorData(new InvalidOperationException("Cannot deserialize sub-objects unless a model is provided"), reader);
			}
			SubItemToken token = ProtoReader.StartSubItem(reader);
			if (key >= 0)
			{
				value = reader.model.Deserialize(key, value, reader);
			}
			else if (type == null || !reader.model.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
			ProtoReader.EndSubItem(token, reader);
			return value;
		}

		public static void EndSubItem(SubItemToken token, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int value = token.value;
			WireType wireType = reader.wireType;
			if (wireType != WireType.EndGroup)
			{
				if (value < reader.position)
				{
					throw reader.CreateException("Sub-message not read entirely");
				}
				if (reader.blockEnd != reader.position && reader.blockEnd != 2147483647)
				{
					throw reader.CreateException("Sub-message not read correctly");
				}
				reader.blockEnd = value;
				reader.depth--;
			}
			else
			{
				if (value >= 0)
				{
					throw ProtoReader.AddErrorData(new ArgumentException("token"), reader);
				}
				if (-value != reader.fieldNumber)
				{
					throw reader.CreateException("Wrong group was ended");
				}
				reader.wireType = WireType.None;
				reader.depth--;
			}
		}

		public static SubItemToken StartSubItem(ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				if (wireType != WireType.StartGroup)
				{
					throw reader.CreateWireTypeException();
				}
				reader.wireType = WireType.None;
				reader.depth++;
				return new SubItemToken(-reader.fieldNumber);
			}
			else
			{
				int num = (int)reader.ReadUInt32Variant(false);
				if (num < 0)
				{
					throw ProtoReader.AddErrorData(new InvalidOperationException(), reader);
				}
				int value = reader.blockEnd;
				reader.blockEnd = reader.position + num;
				reader.depth++;
				return new SubItemToken(value);
			}
		}

		public int ReadFieldHeader()
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return 0;
			}
			uint num;
			if (this.TryReadUInt32Variant(out num))
			{
				this.wireType = (WireType)(num & 7u);
				this.fieldNumber = (int)(num >> 3);
				if (this.fieldNumber < 1)
				{
					throw new ProtoException("Invalid field in source data: " + this.fieldNumber.ToString());
				}
			}
			else
			{
				this.wireType = WireType.None;
				this.fieldNumber = 0;
			}
			if (this.wireType != WireType.EndGroup)
			{
				return this.fieldNumber;
			}
			if (this.depth > 0)
			{
				return 0;
			}
			throw new ProtoException("Unexpected end-group in source data; this usually means the source data is corrupt");
		}

		public bool TryReadFieldHeader(int field)
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return false;
			}
			uint num2;
			int num = this.TryReadUInt32VariantWithoutMoving(false, out num2);
			WireType wireType;
			if (num > 0 && (int)num2 >> 3 == field && (wireType = (WireType)(num2 & 7u)) != WireType.EndGroup)
			{
				this.wireType = wireType;
				this.fieldNumber = field;
				this.position += num;
				this.ioIndex += num;
				this.available -= num;
				return true;
			}
			return false;
		}

		public void Hint(WireType wireType)
		{
			if (this.wireType != wireType)
			{
				if ((wireType & (WireType)7) == this.wireType)
				{
					this.wireType = wireType;
				}
			}
		}

		public void Assert(WireType wireType)
		{
			if (this.wireType != wireType)
			{
				if ((wireType & (WireType)7) != this.wireType)
				{
					throw this.CreateWireTypeException();
				}
				this.wireType = wireType;
			}
		}

		public void SkipField()
		{
			WireType wireType = this.wireType;
			switch (wireType + 1)
			{
			case WireType.Fixed64:
			case (WireType)9:
				this.ReadUInt64Variant();
				return;
			case WireType.String:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.available -= 8;
				this.ioIndex += 8;
				this.position += 8;
				return;
			case WireType.StartGroup:
			{
				int num = (int)this.ReadUInt32Variant(false);
				if (num <= this.available)
				{
					this.available -= num;
					this.ioIndex += num;
					this.position += num;
					return;
				}
				this.position += num;
				num -= this.available;
				this.ioIndex = (this.available = 0);
				if (this.isFixedLength)
				{
					if (num > this.dataRemaining)
					{
						throw ProtoReader.EoF(this);
					}
					this.dataRemaining -= num;
				}
				ProtoReader.Seek(this.source, num, this.ioBuffer);
				return;
			}
			case WireType.EndGroup:
			{
				int num2 = this.fieldNumber;
				this.depth++;
				while (this.ReadFieldHeader() > 0)
				{
					this.SkipField();
				}
				this.depth--;
				if (this.wireType == WireType.EndGroup && this.fieldNumber == num2)
				{
					this.wireType = WireType.None;
					return;
				}
				throw this.CreateWireTypeException();
			}
			case (WireType)6:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.available -= 4;
				this.ioIndex += 4;
				this.position += 4;
				return;
			}
			throw this.CreateWireTypeException();
		}

		public ulong ReadUInt64()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
				return this.ReadUInt64Variant();
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position += 8;
				this.available -= 8;
				return (ulong)this.ioBuffer[this.ioIndex++] | (ulong)this.ioBuffer[this.ioIndex++] << 8 | (ulong)this.ioBuffer[this.ioIndex++] << 16 | (ulong)this.ioBuffer[this.ioIndex++] << 24 | (ulong)this.ioBuffer[this.ioIndex++] << 32 | (ulong)this.ioBuffer[this.ioIndex++] << 40 | (ulong)this.ioBuffer[this.ioIndex++] << 48 | (ulong)this.ioBuffer[this.ioIndex++] << 56;
			case WireType.Fixed32:
				return (ulong)this.ReadUInt32();
			}
			throw this.CreateWireTypeException();
		}

		public float ReadSingle()
		{
			WireType wireType = this.wireType;
			if (wireType != WireType.Fixed64)
			{
				if (wireType != WireType.Fixed32)
				{
					throw this.CreateWireTypeException();
				}
				int num = this.ReadInt32();
				return BitConverter.ToSingle(BitConverter.GetBytes(num), 0);
			}
			else
			{
				double num2 = this.ReadDouble();
				float num3 = (float)num2;
				if (Helpers.IsInfinity(num3) && !Helpers.IsInfinity(num2))
				{
					throw ProtoReader.AddErrorData(new OverflowException(), this);
				}
				return num3;
			}
		}

		public bool ReadBoolean()
		{
			uint num = this.ReadUInt32();
			if (num == 0u)
			{
				return false;
			}
			if (num != 1u)
			{
				throw this.CreateException("Unexpected boolean value");
			}
			return true;
		}

		public static byte[] AppendBytes(byte[] value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				throw reader.CreateWireTypeException();
			}
			int i = (int)reader.ReadUInt32Variant(false);
			reader.wireType = WireType.None;
			if (i == 0)
			{
				return (value != null) ? value : ProtoReader.EmptyBlob;
			}
			int num;
			if (value == null || value.Length == 0)
			{
				num = 0;
				value = new byte[i];
			}
			else
			{
				num = value.Length;
				byte[] array = new byte[value.Length + i];
				Helpers.BlockCopy(value, 0, array, 0, value.Length);
				value = array;
			}
			reader.position += i;
			while (i > reader.available)
			{
				if (reader.available > 0)
				{
					Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, reader.available);
					i -= reader.available;
					num += reader.available;
					reader.ioIndex = (reader.available = 0);
				}
				int num2 = (i <= reader.ioBuffer.Length) ? i : reader.ioBuffer.Length;
				if (num2 > 0)
				{
					reader.Ensure(num2, true);
				}
			}
			if (i > 0)
			{
				Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, i);
				reader.ioIndex += i;
				reader.available -= i;
			}
			return value;
		}

		private static int ReadByteOrThrow(Stream source)
		{
			int num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			return num;
		}

		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber)
		{
			int num;
			return ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out num);
		}

		public static int DirectReadLittleEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
		}

		public static int DirectReadBigEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
		}

		public static int DirectReadVarintInt32(Stream source)
		{
			uint result;
			int num = ProtoReader.TryReadUInt32Variant(source, out result);
			if (num <= 0)
			{
				throw ProtoReader.EoF(null);
			}
			return (int)result;
		}

		public static void DirectReadBytes(Stream source, byte[] buffer, int offset, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			while (count > 0 && (num = source.Read(buffer, offset, count)) > 0)
			{
				count -= num;
				offset += num;
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		public static byte[] DirectReadBytes(Stream source, int count)
		{
			byte[] array = new byte[count];
			ProtoReader.DirectReadBytes(source, array, 0, count);
			return array;
		}

		public static string DirectReadString(Stream source, int length)
		{
			byte[] array = new byte[length];
			ProtoReader.DirectReadBytes(source, array, 0, length);
			return Encoding.get_UTF8().GetString(array, 0, length);
		}

		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber, out int bytesRead)
		{
			fieldNumber = 0;
			switch (style)
			{
			case PrefixStyle.None:
				bytesRead = 0;
				return 2147483647;
			case PrefixStyle.Base128:
			{
				bytesRead = 0;
				uint num2;
				int num;
				if (!expectHeader)
				{
					num = ProtoReader.TryReadUInt32Variant(source, out num2);
					bytesRead += num;
					return (int)((bytesRead >= 0) ? num2 : 4294967295u);
				}
				num = ProtoReader.TryReadUInt32Variant(source, out num2);
				bytesRead += num;
				if (num <= 0)
				{
					bytesRead = 0;
					return -1;
				}
				if ((num2 & 7u) != 2u)
				{
					throw new InvalidOperationException();
				}
				fieldNumber = (int)(num2 >> 3);
				num = ProtoReader.TryReadUInt32Variant(source, out num2);
				bytesRead += num;
				if (bytesRead == 0)
				{
					throw ProtoReader.EoF(null);
				}
				return (int)num2;
			}
			case PrefixStyle.Fixed32:
			{
				int num3 = source.ReadByte();
				if (num3 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num3 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num4 = source.ReadByte();
				if (num4 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num4 << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
		}

		private static int TryReadUInt32Variant(Stream source, out uint value)
		{
			value = 0u;
			int num = source.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			value = (uint)num;
			if ((value & 128u) == 0u)
			{
				return 1;
			}
			value &= 127u;
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 7);
			if ((num & 128) == 0)
			{
				return 2;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 14);
			if ((num & 128) == 0)
			{
				return 3;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 21);
			if ((num & 128) == 0)
			{
				return 4;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)num << 28);
			if ((num & 240) == 0)
			{
				return 5;
			}
			throw new OverflowException();
		}

		internal static void Seek(Stream source, int count, byte[] buffer)
		{
			if (source.get_CanSeek())
			{
				source.Seek((long)count, 1);
				count = 0;
			}
			else if (buffer != null)
			{
				int num;
				while (count > buffer.Length && (num = source.Read(buffer, 0, buffer.Length)) > 0)
				{
					count -= num;
				}
				while (count > 0 && (num = source.Read(buffer, 0, count)) > 0)
				{
					count -= num;
				}
			}
			else
			{
				buffer = BufferPool.GetBuffer();
				try
				{
					int num2;
					while (count > buffer.Length && (num2 = source.Read(buffer, 0, buffer.Length)) > 0)
					{
						count -= num2;
					}
					while (count > 0 && (num2 = source.Read(buffer, 0, count)) > 0)
					{
						count -= num2;
					}
				}
				finally
				{
					BufferPool.ReleaseBufferToPool(ref buffer);
				}
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		internal static Exception AddErrorData(Exception exception, ProtoReader source)
		{
			if (exception != null && source != null && !exception.get_Data().Contains("protoSource"))
			{
				exception.get_Data().Add("protoSource", string.Format("tag={0}; wire-type={1}; offset={2}; depth={3}", new object[]
				{
					source.fieldNumber,
					source.wireType,
					source.position,
					source.depth
				}));
			}
			return exception;
		}

		private static Exception EoF(ProtoReader source)
		{
			return ProtoReader.AddErrorData(new EndOfStreamException(), source);
		}

		public void AppendExtensionData(IExtensible instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IExtension extensionObject = instance.GetExtensionObject(true);
			bool commit = false;
			Stream stream = extensionObject.BeginAppend();
			try
			{
				using (ProtoWriter protoWriter = new ProtoWriter(stream, this.model, null))
				{
					this.AppendExtensionField(protoWriter);
					protoWriter.Close();
				}
				commit = true;
			}
			finally
			{
				extensionObject.EndAppend(stream, commit);
			}
		}

		private void AppendExtensionField(ProtoWriter writer)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, writer);
			WireType wireType = this.wireType;
			switch (wireType + 1)
			{
			case WireType.Fixed64:
			case WireType.String:
			case (WireType)9:
				ProtoWriter.WriteInt64(this.ReadInt64(), writer);
				return;
			case WireType.StartGroup:
				ProtoWriter.WriteBytes(ProtoReader.AppendBytes(null, this), writer);
				return;
			case WireType.EndGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(this);
				SubItemToken token2 = ProtoWriter.StartSubItem(null, writer);
				while (this.ReadFieldHeader() > 0)
				{
					this.AppendExtensionField(writer);
				}
				ProtoReader.EndSubItem(token, this);
				ProtoWriter.EndSubItem(token2, writer);
				return;
			}
			case (WireType)6:
				ProtoWriter.WriteInt32(this.ReadInt32(), writer);
				return;
			}
			throw this.CreateWireTypeException();
		}

		public static bool HasSubValue(WireType wireType, ProtoReader source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.blockEnd <= source.position || wireType == WireType.EndGroup)
			{
				return false;
			}
			source.wireType = wireType;
			return true;
		}

		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		internal Type DeserializeType(string value)
		{
			return TypeModel.DeserializeType(this.model, value);
		}

		internal void SetRootObject(object value)
		{
			this.netCache.SetKeyedObject(0, value);
			this.trapCount -= 1u;
		}

		public static void NoteObject(object value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.trapCount != 0u)
			{
				reader.netCache.RegisterTrappedObject(value);
				reader.trapCount -= 1u;
			}
		}

		public Type ReadType()
		{
			return TypeModel.DeserializeType(this.model, this.ReadString());
		}

		internal void TrapNextObject(int newObjectKey)
		{
			this.trapCount += 1u;
			this.netCache.SetKeyedObject(newObjectKey, null);
		}

		internal void CheckFullyConsumed()
		{
			if (this.isFixedLength)
			{
				if (this.dataRemaining != 0)
				{
					throw new ProtoException("Incorrect number of bytes consumed");
				}
			}
			else if (this.available != 0)
			{
				throw new ProtoException("Unconsumed data left in the buffer; this suggests corrupt input");
			}
		}

		public static object Merge(ProtoReader parent, object from, object to)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			TypeModel typeModel = parent.Model;
			SerializationContext serializationContext = parent.Context;
			if (typeModel == null)
			{
				throw new InvalidOperationException("Types cannot be merged unless a type-model has been specified");
			}
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				typeModel.Serialize(memoryStream, from, serializationContext);
				memoryStream.set_Position(0L);
				result = typeModel.Deserialize(memoryStream, to, null);
			}
			return result;
		}

		internal static ProtoReader Create(Stream source, TypeModel model, SerializationContext context, int len)
		{
			ProtoReader recycled = ProtoReader.GetRecycled();
			if (recycled == null)
			{
				return new ProtoReader(source, model, context, len);
			}
			ProtoReader.Init(recycled, source, model, context, len);
			return recycled;
		}

		private static ProtoReader GetRecycled()
		{
			ProtoReader result = ProtoReader.lastReader;
			ProtoReader.lastReader = null;
			return result;
		}

		internal static void Recycle(ProtoReader reader)
		{
			if (reader != null)
			{
				reader.Dispose();
				ProtoReader.lastReader = reader;
			}
		}
	}
}
