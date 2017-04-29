using AIRuntime;
using System;

namespace AIMind
{
	public class FindValidTargetNode : Action
	{
		protected int nSkill;

		protected bool bUseRang;

		public FindValidTargetNode(int Skill, bool UseRang = false)
		{
			this.nSkill = Skill;
			this.bUseRang = UseRang;
		}

		public override bool Proc(IAIProc theOwner)
		{
			return theOwner.SetTargetBySkillIndex(this.nSkill, (!this.bUseRang) ? TargetRangeType.World : TargetRangeType.SkillRange, false, 50f);
		}
	}
}
