using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class TypeSerializer : IProtoSerializer, IProtoTypeSerializer
	{
		private readonly Type forType;

		private readonly Type constructType;

		private readonly IProtoSerializer[] serializers;

		private readonly int[] fieldNumbers;

		private readonly bool isRootType;

		private readonly bool useConstructor;

		private readonly bool isExtensible;

		private readonly bool hasConstructor;

		private readonly CallbackSet callbacks;

		private readonly MethodInfo[] baseCtorCallbacks;

		private readonly MethodInfo factory;

		private static readonly Type iextensible = typeof(IExtensible);

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
				return false;
			}
		}

		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		private bool CanHaveInheritance
		{
			get
			{
				return (this.forType.get_IsClass() || this.forType.get_IsInterface()) && !this.forType.get_IsSealed();
			}
		}

		public TypeSerializer(TypeModel model, Type forType, int[] fieldNumbers, IProtoSerializer[] serializers, MethodInfo[] baseCtorCallbacks, bool isRootType, bool useConstructor, CallbackSet callbacks, Type constructType, MethodInfo factory)
		{
			Helpers.Sort(fieldNumbers, serializers);
			bool flag = false;
			for (int i = 1; i < fieldNumbers.Length; i++)
			{
				if (fieldNumbers[i] == fieldNumbers[i - 1])
				{
					throw new InvalidOperationException("Duplicate field-number detected; " + fieldNumbers[i].ToString() + " on: " + forType.get_FullName());
				}
				if (!flag && serializers[i].ExpectedType != forType)
				{
					flag = true;
				}
			}
			this.forType = forType;
			this.factory = factory;
			if (constructType == null)
			{
				constructType = forType;
			}
			else if (!forType.IsAssignableFrom(constructType))
			{
				throw new InvalidOperationException(forType.get_FullName() + " cannot be assigned from " + constructType.get_FullName());
			}
			this.constructType = constructType;
			this.serializers = serializers;
			this.fieldNumbers = fieldNumbers;
			this.callbacks = callbacks;
			this.isRootType = isRootType;
			this.useConstructor = useConstructor;
			if (baseCtorCallbacks != null && baseCtorCallbacks.Length == 0)
			{
				baseCtorCallbacks = null;
			}
			this.baseCtorCallbacks = baseCtorCallbacks;
			if (Helpers.GetUnderlyingType(forType) != null)
			{
				throw new ArgumentException("Cannot create a TypeSerializer for nullable types", "forType");
			}
			if (model.MapType(TypeSerializer.iextensible).IsAssignableFrom(forType))
			{
				if (forType.get_IsValueType() || !isRootType || flag)
				{
					throw new NotSupportedException("IExtensible is not supported in structs or classes with inheritance");
				}
				this.isExtensible = true;
			}
			this.hasConstructor = (!constructType.get_IsAbstract() && Helpers.GetConstructor(constructType, Helpers.EmptyTypes, true) != null);
			if (constructType != forType && useConstructor && !this.hasConstructor)
			{
				throw new ArgumentException("The supplied default implementation cannot be created: " + constructType.get_FullName(), "constructType");
			}
		}

		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return true;
		}

		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return this.CreateInstance(source, false);
		}

		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			if (this.callbacks != null && this.callbacks[callbackType] != null)
			{
				return true;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				if (this.serializers[i].ExpectedType != this.forType && ((IProtoTypeSerializer)this.serializers[i]).HasCallbacks(callbackType))
				{
					return true;
				}
			}
			return false;
		}

		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			if (this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks[callbackType], value, context);
			}
			IProtoTypeSerializer protoTypeSerializer = (IProtoTypeSerializer)this.GetMoreSpecificSerializer(value);
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		private IProtoSerializer GetMoreSpecificSerializer(object value)
		{
			if (!this.CanHaveInheritance)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == this.forType)
			{
				return null;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType != this.forType && Helpers.IsAssignableFrom(protoSerializer.ExpectedType, type))
				{
					return protoSerializer;
				}
			}
			if (type == this.constructType)
			{
				return null;
			}
			TypeModel.ThrowUnexpectedSubtype(this.forType, type);
			return null;
		}

		public void Write(object value, ProtoWriter dest)
		{
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeSerialize, dest.Context);
			}
			IProtoSerializer moreSpecificSerializer = this.GetMoreSpecificSerializer(value);
			if (moreSpecificSerializer != null)
			{
				moreSpecificSerializer.Write(value, dest);
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType == this.forType)
				{
					protoSerializer.Write(value, dest);
				}
			}
			if (this.isExtensible)
			{
				ProtoWriter.AppendExtensionData((IExtensible)value, dest);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterSerialize, dest.Context);
			}
		}

		public object Read(object value, ProtoReader source)
		{
			if (this.isRootType && value != null)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeDeserialize, source.Context);
			}
			int num = 0;
			int num2 = 0;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				bool flag = false;
				if (num3 < num)
				{
					num2 = (num = 0);
				}
				for (int i = num2; i < this.fieldNumbers.Length; i++)
				{
					if (this.fieldNumbers[i] == num3)
					{
						IProtoSerializer protoSerializer = this.serializers[i];
						Type expectedType = protoSerializer.ExpectedType;
						if (value == null)
						{
							if (expectedType == this.forType)
							{
								value = this.CreateInstance(source, true);
							}
						}
						else if (expectedType != this.forType && ((IProtoTypeSerializer)protoSerializer).CanCreateInstance() && expectedType.IsSubclassOf(value.GetType()))
						{
							value = ProtoReader.Merge(source, value, ((IProtoTypeSerializer)protoSerializer).CreateInstance(source));
						}
						if (protoSerializer.ReturnsValue)
						{
							value = protoSerializer.Read(value, source);
						}
						else
						{
							protoSerializer.Read(value, source);
						}
						num2 = i;
						num = num3;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (value == null)
					{
						value = this.CreateInstance(source, true);
					}
					if (this.isExtensible)
					{
						source.AppendExtensionData((IExtensible)value);
					}
					else
					{
						source.SkipField();
					}
				}
			}
			if (value == null)
			{
				value = this.CreateInstance(source, true);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterDeserialize, source.Context);
			}
			return value;
		}

		private object InvokeCallback(MethodInfo method, object obj, SerializationContext context)
		{
			object result = null;
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();
				int num = parameters.Length;
				object[] array;
				bool flag;
				if (num != 0)
				{
					array = new object[parameters.Length];
					flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						Type parameterType = parameters[i].get_ParameterType();
						object obj2;
						if (parameterType == typeof(SerializationContext))
						{
							obj2 = context;
						}
						else if (parameterType == typeof(Type))
						{
							obj2 = this.constructType;
						}
						else
						{
							obj2 = null;
							flag = false;
						}
						array[i] = obj2;
					}
				}
				else
				{
					array = null;
					flag = true;
				}
				if (!flag)
				{
					throw CallbackSet.CreateInvalidCallbackSignature(method);
				}
				result = method.Invoke(obj, array);
			}
			return result;
		}

		private object CreateInstance(ProtoReader source, bool includeLocalCallback)
		{
			object obj;
			if (this.factory != null)
			{
				obj = this.InvokeCallback(this.factory, null, source.Context);
			}
			else if (this.useConstructor)
			{
				if (!this.hasConstructor)
				{
					TypeModel.ThrowCannotCreateInstance(this.constructType);
				}
				obj = Activator.CreateInstance(this.constructType, true);
			}
			else
			{
				obj = BclHelpers.GetUninitializedObject(this.constructType);
			}
			ProtoReader.NoteObject(obj, source);
			if (this.baseCtorCallbacks != null)
			{
				for (int i = 0; i < this.baseCtorCallbacks.Length; i++)
				{
					this.InvokeCallback(this.baseCtorCallbacks[i], obj, source.Context);
				}
			}
			if (includeLocalCallback && this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks.BeforeDeserialize, obj, source.Context);
			}
			return obj;
		}
	}
}
