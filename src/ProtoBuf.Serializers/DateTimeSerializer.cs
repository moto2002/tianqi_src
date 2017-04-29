using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class DateTimeSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(DateTime);

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
				return DateTimeSerializer.expectedType;
			}
		}

		public DateTimeSerializer(TypeModel model)
		{
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDateTime(source);
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDateTime((DateTime)value, dest);
		}
	}
}
