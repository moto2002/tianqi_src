using AIRuntime;
using System;

namespace AIMind
{
	public class CheckSelfContainBuffNode : ConditionConnectors
	{
		public int buffID;

		public CheckSelfContainBuffNode(int BuffID)
		{
			this.buffID = BuffID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckOwnerContainBuffID(this.buffID);
		}
	}
}
