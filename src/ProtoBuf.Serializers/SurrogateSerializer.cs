using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class SurrogateSerializer : IProtoSerializer, IProtoTypeSerializer
	{
		private readonly Type forType;

		private readonly Type declaredType;

		private readonly MethodInfo toTail;

		private readonly MethodInfo fromTail;

		private IProtoTypeSerializer rootTail;

		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		public bool RequiresOldValue
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
				return this.forType;
			}
		}

		public SurrogateSerializer(TypeModel model, Type forType, Type declaredType, IProtoTypeSerializer rootTail)
		{
			this.forType = forType;
			this.declaredType = declaredType;
			this.rootTail = rootTail;
			this.toTail = this.GetConversion(model, true);
			this.fromTail = this.GetConversion(model, false);
		}

		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		private static bool HasCast(TypeModel model, Type type, Type from, Type to, out MethodInfo op)
		{
			MethodInfo[] methods = type.GetMethods(56);
			Type type2 = null;
			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo methodInfo = methods[i];
				if (methodInfo.get_ReturnType() == to)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1 && parameters[0].get_ParameterType() == from)
					{
						if (type2 == null)
						{
							type2 = model.MapType(typeof(ProtoConverterAttribute), false);
							if (type2 == null)
							{
								break;
							}
						}
						if (methodInfo.IsDefined(type2, true))
						{
							op = methodInfo;
							return true;
						}
					}
				}
			}
			for (int j = 0; j < methods.Length; j++)
			{
				MethodInfo methodInfo2 = methods[j];
				if ((!(methodInfo2.get_Name() != "op_Implicit") || !(methodInfo2.get_Name() != "op_Explicit")) && methodInfo2.get_ReturnType() == to)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == 1 && parameters[0].get_ParameterType() == from)
					{
						op = methodInfo2;
						return true;
					}
				}
			}
			op = null;
			return false;
		}

		public MethodInfo GetConversion(TypeModel model, bool toTail)
		{
			Type to = (!toTail) ? this.forType : this.declaredType;
			Type from = (!toTail) ? this.declaredType : this.forType;
			MethodInfo result;
			if (SurrogateSerializer.HasCast(model, this.declaredType, from, to, out result) || SurrogateSerializer.HasCast(model, this.forType, from, to, out result))
			{
				return result;
			}
			throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + this.forType.get_FullName() + " / " + this.declaredType.get_FullName());
		}

		public void Write(object value, ProtoWriter writer)
		{
			this.rootTail.Write(this.toTail.Invoke(null, new object[]
			{
				value
			}), writer);
		}

		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[]
			{
				value
			};
			value = this.toTail.Invoke(null, array);
			array[0] = this.rootTail.Read(value, source);
			return this.fromTail.Invoke(null, array);
		}
	}
}
