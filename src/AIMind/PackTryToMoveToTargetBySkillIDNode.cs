using AIRuntime;
using System;

namespace AIMind
{
	public class PackTryToMoveToTargetBySkillIDNode : Action
	{
		protected int skillID;

		protected int random;

		protected bool isUseRange;

		public PackTryToMoveToTargetBySkillIDNode(bool IsUseRange, int Random, int SkillID)
		{
			this.skillID = SkillID;
			this.random = Random;
			this.isUseRange = IsUseRange;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.PackTryToMoveToTargetBySkillID(this.skillID, this.random, (!this.isUseRange) ? TargetRangeType.World : TargetRangeType.SkillRange);
		}
	}
}
