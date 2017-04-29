using System;
using System.Reflection;

namespace XNetwork
{
	public class NetHandler
	{
		public MethodInfo Method
		{
			get;
			set;
		}

		public object Target
		{
			get;
			set;
		}

		public NetHandler(MethodInfo theMethod, object theTarget)
		{
			this.Method = theMethod;
			this.Target = theTarget;
		}
	}
}
