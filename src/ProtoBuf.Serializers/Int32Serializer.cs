using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class Int32Serializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(int);

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
				return Int32Serializer.expectedType;
			}
		}

		public Int32Serializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt32();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt32((int)value, dest);
		}
	}
}
