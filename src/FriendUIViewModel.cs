using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FriendUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_SubChannels = "SubChannels";

		public const string Attr_FriendInfoUnits = "FriendInfoUnits";

		public const string Attr_FriendInfoFinds = "FriendInfoFinds";

		public const string Attr_FriendRecommends = "FriendRecommends";

		public const string Attr_FriendInfosUIVisibility = "FriendInfosUIVisibility";

		public const string Attr_FriendFindsUIVisibility = "FriendFindsUIVisibility";

		public const string Attr_FriendRecommendsUIVisibility = "FriendRecommendsUIVisibility";

		public const string Attr_FriendTip = "FriendTip";

		public const string Attr_ShowFriendTip = "ShowFriendTip";

		public const string Attr_FriendNum = "FriendNum";

		public const string Event_OnClickExchange = "OnClickExchange";
	}

	public enum FriendUIStatus
	{
		Friend = 1,
		Ask,
		Add,
		Black
	}

	public class SubChannelIndex
	{
		public const int Friend = 0;

		public const int Ask = 1;

		public const int Add = 2;

		public const int Black = 3;
	}

	public static FriendUIViewModel Instance;

	private static FriendUIViewModel.FriendUIStatus _UIStatus = FriendUIViewModel.FriendUIStatus.Friend;

	private bool _FriendInfosUIVisibility;

	private bool _FriendFindsUIVisibility;

	private bool _FriendRecommendsUIVisibility;

	private string _FriendTip;

	private bool _ShowFriendTip;

	private string _FriendNum;

	public ObservableCollection<OOButtonToggle2SubUI> SubChannels = new ObservableCollection<OOButtonToggle2SubUI>();

	public ObservableCollection<OOFriendInfoUnit> FriendInfoUnits = new ObservableCollection<OOFriendInfoUnit>();

	public ObservableCollection<OOFriendInfoUnit> FriendInfoFinds = new ObservableCollection<OOFriendInfoUnit>();

	public ObservableCollection<OOFriendInfoUnit> FriendRecommends = new ObservableCollection<OOFriendInfoUnit>();

	public static FriendUIViewModel.FriendUIStatus UIStatus
	{
		get
		{
			return FriendUIViewModel._UIStatus;
		}
		set
		{
			FriendUIViewModel._UIStatus = value;
			if (FriendUIViewModel.Instance != null)
			{
				switch (value)
				{
				case FriendUIViewModel.FriendUIStatus.Friend:
					FriendUIViewModel.Instance.ShowAsChannelFriend();
					break;
				case FriendUIViewModel.FriendUIStatus.Ask:
					FriendUIViewModel.Instance.ShowAsChannelAsk();
					break;
				case FriendUIViewModel.FriendUIStatus.Add:
					FriendUIViewModel.Instance.ShowAsChannelAdd();
					break;
				case FriendUIViewModel.FriendUIStatus.Black:
					FriendUIViewModel.Instance.ShowAsChannelBlack();
					break;
				}
			}
		}
	}

	public bool FriendInfosUIVisibility
	{
		get
		{
			return this._FriendInfosUIVisibility;
		}
		set
		{
			this._FriendInfosUIVisibility = value;
			base.NotifyProperty("FriendInfosUIVisibility", value);
		}
	}

	public bool FriendFindsUIVisibility
	{
		get
		{
			return this._FriendFindsUIVisibility;
		}
		set
		{
			this._FriendFindsUIVisibility = value;
			base.NotifyProperty("FriendFindsUIVisibility", value);
		}
	}

	public bool FriendRecommendsUIVisibility
	{
		get
		{
			return this._FriendRecommendsUIVisibility;
		}
		set
		{
			this._FriendRecommendsUIVisibility = value;
			base.NotifyProperty("FriendRecommendsUIVisibility", value);
		}
	}

	public string FriendTip
	{
		get
		{
			return this._FriendTip;
		}
		set
		{
			this._FriendTip = value;
			base.NotifyProperty("FriendTip", value);
			this.ShowFriendTip = !string.IsNullOrEmpty(value);
		}
	}

	public bool ShowFriendTip
	{
		get
		{
			return this._ShowFriendTip;
		}
		set
		{
			this._ShowFriendTip = value;
			base.NotifyProperty("ShowFriendTip", value);
		}
	}

	public string FriendNum
	{
		get
		{
			return this._FriendNum;
		}
		set
		{
			this._FriendNum = value;
			base.NotifyProperty("FriendNum", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		FriendUIViewModel.Instance = this;
		this.LoadSubChannels();
	}

	private void OnEnable()
	{
		this.SetChannel(FriendUIViewModel.UIStatus - FriendUIViewModel.FriendUIStatus.Friend);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener<bool>("FriendManagerEvents.IsAsksTipOn", new Callback<bool>(this.IsAsksTipOn));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener<bool>("FriendManagerEvents.IsAsksTipOn", new Callback<bool>(this.IsAsksTipOn));
	}

	private void IsAsksTipOn(bool isOn)
	{
		OOButtonToggle2SubUI channel = this.GetChannel(1);
		if (channel != null)
		{
			channel.IsTip = isOn;
		}
	}

	public void UpdateFindFriends(List<BuddyInfo> finds)
	{
		this.FriendInfoFinds.Clear();
		if (finds != null)
		{
			for (int i = 0; i < finds.get_Count(); i++)
			{
				BuddyInfo buddyInfo = finds.get_Item(i);
				OOFriendInfoUnit o;
				if (FriendManager.Instance.IsRelationOfBuddy(buddyInfo.id))
				{
					o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.AddIsFriend, buddyInfo, false);
				}
				else
				{
					o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.AddNotFriend, buddyInfo, false);
				}
				this.FriendInfoFinds.Add(o);
			}
		}
		this.UpdateRecommendsPanelPosition();
	}

	public bool DelAnimOfFindFriends(long uid)
	{
		for (int i = 0; i < this.FriendInfoFinds.Count; i++)
		{
			if (this.FriendInfoFinds[i].FriendUID == uid)
			{
				this.FriendInfoFinds[i].CallAction = true;
				return true;
			}
		}
		return false;
	}

	public void RandomShowRecommendsFriends()
	{
		List<BuddyInfo> recommendInfo = FriendManager.Instance.m_recommendInfo;
		if (recommendInfo != null)
		{
			int num = 3;
			List<TuiJianHaoYou> dataList = DataReader<TuiJianHaoYou>.DataList;
			TuiJianHaoYou tuiJianHaoYou = dataList.get_Item(0);
			if (tuiJianHaoYou != null)
			{
				num = tuiJianHaoYou.displayNumber;
			}
			if (num > recommendInfo.get_Count())
			{
				num = recommendInfo.get_Count();
			}
			for (int i = 0; i < recommendInfo.get_Count(); i++)
			{
				BuddyInfo buddyInfo = recommendInfo.get_Item(i);
				int num2 = Random.Range(0, recommendInfo.get_Count());
				recommendInfo.set_Item(i, recommendInfo.get_Item(num2));
				recommendInfo.set_Item(num2, buddyInfo);
			}
			this.UpdateRecommendsFriends(recommendInfo, num);
		}
	}

	public void UpdateRecommendsFriends(List<BuddyInfo> finds, int count)
	{
		this.FriendRecommends.Clear();
		if (finds != null)
		{
			for (int i = 0; i < count; i++)
			{
				BuddyInfo buddyInfo = finds.get_Item(i);
				OOFriendInfoUnit o;
				if (FriendManager.Instance.IsRelationOfBuddy(buddyInfo.id))
				{
					o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.AddIsFriend, buddyInfo, true);
				}
				else
				{
					o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.AddNotFriend, buddyInfo, true);
				}
				this.FriendRecommends.Add(o);
			}
		}
		this.UpdateRecommendsPanelActive();
	}

	private void LoadSubChannels()
	{
		this.SubChannels.Clear();
		OOButtonToggle2SubUI oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = 0;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.SetChannelFriendOn);
		oOButtonToggle2SubUI.Name = GameDataUtils.GetChineseContent(502022, false);
		oOButtonToggle2SubUI.IsTip = false;
		this.SubChannels.Add(oOButtonToggle2SubUI);
		oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = 1;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.SetChannelAskOn);
		oOButtonToggle2SubUI.Name = GameDataUtils.GetChineseContent(502023, false);
		oOButtonToggle2SubUI.IsTip = FriendManager.Instance.IsAsksTipOn;
		this.SubChannels.Add(oOButtonToggle2SubUI);
		oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = 2;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.SetChannelAddOn);
		oOButtonToggle2SubUI.Name = GameDataUtils.GetChineseContent(502024, false);
		oOButtonToggle2SubUI.IsTip = false;
		this.SubChannels.Add(oOButtonToggle2SubUI);
		oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = 3;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.SetChannelBlackOn);
		oOButtonToggle2SubUI.Name = GameDataUtils.GetChineseContent(502025, false);
		oOButtonToggle2SubUI.IsTip = false;
		this.SubChannels.Add(oOButtonToggle2SubUI);
	}

	private OOButtonToggle2SubUI GetChannel(int toggleIndex)
	{
		for (int i = 0; i < this.SubChannels.Count; i++)
		{
			OOButtonToggle2SubUI oOButtonToggle2SubUI = this.SubChannels[i];
			if (oOButtonToggle2SubUI.ToggleIndex == toggleIndex)
			{
				return oOButtonToggle2SubUI;
			}
		}
		return null;
	}

	public void SetChannel(int channel)
	{
		for (int i = 0; i < this.SubChannels.Count; i++)
		{
			OOButtonToggle2SubUI oOButtonToggle2SubUI = this.SubChannels[i];
			if (oOButtonToggle2SubUI != null)
			{
				oOButtonToggle2SubUI.SetIsToggleOn(false);
			}
		}
		OOButtonToggle2SubUI channel2 = this.GetChannel(channel);
		if (channel2 != null)
		{
			channel2.SetIsToggleOn(true);
		}
	}

	private void SetChannelFriendOn(int index)
	{
		FriendUIViewModel.UIStatus = FriendUIViewModel.FriendUIStatus.Friend;
	}

	private void SetChannelAskOn(int index)
	{
		FriendUIViewModel.UIStatus = FriendUIViewModel.FriendUIStatus.Ask;
	}

	private void SetChannelAddOn(int index)
	{
		FriendUIViewModel.UIStatus = FriendUIViewModel.FriendUIStatus.Add;
	}

	private void SetChannelBlackOn(int index)
	{
		FriendUIViewModel.UIStatus = FriendUIViewModel.FriendUIStatus.Black;
	}

	private void UpdateRecommendsPanelPosition()
	{
		if (FriendUIView.Instance == null)
		{
			return;
		}
		if (this.FriendInfoFinds.Count > 0)
		{
			FriendUIView.Instance.UpdateRecommendsPanelPosition(true);
		}
		else
		{
			FriendUIView.Instance.UpdateRecommendsPanelPosition(false);
		}
	}

	private void UpdateRecommendsPanelActive()
	{
		if (this.FriendFindsUIVisibility)
		{
			if (this.FriendRecommends != null && this.FriendRecommends.Count > 0)
			{
				this.FriendRecommendsUIVisibility = true;
			}
			else
			{
				this.FriendRecommendsUIVisibility = false;
			}
		}
		else
		{
			this.FriendRecommendsUIVisibility = false;
		}
	}

	private void ShowAsChannelFriend()
	{
		this.FriendInfosUIVisibility = true;
		this.FriendFindsUIVisibility = false;
		this.UpdateRecommendsPanelActive();
		this.FriendInfoUnits.Clear();
		List<BuddyInfo> friends = FriendManager.Instance.GetFriends();
		friends.Sort(new Comparison<BuddyInfo>(FriendUIViewModel.FriendSortCompare));
		string text = DataReader<GlobalParams>.Get("friend_limit_i").value;
		text = GameDataUtils.SplitString4Dot0(text);
		if (friends.get_Count() > 0)
		{
			this.FriendTip = string.Empty;
			string color = TextColorMgr.GetColor(friends.get_Count().ToString(), "A45A41", string.Empty);
			this.FriendNum = "好友数量：" + color + "/" + text;
			for (int i = 0; i < friends.get_Count(); i++)
			{
				BuddyInfo sdata = friends.get_Item(i);
				OOFriendInfoUnit o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.Friend, sdata, false);
				this.FriendInfoUnits.Add(o);
			}
		}
		else
		{
			this.FriendTip = GameDataUtils.GetChineseContent(502082, false);
			string color2 = TextColorMgr.GetColor("0", "A45A41", string.Empty);
			this.FriendNum = "好友数量：" + color2 + "/" + text;
		}
	}

	private void ShowAsChannelAsk()
	{
		this.FriendInfosUIVisibility = true;
		this.FriendFindsUIVisibility = false;
		this.UpdateRecommendsPanelActive();
		this.FriendInfoUnits.Clear();
		List<BuddyInfo> asks = FriendManager.Instance.GetAsks();
		string text = DataReader<GlobalParams>.Get("friend_invite_limit_i").value;
		text = GameDataUtils.SplitString4Dot0(text);
		if (asks.get_Count() > 0)
		{
			this.FriendTip = string.Empty;
			string color = TextColorMgr.GetColor(asks.get_Count().ToString(), "A45A41", string.Empty);
			this.FriendNum = "申请数量：" + color + "/" + text;
			for (int i = 0; i < asks.get_Count(); i++)
			{
				BuddyInfo sdata = asks.get_Item(i);
				OOFriendInfoUnit o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.Ask, sdata, false);
				this.FriendInfoUnits.Add(o);
			}
		}
		else
		{
			this.FriendTip = GameDataUtils.GetChineseContent(502083, false);
			string color2 = TextColorMgr.GetColor("0", "A45A41", string.Empty);
			this.FriendNum = "申请数量：" + color2 + "/" + text;
		}
	}

	private void ShowAsChannelAdd()
	{
		this.FriendInfosUIVisibility = false;
		this.FriendFindsUIVisibility = true;
		this.UpdateRecommendsPanelActive();
	}

	private void ShowAsChannelBlack()
	{
		this.FriendInfosUIVisibility = true;
		this.FriendFindsUIVisibility = false;
		this.UpdateRecommendsPanelActive();
		this.FriendInfoUnits.Clear();
		List<BuddyInfo> blacks = FriendManager.Instance.GetBlacks();
		if (blacks.get_Count() > 0)
		{
			this.FriendTip = string.Empty;
			string color = TextColorMgr.GetColor(blacks.get_Count().ToString(), "A45A41", string.Empty);
			this.FriendNum = "黑名单数量：" + color;
			for (int i = 0; i < blacks.get_Count(); i++)
			{
				BuddyInfo sdata = blacks.get_Item(i);
				OOFriendInfoUnit o = this.CreateFriendInfoUnit(OOFriendInfoUnit.UnitType.Black, sdata, false);
				this.FriendInfoUnits.Add(o);
			}
		}
		else
		{
			this.FriendTip = GameDataUtils.GetChineseContent(502084, false);
			string color2 = TextColorMgr.GetColor("0", "A45A41", string.Empty);
			this.FriendNum = "黑名单数量：" + color2;
		}
	}

	private OOFriendInfoUnit CreateFriendInfoUnit(OOFriendInfoUnit.UnitType status, BuddyInfo sdata, bool isSendRecommdReq = false)
	{
		return new OOFriendInfoUnit
		{
			UnitStatus = status,
			FriendUID = sdata.id,
			FriendIcon = UIUtils.GetRoleSmallIcon(sdata.career),
			FriendName = sdata.name,
			OffLineStatus = sdata.online,
			IsFriendRecommend = isSendRecommdReq,
			VIPLevel1 = GameDataUtils.GetNumIcon10(sdata.vipLv, NumType.Yellow_light),
			VIPLevel2 = GameDataUtils.GetNumIcon1(sdata.vipLv, NumType.Yellow_light),
			Level = "Lv." + sdata.lv,
			BattlePower = sdata.fighting.ToString()
		};
	}

	private int GetOnlineNum(List<BuddyInfo> buddys)
	{
		int num = 0;
		for (int i = 0; i < buddys.get_Count(); i++)
		{
			if (buddys.get_Item(i).online)
			{
				num++;
			}
		}
		return num;
	}

	private static int FriendSortCompare(BuddyInfo AF1, BuddyInfo AF2)
	{
		int result = 0;
		if (!AF1.online && AF2.online)
		{
			result = 1;
		}
		else if (AF1.online && !AF2.online)
		{
			result = -1;
		}
		else if (AF1.fighting < AF2.fighting)
		{
			result = 1;
		}
		else if (AF1.fighting > AF2.fighting)
		{
			result = -1;
		}
		return result;
	}
}
