using Package;
using System;
using System.Collections.Generic;

public class LocalBattleProtocolSimulator
{
	public static void SendUseSkill(long casterID, long targetID, int skillID)
	{
		BattleAction_UseSkill battleAction_UseSkill = new BattleAction_UseSkill();
		battleAction_UseSkill.casterId = casterID;
		battleAction_UseSkill.targetId = targetID;
		battleAction_UseSkill.skillId = skillID;
		EventDispatcher.BroadcastAsync<BattleAction_UseSkill, bool>(BattleActionEvent.UseSkill, battleAction_UseSkill, false);
	}

	public static void SendBleed(long targetID, GameObjectType.ENUM targetType, long sourceID, GameObjectType.ENUM sourceType, BattleAction_Bleed.DmgSrcType dmgType, ElemType.ENUM elementType, long damage, long hp, bool isCrt, bool isParry, bool isMiss)
	{
		BattleAction_Bleed battleAction_Bleed = new BattleAction_Bleed();
		battleAction_Bleed.bleedSoldierId = targetID;
		battleAction_Bleed.bleedSoldierWrapType = targetType;
		battleAction_Bleed.dmgSrcSoldierId = sourceID;
		battleAction_Bleed.dmgSrcWrapType = sourceType;
		battleAction_Bleed.dmgSrcType = dmgType;
		battleAction_Bleed.elemType = elementType;
		battleAction_Bleed.bleedHp = damage;
		battleAction_Bleed.hp = hp;
		battleAction_Bleed.isCrt = isCrt;
		battleAction_Bleed.isParry = isParry;
		battleAction_Bleed.isMiss = isMiss;
		EventDispatcher.Broadcast<BattleAction_Bleed, bool>(BattleActionEvent.Bleed, battleAction_Bleed, false);
	}

	public static void SendTreat(long targetID, GameObjectType.ENUM targetType, long sourceID, GameObjectType.ENUM sourceType, BattleAction_Treat.TreatSrcType treatType, long benefit, long hp, Pos position)
	{
		BattleAction_Treat battleAction_Treat = new BattleAction_Treat();
		battleAction_Treat.beTreatedSoldierId = targetID;
		battleAction_Treat.beTreatedWrapType = targetType;
		battleAction_Treat.treatSrcSoldierId = sourceID;
		battleAction_Treat.treatSrcWrapType = sourceType;
		battleAction_Treat.treatSrcType = treatType;
		battleAction_Treat.treatHp = benefit;
		battleAction_Treat.hp = hp;
		battleAction_Treat.pos = position;
		EventDispatcher.Broadcast<BattleAction_Treat, bool>(BattleActionEvent.Treat, battleAction_Treat, false);
	}

	public static void SendUpdateEffect(long casterID, List<EffectTargetInfo> targetInfos, int effectID, int uniqueID)
	{
		BattleAction_UpdateEffect battleAction_UpdateEffect = new BattleAction_UpdateEffect();
		battleAction_UpdateEffect.casterId = casterID;
		battleAction_UpdateEffect.targets.AddRange(targetInfos);
		battleAction_UpdateEffect.effectId = effectID;
		battleAction_UpdateEffect.uniqueId = (long)uniqueID;
		EventDispatcher.BroadcastAsync<BattleAction_UpdateEffect, bool>(BattleActionEvent.UpdateEffect, battleAction_UpdateEffect, false);
	}

	public static void SendRemoveEffect(long casterID, List<long> targetIDs, int effectID, int uniqueID)
	{
		BattleAction_RemoveEffect battleAction_RemoveEffect = new BattleAction_RemoveEffect();
		battleAction_RemoveEffect.casterId = casterID;
		battleAction_RemoveEffect.targetIds.AddRange(targetIDs);
		battleAction_RemoveEffect.effectId = effectID;
		battleAction_RemoveEffect.uniqueId = (long)uniqueID;
		EventDispatcher.BroadcastAsync<BattleAction_RemoveEffect, bool>(BattleActionEvent.RemoveEffect, battleAction_RemoveEffect, false);
	}

	public static void SendRelive(long targetID, int hp)
	{
		BattleAction_Relive arg = new BattleAction_Relive();
		EventDispatcher.BroadcastAsync<BattleAction_Relive, bool>(BattleActionEvent.Relive, arg, false);
	}

	public static void SendPetEnterBattleField(long petID, Pos pos, Vector2 vector, float existTime)
	{
		BattleAction_PetEnterField battleAction_PetEnterField = new BattleAction_PetEnterField();
		battleAction_PetEnterField.petId = petID;
		battleAction_PetEnterField.pos = pos;
		battleAction_PetEnterField.vector = vector;
		battleAction_PetEnterField.exitTime = (int)existTime;
		EventDispatcher.Broadcast<BattleAction_PetEnterField, bool>(BattleActionEvent.PetEnterBattleField, battleAction_PetEnterField, false);
	}

	public static void SendPetLeaveBattleField(long petID)
	{
		BattleAction_PetLeaveField battleAction_PetLeaveField = new BattleAction_PetLeaveField();
		battleAction_PetLeaveField.petId = petID;
		EventDispatcher.Broadcast<BattleAction_PetLeaveField, bool>(BattleActionEvent.PetLeaveBattleField, battleAction_PetLeaveField, false);
	}

	public static void SendFit(long roleID, long petID, int fitModelID, List<KeyValuePair<int, int>> skill, int durationTime)
	{
		BattleAction_Fit battleAction_Fit = new BattleAction_Fit();
		battleAction_Fit.roleId = roleID;
		battleAction_Fit.fitPetId = petID;
		battleAction_Fit.fitModelId = fitModelID;
		using (List<KeyValuePair<int, int>>.Enumerator enumerator = skill.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				battleAction_Fit.fitSkills.Add(new BattleSkillInfo
				{
					skillIdx = current.get_Key(),
					skillId = current.get_Value()
				});
			}
		}
		battleAction_Fit.durationTime = durationTime;
		EventDispatcher.Broadcast<BattleAction_Fit, bool>(BattleActionEvent.Fit, battleAction_Fit, false);
	}

	public static void SendExitFit(long roleID, long petID, int modelID, IndexList<int, int> skill)
	{
		BattleAction_ExitFit battleAction_ExitFit = new BattleAction_ExitFit();
		battleAction_ExitFit.roleId = roleID;
		battleAction_ExitFit.roleModelId = modelID;
		using (Dictionary<int, int>.Enumerator enumerator = skill.GetPairPart().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				battleAction_ExitFit.roleSkills.Add(new BattleSkillInfo
				{
					skillIdx = current.get_Key(),
					skillId = current.get_Value()
				});
			}
		}
		using (List<int>.Enumerator enumerator2 = skill.GetSinglePart().GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				int current2 = enumerator2.get_Current();
				battleAction_ExitFit.roleSkills.Add(new BattleSkillInfo
				{
					skillId = current2
				});
			}
		}
		battleAction_ExitFit.petId = petID;
		EventDispatcher.Broadcast<BattleAction_ExitFit, bool>(BattleActionEvent.ExitFit, battleAction_ExitFit, false);
	}

	public static void SendAddBuff(long casterID, long targetID, int buffID, int buffTime)
	{
		BattleAction_AddBuff battleAction_AddBuff = new BattleAction_AddBuff();
		battleAction_AddBuff.casterId = casterID;
		battleAction_AddBuff.targetId = targetID;
		battleAction_AddBuff.buffId = buffID;
		battleAction_AddBuff.dueTime = buffTime;
		EventDispatcher.Broadcast<BattleAction_AddBuff, bool>(BattleActionEvent.AddBuff, battleAction_AddBuff, false);
	}

	public static void SendUpdateBuff(long targetID, int buffID)
	{
		BattleAction_UpdateBuff battleAction_UpdateBuff = new BattleAction_UpdateBuff();
		battleAction_UpdateBuff.targetId = targetID;
		battleAction_UpdateBuff.buffId = buffID;
		EventDispatcher.Broadcast<BattleAction_UpdateBuff, bool>(BattleActionEvent.UpdateBuff, battleAction_UpdateBuff, false);
	}

	public static void SendRemoveBuff(long targetID, int buffID)
	{
		BattleAction_RemoveBuff battleAction_RemoveBuff = new BattleAction_RemoveBuff();
		battleAction_RemoveBuff.targetId = targetID;
		battleAction_RemoveBuff.buffId = buffID;
		EventDispatcher.Broadcast<BattleAction_RemoveBuff, bool>(BattleActionEvent.RemoveBuff, battleAction_RemoveBuff, false);
	}

	public static void SendSuckBlood(long casterID, int benefit, int hp)
	{
		BattleAction_SuckBlood battleAction_SuckBlood = new BattleAction_SuckBlood();
		battleAction_SuckBlood.soldierId = casterID;
		battleAction_SuckBlood.suckHp = (long)benefit;
		battleAction_SuckBlood.hp = (long)hp;
		EventDispatcher.BroadcastAsync<BattleAction_SuckBlood, bool>(BattleActionEvent.SuckBlood, battleAction_SuckBlood, false);
	}

	public static void SendAddSkill(long targetID, int skillIndex, int skillID, int skillLevel)
	{
		BattleAction_AddSkill battleAction_AddSkill = new BattleAction_AddSkill();
		battleAction_AddSkill.soldierId = targetID;
		battleAction_AddSkill.skillInfo = new BattleSkillInfo
		{
			skillIdx = skillIndex,
			skillId = skillID,
			skillLv = skillLevel
		};
		EventDispatcher.Broadcast<BattleAction_AddSkill, bool>(BattleActionEvent.AddSkill, battleAction_AddSkill, false);
	}

	public static void SendRemoveSkill(long targetID, int skillID)
	{
		BattleAction_RemoveSkill battleAction_RemoveSkill = new BattleAction_RemoveSkill();
		battleAction_RemoveSkill.soldierId = targetID;
		battleAction_RemoveSkill.skillId = skillID;
		EventDispatcher.Broadcast<BattleAction_RemoveSkill, bool>(BattleActionEvent.RemoveSkill, battleAction_RemoveSkill, false);
	}

	public static void SendFix(long targetID)
	{
		BattleAction_Fix battleAction_Fix = new BattleAction_Fix();
		battleAction_Fix.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_Fix, bool>(BattleActionEvent.Fix, battleAction_Fix, false);
	}

	public static void SendEndFix(long targetID)
	{
		BattleAction_EndFix battleAction_EndFix = new BattleAction_EndFix();
		battleAction_EndFix.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndFix, bool>(BattleActionEvent.EndFix, battleAction_EndFix, false);
	}

	public static void SendStatic(long targetID)
	{
		BattleAction_Static battleAction_Static = new BattleAction_Static();
		battleAction_Static.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_Static, bool>(BattleActionEvent.Static, battleAction_Static, false);
	}

	public static void SendEndStatic(long targetID)
	{
		BattleAction_EndStatic battleAction_EndStatic = new BattleAction_EndStatic();
		battleAction_EndStatic.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndStatic, bool>(BattleActionEvent.EndStatic, battleAction_EndStatic, false);
	}

	public static void SendTaunt(long targetID)
	{
		BattleAction_Taunt battleAction_Taunt = new BattleAction_Taunt();
		battleAction_Taunt.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_Taunt, bool>(BattleActionEvent.Taunt, battleAction_Taunt, false);
	}

	public static void SendEndTaunt(long targetID)
	{
		BattleAction_EndTaunt battleAction_EndTaunt = new BattleAction_EndTaunt();
		battleAction_EndTaunt.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndTaunt, bool>(BattleActionEvent.EndTaunt, battleAction_EndTaunt, false);
	}

	public static void SendSuperArmor(long targetID)
	{
		BattleAction_SuperArmor battleAction_SuperArmor = new BattleAction_SuperArmor();
		battleAction_SuperArmor.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_SuperArmor, bool>(BattleActionEvent.SuperArmor, battleAction_SuperArmor, false);
	}

	public static void SendEndSuperArmor(long targetID)
	{
		BattleAction_EndSuperArmor battleAction_EndSuperArmor = new BattleAction_EndSuperArmor();
		battleAction_EndSuperArmor.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndSuperArmor, bool>(BattleActionEvent.EndSuperArmor, battleAction_EndSuperArmor, false);
	}

	public static void SendIgnoreFormula(long targetID)
	{
		BattleAction_IgnoreDmgFormula battleAction_IgnoreDmgFormula = new BattleAction_IgnoreDmgFormula();
		battleAction_IgnoreDmgFormula.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_IgnoreDmgFormula, bool>(BattleActionEvent.IgnoreFormula, battleAction_IgnoreDmgFormula, false);
	}

	public static void SendEndIgnoreFormula(long targetID)
	{
		BattleAction_EndIgnoreDmgFormula battleAction_EndIgnoreDmgFormula = new BattleAction_EndIgnoreDmgFormula();
		battleAction_EndIgnoreDmgFormula.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndIgnoreDmgFormula, bool>(BattleActionEvent.EndIgnoreFormula, battleAction_EndIgnoreDmgFormula, false);
	}

	public static void SendCloseRenderer(long targetID)
	{
		BattleAction_CloseRenderer battleAction_CloseRenderer = new BattleAction_CloseRenderer();
		battleAction_CloseRenderer.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_CloseRenderer, bool>(BattleActionEvent.CloseRenderer, battleAction_CloseRenderer, false);
	}

	public static void SendEndCloseRenderer(long targetID)
	{
		BattleAction_EndCloseRenderer battleAction_EndCloseRenderer = new BattleAction_EndCloseRenderer();
		battleAction_EndCloseRenderer.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_EndCloseRenderer, bool>(BattleActionEvent.EndCloseRenderer, battleAction_EndCloseRenderer, false);
	}

	public static void SendMoveCast(long targetID)
	{
		BattleAction_MoveCast battleAction_MoveCast = new BattleAction_MoveCast();
		battleAction_MoveCast.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_MoveCast, bool>(BattleActionEvent.MoveCast, battleAction_MoveCast, false);
	}

	public static void SendEndMoveCast(long targetID)
	{
		BattleAction_EndMoveCast battleAction_EndMoveCast = new BattleAction_EndMoveCast();
		battleAction_EndMoveCast.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndMoveCast, bool>(BattleActionEvent.EndMoveCast, battleAction_EndMoveCast, false);
	}

	public static void SendDizzy(long targetID)
	{
		BattleAction_Stun battleAction_Stun = new BattleAction_Stun();
		battleAction_Stun.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_Stun, bool>(BattleActionEvent.Dizzy, battleAction_Stun, false);
	}

	public static void SendEndDizzy(long targetID)
	{
		BattleAction_EndStun battleAction_EndStun = new BattleAction_EndStun();
		battleAction_EndStun.soldierId = targetID;
		EventDispatcher.BroadcastAsync<BattleAction_EndStun, bool>(BattleActionEvent.EndDizzy, battleAction_EndStun, false);
	}

	public static void SendUnconspicuous(long targetID)
	{
		BattleAction_AtkProof battleAction_AtkProof = new BattleAction_AtkProof();
		battleAction_AtkProof.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_AtkProof, bool>(BattleActionEvent.AtkProof, battleAction_AtkProof, false);
	}

	public static void SendEndUnconspicuous(long targetID)
	{
		BattleAction_EndAtkProof battleAction_EndAtkProof = new BattleAction_EndAtkProof();
		battleAction_EndAtkProof.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_EndAtkProof, bool>(BattleActionEvent.EndAtkProof, battleAction_EndAtkProof, false);
	}

	public static void SendTeleport(long targetID, Pos toPos)
	{
		BattleAction_Teleport battleAction_Teleport = new BattleAction_Teleport();
		battleAction_Teleport.objId = targetID;
		battleAction_Teleport.toPos = toPos;
		EventDispatcher.Broadcast<BattleAction_Teleport, bool>(BattleActionEvent.Teleport, battleAction_Teleport, false);
	}

	public static void SendWeak(long targetID)
	{
		BattleAction_Weak battleAction_Weak = new BattleAction_Weak();
		battleAction_Weak.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_Weak, bool>(BattleActionEvent.Weak, battleAction_Weak, false);
		if (InstanceManager.CurrentCommunicationType == CommunicationType.Mixed)
		{
			GlobalBattleNetwork.Instance.SendClientDriveWeakState(targetID, true);
		}
	}

	public static void SendEndWeak(long targetID)
	{
		BattleAction_EndWeak battleAction_EndWeak = new BattleAction_EndWeak();
		battleAction_EndWeak.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_EndWeak, bool>(BattleActionEvent.EndWeak, battleAction_EndWeak, false);
		if (InstanceManager.CurrentCommunicationType == CommunicationType.Mixed)
		{
			GlobalBattleNetwork.Instance.SendClientDriveWeakState(targetID, false);
		}
	}

	public static void SendIncurable(long targetID)
	{
		BattleAction_Incurable battleAction_Incurable = new BattleAction_Incurable();
		battleAction_Incurable.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_Incurable, bool>(BattleActionEvent.Incurable, battleAction_Incurable, false);
	}

	public static void SendEndIncurable(long targetID)
	{
		BattleAction_EndIncurable battleAction_EndIncurable = new BattleAction_EndIncurable();
		battleAction_EndIncurable.soldierId = targetID;
		EventDispatcher.Broadcast<BattleAction_EndIncurable, bool>(BattleActionEvent.EndIncurable, battleAction_EndIncurable, false);
	}
}
