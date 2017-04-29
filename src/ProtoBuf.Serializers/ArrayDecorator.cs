using ProtoBuf.Meta;
using System;
using System.Collections;

namespace ProtoBuf.Serializers
{
	internal sealed class ArrayDecorator : ProtoDecoratorBase
	{
		private const byte OPTIONS_WritePacked = 1;

		private const byte OPTIONS_OverwriteList = 2;

		private const byte OPTIONS_SupportNull = 4;

		private readonly int fieldNumber;

		private readonly byte options;

		private readonly WireType packedWireType;

		private readonly Type arrayType;

		private readonly Type itemType;

		public override Type ExpectedType
		{
			get
			{
				return this.arrayType;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		private bool AppendToCollection
		{
			get
			{
				return (this.options & 2) == 0;
			}
		}

		private bool SupportNull
		{
			get
			{
				return (this.options & 4) != 0;
			}
		}

		public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull) : base(tail)
		{
			this.itemType = arrayType.GetElementType();
			Type arg_3E_0 = (!supportNull) ? (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType) : this.itemType;
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			this.packedWireType = packedWireType;
			if (writePacked)
			{
				this.options |= 1;
			}
			if (overwriteList)
			{
				this.options |= 2;
			}
			if (supportNull)
			{
				this.options |= 4;
			}
			this.arrayType = arrayType;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			IList list = (IList)value;
			int count = list.get_Count();
			bool flag = (this.options & 1) != 0;
			SubItemToken token;
			if (flag)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag2 = !this.SupportNull;
			for (int i = 0; i < count; i++)
			{
				object obj = list.get_Item(i);
				if (flag2 && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (flag)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			BasicList basicList = new BasicList();
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			int num = (!this.AppendToCollection) ? 0 : ((value != null) ? ((Array)value).get_Length() : 0);
			Array array = Array.CreateInstance(this.itemType, num + basicList.Count);
			if (num != 0)
			{
				((Array)value).CopyTo(array, 0);
			}
			basicList.CopyTo(array, num);
			return array;
		}
	}
}
