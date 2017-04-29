using AIRuntime;
using System;

namespace AIMind
{
	public class ChangeActNode : Action
	{
		protected string strAct = "idle";

		public ChangeActNode(string Act)
		{
			this.strAct = Act;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.ChangeAction(this.strAct);
		}
	}
}
