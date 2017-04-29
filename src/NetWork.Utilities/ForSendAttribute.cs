using System;

namespace NetWork.Utilities
{
	public class ForSendAttribute : Attribute
	{
		public short code;

		public ForSendAttribute(short code)
		{
			this.code = code;
		}
	}
}
