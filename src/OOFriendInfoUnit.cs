using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OOFriendInfoUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_FriendIcon = "FriendIcon";

		public const string Attr_FriendIconHSV = "FriendIconHSV";

		public const string Attr_FriendName = "FriendName";

		public const string Attr_VIPLevel1 = "VIPLevel1";

		public const string Attr_VIPLevel2 = "VIPLevel2";

		public const string Attr_Level = "Level";

		public const string Attr_BattlePower = "BattlePower";

		public const string Attr_BtnTopsTransform = "BtnTopsTransform";

		public const string Attr_VIPPos = "VIPPos";

		public const string Attr_CallAction = "CallAction";

		public const string Attr_BtnChatEnable = "BtnChatEnable";

		public const string Attr_BtnCheckVisibility = "BtnCheckVisibility";

		public const string Attr_BtnChatVisibility = "BtnChatVisibility";

		public const string Attr_BtnAgreeVisibility = "BtnAgreeVisibility";

		public const string Attr_BtnRefuseVisibility = "BtnRefuseVisibility";

		public const string Attr_BtnRemoveVisibility = "BtnRemoveVisibility";

		public const string Attr_RecommendImgVisibility = "RecommendImgVisibility";

		public const string Event_OnBtnUp = "OnBtnUp";

		public const string Event_OnBtnCheck = "OnBtnCheck";

		public const string Event_OnBtnChat = "OnBtnChat";

		public const string Event_OnBtnAgree = "OnBtnAgree";

		public const string Event_OnBtnRefuse = "OnBtnRefuse";

		public const string Event_OnBtnRemove = "OnBtnRemove";
	}

	public enum UnitType
	{
		Friend,
		Ask,
		AddNotFriend,
		AddIsFriend,
		Black
	}

	public class FriendStatus
	{
		public const int Online = 1;

		public const int Offline = 2;

		public const int Busy = 3;

		public const string Sp_Online = "kongxian";

		public const string Sp_Offline = "lixian";

		public const string Sp_Busy = "manglu";
	}

	public long FriendUID;

	public bool IsFriendRecommend;

	private OOFriendInfoUnit.UnitType _UnitStatus;

	private SpriteRenderer _FriendIcon;

	private int _FriendIconHSV;

	private string _FriendName;

	private SpriteRenderer _VIPLevel1;

	private SpriteRenderer _VIPLevel2;

	private Vector2 _VIPPos;

	private string _Level;

	private string _BattlePower;

	private bool _BtnCheckVisibility;

	private bool _BtnChatVisibility;

	private bool _BtnAgreeVisibility;

	private bool _BtnRefuseVisibility;

	private bool _BtnRemoveVisibility;

	private bool _BtnChatEnable;

	private bool _RecommendImgVisibility;

	private Transform _BtnTopsTransform;

	private bool _CallAction;

	public OOFriendInfoUnit.UnitType UnitStatus
	{
		get
		{
			return this._UnitStatus;
		}
		set
		{
			this._UnitStatus = value;
			this.BtnChatEnable = true;
			switch (value)
			{
			case OOFriendInfoUnit.UnitType.Friend:
				this.BtnCheckVisibility = true;
				this.BtnChatVisibility = true;
				this.BtnRemoveVisibility = false;
				this.BtnRefuseVisibility = false;
				this.BtnAgreeVisibility = false;
				break;
			case OOFriendInfoUnit.UnitType.Ask:
				this.BtnAgreeVisibility = true;
				this.BtnRefuseVisibility = true;
				this.BtnCheckVisibility = false;
				this.BtnRemoveVisibility = false;
				this.BtnChatVisibility = false;
				break;
			case OOFriendInfoUnit.UnitType.AddNotFriend:
				this.BtnCheckVisibility = true;
				this.BtnAgreeVisibility = true;
				this.BtnChatVisibility = false;
				this.BtnRefuseVisibility = false;
				this.BtnRemoveVisibility = false;
				break;
			case OOFriendInfoUnit.UnitType.AddIsFriend:
				this.BtnCheckVisibility = true;
				this.BtnAgreeVisibility = true;
				this.BtnRemoveVisibility = false;
				this.BtnRefuseVisibility = false;
				this.BtnChatVisibility = false;
				break;
			case OOFriendInfoUnit.UnitType.Black:
				this.BtnRemoveVisibility = true;
				this.BtnCheckVisibility = false;
				this.BtnAgreeVisibility = false;
				this.BtnRefuseVisibility = false;
				this.BtnChatVisibility = false;
				break;
			}
		}
	}

	public SpriteRenderer FriendIcon
	{
		get
		{
			return this._FriendIcon;
		}
		set
		{
			this._FriendIcon = value;
			base.NotifyProperty("FriendIcon", value);
		}
	}

	public int FriendIconHSV
	{
		get
		{
			return this._FriendIconHSV;
		}
		set
		{
			this._FriendIconHSV = value;
			base.NotifyProperty("FriendIconHSV", value);
		}
	}

	public string FriendName
	{
		get
		{
			return this._FriendName;
		}
		set
		{
			this._FriendName = value;
			base.NotifyProperty("FriendName", value);
		}
	}

	public bool OffLineStatus
	{
		get
		{
			return this.BtnChatEnable;
		}
		set
		{
			if (this.UnitStatus == OOFriendInfoUnit.UnitType.Friend)
			{
				this.BtnChatEnable = value;
			}
			this.FriendIconHSV = ((!value) ? 6 : 0);
		}
	}

	public SpriteRenderer VIPLevel1
	{
		get
		{
			return this._VIPLevel1;
		}
		set
		{
			this._VIPLevel1 = value;
			base.NotifyProperty("VIPLevel1", value);
		}
	}

	public SpriteRenderer VIPLevel2
	{
		get
		{
			return this._VIPLevel2;
		}
		set
		{
			this._VIPLevel2 = value;
			base.NotifyProperty("VIPLevel2", value);
		}
	}

	public Vector2 VIPPos
	{
		get
		{
			return this._VIPPos;
		}
		set
		{
			this._VIPPos = value;
			base.NotifyProperty("VIPPos", value);
		}
	}

	public string Level
	{
		get
		{
			return this._Level;
		}
		set
		{
			this._Level = value;
			base.NotifyProperty("Level", this._Level);
		}
	}

	public string BattlePower
	{
		get
		{
			return this._BattlePower;
		}
		set
		{
			this._BattlePower = value;
			base.NotifyProperty("BattlePower", this._BattlePower);
		}
	}

	public bool BtnCheckVisibility
	{
		get
		{
			return this._BtnCheckVisibility;
		}
		set
		{
			this._BtnCheckVisibility = value;
			base.NotifyProperty("BtnCheckVisibility", value);
		}
	}

	public bool BtnChatVisibility
	{
		get
		{
			return this._BtnChatVisibility;
		}
		set
		{
			this._BtnChatVisibility = value;
			base.NotifyProperty("BtnChatVisibility", value);
		}
	}

	public bool BtnAgreeVisibility
	{
		get
		{
			return this._BtnAgreeVisibility;
		}
		set
		{
			this._BtnAgreeVisibility = value;
			base.NotifyProperty("BtnAgreeVisibility", value);
		}
	}

	public bool BtnRefuseVisibility
	{
		get
		{
			return this._BtnRefuseVisibility;
		}
		set
		{
			this._BtnRefuseVisibility = value;
			base.NotifyProperty("BtnRefuseVisibility", value);
		}
	}

	public bool BtnRemoveVisibility
	{
		get
		{
			return this._BtnRemoveVisibility;
		}
		set
		{
			this._BtnRemoveVisibility = value;
			base.NotifyProperty("BtnRemoveVisibility", value);
		}
	}

	public bool BtnChatEnable
	{
		get
		{
			return this._BtnChatEnable;
		}
		set
		{
			this._BtnChatEnable = value;
			base.NotifyProperty("BtnChatEnable", value);
		}
	}

	public bool RecommendImgVisibility
	{
		get
		{
			return this._RecommendImgVisibility;
		}
		set
		{
			this._RecommendImgVisibility = value;
			base.NotifyProperty("RecommendImgVisibility", value);
		}
	}

	public Transform BtnTopsTransform
	{
		get
		{
			return this._BtnTopsTransform;
		}
		set
		{
			this._BtnTopsTransform = value;
			base.NotifyProperty("BtnTopsTransform", value);
		}
	}

	public bool CallAction
	{
		get
		{
			return this._CallAction;
		}
		set
		{
			base.NotifyProperty("CallAction", value);
		}
	}

	public void OnBtnUp()
	{
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		switch (this.UnitStatus)
		{
		case OOFriendInfoUnit.UnitType.Friend:
			list.Add(PopButtonTabsManager.GetButtonData2Black(this.FriendUID));
			list.Add(PopButtonTabsManager.GetButtonData2Delete(this.FriendUID));
			if (SystemOpenManager.IsSystemOn(59))
			{
				list.Add(PopButtonTabsManager.GetButtonData2TeamInvite(this.FriendUID));
			}
			break;
		case OOFriendInfoUnit.UnitType.Ask:
			list.Add(PopButtonTabsManager.GetButtonData2Show(this.FriendUID, null));
			break;
		case OOFriendInfoUnit.UnitType.Black:
			list.Add(PopButtonTabsManager.GetButtonData2Show(this.FriendUID, null));
			break;
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIViewModel.Open(UINodesManager.MiddleUIRoot);
			this.BtnTopsTransform = PopButtonsAdjustUIViewModel.Instance.get_transform();
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	public void OnBtnCheck()
	{
		FriendManager.Instance.SendFindBuddy(this.FriendUID);
	}

	public void OnBtnChat()
	{
		UIManagerControl.Instance.HideAll();
		EventDispatcher.Broadcast("SHOW_TOWN_UI");
		ChatManager.Instance.OpenChatUI2ChannelPrivate(this.FriendUID, this.FriendName);
	}

	public void OnBtnAgree()
	{
		OOFriendInfoUnit.UnitType unitStatus = this.UnitStatus;
		if (unitStatus != OOFriendInfoUnit.UnitType.Ask)
		{
			if (unitStatus == OOFriendInfoUnit.UnitType.AddNotFriend)
			{
				if (this.IsFriendRecommend)
				{
					FriendManager.Instance.SendAddBuddy(this.FriendUID, true);
				}
				else
				{
					FriendManager.Instance.SendAddBuddy(this.FriendUID, false);
				}
			}
		}
		else
		{
			FriendManager.Instance.SendApproveApplicant(this.FriendUID);
		}
	}

	public void OnBtnRefuse()
	{
		FriendManager.Instance.SendIgnoreApplicant(this.FriendUID);
	}

	public void OnBtnRemove()
	{
		FriendManager.Instance.SendExcludeFromBlackList(this.FriendUID);
	}
}
