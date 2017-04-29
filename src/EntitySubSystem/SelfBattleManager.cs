using Package;
using System;
using System.Collections.Generic;

namespace EntitySubSystem
{
	public class SelfBattleManager : BattleManager
	{
		public override void Fit(BattleAction_Fit data, bool isServerData)
		{
			if (data.roleId != this.owner.ID)
			{
				return;
			}
			this.Fuse(data.fitPetId, data.fitModelId, data.fitSkills);
			EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, true);
			EventDispatcher.Broadcast<float>(EventNames.FuseTime, (float)data.durationTime);
		}

		public override void ExitFit(BattleAction_ExitFit data, bool isServerData)
		{
			if (data.roleId != this.owner.ID)
			{
				return;
			}
			this.DeFuse(data.roleModelId, data.roleSkills);
			EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, false);
		}

		public override void ChangeCamp(BattleAction_ChangeCamp data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.Camp = data.camp;
			InstanceManager.ResetCameraEvent();
		}

		protected void Fuse(long petID, int fuseModelId, List<BattleSkillInfo> skillInfo)
		{
			(this.owner as EntitySelf).FusePetID = ((PetManager.Instance.GetPetInfo(petID) != null) ? PetManager.Instance.GetPetInfo(petID).petId : 0);
			this.owner.IsFuse = true;
			this.owner.IsFusing = true;
			(this.owner as EntitySelf).ModelIDBackUp = this.owner.FixModelID;
			(this.owner as EntitySelf).BackUpSkill();
			this.owner.ModelID = fuseModelId;
			this.owner.ClearSkill();
			for (int i = 0; i < skillInfo.get_Count(); i++)
			{
				this.owner.AddSkill(skillInfo.get_Item(i).skillIdx, skillInfo.get_Item(i).skillId, skillInfo.get_Item(i).skillLv, skillInfo.get_Item(i).attrAdd);
			}
			this.owner.IsFusing = false;
			GlobalBattleNetwork.Instance.SendEndFusing(this.owner.ID);
		}

		protected void DeFuse(int defuseModelID, List<BattleSkillInfo> skillInfo)
		{
			(this.owner as EntityPlayer).FusePetID = 0;
			this.owner.IsFuse = false;
			this.owner.ModelID = defuseModelID;
			this.owner.ClearSkill();
			for (int i = 0; i < skillInfo.get_Count(); i++)
			{
				BattleSkillInfo battleSkillInfo = skillInfo.get_Item(i);
				if (battleSkillInfo.skillIdx > 0)
				{
					this.owner.AddSkill(battleSkillInfo.skillIdx, battleSkillInfo.skillId, battleSkillInfo.skillLv, battleSkillInfo.attrAdd);
				}
				else
				{
					this.owner.AddSkill(battleSkillInfo.skillId, battleSkillInfo.skillLv, battleSkillInfo.attrAdd);
				}
			}
		}
	}
}
