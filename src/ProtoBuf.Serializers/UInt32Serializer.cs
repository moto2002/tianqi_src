using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class UInt32Serializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(uint);

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
				return UInt32Serializer.expectedType;
			}
		}

		public UInt32Serializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt32();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt32((uint)value, dest);
		}
	}
}
