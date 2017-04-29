using System;

namespace Foundation.Core
{
	public static class ConverterHelper
	{
		public static object ConvertTo<T>(object paramater)
		{
			return ConverterHelper.ConvertTo(typeof(T), paramater);
		}

		public static object ConvertTo(Type desiredType, object paramater)
		{
			if (paramater == null)
			{
				return null;
			}
			if (desiredType.IsInstanceOfType(paramater))
			{
				return paramater;
			}
			if (desiredType.get_IsEnum())
			{
				return Enum.Parse(desiredType, paramater.ToString());
			}
			if (desiredType == typeof(string))
			{
				return paramater.ToString();
			}
			if (desiredType == typeof(bool))
			{
				return bool.Parse(paramater.ToString());
			}
			if (desiredType == typeof(int))
			{
				return int.Parse(paramater.ToString());
			}
			if (desiredType == typeof(float))
			{
				return float.Parse(paramater.ToString());
			}
			if (desiredType == typeof(long))
			{
				return long.Parse(paramater.ToString());
			}
			if (desiredType == typeof(double))
			{
				return double.Parse(paramater.ToString());
			}
			if (desiredType == typeof(short))
			{
				return short.Parse(paramater.ToString());
			}
			if (desiredType != typeof(object) || paramater is string)
			{
			}
			return null;
		}

		public static object ConvertTo(Type desiredType, float paramater)
		{
			if (desiredType == typeof(int))
			{
				return (int)paramater;
			}
			return ConverterHelper.ConvertTo(desiredType, paramater);
		}
	}
}
