using AIRuntime;
using System;

namespace AIMind
{
	public class MoveByForwardInThinkCountNode : Action
	{
		protected int thinkCount;

		public MoveByForwardInThinkCountNode(int ThinkCount)
		{
			this.thinkCount = ThinkCount;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveByForwardInThinkCount(this.thinkCount);
		}
	}
}
