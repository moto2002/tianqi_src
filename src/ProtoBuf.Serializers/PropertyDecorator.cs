using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class PropertyDecorator : ProtoDecoratorBase
	{
		private readonly PropertyInfo property;

		private readonly Type forType;

		private readonly bool readOptionsWriteValue;

		private readonly MethodInfo shadowSetter;

		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		public PropertyDecorator(TypeModel model, Type forType, PropertyInfo property, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.property = property;
			PropertyDecorator.SanityCheck(model, property, tail, out this.readOptionsWriteValue, true, true);
			this.shadowSetter = PropertyDecorator.GetShadowSetter(model, property);
		}

		private static void SanityCheck(TypeModel model, PropertyInfo property, IProtoSerializer tail, out bool writeValue, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			writeValue = (tail.ReturnsValue && (PropertyDecorator.GetShadowSetter(model, property) != null || (property.get_CanWrite() && Helpers.GetSetMethod(property, nonPublic, allowInternal) != null)));
			if (!property.get_CanRead() || Helpers.GetGetMethod(property, nonPublic, allowInternal) == null)
			{
				throw new InvalidOperationException("Cannot serialize property without a get accessor");
			}
			if (!writeValue && (!tail.RequiresOldValue || Helpers.IsValueType(tail.ExpectedType)))
			{
				throw new InvalidOperationException("Cannot apply changes to property " + property.get_DeclaringType().get_FullName() + "." + property.get_Name());
			}
		}

		private static MethodInfo GetShadowSetter(TypeModel model, PropertyInfo property)
		{
			Type reflectedType = property.get_ReflectedType();
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(reflectedType, "Set" + property.get_Name(), new Type[]
			{
				property.get_PropertyType()
			});
			if (instanceMethod == null || !instanceMethod.get_IsPublic() || instanceMethod.get_ReturnType() != model.MapType(typeof(void)))
			{
				return null;
			}
			return instanceMethod;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			value = this.property.GetValue(value, null);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			object value2 = (!this.Tail.RequiresOldValue) ? null : this.property.GetValue(value, null);
			object obj = this.Tail.Read(value2, source);
			if (this.readOptionsWriteValue && obj != null)
			{
				if (this.shadowSetter == null)
				{
					this.property.SetValue(value, obj, null);
				}
				else
				{
					this.shadowSetter.Invoke(value, new object[]
					{
						obj
					});
				}
			}
			return null;
		}

		internal static bool CanWrite(TypeModel model, MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.get_CanWrite() || PropertyDecorator.GetShadowSetter(model, propertyInfo) != null;
			}
			return member is FieldInfo;
		}
	}
}
