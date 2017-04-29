using System;
using System.Diagnostics;

namespace DesignByContract
{
	public sealed class Check
	{
		private static bool useAssertions;

		public static bool UseAssertions
		{
			get
			{
				return Check.useAssertions;
			}
			set
			{
				Check.useAssertions = value;
			}
		}

		private static bool UseExceptions
		{
			get
			{
				return !Check.useAssertions;
			}
		}

		private Check()
		{
		}

		[Conditional("DBC_CHECK_PRECONDITION"), Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT")]
		public static void Require(bool assertion, string message)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PreconditionException(message);
				}
			}
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_PRECONDITION"), Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Require(bool assertion, string message, Exception inner)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PreconditionException(message, inner);
				}
			}
		}

		[Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_PRECONDITION")]
		public static void Require(bool assertion)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PreconditionException("Precondition failed.");
				}
			}
		}

		[Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Ensure(bool assertion, string message)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PostconditionException(message);
				}
			}
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_ALL")]
		public static void Ensure(bool assertion, string message, Exception inner)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PostconditionException(message, inner);
				}
			}
		}

		[Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Ensure(bool assertion)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new PostconditionException("Postcondition failed.");
				}
			}
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_ALL")]
		public static void Invariant(bool assertion, string message)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new InvariantException(message);
				}
			}
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_ALL")]
		public static void Invariant(bool assertion, string message, Exception inner)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new InvariantException(message, inner);
				}
			}
		}

		[Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT")]
		public static void Invariant(bool assertion)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new InvariantException("Invariant failed.");
				}
			}
		}

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion, string message)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new AssertionException(message);
				}
			}
		}

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion, string message, Exception inner)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new AssertionException(message, inner);
				}
			}
		}

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion)
		{
			if (Check.UseExceptions)
			{
				if (!assertion)
				{
					throw new AssertionException("Assertion failed.");
				}
			}
		}

		[Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_PRECONDITION"), Obsolete("Set Check.UseAssertions = true and then call Check.Require")]
		public static void RequireTrace(bool assertion, string message)
		{
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_PRECONDITION"), Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Require")]
		public static void RequireTrace(bool assertion)
		{
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Ensure")]
		public static void EnsureTrace(bool assertion, string message)
		{
		}

		[Conditional("DBC_CHECK_POSTCONDITION"), Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Ensure")]
		public static void EnsureTrace(bool assertion)
		{
		}

		[Conditional("DBC_CHECK_INVARIANT"), Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Invariant")]
		public static void InvariantTrace(bool assertion, string message)
		{
		}

		[Conditional("DBC_CHECK_ALL"), Conditional("DBC_CHECK_INVARIANT"), Obsolete("Set Check.UseAssertions = true and then call Check.Invariant")]
		public static void InvariantTrace(bool assertion)
		{
		}

		[Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Assert")]
		public static void AssertTrace(bool assertion, string message)
		{
		}

		[Conditional("DBC_CHECK_ALL"), Obsolete("Set Check.UseAssertions = true and then call Check.Assert")]
		public static void AssertTrace(bool assertion)
		{
		}
	}
}
