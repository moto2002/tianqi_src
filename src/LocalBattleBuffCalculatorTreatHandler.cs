using GameData;
using Package;
using System;
using System.Collections.Generic;

public class LocalBattleBuffCalculatorTreatHandler : LocalBattleBuffCalculatorHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffCalculatorTreatHandler.AppTreat(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffCalculatorTreatHandler.AppTreat(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void KillBuff(int buffID, long casterID, long targetID, int fromSkillLevel, bool isCommunicateMix)
	{
	}

	protected static void AppTreat(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		if (!LocalAgent.GetEntityCalculatable(caster, isCommunicateMix))
		{
			return;
		}
		if (!LocalAgent.GetEntityIsCurable(target, isCommunicateMix))
		{
			return;
		}
		if (target.IsUnconspicuous && buffData.forceHandle == 0)
		{
			return;
		}
		XDict<GameData.AttrType, long> buffCasterTempAttr = LocalBattleBuffCalculatorHandler.GetBuffCasterTempAttr(buffData, caster, fromSkillLevel, fromSkillAttrChange);
		XDict<GameData.AttrType, long> buffTargetTempAttr = LocalBattleBuffCalculatorHandler.GetBuffTargetTempAttr(buffData, caster, fromSkillLevel, fromSkillAttrChange);
		long num = BattleCalculator.CalculateTreatment(caster.BattleBaseAttrs, target.BattleBaseAttrs, target.IsEntitySelfType || target.IsEntityPlayerType, buffCasterTempAttr, buffTargetTempAttr);
		if (num != 0L)
		{
			List<ClientDrvBuffInfo> casterBuffInfo = null;
			List<ClientDrvBuffInfo> targetBuffInfo = null;
			if (isCommunicateMix)
			{
				casterBuffInfo = LocalAgent.MakeClientDrvBuffInfo(caster.ID);
				targetBuffInfo = LocalAgent.MakeClientDrvBuffInfo(target.ID);
			}
			long num2 = LocalAgent.GetSpiritCurHp(target, isCommunicateMix) + num;
			if (num2 > target.RealHpLmt)
			{
				num2 = target.RealHpLmt;
			}
			Pos pos = null;
			if (caster.Actor)
			{
				pos = new Pos();
				pos.x = caster.Actor.FixTransform.get_position().x * 100f;
				pos.y = caster.Actor.FixTransform.get_position().z * 100f;
			}
			LocalAgent.SetSpiritCurHp(target, num2, isCommunicateMix);
			if (isCommunicateMix)
			{
				GlobalBattleNetwork.Instance.SendClientDriveBattleBuffDamage(caster.ID, target.ID, caster.Hp, num2, num, buffData.id, true, casterBuffInfo, targetBuffInfo, new List<long>(), string.Concat(new object[]
				{
					caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOAtk, buffCasterTempAttr),
					"_",
					caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOHpLmt, buffCasterTempAttr),
					"_",
					caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, buffCasterTempAttr)
				}));
			}
			LocalBattleProtocolSimulator.SendTreat(target.ID, (GameObjectType.ENUM)target.WrapType, caster.ID, (GameObjectType.ENUM)caster.WrapType, BattleAction_Treat.TreatSrcType.Treat, num, num2, pos);
		}
	}
}
