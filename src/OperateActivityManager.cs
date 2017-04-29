using GameData;
using LuaFramework;
using Package;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XNetwork;

public class OperateActivityManager : BaseSubSystemManager
{
	private const int TIME = 30000;

	public int CurrentOpenTypeId = -1;

	private uint mCheckWifiID;

	private int mDownloadingAcId = -1;

	private int mDownloadingPackId = -1;

	private int mDownloadVersion = -1;

	private bool mIsNeedRestart;

	private bool mUsing4GDownload;

	private Dictionary<int, float> mPackagesSize;

	private bool mIsForecUpdate;

	private static readonly string[] ChineseNumber = new string[]
	{
		"零",
		"一",
		"二",
		"三",
		"四",
		"五",
		"六",
		"七",
		"八",
		"九",
		"十"
	};

	private static string AssetBundleBuildRoot = Util.DataPath;

	private List<ActivityInfo> AllActivityInfoList = new List<ActivityInfo>();

	private List<UpdateAcInfo> mLocalUpdateGiftInfos;

	private List<UpdateAcInfo> mServerUpdateGiftInfos;

	private static OperateActivityManager instance;

	public List<UpdateAcInfo> LocalUpdateGiftInfos
	{
		get
		{
			return this.mLocalUpdateGiftInfos;
		}
	}

	public List<UpdateAcInfo> ServerUpdateGiftInfos
	{
		get
		{
			return this.mServerUpdateGiftInfos;
		}
	}

	public static OperateActivityManager Instance
	{
		get
		{
			if (OperateActivityManager.instance == null)
			{
				OperateActivityManager.instance = new OperateActivityManager();
			}
			return OperateActivityManager.instance;
		}
	}

	public bool IsNeedUpdatePack
	{
		get
		{
			return false;
		}
	}

	public bool IsWifi
	{
		get
		{
			return Application.get_internetReachability() == 2;
		}
	}

	public int CurrentVersion
	{
		get
		{
			int num = int.Parse(GameManager.Instance.GetLocalVersions()[2]);
			if (num < 1)
			{
				num = 1;
			}
			return num;
		}
	}

	public int DownloadPackID
	{
		get
		{
			return this.mDownloadingAcId;
		}
	}

	public bool isForecUpdate
	{
		get
		{
			return this.mIsForecUpdate;
		}
	}

	private OperateActivityManager()
	{
	}

	private static string LocalVersionFileName()
	{
		return Path.Combine(OperateActivityManager.AssetBundleBuildRoot, "local_version.txt");
	}

	public List<ActivityInfo> GetOpenActivitys()
	{
		List<ActivityInfo> list = new List<ActivityInfo>();
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			if (!this.AllActivityInfoList.get_Item(i).overdueFlag)
			{
				if (this.AllActivityInfoList.get_Item(i).typeId != 5)
				{
					if (this.AllActivityInfoList.get_Item(i).typeId != 4)
					{
						list.Add(this.AllActivityInfoList.get_Item(i));
					}
				}
			}
		}
		return list;
	}

	public bool isSevenDayOn()
	{
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			if (!this.AllActivityInfoList.get_Item(i).overdueFlag && this.AllActivityInfoList.get_Item(i).typeId == 4)
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateInfo(ActivityInfo ai)
	{
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			if (this.AllActivityInfoList.get_Item(i).typeId == ai.typeId)
			{
				this.AllActivityInfoList.set_Item(i, ai);
				return;
			}
		}
		this.AllActivityInfoList.Add(ai);
	}

	public override void Init()
	{
		base.Init();
		this.mDownloadVersion = this.CurrentVersion;
		this.AddCheckIsWifi();
	}

	public override void Release()
	{
		this.AllActivityInfoList.Clear();
		this.RemoveCheckIsWifi();
		this.mDownloadVersion = -1;
		this.mIsNeedRestart = false;
	}

	protected override void AddListener()
	{
		global::NetworkManager.AddListenEvent<ActivityChangeNty>(new NetCallBackMethod<ActivityChangeNty>(this.OnActivityChangeNty));
		global::NetworkManager.AddListenEvent<ActivityLoginPush>(new NetCallBackMethod<ActivityLoginPush>(this.OnActivityLoginPush));
		global::NetworkManager.AddListenEvent<ReceivePrizeRes>(new NetCallBackMethod<ReceivePrizeRes>(this.OnReceivePrizeRes));
		global::NetworkManager.AddListenEvent<FirstOpenRes>(new NetCallBackMethod<FirstOpenRes>(this.OnFirstOpenRes));
		global::NetworkManager.AddListenEvent<UpdateAwardPush>(new NetCallBackMethod<UpdateAwardPush>(this.OnUpdateAwardPush));
		global::NetworkManager.AddListenEvent<UpdateAwardRes>(new NetCallBackMethod<UpdateAwardRes>(this.OnUpdateAwardRes));
		global::NetworkManager.AddListenEvent<DownLoadFinishRes>(new NetCallBackMethod<DownLoadFinishRes>(this.OnDownLoadFinishRes));
		global::NetworkManager.AddListenEvent<GetUpdateAwardRes>(new NetCallBackMethod<GetUpdateAwardRes>(this.OnGetUpdateAwardRes));
		EventDispatcher.AddListener<int>(EventNames.OpenActivityUI, new Callback<int>(this.OnOpenActivityUI));
		EventDispatcher.AddListener<int>(EventNames.UpdateGrowUpPlanReward, new Callback<int>(this.OnUpdateGrowUpPlanReward));
		EventDispatcher.AddListener(EventNames.OnGetSignChangedNty, new Callback(this.OnGetSignChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetMonthTotalChangeNty, new Callback(this.OnGetSignChangedNty));
	}

	public void SendUpdateAwardReq(int acId)
	{
		Debug.Log("开始下载[" + acId + "]更新包");
		global::NetworkManager.Send(new UpdateAwardReq
		{
			acId = acId
		}, ServerType.Data);
		this.mDownloadingAcId = acId;
	}

	public void SendDownLoadFinishReq(int acId)
	{
		Debug.Log("下载完成[" + acId + "]更新包");
		global::NetworkManager.Send(new DownLoadFinishReq
		{
			acId = acId
		}, ServerType.Data);
		this.mDownloadingAcId = -1;
	}

	public void SendGetUpdateAwardReq(int acId)
	{
		global::NetworkManager.Send(new GetUpdateAwardReq
		{
			acId = acId
		}, ServerType.Data);
	}

	private void OnActivityChangeNty(short state, ActivityChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.activitiesInfo.get_Count(); i++)
			{
				ActivityInfo activityInfo = down.activitiesInfo.get_Item(i);
				if (activityInfo.typeId == 10 && activityInfo.overdueFlag)
				{
					LimitTimeMarketManager.Instance.IsPush = false;
				}
				this.UpdateInfo(activityInfo);
			}
			this.SortedInfoList(false);
			this.BroadcastRefreshEvent();
		}
	}

	private void OnActivityLoginPush(short state, ActivityLoginPush down = null)
	{
		if (down != null)
		{
			for (int i = 0; i < down.activitiesInfo.get_Count(); i++)
			{
				ActivityInfo ai = down.activitiesInfo.get_Item(i);
				this.UpdateInfo(ai);
			}
			this.SortedInfoList(true);
			this.BroadcastRefreshEvent();
			Activity7DayManager.Instance.startDay = down.startDay;
			Activity7DayManager.Instance.endTimeouts = down.endTimeouts;
		}
	}

	private void OnReceivePrizeRes(short state, ReceivePrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513103, false), 1f, 2f);
	}

	private void OnFirstOpenRes(short state, FirstOpenRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnUpdateAwardPush(short state, UpdateAwardPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.mServerUpdateGiftInfos == null)
		{
			this.mServerUpdateGiftInfos = down.ac;
			this.mLocalUpdateGiftInfos = new List<UpdateAcInfo>();
			for (int i = 0; i < this.mServerUpdateGiftInfos.get_Count(); i++)
			{
				UpdateAcInfo updateAcInfo = new UpdateAcInfo();
				updateAcInfo.acId = this.mServerUpdateGiftInfos.get_Item(i).acId;
				updateAcInfo.status = this.mServerUpdateGiftInfos.get_Item(i).status;
				this.mLocalUpdateGiftInfos.Add(updateAcInfo);
			}
		}
		else
		{
			using (List<UpdateAcInfo>.Enumerator enumerator = down.ac.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UpdateAcInfo current = enumerator.get_Current();
					bool flag = true;
					for (int j = 0; j < this.mServerUpdateGiftInfos.get_Count(); j++)
					{
						if (this.mServerUpdateGiftInfos.get_Item(j).acId == current.acId)
						{
							this.mServerUpdateGiftInfos.get_Item(j).status = current.status;
							this.mLocalUpdateGiftInfos.get_Item(j).status = current.status;
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.mServerUpdateGiftInfos.Add(current);
						UpdateAcInfo updateAcInfo2 = new UpdateAcInfo();
						updateAcInfo2.acId = current.acId;
						updateAcInfo2.status = current.status;
						this.mLocalUpdateGiftInfos.Add(updateAcInfo2);
					}
				}
			}
			EventDispatcher.Broadcast(EventNames.UpdateAwardPush);
		}
		if (this.IsNeedUpdatePack)
		{
			this.AddCheckIsWifi();
		}
	}

	private void OnUpdateAwardRes(short state, UpdateAwardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.UpdateStartDownloadRes);
	}

	private void OnDownLoadFinishRes(short state, DownLoadFinishRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGetUpdateAwardRes(short state, GetUpdateAwardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.GetUpdateRewardRes);
		if (!this.IsNeedUpdatePack)
		{
			EventDispatcher.Broadcast(EventNames.GetAllUpdateReward);
		}
	}

	public void SortedInfoList(bool filterOverdue)
	{
		SortedList<int, ActivityInfo> sortedList = new SortedList<int, ActivityInfo>();
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			ActivityInfo activityInfo = this.AllActivityInfoList.get_Item(i);
			HuoDongMuLu huoDongMuLu = DataReader<HuoDongMuLu>.Get(activityInfo.typeId);
			if (huoDongMuLu != null)
			{
				if (filterOverdue)
				{
					if (!activityInfo.overdueFlag)
					{
						int priority = huoDongMuLu.priority;
						sortedList.Remove(priority);
						sortedList.Add(priority, activityInfo);
					}
				}
				else
				{
					int priority2 = huoDongMuLu.priority;
					sortedList.Remove(priority2);
					sortedList.Add(priority2, activityInfo);
				}
			}
		}
		this.AllActivityInfoList.Clear();
		using (IEnumerator<KeyValuePair<int, ActivityInfo>> enumerator = sortedList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActivityInfo> current = enumerator.get_Current();
				this.AllActivityInfoList.Add(current.get_Value());
			}
		}
	}

	public void BroadcastRefreshEvent()
	{
		bool arg = false;
		bool flag = true;
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			ActivityInfo activityInfo = this.AllActivityInfoList.get_Item(i);
			if (activityInfo.canGetFlag && activityInfo.typeId != 4)
			{
				arg = true;
			}
			if (!activityInfo.overdueFlag)
			{
				flag = false;
			}
		}
		if (!flag)
		{
			EventDispatcher.Broadcast(EventNames.RefreshActivityInfo);
		}
		ActivityInfo activityInfo2 = this.GetActivityInfo(1);
		TownUI.IsFirstChargeOnSelf = (activityInfo2 != null && !activityInfo2.overdueFlag);
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTownUiFirstPayGift, activityInfo2 != null && !activityInfo2.overdueFlag);
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsTownUiFirstPayGift, arg);
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsTownUiOperateActivity, arg);
	}

	public void BroadcastRefreshTownTipEvent()
	{
		bool arg = false;
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			ActivityInfo activityInfo = this.AllActivityInfoList.get_Item(i);
			if (activityInfo.canGetFlag && activityInfo.typeId != 4)
			{
				arg = true;
				break;
			}
		}
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsTownUiOperateActivity, arg);
	}

	public bool HasActivity()
	{
		for (int i = 0; i < this.AllActivityInfoList.get_Count(); i++)
		{
			if (!this.AllActivityInfoList.get_Item(i).overdueFlag && this.AllActivityInfoList.get_Item(i).typeId != 5 && this.AllActivityInfoList.get_Item(i).typeId != 4)
			{
				return true;
			}
		}
		return false;
	}

	public int GetDefaultOpenTypeID()
	{
		int result = -1;
		ActivityInfo activityInfo = this.GetActivityInfo(this.CurrentOpenTypeId);
		if (activityInfo != null && !activityInfo.overdueFlag && activityInfo.typeId != 4)
		{
			result = this.CurrentOpenTypeId;
		}
		else
		{
			using (List<ActivityInfo>.Enumerator enumerator = this.AllActivityInfoList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActivityInfo current = enumerator.get_Current();
					if (!current.overdueFlag && current.typeId != 4)
					{
						result = current.typeId;
						break;
					}
				}
			}
		}
		return result;
	}

	public ActivityInfo GetActivityInfo(int id)
	{
		using (List<ActivityInfo>.Enumerator enumerator = this.AllActivityInfoList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ActivityInfo current = enumerator.get_Current();
				if (current.typeId == id && current.typeId != 4)
				{
					return current;
				}
			}
		}
		return null;
	}

	public ActivityInfo GetActivityInfoCurrent()
	{
		return this.GetActivityInfo(this.CurrentOpenTypeId);
	}

	public void OnOpenActivityUI(int id)
	{
		this.CurrentOpenTypeId = id;
		if (this.HasActivity())
		{
			UIManagerControl.Instance.OpenUI("OperateActivityUI", null, true, UIType.FullScreen);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513102, false), 1f, 2f);
		}
	}

	public bool ShowFirstPayButtonInTownUI()
	{
		ActivityInfo activityInfo = this.GetActivityInfo(1);
		return activityInfo != null && !activityInfo.overdueFlag;
	}

	public bool GetActivityRedTips(int ActivityTypeId)
	{
		if (ActivityTypeId == 4)
		{
			return SignInManager.Instance.CheckSeverSignBadage();
		}
		if (ActivityTypeId == 8)
		{
			return GrowUpPlanManager.Instance.CheckGrwoUpPlanReward();
		}
		if (ActivityTypeId == 1)
		{
			return ConsumeRechargeManager.Instance.GetRechargeGiftPoint();
		}
		if (ActivityTypeId == 6)
		{
			return ConsumeRechargeManager.Instance.GetConsumeGiftPoint();
		}
		if (ActivityTypeId == 2)
		{
			return Activity7DayManager.Instance.GetRedPoint();
		}
		ActivityInfo activityInfo = this.GetActivityInfo(ActivityTypeId);
		return activityInfo != null && activityInfo.canGetFlag;
	}

	public void UpdateActivityRedTips(int ActivityTypeId)
	{
		EventDispatcher.Broadcast<int>(EventNames.OperateActivityTipsUpdate, ActivityTypeId);
		this.BroadcastRefreshTownTipEvent();
	}

	public void OnUpdateActivity7DayReward()
	{
		ActivityInfo activityInfo = this.GetActivityInfo(2);
		if (activityInfo != null)
		{
			activityInfo.canGetFlag = Activity7DayManager.Instance.GetRedPoint();
		}
		this.UpdateActivityRedTips(2);
	}

	public void OnUpdateGrowUpPlanReward(int type)
	{
		ActivityInfo activityInfo = this.GetActivityInfo(8);
		if (activityInfo != null)
		{
			activityInfo.canGetFlag = GrowUpPlanManager.Instance.CheckGrwoUpPlanReward();
		}
		this.UpdateActivityRedTips(8);
	}

	public void OnGetSignChangedNty()
	{
		ActivityInfo activityInfo = this.GetActivityInfo(4);
		if (activityInfo != null)
		{
			activityInfo.canGetFlag = SignInManager.Instance.CheckSeverSignBadage();
		}
		this.UpdateActivityRedTips(4);
	}

	public void OnUpdateOperateAcPush()
	{
		ActivityInfo activityInfo = this.GetActivityInfo(1);
		if (activityInfo != null)
		{
			activityInfo.canGetFlag = ConsumeRechargeManager.Instance.GetRechargeGiftPoint();
		}
		this.UpdateActivityRedTips(1);
		ActivityInfo activityInfo2 = this.GetActivityInfo(6);
		if (activityInfo2 != null)
		{
			activityInfo2.canGetFlag = ConsumeRechargeManager.Instance.GetConsumeGiftPoint();
		}
		this.UpdateActivityRedTips(6);
	}

	public void AddCheckIsWifi()
	{
	}

	private void CheckIsWifi()
	{
		if (!UIManagerControl.Instance.IsOpen("TownUI") && !this.mIsForecUpdate)
		{
			if (this.mDownloadingAcId > 0)
			{
				Downloader.Instance.OnPauseClick();
				this.mDownloadingAcId = -1;
				Debug.Log("===[暂停下载]===");
			}
		}
		else if (!this.IsWifi && this.mDownloadingAcId > 0 && !this.mUsing4GDownload)
		{
			Downloader.Instance.OnPauseClick();
			this.mDownloadingAcId = -1;
			Debug.Log("===[暂停下载]===");
		}
		else if (!this.mIsForecUpdate && this.IsWifi && this.mDownloadingAcId == -1)
		{
			using (List<UpdateAcInfo>.Enumerator enumerator = this.mLocalUpdateGiftInfos.GetEnumerator())
			{
				UpdateAcInfo info;
				while (enumerator.MoveNext())
				{
					info = enumerator.get_Current();
					if (info.status == UpdateAcInfo.AcStep.STEP.Ready)
					{
						GengXinYouLi gengXinYouLi = DataReader<GengXinYouLi>.Get(info.acId);
						if (gengXinYouLi != null && gengXinYouLi.FinishPar > this.mDownloadVersion)
						{
							this.StartDownloadPackageById(info.acId, false);
							UpdateAcInfo updateAcInfo = this.mLocalUpdateGiftInfos.Find((UpdateAcInfo e) => e.acId == info.acId);
							if (updateAcInfo != null)
							{
								updateAcInfo.status = UpdateAcInfo.AcStep.STEP.Start;
							}
							Debug.Log("===[开始下载]===");
							return;
						}
					}
				}
			}
			this.RemoveCheckIsWifi();
		}
	}

	private void UpdatePackageProgress(float curSize, float allSize, string fileName)
	{
		Debug.Log(string.Concat(new object[]
		{
			"已下载[",
			curSize,
			"]KB, 总大小[",
			allSize,
			"]KB"
		}));
		EventDispatcher.Broadcast<int, float, float>(EventNames.UpdateDownloadProgress, this.mDownloadingAcId, curSize, allSize);
	}

	private void DownloadPackageFinish(bool isDone)
	{
		if (this.mDownloadingAcId > 0 && isDone)
		{
			this.SetFinishedUpdatePackageId();
			UpdateAcInfo updateAcInfo = this.mLocalUpdateGiftInfos.Find((UpdateAcInfo e) => e.acId == this.mDownloadingAcId);
			if (updateAcInfo != null)
			{
				updateAcInfo.status = UpdateAcInfo.AcStep.STEP.Finish;
			}
			EventDispatcher.Broadcast<int>(EventNames.UpdateDownloadFinish, this.mDownloadingAcId);
			this.SendDownLoadFinishReq(this.mDownloadingAcId);
			if (this.mIsForecUpdate)
			{
				this.RestartConfirm(GameDataUtils.GetChineseContent(513157, false));
			}
			else
			{
				this.mDownloadVersion++;
			}
			this.mIsNeedRestart = true;
		}
		else
		{
			UpdateAcInfo updateAcInfo2 = this.mLocalUpdateGiftInfos.Find((UpdateAcInfo e) => e.acId == this.mDownloadingAcId);
			if (updateAcInfo2 != null)
			{
				updateAcInfo2.status = UpdateAcInfo.AcStep.STEP.Ready;
			}
			this.mDownloadingAcId = -1;
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513150, false));
			this.RemoveCheckIsWifi();
		}
		EventDispatcher.Broadcast(EventNames.UpdateAwardPush);
		if (!this.IsNeedUpdatePack)
		{
			EventDispatcher.Broadcast(EventNames.GetAllUpdateReward);
		}
	}

	private void SetFinishedUpdatePackageId()
	{
		PlayerPrefs.SetInt("FinishedUpdateBaseId", this.mDownloadingPackId);
		string[] array = new string[]
		{
			"-1",
			"-1",
			"-1",
			"0"
		};
		array[2] = this.mDownloadingPackId.ToString();
		GameManager.Instance.UpdateClientVersionFileCfg(array);
	}

	private void RestartConfirm(string content)
	{
		UIManagerControl.Instance.OpenUI("DialogBoxUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
		DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(513156, false), content, delegate
		{
			ClientApp.QuitApp();
		}, GameDataUtils.GetChineseContent(513154, false), "button_orange_1", null);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void RemoveCheckIsWifi()
	{
		if (this.mCheckWifiID > 0u)
		{
			TimerHeap.DelTimer(this.mCheckWifiID);
			this.mCheckWifiID = 0u;
			Debug.Log("移除WIFI网络检查器");
		}
	}

	public void StartDownloadPackageById(int acId, bool isUsing4G)
	{
		this.mDownloadingAcId = acId;
		this.mUsing4GDownload = isUsing4G;
		this.SendUpdateAwardReq(this.mDownloadingAcId);
		GengXinYouLi gengXinYouLi = DataReader<GengXinYouLi>.Get(this.mDownloadingAcId);
		this.mDownloadingPackId = gengXinYouLi.FinishPar;
		DownloaderManager.Instance.DowanloadUpdatePackage(this.mDownloadingPackId, 0, new Action<float, float, string>(this.UpdatePackageProgress), new Action<bool>(this.DownloadPackageFinish), true, true);
	}

	public void CheckUpdateMaxLevel()
	{
	}

	public string SwitchChineseNumber(int num)
	{
		if (num > 99)
		{
			return this.SwitchChineseNumber(99);
		}
		if (num >= 20)
		{
			string text = OperateActivityManager.ChineseNumber[num / 10] + OperateActivityManager.ChineseNumber[10];
			if (num % 10 > 0)
			{
				text += OperateActivityManager.ChineseNumber[num % 10];
			}
			return text;
		}
		if (num > 10)
		{
			return OperateActivityManager.ChineseNumber[10] + OperateActivityManager.ChineseNumber[num % 10];
		}
		return OperateActivityManager.ChineseNumber[num];
	}

	public float GetPackageSizeById(int id)
	{
		if (this.mPackagesSize != null && this.mPackagesSize.ContainsKey(id))
		{
			return this.mPackagesSize.get_Item(id);
		}
		return -1f;
	}
}
