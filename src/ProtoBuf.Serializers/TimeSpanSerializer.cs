using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class TimeSpanSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(TimeSpan);

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
				return TimeSpanSerializer.expectedType;
			}
		}

		public TimeSpanSerializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadTimeSpan(source);
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteTimeSpan((TimeSpan)value, dest);
		}
	}
}
