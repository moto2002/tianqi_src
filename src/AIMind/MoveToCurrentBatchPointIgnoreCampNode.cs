using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToCurrentBatchPointIgnoreCampNode : Action
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToCurrentBatchPointIgnoreCamp();
		}
	}
}
