using GameData;
using Package;
using System;
using System.Collections.Generic;

public class LocalBattleEffectCalculatorDamageHandler : LocalBattleEffectCalculatorHandler
{
	public static void AppDamage(Effect effectData, EntityParent caster, List<long> effectTargetIDs, XPoint basePoint, int skillID, bool isAddEffect, bool isCommunicateMix)
	{
		if (!LocalAgent.GetEntityCalculatable(caster, isCommunicateMix))
		{
			return;
		}
		XDict<GameData.AttrType, long> effectCasterTempAttr = LocalBattleEffectCalculatorHandler.GetEffectCasterTempAttr(effectData, caster, skillID);
		bool flag = isAddEffect && isCommunicateMix;
		for (int i = 0; i < effectTargetIDs.get_Count(); i++)
		{
			if (LocalAgent.GetEntityCalculatable(caster, isCommunicateMix))
			{
				EntityParent entityByID = LocalAgent.GetEntityByID(effectTargetIDs.get_Item(i));
				if (LocalAgent.GetEntityCalculatable(entityByID, isCommunicateMix))
				{
					if (!entityByID.IsUnconspicuous || effectData.forcePickup != 0)
					{
						if (!entityByID.IsIgnoreFormula)
						{
							CauseDamageEffectAnnouncer.Announce(caster, entityByID, effectData.id);
							UnderDamageEffectAnnouncer.Announce(entityByID, caster, effectData.id);
							BattleCalculator.DamageResult damageResult = BattleCalculator.CalculateDamage(caster.BattleBaseAttrs, entityByID.BattleBaseAttrs, caster.IsEntitySelfType || caster.IsEntityPlayerType || caster.IsEntityPetType, entityByID.IsEntitySelfType || entityByID.IsEntityPlayerType, effectCasterTempAttr, null);
							List<ClientDrvBuffInfo> casterBuffInfo = null;
							List<ClientDrvBuffInfo> targetBuffInfo = null;
							if (isCommunicateMix)
							{
								casterBuffInfo = LocalAgent.MakeClientDrvBuffInfo(caster.ID);
								targetBuffInfo = LocalAgent.MakeClientDrvBuffInfo(effectTargetIDs.get_Item(i));
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
							long num2 = LocalAgent.GetSpiritCurHp(entityByID, isCommunicateMix) - damageResult.Damage;
							if (damageResult.IsMiss)
							{
								CauseMissAnnouncer.Announce(caster, entityByID);
								UnderMissAnnouncer.Announce(entityByID, caster);
							}
							else if (num2 <= 0L)
							{
								num2 = 0L;
								LocalBattleHitHandler.AppDead(effectTargetIDs.get_Item(i));
								GlobalBattleNetwork.Instance.SendClientDrvBattleDeathNty(effectTargetIDs.get_Item(i));
							}
							else
							{
								if (damageResult.IsCrit)
								{
									CauseCritAnnouncer.Announce(caster, entityByID);
									UnderCritAnnouncer.Announce(entityByID, caster);
								}
								if (damageResult.IsParry)
								{
									CauseParryAnnouncer.Announce(caster, entityByID);
									UnderParryAnnouncer.Announce(entityByID, caster);
									LocalBattleHitHandler.AppParryFx(entityByID);
								}
								else
								{
									LocalBattleHitHandler.AppHit(effectData, caster, entityByID, basePoint, isAddEffect);
								}
								LocalBattleHitHandler.AppHitAudio(effectData, entityByID);
								LocalBattleHitHandler.AppWeakCalculate(caster, entityByID, effectCasterTempAttr);
							}
							LocalAgent.SetSpiritCurHp(entityByID, num2, isCommunicateMix);
							List<long> list = new List<long>();
							if (isCommunicateMix && !damageResult.IsMiss)
							{
								list.Add((long)damageResult.parryRandomIndex);
								list.Add((long)damageResult.critRandomIndex);
								list.Add((long)damageResult.damageRandomIndex);
								GlobalBattleNetwork.Instance.SendClientDriveBattleEffectDamage(caster.ID, effectTargetIDs.get_Item(i), caster.Hp, num2, damageResult.Damage, skillID, effectData.id, flag, true, casterBuffInfo, targetBuffInfo, basePoint, list, string.Concat(new object[]
								{
									caster.TryAddValue(GameData.AttrType.SkillNmlDmgScale, effectCasterTempAttr),
									"_",
									caster.TryAddValue(GameData.AttrType.SkillNmlDmgAddAmend, effectCasterTempAttr),
									"_",
									entityByID.DefMulAmend,
									"_",
									caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, effectCasterTempAttr)
								}), false);
							}
							if (list.get_Count() == 3)
							{
							}
							LocalBattleProtocolSimulator.SendBleed(effectTargetIDs.get_Item(i), (GameObjectType.ENUM)entityByID.WrapType, caster.ID, (GameObjectType.ENUM)caster.WrapType, BattleAction_Bleed.DmgSrcType.Attack, ElemType.ENUM.Normal, damageResult.Damage, num2, damageResult.IsCrit, damageResult.IsParry, damageResult.IsMiss);
							if (flag)
							{
								flag = false;
							}
						}
					}
				}
			}
		}
	}
}
