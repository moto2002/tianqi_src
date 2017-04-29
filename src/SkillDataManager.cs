using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class SkillDataManager : BaseSubSystemManager
{
	protected static SkillDataManager instance;

	protected List<BattleSkillInfo> staticSkills = new List<BattleSkillInfo>();

	public static SkillDataManager Instance
	{
		get
		{
			if (SkillDataManager.instance == null)
			{
				SkillDataManager.instance = new SkillDataManager();
			}
			return SkillDataManager.instance;
		}
	}

	protected SkillDataManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.staticSkills.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<PushFixedSkill>(new NetCallBackMethod<PushFixedSkill>(this.OnPushFixedSkill));
	}

	protected void OnPushFixedSkill(short state, PushFixedSkill down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.SetFixedSkill(down.skills);
	}

	public void SetFixedSkill(List<BattleSkillInfo> skills)
	{
		this.staticSkills.Clear();
		this.staticSkills.AddRange(skills);
		EntityWorld.Instance.EntSelf.RefreshStaticSkills(skills);
	}

	public void ConstructBattleSkillInfo(List<BattleSkillInfo> battleSkillInfos, List<BattleSkillInfo> appointedBattleSkillInfos, List<BattleSkillExtend> battleSkillExtends)
	{
		XDict<int, BattleSkillInfo> originBattleSkillInfos = this.GetOriginBattleSkillInfos(battleSkillInfos, appointedBattleSkillInfos);
		XDict<int, BattleSkillExtend> xDict = new XDict<int, BattleSkillExtend>();
		this.FixBattleSkillIndfoAndExtendBySkillProData(this.GetSkillProDataID(), originBattleSkillInfos, xDict);
		this.FixBattleSkillInfoByProPropertyData(this.GetSkillProPropertyDataID(), originBattleSkillInfos);
		battleSkillInfos.Clear();
		battleSkillInfos.AddRange(originBattleSkillInfos.Values);
		battleSkillExtends.Clear();
		battleSkillExtends.AddRange(xDict.Values);
	}

	protected XDict<int, BattleSkillInfo> GetOriginBattleSkillInfos(List<BattleSkillInfo> battleSkillInfos, List<BattleSkillInfo> appointedBattleSkillInfos)
	{
		XDict<int, BattleSkillInfo> xDict = new XDict<int, BattleSkillInfo>();
		for (int i = 0; i < battleSkillInfos.get_Count(); i++)
		{
			if (xDict.ContainsKey(battleSkillInfos.get_Item(i).skillId))
			{
				xDict[battleSkillInfos.get_Item(i).skillId] = battleSkillInfos.get_Item(i);
			}
			else
			{
				xDict.Add(battleSkillInfos.get_Item(i).skillId, battleSkillInfos.get_Item(i));
			}
		}
		if (appointedBattleSkillInfos == null)
		{
			for (int j = 0; j < this.staticSkills.get_Count(); j++)
			{
				if (xDict.ContainsKey(this.staticSkills.get_Item(j).skillId))
				{
					xDict[this.staticSkills.get_Item(j).skillId] = this.staticSkills.get_Item(j);
				}
				else
				{
					xDict.Add(this.staticSkills.get_Item(j).skillId, this.staticSkills.get_Item(j));
				}
			}
			List<SkillUIManager.BattleSkillData> battleSkillData = SkillUIManager.Instance.GetBattleSkillData();
			for (int k = 0; k < battleSkillData.get_Count(); k++)
			{
				if (battleSkillData.get_Item(k).skillId > 0)
				{
					BattleSkillInfo value = new BattleSkillInfo
					{
						skillIdx = 10 + battleSkillData.get_Item(k).notchId,
						skillId = battleSkillData.get_Item(k).skillId,
						skillLv = battleSkillData.get_Item(k).skillLv
					};
					if (xDict.ContainsKey(battleSkillData.get_Item(k).skillId))
					{
						xDict[battleSkillData.get_Item(k).skillId] = value;
					}
					else
					{
						xDict.Add(battleSkillData.get_Item(k).skillId, value);
					}
				}
			}
		}
		else
		{
			for (int l = 0; l < appointedBattleSkillInfos.get_Count(); l++)
			{
				if (xDict.ContainsKey(appointedBattleSkillInfos.get_Item(l).skillId))
				{
					xDict[appointedBattleSkillInfos.get_Item(l).skillId] = appointedBattleSkillInfos.get_Item(l);
				}
				else
				{
					xDict.Add(appointedBattleSkillInfos.get_Item(l).skillId, appointedBattleSkillInfos.get_Item(l));
				}
			}
		}
		return xDict;
	}

	protected List<KeyValuePair<int, int>> GetSkillProDataID()
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		list.AddRange(SkillRuneManager.Instance.GetBattleSkillExtendIDs());
		list.AddRange(SkillUIManager.Instance.GetBattleBeActivitySkillExtendIDs());
		list.AddRange(RankUpChangeManager.Instance.GetSkillExtendIDs());
		list.AddRange(EquipmentManager.Instance.GetEquipSuitBattleSkillExtendIDs());
		return list;
	}

	protected List<int> GetSkillProPropertyDataID()
	{
		List<int> list = new List<int>();
		list.AddRange(SkillRuneManager.Instance.GetBattleSkillDamageIncreaseIDs());
		return list;
	}

	protected void FixBattleSkillIndfoAndExtendBySkillProData(List<KeyValuePair<int, int>> skillProDataIDs, XDict<int, BattleSkillInfo> battleSkillInfos, XDict<int, BattleSkillExtend> battleSkillExtends)
	{
		for (int i = 0; i < skillProDataIDs.get_Count(); i++)
		{
			if (DataReader<skillExtend>.Contains(skillProDataIDs.get_Item(i).get_Key()))
			{
				skillExtend skillExtend = DataReader<skillExtend>.Get(skillProDataIDs.get_Item(i).get_Key());
				int type = skillExtend.type;
				switch (type)
				{
				case 2:
					this.UpdateSkillChange(skillExtend, battleSkillInfos);
					goto IL_AD;
				case 3:
					IL_5B:
					if (type != 10)
					{
						goto IL_AD;
					}
					this.UpdatePassiveSkillID(skillExtend, battleSkillInfos, skillProDataIDs.get_Item(i).get_Value());
					goto IL_AD;
				case 4:
					this.UpdateSkillCDChange(skillExtend, battleSkillExtends);
					goto IL_AD;
				case 5:
					this.UpdateSkillActPointChange(skillExtend, battleSkillExtends);
					goto IL_AD;
				}
				goto IL_5B;
			}
			IL_AD:;
		}
	}

	protected void FixBattleSkillInfoByProPropertyData(List<int> skillProPropertyDataIDs, XDict<int, BattleSkillInfo> battleSkillInfos)
	{
		for (int i = 0; i < skillProPropertyDataIDs.get_Count(); i++)
		{
			if (DataReader<damageIncrease>.Contains(skillProPropertyDataIDs.get_Item(i)))
			{
				damageIncrease damageIncrease = DataReader<damageIncrease>.Get(skillProPropertyDataIDs.get_Item(i));
				int num = -1;
				for (int j = 0; j < battleSkillInfos.Values.get_Count(); j++)
				{
					if (battleSkillInfos.Values.get_Item(j).skillId == damageIncrease.targetSkill)
					{
						if (j < battleSkillInfos.Keys.get_Count())
						{
							num = battleSkillInfos.Keys.get_Item(j);
						}
						break;
					}
				}
				if (num != -1)
				{
					battleSkillInfos[num].attrAdd.Add(new BattleSkillAttrAdd
					{
						attrType = damageIncrease.attrType,
						multiAdd = damageIncrease.Value1,
						addiAdd = damageIncrease.Value2
					});
				}
			}
		}
	}

	protected void UpdateSkillChange(skillExtend skillExtendData, XDict<int, BattleSkillInfo> battleSkillInfos)
	{
		string[] array = skillExtendData.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return;
		}
		int key = int.Parse(array[0]);
		if (!battleSkillInfos.ContainsKey(key))
		{
			return;
		}
		battleSkillInfos[key].skillId = int.Parse(array[1]);
	}

	protected void UpdateSkillCDChange(skillExtend skillExtendData, XDict<int, BattleSkillExtend> battleSkillExtends)
	{
		string[] array = skillExtendData.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return;
		}
		int num = int.Parse(array[0]);
		string text = array[1].Substring(0, 1);
		string text2 = array[1].Substring(1, array[1].get_Length() - 1);
		int num2 = (!(text == "-")) ? int.Parse(text2) : (-int.Parse(text2));
		if (battleSkillExtends.ContainsKey(num))
		{
			battleSkillExtends[num].cdOffset += num2;
		}
		else
		{
			battleSkillExtends.Add(num, new BattleSkillExtend
			{
				skillType = num,
				cdOffset = num2
			});
		}
	}

	protected void UpdateSkillActPointChange(skillExtend skillExtendData, XDict<int, BattleSkillExtend> battleSkillExtends)
	{
		string[] array = skillExtendData.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return;
		}
		int num = int.Parse(array[0]);
		string text = array[1].Substring(0, 1);
		string text2 = array[1].Substring(1, array[1].get_Length() - 1);
		int num2 = (!(text == "-")) ? int.Parse(text2) : (-int.Parse(text2));
		if (battleSkillExtends.ContainsKey(num))
		{
			battleSkillExtends[num].actPointOffset += num2;
		}
		else
		{
			battleSkillExtends.Add(num, new BattleSkillExtend
			{
				skillType = num,
				actPointOffset = num2
			});
		}
	}

	protected void UpdatePassiveSkillID(skillExtend skillExtendData, XDict<int, BattleSkillInfo> battleSkillInfos, int skillLevel)
	{
		int num = (int)float.Parse(skillExtendData.ruleDetail);
		if (battleSkillInfos.ContainsKey(num))
		{
			return;
		}
		battleSkillInfos.Add(num, new BattleSkillInfo
		{
			skillId = num,
			skillIdx = 0,
			skillLv = skillLevel
		});
	}

	public int GetSkillProInitActPoint()
	{
		int num = 0;
		List<KeyValuePair<int, int>> skillProDataID = this.GetSkillProDataID();
		int num2 = 106;
		for (int i = 0; i < skillProDataID.get_Count(); i++)
		{
			if (DataReader<skillExtend>.Contains(skillProDataID.get_Item(i).get_Key()))
			{
				skillExtend skillExtend = DataReader<skillExtend>.Get(skillProDataID.get_Item(i).get_Key());
				if (skillExtend.type == 1)
				{
					int key = int.Parse(GameDataUtils.SplitString4Dot0(skillExtend.ruleDetail));
					if (DataReader<Attrs>.Contains(key))
					{
						Attrs attrs = DataReader<Attrs>.Get(key);
						for (int j = 0; j < attrs.attrs.get_Count(); j++)
						{
							if (attrs.attrs.get_Item(j) == num2)
							{
								if (j < attrs.values.get_Count())
								{
									num += attrs.values.get_Item(j);
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	protected XDict<int, int> GetPetExistTimeChange()
	{
		XDict<int, int> xDict = new XDict<int, int>();
		List<KeyValuePair<int, int>> skillProDataID = this.GetSkillProDataID();
		for (int i = 0; i < skillProDataID.get_Count(); i++)
		{
			if (DataReader<skillExtend>.Contains(skillProDataID.get_Item(i).get_Key()))
			{
				skillExtend skillExtend = DataReader<skillExtend>.Get(skillProDataID.get_Item(i).get_Key());
				if (skillExtend.type == 6)
				{
					string[] array = skillExtend.ruleDetail.Split(new char[]
					{
						','
					});
					if (array.Length >= 2)
					{
						int num = int.Parse(array[0]);
						string text = array[1].Substring(0, 1);
						string text2 = array[1].Substring(1, array[1].get_Length() - 1);
						int num2 = (!(text == "-")) ? int.Parse(text2) : (-int.Parse(text2));
						if (xDict.ContainsKey(num))
						{
							XDict<int, int> xDict2;
							XDict<int, int> expr_E5 = xDict2 = xDict;
							int num3;
							int expr_EA = num3 = num;
							num3 = xDict2[num3];
							expr_E5[expr_EA] = num3 + num2;
						}
						else
						{
							xDict.Add(num, num2);
						}
					}
				}
			}
		}
		return xDict;
	}

	public int GetPetExistTimeChange(Pet petData)
	{
		XDict<int, int> petExistTimeChange = this.GetPetExistTimeChange();
		int key = petData.function + 9;
		return (!petExistTimeChange.ContainsKey(key)) ? 0 : petExistTimeChange[key];
	}
}
