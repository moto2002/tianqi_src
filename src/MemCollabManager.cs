using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class MemCollabManager : BaseSubSystemManager
{
	public const int MAX_SECOND = 300;

	public const int MAX_MINUTE = 5;

	public bool IsMemCollabGoing;

	private List<int> m_cardIndexs;

	private List<int> m_successIndexs = new List<int>();

	private int m_flopTimes;

	private int m_todayRestTimes;

	private int m_todayExtendTimes;

	private List<MemoryFlopRankInfo> _RankInfo = new List<MemoryFlopRankInfo>();

	private static MemCollabManager m_Instance;

	public bool IsLockBeginMemoryFlop;

	public int LEVEL_S;

	public int LEVEL_A;

	public string ImgScore_S = string.Empty;

	public string ImgScore_A = string.Empty;

	public string ImgScore_B = string.Empty;

	public string Treasure_S = string.Empty;

	public string Treasure_A = string.Empty;

	public string Treasure_B = string.Empty;

	public int RewardID_S;

	public int RewardID_A;

	public int RewardID_B;

	private TimeCountDown timeCountDown;

	public List<int> CardIndexs
	{
		get
		{
			if (this.m_cardIndexs == null)
			{
				this.m_cardIndexs = new List<int>();
			}
			return this.m_cardIndexs;
		}
		set
		{
			this.m_cardIndexs = value;
			this.CheckCardIsValid();
		}
	}

	public List<int> SuccessIndexs
	{
		get
		{
			return this.m_successIndexs;
		}
		set
		{
			this.m_successIndexs = value;
		}
	}

	public int FlopTimes
	{
		get
		{
			return this.m_flopTimes;
		}
		set
		{
			this.m_flopTimes = value;
		}
	}

	public int TodayRestTimes
	{
		get
		{
			return this.m_todayRestTimes;
		}
		set
		{
			this.m_todayRestTimes = value;
			if (UIManagerControl.Instance.IsOpen("MemCollabUI"))
			{
				MemCollabUIView.Instance.SetRewardTimes(value);
			}
		}
	}

	public int TodayExtendTimes
	{
		get
		{
			return this.m_todayExtendTimes;
		}
		set
		{
			this.m_todayExtendTimes = value;
			if (UIManagerControl.Instance.IsOpen("MemCollabUI"))
			{
				MemCollabUIView.Instance.ShowRewardTimesAdd(this.IsTodayRestBuyTimesOn());
			}
		}
	}

	public int MaxExtendTimes
	{
		get
		{
			return VIPPrivilegeManager.Instance.GetVipTimesByType(13);
		}
	}

	public int MaxVipTimes
	{
		get
		{
			return VIPPrivilegeManager.Instance.GetMaxVipTimesByType(13);
		}
	}

	public List<MemoryFlopRankInfo> RankInfo
	{
		get
		{
			return this._RankInfo;
		}
		set
		{
			this._RankInfo = value;
			if (UIManagerControl.Instance.IsOpen("MemCollabUI"))
			{
				MemCollabUIView.Instance.SetItemsOfRank();
			}
		}
	}

	public static MemCollabManager Instance
	{
		get
		{
			if (MemCollabManager.m_Instance == null)
			{
				MemCollabManager.m_Instance = new MemCollabManager();
			}
			return MemCollabManager.m_Instance;
		}
	}

	public bool IsSame(int index01, int index02)
	{
		return index01 < this.CardIndexs.get_Count() && index02 < this.CardIndexs.get_Count() && this.CardIndexs.get_Item(index01) == this.CardIndexs.get_Item(index02);
	}

	public override void Init()
	{
		base.Init();
		this.InitScore();
	}

	public override void Release()
	{
		this.IsMemCollabGoing = false;
		if (this.m_cardIndexs != null)
		{
			this.m_cardIndexs.Clear();
		}
		if (this.m_successIndexs != null)
		{
			this.m_successIndexs.Clear();
		}
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<BeginMemoryFlopRes>(new NetCallBackMethod<BeginMemoryFlopRes>(this.OnBeginMemoryFlopRes));
		NetworkManager.AddListenEvent<EndMemoryFlopRes>(new NetCallBackMethod<EndMemoryFlopRes>(this.OnEndMemoryFlopRes));
		NetworkManager.AddListenEvent<MemoryFlopInfoNty>(new NetCallBackMethod<MemoryFlopInfoNty>(this.OnMemoryFlopInfoNty));
		NetworkManager.AddListenEvent<ExtendMemoryFlopTimesRes>(new NetCallBackMethod<ExtendMemoryFlopTimesRes>(this.OnExtendMemoryFlopTimesRes));
		NetworkManager.AddListenEvent<MemoryFlopOpenUIRes>(new NetCallBackMethod<MemoryFlopOpenUIRes>(this.OnMemoryFlopOpenUIRes));
	}

	public void SendBeginMemoryFlop()
	{
		InstanceManager.SecurityCheck(delegate
		{
			NetworkManager.Send(new BeginMemoryFlopReq(), ServerType.Data);
		}, null);
	}

	public void SendEndMemoryFlop()
	{
		this.IsLockBeginMemoryFlop = true;
		EndMemoryFlopReq endMemoryFlopReq = new EndMemoryFlopReq();
		endMemoryFlopReq.flopTimes = this.FlopTimes;
		endMemoryFlopReq.cardArrange.AddRange(this.SuccessIndexs);
		NetworkManager.Send(endMemoryFlopReq, ServerType.Data);
	}

	public void SendExtendMemoryFlopTimes()
	{
		NetworkManager.Send(new ExtendMemoryFlopTimesReq(), ServerType.Data);
	}

	public void SendMemoryFlopOpenUI()
	{
		NetworkManager.Send(new MemoryFlopOpenUIReq(), ServerType.Data);
	}

	private void OnBeginMemoryFlopRes(short state, BeginMemoryFlopRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.SuccessIndexs.Clear();
			this.FlopTimes = 0;
			this.IsMemCollabGoing = true;
			this.CardIndexs = down.cardIndex;
			this.ResetTimeCountDown(300);
			if (MemCollabUIView.Instance != null && MemCollabUIView.Instance.get_gameObject().get_activeSelf())
			{
				MemCollabUIView.Instance.MemCollabBegin();
			}
		}
	}

	private void OnEndMemoryFlopRes(short state, EndMemoryFlopRes down)
	{
		this.IsLockBeginMemoryFlop = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsMemCollabGoing = false;
			this.StopTimeCountDown();
			if (MemCollabUIView.Instance != null)
			{
				this.SendMemoryFlopOpenUI();
				MemCollabUIView.Instance.RefreshUI();
			}
			UIManagerControl.Instance.OpenUI("MemCollabRewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			MemCollabRewardUIView.Instance.SetTime(down.passTime);
			MemCollabRewardUIView.Instance.StartSpine(down.passTime);
			MemCollabRewardUIView.Instance.SetRewards(down.scoreRewards);
			MemCollabRewardUIView.Instance.SetRewardsToScore(null);
		}
	}

	private void OnMemoryFlopInfoNty(short state, MemoryFlopInfoNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.TodayRestTimes = down.todayRestTimes;
			this.TodayExtendTimes = down.todayExtendTimes;
		}
	}

	private void OnExtendMemoryFlopTimesRes(short state, ExtendMemoryFlopTimesRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (UIManagerControl.Instance.IsOpen("DailyTaskUI"))
		{
			EventDispatcher.Broadcast(EventNames.DailyTaskNty);
		}
	}

	private void OnMemoryFlopOpenUIRes(short state, MemoryFlopOpenUIRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RankInfo = down.rankInfo;
		}
	}

	private void InitScore()
	{
		FanPaiPingFenSheZhi fPPFSZ = this.GetFPPFSZ(1);
		if (fPPFSZ != null)
		{
			this.LEVEL_S = fPPFSZ.time;
			this.ImgScore_S = fPPFSZ.imgScore;
			this.Treasure_S = fPPFSZ.imgTreasure;
			this.RewardID_S = fPPFSZ.reward;
		}
		FanPaiPingFenSheZhi fPPFSZ2 = this.GetFPPFSZ(2);
		if (fPPFSZ2 != null)
		{
			this.LEVEL_A = fPPFSZ2.time;
			this.ImgScore_A = fPPFSZ2.imgScore;
			this.Treasure_A = fPPFSZ2.imgTreasure;
			this.RewardID_A = fPPFSZ2.reward;
		}
		FanPaiPingFenSheZhi fPPFSZ3 = this.GetFPPFSZ(3);
		if (fPPFSZ3 != null)
		{
			this.ImgScore_B = fPPFSZ3.imgScore;
			this.Treasure_B = fPPFSZ3.imgTreasure;
			this.RewardID_B = fPPFSZ3.reward;
		}
	}

	private FanPaiPingFenSheZhi GetFPPFSZ(int value)
	{
		List<FanPaiPingFenSheZhi> dataList = DataReader<FanPaiPingFenSheZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).value == value)
			{
				return dataList.get_Item(i);
			}
		}
		return null;
	}

	public string GetScoreSprite(int second)
	{
		if (second <= this.LEVEL_S)
		{
			return this.ImgScore_S;
		}
		if (second <= this.LEVEL_A)
		{
			return this.ImgScore_A;
		}
		return this.ImgScore_B;
	}

	public string GetTreasureSprite(int second)
	{
		if (second <= this.LEVEL_S)
		{
			return this.Treasure_S;
		}
		if (second <= this.LEVEL_A)
		{
			return this.Treasure_A;
		}
		return this.Treasure_B;
	}

	public int GetTreasureItemID(int second)
	{
		if (second <= this.LEVEL_S)
		{
			return this.RewardID_S;
		}
		if (second <= this.LEVEL_A)
		{
			return this.RewardID_A;
		}
		return this.RewardID_B;
	}

	public void BeginMemoryFlop()
	{
		if (this.IsLockBeginMemoryFlop)
		{
			UIManagerControl.Instance.ShowToastText("请等待服务器返回奖励");
			return;
		}
		if (this.TodayRestTimes > 0)
		{
			this.SendBeginMemoryFlop();
		}
		else if (!this.IsTodayRestBuyTimesOn())
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(500119, false));
		}
		else
		{
			this.BuyExtentTimes();
		}
	}

	public void BuyExtentTimes()
	{
		if (this.TodayRestTimes > 0)
		{
			return;
		}
		if (this.TodayExtendTimes >= this.MaxVipTimes)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(513531, false), GameDataUtils.GetChineseContent(513528, false), null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
			return;
		}
		if (this.TodayExtendTimes >= this.MaxExtendTimes)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505105, false), null, delegate
			{
				LinkNavigationManager.OpenVIPUI2Privilege();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		FanPaiSheZhi fanPaiSheZhi = DataReader<FanPaiSheZhi>.Get("price");
		if (fanPaiSheZhi == null || fanPaiSheZhi.date.get_Count() == 0)
		{
			return;
		}
		int num;
		if (this.TodayExtendTimes < fanPaiSheZhi.date.get_Count())
		{
			num = fanPaiSheZhi.date.get_Item(this.TodayExtendTimes);
		}
		else
		{
			num = fanPaiSheZhi.date.get_Item(fanPaiSheZhi.date.get_Count() - 1);
		}
		string content = string.Format("是否花费{0}钻石增加一次通关奖励次数", num);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("购买通关奖励次数", content, null, delegate
		{
			this.SendExtendMemoryFlopTimes();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	public bool IsTodayRestBuyTimesOn()
	{
		return this.MaxExtendTimes - this.TodayExtendTimes > 0 && this.TodayRestTimes <= 0;
	}

	public void SetTime()
	{
		if (MemCollabUIView.Instance != null && (this.timeCountDown == null || this.timeCountDown.IsStop))
		{
			MemCollabUIView.Instance.SetTimeUseUp(5);
			MemCollabUIView.Instance.SetScore(300);
		}
	}

	public void ResetTimeCountDown(int seconds)
	{
		if (this.timeCountDown == null)
		{
			this.timeCountDown = new TimeCountDown(seconds, TimeFormat.SECOND, delegate
			{
				if (MemCollabUIView.Instance != null && MemCollabUIView.Instance.get_gameObject().get_activeSelf() && this.timeCountDown != null)
				{
					MemCollabUIView.Instance.SetTime(this.timeCountDown.GetSeconds());
					MemCollabUIView.Instance.SetScore(this.timeCountDown.GetSeconds());
				}
			}, delegate
			{
				if (MemCollabUIView.Instance != null && MemCollabUIView.Instance.get_gameObject().get_activeSelf())
				{
					MemCollabUIView.Instance.SetTimeUseUp(5);
					MemCollabUIView.Instance.SetScore(300);
				}
			}, false);
		}
		else
		{
			this.timeCountDown.ResetSeconds(seconds);
		}
	}

	public void StopTimeCountDown()
	{
		this.timeCountDown.Dispose();
		this.timeCountDown = null;
	}

	private void CheckCardIsValid()
	{
		if (this.m_cardIndexs == null)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < this.m_cardIndexs.get_Count(); i++)
		{
			int num = this.m_cardIndexs.get_Item(i);
			if (dictionary.ContainsKey(num))
			{
				dictionary.set_Item(num, dictionary.get_Item(num) + 1);
			}
			else
			{
				dictionary.set_Item(num, 1);
			}
		}
		using (Dictionary<int, int>.ValueCollection.Enumerator enumerator = dictionary.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				if (current % 2 != 0)
				{
					string text = "卡牌数据异常: 卡牌数据没有成对";
					RemoteLogSender.Instance.SendToRemote(text, string.Empty, 4);
					Debug.LogError(text);
					Debug.LogError(this.m_cardIndexs.ToString());
					UIManagerControl.Instance.ShowToastText(text);
					break;
				}
			}
		}
	}
}
