using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class GuidSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(Guid);

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
				return GuidSerializer.expectedType;
			}
		}

		public GuidSerializer(TypeModel model)
		{
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteGuid((Guid)value, dest);
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadGuid(source);
		}
	}
}
