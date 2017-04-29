using GameData;
using Package;
using System;
using System.Linq;
using UnityEngine;

public class LocalDimensionMonsterInfoCreator
{
	public static MapObjInfo CreateMonsterMapObjInfo(int poolID, int monsterTypeID, int monsterLevel, long ownerID, int camp, bool isBoss, Vector3 position, bool isFighting = true)
	{
		Monster monsterData = DataReader<Monster>.Get(monsterTypeID);
		if (monsterData == null)
		{
			EntityWorld.Instance.ForceOut("没有数据", string.Format("Monster表根本没有怪物{0}的数据", monsterTypeID), null);
		}
		MonsterAttr monsterAttr = Enumerable.FirstOrDefault<MonsterAttr>(DataReader<MonsterAttr>.DataList, (MonsterAttr x) => x.lv == monsterLevel && x.id == monsterData.AttributeTemplateID);
		if (monsterAttr == null)
		{
			EntityWorld.Instance.ForceOut("没有数据", string.Format("怪物属性表里没有模板ID{0} & 等级{1}的数据\n怪物id为{2}", monsterData.AttributeTemplateID, monsterLevel, monsterTypeID), null);
		}
		MapObjInfo mapObjInfo = new MapObjInfo();
		mapObjInfo.objType = GameObjectType.ENUM.Soldier;
		mapObjInfo.id = (long)poolID;
		mapObjInfo.ownerId = ownerID;
		mapObjInfo.typeId = monsterTypeID;
		mapObjInfo.modelId = monsterData.model;
		mapObjInfo.name = mapObjInfo.id.ToString();
		mapObjInfo.rankValue = 0;
		mapObjInfo.pos = new Pos();
		mapObjInfo.pos.x = (float)((int)position.x * 100);
		mapObjInfo.pos.y = (float)((int)position.z * 100);
		mapObjInfo.vector = InstanceManager.GetMonsterFixBornDirection(monsterData.monsterBornDirection, position, ownerID, monsterData.scenePoint);
		mapObjInfo.mapLayer = 0;
		BattleBaseAttrs battleBaseAttrs = new BattleBaseAttrs();
		battleBaseAttrs.SetValue(GameData.AttrType.Lv, monsterLevel, true);
		battleBaseAttrs.SetValue(GameData.AttrType.HpLmt, monsterAttr.hp, true);
		battleBaseAttrs.SetValue(GameData.AttrType.Atk, monsterAttr.atk, true);
		battleBaseAttrs.SetValue(GameData.AttrType.Defence, monsterAttr.defence, true);
		battleBaseAttrs.SetValue(GameData.AttrType.HitRatio, monsterAttr.hit, true);
		battleBaseAttrs.SetValue(GameData.AttrType.DodgeRatio, monsterAttr.dex, true);
		battleBaseAttrs.SetValue(GameData.AttrType.CritRatio, monsterAttr.crt, true);
		battleBaseAttrs.SetValue(GameData.AttrType.DecritRatio, monsterAttr.penetration, true);
		battleBaseAttrs.SetValue(GameData.AttrType.CritHurtAddRatio, monsterAttr.critHurtAddRatio, true);
		battleBaseAttrs.SetValue(GameData.AttrType.ParryRatio, monsterAttr.parry, true);
		battleBaseAttrs.SetValue(GameData.AttrType.DeparryRatio, monsterAttr.vigour, true);
		battleBaseAttrs.SetValue(GameData.AttrType.ParryHurtDeRatio, monsterAttr.parryHurtDeRatio, true);
		battleBaseAttrs.SetValue(GameData.AttrType.ActSpeed, monsterAttr.attackSpeed, true);
		battleBaseAttrs.SetValue(GameData.AttrType.VpLmt, monsterAttr.Vp, true);
		battleBaseAttrs.SetValue(GameData.AttrType.VpResume, monsterAttr.VpResume, true);
		battleBaseAttrs.SetValue(GameData.AttrType.IdleVpResume, monsterAttr.IdleVpResume, true);
		if (monsterData.propId != 0)
		{
			battleBaseAttrs.AddValuesByTemplateID(monsterData.propId);
		}
		battleBaseAttrs.SetValue(GameData.AttrType.HpLmt, (long)((double)battleBaseAttrs.GetValue(GameData.AttrType.HpLmt) * (1.0 + (double)monsterData.HpAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.Atk, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.Atk) * (1.0 + (double)monsterData.AttAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.Defence, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.Defence) * (1.0 + (double)monsterData.DefAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.HitRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.HitRatio) * (1.0 + (double)monsterData.HitAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.DodgeRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.DodgeRatio) * (1.0 + (double)monsterData.DexAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.CritRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.CritRatio) * (1.0 + (double)monsterData.CrtAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.DecritRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.DecritRatio) * (1.0 + (double)monsterData.PenAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.CritHurtAddRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.CritHurtAddRatio) * (1.0 + (double)monsterData.CthAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.ParryRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.ParryRatio) * (1.0 + (double)monsterData.ParAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.DeparryRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.DeparryRatio) * (1.0 + (double)monsterData.VigAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.ParryHurtDeRatio, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.ParryHurtDeRatio) * (1.0 + (double)monsterData.PrhAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.ActSpeed, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.ActSpeed) * (1.0 + (double)monsterData.AtsAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.VpLmt, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.VpLmt) * (1.0 + (double)monsterData.VpAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.VpResume, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.VpResume) * (1.0 + (double)monsterData.VsAmplificationRate * 0.001)), true);
		battleBaseAttrs.SetValue(GameData.AttrType.IdleVpResume, (int)((double)battleBaseAttrs.GetValue(GameData.AttrType.IdleVpResume) * (1.0 + (double)monsterData.IvAmplificationRate * 0.001)), true);
		mapObjInfo.battleInfo = LocalDimensionMonsterInfoCreator.CreateMonsterBattleBaseInfo(monsterData, battleBaseAttrs, camp, isBoss, isFighting);
		return mapObjInfo;
	}

	protected static BattleBaseInfo CreateMonsterBattleBaseInfo(Monster monsterData, BattleBaseAttrs attrs, int camp, bool isBoss, bool isFighting = true)
	{
		if (attrs == null)
		{
			return null;
		}
		BattleBaseInfo battleBaseInfo = new BattleBaseInfo();
		battleBaseInfo.publicBaseInfo = new PublicBaseInfo();
		battleBaseInfo.publicBaseInfo.simpleInfo = new SimpleBaseInfo();
		battleBaseInfo.battleBaseAttr = new BattleBaseAttr();
		battleBaseInfo.wrapType = GameObjectType.ENUM.Monster;
		battleBaseInfo.camp = camp;
		battleBaseInfo.publicBaseInfo.simpleInfo.MoveSpeed = DataReader<AvatarModel>.Get(monsterData.model).speed;
		battleBaseInfo.publicBaseInfo.simpleInfo.AtkSpeed = attrs.ActSpeed;
		battleBaseInfo.publicBaseInfo.simpleInfo.Lv = attrs.Lv;
		battleBaseInfo.publicBaseInfo.simpleInfo.Fighting = attrs.Fighting;
		battleBaseInfo.publicBaseInfo.simpleInfo.VipLv = attrs.VipLv;
		battleBaseInfo.publicBaseInfo.Atk = attrs.Atk;
		battleBaseInfo.publicBaseInfo.Defence = attrs.Defence;
		battleBaseInfo.publicBaseInfo.HpLmt = attrs.HpLmt;
		battleBaseInfo.publicBaseInfo.PveAtk = attrs.PveAtk;
		battleBaseInfo.publicBaseInfo.PvpAtk = attrs.PvpAtk;
		battleBaseInfo.publicBaseInfo.HitRatio = attrs.HitRatio;
		battleBaseInfo.publicBaseInfo.DodgeRatio = attrs.DodgeRatio;
		battleBaseInfo.publicBaseInfo.CritRatio = attrs.CritRatio;
		battleBaseInfo.publicBaseInfo.DecritRatio = attrs.DecritRatio;
		battleBaseInfo.publicBaseInfo.CritHurtAddRatio = attrs.CritHurtAddRatio;
		battleBaseInfo.publicBaseInfo.ParryRatio = attrs.ParryRatio;
		battleBaseInfo.publicBaseInfo.DeparryRatio = attrs.DeparryRatio;
		battleBaseInfo.publicBaseInfo.ParryHurtDeRatio = attrs.ParryHurtDeRatio;
		battleBaseInfo.publicBaseInfo.SuckBloodScale = attrs.SuckBloodScale;
		battleBaseInfo.publicBaseInfo.HurtAddRatio = attrs.HurtAddRatio;
		battleBaseInfo.publicBaseInfo.HurtDeRatio = attrs.HurtDeRatio;
		battleBaseInfo.publicBaseInfo.PveHurtAddRatio = attrs.PveHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PveHurtDeRatio = attrs.PveHurtDeRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtAddRatio = attrs.PvpHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtDeRatio = attrs.PvpHurtDeRatio;
		battleBaseInfo.publicBaseInfo.AtkMulAmend = attrs.AtkMulAmend;
		battleBaseInfo.publicBaseInfo.DefMulAmend = attrs.DefMulAmend;
		battleBaseInfo.publicBaseInfo.HpLmtMulAmend = attrs.HpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.PveAtkMulAmend = attrs.PveAtkMulAmend;
		battleBaseInfo.publicBaseInfo.PvpAtkMulAmend = attrs.PvpAtkMulAmend;
		battleBaseInfo.battleBaseAttr.ActPointLmt = attrs.ActPointLmt;
		battleBaseInfo.publicBaseInfo.ActPointRecoverSpeedAmend = attrs.ActPointRecoverSpeedAmend;
		battleBaseInfo.publicBaseInfo.VpLmt = attrs.VpLmt;
		battleBaseInfo.publicBaseInfo.VpLmtMulAmend = attrs.VpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.VpAtk = attrs.VpAtk;
		battleBaseInfo.publicBaseInfo.VpAtkMulAmend = attrs.VpAtkMulAmend;
		battleBaseInfo.publicBaseInfo.VpResume = attrs.VpResume;
		battleBaseInfo.publicBaseInfo.IdleVpResume = attrs.IdleVpResume;
		battleBaseInfo.publicBaseInfo.WaterBuffAddProbAddAmend = attrs.WaterBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.WaterBuffDurTimeAddAmend = attrs.WaterBuffDurTimeAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffAddProbAddAmend = attrs.ThunderBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffAddProbAddAmend = attrs.ThunderBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.HealIncreasePercent = attrs.HealIncreasePercent;
		battleBaseInfo.publicBaseInfo.CritAddValue = attrs.CritAddValue;
		battleBaseInfo.publicBaseInfo.HpRestore = attrs.HpRestore;
		battleBaseInfo.battleBaseAttr.BuffMoveSpeedMulPosAmend = 0;
		battleBaseInfo.battleBaseAttr.BuffActSpeedMulPosAmend = 0;
		battleBaseInfo.battleBaseAttr.SkillTreatScaleBOAtk = 0;
		battleBaseInfo.battleBaseAttr.SkillTreatScaleBOHpLmt = 0;
		battleBaseInfo.battleBaseAttr.SkillNmlDmgScale = 0;
		battleBaseInfo.battleBaseAttr.SkillNmlDmgAddAmend = 0;
		battleBaseInfo.battleBaseAttr.SkillHolyDmgScaleBOMaxHp = 0;
		battleBaseInfo.battleBaseAttr.SkillHolyDmgScaleBOCurHp = 0;
		battleBaseInfo.battleBaseAttr.Affinity = 0;
		battleBaseInfo.battleBaseAttr.OnlineTime = 0;
		battleBaseInfo.battleBaseAttr.ActPoint = attrs.ActPoint;
		battleBaseInfo.battleBaseAttr.Hp = (long)((double)battleBaseInfo.publicBaseInfo.HpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.HpLmtMulAmend * 0.001));
		battleBaseInfo.battleBaseAttr.Vp = (int)((double)battleBaseInfo.publicBaseInfo.VpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.VpLmtMulAmend * 0.001));
		battleBaseInfo.ownedListIdx = 0;
		battleBaseInfo.ownedIds.Clear();
		for (int i = 0; i < monsterData.skill.get_Count(); i++)
		{
			battleBaseInfo.skills.Add(new BattleSkillInfo
			{
				skillIdx = i + 1,
				skillId = monsterData.skill.get_Item(i)
			});
		}
		battleBaseInfo.isLoading = false;
		battleBaseInfo.isFit = false;
		battleBaseInfo.isInFit = false;
		battleBaseInfo.isFighting = isFighting;
		battleBaseInfo.isFixed = false;
		battleBaseInfo.isStatic = false;
		battleBaseInfo.isTaunt = false;
		battleBaseInfo.isSuperArmor = false;
		battleBaseInfo.isIgnoreDmgFormula = false;
		battleBaseInfo.isCloseRenderer = false;
		battleBaseInfo.isStun = false;
		battleBaseInfo.isMoveCast = false;
		battleBaseInfo.isAssaulting = false;
		battleBaseInfo.isKnocking = false;
		battleBaseInfo.isSuspended = false;
		battleBaseInfo.isSkillManaging = false;
		battleBaseInfo.isSkillPressing = false;
		battleBaseInfo.isBorning = true;
		battleBaseInfo.isBoss = isBoss;
		battleBaseInfo.pressingSkillId = 0;
		battleBaseInfo.reliveTimes = 0;
		return battleBaseInfo;
	}
}
