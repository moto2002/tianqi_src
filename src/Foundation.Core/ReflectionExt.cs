using System;
using System.Linq;
using System.Reflection;

namespace Foundation.Core
{
	public static class ReflectionExt
	{
		public static bool HasAttribute<T>(this MemberInfo m) where T : Attribute
		{
			return Attribute.IsDefined(m, typeof(T));
		}

		public static T GetAttribute<T>(this MemberInfo m) where T : Attribute
		{
			return Enumerable.FirstOrDefault<object>(m.GetCustomAttributes(typeof(T), true)) as T;
		}

		public static T GetAttribute<T>(this object m, string memberName) where T : Attribute
		{
			MemberInfo memberInfo = Enumerable.FirstOrDefault<MemberInfo>(m.GetType().GetMember(memberName, 1060));
			if (memberInfo == null)
			{
				return (T)((object)null);
			}
			return memberInfo.GetAttribute<T>();
		}

		public static Type GetMemberType(this MemberInfo member)
		{
			if (member is MethodInfo)
			{
				return ((MethodInfo)member).get_ReturnType();
			}
			if (member is PropertyInfo)
			{
				return ((PropertyInfo)member).get_PropertyType();
			}
			return ((FieldInfo)member).get_FieldType();
		}

		public static Type GetParamaterType(this MemberInfo member)
		{
			if (member is MethodInfo)
			{
				ParameterInfo parameterInfo = Enumerable.FirstOrDefault<ParameterInfo>(((MethodInfo)member).GetParameters());
				if (parameterInfo == null)
				{
					return null;
				}
				return parameterInfo.get_ParameterType();
			}
			else
			{
				if (member is PropertyInfo)
				{
					return ((PropertyInfo)member).get_PropertyType();
				}
				if (member is FieldInfo)
				{
					return ((FieldInfo)member).get_FieldType();
				}
				return null;
			}
		}

		public static void SetMemberValue(this MemberInfo member, object instance, object value)
		{
			if (member is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)member;
				if (Enumerable.Any<ParameterInfo>(methodInfo.GetParameters()))
				{
					methodInfo.Invoke(instance, new object[]
					{
						value
					});
				}
				else
				{
					methodInfo.Invoke(instance, null);
				}
			}
			else if (member is PropertyInfo)
			{
				((PropertyInfo)member).SetValue(instance, value, null);
			}
			else
			{
				((FieldInfo)member).SetValue(instance, value);
			}
		}

		public static object GetMemberValue(this MemberInfo member, object instance)
		{
			if (member is MethodInfo)
			{
				return ((MethodInfo)member).Invoke(instance, null);
			}
			if (member is PropertyInfo)
			{
				return ((PropertyInfo)member).GetValue(instance, null);
			}
			return ((FieldInfo)member).GetValue(instance);
		}

		public static object GetMemberValue(this object instance, string propertyName)
		{
			MemberInfo memberInfo = Enumerable.FirstOrDefault<MemberInfo>(instance.GetType().GetMember(propertyName));
			if (memberInfo == null)
			{
				return null;
			}
			if (memberInfo is MethodInfo)
			{
				return ((MethodInfo)memberInfo).Invoke(instance, null);
			}
			if (memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)memberInfo).GetValue(instance, null);
			}
			return ((FieldInfo)memberInfo).GetValue(instance);
		}

		public static T GetMemberValue<T>(this MemberInfo member, object instance)
		{
			return (T)((object)member.GetMemberValue(instance));
		}
	}
}
