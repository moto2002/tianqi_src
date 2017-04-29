using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	public class CallbackSet
	{
		private readonly MetaType metaType;

		private MethodInfo beforeSerialize;

		private MethodInfo afterSerialize;

		private MethodInfo beforeDeserialize;

		private MethodInfo afterDeserialize;

		internal MethodInfo this[TypeModel.CallbackType callbackType]
		{
			get
			{
				switch (callbackType)
				{
				case TypeModel.CallbackType.BeforeSerialize:
					return this.beforeSerialize;
				case TypeModel.CallbackType.AfterSerialize:
					return this.afterSerialize;
				case TypeModel.CallbackType.BeforeDeserialize:
					return this.beforeDeserialize;
				case TypeModel.CallbackType.AfterDeserialize:
					return this.afterDeserialize;
				default:
					throw new ArgumentException("Callback type not supported: " + callbackType.ToString(), "callbackType");
				}
			}
		}

		public MethodInfo BeforeSerialize
		{
			get
			{
				return this.beforeSerialize;
			}
			set
			{
				this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		public MethodInfo BeforeDeserialize
		{
			get
			{
				return this.beforeDeserialize;
			}
			set
			{
				this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		public MethodInfo AfterSerialize
		{
			get
			{
				return this.afterSerialize;
			}
			set
			{
				this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		public MethodInfo AfterDeserialize
		{
			get
			{
				return this.afterDeserialize;
			}
			set
			{
				this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		public bool NonTrivial
		{
			get
			{
				return this.beforeSerialize != null || this.beforeDeserialize != null || this.afterSerialize != null || this.afterDeserialize != null;
			}
		}

		internal CallbackSet(MetaType metaType)
		{
			if (metaType == null)
			{
				throw new ArgumentNullException("metaType");
			}
			this.metaType = metaType;
		}

		internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				Type parameterType = parameters[i].get_ParameterType();
				if (parameterType != model.MapType(typeof(SerializationContext)))
				{
					if (parameterType != model.MapType(typeof(Type)))
					{
						return false;
					}
				}
			}
			return true;
		}

		private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
		{
			this.metaType.ThrowIfFrozen();
			if (callback == null)
			{
				return callback;
			}
			if (callback.get_IsStatic())
			{
				throw new ArgumentException("Callbacks cannot be static", "callback");
			}
			if (callback.get_ReturnType() != model.MapType(typeof(void)) || !CallbackSet.CheckCallbackParameters(model, callback))
			{
				throw CallbackSet.CreateInvalidCallbackSignature(callback);
			}
			return callback;
		}

		internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
		{
			return new NotSupportedException("Invalid callback signature in " + method.get_DeclaringType().get_FullName() + "." + method.get_Name());
		}
	}
}
