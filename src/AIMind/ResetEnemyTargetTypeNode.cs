using AIRuntime;
using System;

namespace AIMind
{
	public class ResetEnemyTargetTypeNode : Action
	{
		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.ResetEnemyTargetType();
		}
	}
}
