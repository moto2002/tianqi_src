using AIRuntime;
using System;

namespace AIMind
{
	public class MoveToEffectOutsideNode : Action
	{
		protected int thinkCount;

		public MoveToEffectOutsideNode(int ThinkCount)
		{
			this.thinkCount = ThinkCount;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.MoveToEffectOutside(this.thinkCount);
		}
	}
}
