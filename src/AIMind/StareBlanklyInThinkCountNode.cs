using AIRuntime;
using System;

namespace AIMind
{
	public class StareBlanklyInThinkCountNode : Action
	{
		protected int thinkCount;

		public StareBlanklyInThinkCountNode(int ThinkCount)
		{
			this.thinkCount = ThinkCount;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.StareBlanklyInThinkCount(this.thinkCount);
		}
	}
}
