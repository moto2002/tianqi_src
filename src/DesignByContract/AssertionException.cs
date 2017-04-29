using System;

namespace DesignByContract
{
	public class AssertionException : DesignByContractException
	{
		public AssertionException()
		{
		}

		public AssertionException(string message) : base(message)
		{
		}

		public AssertionException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
