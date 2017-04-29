using GameData;
using Package;
using System;
using System.Collections.Generic;

public class LocalBattleBuffCalculatorDamageHandler : LocalBattleBuffCalculatorHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffCalculatorDamageHandler.AppDamage(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffCalculatorDamageHandler.AppDamage(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void KillBuff(int buffID, long casterID, long targetID, int fromSkillLevel, bool isCommunicateMix)
	{
	}

	protected static void AppDamage(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		if (!LocalAgent.GetEntityCalculatable(caster, isCommunicateMix))
		{
			return;
		}
		if (!LocalAgent.GetEntityCalculatable(target, isCommunicateMix))
		{
			return;
		}
		if (target.IsUnconspicuous && buffData.forceHandle == 0)
		{
			return;
		}
		if (target.IsIgnoreFormula)
		{
			return;
		}
		XDict<GameData.AttrType, long> buffCasterTempAttr = LocalBattleBuffCalculatorHandler.GetBuffCasterTempAttr(buffData, caster, fromSkillLevel, fromSkillAttrChange);
		XDict<GameData.AttrType, long> buffTargetTempAttr = LocalBattleBuffCalculatorHandler.GetBuffTargetTempAttr(buffData, caster, fromSkillLevel, fromSkillAttrChange);
		BattleCalculator.DamageResult damageResult = BattleCalculator.CalculateDamage(caster.BattleBaseAttrs, target.BattleBaseAttrs, caster.IsEntitySelfType || caster.IsEntityPlayerType || caster.IsEntityPetType, target.IsEntitySelfType || target.IsEntityPlayerType, buffCasterTempAttr, buffTargetTempAttr);
		List<ClientDrvBuffInfo> casterBuffInfo = null;
		List<ClientDrvBuffInfo> targetBuffInfo = null;
		if (isCommunicateMix)
		{
			casterBuffInfo = LocalAgent.MakeClientDrvBuffInfo(caster.ID);
			targetBuffInfo = LocalAgent.MakeClientDrvBuffInfo(target.ID);
		}
		if (damageResult.Lifesteal > 0L && !damageResult.IsMiss && LocalAgent.GetEntityIsCurable(caster, isCommunicateMix))
		{
			long num = LocalAgent.GetSpiritCurHp(caster, isCommunicateMix) + damageResult.Lifesteal;
			if (num > caster.RealHpLmt)
			{
				num = caster.RealHpLmt;
			}
			Pos pos = null;
			if (caster.Actor)
			{
				pos = new Pos();
				pos.x = caster.Actor.FixTransform.get_position().x * 100f;
				pos.y = caster.Actor.FixTransform.get_position().z * 100f;
			}
			LocalAgent.SetSpiritCurHp(caster, num, isCommunicateMix);
			LocalBattleProtocolSimulator.SendTreat(caster.ID, (GameObjectType.ENUM)caster.WrapType, caster.ID, (GameObjectType.ENUM)caster.WrapType, BattleAction_Treat.TreatSrcType.SuckBlood, damageResult.Lifesteal, num, pos);
		}
		long num2 = LocalAgent.GetSpiritCurHp(target, isCommunicateMix) - damageResult.Damage;
		if (damageResult.IsMiss)
		{
			CauseMissAnnouncer.Announce(caster, target);
			UnderMissAnnouncer.Announce(target, caster);
		}
		else if (num2 <= 0L)
		{
			num2 = 0L;
			LocalBattleHitHandler.AppDead(target.ID);
			GlobalBattleNetwork.Instance.SendClientDrvBattleDeathNty(target.ID);
		}
		else
		{
			if (damageResult.IsCrit)
			{
				CauseCritAnnouncer.Announce(caster, target);
				UnderCritAnnouncer.Announce(target, caster);
			}
			if (damageResult.IsParry)
			{
				CauseParryAnnouncer.Announce(caster, target);
				UnderParryAnnouncer.Announce(target, caster);
			}
			LocalBattleHitHandler.AppWeakCalculate(caster, target, buffCasterTempAttr);
		}
		LocalAgent.SetSpiritCurHp(target, num2, isCommunicateMix);
		List<long> list = new List<long>();
		if (isCommunicateMix && !damageResult.IsMiss)
		{
			list.Add((long)damageResult.parryRandomIndex);
			list.Add((long)damageResult.critRandomIndex);
			list.Add((long)damageResult.damageRandomIndex);
			GlobalBattleNetwork.Instance.SendClientDriveBattleBuffDamage(caster.ID, target.ID, caster.Hp, num2, damageResult.Damage, buffData.id, true, casterBuffInfo, targetBuffInfo, list, string.Concat(new object[]
			{
				caster.TryAddValue(GameData.AttrType.SkillNmlDmgScale, buffCasterTempAttr),
				"_",
				caster.TryAddValue(GameData.AttrType.SkillNmlDmgAddAmend, buffCasterTempAttr),
				"_",
				target.DefMulAmend,
				"_",
				caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, buffCasterTempAttr)
			}));
		}
		if (list.get_Count() == 3)
		{
		}
		LocalBattleProtocolSimulator.SendBleed(target.ID, (GameObjectType.ENUM)target.WrapType, caster.ID, (GameObjectType.ENUM)caster.WrapType, BattleAction_Bleed.DmgSrcType.Attack, ElemType.ENUM.Normal, damageResult.Damage, num2, damageResult.IsCrit, damageResult.IsParry, damageResult.IsMiss);
	}
}
