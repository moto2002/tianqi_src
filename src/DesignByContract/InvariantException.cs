using System;

namespace DesignByContract
{
	public class InvariantException : DesignByContractException
	{
		public InvariantException()
		{
		}

		public InvariantException(string message) : base(message)
		{
		}

		public InvariantException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
