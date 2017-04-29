using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class RoleTalentManager : BaseSubSystemManager
{
	private const int TALENT_POINT_ITEM_ID = 1014;

	private const int ACT_POINT_ID = 106;

	private List<Talent> listProfessionTalent;

	private Dictionary<int, List<int>> zoneToTalent;

	private List<RoleTalentInfo> _RoleTalentInfos = new List<RoleTalentInfo>();

	private int _TalentPoint;

	private int _ResetTalentCount;

	private bool _TalentPointBadge;

	private static RoleTalentManager instance;

	public List<BattleSkillExtend> BattleSkillExtends = new List<BattleSkillExtend>();

	public List<BattleSkillInfo> BattleSkillInfos = new List<BattleSkillInfo>();

	public int InitActPoint;

	private Dictionary<int, int> SkillTypeExistTimeOffsets = new Dictionary<int, int>();

	public List<Talent> ListProfessionTalent
	{
		get
		{
			if (this.listProfessionTalent == null)
			{
				this.InitData();
			}
			return this.listProfessionTalent;
		}
	}

	public Dictionary<int, List<int>> ZoneToTalent
	{
		get
		{
			if (this.zoneToTalent == null)
			{
				this.InitData();
			}
			return this.zoneToTalent;
		}
	}

	public List<RoleTalentInfo> RoleTalentInfos
	{
		get
		{
			return this._RoleTalentInfos;
		}
		set
		{
			this._RoleTalentInfos = value;
			this.RefreshTalentTemplates();
		}
	}

	public int TalentPoint
	{
		get
		{
			return this._TalentPoint;
		}
		set
		{
			this._TalentPoint = value;
			this.TalentPointBadge = (this._TalentPoint > 0);
		}
	}

	public int ResetTalentCount
	{
		get
		{
			return this._ResetTalentCount;
		}
		set
		{
			this._ResetTalentCount = value;
		}
	}

	public bool TalentPointBadge
	{
		get
		{
			return this._TalentPointBadge;
		}
		set
		{
			this._TalentPointBadge = (value && SystemOpenManager.IsSystemOn(36));
			EventDispatcher.Broadcast<bool>(EventNames.TalentPointBadge, this._TalentPointBadge);
		}
	}

	public static RoleTalentManager Instance
	{
		get
		{
			if (RoleTalentManager.instance == null)
			{
				RoleTalentManager.instance = new RoleTalentManager();
			}
			return RoleTalentManager.instance;
		}
	}

	private RoleTalentManager()
	{
	}

	public Talent GetDataCurrent(int cfgId)
	{
		return this.GetData(cfgId, this.GetLevel(cfgId));
	}

	public Talent GetData(int cfgId, int level)
	{
		for (int i = 0; i < this.ListProfessionTalent.get_Count(); i++)
		{
			Talent talent = this.ListProfessionTalent.get_Item(i);
			if (talent.id == cfgId && talent.lv == level)
			{
				return talent;
			}
		}
		return null;
	}

	public int GetLevel(int cfgId)
	{
		RoleTalentInfo talentInfo = this.GetTalentInfo(cfgId);
		if (talentInfo != null)
		{
			return talentInfo.lv;
		}
		return 0;
	}

	public int GetMaxLevel(int cfgId)
	{
		for (int i = 0; i < this.ListProfessionTalent.get_Count(); i++)
		{
			Talent talent = this.ListProfessionTalent.get_Item(i);
			if (talent.id == cfgId && (talent.lv == talent.nextLv || talent.nextLv <= 0))
			{
				return talent.lv;
			}
		}
		return 100;
	}

	public bool IsActivation(int cfgId)
	{
		return this.GetTalentInfo(cfgId) != null;
	}

	public bool IsActivationReach(int cfgId, int level)
	{
		RoleTalentInfo talentInfo = this.GetTalentInfo(cfgId);
		return talentInfo != null && talentInfo.lv >= level;
	}

	public bool IsActivationCan(Talent data)
	{
		if (data == null)
		{
			return true;
		}
		for (int i = 0; i < data.activation.get_Count(); i++)
		{
			if (data.activation.get_Item(i).key != 1014 && (long)data.activation.get_Item(i).value > BackpackManager.Instance.OnGetGoodCount(data.activation.get_Item(i).key))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsUpgradeCan(Talent data)
	{
		if (data.minRoleLv > EntityWorld.Instance.EntSelf.Lv)
		{
			return false;
		}
		for (int i = 0; i < data.preTalent.get_Count(); i++)
		{
			if (!this.IsActivationReach(data.preTalent.get_Item(i).key, data.preTalent.get_Item(i).value))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsResetCan()
	{
		for (int i = 0; i < this.RoleTalentInfos.get_Count(); i++)
		{
			if (this.RoleTalentInfos.get_Item(i).lv > 0)
			{
				return true;
			}
		}
		return false;
	}

	public int GetDefaultCfgId()
	{
		if (this.ListProfessionTalent.get_Count() > 0)
		{
			return this.ListProfessionTalent.get_Item(0).id;
		}
		return 0;
	}

	public int GetResetPrice()
	{
		int key = this.ResetTalentCount + 1;
		ResetConsume resetConsume = DataReader<ResetConsume>.Get(key);
		if (resetConsume != null)
		{
			return resetConsume.needDiamond;
		}
		List<ResetConsume> dataList = DataReader<ResetConsume>.DataList;
		if (dataList.get_Count() > 0)
		{
			return dataList.get_Item(dataList.get_Count() - 1).needDiamond;
		}
		return 0;
	}

	public Talent.ActivationPair GetActivationItem(Talent data, int itemindex)
	{
		int num = 0;
		for (int i = 0; i < data.activation.get_Count(); i++)
		{
			if (data.activation.get_Item(i).key != 1014)
			{
				if (num == itemindex)
				{
					return data.activation.get_Item(i);
				}
				num++;
			}
		}
		return null;
	}

	public Talent.ActivationPair GetUpgradeItemOfPoint(int cfgId)
	{
		Talent data = DataReader<Talent>.Get(cfgId);
		return this.GetUpgradeItemOfPoint(data);
	}

	public Talent.ActivationPair GetUpgradeItemOfPoint(Talent data)
	{
		if (data == null)
		{
			return null;
		}
		for (int i = 0; i < data.activation.get_Count(); i++)
		{
			if (data.activation.get_Item(i).key == 1014)
			{
				return data.activation.get_Item(i);
			}
		}
		return null;
	}

	private void InitData()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			Debug.LogError("EntityWorld.Instance.EntSelf is null !!!");
			return;
		}
		if (this.listProfessionTalent == null)
		{
			this.listProfessionTalent = new List<Talent>();
		}
		this.listProfessionTalent.Clear();
		List<Talent> dataList = DataReader<Talent>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			Talent talent = dataList.get_Item(i);
			if (talent.job == EntityWorld.Instance.EntSelf.TypeID)
			{
				this.listProfessionTalent.Add(talent);
			}
		}
		if (this.zoneToTalent == null)
		{
			this.zoneToTalent = new Dictionary<int, List<int>>();
		}
		this.zoneToTalent.Clear();
		for (int j = 0; j < this.listProfessionTalent.get_Count(); j++)
		{
			Talent talent2 = this.listProfessionTalent.get_Item(j);
			if (!this.zoneToTalent.ContainsKey(talent2.minRoleLv))
			{
				this.zoneToTalent.set_Item(talent2.minRoleLv, new List<int>());
			}
			if (!this.zoneToTalent.get_Item(talent2.minRoleLv).Contains(talent2.id))
			{
				this.zoneToTalent.get_Item(talent2.minRoleLv).Add(talent2.id);
			}
		}
	}

	public void UpdateTalentInfo(RoleTalentInfo info)
	{
		for (int i = 0; i < this.RoleTalentInfos.get_Count(); i++)
		{
			if (this.RoleTalentInfos.get_Item(i).cfgId == info.cfgId)
			{
				this.RoleTalentInfos.set_Item(i, info);
				this.RefreshTalentTemplates();
				return;
			}
		}
		this.RoleTalentInfos.Add(info);
		this.RefreshTalentTemplates();
	}

	public RoleTalentInfo GetTalentInfo(int cfgId)
	{
		for (int i = 0; i < this.RoleTalentInfos.get_Count(); i++)
		{
			if (this.RoleTalentInfos.get_Item(i).cfgId == cfgId)
			{
				return this.RoleTalentInfos.get_Item(i);
			}
		}
		return null;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.RoleTalentInfos.Clear();
		this.ResetTalentTemplatesData();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<RoleTalentInfoPush>(new NetCallBackMethod<RoleTalentInfoPush>(this.OnRoleTalentInfoPush));
		NetworkManager.AddListenEvent<RoleTalentActivateRes>(new NetCallBackMethod<RoleTalentActivateRes>(this.OnRoleTalentActivateRes));
		NetworkManager.AddListenEvent<RoleTalentUpgradeRes>(new NetCallBackMethod<RoleTalentUpgradeRes>(this.OnRoleTalentUpgradeRes));
		NetworkManager.AddListenEvent<RoleTalentResetRes>(new NetCallBackMethod<RoleTalentResetRes>(this.OnRoleTalentResetRes));
		NetworkManager.AddListenEvent<RoleTalentPointNty>(new NetCallBackMethod<RoleTalentPointNty>(this.OnRoleTalentPointNty));
	}

	public void SendRoleTalentActivate(int cfgId)
	{
		if (cfgId <= 0)
		{
			return;
		}
		NetworkManager.Send(new RoleTalentActivateReq
		{
			talentCfgId = cfgId
		}, ServerType.Data);
	}

	public void SendRoleTalentUpgrade(int cfgId)
	{
		if (cfgId <= 0)
		{
			return;
		}
		NetworkManager.Send(new RoleTalentUpgradeReq
		{
			talentCfgId = cfgId
		}, ServerType.Data);
	}

	public void SendRoleTalentReset()
	{
		NetworkManager.Send(new RoleTalentResetReq(), ServerType.Data);
	}

	private void OnRoleTalentInfoPush(short state, RoleTalentInfoPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.InitData();
			this.RoleTalentInfos = down.talents;
			this.TalentPoint = down.talentPoints;
			this.ResetTalentCount = down.resetTalentCount;
			this.RefreshUI();
			if (RoleTalentUIView.Instance != null && RoleTalentUIView.Instance.get_gameObject().get_activeSelf())
			{
				RoleTalentUIView.Instance.TalentReset();
			}
		}
	}

	private void OnRoleTalentPointNty(short state, RoleTalentPointNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.TalentPoint = down.talentPoints;
			this.RefreshUI();
		}
	}

	private void OnRoleTalentActivateRes(short state, RoleTalentActivateRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.UpdateTalentInfo(down.talent);
			this.RefreshUI();
			if (RoleTalentUIView.Instance != null && RoleTalentUIView.Instance.get_gameObject().get_activeSelf())
			{
				RoleTalentUIView.Instance.TalentActivation(down.talent.cfgId);
			}
		}
	}

	private void OnRoleTalentUpgradeRes(short state, RoleTalentUpgradeRes down = null)
	{
		if (state != 0)
		{
			if (down == null)
			{
				StateManager.Instance.StateShow(state, 0);
			}
			else
			{
				StateManager.Instance.StateShow(state, down.itemId);
			}
			return;
		}
		if (down != null)
		{
			this.UpdateTalentInfo(down.talent);
			this.RefreshUI();
			if (RoleTalentUIView.Instance != null && RoleTalentUIView.Instance.get_gameObject().get_activeSelf())
			{
				RoleTalentUIView.Instance.TalentLevelUp(down.talent.cfgId);
			}
		}
	}

	private void OnRoleTalentResetRes(short state, RoleTalentResetRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RoleTalentInfos = down.talent;
			this.ResetTalentCount = down.resetTalentCount;
			this.RefreshUI();
			if (RoleTalentUIView.Instance != null && RoleTalentUIView.Instance.get_gameObject().get_activeSelf())
			{
				RoleTalentUIView.Instance.TalentReset();
			}
		}
	}

	public bool IsRoleTalentSystemOpen()
	{
		int num = 1;
		return EntityWorld.Instance.EntSelf.Lv >= num && ChangeCareerManager.Instance.IsCareerChanged();
	}

	private void RefreshUI()
	{
		if (RoleTalentUIView.Instance != null && RoleTalentUIView.Instance.get_gameObject().get_activeSelf())
		{
			RoleTalentUIView.Instance.RefreshUI();
		}
	}

	private void ResetTalentTemplatesData()
	{
		this.BattleSkillExtends.Clear();
		this.BattleSkillInfos.Clear();
		this.InitActPoint = 0;
		this.SkillTypeExistTimeOffsets.Clear();
	}

	private void RefreshTalentTemplates()
	{
		this.ResetTalentTemplatesData();
		for (int i = 0; i < this.RoleTalentInfos.get_Count(); i++)
		{
			RoleTalentInfo roleTalentInfo = this.RoleTalentInfos.get_Item(i);
			Talent data = this.GetData(roleTalentInfo.cfgId, roleTalentInfo.lv);
			if (data != null)
			{
				if (data.templateId > 0)
				{
					talent_template talent_template = DataReader<talent_template>.Get(data.templateId);
					if (talent_template == null)
					{
						Debug.LogError("GameData.talent_template no exist, id = " + data.templateId);
					}
					else if (!this.RefreshInitActPoint(talent_template))
					{
						if (!this.RefreshSkillTypeCDOffsets(talent_template))
						{
							if (!this.RefreshSkillTypeActPointOffsets(talent_template))
							{
								if (!this.RefreshSkillTypeExistTimeOffsets(talent_template))
								{
									if (this.RefreshPassiveSkillIDs(talent_template))
									{
									}
								}
							}
						}
					}
				}
			}
		}
	}

	private bool RefreshInitActPoint(talent_template dataTT)
	{
		if (dataTT.type == 1)
		{
			int key = int.Parse(GameDataUtils.SplitString4Dot0(dataTT.ruleDetail));
			Attrs attrs = DataReader<Attrs>.Get(key);
			if (attrs == null)
			{
				return true;
			}
			for (int i = 0; i < attrs.attrs.get_Count(); i++)
			{
				if (attrs.attrs.get_Item(i) == 106)
				{
					if (i < attrs.values.get_Count())
					{
						this.InitActPoint += attrs.values.get_Item(i);
					}
					return true;
				}
			}
		}
		return false;
	}

	private bool RefreshSkillTypeCDOffsets(talent_template dataTT)
	{
		if (dataTT.type != 4)
		{
			return false;
		}
		string[] array = dataTT.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return true;
		}
		int skillType = int.Parse(array[0]);
		string text = array[1].Substring(0, 1);
		string text2 = array[1].Substring(1, array[1].get_Length() - 1);
		BattleSkillExtend battleSkillExtend = this.GetBattleSkillExtend(skillType);
		if (battleSkillExtend == null)
		{
			battleSkillExtend = new BattleSkillExtend();
			battleSkillExtend.skillType = skillType;
			this.BattleSkillExtends.Add(battleSkillExtend);
		}
		if (text == "-")
		{
			battleSkillExtend.cdOffset -= int.Parse(text2);
		}
		else
		{
			battleSkillExtend.cdOffset += int.Parse(text2);
		}
		return true;
	}

	private bool RefreshSkillTypeActPointOffsets(talent_template dataTT)
	{
		if (dataTT.type != 5)
		{
			return false;
		}
		string[] array = dataTT.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return true;
		}
		int skillType = int.Parse(array[0]);
		string text = array[1].Substring(0, 1);
		string text2 = array[1].Substring(1, array[1].get_Length() - 1);
		BattleSkillExtend battleSkillExtend = this.GetBattleSkillExtend(skillType);
		if (battleSkillExtend == null)
		{
			battleSkillExtend = new BattleSkillExtend();
			battleSkillExtend.skillType = skillType;
			this.BattleSkillExtends.Add(battleSkillExtend);
		}
		if (text == "-")
		{
			battleSkillExtend.actPointOffset -= int.Parse(text2);
		}
		else
		{
			battleSkillExtend.actPointOffset += int.Parse(text2);
		}
		return true;
	}

	private BattleSkillExtend GetBattleSkillExtend(int skillType)
	{
		for (int i = 0; i < this.BattleSkillExtends.get_Count(); i++)
		{
			if (this.BattleSkillExtends.get_Item(i).skillType == skillType)
			{
				return this.BattleSkillExtends.get_Item(i);
			}
		}
		return null;
	}

	private bool RefreshSkillTypeExistTimeOffsets(talent_template dataTT)
	{
		if (dataTT.type != 6)
		{
			return false;
		}
		string[] array = dataTT.ruleDetail.Split(new char[]
		{
			','
		});
		if (array.Length < 2)
		{
			return true;
		}
		int num = int.Parse(array[0]);
		string text = array[1].Substring(0, 1);
		string text2 = array[1].Substring(1, array[1].get_Length() - 1);
		if (!this.SkillTypeExistTimeOffsets.ContainsKey(num))
		{
			this.SkillTypeExistTimeOffsets.set_Item(num, 0);
		}
		if (text == "-")
		{
			this.SkillTypeExistTimeOffsets.set_Item(num, this.SkillTypeExistTimeOffsets.get_Item(num) - int.Parse(text2));
		}
		else
		{
			this.SkillTypeExistTimeOffsets.set_Item(num, this.SkillTypeExistTimeOffsets.get_Item(num) + int.Parse(text2));
		}
		return true;
	}

	public int GetSkillTypeExistTimeOffset(Pet dataPet)
	{
		int num = dataPet.function + 9;
		if (this.SkillTypeExistTimeOffsets.ContainsKey(num))
		{
			return this.SkillTypeExistTimeOffsets.get_Item(num);
		}
		return 0;
	}

	private bool RefreshPassiveSkillIDs(talent_template dataTT)
	{
		if (dataTT.type == 10)
		{
			int skillId = (int)float.Parse(dataTT.ruleDetail);
			if (this.GetBattleSkillInfo(skillId) == null)
			{
				BattleSkillInfo battleSkillInfo = new BattleSkillInfo();
				battleSkillInfo.skillId = skillId;
				battleSkillInfo.skillIdx = 0;
				battleSkillInfo.skillLv = 1;
				this.BattleSkillInfos.Add(battleSkillInfo);
			}
			return true;
		}
		return false;
	}

	private BattleSkillInfo GetBattleSkillInfo(int skillId)
	{
		for (int i = 0; i < this.BattleSkillInfos.get_Count(); i++)
		{
			if (this.BattleSkillInfos.get_Item(i).skillId == skillId)
			{
				return this.BattleSkillInfos.get_Item(i);
			}
		}
		return null;
	}
}
