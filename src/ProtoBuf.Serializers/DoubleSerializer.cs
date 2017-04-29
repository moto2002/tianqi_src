using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class DoubleSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(double);

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
				return DoubleSerializer.expectedType;
			}
		}

		public DoubleSerializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadDouble();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteDouble((double)value, dest);
		}
	}
}
