using System;

namespace DesignByContract
{
	public class PreconditionException : DesignByContractException
	{
		public PreconditionException()
		{
		}

		public PreconditionException(string message) : base(message)
		{
		}

		public PreconditionException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
