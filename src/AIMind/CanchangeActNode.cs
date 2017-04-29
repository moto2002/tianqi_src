using AIRuntime;
using System;

namespace AIMind
{
	public class CanchangeActNode : ConditionConnectors
	{
		protected string strAct = "strAct";

		public CanchangeActNode(string Act)
		{
			this.strAct = Act;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.CheckCanChangeActionTo(this.strAct);
		}
	}
}
