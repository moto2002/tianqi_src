using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalDimensionPlayerInfoCreator
{
	public static MapObjInfo CreateSelfCopyMapObjInfo(int poolID, int sceneID, int pointGroupID, BattleBaseInfo battleInfo)
	{
		MapObjInfo mapObjInfo = new MapObjInfo();
		mapObjInfo.objType = GameObjectType.ENUM.Role;
		mapObjInfo.id = (long)poolID;
		mapObjInfo.ownerId = EntityWorld.Instance.EntSelf.OwnerID;
		mapObjInfo.typeId = EntityWorld.Instance.EntSelf.TypeID;
		mapObjInfo.modelId = EntityWorld.Instance.EntSelf.ModelID;
		mapObjInfo.name = EntityWorld.Instance.EntSelf.Name;
		mapObjInfo.rankValue = EntityWorld.Instance.EntSelf.TypeRank;
		Vector2 point = MapDataManager.Instance.GetPoint(sceneID, pointGroupID);
		string[] array = DataReader<Scene>.Get(sceneID).LockLookPoint.Split(new char[]
		{
			';'
		});
		float num = float.Parse(array[0]);
		float num2 = float.Parse(array[1]);
		Vector2 vector = new Vector2(num, num2);
		mapObjInfo.pos = new Pos();
		mapObjInfo.pos.x = point.x;
		mapObjInfo.pos.y = point.y;
		mapObjInfo.vector = new Vector2();
		mapObjInfo.vector.x = (vector - point).get_normalized().x;
		mapObjInfo.vector.y = (vector - point).get_normalized().y;
		mapObjInfo.mapLayer = 0;
		mapObjInfo.battleInfo = battleInfo;
		return mapObjInfo;
	}

	public static BattleBaseInfo CreateSelfCopyBattleInfo(bool isSameCamp, List<PetInfo> petOriginInfos, float instanceActionPoint)
	{
		BattleBaseInfo battleBaseInfo = new BattleBaseInfo();
		battleBaseInfo.publicBaseInfo = new PublicBaseInfo();
		battleBaseInfo.publicBaseInfo.simpleInfo = new SimpleBaseInfo();
		battleBaseInfo.battleBaseAttr = new BattleBaseAttr();
		battleBaseInfo.wrapType = GameObjectType.ENUM.Pet;
		battleBaseInfo.camp = ((!isSameCamp) ? ((EntityWorld.Instance.EntSelf.Camp != 1) ? 1 : 2) : EntityWorld.Instance.EntSelf.Camp);
		battleBaseInfo.publicBaseInfo.simpleInfo.MoveSpeed = EntityWorld.Instance.EntSelf.CityBaseAttrs.MoveSpeed;
		battleBaseInfo.publicBaseInfo.simpleInfo.AtkSpeed = EntityWorld.Instance.EntSelf.CityBaseAttrs.ActSpeed;
		battleBaseInfo.publicBaseInfo.simpleInfo.Lv = EntityWorld.Instance.EntSelf.CityBaseAttrs.Lv;
		battleBaseInfo.publicBaseInfo.simpleInfo.Fighting = EntityWorld.Instance.EntSelf.CityBaseAttrs.Fighting;
		battleBaseInfo.publicBaseInfo.simpleInfo.VipLv = EntityWorld.Instance.EntSelf.CityBaseAttrs.VipLv;
		battleBaseInfo.publicBaseInfo.Atk = EntityWorld.Instance.EntSelf.CityBaseAttrs.Atk;
		battleBaseInfo.publicBaseInfo.Defence = EntityWorld.Instance.EntSelf.CityBaseAttrs.Defence;
		battleBaseInfo.publicBaseInfo.HpLmt = EntityWorld.Instance.EntSelf.CityBaseAttrs.HpLmt;
		battleBaseInfo.publicBaseInfo.PveAtk = EntityWorld.Instance.EntSelf.CityBaseAttrs.PveAtk;
		battleBaseInfo.publicBaseInfo.PvpAtk = EntityWorld.Instance.EntSelf.CityBaseAttrs.PvpAtk;
		battleBaseInfo.publicBaseInfo.HitRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.HitRatio;
		battleBaseInfo.publicBaseInfo.DodgeRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.DodgeRatio;
		battleBaseInfo.publicBaseInfo.CritRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.CritRatio;
		battleBaseInfo.publicBaseInfo.DecritRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.DecritRatio;
		battleBaseInfo.publicBaseInfo.CritHurtAddRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.CritHurtAddRatio;
		battleBaseInfo.publicBaseInfo.ParryRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.ParryRatio;
		battleBaseInfo.publicBaseInfo.DeparryRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.DeparryRatio;
		battleBaseInfo.publicBaseInfo.ParryHurtDeRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.ParryHurtDeRatio;
		battleBaseInfo.publicBaseInfo.SuckBloodScale = EntityWorld.Instance.EntSelf.CityBaseAttrs.SuckBloodScale;
		battleBaseInfo.publicBaseInfo.HurtAddRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.HurtAddRatio;
		battleBaseInfo.publicBaseInfo.HurtDeRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.HurtDeRatio;
		battleBaseInfo.publicBaseInfo.PveHurtAddRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.PveHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PveHurtDeRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.PveHurtDeRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtAddRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.PvpHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtDeRatio = EntityWorld.Instance.EntSelf.CityBaseAttrs.PvpHurtDeRatio;
		battleBaseInfo.publicBaseInfo.AtkMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.AtkMulAmend;
		battleBaseInfo.publicBaseInfo.DefMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.DefMulAmend;
		battleBaseInfo.publicBaseInfo.HpLmtMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.HpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.PveAtkMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.PveAtkMulAmend;
		battleBaseInfo.publicBaseInfo.PvpAtkMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.PvpAtkMulAmend;
		battleBaseInfo.battleBaseAttr.ActPointLmt = (int)float.Parse(DataReader<GlobalParams>.Get("actionpoint_limit_i").value);
		battleBaseInfo.publicBaseInfo.ActPointRecoverSpeedAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.ActPointRecoverSpeedAmend;
		battleBaseInfo.publicBaseInfo.VpLmt = EntityWorld.Instance.EntSelf.CityBaseAttrs.VpLmt;
		battleBaseInfo.publicBaseInfo.VpLmtMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.VpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.VpAtk = EntityWorld.Instance.EntSelf.CityBaseAttrs.VpAtk;
		battleBaseInfo.publicBaseInfo.VpAtkMulAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.VpAtkMulAmend;
		battleBaseInfo.publicBaseInfo.VpResume = EntityWorld.Instance.EntSelf.CityBaseAttrs.VpResume;
		battleBaseInfo.publicBaseInfo.IdleVpResume = EntityWorld.Instance.EntSelf.CityBaseAttrs.IdleVpResume;
		battleBaseInfo.publicBaseInfo.WaterBuffAddProbAddAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.WaterBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.WaterBuffDurTimeAddAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.WaterBuffDurTimeAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffAddProbAddAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.ThunderBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffDurTimeAddAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.ThunderBuffDurTimeAddAmend;
		battleBaseInfo.publicBaseInfo.HealIncreasePercent = EntityWorld.Instance.EntSelf.CityBaseAttrs.HealIncreasePercent;
		battleBaseInfo.publicBaseInfo.CritAddValue = EntityWorld.Instance.EntSelf.CityBaseAttrs.CritAddValue;
		battleBaseInfo.publicBaseInfo.HpRestore = EntityWorld.Instance.EntSelf.CityBaseAttrs.HpRestore;
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
		battleBaseInfo.battleBaseAttr.ActPoint = (int)(instanceActionPoint + (float)SkillDataManager.Instance.GetSkillProInitActPoint() + (float)EntityWorld.Instance.EntSelf.TotalBeginActPoint);
		battleBaseInfo.battleBaseAttr.Hp = (long)((double)battleBaseInfo.publicBaseInfo.HpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.HpLmtMulAmend * 0.001));
		battleBaseInfo.battleBaseAttr.Vp = (int)((double)battleBaseInfo.publicBaseInfo.VpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.VpLmtMulAmend * 0.001));
		battleBaseInfo.ownedListIdx = 0;
		int num = 0;
		if (petOriginInfos != null)
		{
			for (int i = 0; i < petOriginInfos.get_Count(); i++)
			{
				if (DataReader<Pet>.Get(petOriginInfos.get_Item(i).petId) != null)
				{
					battleBaseInfo.ownedIds.Add(petOriginInfos.get_Item(i).id);
					BattleSkillInfo summonSkillInfo = PetManager.Instance.GetSummonSkillInfo(petOriginInfos.get_Item(i), num);
					if (summonSkillInfo != null)
					{
						battleBaseInfo.skills.Add(summonSkillInfo);
					}
					num++;
				}
			}
		}
		battleBaseInfo.finalOwnerId = 0L;
		SkillDataManager.Instance.ConstructBattleSkillInfo(battleBaseInfo.skills, null, battleBaseInfo.skillExs);
		battleBaseInfo.isFit = false;
		battleBaseInfo.isInFit = false;
		battleBaseInfo.isFighting = true;
		battleBaseInfo.isFixed = false;
		battleBaseInfo.isStatic = false;
		battleBaseInfo.isTaunt = false;
		battleBaseInfo.isSuperArmor = false;
		battleBaseInfo.isIgnoreDmgFormula = false;
		battleBaseInfo.isCloseRenderer = false;
		battleBaseInfo.isMoveCast = false;
		battleBaseInfo.isKnocking = false;
		battleBaseInfo.isSuspended = false;
		battleBaseInfo.isStun = false;
		battleBaseInfo.isBoss = false;
		return battleBaseInfo;
	}

	public static BattleBaseInfo CreateSelfCopyNowBattleInfo(bool isSameCamp, List<PetInfo> petOriginInfos)
	{
		BattleBaseInfo battleBaseInfo = new BattleBaseInfo();
		battleBaseInfo.publicBaseInfo = new PublicBaseInfo();
		battleBaseInfo.publicBaseInfo.simpleInfo = new SimpleBaseInfo();
		battleBaseInfo.battleBaseAttr = new BattleBaseAttr();
		battleBaseInfo.wrapType = GameObjectType.ENUM.Pet;
		battleBaseInfo.camp = ((!isSameCamp) ? ((EntityWorld.Instance.EntSelf.Camp != 1) ? 1 : 2) : EntityWorld.Instance.EntSelf.Camp);
		battleBaseInfo.publicBaseInfo.simpleInfo.MoveSpeed = EntityWorld.Instance.EntSelf.BattleBaseAttrs.MoveSpeed;
		battleBaseInfo.publicBaseInfo.simpleInfo.AtkSpeed = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ActSpeed;
		battleBaseInfo.publicBaseInfo.simpleInfo.Lv = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Lv;
		battleBaseInfo.publicBaseInfo.simpleInfo.Fighting = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Fighting;
		battleBaseInfo.publicBaseInfo.simpleInfo.VipLv = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VipLv;
		battleBaseInfo.publicBaseInfo.Atk = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Atk;
		battleBaseInfo.publicBaseInfo.Defence = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Defence;
		battleBaseInfo.publicBaseInfo.HpLmt = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HpLmt;
		battleBaseInfo.publicBaseInfo.PveAtk = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PveAtk;
		battleBaseInfo.publicBaseInfo.PvpAtk = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PvpAtk;
		battleBaseInfo.publicBaseInfo.HitRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HitRatio;
		battleBaseInfo.publicBaseInfo.DodgeRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.DodgeRatio;
		battleBaseInfo.publicBaseInfo.CritRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.CritRatio;
		battleBaseInfo.publicBaseInfo.DecritRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.DecritRatio;
		battleBaseInfo.publicBaseInfo.CritHurtAddRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.CritHurtAddRatio;
		battleBaseInfo.publicBaseInfo.ParryRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ParryRatio;
		battleBaseInfo.publicBaseInfo.DeparryRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.DeparryRatio;
		battleBaseInfo.publicBaseInfo.ParryHurtDeRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ParryHurtDeRatio;
		battleBaseInfo.publicBaseInfo.SuckBloodScale = EntityWorld.Instance.EntSelf.BattleBaseAttrs.SuckBloodScale;
		battleBaseInfo.publicBaseInfo.HurtAddRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HurtAddRatio;
		battleBaseInfo.publicBaseInfo.HurtDeRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HurtDeRatio;
		battleBaseInfo.publicBaseInfo.PveHurtAddRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PveHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PveHurtDeRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PveHurtDeRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtAddRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PvpHurtAddRatio;
		battleBaseInfo.publicBaseInfo.PvpHurtDeRatio = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PvpHurtDeRatio;
		battleBaseInfo.publicBaseInfo.AtkMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.AtkMulAmend;
		battleBaseInfo.publicBaseInfo.DefMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.DefMulAmend;
		battleBaseInfo.publicBaseInfo.HpLmtMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.PveAtkMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PveAtkMulAmend;
		battleBaseInfo.publicBaseInfo.PvpAtkMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.PvpAtkMulAmend;
		battleBaseInfo.battleBaseAttr.ActPointLmt = (int)float.Parse(DataReader<GlobalParams>.Get("actionpoint_limit_i").value);
		battleBaseInfo.publicBaseInfo.ActPointRecoverSpeedAmend = EntityWorld.Instance.EntSelf.CityBaseAttrs.ActPointRecoverSpeedAmend;
		battleBaseInfo.publicBaseInfo.VpLmt = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VpLmt;
		battleBaseInfo.publicBaseInfo.VpLmtMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VpLmtMulAmend;
		battleBaseInfo.publicBaseInfo.VpAtk = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VpAtk;
		battleBaseInfo.publicBaseInfo.VpAtkMulAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VpAtkMulAmend;
		battleBaseInfo.publicBaseInfo.VpResume = EntityWorld.Instance.EntSelf.BattleBaseAttrs.VpResume;
		battleBaseInfo.publicBaseInfo.IdleVpResume = EntityWorld.Instance.EntSelf.BattleBaseAttrs.IdleVpResume;
		battleBaseInfo.publicBaseInfo.WaterBuffAddProbAddAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.WaterBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.WaterBuffDurTimeAddAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.WaterBuffDurTimeAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffAddProbAddAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ThunderBuffAddProbAddAmend;
		battleBaseInfo.publicBaseInfo.ThunderBuffDurTimeAddAmend = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ThunderBuffDurTimeAddAmend;
		battleBaseInfo.publicBaseInfo.HealIncreasePercent = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HealIncreasePercent;
		battleBaseInfo.publicBaseInfo.CritAddValue = EntityWorld.Instance.EntSelf.BattleBaseAttrs.CritAddValue;
		battleBaseInfo.publicBaseInfo.HpRestore = EntityWorld.Instance.EntSelf.BattleBaseAttrs.HpRestore;
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
		battleBaseInfo.battleBaseAttr.ActPoint = EntityWorld.Instance.EntSelf.BattleBaseAttrs.ActPoint;
		battleBaseInfo.battleBaseAttr.Hp = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Hp;
		battleBaseInfo.battleBaseAttr.Vp = EntityWorld.Instance.EntSelf.BattleBaseAttrs.Vp;
		battleBaseInfo.ownedListIdx = 0;
		int num = 0;
		for (int i = 0; i < petOriginInfos.get_Count(); i++)
		{
			if (DataReader<Pet>.Get(petOriginInfos.get_Item(i).petId) != null)
			{
				battleBaseInfo.ownedIds.Add(petOriginInfos.get_Item(i).id);
				BattleSkillInfo summonSkillInfo = PetManager.Instance.GetSummonSkillInfo(petOriginInfos.get_Item(i), num);
				if (summonSkillInfo != null)
				{
					battleBaseInfo.skills.Add(summonSkillInfo);
				}
				num++;
			}
		}
		battleBaseInfo.finalOwnerId = 0L;
		SkillDataManager.Instance.ConstructBattleSkillInfo(battleBaseInfo.skills, null, battleBaseInfo.skillExs);
		battleBaseInfo.isFit = false;
		battleBaseInfo.isInFit = false;
		battleBaseInfo.isFighting = true;
		battleBaseInfo.isFixed = false;
		battleBaseInfo.isStatic = false;
		battleBaseInfo.isTaunt = false;
		battleBaseInfo.isSuperArmor = false;
		battleBaseInfo.isIgnoreDmgFormula = false;
		battleBaseInfo.isCloseRenderer = false;
		battleBaseInfo.isMoveCast = false;
		battleBaseInfo.isKnocking = false;
		battleBaseInfo.isSuspended = false;
		battleBaseInfo.isStun = false;
		battleBaseInfo.isBoss = false;
		return battleBaseInfo;
	}
}
