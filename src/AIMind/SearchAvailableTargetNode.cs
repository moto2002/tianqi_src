using AIRuntime;
using System;

namespace AIMind
{
	public class SearchAvailableTargetNode : Action
	{
		protected int skillIndex;

		protected bool isUseSkillRange;

		protected bool isUseConfigRange;

		public SearchAvailableTargetNode(int SkillIndex, bool IsUseSkillRange = false, bool IsUseConfigRange = false)
		{
			this.skillIndex = SkillIndex;
			this.isUseSkillRange = IsUseSkillRange;
			this.isUseConfigRange = IsUseConfigRange;
		}

		public override bool Proc(IAIProc theOwner)
		{
			TargetRangeType rangeType = TargetRangeType.World;
			if (this.isUseConfigRange)
			{
				rangeType = TargetRangeType.Configure;
			}
			else if (this.isUseSkillRange)
			{
				rangeType = TargetRangeType.SkillRange;
			}
			return theOwner.SetTargetBySkillIndex(this.skillIndex, rangeType, false, 50f);
		}
	}
}
