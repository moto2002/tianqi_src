using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class SkillRuneManager : BaseSubSystemManager
{
	public const int RuneGroupNumPerSkill = 4;

	private Dictionary<int, RunedStoneInfo> skillRuneInfoDic;

	private List<RunedStone> unLockRuneStoneList;

	private Dictionary<int, List<int>> newOpenRuneStones;

	private static SkillRuneManager instance;

	public Dictionary<int, RunedStoneInfo> SkillRuneInfoDic
	{
		get
		{
			return this.skillRuneInfoDic;
		}
	}

	public List<RunedStone> UnLockRuneStoneList
	{
		get
		{
			return this.unLockRuneStoneList;
		}
	}

	public Dictionary<int, List<int>> NewOpenRuneStones
	{
		get
		{
			return this.newOpenRuneStones;
		}
	}

	public static SkillRuneManager Instance
	{
		get
		{
			if (SkillRuneManager.instance == null)
			{
				SkillRuneManager.instance = new SkillRuneManager();
			}
			return SkillRuneManager.instance;
		}
	}

	public override void Init()
	{
		this.skillRuneInfoDic = new Dictionary<int, RunedStoneInfo>();
		this.unLockRuneStoneList = new List<RunedStone>();
		this.newOpenRuneStones = new Dictionary<int, List<int>>();
		base.Init();
	}

	public override void Release()
	{
		this.skillRuneInfoDic = null;
		this.unLockRuneStoneList = null;
		this.newOpenRuneStones = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<RunedStoneLoginPush>(new NetCallBackMethod<RunedStoneLoginPush>(this.OnRuneStoneLoginPush));
		NetworkManager.AddListenEvent<RunedStoneChangedNty>(new NetCallBackMethod<RunedStoneChangedNty>(this.OnRuneStoneChangedNty));
		NetworkManager.AddListenEvent<RunedStoneEmbedRes>(new NetCallBackMethod<RunedStoneEmbedRes>(this.OnRunedStoneEmbedRes));
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionReadyChange", new Callback(this.OnRoleSelfProfessionChange));
	}

	private void OnRuneStoneLoginPush(short state, RunedStoneLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.stoneInfos.get_Count(); i++)
			{
				RunedStoneInfo runedStoneInfo = down.stoneInfos.get_Item(i);
				if (!this.skillRuneInfoDic.ContainsKey(runedStoneInfo.skillId))
				{
					this.skillRuneInfoDic.Add(runedStoneInfo.skillId, runedStoneInfo);
				}
				else
				{
					this.skillRuneInfoDic.set_Item(runedStoneInfo.skillId, runedStoneInfo);
				}
			}
			this.UpdateUnLockRuneStoneList();
		}
	}

	private void OnRuneStoneChangedNty(short state, RunedStoneChangedNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.stoneInfos.get_Count(); i++)
			{
				RunedStoneInfo runedStoneInfo = down.stoneInfos.get_Item(i);
				switch (down.changeType)
				{
				case RunedStoneChangedNty.ChangeType.Add:
					if (this.skillRuneInfoDic.ContainsKey(runedStoneInfo.skillId))
					{
						this.skillRuneInfoDic.set_Item(runedStoneInfo.skillId, runedStoneInfo);
					}
					else
					{
						this.skillRuneInfoDic.Add(runedStoneInfo.skillId, runedStoneInfo);
					}
					for (int j = 0; j < runedStoneInfo.runedStones.get_Count(); j++)
					{
						int runeStoneID = runedStoneInfo.runedStones.get_Item(j).cfgId;
						if (this.newOpenRuneStones.ContainsKey(runedStoneInfo.skillId))
						{
							int num = this.newOpenRuneStones.get_Item(runedStoneInfo.skillId).FindIndex((int a) => a == runeStoneID);
							if (num < 0)
							{
								this.newOpenRuneStones.get_Item(runedStoneInfo.skillId).Add(runeStoneID);
							}
						}
						else
						{
							List<int> list = new List<int>();
							list.Add(runeStoneID);
							this.newOpenRuneStones.Add(runedStoneInfo.skillId, list);
						}
					}
					break;
				case RunedStoneChangedNty.ChangeType.Update:
					if (this.skillRuneInfoDic.ContainsKey(runedStoneInfo.skillId))
					{
						this.skillRuneInfoDic.set_Item(runedStoneInfo.skillId, runedStoneInfo);
					}
					break;
				case RunedStoneChangedNty.ChangeType.Remove:
					if (this.skillRuneInfoDic.ContainsKey(runedStoneInfo.skillId))
					{
						this.skillRuneInfoDic.set_Item(runedStoneInfo.skillId, runedStoneInfo);
					}
					break;
				}
			}
			this.UpdateUnLockRuneStoneList();
			EventDispatcher.Broadcast(EventNames.OnRuneStoneChangedNty);
		}
	}

	private void OnRunedStoneEmbedRes(short state, RunedStoneEmbedRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.skillRuneInfoDic.ContainsKey(down.skillId))
		{
			this.skillRuneInfoDic.get_Item(down.skillId).embedGroupId = down.groupId;
			EventDispatcher.Broadcast<int>(EventNames.OnRuneStoneEmbedRes, down.skillId);
		}
	}

	public void SendEmbedRunedStoneReq(int skillID, int runeStoneID)
	{
		Runes_basic runesBasicCfgData = this.GetRunesBasicCfgData(runeStoneID);
		if (runesBasicCfgData != null)
		{
			int runesGroup = runesBasicCfgData.runesGroup;
			if (this.skillRuneInfoDic != null && this.skillRuneInfoDic.ContainsKey(skillID) && this.skillRuneInfoDic.get_Item(skillID).embedGroupId == runesGroup)
			{
				return;
			}
			Debug.Log(string.Concat(new object[]
			{
				"请求符文组镶嵌,技能ID为：",
				skillID,
				"符文组为：",
				runesGroup
			}));
			NetworkManager.Send(new RunedStoneEmbedReq
			{
				skillId = skillID,
				groupId = runesGroup
			}, ServerType.Data);
		}
	}

	public bool CheckHaveUnLockRuneStonBySkillID(int skillID)
	{
		Dictionary<int, List<Runes_basic>> runeInfoDataBySkillID = SkillRuneManager.Instance.GetRuneInfoDataBySkillID(skillID);
		if (runeInfoDataBySkillID != null)
		{
			for (int i = 1; i <= 4; i++)
			{
				if (runeInfoDataBySkillID.ContainsKey(i))
				{
					List<Runes_basic> list = runeInfoDataBySkillID.get_Item(i);
					for (int j = 0; j < list.get_Count(); j++)
					{
						int id = list.get_Item(j).id;
						if (this.CheckRuneStoneIsUnLock(id))
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	public bool CheckRuneStoneIsUnLock(int runeStoneID)
	{
		int num = this.unLockRuneStoneList.FindIndex((RunedStone a) => a.cfgId == runeStoneID);
		return num >= 0;
	}

	public bool CheckRuneStoneCanUpgrade(int runeStoneID)
	{
		bool flag = this.CheckRuneStoneIsUnLock(runeStoneID);
		if (flag)
		{
			int lv = this.GetRuneStoneLV(runeStoneID);
			Runes runes = DataReader<Runes>.DataList.Find((Runes a) => a.id == runeStoneID && a.lv == lv + 1);
			if (runes != null)
			{
				return true;
			}
		}
		return false;
	}

	public Dictionary<int, List<Runes_basic>> GetRuneInfoDataBySkillID(int skillID)
	{
		SkillRuneManager.<GetRuneInfoDataBySkillID>c__AnonStorey199 <GetRuneInfoDataBySkillID>c__AnonStorey = new SkillRuneManager.<GetRuneInfoDataBySkillID>c__AnonStorey199();
		<GetRuneInfoDataBySkillID>c__AnonStorey.skillID = skillID;
		Dictionary<int, List<Runes_basic>> dictionary = new Dictionary<int, List<Runes_basic>>();
		int groupID;
		for (groupID = 1; groupID <= 4; groupID++)
		{
			List<Runes_basic> list = DataReader<Runes_basic>.DataList.FindAll((Runes_basic runes) => runes.skillId == <GetRuneInfoDataBySkillID>c__AnonStorey.skillID && runes.runesGroup == groupID);
			if (!dictionary.ContainsKey(groupID))
			{
				dictionary.Add(groupID, list);
			}
		}
		return dictionary;
	}

	public List<int> GetSkillList()
	{
		List<int> list = new List<int>();
		if (this.skillRuneInfoDic == null)
		{
			return list;
		}
		using (Dictionary<int, RunedStoneInfo>.Enumerator enumerator = this.skillRuneInfoDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, RunedStoneInfo> current = enumerator.get_Current();
				list.Add(current.get_Key());
			}
		}
		return list;
	}

	public int GetRuneStoneLV(int runeStoneID)
	{
		int result = 1;
		if (this.unLockRuneStoneList == null)
		{
			return result;
		}
		int num = this.unLockRuneStoneList.FindIndex((RunedStone a) => a.cfgId == runeStoneID);
		if (num >= 0)
		{
			return this.unLockRuneStoneList.get_Item(num).lv;
		}
		return result;
	}

	public Runes_basic GetRunesBasicCfgData(int runeStoneID)
	{
		return DataReader<Runes_basic>.Get(runeStoneID);
	}

	public Runes GetRunesCfgData(int runeStoneID)
	{
		int runeStoneLv = SkillRuneManager.Instance.GetRuneStoneLV(runeStoneID);
		Runes runes2 = DataReader<Runes>.DataList.Find((Runes runes) => runes.id == runeStoneID && runes.lv == runeStoneLv);
		if (runes2 == null)
		{
			Debuger.Error(string.Concat(new object[]
			{
				"找不到符文ID为",
				runeStoneID,
				"等级为",
				runeStoneLv,
				"的符文强化配置表"
			}), new object[0]);
		}
		return runes2;
	}

	public int GetEmbedRuneStoneAddAttrToSkill(int skillID)
	{
		int num = 0;
		List<int> list = new List<int>();
		if (this.skillRuneInfoDic.ContainsKey(skillID) && this.skillRuneInfoDic.get_Item(skillID).embedGroupId > 0)
		{
			List<RunedStone> embedRunedStoneInfoListBySkillID = this.GetEmbedRunedStoneInfoListBySkillID(skillID);
			for (int i = 0; i < embedRunedStoneInfoListBySkillID.get_Count(); i++)
			{
				RunedStone runedStone = embedRunedStoneInfoListBySkillID.get_Item(i);
				Runes runesCfgData = this.GetRunesCfgData(runedStone.cfgId);
				if (runesCfgData != null)
				{
					for (int j = 0; j < runesCfgData.templateId.get_Count(); j++)
					{
						int key = runesCfgData.templateId.get_Item(j);
						Runes_template runes_template = DataReader<Runes_template>.Get(key);
						if (runes_template != null)
						{
							list.AddRange(runes_template.damageIncreaseId);
						}
					}
				}
			}
			for (int k = 0; k < list.get_Count(); k++)
			{
				int key2 = list.get_Item(k);
				damageIncrease damageIncrease = DataReader<damageIncrease>.Get(key2);
				if (damageIncrease != null)
				{
					num += damageIncrease.Value1;
				}
			}
		}
		return num;
	}

	public List<KeyValuePair<int, int>> GetBattleSkillExtendIDs()
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		List<RunedStone> list2 = new List<RunedStone>();
		using (Dictionary<int, RunedStoneInfo>.Enumerator enumerator = this.skillRuneInfoDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, RunedStoneInfo> current = enumerator.get_Current();
				RunedStoneInfo value = current.get_Value();
				if (value.embedGroupId > 0)
				{
					List<RunedStone> embedRunedStoneInfoListBySkillID = this.GetEmbedRunedStoneInfoListBySkillID(value.skillId);
					list2.AddRange(embedRunedStoneInfoListBySkillID);
				}
			}
		}
		for (int i = 0; i < list2.get_Count(); i++)
		{
			RunedStone runedStone = list2.get_Item(i);
			int lv = runedStone.lv;
			Runes runesCfgData = this.GetRunesCfgData(runedStone.cfgId);
			if (runesCfgData != null)
			{
				for (int j = 0; j < runesCfgData.templateId.get_Count(); j++)
				{
					int key = runesCfgData.templateId.get_Item(j);
					Runes_template runes_template = DataReader<Runes_template>.Get(key);
					if (runes_template != null)
					{
						if (runes_template.skillExtendId > 0)
						{
							list.Add(new KeyValuePair<int, int>(runes_template.skillExtendId, lv));
						}
					}
				}
			}
		}
		return list;
	}

	public List<int> GetBattleSkillDamageIncreaseIDs()
	{
		List<int> list = new List<int>();
		List<RunedStone> list2 = new List<RunedStone>();
		using (Dictionary<int, RunedStoneInfo>.Enumerator enumerator = this.skillRuneInfoDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, RunedStoneInfo> current = enumerator.get_Current();
				RunedStoneInfo value = current.get_Value();
				if (value.embedGroupId > 0)
				{
					List<RunedStone> embedRunedStoneInfoListBySkillID = this.GetEmbedRunedStoneInfoListBySkillID(value.skillId);
					list2.AddRange(embedRunedStoneInfoListBySkillID);
				}
			}
		}
		for (int i = 0; i < list2.get_Count(); i++)
		{
			RunedStone runedStone = list2.get_Item(i);
			Runes runesCfgData = this.GetRunesCfgData(runedStone.cfgId);
			if (runesCfgData != null)
			{
				for (int j = 0; j < runesCfgData.templateId.get_Count(); j++)
				{
					int key = runesCfgData.templateId.get_Item(j);
					Runes_template runes_template = DataReader<Runes_template>.Get(key);
					if (runes_template != null)
					{
						if (runes_template.damageIncreaseId != null && runes_template.damageIncreaseId.get_Count() > 0)
						{
							list.AddRange(runes_template.damageIncreaseId);
						}
					}
				}
			}
		}
		return list;
	}

	public int GetSkillEmbedGroupIndex(int skillID)
	{
		int result = -1;
		if (this.skillRuneInfoDic != null && this.skillRuneInfoDic.ContainsKey(skillID))
		{
			result = this.skillRuneInfoDic.get_Item(skillID).embedGroupId;
		}
		return result;
	}

	private void UpdateUnLockRuneStoneList()
	{
		this.unLockRuneStoneList.Clear();
		using (Dictionary<int, RunedStoneInfo>.Enumerator enumerator = this.skillRuneInfoDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, RunedStoneInfo> current = enumerator.get_Current();
				this.unLockRuneStoneList.AddRange(current.get_Value().runedStones);
			}
		}
	}

	private List<RunedStone> GetEmbedRunedStoneInfoListBySkillID(int skillID)
	{
		List<RunedStone> list = new List<RunedStone>();
		if (this.skillRuneInfoDic != null && this.skillRuneInfoDic.ContainsKey(skillID))
		{
			List<RunedStone> runedStones = this.skillRuneInfoDic.get_Item(skillID).runedStones;
			for (int i = 0; i < runedStones.get_Count(); i++)
			{
				RunedStone runedStone = runedStones.get_Item(i);
				if (runedStone.groupId == this.skillRuneInfoDic.get_Item(skillID).embedGroupId)
				{
					list.Add(runedStone);
				}
			}
		}
		return list;
	}

	private void OnRoleSelfProfessionChange()
	{
		this.skillRuneInfoDic = null;
		this.skillRuneInfoDic = new Dictionary<int, RunedStoneInfo>();
		this.unLockRuneStoneList = null;
		this.unLockRuneStoneList = new List<RunedStone>();
	}
}
