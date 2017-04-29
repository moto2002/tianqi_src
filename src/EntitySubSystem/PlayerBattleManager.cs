using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntitySubSystem
{
	public class PlayerBattleManager : BattleManager
	{
		public override void Bleed(BattleAction_Bleed data, bool isServerData)
		{
			if (data.bleedSoldierId != this.owner.ID)
			{
				return;
			}
			if (data.dmgSrcSoldierId == EntityWorld.Instance.EntSelf.ID || EntityWorld.Instance.EntSelf.OwnedIDs.Contains(data.dmgSrcSoldierId))
			{
				if (EntityWorld.Instance.AllEntities.ContainsKey(data.dmgSrcSoldierId))
				{
					this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, EntityWorld.Instance.AllEntities[data.dmgSrcSoldierId], data.isCrt, data.isParry, data.isMiss));
				}
				else
				{
					this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, null, data.isCrt, data.isParry, data.isMiss));
				}
			}
			this.owner.DamageSourceID = data.dmgSrcSoldierId;
			this.owner.Hp = data.hp;
		}

		public override void Relive(BattleAction_Relive data, bool isServerData)
		{
			base.Relive(data, isServerData);
			if (this.owner.Actor)
			{
				AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.owner.FixModelID);
				this.owner.InitBillboard((float)avatarModel.height_HP, avatarModel.bloodBar);
				WaveBloodManager.Instance.AddWaveBloodControl(this.owner.Actor.FixTransform, (float)avatarModel.height_Damage, this.owner.ID);
				if (!this.owner.IsPlayerMate)
				{
					EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerBorn, this.owner.TypeID, this.owner.Actor.FixTransform);
				}
			}
			else
			{
				Debuger.Error("Relive No Actor", new object[0]);
			}
		}

		public override void Fit(BattleAction_Fit data, bool isServerData)
		{
			if (data.roleId != this.owner.ID)
			{
				return;
			}
			this.Fuse(data.fitPetId, data.fitModelId, data.fitSkills);
			EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, true);
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

		public override void Assault(BattleAction_Assault data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.CheckCancelManage(data.soldierId, data.oldManageState, false);
			this.owner.IsAssault = true;
			if (this.owner.Actor)
			{
				this.owner.Actor.StopMoveToPoint();
				this.owner.GetSkillManager().ServerBeginAssault(new Vector3(data.toPos.x * 0.01f, this.owner.Actor.FixTransform.get_position().y, data.toPos.y * 0.01f), data.curAniPri);
			}
			else
			{
				this.owner.Pos = PosDirUtility.ToTerrainPoint(data.toPos, this.owner.CurFloorStandardHeight);
			}
		}

		public override void EndAssault(BattleAction_EndAssault data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			Vector3 vector = PosDirUtility.ToTerrainPoint(data.pos, (!this.owner.Actor) ? this.owner.CurFloorStandardHeight : this.owner.Actor.FixTransform.get_position().y);
			Vector3 vector2 = new Vector3(data.vector.x, 0f, data.vector.y);
			this.owner.IsAssault = false;
			if (this.owner.Actor)
			{
				this.owner.GetSkillManager().ServerEndAssault(vector, vector2);
			}
			else
			{
				this.owner.Pos = vector;
				this.owner.Dir = vector2;
			}
		}

		public override void ChangeCamp(BattleAction_ChangeCamp data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.Camp = data.camp;
			if (this.owner.Actor)
			{
				if (this.owner.IsPlayerMate)
				{
					EventDispatcher.Broadcast<Transform>(CameraEvent.PlayerDie, this.owner.Actor.FixTransform);
				}
				else
				{
					EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerBorn, this.owner.TypeID, this.owner.Actor.FixTransform);
				}
			}
		}

		protected void Fuse(long petID, int fuseModelId, List<BattleSkillInfo> skillInfo)
		{
			if (!this.owner.IsEntityPlayerType)
			{
				return;
			}
			(this.owner as EntityPlayer).FusePetID = ((PetManager.Instance.GetPetInfo(petID) != null) ? PetManager.Instance.GetPetInfo(petID).petId : 0);
			this.owner.IsFuse = true;
			(this.owner as EntityPlayer).ModelIDBackUp = this.owner.FixModelID;
			(this.owner as EntityPlayer).BackUpSkill();
			this.owner.ModelID = fuseModelId;
			this.owner.ClearSkill();
			for (int i = 0; i < skillInfo.get_Count(); i++)
			{
				this.owner.AddSkill(skillInfo.get_Item(i).skillIdx, skillInfo.get_Item(i).skillId, skillInfo.get_Item(i).skillLv, skillInfo.get_Item(i).attrAdd);
			}
		}

		protected void DeFuse(int defuseModelID, List<BattleSkillInfo> skillInfo)
		{
			if (!this.owner.IsEntityPlayerType)
			{
				return;
			}
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
