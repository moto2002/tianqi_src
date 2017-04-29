using AIRuntime;
using System;

namespace AIMind
{
	public class CheckIsInStrategyNode : ConditionConnectors
	{
		public int strategyID;

		public bool isTrue = true;

		public CheckIsInStrategyNode(bool IsTrue, int StrategyID)
		{
			this.isTrue = IsTrue;
			this.strategyID = StrategyID;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckIsInStrategy(this.strategyID, this.isTrue);
		}
	}
}
