using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class Int64Serializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(long);

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
				return Int64Serializer.expectedType;
			}
		}

		public Int64Serializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt64();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt64((long)value, dest);
		}
	}
}
