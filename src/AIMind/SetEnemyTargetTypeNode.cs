using AIRuntime;
using System;

namespace AIMind
{
	public class SetEnemyTargetTypeNode : Action
	{
		protected int mode;

		public SetEnemyTargetTypeNode(int Mode)
		{
			this.mode = Mode;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.SetEnemyTargetType(this.mode);
		}
	}
}
