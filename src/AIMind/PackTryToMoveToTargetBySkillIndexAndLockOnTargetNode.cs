using AIRuntime;
using System;

namespace AIMind
{
	public class PackTryToMoveToTargetBySkillIndexAndLockOnTargetNode : Action
	{
		protected int skillIndex;

		protected int random;

		protected bool isUseRange;

		public PackTryToMoveToTargetBySkillIndexAndLockOnTargetNode(bool IsUseRange, int Random, int SkillIndex)
		{
			this.skillIndex = SkillIndex;
			this.random = Random;
			this.isUseRange = IsUseRange;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PackTryToMoveToTargetBySkillIndexAndLockOnTarget(this.skillIndex, this.random, (!this.isUseRange) ? TargetRangeType.World : TargetRangeType.SkillRange);
		}
	}
}
