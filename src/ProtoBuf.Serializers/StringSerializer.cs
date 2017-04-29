using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class StringSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(string);

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
				return StringSerializer.expectedType;
			}
		}

		public StringSerializer(TypeModel model)
		{
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString((string)value, dest);
		}

		public object Read(object value, ProtoReader source)
		{
			return source.ReadString();
		}
	}
}
