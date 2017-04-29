using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	internal abstract class AttributeMap
	{
		private sealed class ReflectionAttributeMap : AttributeMap
		{
			private readonly Attribute attribute;

			public override object Target
			{
				get
				{
					return this.attribute;
				}
			}

			public override Type AttributeType
			{
				get
				{
					return this.attribute.GetType();
				}
			}

			public ReflectionAttributeMap(Attribute attribute)
			{
				this.attribute = attribute;
			}

			public override bool TryGet(string key, bool publicOnly, out object value)
			{
				MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(this.attribute.GetType(), publicOnly);
				MemberInfo[] array = instanceFieldsAndProperties;
				int i = 0;
				while (i < array.Length)
				{
					MemberInfo memberInfo = array[i];
					if (string.Equals(memberInfo.get_Name(), key, 5))
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null)
						{
							value = propertyInfo.GetValue(this.attribute, null);
							return true;
						}
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							value = fieldInfo.GetValue(this.attribute);
							return true;
						}
						throw new NotSupportedException(memberInfo.GetType().get_Name());
					}
					else
					{
						i++;
					}
				}
				value = null;
				return false;
			}
		}

		public abstract Type AttributeType
		{
			get;
		}

		public abstract object Target
		{
			get;
		}

		public abstract bool TryGet(string key, bool publicOnly, out object value);

		public bool TryGet(string key, out object value)
		{
			return this.TryGet(key, true, out value);
		}

		public static AttributeMap[] Create(TypeModel model, Type type, bool inherit)
		{
			object[] customAttributes = type.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		public static AttributeMap[] Create(TypeModel model, MemberInfo member, bool inherit)
		{
			object[] customAttributes = member.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		public static AttributeMap[] Create(TypeModel model, Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(false);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}
	}
}
