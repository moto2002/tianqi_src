using System;
using System.Collections.Generic;

namespace EntitySubSystem
{
	public interface IConditionManager
	{
		void RegistCounterSkillCondition(List<int> skillID);

		void RegistThinkCondition(List<int> node);

		void RegistTriggerSkillCondition(List<int> conditionID, List<int> skillID);

		void UnregistCondition();
	}
}
