using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class ChangeCareerManager : BaseSubSystemManager
{
	public class EventNames
	{
		public const string RoleSelfProfessionReadyChange = "ChangeCareerManager.RoleSelfProfessionReadyChange";

		public const string RoleSelfProfessionChange = "ChangeCareerManager.RoleSelfProfessionChange";
	}

	private List<int> m_hadCareerIDs = new List<int>();

	private List<OneCareer> m_rpcCareerTasks;

	private static ChangeCareerManager instance;

	private int RoleChangeProfession;

	public static ChangeCareerManager Instance
	{
		get
		{
			if (ChangeCareerManager.instance == null)
			{
				ChangeCareerManager.instance = new ChangeCareerManager();
			}
			return ChangeCareerManager.instance;
		}
	}

	private ChangeCareerManager()
	{
	}

	public List<CareerTask> GetCareerTasks(int profession)
	{
		if (this.m_rpcCareerTasks == null)
		{
			return null;
		}
		for (int i = 0; i < this.m_rpcCareerTasks.get_Count(); i++)
		{
			if (this.m_rpcCareerTasks.get_Item(i).dstCareer == profession)
			{
				this.m_rpcCareerTasks.get_Item(i).tasks.Sort(new Comparison<CareerTask>(ChangeCareerManager.SortCompare));
				return this.m_rpcCareerTasks.get_Item(i).tasks;
			}
		}
		return null;
	}

	private static int SortCompare(CareerTask task1, CareerTask task2)
	{
		int result = 0;
		if (task1.taskId < task2.taskId)
		{
			result = -1;
		}
		else if (task1.taskId > task2.taskId)
		{
			result = 1;
		}
		return result;
	}

	private void UpdateCareer(OneCareer oneCareer)
	{
		if (this.m_rpcCareerTasks == null)
		{
			this.m_rpcCareerTasks = new List<OneCareer>();
		}
		for (int i = 0; i < this.m_rpcCareerTasks.get_Count(); i++)
		{
			OneCareer oneCareer2 = this.m_rpcCareerTasks.get_Item(i);
			if (oneCareer2.dstCareer == oneCareer.dstCareer)
			{
				this.UpdateCareerTasks(oneCareer2, oneCareer);
				return;
			}
		}
		this.m_rpcCareerTasks.Add(oneCareer);
	}

	private void UpdateCareerTasks(OneCareer thisCareer, OneCareer updateCareer)
	{
		for (int i = 0; i < updateCareer.tasks.get_Count(); i++)
		{
			int num = this.IsCareerContainsTask(thisCareer, updateCareer.tasks.get_Item(i).taskId);
			if (num >= 0)
			{
				if (thisCareer.tasks.get_Item(num).status != CareerTask.TaskStatus.Finish && updateCareer.tasks.get_Item(i).status == CareerTask.TaskStatus.Finish)
				{
					this.MessageOneTaskFinish(thisCareer.tasks.get_Item(num).taskId);
				}
				thisCareer.tasks.set_Item(num, updateCareer.tasks.get_Item(i));
			}
			else
			{
				thisCareer.tasks.Add(updateCareer.tasks.get_Item(i));
			}
		}
		if (this.IsCareerAllTaskFinished(thisCareer.tasks))
		{
			this.MessageAllTaskFinish(thisCareer.dstCareer);
		}
	}

	private bool IsCareerAllTaskFinished(int profession)
	{
		List<CareerTask> careerTasks = this.GetCareerTasks(profession);
		return careerTasks != null && this.IsCareerAllTaskFinished(careerTasks);
	}

	private bool IsCareerAllTaskFinished(List<CareerTask> tasks)
	{
		for (int i = 0; i < tasks.get_Count(); i++)
		{
			if (tasks.get_Item(i).status != CareerTask.TaskStatus.Finish)
			{
				return false;
			}
		}
		return true;
	}

	private int IsCareerContainsTask(OneCareer thisCareer, int taskId)
	{
		for (int i = 0; i < thisCareer.tasks.get_Count(); i++)
		{
			if (thisCareer.tasks.get_Item(i).taskId == taskId)
			{
				return i;
			}
		}
		return -1;
	}

	private void DeleteCareerTask(int profession)
	{
		if (this.m_rpcCareerTasks == null)
		{
			return;
		}
		for (int i = 0; i < this.m_rpcCareerTasks.get_Count(); i++)
		{
			if (this.m_rpcCareerTasks.get_Item(i).dstCareer == profession)
			{
				this.m_rpcCareerTasks.RemoveAt(i);
				return;
			}
		}
	}

	private void MessageOneTaskFinish(int taskId)
	{
		ZhuanZhiRenWu zhuanZhiRenWu = DataReader<ZhuanZhiRenWu>.Get(taskId);
		if (zhuanZhiRenWu != null)
		{
			string chineseContent = GameDataUtils.GetChineseContent(1410, false);
			UIManagerControl.Instance.ShowToastText(string.Format(chineseContent, ChangeCareerManager.GetTaskName(zhuanZhiRenWu)));
		}
	}

	private void MessageAllTaskFinish(int profession)
	{
		ZhuanZhiJiChuPeiZhi dataZZ = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (dataZZ == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			UIQueueManager.Instance.Push(delegate
			{
				this.JustMessageAllTaskFinish(dataZZ);
			}, PopPriority.Normal, PopCondition.BackToCity);
			return;
		}
		if (UIManagerControl.Instance.IsOpen("ChangeCareerUI"))
		{
			return;
		}
		this.JustMessageAllTaskFinish(dataZZ);
	}

	private void JustMessageAllTaskFinish(ZhuanZhiJiChuPeiZhi dataZZ)
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		if (this.m_rpcCareerTasks != null)
		{
			this.m_rpcCareerTasks.Clear();
		}
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ChangeCareerRes>(new NetCallBackMethod<ChangeCareerRes>(this.OnChangeCareerRes));
		NetworkManager.AddListenEvent<PushCareerTask2Client>(new NetCallBackMethod<PushCareerTask2Client>(this.OnPushCareerTask2Client));
		NetworkManager.AddListenEvent<PushHadCareer2Client>(new NetCallBackMethod<PushHadCareer2Client>(this.OnPushHadCareer2Client));
		NetworkManager.AddListenEvent<RoleChangeCareerNty>(new NetCallBackMethod<RoleChangeCareerNty>(this.OnRoleChangeCareerNty));
		NetworkManager.AddListenEvent<SelectCareerRes>(new NetCallBackMethod<SelectCareerRes>(this.OnSelectCareerRes));
		NetworkManager.AddListenEvent<NtyReadyChangeCareer>(new NetCallBackMethod<NtyReadyChangeCareer>(this.OnNtyReadyChangeCareer));
	}

	public void SendChangeCareer(int profession)
	{
		if (profession <= 0)
		{
			return;
		}
		this.RoleChangeProfession = profession;
		NetworkManager.Send(new ChangeCareerReq
		{
			career = (CareerType.CT)profession
		}, ServerType.Data);
	}

	private void SendSelectCareer(int profession)
	{
		if (profession <= 0)
		{
			return;
		}
		NetworkManager.Send(new SelectCareerReq
		{
			career = (CareerType.CT)profession
		}, ServerType.Data);
	}

	public void SendInChallengeNty()
	{
		NetworkManager.Send(new InChallengeNty(), ServerType.Data);
	}

	public void SendOutChallengeNty()
	{
		NetworkManager.Send(new OutChallengeNty(), ServerType.Data);
	}

	private void OnChangeCareerRes(short state, ChangeCareerRes down = null)
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
		this.DeleteCareerTask(down.dstCareer);
		if (ChangeCareerUIView.Instance != null && ChangeCareerUIView.Instance.get_gameObject().get_activeSelf())
		{
			ChangeCareerUIView.Instance.RefreshTaskInfo();
		}
	}

	private void OnPushCareerTask2Client(short state, PushCareerTask2Client down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.tasks.get_Count(); i++)
			{
				this.UpdateCareer(down.tasks.get_Item(i));
			}
			if (ChangeCareerUIView.Instance != null && ChangeCareerUIView.Instance.get_gameObject().get_activeSelf())
			{
				ChangeCareerUIView.Instance.RefreshTaskInfo();
			}
		}
	}

	private void OnPushHadCareer2Client(short state, PushHadCareer2Client down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.hadCareer.get_Count(); i++)
			{
				if (!this.m_hadCareerIDs.Contains(down.hadCareer.get_Item(i)))
				{
					this.m_hadCareerIDs.Add(down.hadCareer.get_Item(i));
				}
			}
		}
	}

	private void OnRoleChangeCareerNty(short state, RoleChangeCareerNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EntityWorld.Instance.EntSelf.TypeID = this.RoleChangeProfession;
			EventDispatcher.Broadcast("ChangeCareerManager.RoleSelfProfessionChange");
			this.ChangeCareerSuccess(down);
		}
	}

	private void OnSelectCareerRes(short state, SelectCareerRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnNtyReadyChangeCareer(short state, NtyReadyChangeCareer down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast("ChangeCareerManager.RoleSelfProfessionReadyChange");
	}

	private void ChangeCareerSuccess(RoleChangeCareerNty down)
	{
		List<LevelUpUnitData> list = new List<LevelUpUnitData>();
		LevelUpUnitData levelUpUnitData = new LevelUpUnitData();
		string beginStr = AttrUtility.GetAttrName(GameData.AttrType.Fighting) + ":";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldFighting;
		levelUpUnitData.End = (float)down.newFighting;
		list.Add(levelUpUnitData);
		levelUpUnitData = new LevelUpUnitData();
		beginStr = "生命:";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldHp;
		levelUpUnitData.End = (float)down.newHp;
		list.Add(levelUpUnitData);
		levelUpUnitData = new LevelUpUnitData();
		beginStr = "攻击:";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldAttack;
		levelUpUnitData.End = (float)down.newAttack;
		list.Add(levelUpUnitData);
		levelUpUnitData = new LevelUpUnitData();
		beginStr = "防御:";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldDefence;
		levelUpUnitData.End = (float)down.newDefence;
		list.Add(levelUpUnitData);
		levelUpUnitData = new LevelUpUnitData();
		beginStr = "格挡:";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldParryRatio;
		levelUpUnitData.End = (float)down.newParryRatio;
		list.Add(levelUpUnitData);
		levelUpUnitData = new LevelUpUnitData();
		beginStr = "暴击:";
		levelUpUnitData.BeginStr = beginStr;
		levelUpUnitData.Begin = (float)down.oldCritRatio;
		levelUpUnitData.End = (float)down.newCritRatio;
		list.Add(levelUpUnitData);
		ChangeCareerSuccessUIViewModel.Instance.ShowChangeCareer(list, this.RoleChangeProfession);
	}

	public void OnClickSelectCareer(int profession)
	{
		if (profession <= 0)
		{
			return;
		}
		if (DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession) == null)
		{
			return;
		}
		if (this.IsCareerChanged(profession))
		{
			this.SendChangeCareer(profession);
			return;
		}
		if (this.IsCareerChanged())
		{
			this.ChangeCareerNoFirstTime(profession);
			return;
		}
		if (this.GetTaskFirstTime())
		{
			this.SendSelectCareer(profession);
			return;
		}
		this.SendSelectCareer(profession);
	}

	public bool IsCareerChangedOrSelectedChange(int profession)
	{
		return this.m_hadCareerIDs.Contains(profession) || this.IsContainsCarrerInTasks(profession);
	}

	private bool IsContainsCarrerInTasks(int profession)
	{
		if (this.m_rpcCareerTasks == null)
		{
			return false;
		}
		for (int i = 0; i < this.m_rpcCareerTasks.get_Count(); i++)
		{
			if (this.m_rpcCareerTasks.get_Item(i).dstCareer == profession)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetTaskFirstTime()
	{
		return !this.IsCareerChanged() && (this.m_rpcCareerTasks == null || this.m_rpcCareerTasks.get_Count() <= 0);
	}

	public bool IsCareerChanged()
	{
		return this.m_hadCareerIDs.get_Count() > 0;
	}

	public bool IsCareerChanged(int profession)
	{
		return this.m_hadCareerIDs.Contains(profession);
	}

	public static string GetTaskName(ZhuanZhiRenWu dataRW)
	{
		string result = string.Empty;
		if (dataRW.missionType == 1)
		{
			if (dataRW.missionData.get_Count() >= 1)
			{
				ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(dataRW.missionData.get_Item(0));
				if (zhuXianPeiZhi != null)
				{
					result = string.Format(dataRW.message, GameDataUtils.GetChineseContent(zhuXianPeiZhi.name, false));
				}
			}
		}
		else if (dataRW.missionType == 2)
		{
			result = dataRW.message;
		}
		else if (dataRW.missionType == 3)
		{
			if (dataRW.missionData.get_Count() >= 2)
			{
				result = string.Format(dataRW.message, GemGlobal.GetGemName(dataRW.missionData.get_Item(0), dataRW.missionData.get_Item(1)));
			}
		}
		else if (dataRW.missionType == 4)
		{
			result = dataRW.message;
		}
		else if (dataRW.missionType == 5 && dataRW.missionData.get_Count() >= 1)
		{
			result = string.Format(dataRW.message, dataRW.missionData.get_Item(0));
		}
		return result;
	}

	private void ChangeCareerNoFirstTime(int profession)
	{
		if (profession <= 0)
		{
			return;
		}
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return;
		}
		int vIP = ChangeCareerManager.GetVIP(profession);
		if (EntityWorld.Instance.EntSelf.VipLv < vIP)
		{
			UIManagerControl.Instance.ShowToastText("VIP等级不足, 需要VIP等级" + vIP);
			return;
		}
		if (EntityWorld.Instance.EntSelf.Diamond < ChangeCareerManager.GetDiamond(profession))
		{
			UIManagerControl.Instance.ShowToastText("钻石不足");
			return;
		}
		string text = "是否选择{0}";
		string chineseContent = GameDataUtils.GetChineseContent(zhuanZhiJiChuPeiZhi.jobName, false);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("选择职业", string.Format(text, chineseContent), null, delegate
		{
			this.SendChangeCareer(profession);
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	public static int GetChangeCareerID(int index)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return 0;
		}
		if (zhuanZhiJiChuPeiZhi.jobList.get_Count() >= 2)
		{
			if (index < zhuanZhiJiChuPeiZhi.jobList.get_Count())
			{
				return zhuanZhiJiChuPeiZhi.jobList.get_Item(index);
			}
		}
		else if (zhuanZhiJiChuPeiZhi.jobList.get_Count() >= 1)
		{
			ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhiBasic = ChangeCareerManager.GetZhuanZhiJiChuPeiZhiBasic(zhuanZhiJiChuPeiZhi.job, zhuanZhiJiChuPeiZhi.jobList.get_Item(0));
			if (zhuanZhiJiChuPeiZhiBasic.jobList.get_Count() >= 2 && index < zhuanZhiJiChuPeiZhiBasic.jobList.get_Count())
			{
				return zhuanZhiJiChuPeiZhiBasic.jobList.get_Item(index);
			}
		}
		return 0;
	}

	private static ZhuanZhiJiChuPeiZhi GetZhuanZhiJiChuPeiZhiBasic(int id1, int id2)
	{
		List<ZhuanZhiJiChuPeiZhi> dataList = DataReader<ZhuanZhiJiChuPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = dataList.get_Item(i);
			if (zhuanZhiJiChuPeiZhi.jobList.Contains(id1) && zhuanZhiJiChuPeiZhi.jobList.Contains(id2))
			{
				return zhuanZhiJiChuPeiZhi;
			}
		}
		return null;
	}

	public static int GetVIP(int profession)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return 0;
		}
		return zhuanZhiJiChuPeiZhi.vipLevel;
	}

	public static int GetDiamond(int profession)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return 0;
		}
		return zhuanZhiJiChuPeiZhi.price;
	}
}
