using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class NetObjectSerializer : IProtoSerializer
	{
		private readonly int key;

		private readonly Type type;

		private readonly BclHelpers.NetObjectOptions options;

		public Type ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		public bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
		{
			bool flag = (byte)(options & BclHelpers.NetObjectOptions.DynamicType) != 0;
			this.key = ((!flag) ? key : -1);
			this.type = ((!flag) ? type : model.MapType(typeof(object)));
			this.options = options;
		}

		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadNetObject(value, source, this.key, (this.type != typeof(object)) ? this.type : null, this.options);
		}

		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteNetObject(value, dest, this.key, this.options);
		}
	}
}
