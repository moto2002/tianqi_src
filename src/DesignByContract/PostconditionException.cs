using System;

namespace DesignByContract
{
	public class PostconditionException : DesignByContractException
	{
		public PostconditionException()
		{
		}

		public PostconditionException(string message) : base(message)
		{
		}

		public PostconditionException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
