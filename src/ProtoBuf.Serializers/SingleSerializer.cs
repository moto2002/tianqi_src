using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class SingleSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(float);

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
				return SingleSerializer.expectedType;
			}
		}

		public SingleSerializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadSingle();
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSingle((float)value, dest);
		}
	}
}
