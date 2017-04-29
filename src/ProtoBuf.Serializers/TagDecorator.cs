using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class TagDecorator : ProtoDecoratorBase, IProtoSerializer, IProtoTypeSerializer
	{
		private readonly bool strict;

		private readonly int fieldNumber;

		private readonly WireType wireType;

		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		private bool NeedsHint
		{
			get
			{
				return (this.wireType & (WireType)(-8)) != WireType.Variant;
			}
		}

		public TagDecorator(int fieldNumber, WireType wireType, bool strict, IProtoSerializer tail) : base(tail)
		{
			this.fieldNumber = fieldNumber;
			this.wireType = wireType;
			this.strict = strict;
		}

		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.HasCallbacks(callbackType);
		}

		public bool CanCreateInstance()
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.CanCreateInstance();
		}

		public object CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.Tail).CreateInstance(source);
		}

		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			if (this.strict)
			{
				source.Assert(this.wireType);
			}
			else if (this.NeedsHint)
			{
				source.Hint(this.wireType);
			}
			return this.Tail.Read(value, source);
		}

		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
			this.Tail.Write(value, dest);
		}
	}
}
