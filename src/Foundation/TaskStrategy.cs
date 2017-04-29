using System;

namespace Foundation
{
	public enum TaskStrategy
	{
		BackgroundThread,
		MainThread,
		CurrentThread,
		Coroutine,
		Custom
	}
}
