using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class SubItemSerializer : IProtoSerializer, IProtoTypeSerializer
	{
		private readonly int key;

		private readonly Type type;

		private readonly ISerializerProxy proxy;

		private readonly bool recursionCheck;

		Type IProtoSerializer.ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		public SubItemSerializer(Type type, int key, ISerializerProxy proxy, bool recursionCheck)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.type = type;
			this.proxy = proxy;
			this.key = key;
			this.recursionCheck = recursionCheck;
		}

		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).HasCallbacks(callbackType);
		}

		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CanCreateInstance();
		}

		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).Callback(value, callbackType, context);
		}

		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CreateInstance(source);
		}

		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			if (this.recursionCheck)
			{
				ProtoWriter.WriteObject(value, this.key, dest);
			}
			else
			{
				ProtoWriter.WriteRecursionSafeObject(value, this.key, dest);
			}
		}

		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return ProtoReader.ReadObject(value, this.key, source);
		}
	}
}
