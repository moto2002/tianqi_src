using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ProtoBuf
{
	internal sealed class Helpers
	{
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;

		private Helpers()
		{
		}

		public static StringBuilder AppendLine(StringBuilder builder)
		{
			return builder.AppendLine();
		}

		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.get_Length() == 0;
		}

		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message, object obj)
		{
		}

		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message)
		{
		}

		[Conditional("TRACE")]
		public static void TraceWriteLine(string message)
		{
		}

		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message)
		{
		}

		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message, params object[] args)
		{
		}

		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition)
		{
		}

		public static void Sort(int[] keys, object[] values)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 1; i < keys.Length; i++)
				{
					if (keys[i - 1] > keys[i])
					{
						int num = keys[i];
						keys[i] = keys[i - 1];
						keys[i - 1] = num;
						object obj = values[i];
						values[i] = values[i - 1];
						values[i - 1] = obj;
						flag = true;
					}
				}
			}
			while (flag);
		}

		public static void BlockCopy(byte[] from, int fromIndex, byte[] to, int toIndex, int count)
		{
			Buffer.BlockCopy(from, fromIndex, to, toIndex, count);
		}

		public static bool IsInfinity(float value)
		{
			return float.IsInfinity(value);
		}

		internal static MethodInfo GetInstanceMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, 52);
		}

		internal static MethodInfo GetStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, 56);
		}

		internal static MethodInfo GetInstanceMethod(Type declaringType, string name, Type[] types)
		{
			if (types == null)
			{
				types = Helpers.EmptyTypes;
			}
			return declaringType.GetMethod(name, 52, null, types, null);
		}

		internal static bool IsSubclassOf(Type type, Type baseClass)
		{
			return type.IsSubclassOf(baseClass);
		}

		public static bool IsInfinity(double value)
		{
			return double.IsInfinity(value);
		}

		public static ProtoTypeCode GetTypeCode(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case 0:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
			case 18:
				return typeCode;
			}
			if (type == typeof(TimeSpan))
			{
				return ProtoTypeCode.TimeSpan;
			}
			if (type == typeof(Guid))
			{
				return ProtoTypeCode.Guid;
			}
			if (type == typeof(Uri))
			{
				return ProtoTypeCode.Uri;
			}
			if (type == typeof(byte[]))
			{
				return ProtoTypeCode.ByteArray;
			}
			if (type == typeof(Type))
			{
				return ProtoTypeCode.Type;
			}
			return ProtoTypeCode.Unknown;
		}

		internal static Type GetUnderlyingType(Type type)
		{
			return Nullable.GetUnderlyingType(type);
		}

		internal static bool IsValueType(Type type)
		{
			return type.get_IsValueType();
		}

		internal static bool IsEnum(Type type)
		{
			return type.get_IsEnum();
		}

		internal static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetGetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.get_IsAssembly() && !methodInfo.get_IsFamilyOrAssembly())
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		internal static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetSetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.get_IsAssembly() && !methodInfo.get_IsFamilyOrAssembly())
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		internal static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes, bool nonPublic)
		{
			return type.GetConstructor((!nonPublic) ? 20 : 52, null, parameterTypes, null);
		}

		internal static ConstructorInfo[] GetConstructors(Type type, bool nonPublic)
		{
			return type.GetConstructors((!nonPublic) ? 20 : 52);
		}

		internal static PropertyInfo GetProperty(Type type, string name, bool nonPublic)
		{
			return type.GetProperty(name, (!nonPublic) ? 20 : 52);
		}

		internal static object ParseEnum(Type type, string value)
		{
			return Enum.Parse(type, value, true);
		}

		internal static MemberInfo[] GetInstanceFieldsAndProperties(Type type, bool publicOnly)
		{
			BindingFlags bindingFlags = (!publicOnly) ? 52 : 20;
			PropertyInfo[] properties = type.GetProperties(bindingFlags);
			FieldInfo[] fields = type.GetFields(bindingFlags);
			MemberInfo[] array = new MemberInfo[fields.Length + properties.Length];
			properties.CopyTo(array, 0);
			fields.CopyTo(array, properties.Length);
			return array;
		}

		internal static Type GetMemberType(MemberInfo member)
		{
			MemberTypes memberType = member.get_MemberType();
			if (memberType == 4)
			{
				return ((FieldInfo)member).get_FieldType();
			}
			if (memberType != 16)
			{
				return null;
			}
			return ((PropertyInfo)member).get_PropertyType();
		}

		internal static bool IsAssignableFrom(Type target, Type type)
		{
			return target.IsAssignableFrom(type);
		}
	}
}
