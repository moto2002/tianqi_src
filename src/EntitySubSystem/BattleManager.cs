using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntitySubSystem
{
	public class BattleManager : IBattleManager, ISubSystem
	{
		protected EntityParent owner;

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.AddListeners();
		}

		public void OnDestroy()
		{
			this.RemoveListeners();
			this.owner = null;
		}

		protected virtual void AddListeners()
		{
			EventDispatcher.AddListener<BattleAction_UseSkill, bool>(BattleActionEvent.UseSkill, new Callback<BattleAction_UseSkill, bool>(this.UseSkill));
			EventDispatcher.AddListener<BattleAction_AttrChanged, bool>(BattleActionEvent.AttrChanged, new Callback<BattleAction_AttrChanged, bool>(this.AttrChanged));
			EventDispatcher.AddListener<BattleAction_Bleed, bool>(BattleActionEvent.Bleed, new Callback<BattleAction_Bleed, bool>(this.Bleed));
			EventDispatcher.AddListener<BattleAction_Treat, bool>(BattleActionEvent.Treat, new Callback<BattleAction_Treat, bool>(this.Treat));
			EventDispatcher.AddListener<BattleAction_UpdateEffect, bool>(BattleActionEvent.UpdateEffect, new Callback<BattleAction_UpdateEffect, bool>(this.UpdateEffect));
			EventDispatcher.AddListener<BattleAction_RemoveEffect, bool>(BattleActionEvent.RemoveEffect, new Callback<BattleAction_RemoveEffect, bool>(this.RemoveEffect));
			EventDispatcher.AddListener<BattleAction_Relive, bool>(BattleActionEvent.Relive, new Callback<BattleAction_Relive, bool>(this.Relive));
			EventDispatcher.AddListener<BattleAction_PetEnterField, bool>(BattleActionEvent.PetEnterBattleField, new Callback<BattleAction_PetEnterField, bool>(this.PetEnterBattleField));
			EventDispatcher.AddListener<BattleAction_PetLeaveField, bool>(BattleActionEvent.PetLeaveBattleField, new Callback<BattleAction_PetLeaveField, bool>(this.PetLeaveBattleField));
			EventDispatcher.AddListener<BattleAction_Fit, bool>(BattleActionEvent.Fit, new Callback<BattleAction_Fit, bool>(this.Fit));
			EventDispatcher.AddListener<BattleAction_ExitFit, bool>(BattleActionEvent.ExitFit, new Callback<BattleAction_ExitFit, bool>(this.ExitFit));
			EventDispatcher.AddListener<BattleAction_AddBuff, bool>(BattleActionEvent.AddBuff, new Callback<BattleAction_AddBuff, bool>(this.AddBuff));
			EventDispatcher.AddListener<BattleAction_UpdateBuff, bool>(BattleActionEvent.UpdateBuff, new Callback<BattleAction_UpdateBuff, bool>(this.UpdateBuff));
			EventDispatcher.AddListener<BattleAction_RemoveBuff, bool>(BattleActionEvent.RemoveBuff, new Callback<BattleAction_RemoveBuff, bool>(this.RemoveBuff));
			EventDispatcher.AddListener<BattleAction_SuckBlood, bool>(BattleActionEvent.SuckBlood, new Callback<BattleAction_SuckBlood, bool>(this.SuckBlood));
			EventDispatcher.AddListener<BattleAction_LegalizeHp, bool>(BattleActionEvent.LegalizeHp, new Callback<BattleAction_LegalizeHp, bool>(this.LegalizeHp));
			EventDispatcher.AddListener<BattleAction_AddSkill, bool>(BattleActionEvent.AddSkill, new Callback<BattleAction_AddSkill, bool>(this.AddSkill));
			EventDispatcher.AddListener<BattleAction_RemoveSkill, bool>(BattleActionEvent.RemoveSkill, new Callback<BattleAction_RemoveSkill, bool>(this.RemoveSkill));
			EventDispatcher.AddListener<BattleAction_Teleport, bool>(BattleActionEvent.Teleport, new Callback<BattleAction_Teleport, bool>(this.Teleport));
			EventDispatcher.AddListener<BattleAction_Fix, bool>(BattleActionEvent.Fix, new Callback<BattleAction_Fix, bool>(this.Fixed));
			EventDispatcher.AddListener<BattleAction_EndFix, bool>(BattleActionEvent.EndFix, new Callback<BattleAction_EndFix, bool>(this.EndFixed));
			EventDispatcher.AddListener<BattleAction_Static, bool>(BattleActionEvent.Static, new Callback<BattleAction_Static, bool>(this.Static));
			EventDispatcher.AddListener<BattleAction_EndStatic, bool>(BattleActionEvent.EndStatic, new Callback<BattleAction_EndStatic, bool>(this.EndStatic));
			EventDispatcher.AddListener<BattleAction_Taunt, bool>(BattleActionEvent.Taunt, new Callback<BattleAction_Taunt, bool>(this.Taunt));
			EventDispatcher.AddListener<BattleAction_EndTaunt, bool>(BattleActionEvent.EndTaunt, new Callback<BattleAction_EndTaunt, bool>(this.EndTaunt));
			EventDispatcher.AddListener<BattleAction_SuperArmor, bool>(BattleActionEvent.SuperArmor, new Callback<BattleAction_SuperArmor, bool>(this.SuperArmor));
			EventDispatcher.AddListener<BattleAction_EndSuperArmor, bool>(BattleActionEvent.EndSuperArmor, new Callback<BattleAction_EndSuperArmor, bool>(this.EndSuperArmor));
			EventDispatcher.AddListener<BattleAction_IgnoreDmgFormula, bool>(BattleActionEvent.IgnoreFormula, new Callback<BattleAction_IgnoreDmgFormula, bool>(this.IgnoreDmgFormula));
			EventDispatcher.AddListener<BattleAction_EndIgnoreDmgFormula, bool>(BattleActionEvent.EndIgnoreFormula, new Callback<BattleAction_EndIgnoreDmgFormula, bool>(this.EndIgnoreDmgFormula));
			EventDispatcher.AddListener<BattleAction_CloseRenderer, bool>(BattleActionEvent.CloseRenderer, new Callback<BattleAction_CloseRenderer, bool>(this.CloseRenderer));
			EventDispatcher.AddListener<BattleAction_EndCloseRenderer, bool>(BattleActionEvent.EndCloseRenderer, new Callback<BattleAction_EndCloseRenderer, bool>(this.EndCloseRenderer));
			EventDispatcher.AddListener<BattleAction_Stun, bool>(BattleActionEvent.Dizzy, new Callback<BattleAction_Stun, bool>(this.Dizzy));
			EventDispatcher.AddListener<BattleAction_EndStun, bool>(BattleActionEvent.EndDizzy, new Callback<BattleAction_EndStun, bool>(this.EndDizzy));
			EventDispatcher.AddListener<BattleAction_MoveCast, bool>(BattleActionEvent.MoveCast, new Callback<BattleAction_MoveCast, bool>(this.MoveCast));
			EventDispatcher.AddListener<BattleAction_EndMoveCast, bool>(BattleActionEvent.EndMoveCast, new Callback<BattleAction_EndMoveCast, bool>(this.EndMoveCast));
			EventDispatcher.AddListener<BattleAction_EndFitAction, bool>(BattleActionEvent.EndFitAction, new Callback<BattleAction_EndFitAction, bool>(this.EndFitAction));
			EventDispatcher.AddListener<BattleAction_Assault, bool>(BattleActionEvent.Assault, new Callback<BattleAction_Assault, bool>(this.Assault));
			EventDispatcher.AddListener<BattleAction_EndAssault, bool>(BattleActionEvent.EndAssault, new Callback<BattleAction_EndAssault, bool>(this.EndAssault));
			EventDispatcher.AddListener<BattleAction_EndKnock, bool>(BattleActionEvent.EndKnock, new Callback<BattleAction_EndKnock, bool>(this.EndKnock));
			EventDispatcher.AddListener<BattleAction_EndSkillManage, bool>(BattleActionEvent.EndSkillManage, new Callback<BattleAction_EndSkillManage, bool>(this.EndSkillManage));
			EventDispatcher.AddListener<BattleAction_EndLoading, bool>(BattleActionEvent.EndLoading, new Callback<BattleAction_EndLoading, bool>(this.EndLoading));
			EventDispatcher.AddListener<BattleAction_EndSkillPress, bool>(BattleActionEvent.EndSkillPress, new Callback<BattleAction_EndSkillPress, bool>(this.EndSkillPress));
			EventDispatcher.AddListener<BattleAction_MakeDead, bool>(BattleActionEvent.MakeDead, new Callback<BattleAction_MakeDead, bool>(this.MakeDead));
			EventDispatcher.AddListener<BattleAction_AtkProof, bool>(BattleActionEvent.AtkProof, new Callback<BattleAction_AtkProof, bool>(this.AtkProof));
			EventDispatcher.AddListener<BattleAction_EndAtkProof, bool>(BattleActionEvent.EndAtkProof, new Callback<BattleAction_EndAtkProof, bool>(this.EndAtkProof));
			EventDispatcher.AddListener<BattleAction_Weak, bool>(BattleActionEvent.Weak, new Callback<BattleAction_Weak, bool>(this.Weak));
			EventDispatcher.AddListener<BattleAction_EndWeak, bool>(BattleActionEvent.EndWeak, new Callback<BattleAction_EndWeak, bool>(this.EndWeak));
			EventDispatcher.AddListener<BattleAction_ChangeCamp, bool>(BattleActionEvent.ChangeCamp, new Callback<BattleAction_ChangeCamp, bool>(this.ChangeCamp));
			EventDispatcher.AddListener<BattleAction_Incurable, bool>(BattleActionEvent.Incurable, new Callback<BattleAction_Incurable, bool>(this.Incurable));
			EventDispatcher.AddListener<BattleAction_EndIncurable, bool>(BattleActionEvent.EndIncurable, new Callback<BattleAction_EndIncurable, bool>(this.EndIncurable));
		}

		protected virtual void RemoveListeners()
		{
			EventDispatcher.RemoveListener<BattleAction_UseSkill, bool>(BattleActionEvent.UseSkill, new Callback<BattleAction_UseSkill, bool>(this.UseSkill));
			EventDispatcher.RemoveListener<BattleAction_AttrChanged, bool>(BattleActionEvent.AttrChanged, new Callback<BattleAction_AttrChanged, bool>(this.AttrChanged));
			EventDispatcher.RemoveListener<BattleAction_Bleed, bool>(BattleActionEvent.Bleed, new Callback<BattleAction_Bleed, bool>(this.Bleed));
			EventDispatcher.RemoveListener<BattleAction_Treat, bool>(BattleActionEvent.Treat, new Callback<BattleAction_Treat, bool>(this.Treat));
			EventDispatcher.RemoveListener<BattleAction_UpdateEffect, bool>(BattleActionEvent.UpdateEffect, new Callback<BattleAction_UpdateEffect, bool>(this.UpdateEffect));
			EventDispatcher.RemoveListener<BattleAction_RemoveEffect, bool>(BattleActionEvent.RemoveEffect, new Callback<BattleAction_RemoveEffect, bool>(this.RemoveEffect));
			EventDispatcher.RemoveListener<BattleAction_Relive, bool>(BattleActionEvent.Relive, new Callback<BattleAction_Relive, bool>(this.Relive));
			EventDispatcher.RemoveListener<BattleAction_PetEnterField, bool>(BattleActionEvent.PetEnterBattleField, new Callback<BattleAction_PetEnterField, bool>(this.PetEnterBattleField));
			EventDispatcher.RemoveListener<BattleAction_PetLeaveField, bool>(BattleActionEvent.PetLeaveBattleField, new Callback<BattleAction_PetLeaveField, bool>(this.PetLeaveBattleField));
			EventDispatcher.RemoveListener<BattleAction_Fit, bool>(BattleActionEvent.Fit, new Callback<BattleAction_Fit, bool>(this.Fit));
			EventDispatcher.RemoveListener<BattleAction_ExitFit, bool>(BattleActionEvent.ExitFit, new Callback<BattleAction_ExitFit, bool>(this.ExitFit));
			EventDispatcher.RemoveListener<BattleAction_AddBuff, bool>(BattleActionEvent.AddBuff, new Callback<BattleAction_AddBuff, bool>(this.AddBuff));
			EventDispatcher.RemoveListener<BattleAction_UpdateBuff, bool>(BattleActionEvent.UpdateBuff, new Callback<BattleAction_UpdateBuff, bool>(this.UpdateBuff));
			EventDispatcher.RemoveListener<BattleAction_RemoveBuff, bool>(BattleActionEvent.RemoveBuff, new Callback<BattleAction_RemoveBuff, bool>(this.RemoveBuff));
			EventDispatcher.RemoveListener<BattleAction_SuckBlood, bool>(BattleActionEvent.SuckBlood, new Callback<BattleAction_SuckBlood, bool>(this.SuckBlood));
			EventDispatcher.RemoveListener<BattleAction_LegalizeHp, bool>(BattleActionEvent.LegalizeHp, new Callback<BattleAction_LegalizeHp, bool>(this.LegalizeHp));
			EventDispatcher.RemoveListener<BattleAction_AddSkill, bool>(BattleActionEvent.AddSkill, new Callback<BattleAction_AddSkill, bool>(this.AddSkill));
			EventDispatcher.RemoveListener<BattleAction_RemoveSkill, bool>(BattleActionEvent.RemoveSkill, new Callback<BattleAction_RemoveSkill, bool>(this.RemoveSkill));
			EventDispatcher.RemoveListener<BattleAction_Teleport, bool>(BattleActionEvent.Teleport, new Callback<BattleAction_Teleport, bool>(this.Teleport));
			EventDispatcher.RemoveListener<BattleAction_Fix, bool>(BattleActionEvent.Fix, new Callback<BattleAction_Fix, bool>(this.Fixed));
			EventDispatcher.RemoveListener<BattleAction_EndFix, bool>(BattleActionEvent.EndFix, new Callback<BattleAction_EndFix, bool>(this.EndFixed));
			EventDispatcher.RemoveListener<BattleAction_Static, bool>(BattleActionEvent.Static, new Callback<BattleAction_Static, bool>(this.Static));
			EventDispatcher.RemoveListener<BattleAction_EndStatic, bool>(BattleActionEvent.EndStatic, new Callback<BattleAction_EndStatic, bool>(this.EndStatic));
			EventDispatcher.RemoveListener<BattleAction_Taunt, bool>(BattleActionEvent.Taunt, new Callback<BattleAction_Taunt, bool>(this.Taunt));
			EventDispatcher.RemoveListener<BattleAction_EndTaunt, bool>(BattleActionEvent.EndTaunt, new Callback<BattleAction_EndTaunt, bool>(this.EndTaunt));
			EventDispatcher.RemoveListener<BattleAction_SuperArmor, bool>(BattleActionEvent.SuperArmor, new Callback<BattleAction_SuperArmor, bool>(this.SuperArmor));
			EventDispatcher.RemoveListener<BattleAction_EndSuperArmor, bool>(BattleActionEvent.EndSuperArmor, new Callback<BattleAction_EndSuperArmor, bool>(this.EndSuperArmor));
			EventDispatcher.RemoveListener<BattleAction_IgnoreDmgFormula, bool>(BattleActionEvent.IgnoreFormula, new Callback<BattleAction_IgnoreDmgFormula, bool>(this.IgnoreDmgFormula));
			EventDispatcher.RemoveListener<BattleAction_EndIgnoreDmgFormula, bool>(BattleActionEvent.EndIgnoreFormula, new Callback<BattleAction_EndIgnoreDmgFormula, bool>(this.EndIgnoreDmgFormula));
			EventDispatcher.RemoveListener<BattleAction_CloseRenderer, bool>(BattleActionEvent.CloseRenderer, new Callback<BattleAction_CloseRenderer, bool>(this.CloseRenderer));
			EventDispatcher.RemoveListener<BattleAction_EndCloseRenderer, bool>(BattleActionEvent.EndCloseRenderer, new Callback<BattleAction_EndCloseRenderer, bool>(this.EndCloseRenderer));
			EventDispatcher.RemoveListener<BattleAction_Stun, bool>(BattleActionEvent.Dizzy, new Callback<BattleAction_Stun, bool>(this.Dizzy));
			EventDispatcher.RemoveListener<BattleAction_EndStun, bool>(BattleActionEvent.EndDizzy, new Callback<BattleAction_EndStun, bool>(this.EndDizzy));
			EventDispatcher.RemoveListener<BattleAction_MoveCast, bool>(BattleActionEvent.MoveCast, new Callback<BattleAction_MoveCast, bool>(this.MoveCast));
			EventDispatcher.RemoveListener<BattleAction_EndMoveCast, bool>(BattleActionEvent.EndMoveCast, new Callback<BattleAction_EndMoveCast, bool>(this.EndMoveCast));
			EventDispatcher.RemoveListener<BattleAction_EndFitAction, bool>(BattleActionEvent.EndFitAction, new Callback<BattleAction_EndFitAction, bool>(this.EndFitAction));
			EventDispatcher.RemoveListener<BattleAction_Assault, bool>(BattleActionEvent.Assault, new Callback<BattleAction_Assault, bool>(this.Assault));
			EventDispatcher.RemoveListener<BattleAction_EndAssault, bool>(BattleActionEvent.EndAssault, new Callback<BattleAction_EndAssault, bool>(this.EndAssault));
			EventDispatcher.RemoveListener<BattleAction_EndKnock, bool>(BattleActionEvent.EndKnock, new Callback<BattleAction_EndKnock, bool>(this.EndKnock));
			EventDispatcher.RemoveListener<BattleAction_EndSkillManage, bool>(BattleActionEvent.EndSkillManage, new Callback<BattleAction_EndSkillManage, bool>(this.EndSkillManage));
			EventDispatcher.RemoveListener<BattleAction_EndLoading, bool>(BattleActionEvent.EndLoading, new Callback<BattleAction_EndLoading, bool>(this.EndLoading));
			EventDispatcher.RemoveListener<BattleAction_EndSkillPress, bool>(BattleActionEvent.EndSkillPress, new Callback<BattleAction_EndSkillPress, bool>(this.EndSkillPress));
			EventDispatcher.RemoveListener<BattleAction_MakeDead, bool>(BattleActionEvent.MakeDead, new Callback<BattleAction_MakeDead, bool>(this.MakeDead));
			EventDispatcher.RemoveListener<BattleAction_AtkProof, bool>(BattleActionEvent.AtkProof, new Callback<BattleAction_AtkProof, bool>(this.AtkProof));
			EventDispatcher.RemoveListener<BattleAction_EndAtkProof, bool>(BattleActionEvent.EndAtkProof, new Callback<BattleAction_EndAtkProof, bool>(this.EndAtkProof));
			EventDispatcher.RemoveListener<BattleAction_Weak, bool>(BattleActionEvent.Weak, new Callback<BattleAction_Weak, bool>(this.Weak));
			EventDispatcher.RemoveListener<BattleAction_EndWeak, bool>(BattleActionEvent.EndWeak, new Callback<BattleAction_EndWeak, bool>(this.EndWeak));
			EventDispatcher.RemoveListener<BattleAction_ChangeCamp, bool>(BattleActionEvent.ChangeCamp, new Callback<BattleAction_ChangeCamp, bool>(this.ChangeCamp));
			EventDispatcher.RemoveListener<BattleAction_Incurable, bool>(BattleActionEvent.Incurable, new Callback<BattleAction_Incurable, bool>(this.Incurable));
			EventDispatcher.RemoveListener<BattleAction_EndIncurable, bool>(BattleActionEvent.EndIncurable, new Callback<BattleAction_EndIncurable, bool>(this.EndIncurable));
		}

		public void UseSkill(BattleAction_UseSkill data, bool isServerData)
		{
			if (this.owner.Actor == null)
			{
				return;
			}
			if (data.casterId != this.owner.ID)
			{
				return;
			}
			Debug.Log(string.Concat(new object[]
			{
				"下发 ServerCastSkillByID: ",
				data.skillId,
				" ",
				data.needManage,
				" ",
				data.mgrSn,
				" (",
				data.casterVector.x,
				", ",
				data.casterVector.y,
				")"
			}));
			Skill skill = DataReader<Skill>.Get(data.skillId);
			if (skill == null)
			{
				return;
			}
			if (skill.type3 == 1)
			{
				this.owner.AITargetID = data.targetId;
				this.owner.AITarget = ((!EntityWorld.Instance.AllEntities.ContainsKey(data.targetId)) ? null : EntityWorld.Instance.AllEntities[data.targetId]);
			}
			this.owner.CheckCancelManage(data.targetId, data.oldManageState, false);
			if (data.needManage && data.isManaged)
			{
				this.owner.IsSkillInTrustee = true;
			}
			if (isServerData)
			{
				this.owner.GetSkillManager().ServerCastSkillByID(data.skillId, data.curAniPri, new Vector3(data.casterVector.x, 0f, data.casterVector.y), data.needManage, data.mgrSn);
			}
			else
			{
				this.owner.GetSkillManager().ClientCastSkillByID(data.skillId);
			}
		}

		public void AttrChanged(BattleAction_AttrChanged data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			if (this.owner.BattleBaseAttrs == null)
			{
				return;
			}
			for (int i = 0; i < data.attrs.get_Count(); i++)
			{
				this.owner.BattleBaseAttrs.SetValue((GameData.AttrType)data.attrs.get_Item(i).attrType, data.attrs.get_Item(i).attrValue, true);
			}
		}

		public virtual void Bleed(BattleAction_Bleed data, bool isServerData)
		{
			if (data.bleedSoldierId != this.owner.ID)
			{
				return;
			}
			if (EntityWorld.Instance.AllEntities.ContainsKey(data.dmgSrcSoldierId))
			{
				this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, EntityWorld.Instance.AllEntities[data.dmgSrcSoldierId], data.isCrt, data.isParry, data.isMiss));
			}
			else
			{
				this.owner.SetHPChange(HPChangeMessage.GetDamageMessage(data.bleedHp, data.dmgSrcType, data.elemType, this.owner, null, data.isCrt, data.isParry, data.isMiss));
			}
			this.owner.DamageSourceID = data.dmgSrcSoldierId;
			this.owner.Hp = data.hp;
		}

		public void Treat(BattleAction_Treat data, bool isServerData)
		{
			if (data.beTreatedSoldierId != this.owner.ID)
			{
				return;
			}
			Vector3 casterPosition = Vector3.get_zero();
			if (EntityWorld.Instance.AllEntities.ContainsKey(data.treatSrcSoldierId))
			{
				bool flag;
				if (EntityWorld.Instance.AllEntities[data.treatSrcSoldierId].Actor)
				{
					flag = true;
					casterPosition = EntityWorld.Instance.AllEntities[data.treatSrcSoldierId].Actor.FixTransform.get_position();
				}
				else
				{
					flag = (data.pos != null);
					if (flag)
					{
						casterPosition = XUtility.GetTerrainPoint(data.pos.x * 0.01f, data.pos.y * 0.01f, 0f);
					}
				}
				this.owner.SetHPChange(HPChangeMessage.GetTreatMessage(data.treatHp, data.treatSrcType, this.owner, EntityWorld.Instance.AllEntities[data.treatSrcSoldierId], flag, casterPosition));
			}
			else
			{
				bool flag = data.pos != null;
				if (flag)
				{
					casterPosition = XUtility.GetTerrainPoint(data.pos.x * 0.01f, data.pos.y * 0.01f, 0f);
				}
				this.owner.SetHPChange(HPChangeMessage.GetTreatMessage(data.treatHp, data.treatSrcType, this.owner, null, flag, casterPosition));
			}
			this.owner.Hp = data.hp;
		}

		public void UpdateEffect(BattleAction_UpdateEffect data, bool isServerData)
		{
			if (data.casterId == this.owner.ID && isServerData && this.owner.Actor)
			{
				this.DrawServerRange(data);
			}
			Debuger.Error(string.Concat(new object[]
			{
				"effectId:",
				data.effectId,
				" uniqueId: ",
				data.uniqueId,
				" casterId: ",
				data.casterId,
				" \nneedManageTargets: ",
				data.needManageTargets.get_Count(),
				" data.targets.Count: ",
				data.targets.get_Count(),
				" pos:(",
				data.pos.x,
				",",
				data.pos.y,
				") vector:(",
				data.vector.x,
				",",
				data.vector.y
			}), new object[0]);
			List<long> list = new List<long>();
			for (int i = 0; i < data.needManageTargets.get_Count(); i++)
			{
				if (data.needManageTargets.get_Item(i).managerId == EntityWorld.Instance.EntSelf.ID)
				{
					Debuger.Error(string.Concat(new object[]
					{
						"===============11111111111111111111111111==============: ",
						data.effectId,
						" ",
						data.needManageTargets.get_Item(i).managerId == this.owner.ID
					}), new object[0]);
					list.AddRange(data.needManageTargets.get_Item(i).managedIds);
					break;
				}
				Debuger.Error("===============00000000000000000000000000==============: " + data.effectId, new object[0]);
			}
			Debuger.Error("=============================: " + this.owner.GetType(), new object[0]);
			for (int j = 0; j < data.needManageTargets.get_Count(); j++)
			{
				Debuger.Error("manager: " + data.needManageTargets.get_Item(j).managerId, new object[0]);
			}
			for (int k = 0; k < list.get_Count(); k++)
			{
				Debuger.Error("clientManageEntityIDs: " + list.get_Item(k), new object[0]);
			}
			for (int l = 0; l < data.targets.get_Count(); l++)
			{
				Debuger.Error(string.Concat(new object[]
				{
					"AllTargets: ",
					data.targets.get_Item(l).targetId,
					" ",
					data.targets.get_Item(l).hitAction
				}), new object[0]);
			}
			for (int m = 0; m < data.targets.get_Count(); m++)
			{
				if (data.targets.get_Item(m).targetId == this.owner.ID)
				{
					if (data.targets.get_Item(m).isParry)
					{
						Debug.Log("isParry!!!");
						if (this.owner.Actor)
						{
							this.owner.Actor.PlayParryFx();
						}
					}
					else if (this.owner.Actor)
					{
						Debuger.Error(string.Concat(new object[]
						{
							"Handle Hit: ",
							data.targets.get_Item(m).knocked,
							" ",
							new Vector3(data.targets.get_Item(m).toPos.x * 0.01f, this.owner.Actor.FixTransform.get_position().y, data.targets.get_Item(m).toPos.y * 0.01f),
							" ",
							data.targets.get_Item(m).mgrSn
						}), new object[0]);
						this.owner.GetSkillManager().ServerHandleHit(data.casterId, data.effectId, data.targets.get_Item(m).hitAction, data.targets.get_Item(m).curAniPri, data.targets.get_Item(m).knocked, new Vector3(data.targets.get_Item(m).toPos.x * 0.01f, this.owner.Actor.FixTransform.get_position().y, data.targets.get_Item(m).toPos.y * 0.01f), list.Contains(data.targets.get_Item(m).targetId), data.targets.get_Item(m).oldManageState, data.targets.get_Item(m).mgrSn);
					}
					else
					{
						EntityParent arg_5F5_0 = this.owner;
						Vector3 vector = new Vector3(data.targets.get_Item(m).toPos.x * 0.01f - this.owner.Pos.x, 0f, data.targets.get_Item(m).toPos.y * 0.01f - this.owner.Pos.z);
						arg_5F5_0.Dir = vector.get_normalized();
						this.owner.Pos = PosDirUtility.ToTerrainPoint(data.targets.get_Item(m).toPos, this.owner.CurFloorStandardHeight);
						if (list.Contains(data.targets.get_Item(m).targetId))
						{
							GlobalBattleNetwork.Instance.SendEndKnock(data.targets.get_Item(m).targetId, this.owner.Pos, this.owner.Dir, data.targets.get_Item(m).mgrSn);
						}
					}
					break;
				}
			}
			this.CheckHitException(data, list);
		}

		public void RemoveEffect(BattleAction_RemoveEffect data, bool isServerData)
		{
			if (data.casterId == this.owner.ID)
			{
			}
			if (data.targetIds.Contains(this.owner.ID))
			{
			}
		}

		public virtual void Relive(BattleAction_Relive data, bool isServerData)
		{
			if (data.soldierInfo.id != this.owner.ID)
			{
				return;
			}
			data.soldierInfo.pos = null;
			this.owner.SetDataByMapObjInfoOnRelive(data.soldierInfo, !isServerData);
		}

		public virtual void PetEnterBattleField(BattleAction_PetEnterField data, bool isServerData)
		{
		}

		public virtual void PetLeaveBattleField(BattleAction_PetLeaveField data, bool isServerData)
		{
		}

		public virtual void Fit(BattleAction_Fit data, bool isServerData)
		{
		}

		public virtual void ExitFit(BattleAction_ExitFit data, bool isServerData)
		{
		}

		public void AddBuff(BattleAction_AddBuff data, bool isServerData)
		{
			if (data.targetId != this.owner.ID)
			{
				return;
			}
			this.owner.GetBuffManager().AddBuff(data.buffId, data.dueTime);
		}

		public void UpdateBuff(BattleAction_UpdateBuff data, bool isServerData)
		{
			if (data.targetId != this.owner.ID)
			{
				return;
			}
		}

		public void RemoveBuff(BattleAction_RemoveBuff data, bool isServerData)
		{
			if (data.targetId != this.owner.ID)
			{
				return;
			}
			this.owner.GetBuffManager().RemoveBuff(data.buffId);
		}

		public void SuckBlood(BattleAction_SuckBlood data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
		}

		public void LegalizeHp(BattleAction_LegalizeHp data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.HpLmt = data.hpLmt;
			this.owner.HpLmtMulAmend = data.hpLmtMulAmend;
			this.owner.Hp = data.hp;
		}

		public void AddSkill(BattleAction_AddSkill data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.AddSkill(data.skillInfo.skillIdx, data.skillInfo.skillId, data.skillInfo.skillLv, data.skillInfo.attrAdd);
		}

		public void RemoveSkill(BattleAction_RemoveSkill data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.RemoveSkill(data.skillId);
		}

		public void Teleport(BattleAction_Teleport data, bool isServerData)
		{
			if (data.objId != this.owner.ID)
			{
				return;
			}
			Vector3 vector = PosDirUtility.ToTerrainPoint(data.toPos, (!this.owner.Actor) ? this.owner.CurFloorStandardHeight : this.owner.Actor.FixTransform.get_position().y);
			if (this.owner.Actor)
			{
				this.owner.Actor.FixTransform.set_position(vector);
			}
			else
			{
				this.owner.Pos = vector;
			}
		}

		public void Fixed(BattleAction_Fix data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsFixed = true;
		}

		public void EndFixed(BattleAction_EndFix data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsFixed = false;
		}

		public void Static(BattleAction_Static data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsStatic = true;
		}

		public void EndStatic(BattleAction_EndStatic data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsStatic = false;
		}

		public void Taunt(BattleAction_Taunt data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsTaunt = true;
		}

		public void EndTaunt(BattleAction_EndTaunt data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsTaunt = false;
		}

		public void SuperArmor(BattleAction_SuperArmor data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsEndure = true;
		}

		public void EndSuperArmor(BattleAction_EndSuperArmor data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsEndure = false;
		}

		public void IgnoreDmgFormula(BattleAction_IgnoreDmgFormula data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsIgnoreFormula = true;
		}

		public void EndIgnoreDmgFormula(BattleAction_EndIgnoreDmgFormula data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsIgnoreFormula = false;
		}

		public void CloseRenderer(BattleAction_CloseRenderer data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsCloseRenderer = true;
		}

		public void EndCloseRenderer(BattleAction_EndCloseRenderer data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsCloseRenderer = false;
		}

		public void Dizzy(BattleAction_Stun data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsDizzy = true;
		}

		public void EndDizzy(BattleAction_EndStun data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsDizzy = false;
		}

		public void MoveCast(BattleAction_MoveCast data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
		}

		public void EndMoveCast(BattleAction_EndMoveCast data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
		}

		public void EndFitAction(BattleAction_EndFitAction data, bool isServerData)
		{
			if (data.roleId != this.owner.ID)
			{
				return;
			}
			this.owner.IsFusing = false;
		}

		public virtual void Assault(BattleAction_Assault data, bool isServerData)
		{
		}

		public virtual void EndAssault(BattleAction_EndAssault data, bool isServerData)
		{
		}

		public virtual void EndKnock(BattleAction_EndKnock data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			Vector3 vector = PosDirUtility.ToTerrainPoint(data.pos, (!this.owner.Actor) ? this.owner.CurFloorStandardHeight : this.owner.Actor.FixTransform.get_position().y);
			Vector3 vector2 = new Vector3(data.vector.x, 0f, data.vector.y);
			this.owner.IsHitMoving = false;
			if (this.owner.Actor)
			{
				this.owner.Actor.StopMoveToPoint();
				Debuger.Error(string.Concat(new object[]
				{
					this.owner.ID,
					" EndKnock瞬移：",
					data.reason,
					" ",
					data.mgrSn,
					"\ntoPos：",
					vector,
					" before: ",
					this.owner.Actor.FixTransform.get_position(),
					"\ntoDir：",
					vector2.get_normalized(),
					" before: ",
					this.owner.Actor.FixTransform.get_forward().get_normalized()
				}), new object[0]);
				this.owner.Actor.FixTransform.set_forward(vector2);
				if (this.owner.Actor.EndHitMoveDeviationEstimated(vector))
				{
					this.owner.Actor.SetAndFixPosition(vector, "EndKnock瞬移", 1000007);
				}
				else
				{
					this.owner.Actor.MoveToPoint(vector, 0f, null);
				}
			}
			else
			{
				this.owner.Pos = vector;
				this.owner.Dir = vector2;
			}
		}

		public virtual void EndSkillManage(BattleAction_EndSkillManage data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			Vector3 vector = PosDirUtility.ToTerrainPoint(data.pos, (!this.owner.Actor) ? this.owner.CurFloorStandardHeight : this.owner.Actor.FixTransform.get_position().y);
			Vector3 vector2 = new Vector3(data.vector.x, 0f, data.vector.y);
			this.owner.IsSkillInTrustee = false;
			if (this.owner.Actor)
			{
				this.owner.Actor.StopMoveToPoint();
				this.owner.Actor.FixTransform.set_forward(vector2);
				if (this.owner.Actor.EndSkillManageDeviationEstimated(vector))
				{
					this.owner.Actor.SetAndFixPosition(vector, "EndSkillManage瞬移", 1000004);
				}
				else
				{
					this.owner.Actor.MoveToPoint(vector, 0f, null);
				}
			}
			else
			{
				this.owner.Pos = vector;
				this.owner.Dir = vector2;
			}
		}

		public virtual void EndLoading(BattleAction_EndLoading data, bool isServerData)
		{
			if (data.roleId != this.owner.ID)
			{
				return;
			}
			this.owner.IsLoading = false;
		}

		public virtual void EndSkillPress(BattleAction_EndSkillPress data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.GetSkillManager().ServerEndRepeatSkill(data.skillId);
		}

		public void MakeDead(BattleAction_MakeDead data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.Hp = 0L;
		}

		public void AtkProof(BattleAction_AtkProof data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsUnconspicuous = true;
		}

		public void EndAtkProof(BattleAction_EndAtkProof data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsUnconspicuous = false;
		}

		public void Weak(BattleAction_Weak data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsWeak = true;
		}

		public void EndWeak(BattleAction_EndWeak data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsWeak = false;
		}

		public virtual void ChangeCamp(BattleAction_ChangeCamp data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.Camp = data.camp;
		}

		public void Incurable(BattleAction_Incurable data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsIncurable = true;
		}

		public void EndIncurable(BattleAction_EndIncurable data, bool isServerData)
		{
			if (data.soldierId != this.owner.ID)
			{
				return;
			}
			this.owner.IsIncurable = false;
		}

		protected void DrawServerRange(BattleAction_UpdateEffect data)
		{
			if (this.owner.Actor.FixTransform == null)
			{
				return;
			}
			Effect effect = DataReader<Effect>.Get(data.effectId);
			GameObject gameObject = GameObject.Find("ServerEffectPainter");
			if (gameObject == null)
			{
				gameObject = new GameObject();
				gameObject.set_name("ServerEffectPainter");
			}
			gameObject.get_transform().set_position(new Vector3(data.pos.x * 0.01f, this.owner.Actor.FixTransform.get_position().y, data.pos.y * 0.01f));
			gameObject.get_transform().set_forward(new Vector3(data.vector.x, 0f, data.vector.y));
			XPoint xPoint = new XPoint
			{
				position = gameObject.get_transform().get_position(),
				rotation = gameObject.get_transform().get_rotation()
			};
			XPoint theFixBasePoint = xPoint.ApplyOffset(effect.offset).ApplyOffset(effect.offset2).ApplyForwardFix(effect.forwardFixAngle);
			GraghMessage graghMessage = null;
			GraghMessage graghMessage2 = null;
			bool flag = true;
			if (effect.range.get_Count() > 2)
			{
				int num = Mathf.Abs(effect.range.get_Item(0));
				if (num != 1)
				{
					if (num == 2)
					{
						graghMessage = new GraghMessage(theFixBasePoint, GraghShape.Rect, 0f, 0f, (float)effect.range.get_Item(1) * 0.01f, (float)effect.range.get_Item(2) * 0.01f);
					}
				}
				else
				{
					graghMessage = new GraghMessage(theFixBasePoint, GraghShape.Sector, (float)effect.range.get_Item(1) * 0.01f, (float)effect.range.get_Item(2), 0f, 0f);
				}
			}
			if (effect.range2.get_Count() > 2)
			{
				if (effect.range2.get_Item(0) < 0)
				{
					flag = false;
				}
				int num = Mathf.Abs(effect.range2.get_Item(0));
				if (num != 1)
				{
					if (num == 2)
					{
						graghMessage2 = new GraghMessage(theFixBasePoint, GraghShape.Rect, 0f, 0f, (float)effect.range2.get_Item(1) * 0.01f, (float)effect.range2.get_Item(2) * 0.01f);
					}
				}
				else
				{
					graghMessage2 = new GraghMessage(theFixBasePoint, GraghShape.Sector, (float)effect.range2.get_Item(1) * 0.01f, (float)effect.range2.get_Item(2), 0f, 0f);
				}
			}
			if (graghMessage != null)
			{
				graghMessage.DrawShape(Color.get_black());
			}
			if (graghMessage2 != null)
			{
				if (flag)
				{
					graghMessage2.DrawShape(Color.get_black());
				}
				else
				{
					graghMessage2.DrawShape(Color.get_black());
				}
			}
		}

		protected void CheckHitException(BattleAction_UpdateEffect data, List<long> clientManageEntityIDs)
		{
			for (int i = 0; i < clientManageEntityIDs.get_Count(); i++)
			{
				bool flag = false;
				int num = 0;
				for (int j = 0; j < data.targets.get_Count(); j++)
				{
					if (data.targets.get_Item(j).targetId == clientManageEntityIDs.get_Item(i))
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag && num == data.targets.get_Count())
				{
					Debuger.Error("Can't find: " + clientManageEntityIDs.get_Item(i) + "======================================", new object[0]);
				}
			}
			Debuger.Error("======================================", new object[0]);
		}
	}
}
