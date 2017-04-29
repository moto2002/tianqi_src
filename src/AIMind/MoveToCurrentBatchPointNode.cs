using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToCurrentBatchPointNode : Action
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToCurrentBatchPoint();
		}
	}
}
