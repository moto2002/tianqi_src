using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal class UInt16Serializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(ushort);

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

		public virtual Type ExpectedType
		{
			get
			{
				return UInt16Serializer.expectedType;
			}
		}

		public UInt16Serializer(TypeModel model)
		{
		}

		public virtual object Read(object value, ProtoReader source)
		{
			return source.ReadUInt16();
		}

		public virtual void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)value, dest);
		}
	}
}
