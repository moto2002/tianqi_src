using GameData;
using System;

namespace EntitySubSystem
{
	public class PlayerConditionManager : ConditionManager
	{
		protected override bool CheckConditionTarget(Condition conditionData, EntityParent announcer)
		{
			switch (conditionData.target)
			{
			case 1:
				return this.owner.Camp != announcer.Camp;
			case 2:
				return this.owner.Camp == announcer.Camp && this.owner.ID != announcer.ID;
			case 3:
				return this.owner.ID == announcer.ID;
			case 4:
				return this.owner.Camp == announcer.Camp;
			case 5:
				return false;
			case 6:
				return true;
			case 7:
				return this.owner.OwnedIDs.Contains(announcer.ID);
			case 8:
				return announcer.ID == this.owner.DamageSourceID;
			case 9:
				return announcer.ID == EntityWorld.Instance.EntSelf.ID;
			case 10:
				return announcer.IsLogicBoss;
			case 11:
				return announcer.IsBuffEntity;
			default:
				return true;
			}
		}
	}
}
