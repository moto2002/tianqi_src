using GameData;
using Package;
using System;
using System.Collections.Generic;

public class BattleDmgCollectManager
{
	private static BattleDmgCollectManager m_Instance;

	private List<ChallengeResult.SoldierSettleInfo> m_soldierSettleInfos = new List<ChallengeResult.SoldierSettleInfo>();

	private Dictionary<long, ChallengeResult.SoldierSettleInfo> dicSoldierSettleInfo = new Dictionary<long, ChallengeResult.SoldierSettleInfo>();

	private Dictionary<long, DamageCalModel> dicActiveModeDataCampSelf = new Dictionary<long, DamageCalModel>();

	private Dictionary<long, DamageCalModel> dicActiveModeDataCampEnemy = new Dictionary<long, DamageCalModel>();

	private Dictionary<long, DamageCalModel> dicInActiveModeDataCampSelf = new Dictionary<long, DamageCalModel>();

	private Dictionary<long, DamageCalModel> dicInActiveModeDataCampEnemy = new Dictionary<long, DamageCalModel>();

	public List<DamageCalModel> listActiveModeDataCampSelf = new List<DamageCalModel>();

	public List<DamageCalModel> listActiveModeDataCampEnemy = new List<DamageCalModel>();

	public List<DamageCalModel> listInActiveModeDataCampSelf = new List<DamageCalModel>();

	public List<DamageCalModel> listInActiveModeDataCampEnemy = new List<DamageCalModel>();

	private int skillIconNormalId = 3619;

	private string skillNameNormal = "技能";

	public long campSelfTotalActive;

	public long campEnemyTotalActive;

	public long campSelfTotalInActive;

	public long campEnemyTotalInActive;

	public List<ChallengeResult.SoldierSettleInfo> localSoldierSettleInfos = new List<ChallengeResult.SoldierSettleInfo>();

	private Dictionary<long, ChallengeResult.SoldierSettleInfo> localDicSoldierSettleInfo = new Dictionary<long, ChallengeResult.SoldierSettleInfo>();

	public static BattleDmgCollectManager Instance
	{
		get
		{
			if (BattleDmgCollectManager.m_Instance == null)
			{
				BattleDmgCollectManager.m_Instance = new BattleDmgCollectManager();
			}
			return BattleDmgCollectManager.m_Instance;
		}
	}

	public void ClearData()
	{
		this.campSelfTotalActive = 0L;
		this.campEnemyTotalActive = 0L;
		this.campSelfTotalInActive = 0L;
		this.campEnemyTotalInActive = 0L;
		this.dicSoldierSettleInfo.Clear();
		this.m_soldierSettleInfos.Clear();
		this.dicActiveModeDataCampSelf.Clear();
		this.dicActiveModeDataCampEnemy.Clear();
		this.dicInActiveModeDataCampSelf.Clear();
		this.dicInActiveModeDataCampEnemy.Clear();
		this.listActiveModeDataCampSelf.Clear();
		this.listActiveModeDataCampEnemy.Clear();
		this.listInActiveModeDataCampSelf.Clear();
		this.listInActiveModeDataCampEnemy.Clear();
		this.localSoldierSettleInfos.Clear();
		this.localDicSoldierSettleInfo.Clear();
	}

	public void SetDmgCalByChallengeResult(List<ChallengeResult.SoldierSettleInfo> soldierSettleInfos)
	{
		this.DebugLog(soldierSettleInfos);
		if (soldierSettleInfos == null)
		{
			Debuger.Error("soldierSettleInfos == null", new object[0]);
			return;
		}
		if (soldierSettleInfos.get_Count() == 0)
		{
			Debuger.Error("soldierSettleInfos.Count == 0", new object[0]);
			return;
		}
		this.m_soldierSettleInfos = soldierSettleInfos;
		ChallengeResult.SoldierSettleInfo soldierSettleInfo = null;
		for (int i = 0; i < soldierSettleInfos.get_Count(); i++)
		{
			ChallengeResult.SoldierSettleInfo soldierSettleInfo2 = soldierSettleInfos.get_Item(i);
			Debuger.Error(string.Concat(new object[]
			{
				"ssi.soldierId  ",
				soldierSettleInfo2.soldierId,
				"  EntityWorld.Instance.EntSelf.ID  ",
				EntityWorld.Instance.EntSelf.ID
			}), new object[0]);
			if (soldierSettleInfo2.soldierId == EntityWorld.Instance.EntSelf.ID)
			{
				Debuger.Error("ssi.soldierId == EntityWorld.Instance.EntSelf.ID", new object[0]);
				soldierSettleInfo = soldierSettleInfo2;
				break;
			}
		}
		if (soldierSettleInfo == null)
		{
			Debuger.Error("ssiSelf == null EntityWorld.Instance.EntSelf.ID  " + EntityWorld.Instance.EntSelf.ID, new object[0]);
			return;
		}
		CampType.ENUM camp = soldierSettleInfo.camp;
		for (int j = 0; j < soldierSettleInfos.get_Count(); j++)
		{
			ChallengeResult.SoldierSettleInfo soldierSettleInfo3 = soldierSettleInfos.get_Item(j);
			if (!this.dicSoldierSettleInfo.ContainsKey(soldierSettleInfo3.soldierId))
			{
				this.dicSoldierSettleInfo.Add(soldierSettleInfo3.soldierId, soldierSettleInfo3);
			}
		}
		for (int k = 0; k < soldierSettleInfos.get_Count(); k++)
		{
			ChallengeResult.SoldierSettleInfo soldierSettleInfo4 = soldierSettleInfos.get_Item(k);
			if (soldierSettleInfo4.dmgTreatRcds != null)
			{
				for (int l = 0; l < 2; l++)
				{
					if (l == 0)
					{
						DamageCalModel damageCalModel;
						if (soldierSettleInfo4.camp == camp)
						{
							GameObjectType.ENUM wrapType = soldierSettleInfo4.wrapType;
							if (wrapType != GameObjectType.ENUM.Monster)
							{
								if (!this.dicActiveModeDataCampSelf.ContainsKey(soldierSettleInfo4.soldierId))
								{
									this.dicActiveModeDataCampSelf.Add(soldierSettleInfo4.soldierId, new DamageCalModel(soldierSettleInfo4.soldierId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicActiveModeDataCampSelf.get_Item(soldierSettleInfo4.soldierId);
								damageCalModel.amount = 1L;
							}
							else
							{
								if (!this.dicActiveModeDataCampSelf.ContainsKey((long)soldierSettleInfo4.soldierTypeId))
								{
									this.dicActiveModeDataCampSelf.Add((long)soldierSettleInfo4.soldierTypeId, new DamageCalModel((long)soldierSettleInfo4.soldierTypeId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicActiveModeDataCampSelf.get_Item((long)soldierSettleInfo4.soldierTypeId);
								damageCalModel.amount += 1L;
							}
						}
						else
						{
							GameObjectType.ENUM wrapType = soldierSettleInfo4.wrapType;
							if (wrapType != GameObjectType.ENUM.Monster)
							{
								if (!this.dicActiveModeDataCampEnemy.ContainsKey(soldierSettleInfo4.soldierId))
								{
									this.dicActiveModeDataCampEnemy.Add(soldierSettleInfo4.soldierId, new DamageCalModel(soldierSettleInfo4.soldierId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicActiveModeDataCampEnemy.get_Item(soldierSettleInfo4.soldierId);
								damageCalModel.amount = 1L;
							}
							else
							{
								if (!this.dicActiveModeDataCampEnemy.ContainsKey((long)soldierSettleInfo4.soldierTypeId))
								{
									this.dicActiveModeDataCampEnemy.Add((long)soldierSettleInfo4.soldierTypeId, new DamageCalModel((long)soldierSettleInfo4.soldierTypeId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicActiveModeDataCampEnemy.get_Item((long)soldierSettleInfo4.soldierTypeId);
								damageCalModel.amount += 1L;
							}
						}
						KeyValuePair<string, int> name = this.GetName(soldierSettleInfo4);
						damageCalModel.name = name.get_Key();
						damageCalModel.iconId = name.get_Value();
						for (int m = 0; m < soldierSettleInfo4.dmgTreatRcds.actives.get_Count(); m++)
						{
							BattleDmgTreatRcd battleDmgTreatRcd = soldierSettleInfo4.dmgTreatRcds.actives.get_Item(m);
							int skillID = 0;
							if (battleDmgTreatRcd.fitPetTypeId != 0)
							{
								skillID = DataReader<Pet>.Get(battleDmgTreatRcd.fitPetTypeId).fuseNeedSkill;
							}
							else
							{
								skillID = battleDmgTreatRcd.skillId;
							}
							DamageCalModel damageCalModel2 = damageCalModel.listChildren.Find((DamageCalModel a) => a.id == (long)skillID);
							if (damageCalModel2 == null)
							{
								damageCalModel2 = new DamageCalModel((long)skillID, GameObjectType.ENUM.OtherType);
								damageCalModel.listChildren.Add(damageCalModel2);
								Skill skill = DataReader<Skill>.Get(skillID);
								if (skill == null)
								{
									damageCalModel2.name = this.skillNameNormal + skillID;
									damageCalModel2.iconId = this.skillIconNormalId;
								}
								else
								{
									damageCalModel2.name = GameDataUtils.GetChineseContent(skill.name, false);
									damageCalModel2.iconId = ((skill.icon != 0) ? skill.icon : this.skillIconNormalId);
								}
								if (damageCalModel2.name.get_Length() == 0)
								{
									damageCalModel2.name = this.skillNameNormal + skillID;
								}
								damageCalModel2.amount = 1L;
							}
							ChallengeResult.SoldierSettleInfo targetSsi = this.dicSoldierSettleInfo.get_Item(battleDmgTreatRcd.targetId);
							DamageCalModel damageCalModel3;
							if (targetSsi.wrapType == GameObjectType.ENUM.Monster)
							{
								damageCalModel3 = damageCalModel2.listChildren.Find((DamageCalModel a) => a.id == (long)targetSsi.soldierTypeId);
								if (damageCalModel3 == null)
								{
									damageCalModel3 = new DamageCalModel((long)targetSsi.soldierTypeId, targetSsi.wrapType);
									damageCalModel2.listChildren.Add(damageCalModel3);
									KeyValuePair<string, int> name2 = this.GetName(targetSsi);
									damageCalModel3.name = name2.get_Key();
									damageCalModel3.iconId = name2.get_Value();
								}
								damageCalModel3.amount += 1L;
							}
							else
							{
								damageCalModel3 = damageCalModel2.listChildren.Find((DamageCalModel a) => a.id == targetSsi.soldierId);
								if (damageCalModel3 == null)
								{
									damageCalModel3 = new DamageCalModel(targetSsi.soldierId, targetSsi.wrapType);
									damageCalModel2.listChildren.Add(damageCalModel3);
									damageCalModel3.amount = 1L;
									KeyValuePair<string, int> name3 = this.GetName(targetSsi);
									damageCalModel3.name = name3.get_Key();
									damageCalModel3.iconId = name3.get_Value();
								}
							}
							if (battleDmgTreatRcd.type == BattleDmgTreatRcd.ENUM.Dmg)
							{
								damageCalModel.damage += battleDmgTreatRcd.val;
								damageCalModel2.damage += battleDmgTreatRcd.val;
								damageCalModel3.damage += battleDmgTreatRcd.val;
							}
							else
							{
								damageCalModel.heal += battleDmgTreatRcd.val;
								damageCalModel2.heal += battleDmgTreatRcd.val;
								damageCalModel3.heal += battleDmgTreatRcd.val;
							}
							damageCalModel.total += battleDmgTreatRcd.val;
							damageCalModel2.total += battleDmgTreatRcd.val;
							damageCalModel3.total += battleDmgTreatRcd.val;
							if (soldierSettleInfo4.camp == camp)
							{
								this.campSelfTotalActive += battleDmgTreatRcd.val;
							}
							else
							{
								this.campEnemyTotalActive += battleDmgTreatRcd.val;
							}
						}
					}
					else
					{
						DamageCalModel damageCalModel;
						if (soldierSettleInfo4.camp == camp)
						{
							GameObjectType.ENUM wrapType = soldierSettleInfo4.wrapType;
							if (wrapType != GameObjectType.ENUM.Monster)
							{
								if (!this.dicInActiveModeDataCampSelf.ContainsKey(soldierSettleInfo4.soldierId))
								{
									this.dicInActiveModeDataCampSelf.Add(soldierSettleInfo4.soldierId, new DamageCalModel(soldierSettleInfo4.soldierId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicInActiveModeDataCampSelf.get_Item(soldierSettleInfo4.soldierId);
								damageCalModel.amount = 1L;
							}
							else
							{
								if (!this.dicInActiveModeDataCampSelf.ContainsKey((long)soldierSettleInfo4.soldierTypeId))
								{
									this.dicInActiveModeDataCampSelf.Add((long)soldierSettleInfo4.soldierTypeId, new DamageCalModel((long)soldierSettleInfo4.soldierTypeId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicInActiveModeDataCampSelf.get_Item((long)soldierSettleInfo4.soldierTypeId);
								damageCalModel.amount += 1L;
							}
						}
						else
						{
							GameObjectType.ENUM wrapType = soldierSettleInfo4.wrapType;
							if (wrapType != GameObjectType.ENUM.Monster)
							{
								if (!this.dicInActiveModeDataCampEnemy.ContainsKey(soldierSettleInfo4.soldierId))
								{
									this.dicInActiveModeDataCampEnemy.Add(soldierSettleInfo4.soldierId, new DamageCalModel(soldierSettleInfo4.soldierId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicInActiveModeDataCampEnemy.get_Item(soldierSettleInfo4.soldierId);
								damageCalModel.amount = 1L;
							}
							else
							{
								if (!this.dicInActiveModeDataCampEnemy.ContainsKey((long)soldierSettleInfo4.soldierTypeId))
								{
									this.dicInActiveModeDataCampEnemy.Add((long)soldierSettleInfo4.soldierTypeId, new DamageCalModel((long)soldierSettleInfo4.soldierTypeId, soldierSettleInfo4.wrapType));
								}
								damageCalModel = this.dicInActiveModeDataCampEnemy.get_Item((long)soldierSettleInfo4.soldierTypeId);
								damageCalModel.amount += 1L;
							}
						}
						KeyValuePair<string, int> name4 = this.GetName(soldierSettleInfo4);
						damageCalModel.name = name4.get_Key();
						damageCalModel.iconId = name4.get_Value();
						for (int n = 0; n < soldierSettleInfo4.dmgTreatRcds.inActives.get_Count(); n++)
						{
							BattleDmgTreatRcd battleDmgTreatRcd2 = soldierSettleInfo4.dmgTreatRcds.inActives.get_Item(n);
							ChallengeResult.SoldierSettleInfo targetSsi = this.dicSoldierSettleInfo.get_Item(battleDmgTreatRcd2.targetId);
							DamageCalModel damageCalModel4;
							if (targetSsi.wrapType == GameObjectType.ENUM.Monster)
							{
								damageCalModel4 = damageCalModel.listChildren.Find((DamageCalModel a) => a.id == (long)targetSsi.soldierTypeId);
								if (damageCalModel4 == null)
								{
									damageCalModel4 = new DamageCalModel((long)targetSsi.soldierTypeId, targetSsi.wrapType);
									damageCalModel.listChildren.Add(damageCalModel4);
									KeyValuePair<string, int> name5 = this.GetName(targetSsi);
									damageCalModel4.name = name5.get_Key();
									damageCalModel4.iconId = name5.get_Value();
								}
								damageCalModel4.amount += 1L;
							}
							else
							{
								damageCalModel4 = damageCalModel.listChildren.Find((DamageCalModel a) => a.id == targetSsi.soldierId);
								if (damageCalModel4 == null)
								{
									damageCalModel4 = new DamageCalModel(targetSsi.soldierId, targetSsi.wrapType);
									damageCalModel.listChildren.Add(damageCalModel4);
									damageCalModel4.amount = 1L;
									KeyValuePair<string, int> name6 = this.GetName(targetSsi);
									damageCalModel4.name = name6.get_Key();
									damageCalModel4.iconId = name6.get_Value();
								}
							}
							int skillID = 0;
							if (battleDmgTreatRcd2.fitPetTypeId != 0)
							{
								skillID = DataReader<Pet>.Get(battleDmgTreatRcd2.fitPetTypeId).fuseNeedSkill;
							}
							else
							{
								skillID = battleDmgTreatRcd2.skillId;
							}
							DamageCalModel damageCalModel5 = damageCalModel4.listChildren.Find((DamageCalModel a) => a.id == (long)skillID);
							if (damageCalModel5 == null)
							{
								damageCalModel5 = new DamageCalModel((long)skillID, GameObjectType.ENUM.OtherType);
								damageCalModel4.listChildren.Add(damageCalModel5);
								Skill skill2 = DataReader<Skill>.Get(skillID);
								if (skill2 == null)
								{
									damageCalModel5.name = this.skillNameNormal + skillID;
									damageCalModel5.iconId = this.skillIconNormalId;
								}
								else
								{
									damageCalModel5.name = GameDataUtils.GetChineseContent(skill2.name, false);
									damageCalModel5.iconId = ((skill2.icon != 0) ? skill2.icon : this.skillIconNormalId);
								}
							}
							if (damageCalModel5.name.get_Length() == 0)
							{
								damageCalModel5.name = this.skillNameNormal + skillID;
							}
							damageCalModel5.amount = 1L;
							if (battleDmgTreatRcd2.type == BattleDmgTreatRcd.ENUM.Dmg)
							{
								damageCalModel.damage += battleDmgTreatRcd2.val;
								damageCalModel4.damage += battleDmgTreatRcd2.val;
								damageCalModel5.damage += battleDmgTreatRcd2.val;
							}
							else
							{
								damageCalModel.heal += battleDmgTreatRcd2.val;
								damageCalModel4.heal += battleDmgTreatRcd2.val;
								damageCalModel5.heal += battleDmgTreatRcd2.val;
							}
							damageCalModel.total += battleDmgTreatRcd2.val;
							damageCalModel4.total += battleDmgTreatRcd2.val;
							damageCalModel5.total += battleDmgTreatRcd2.val;
							if (soldierSettleInfo4.camp == camp)
							{
								this.campSelfTotalInActive += battleDmgTreatRcd2.val;
							}
							else
							{
								this.campEnemyTotalInActive += battleDmgTreatRcd2.val;
							}
						}
					}
				}
			}
		}
		using (Dictionary<long, DamageCalModel>.Enumerator enumerator = this.dicActiveModeDataCampSelf.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, DamageCalModel> current = enumerator.get_Current();
				DamageCalModel value = current.get_Value();
				if (value.total != 0L)
				{
					this.listActiveModeDataCampSelf.Add(value);
				}
			}
		}
		using (Dictionary<long, DamageCalModel>.Enumerator enumerator2 = this.dicActiveModeDataCampEnemy.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<long, DamageCalModel> current2 = enumerator2.get_Current();
				DamageCalModel value2 = current2.get_Value();
				if (value2.total != 0L)
				{
					this.listActiveModeDataCampEnemy.Add(value2);
				}
			}
		}
		using (Dictionary<long, DamageCalModel>.Enumerator enumerator3 = this.dicInActiveModeDataCampSelf.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<long, DamageCalModel> current3 = enumerator3.get_Current();
				DamageCalModel value3 = current3.get_Value();
				if (value3.total != 0L)
				{
					this.listInActiveModeDataCampSelf.Add(value3);
				}
			}
		}
		using (Dictionary<long, DamageCalModel>.Enumerator enumerator4 = this.dicInActiveModeDataCampEnemy.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				KeyValuePair<long, DamageCalModel> current4 = enumerator4.get_Current();
				DamageCalModel value4 = current4.get_Value();
				if (value4.total != 0L)
				{
					this.listInActiveModeDataCampEnemy.Add(value4);
				}
			}
		}
		this.SortDamageCalModelLv1(this.listActiveModeDataCampSelf);
		this.SortDamageCalModelLv1(this.listActiveModeDataCampEnemy);
		this.SortDamageCalModelLv1(this.listInActiveModeDataCampSelf);
		this.SortDamageCalModelLv1(this.listInActiveModeDataCampEnemy);
		for (int num = 0; num < this.listActiveModeDataCampSelf.get_Count(); num++)
		{
			Debuger.Error("1----  " + this.listActiveModeDataCampSelf.get_Item(num).total, new object[0]);
		}
		this.SortDamageCalModelLv2(this.listActiveModeDataCampSelf);
		this.SortDamageCalModelLv2(this.listActiveModeDataCampEnemy);
		this.SortDamageCalModelLv2(this.listInActiveModeDataCampSelf);
		this.SortDamageCalModelLv2(this.listInActiveModeDataCampEnemy);
		this.SortDamageCalModelLv3(this.listActiveModeDataCampSelf);
		this.SortDamageCalModelLv3(this.listActiveModeDataCampEnemy);
		this.SortDamageCalModelLv3(this.listInActiveModeDataCampSelf);
		this.SortDamageCalModelLv3(this.listInActiveModeDataCampEnemy);
	}

	private void SortDamageCalModelLv3(List<DamageCalModel> list)
	{
		for (int i = 0; i < list.get_Count(); i++)
		{
			DamageCalModel damageCalModel = list.get_Item(i);
			for (int j = 0; j < damageCalModel.listChildren.get_Count() - 1; j++)
			{
				DamageCalModel damageCalModel2 = damageCalModel.listChildren.get_Item(j);
				for (int k = 0; k < damageCalModel2.listChildren.get_Count(); k++)
				{
					int num = k;
					for (int l = k + 1; l < damageCalModel2.listChildren.get_Count(); l++)
					{
						DamageCalModel damageCalModel3 = damageCalModel2.listChildren.get_Item(num);
						DamageCalModel damageCalModel4 = damageCalModel2.listChildren.get_Item(l);
						if (damageCalModel3.total < damageCalModel4.total)
						{
							num = l;
						}
					}
					if (num != k)
					{
						XUtility.ListExchange<DamageCalModel>(damageCalModel2.listChildren, num, k);
					}
				}
			}
		}
	}

	private void SortDamageCalModelLv2(List<DamageCalModel> list)
	{
		for (int i = 0; i < list.get_Count(); i++)
		{
			DamageCalModel damageCalModel = list.get_Item(i);
			for (int j = 0; j < damageCalModel.listChildren.get_Count() - 1; j++)
			{
				int num = j;
				for (int k = j + 1; k < damageCalModel.listChildren.get_Count(); k++)
				{
					DamageCalModel damageCalModel2 = damageCalModel.listChildren.get_Item(num);
					DamageCalModel damageCalModel3 = damageCalModel.listChildren.get_Item(k);
					if (damageCalModel2.total < damageCalModel3.total)
					{
						num = k;
					}
				}
				if (num != j)
				{
					XUtility.ListExchange<DamageCalModel>(damageCalModel.listChildren, num, j);
				}
			}
		}
	}

	private void SortDamageCalModelLv1(List<DamageCalModel> list)
	{
		for (int i = 0; i < list.get_Count() - 1; i++)
		{
			int num = i;
			for (int j = i + 1; j < list.get_Count(); j++)
			{
				DamageCalModel damageCalModel = list.get_Item(num);
				DamageCalModel damageCalModel2 = list.get_Item(j);
				if (damageCalModel.total < damageCalModel2.total)
				{
					num = j;
				}
			}
			if (i != num)
			{
				XUtility.ListExchange<DamageCalModel>(list, i, num);
			}
		}
	}

	private KeyValuePair<string, int> GetName(ChallengeResult.SoldierSettleInfo ssi)
	{
		string text = string.Empty;
		int num = 0;
		if (ssi.wrapType == GameObjectType.ENUM.Monster)
		{
			ChallengeResult.SoldierSettleInfo soldierSettleInfo = null;
			if (this.dicSoldierSettleInfo.ContainsKey(ssi.ownerId))
			{
				soldierSettleInfo = this.dicSoldierSettleInfo.get_Item(ssi.ownerId);
			}
			Monster monster = DataReader<Monster>.Get(ssi.soldierTypeId);
			if (monster != null)
			{
				num = DataReader<AvatarModel>.Get(monster.model).icon;
			}
			else
			{
				this.DebugNull("Monster", ssi.soldierTypeId.ToString());
			}
			if (soldierSettleInfo != null)
			{
				if (monster != null)
				{
					if (soldierSettleInfo.soldierName != null && soldierSettleInfo.soldierName.get_Length() > 0)
					{
						text = soldierSettleInfo.soldierName + GameDataUtils.GetChineseContent(505063, false) + GameDataUtils.GetChineseContent(monster.name, false);
					}
					else
					{
						text = GameDataUtils.GetChineseContent(monster.name, false);
					}
				}
				else
				{
					this.DebugNull("Monster", ssi.soldierTypeId.ToString());
				}
			}
			else if (monster != null)
			{
				text = GameDataUtils.GetChineseContent(monster.name, false);
			}
			else
			{
				this.DebugNull("Monster", ssi.soldierTypeId.ToString());
			}
		}
		else if (ssi.wrapType == GameObjectType.ENUM.Pet)
		{
			ChallengeResult.SoldierSettleInfo soldierSettleInfo2 = null;
			if (this.dicSoldierSettleInfo.ContainsKey(ssi.ownerId))
			{
				soldierSettleInfo2 = this.dicSoldierSettleInfo.get_Item(ssi.ownerId);
			}
			Pet pet = DataReader<Pet>.Get(ssi.soldierTypeId);
			if (pet != null)
			{
				num = DataReader<AvatarModel>.Get(PetManagerBase.GetPlayerPetModel(pet, 0)).icon;
			}
			else
			{
				this.DebugNull("Pet", ssi.soldierTypeId.ToString());
			}
			if (soldierSettleInfo2 != null)
			{
				if (pet != null)
				{
					if (soldierSettleInfo2.soldierName != null && soldierSettleInfo2.soldierName.get_Length() > 0)
					{
						text = soldierSettleInfo2.soldierName + GameDataUtils.GetChineseContent(505063, false) + GameDataUtils.GetChineseContent(pet.name, false);
					}
					else
					{
						text = GameDataUtils.GetChineseContent(pet.name, false);
					}
				}
				else
				{
					this.DebugNull("Pet", ssi.soldierTypeId.ToString());
				}
			}
			else if (pet != null)
			{
				text = GameDataUtils.GetChineseContent(pet.name, false);
			}
			else
			{
				this.DebugNull("Pet", ssi.soldierTypeId.ToString());
			}
		}
		else if (ssi.wrapType == GameObjectType.ENUM.Role)
		{
			text = ssi.soldierName;
			num = DataReader<AvatarModel>.Get(DataReader<RoleCreate>.Get(ssi.soldierTypeId).modle).icon;
		}
		KeyValuePair<string, int> result = new KeyValuePair<string, int>(text, num);
		return result;
	}

	public void CollectDamageHeal(GameObjectType.ENUM wrapTypeActive, CampType.ENUM campActive, bool isBossActive, long soldierIdActive, long ownerIdActive, int soldierTypeIdActive, string soldierNameActive, GameObjectType.ENUM wrapTypeInActive, CampType.ENUM campInActive, bool isBossInActive, long soldierIdInActive, long ownerIdInActive, int soldierTypeIdInActive, string soldierNameInActive, BattleDmgTreatRcd.ENUM battleDmgTreatRcdType, long targetId, long targetOwnerID, int skillId, int fitPetID, bool isFit, long battleDmgTreatRcdValue)
	{
		ChallengeResult.SoldierSettleInfo soldierSettleInfo;
		if (!this.localDicSoldierSettleInfo.ContainsKey(soldierIdActive))
		{
			soldierSettleInfo = new ChallengeResult.SoldierSettleInfo();
			this.localSoldierSettleInfos.Add(soldierSettleInfo);
			this.localDicSoldierSettleInfo.Add(soldierIdActive, soldierSettleInfo);
			soldierSettleInfo.dmgTreatRcds = new BattleDmgTreatRcdsCustom();
			soldierSettleInfo.camp = campActive;
			soldierSettleInfo.isBoss = isBossActive;
			soldierSettleInfo.ownerId = ownerIdActive;
			soldierSettleInfo.soldierId = soldierIdActive;
			soldierSettleInfo.soldierName = soldierNameActive;
			soldierSettleInfo.soldierTypeId = soldierTypeIdActive;
			soldierSettleInfo.wrapType = wrapTypeActive;
		}
		soldierSettleInfo = this.localDicSoldierSettleInfo.get_Item(soldierIdActive);
		string text = soldierIdInActive.ToString() + skillId.ToString() + fitPetID.ToString();
		BattleDmgTreatRcdsCustom battleDmgTreatRcdsCustom = (BattleDmgTreatRcdsCustom)soldierSettleInfo.dmgTreatRcds;
		BattleDmgTreatRcd battleDmgTreatRcd;
		if (!battleDmgTreatRcdsCustom.dicBattleDmgTreatRcds.ContainsKey(text))
		{
			battleDmgTreatRcd = new BattleDmgTreatRcd();
			battleDmgTreatRcdsCustom.dicBattleDmgTreatRcds.Add(text, battleDmgTreatRcd);
			battleDmgTreatRcdsCustom.actives.Add(battleDmgTreatRcd);
			battleDmgTreatRcd.fitPetTypeId = fitPetID;
			battleDmgTreatRcd.isActive = true;
			battleDmgTreatRcd.ownerId = ownerIdActive;
			battleDmgTreatRcd.skillId = skillId;
			battleDmgTreatRcd.targetId = soldierIdInActive;
			battleDmgTreatRcd.type = battleDmgTreatRcdType;
		}
		battleDmgTreatRcd = battleDmgTreatRcdsCustom.dicBattleDmgTreatRcds.get_Item(text);
		battleDmgTreatRcd.val += battleDmgTreatRcdValue;
		ChallengeResult.SoldierSettleInfo soldierSettleInfo2;
		if (!this.localDicSoldierSettleInfo.ContainsKey(soldierIdInActive))
		{
			soldierSettleInfo2 = new ChallengeResult.SoldierSettleInfo();
			this.localSoldierSettleInfos.Add(soldierSettleInfo2);
			this.localDicSoldierSettleInfo.Add(soldierIdInActive, soldierSettleInfo2);
			soldierSettleInfo2.dmgTreatRcds = new BattleDmgTreatRcdsCustom();
			soldierSettleInfo2.camp = campInActive;
			soldierSettleInfo2.isBoss = isBossInActive;
			soldierSettleInfo2.ownerId = ownerIdInActive;
			soldierSettleInfo2.soldierId = soldierIdInActive;
			soldierSettleInfo2.soldierName = soldierNameInActive;
			soldierSettleInfo2.soldierTypeId = soldierTypeIdInActive;
			soldierSettleInfo2.wrapType = wrapTypeInActive;
		}
		soldierSettleInfo2 = this.localDicSoldierSettleInfo.get_Item(soldierIdInActive);
		string text2 = soldierIdActive.ToString() + skillId.ToString() + fitPetID.ToString();
		BattleDmgTreatRcdsCustom battleDmgTreatRcdsCustom2 = (BattleDmgTreatRcdsCustom)soldierSettleInfo2.dmgTreatRcds;
		BattleDmgTreatRcd battleDmgTreatRcd2;
		if (!battleDmgTreatRcdsCustom2.dicBattleDmgTreatRcds.ContainsKey(text2))
		{
			battleDmgTreatRcd2 = new BattleDmgTreatRcd();
			battleDmgTreatRcdsCustom2.dicBattleDmgTreatRcds.Add(text2, battleDmgTreatRcd2);
			battleDmgTreatRcdsCustom2.inActives.Add(battleDmgTreatRcd2);
			battleDmgTreatRcd2.fitPetTypeId = fitPetID;
			battleDmgTreatRcd2.isActive = false;
			battleDmgTreatRcd2.ownerId = ownerIdInActive;
			battleDmgTreatRcd2.skillId = skillId;
			battleDmgTreatRcd2.targetId = soldierIdActive;
			battleDmgTreatRcd2.type = battleDmgTreatRcdType;
		}
		battleDmgTreatRcd2 = battleDmgTreatRcdsCustom2.dicBattleDmgTreatRcds.get_Item(text2);
		battleDmgTreatRcd2.val += battleDmgTreatRcdValue;
	}

	private void DebugLog(List<ChallengeResult.SoldierSettleInfo> soldierSettleInfos)
	{
	}

	private void DebugNull(string typeStr, string idStr)
	{
		Debuger.Error("表 [" + typeStr + " ]没有id =  " + idStr, new object[0]);
	}
}
