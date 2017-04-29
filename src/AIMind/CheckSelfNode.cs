using AIRuntime;
using System;

namespace AIMind
{
	public class CheckSelfNode : ConditionConnectors
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckSelf();
		}
	}
}
