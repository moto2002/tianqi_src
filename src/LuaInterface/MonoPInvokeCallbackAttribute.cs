using System;

namespace LuaInterface
{
	[AttributeUsage]
	public sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		public MonoPInvokeCallbackAttribute(Type type)
		{
		}
	}
}
