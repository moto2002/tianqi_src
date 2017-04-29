using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class AcOpenServerManager : BaseSubSystemManager
{
	private List<int> unlockedActivityTypeList;

	private Dictionary<int, List<TargetTaskInfo>> activityTargetTaskDic;

	private Dictionary<int, List<RankingRoleInfo>> activityRankingRoleInfoDic;

	private Dictionary<int, bool> checkAcTypeCanGetRewardDic;

	public bool IsOpenActivity;

	public bool IsHideTownFX;

	public int MyRoleRankNum = -1;

	public long MyRoleScore;

	public int OpenServerDay;

	private static AcOpenServerManager instance;

	public Dictionary<int, List<TargetTaskInfo>> ActivityTargetTaskDic
	{
		get
		{
			return this.activityTargetTaskDic;
		}
	}

	public Dictionary<int, List<RankingRoleInfo>> ActivityRankingRoleInfoDic
	{
		get
		{
			return this.activityRankingRoleInfoDic;
		}
	}

	public static AcOpenServerManager Instance
	{
		get
		{
			if (AcOpenServerManager.instance == null)
			{
				AcOpenServerManager.instance = new AcOpenServerManager();
			}
			return AcOpenServerManager.instance;
		}
	}

	public override void Init()
	{
		this.checkAcTypeCanGetRewardDic = new Dictionary<int, bool>();
		base.Init();
	}

	public override void Release()
	{
		this.unlockedActivityTypeList = null;
		this.activityTargetTaskDic = null;
		this.activityRankingRoleInfoDic = null;
		this.checkAcTypeCanGetRewardDic = null;
		this.IsHideTownFX = false;
		this.IsOpenActivity = false;
		this.MyRoleRankNum = -1;
		this.OpenServerDay = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<OpenServerActStatusNty>(new NetCallBackMethod<OpenServerActStatusNty>(this.OnOpenServerActStatusNty));
		NetworkManager.AddListenEvent<GetOpenServerActInfoRes>(new NetCallBackMethod<GetOpenServerActInfoRes>(this.OnGetOpenServerActInfoRes));
		NetworkManager.AddListenEvent<GetOpenServerActRewardRes>(new NetCallBackMethod<GetOpenServerActRewardRes>(this.OnGetOpenServerActRewardRes));
	}

	public void SendGetOpenServerActInfoReq(int typeID)
	{
		NetworkManager.Send(new GetOpenServerActInfoReq
		{
			activityType = (OpenServerType.acType)typeID
		}, ServerType.Data);
	}

	public void SendGetOpenServerActRewardReq(int targetID, int typeID)
	{
		NetworkManager.Send(new GetOpenServerActRewardReq
		{
			activityType = (OpenServerType.acType)typeID,
			targetID = targetID
		}, ServerType.Data);
	}

	private void OnOpenServerActStatusNty(short state, OpenServerActStatusNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		this.IsOpenActivity = (msg.isOpenActivity && msg.unLockActTypes.get_Count() > 0);
		this.OpenServerDay = msg.nowDay;
		if (this.unlockedActivityTypeList == null)
		{
			this.unlockedActivityTypeList = new List<int>();
		}
		this.unlockedActivityTypeList.Clear();
		this.unlockedActivityTypeList.AddRange(msg.unLockActTypes);
		this.UpdateCheckAcTypeCanGetRewardDic(msg.targetInfos);
		Debug.Log(string.Concat(new object[]
		{
			"OpenServerDay=======",
			this.OpenServerDay,
			"IsOpenActivity=====",
			this.IsOpenActivity,
			"unLockActTypes=======",
			msg.unLockActTypes.get_Count()
		}));
		EventDispatcher.Broadcast(EventNames.OnOpenServerActStatusNty);
	}

	private void OnGetOpenServerActInfoRes(short state, GetOpenServerActInfoRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		if (this.activityTargetTaskDic == null)
		{
			this.activityTargetTaskDic = new Dictionary<int, List<TargetTaskInfo>>();
		}
		if (this.activityTargetTaskDic.ContainsKey((int)msg.activityType))
		{
			this.activityTargetTaskDic.set_Item((int)msg.activityType, msg.targetInfos);
		}
		else
		{
			this.activityTargetTaskDic.Add((int)msg.activityType, msg.targetInfos);
		}
		if (this.activityRankingRoleInfoDic == null)
		{
			this.activityRankingRoleInfoDic = new Dictionary<int, List<RankingRoleInfo>>();
		}
		if (this.activityRankingRoleInfoDic.ContainsKey((int)msg.activityType))
		{
			this.activityRankingRoleInfoDic.set_Item((int)msg.activityType, msg.role);
		}
		else
		{
			this.activityRankingRoleInfoDic.Add((int)msg.activityType, msg.role);
		}
		this.UpdateCheckAcTypeCanGetRewardDic((int)msg.activityType);
		this.MyRoleRankNum = msg.myRankNum;
		this.MyRoleScore = msg.myScore;
		Debug.Log("msg.myScore========" + msg.myScore);
		EventDispatcher.Broadcast(EventNames.OnGetOpenServerActInfoRes);
	}

	private void OnGetOpenServerActRewardRes(short state, GetOpenServerActRewardRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		if (this.activityTargetTaskDic != null && this.activityTargetTaskDic.ContainsKey((int)msg.activityType))
		{
			List<TargetTaskInfo> list = this.activityTargetTaskDic.get_Item((int)msg.activityType);
			if (list != null)
			{
				for (int i = 0; i < list.get_Count(); i++)
				{
					int targetID = list.get_Item(i).targetID;
					if (targetID == msg.targetID)
					{
						list.get_Item(i).status = TargetTaskInfo.GetRewardStatus.HadGet;
					}
				}
			}
		}
		List<int> list2 = new List<int>();
		List<long> list3 = new List<long>();
		for (int j = 0; j < msg.rewards.get_Count(); j++)
		{
			list2.Add(msg.rewards.get_Item(j).cfgId);
			list3.Add(msg.rewards.get_Item(j).count);
		}
		if (list2 != null && list2.get_Count() > 0)
		{
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
			rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513164, false), list2, list3, true, false, null, null);
			rewardUI.get_transform().SetAsLastSibling();
		}
		this.UpdateCheckAcTypeCanGetRewardDic((int)msg.activityType);
		EventDispatcher.Broadcast<int>(EventNames.OnGetOpenServerActRewardRes, msg.targetID);
	}

	public List<KaiFuHuoDong> GetCanShowActivityTypes()
	{
		List<KaiFuHuoDong> list = new List<KaiFuHuoDong>();
		if (DataReader<KaiFuHuoDong>.DataList != null && DataReader<KaiFuHuoDong>.DataList.get_Count() > 0)
		{
			List<KaiFuHuoDong> dataList = DataReader<KaiFuHuoDong>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				if (dataList.get_Item(i).openDay != null && dataList.get_Item(i).openDay.get_Count() >= 1 && dataList.get_Item(i).openDay.get_Item(0) > 0)
				{
					list.Add(dataList.get_Item(i));
				}
			}
		}
		return list;
	}

	public string GetAcOpenServerRightText(int type)
	{
		if (DataReader<KaiFuHuoDong>.Contains(type))
		{
			int chinese = DataReader<KaiFuHuoDong>.Get(type).chinese;
			return GameDataUtils.GetChineseContent(chinese, false);
		}
		return string.Empty;
	}

	public List<KaiFuPaiMing> GetActivityTaskListByType(int type)
	{
		List<KaiFuPaiMing> list = new List<KaiFuPaiMing>();
		if (DataReader<KaiFuHuoDong>.Contains(type))
		{
			KaiFuHuoDong kaiFuHuoDong = DataReader<KaiFuHuoDong>.Get(type);
			List<int> taskList = kaiFuHuoDong.taskList;
			if (taskList != null && taskList.get_Count() > 0)
			{
				for (int i = 0; i < taskList.get_Count(); i++)
				{
					int key = taskList.get_Item(i);
					if (DataReader<KaiFuPaiMing>.Contains(key))
					{
						KaiFuPaiMing kaiFuPaiMing = DataReader<KaiFuPaiMing>.Get(key);
						list.Add(kaiFuPaiMing);
					}
				}
			}
		}
		return list;
	}

	public bool CheckActivityTypeUnLock(int type)
	{
		if (this.unlockedActivityTypeList != null)
		{
			int num = this.unlockedActivityTypeList.FindIndex((int typeID) => typeID == type);
			if (num >= 0)
			{
				return true;
			}
		}
		return false;
	}

	public string GetAcTypeLeftPictureName(int type)
	{
		KaiFuHuoDong kaiFuHuoDong = DataReader<KaiFuHuoDong>.Get(type);
		if (kaiFuHuoDong != null)
		{
			return GameDataUtils.GetIconName(kaiFuHuoDong.picture);
		}
		return string.Empty;
	}

	public int GetRemainActivityTime(int type)
	{
		if (DataReader<KaiFuHuoDong>.Contains(type))
		{
			List<int> openDay = DataReader<KaiFuHuoDong>.Get(type).openDay;
			if (openDay != null && openDay.get_Count() > 0)
			{
				int num = openDay.get_Item(openDay.get_Count() - 1);
				if (num < this.OpenServerDay)
				{
					return -1;
				}
				if (num >= this.OpenServerDay)
				{
					int num2 = (num - this.OpenServerDay + 1) * 86400;
					return num2 - (int)TimeManager.Instance.PreciseServerTime.get_TimeOfDay().get_TotalSeconds();
				}
			}
		}
		return -1;
	}

	public List<int> GetUpWayListByType(int type)
	{
		List<int> list = new List<int>();
		if (DataReader<KaiFuHuoDong>.Contains(type))
		{
			KaiFuHuoDong kaiFuHuoDong = DataReader<KaiFuHuoDong>.Get(type);
			list.AddRange(kaiFuHuoDong.link);
		}
		return list;
	}

	public int GetSelectBtnTab()
	{
		List<KaiFuHuoDong> dataList = DataReader<KaiFuHuoDong>.DataList;
		if (dataList != null && dataList.get_Count() > 0)
		{
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				List<int> openDay = dataList.get_Item(i).openDay;
				for (int j = 0; j < openDay.get_Count(); j++)
				{
					int num = openDay.get_Item(j);
					if (this.checkAcTypeCanGetRewardDic.ContainsKey(num) && this.checkAcTypeCanGetRewardDic.get_Item(num))
					{
						return i;
					}
					if (num == this.OpenServerDay)
					{
						return i;
					}
				}
			}
		}
		return 0;
	}

	public TargetTaskInfo GetTargetTaskInfo(int type, int taskID)
	{
		TargetTaskInfo result = null;
		if (this.activityTargetTaskDic != null && this.activityTargetTaskDic.ContainsKey(type))
		{
			List<TargetTaskInfo> list = this.activityTargetTaskDic.get_Item(type);
			if (list != null)
			{
				int num = list.FindIndex((TargetTaskInfo a) => a.targetID == taskID);
				if (num >= 0)
				{
					return list.get_Item(num);
				}
			}
		}
		return result;
	}

	public List<RankingRoleInfo> GetRoleRankingInfoListByTargetID(int taskID)
	{
		List<RankingRoleInfo> list = new List<RankingRoleInfo>();
		if (DataReader<KaiFuPaiMing>.Contains(taskID))
		{
			KaiFuPaiMing kaiFuPaiMing = DataReader<KaiFuPaiMing>.Get(taskID);
			if (kaiFuPaiMing.objective == 1)
			{
				return list;
			}
			int type = kaiFuPaiMing.Type;
			if (this.activityRankingRoleInfoDic != null && this.activityRankingRoleInfoDic.ContainsKey(type))
			{
				List<RankingRoleInfo> list2 = this.activityRankingRoleInfoDic.get_Item(type);
				for (int i = 0; i < list2.get_Count(); i++)
				{
					if (list2.get_Item(i).roleId != 0L && list2.get_Item(i).number >= kaiFuPaiMing.ranking1 && list2.get_Item(i).number <= kaiFuPaiMing.ranking2)
					{
						list.Add(list2.get_Item(i));
					}
				}
			}
		}
		return list;
	}

	public List<string> GetRoleRankingNameContent(int taskID)
	{
		List<string> list = new List<string>();
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		List<RankingRoleInfo> roleRankingInfoListByTargetID = this.GetRoleRankingInfoListByTargetID(taskID);
		if (roleRankingInfoListByTargetID != null && roleRankingInfoListByTargetID.get_Count() > 0)
		{
			for (int i = 0; i < roleRankingInfoListByTargetID.get_Count(); i++)
			{
				string text4 = text;
				text = string.Concat(new object[]
				{
					text4,
					"<color=#f0d7b9>",
					roleRankingInfoListByTargetID.get_Item(i).number,
					".</color>\n"
				});
				text2 = text2 + "<color=#f0d7b9>" + roleRankingInfoListByTargetID.get_Item(i).name + "</color>\n";
				text4 = text3;
				text3 = string.Concat(new object[]
				{
					text4,
					"<color=#f0d7b9>",
					roleRankingInfoListByTargetID.get_Item(i).value,
					"</color>\n"
				});
			}
		}
		list.Add(text);
		list.Add(text2);
		list.Add(text3);
		return list;
	}

	private void UpdateCheckAcTypeCanGetRewardDic(List<TargetTaskInfo> taskList)
	{
		if (taskList == null)
		{
			return;
		}
		for (int i = 0; i < taskList.get_Count(); i++)
		{
			int targetID = taskList.get_Item(i).targetID;
			if (DataReader<KaiFuPaiMing>.Contains(targetID) && taskList.get_Item(i).status == TargetTaskInfo.GetRewardStatus.Available)
			{
				int type = DataReader<KaiFuPaiMing>.Get(targetID).Type;
				if (this.checkAcTypeCanGetRewardDic.ContainsKey(type))
				{
					this.checkAcTypeCanGetRewardDic.set_Item(type, true);
				}
				else
				{
					this.checkAcTypeCanGetRewardDic.Add(type, true);
				}
			}
		}
	}

	private void UpdateCheckAcTypeCanGetRewardDic(int type)
	{
		if (this.activityTargetTaskDic != null && this.activityTargetTaskDic.ContainsKey(type))
		{
			bool flag = false;
			List<TargetTaskInfo> list = this.activityTargetTaskDic.get_Item(type);
			for (int i = 0; i < list.get_Count(); i++)
			{
				if (list.get_Item(i).status == TargetTaskInfo.GetRewardStatus.Available)
				{
					flag = true;
					break;
				}
			}
			if (this.checkAcTypeCanGetRewardDic.ContainsKey(type))
			{
				this.checkAcTypeCanGetRewardDic.set_Item(type, flag);
			}
			else
			{
				this.checkAcTypeCanGetRewardDic.Add(type, flag);
			}
		}
		EventDispatcher.Broadcast(EventNames.UpdateAcTypeCanGetRewardTip);
	}

	public bool CheckCanShowAllRedPoint()
	{
		return this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.RoleFighting) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.Recharge) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.PetFighting) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.GemLv) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.GodWeapon) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.Level) || this.CheckCanShowAcTypeRedPoint(OpenServerType.acType.Wings);
	}

	public bool CheckCanShowAcTypeRedPoint(OpenServerType.acType type)
	{
		return this.checkAcTypeCanGetRewardDic.ContainsKey((int)type) && this.checkAcTypeCanGetRewardDic.get_Item((int)type);
	}
}
