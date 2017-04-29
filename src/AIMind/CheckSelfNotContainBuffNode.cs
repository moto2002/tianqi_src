using AIRuntime;
using System;

namespace AIMind
{
	public class CheckSelfNotContainBuffNode : ConditionConnectors
	{
		public int buffID;

		public CheckSelfNotContainBuffNode(int BuffID)
		{
			this.buffID = BuffID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return !theOwner.CheckOwnerContainBuffID(this.buffID);
		}
	}
}
