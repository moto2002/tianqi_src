using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class BlobSerializer : IProtoSerializer
	{
		private static readonly Type expectedType = typeof(byte[]);

		private readonly bool overwriteList;

		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return !this.overwriteList;
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
				return BlobSerializer.expectedType;
			}
		}

		public BlobSerializer(TypeModel model, bool overwriteList)
		{
			this.overwriteList = overwriteList;
		}

		public object Read(object value, ProtoReader source)
		{
			return ProtoReader.AppendBytes((!this.overwriteList) ? ((byte[])value) : null, source);
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBytes((byte[])value, dest);
		}
	}
}
