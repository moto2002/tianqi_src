using AIRuntime;
using System;

namespace AIMind
{
	public class CheckExitWeakNode : ConditionConnectors
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckExitWeak();
		}
	}
}
