using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class BooleanSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(bool);

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
				return BooleanSerializer.expectedType;
			}
		}

		public BooleanSerializer(TypeModel model)
		{
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBoolean((bool)value, dest);
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadBoolean();
		}
	}
}
