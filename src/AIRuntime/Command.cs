using AIMind;
using System;

namespace AIRuntime
{
	public abstract class Command
	{
		public abstract void AddChild(Command root);

		public abstract bool Proc(IAIProc theOwner);
	}
}
