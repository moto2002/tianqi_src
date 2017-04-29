using System;

namespace NetWork.Utilities
{
	public class ForRecvAttribute : Attribute
	{
		public short code;

		public ForRecvAttribute(short code)
		{
			this.code = code;
		}
	}
}
