using AIRuntime;
using System;

namespace AIMind
{
	public class SetTargetByLockOnTargetNode : Action
	{
		protected int skillIndex;

		protected bool isUseRushDistance;

		public SetTargetByLockOnTargetNode(int SkillIndex, bool IsUseRushDistance = false)
		{
			this.skillIndex = SkillIndex;
			this.isUseRushDistance = IsUseRushDistance;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.SetTargetFromLockOnTargetBySkillIndex(this.skillIndex, TargetRangeType.SkillRange, this.isUseRushDistance);
		}
	}
}
