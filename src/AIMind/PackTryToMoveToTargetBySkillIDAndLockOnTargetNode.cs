using AIRuntime;
using System;

namespace AIMind
{
	public class PackTryToMoveToTargetBySkillIDAndLockOnTargetNode : Action
	{
		protected int skillID;

		protected int random;

		protected bool isUseRange;

		public PackTryToMoveToTargetBySkillIDAndLockOnTargetNode(bool IsUseRange, int Random, int SkillID)
		{
			this.skillID = SkillID;
			this.random = Random;
			this.isUseRange = IsUseRange;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PackTryToMoveToTargetBySkillIDAndLockOnTarget(this.skillID, this.random, (!this.isUseRange) ? TargetRangeType.World : TargetRangeType.SkillRange);
		}
	}
}
