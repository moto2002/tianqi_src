using AIRuntime;
using System;

namespace AIMind
{
	public class CheckConditionNode : ConditionConnectors
	{
		public int conditionID;

		public CheckConditionNode(int ConditionID)
		{
			this.conditionID = ConditionID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckCondition(this.conditionID);
		}
	}
}
