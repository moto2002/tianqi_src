using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class SystemTypeSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(Type);

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
				return SystemTypeSerializer.expectedType;
			}
		}

		public SystemTypeSerializer(TypeModel model)
		{
		}

		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteType((Type)value, dest);
		}

		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return source.ReadType();
		}
	}
}
