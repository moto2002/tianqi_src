using AIRuntime;
using System;

namespace AIMind
{
	public class CheckTargetContainBuffNode : ConditionConnectors
	{
		public int buffID;

		public CheckTargetContainBuffNode(int BuffID)
		{
			this.buffID = BuffID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckTargetContainBuffID(this.buffID);
		}
	}
}
