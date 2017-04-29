using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class DecimalSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(decimal);

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
				return DecimalSerializer.expectedType;
			}
		}

		public DecimalSerializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDecimal(source);
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDecimal((decimal)value, dest);
		}
	}
}
