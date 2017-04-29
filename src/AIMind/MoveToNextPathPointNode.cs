using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToNextPathPointNode : Action
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToCurrentBatchPoint();
		}
	}
}
