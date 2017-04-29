using System;

namespace DesignByContract
{
	public class DesignByContractException : ApplicationException
	{
		protected DesignByContractException()
		{
		}

		protected DesignByContractException(string message) : base(message)
		{
		}

		protected DesignByContractException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
