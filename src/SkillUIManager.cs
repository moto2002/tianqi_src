using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class SkillUIManager : BaseSubSystemManager
{
	public struct BattleSkillData
	{
		public const int BeginIndex = 10;

		public int notchId;

		public int skillId;

		public int skillLv;
	}

	private int currentUseGroupSkill = 1;

	public int group;

	public int RiseItemID;

	public int SkillCanUpLevel;

	private List<int> newOpenSkillIDs;

	private Dictionary<int, SkillNotchInfo> skillNotchDic;

	private List<int> openSkillIDs;

	private Dictionary<int, SkillTrainInfo> skillTrainDic;

	private List<PassiveSkillInfo> passiveSkillInfoList;

	private static SkillUIManager instance;

	private int targetNotch;

	private int upgradeSkillID;

	public List<int> NewOpenSkillIDs
	{
		get
		{
			return this.newOpenSkillIDs;
		}
	}

	public Dictionary<int, SkillNotchInfo> SkillNotchDic
	{
		get
		{
			return this.skillNotchDic;
		}
	}

	public List<int> OpenSkillIDs
	{
		get
		{
			return this.openSkillIDs;
		}
	}

	public Dictionary<int, SkillTrainInfo> SkillTrainDic
	{
		get
		{
			return this.skillTrainDic;
		}
	}

	public List<PassiveSkillInfo> PassiveSkillInfoList
	{
		get
		{
			return this.passiveSkillInfoList;
		}
	}

	public static SkillUIManager Instance
	{
		get
		{
			if (SkillUIManager.instance == null)
			{
				SkillUIManager.instance = new SkillUIManager();
			}
			return SkillUIManager.instance;
		}
	}

	public override void Init()
	{
		this.skillTrainDic = new Dictionary<int, SkillTrainInfo>();
		this.skillNotchDic = new Dictionary<int, SkillNotchInfo>();
		this.passiveSkillInfoList = new List<PassiveSkillInfo>();
		this.newOpenSkillIDs = new List<int>();
		this.openSkillIDs = new List<int>();
		this.SkillCanUpLevel = (int)float.Parse(DataReader<GlobalParams>.Get("skillOpenLevel").value);
		this.SkillCanUpLevel = ((this.SkillCanUpLevel <= 0) ? 30 : this.SkillCanUpLevel);
		base.Init();
	}

	public override void Release()
	{
		this.skillTrainDic = null;
		this.skillNotchDic = null;
		this.passiveSkillInfoList = null;
		this.newOpenSkillIDs = null;
		this.openSkillIDs = null;
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionChange", new Callback(this.OnRoleSelfProfessionChange));
		NetworkManager.AddListenEvent<SkillConfigInfoLoginPush>(new NetCallBackMethod<SkillConfigInfoLoginPush>(this.OnSkillConfigInfoLoginPush));
		NetworkManager.AddListenEvent<SkillConfigInfoChangeNty>(new NetCallBackMethod<SkillConfigInfoChangeNty>(this.OnSkillConfigInfoChangeNty));
		NetworkManager.AddListenEvent<PushFixedSkill>(new NetCallBackMethod<PushFixedSkill>(this.OnPushFixedSkill));
		NetworkManager.AddListenEvent<SkillTrainLoginPush>(new NetCallBackMethod<SkillTrainLoginPush>(this.OnSkillTrainLoginPush));
		NetworkManager.AddListenEvent<SkillTrainChangeNty>(new NetCallBackMethod<SkillTrainChangeNty>(this.OnSkillTrainChangeNty));
		NetworkManager.AddListenEvent<SkillUpRes>(new NetCallBackMethod<SkillUpRes>(this.OnSkillUpRes));
		NetworkManager.AddListenEvent<SkillConfigRes>(new NetCallBackMethod<SkillConfigRes>(this.OnSkillConfigRes));
		NetworkManager.AddListenEvent<SkillFormationLoginPush>(new NetCallBackMethod<SkillFormationLoginPush>(this.OnSkillFormationLoginPush));
		NetworkManager.AddListenEvent<SetSkillFormationChangeNty>(new NetCallBackMethod<SetSkillFormationChangeNty>(this.OnSetSkillFormationChangeNty));
		NetworkManager.AddListenEvent<PassiveSkillPush>(new NetCallBackMethod<PassiveSkillPush>(this.OnPassiveSkillPush));
	}

	public void SendSkillConfigReq(int skillSlotNum, int targetNotchIndex, int groupId = 1)
	{
		this.targetNotch = targetNotchIndex;
		NetworkManager.Send(new SkillConfigReq
		{
			sourceNum = skillSlotNum,
			index = targetNotchIndex,
			skillConfigNum = groupId
		}, ServerType.Data);
	}

	public void SendSkillUpReq(int num, int upSkillID)
	{
		this.upgradeSkillID = upSkillID;
		NetworkManager.Send(new SkillUpReq
		{
			sortNum = num
		}, ServerType.Data);
	}

	private void OnSkillTrainLoginPush(short state, SkillTrainLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.skillTrainDic.Clear();
			if (this.openSkillIDs == null)
			{
				this.openSkillIDs = new List<int>();
			}
			this.openSkillIDs.Clear();
			for (int i = 0; i < down.skills.get_Count(); i++)
			{
				SkillTrainInfo skillTrainInfo = down.skills.get_Item(i);
				if (!this.skillTrainDic.ContainsKey(skillTrainInfo.skillId))
				{
					this.skillTrainDic.Add(skillTrainInfo.skillId, skillTrainInfo);
				}
				else
				{
					this.skillTrainDic.set_Item(skillTrainInfo.skillId, skillTrainInfo);
				}
				this.openSkillIDs.Add(skillTrainInfo.skillId);
			}
		}
	}

	private void OnSkillConfigInfoLoginPush(short state, SkillConfigInfoLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.skillNotchDic.Clear();
			for (int i = 0; i < down.infos.get_Count(); i++)
			{
				if (i > 1)
				{
					break;
				}
				SkillConfigInfo skillConfigInfo = down.infos.get_Item(i);
				this.skillNotchDic.Add(1, skillConfigInfo.notch1);
				this.skillNotchDic.Add(2, skillConfigInfo.notch2);
				this.skillNotchDic.Add(3, skillConfigInfo.notch3);
			}
		}
	}

	private void OnPushFixedSkill(short state, PushFixedSkill down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EntityWorld.Instance.EntSelf.RefreshStaticSkills(down.skills);
		}
	}

	private void OnSkillConfigInfoChangeNty(short state, SkillConfigInfoChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.infos.get_Count() > 0)
		{
			for (int i = 0; i < down.infos.get_Count(); i++)
			{
				if (i > 1)
				{
					break;
				}
				SkillConfigInfo skillConfigInfo = down.infos.get_Item(i);
				if (this.skillNotchDic.ContainsKey(1))
				{
					this.skillNotchDic.set_Item(1, skillConfigInfo.notch1);
				}
				else
				{
					this.skillNotchDic.Add(1, skillConfigInfo.notch1);
				}
				if (this.skillNotchDic.ContainsKey(2))
				{
					this.skillNotchDic.set_Item(2, skillConfigInfo.notch2);
				}
				else
				{
					this.skillNotchDic.Add(2, skillConfigInfo.notch2);
				}
				if (this.skillNotchDic.ContainsKey(3))
				{
					this.skillNotchDic.set_Item(3, skillConfigInfo.notch3);
				}
				else
				{
					this.skillNotchDic.Add(3, skillConfigInfo.notch3);
				}
			}
			EventDispatcher.Broadcast(EventNames.OnSkillConfigInfoChangeNty);
		}
	}

	private void OnSkillTrainChangeNty(short state, SkillTrainChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.skills.get_Count(); i++)
			{
				SkillTrainInfo skillTrainInfo = down.skills.get_Item(i);
				if (this.skillTrainDic.ContainsKey(skillTrainInfo.skillId))
				{
					this.skillTrainDic.set_Item(skillTrainInfo.skillId, skillTrainInfo);
				}
				else
				{
					this.skillTrainDic.Add(skillTrainInfo.skillId, skillTrainInfo);
				}
				if (!UIManagerControl.Instance.IsOpen("SkillUI"))
				{
					int num = this.newOpenSkillIDs.FindIndex((int a) => a == skillTrainInfo.skillId);
					if (num < 0)
					{
						this.newOpenSkillIDs.Add(skillTrainInfo.skillId);
					}
				}
				if (this.openSkillIDs != null)
				{
					int num2 = this.openSkillIDs.FindIndex((int a) => a == skillTrainInfo.skillId);
					if (num2 < 0)
					{
						this.openSkillIDs.Add(skillTrainInfo.skillId);
					}
				}
			}
			EventDispatcher.Broadcast(EventNames.OnSkillTrainChangeNty);
		}
	}

	private void OnSkillConfigRes(short state, SkillConfigRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (state == 0)
		{
			EventDispatcher.Broadcast<int>(EventNames.OnSkillConfigRes, this.targetNotch);
		}
	}

	private void OnSkillUpRes(short state, SkillUpRes down = null)
	{
		if ((int)state == Status.SKILL_POINT_NOT_ENOUGH)
		{
			LinkNavigationManager.ItemNotEnoughToLink(11, true, null, true);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, this.RiseItemID);
			return;
		}
		if (state == 0)
		{
			EventDispatcher.Broadcast<int>(EventNames.OnSkillUpgradeRes, this.upgradeSkillID);
		}
	}

	private void OnSkillFormationLoginPush(short state, SkillFormationLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
		}
	}

	private void OnSetSkillFormationChangeNty(short state, SetSkillFormationChangeNty down = null)
	{
	}

	private void OnPassiveSkillPush(short state, PassiveSkillPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.passiveSkillInfoList == null)
			{
				this.passiveSkillInfoList = new List<PassiveSkillInfo>();
			}
			this.passiveSkillInfoList.Clear();
			for (int i = 0; i < down.skill.get_Count(); i++)
			{
				if (down.skill.get_Item(i).State)
				{
					this.passiveSkillInfoList.Add(down.skill.get_Item(i));
				}
			}
		}
	}

	private void OnRoleSelfProfessionChange()
	{
	}

	public List<SkillUIManager.BattleSkillData> GetBattleSkillData()
	{
		List<SkillUIManager.BattleSkillData> list = new List<SkillUIManager.BattleSkillData>();
		using (Dictionary<int, SkillNotchInfo>.Enumerator enumerator = this.skillNotchDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, SkillNotchInfo> current = enumerator.get_Current();
				if (current.get_Value().skillId > 0)
				{
					list.Add(new SkillUIManager.BattleSkillData
					{
						skillId = current.get_Value().skillId,
						skillLv = this.GetSkillLvByID(current.get_Value().skillId),
						notchId = current.get_Key()
					});
				}
			}
		}
		return list;
	}

	public int GetSkillSlotNumByID(int skillID)
	{
		if (this.skillTrainDic != null && this.skillTrainDic.ContainsKey(skillID))
		{
			return this.skillTrainDic.get_Item(skillID).number;
		}
		return -1;
	}

	public JiNengShengJiBiao GetSkillUpgradeCfgDataByID(int skillID)
	{
		int skillLv = this.GetSkillLvByID(skillID);
		return DataReader<JiNengShengJiBiao>.DataList.Find((JiNengShengJiBiao a) => a.lv == skillLv && a.skillId == skillID);
	}

	public JiNengShengJiBiao GetSkillNextLvCfgDataByID(int skillID)
	{
		int skillLv = this.GetSkillLvByID(skillID);
		return DataReader<JiNengShengJiBiao>.DataList.Find((JiNengShengJiBiao a) => a.lv == skillLv + 1 && a.skillId == skillID);
	}

	public JiNengJieSuoBiao GetSkillUnLockCfgDataBySkillID(int skillID)
	{
		return DataReader<JiNengJieSuoBiao>.DataList.Find((JiNengJieSuoBiao a) => a.skillId == skillID);
	}

	public int GetSkillNotchNumber(int skillID)
	{
		if (this.skillNotchDic == null || this.skillNotchDic.get_Count() <= 0)
		{
			return 0;
		}
		using (Dictionary<int, SkillNotchInfo>.Enumerator enumerator = this.skillNotchDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, SkillNotchInfo> current = enumerator.get_Current();
				if (current.get_Value().skillId == skillID)
				{
					return current.get_Key();
				}
			}
		}
		return 0;
	}

	public int GetSkillLvByID(int skillID)
	{
		if (this.skillTrainDic == null || this.skillTrainDic.get_Count() <= 0)
		{
			return 1;
		}
		if (this.skillTrainDic.ContainsKey(skillID))
		{
			return this.skillTrainDic.get_Item(skillID).skillLv;
		}
		return 1;
	}

	public List<JiNengJieSuoBiao> GetSkillUnlockCfgData()
	{
		List<JiNengJieSuoBiao> result = new List<JiNengJieSuoBiao>();
		if (EntityWorld.Instance.EntSelf != null)
		{
			result = DataReader<JiNengJieSuoBiao>.DataList.FindAll((JiNengJieSuoBiao a) => a.typeId == EntityWorld.Instance.EntSelf.TypeID);
		}
		return result;
	}

	public int GetSkillUnlockArtifactID(int skillID)
	{
		int result = -1;
		JiNengJieSuoBiao skillUnLockCfgDataBySkillID = this.GetSkillUnLockCfgDataBySkillID(skillID);
		if (skillUnLockCfgDataBySkillID != null)
		{
			int num = skillUnLockCfgDataBySkillID.deblockingType.FindIndex((int a) => a == 2);
			if (num >= 0)
			{
				if (num >= skillUnLockCfgDataBySkillID.condition.get_Count())
				{
					return result;
				}
				result = skillUnLockCfgDataBySkillID.condition.get_Item(num);
			}
		}
		return result;
	}

	public bool CheckRoleSkillsCanUpgrade()
	{
		if (EntityWorld.Instance.EntSelf == null || EntityWorld.Instance.EntSelf.Lv < this.SkillCanUpLevel)
		{
			return false;
		}
		List<JiNengJieSuoBiao> skillUnlockCfgData = this.GetSkillUnlockCfgData();
		for (int i = 0; i < skillUnlockCfgData.get_Count(); i++)
		{
			bool flag = this.CheckSkillUpgradeCanShowRedPointTip(skillUnlockCfgData.get_Item(i).skillId);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckSkillUpgradeCanShowRedPointTip(int skillID)
	{
		if (EntityWorld.Instance.EntSelf == null || EntityWorld.Instance.EntSelf.Lv < this.SkillCanUpLevel)
		{
			return false;
		}
		if (this.skillTrainDic != null && this.skillTrainDic.ContainsKey(skillID))
		{
			int skillLv = this.skillTrainDic.get_Item(skillID).skillLv;
			JiNengShengJiBiao skillNextLvCfgDataByID = this.GetSkillNextLvCfgDataByID(skillID);
			if (skillNextLvCfgDataByID == null)
			{
				return false;
			}
			long num = (long)skillNextLvCfgDataByID.itemNum.get_Item(0);
			if (EntityWorld.Instance.EntSelf.SkillPoint >= num)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckSkillIsEmbedNotch(int skillID)
	{
		if (this.skillNotchDic == null || this.skillNotchDic.get_Count() <= 0)
		{
			return false;
		}
		using (Dictionary<int, SkillNotchInfo>.Enumerator enumerator = this.skillNotchDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, SkillNotchInfo> current = enumerator.get_Current();
				if (current.get_Value().skillId == skillID)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckSkillIsUnLock(int skillID)
	{
		return this.skillTrainDic != null && this.skillTrainDic.ContainsKey(skillID);
	}

	public bool CheckSkillIsCanUpgrade(int skillID)
	{
		int skillLv = 0;
		if (this.skillTrainDic != null && this.skillTrainDic.ContainsKey(skillID))
		{
			skillLv = this.skillTrainDic.get_Item(skillID).skillLv;
			return DataReader<JiNengShengJiBiao>.DataList.Find((JiNengShengJiBiao a) => a.skillId == skillID && a.lv == skillLv + 1) != null;
		}
		return false;
	}

	public List<ArtifactSkill> GetAllArtifactSkillCfgData()
	{
		List<ArtifactSkill> result = new List<ArtifactSkill>();
		if (EntityWorld.Instance.EntSelf != null)
		{
			result = DataReader<ArtifactSkill>.DataList.FindAll((ArtifactSkill a) => a.job == EntityWorld.Instance.EntSelf.TypeID || a.job == 0);
		}
		return result;
	}

	public ArtifactSkill GetArtifactSkillCfgDataByID(int id)
	{
		return DataReader<ArtifactSkill>.Get(id);
	}

	public List<KeyValuePair<int, int>> GetBattleBeActivitySkillExtendIDs()
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		if (this.passiveSkillInfoList == null || this.passiveSkillInfoList.get_Count() <= 0)
		{
			return list;
		}
		for (int i = 0; i < this.passiveSkillInfoList.get_Count(); i++)
		{
			int id = this.passiveSkillInfoList.get_Item(i).Id;
			int num = this.passiveSkillInfoList.get_Item(i).Lv;
			num = ((num <= 0) ? 1 : num);
			ArtifactSkill artifactSkillCfgDataByID = this.GetArtifactSkillCfgDataByID(id);
			if (artifactSkillCfgDataByID != null)
			{
				if (artifactSkillCfgDataByID.skillType == 2)
				{
					for (int j = 0; j < artifactSkillCfgDataByID.effect.get_Count(); j++)
					{
						list.Add(new KeyValuePair<int, int>(artifactSkillCfgDataByID.effect.get_Item(j), num));
					}
				}
			}
		}
		return list;
	}

	public bool CheckArtifactSkillIsUnlock(int id)
	{
		if (this.passiveSkillInfoList != null)
		{
			int num = this.passiveSkillInfoList.FindIndex((PassiveSkillInfo a) => a.Id == id);
			if (num >= 0 && this.passiveSkillInfoList.get_Item(num).State)
			{
				return true;
			}
		}
		return false;
	}

	public string GetArtifactNameLockTipByID(int id)
	{
		ArtifactSkill artifactSkillCfgDataByID = this.GetArtifactSkillCfgDataByID(id);
		if (artifactSkillCfgDataByID == null)
		{
			return string.Empty;
		}
		string result = string.Empty;
		int activation = artifactSkillCfgDataByID.activation;
		if (activation > 0)
		{
			Artifact artifact = DataReader<Artifact>.Get(activation);
			string text = string.Empty;
			if (artifact != null)
			{
				text = GameDataUtils.GetChineseContent(artifact.name, false);
			}
			result = string.Format(GameDataUtils.GetChineseContent(518005, false), text);
		}
		return result;
	}
}
