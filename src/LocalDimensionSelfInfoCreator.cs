using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalDimensionSelfInfoCreator
{
	public static MapObjInfo CreateSelfMapObjInfo(int instanceType, int instanceDataID, int sceneID, int trailType = 0, int trailModel = 0, MapObjDecorations trialDecorations = null, List<PetInfo> trialPetInfos = null, List<BattleSkillInfo> trialSkillInfos = null)
	{
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceDataID);
		MapObjInfo mapObjInfo = new MapObjInfo();
		mapObjInfo.objType = GameObjectType.ENUM.Role;
		mapObjInfo.id = EntityWorld.Instance.EntSelf.ID;
		mapObjInfo.ownerId = EntityWorld.Instance.EntSelf.OwnerID;
		if (trailType == 0)
		{
			mapObjInfo.typeId = EntityWorld.Instance.EntSelf.TypeID;
		}
		else
		{
			mapObjInfo.typeId = trailType;
		}
		if (trailModel == 0)
		{
			mapObjInfo.modelId = EntityWorld.Instance.EntSelf.ModelID;
		}
		else
		{
			mapObjInfo.modelId = trailModel;
		}
		mapObjInfo.name = EntityWorld.Instance.EntSelf.Name;
		mapObjInfo.rankValue = EntityWorld.Instance.EntSelf.TypeRank;
		Vector2 point = MapDataManager.Instance.GetPoint(sceneID, fuBenJiChuPeiZhi.pointId);
		string[] array = DataReader<Scene>.Get(sceneID).LockLookPoint.Split(new char[]
		{
			';'
		});
		Vector2 vector = new Vector2(float.Parse(array[0]), float.Parse(array[1]));
		mapObjInfo.pos = new Pos();
		mapObjInfo.pos.x = point.x;
		mapObjInfo.pos.y = point.y;
		mapObjInfo.vector = new Vector2();
		mapObjInfo.vector.x = (vector - point).get_normalized().x;
		mapObjInfo.vector.y = (vector - point).get_normalized().y;
		mapObjInfo.mapLayer = 0;
		mapObjInfo.decorations = new MapObjDecorations();
		if (trialDecorations == null)
		{
			mapObjInfo.decorations.career = EntityWorld.Instance.EntSelf.Decorations.career;
			mapObjInfo.decorations.modelId = EntityWorld.Instance.EntSelf.Decorations.modelId;
			mapObjInfo.decorations.equipIds.Clear();
			if (EntityWorld.Instance.EntSelf.Decorations.equipIds != null)
			{
				mapObjInfo.decorations.equipIds.AddRange(EntityWorld.Instance.EntSelf.Decorations.equipIds);
			}
			mapObjInfo.decorations.petUUId = EntityWorld.Instance.EntSelf.Decorations.petUUId;
			mapObjInfo.decorations.petId = EntityWorld.Instance.EntSelf.Decorations.petId;
			mapObjInfo.decorations.petStar = EntityWorld.Instance.EntSelf.Decorations.petStar;
			mapObjInfo.decorations.wingId = EntityWorld.Instance.EntSelf.Decorations.wingId;
			mapObjInfo.decorations.wingLv = EntityWorld.Instance.EntSelf.Decorations.wingLv;
			mapObjInfo.decorations.wingHidden = EntityWorld.Instance.EntSelf.Decorations.wingHidden;
			mapObjInfo.decorations.fashions.Clear();
			if (EntityWorld.Instance.EntSelf.Decorations.fashions != null)
			{
				mapObjInfo.decorations.fashions.AddRange(EntityWorld.Instance.EntSelf.Decorations.fashions);
			}
		}
		else
		{
			mapObjInfo.decorations.career = trialDecorations.career;
			mapObjInfo.decorations.modelId = trialDecorations.modelId;
			mapObjInfo.decorations.equipIds.Clear();
			if (trialDecorations.equipIds != null)
			{
				mapObjInfo.decorations.equipIds.AddRange(trialDecorations.equipIds);
			}
			mapObjInfo.decorations.petUUId = trialDecorations.petUUId;
			mapObjInfo.decorations.petId = trialDecorations.petId;
			mapObjInfo.decorations.petStar = trialDecorations.petStar;
			mapObjInfo.decorations.wingId = trialDecorations.wingId;
			mapObjInfo.decorations.wingLv = trialDecorations.wingLv;
			mapObjInfo.decorations.wingHidden = trialDecorations.wingHidden;
			mapObjInfo.decorations.fashions.Clear();
			if (trialDecorations.fashions != null)
			{
				mapObjInfo.decorations.fashions.AddRange(trialDecorations.fashions);
			}
		}
		mapObjInfo.battleInfo = LocalDimensionSelfInfoCreator.CreateSelfBattleBaseInfo(instanceType, fuBenJiChuPeiZhi, trialPetInfos, trialSkillInfos);
		return mapObjInfo;
	}

	protected static BattleBaseInfo CreateSelfBattleBaseInfo(int instanceType, FuBenJiChuPeiZhi instanceData, List<PetInfo> trialPetInfos = null, List<BattleSkillInfo> trialSkillInfos = null)
	{
		BattleBaseInfo battleBaseInfo = new BattleBaseInfo();
		battleBaseInfo.publicBaseInfo = new PublicBaseInfo();
		battleBaseInfo.publicBaseInfo.simpleInfo = new SimpleBaseInfo();
		battleBaseInfo.battleBaseAttr = new BattleBaseAttr();
		battleBaseInfo.wrapType = GameObjectType.ENUM.Role;
		battleBaseInfo.camp = 1;
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
		int num = instanceData.actionPoint + SkillDataManager.Instance.GetSkillProInitActPoint() + EntityWorld.Instance.EntSelf.TotalBeginActPoint;
		battleBaseInfo.battleBaseAttr.ActPoint = ((num <= battleBaseInfo.battleBaseAttr.ActPointLmt) ? num : battleBaseInfo.battleBaseAttr.ActPointLmt);
		battleBaseInfo.battleBaseAttr.Hp = (long)((double)battleBaseInfo.publicBaseInfo.HpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.HpLmtMulAmend * 0.001));
		battleBaseInfo.battleBaseAttr.Vp = (int)((double)battleBaseInfo.publicBaseInfo.VpLmt * (1.0 + (double)battleBaseInfo.publicBaseInfo.VpLmtMulAmend * 0.001));
		battleBaseInfo.ownedListIdx = 0;
		List<PetInfo> list;
		if (trialPetInfos == null)
		{
			list = PetManager.Instance.GetPetBattleInfo(instanceType);
		}
		else
		{
			list = trialPetInfos;
		}
		int num2 = 0;
		for (int i = 0; i < list.get_Count(); i++)
		{
			if (DataReader<Pet>.Get(list.get_Item(i).petId) != null)
			{
				battleBaseInfo.ownedIds.Add(list.get_Item(i).id);
				BattleSkillInfo summonSkillInfo = PetManager.Instance.GetSummonSkillInfo(list.get_Item(i), num2);
				if (summonSkillInfo != null)
				{
					battleBaseInfo.skills.Add(summonSkillInfo);
				}
				num2++;
			}
		}
		battleBaseInfo.finalOwnerId = 0L;
		for (int j = 0; j < instanceData.beginSkill.get_Count(); j++)
		{
			battleBaseInfo.skills.Add(new BattleSkillInfo
			{
				skillId = instanceData.beginSkill.get_Item(j),
				skillIdx = 0,
				skillLv = 1
			});
		}
		SkillDataManager.Instance.ConstructBattleSkillInfo(battleBaseInfo.skills, trialSkillInfos, battleBaseInfo.skillExs);
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
