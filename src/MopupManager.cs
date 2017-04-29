using GameData;
using Package;
using System;
using UnityEngine;

public class MopupManager
{
	private int currentMopupInstance;

	private FuBenJiChuPeiZhi currentInstanceData;

	public int mopupTimes;

	public int remainingMopupTimes = 20;

	private bool isMutliMopup;

	private static MopupManager m_instance;

	public static MopupManager Instance
	{
		get
		{
			if (MopupManager.m_instance == null)
			{
				MopupManager.m_instance = new MopupManager();
			}
			return MopupManager.m_instance;
		}
	}

	private MopupManager()
	{
		EventDispatcher.AddListener(DungeonManagerEvent.OnMopUpDungeonRes, new Callback(this.OnMopUpDungeonRes));
	}

	public void StartMopup(int instanceID, int times)
	{
		if (times > 1)
		{
			this.isMutliMopup = true;
		}
		else
		{
			this.isMutliMopup = false;
		}
		this.mopupTimes = times;
		this.currentMopupInstance = instanceID;
		this.currentInstanceData = DataReader<FuBenJiChuPeiZhi>.Get(instanceID);
		if (this.currentInstanceData == null)
		{
			Debug.LogWarning("没找到副本ID " + instanceID);
			return;
		}
		this.GoOnMopUp();
	}

	private bool CheckChallangeTimes()
	{
		DungeonInfo dungeonInfo = DungeonManager.Instance.GetDungeonInfo(this.currentMopupInstance);
		if (dungeonInfo == null)
		{
			return false;
		}
		if (dungeonInfo.remainingChallengeTimes < this.mopupTimes || dungeonInfo.remainingChallengeTimes == 0)
		{
			if (dungeonInfo.resetChallengeTimes >= VIPPrivilegeManager.Instance.GetVipTimesByType(4))
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505118, false), 1f, 1f);
			}
			DungeonManager.Instance.BuyChallengeTimes(this.currentMopupInstance, delegate
			{
				if (this.isMutliMopup)
				{
					this.mopupTimes = DungeonManager.Instance.GetDungeonInfo(this.currentMopupInstance).remainingChallengeTimes;
					if (this.mopupTimes > 10)
					{
						this.mopupTimes = 10;
					}
				}
				else
				{
					this.mopupTimes = 1;
				}
				this.GoOnMopUp();
			});
			return false;
		}
		return true;
	}

	private bool CheckEnergy()
	{
		if (DataReader<ZhuXianPeiZhi>.Get(this.currentInstanceData.id).expendVit * this.mopupTimes > EntityWorld.Instance.EntSelf.Energy)
		{
			UIManagerControl.Instance.ShowToastText("没有足够的体力");
			EnergyManager.Instance.BuyEnergy(new Action(this.GoOnMopUp));
			return false;
		}
		return true;
	}

	private bool CheckMopupTimes()
	{
		int vipTimesByType = VIPPrivilegeManager.Instance.GetVipTimesByType(3);
		int usedFreeMopUpTimes = DungeonManager.Instance.usedFreeMopUpTimes;
		Debug.LogError(string.Concat(new object[]
		{
			"remaingNum  ",
			vipTimesByType - usedFreeMopUpTimes,
			"  maxNum  ",
			vipTimesByType,
			"  useNum  ",
			usedFreeMopUpTimes
		}));
		if (vipTimesByType != -1)
		{
			int num = vipTimesByType - usedFreeMopUpTimes;
			if (this.mopupTimes > num)
			{
				int needBuy = this.mopupTimes - num;
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502210, false), 1f, 1f);
				string chineseContent = GameDataUtils.GetChineseContent(502210, false);
				string text = GameDataUtils.GetChineseContent(502211, false);
				string text2 = ((int)float.Parse(DataReader<GlobalParams>.Get("per_time_mopup_need_diamond").value) * needBuy).ToString();
				text = text.Replace("x{0}", text2);
				text = text.Replace("x{1}", needBuy.ToString());
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, text, null, delegate
				{
					DungeonManager.Instance.SendBuyMopUpTimeReq(this.currentMopupInstance, needBuy, new Action(this.GoOnMopUp));
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
				return false;
			}
		}
		return true;
	}

	private bool CheckBackageNotFull()
	{
		return !BackpackManager.Instance.ShowBackpackFull();
	}

	private bool CheckMopupCondition()
	{
		return this.CheckEnergy() && this.CheckChallangeTimes() && this.CheckMopupTimes() && this.CheckBackageNotFull();
	}

	private void GoOnMopUp()
	{
		if (this.CheckMopupCondition())
		{
			GlobalManager.Instance.CollectMopupDropitems(this.mopupTimes);
			DungeonManager.Instance.SendMopUpDungeonReq(this.currentMopupInstance, this.mopupTimes);
			EventDispatcher.Broadcast(EventNames.CloseMopupBtn);
		}
	}

	private void OnMopUpDungeonRes()
	{
		InstanceCleanFinishUI instanceCleanFinishUI = UIManagerControl.Instance.OpenUI("InstanceCleanFinishUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as InstanceCleanFinishUI;
		if (GlobalManager.Instance.ListDropGoods != null)
		{
			instanceCleanFinishUI.AddInstanceCleanFinishItems(GlobalManager.Instance.ListDropGoods);
		}
		else
		{
			Debug.LogError("GlobalManager.Instance.DropGoods == null");
		}
	}
}
