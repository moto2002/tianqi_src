using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class Int16Serializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(short);

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
				return Int16Serializer.expectedType;
			}
		}

		public Int16Serializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt16();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt16((short)value, dest);
		}
	}
}
