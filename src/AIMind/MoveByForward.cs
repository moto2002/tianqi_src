using AIRuntime;
using System;

namespace AIMind
{
	public class MoveByForward : Action
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveByForward();
		}
	}
}
