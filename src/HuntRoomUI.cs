using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntRoomUI : UIBase
{
	private GameObject mRoomPanel;

	private Text mTxTitle;

	private Image mRefreshButtonFg;

	private List<HuntRoomItem> mRoomList;

	private float mDeltaTime;

	private bool mIsCD;

	private bool mIsShowTips;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mRoomList = new List<HuntRoomItem>();
		this.mRoomPanel = base.FindTransform("Grid").get_gameObject();
		this.mTxTitle = base.FindTransform("txTitle").GetComponent<Text>();
		this.mRefreshButtonFg = base.FindTransform("BtnRefreshFg").GetComponent<Image>();
		base.FindTransform("BtnRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		base.FindTransform("BtnQuick").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuick);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.mTxTitle.set_text(HuntManager.Instance.GetRoomName(HuntManager.Instance.MapId, HuntManager.Instance.AreaId) + "狩猎点列表");
		this.RefreshRoomList();
		this.StartRefreshButton();
	}

	private void Update()
	{
		this.RefreshButton();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.GetHuntRoomList, new Callback(this.RefreshRoomList));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.GetHuntRoomList, new Callback(this.RefreshRoomList));
	}

	private void OnClickRefresh(GameObject go)
	{
		if (HuntManager.Instance.CurRoomCD <= 0)
		{
			HuntManager.Instance.SendEnterMapUiReq(HuntManager.Instance.MapId, HuntManager.Instance.AreaId, HuntManager.Instance.AreaType);
			this.StartRefreshButton();
		}
		else if (!this.mIsShowTips)
		{
			this.mIsShowTips = true;
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511634, false));
		}
	}

	private void OnClickQuick(GameObject go)
	{
		List<RoomUiInfo> curRoomInfos = HuntManager.Instance.CurRoomInfos;
		if (curRoomInfos != null)
		{
			for (int i = 0; i < curRoomInfos.get_Count(); i++)
			{
				if (curRoomInfos.get_Item(i).playerNums < HuntManager.Instance.RoomMaxPlayer)
				{
					HuntEnterRoomUI huntEnterRoomUI = UIManagerControl.Instance.OpenUI("HuntEnterRoomUI", null, false, UIType.NonPush) as HuntEnterRoomUI;
					huntEnterRoomUI.SetData(curRoomInfos.get_Item(i).roomId);
					return;
				}
			}
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511621, false));
	}

	private void OnClickRoom(HuntRoomItem item)
	{
		if (item != null && item.Info != null)
		{
			if (item.Info.playerNums < HuntManager.Instance.RoomMaxPlayer)
			{
				HuntEnterRoomUI huntEnterRoomUI = UIManagerControl.Instance.OpenUI("HuntEnterRoomUI", null, false, UIType.NonPush) as HuntEnterRoomUI;
				huntEnterRoomUI.SetData(item.Info.roomId);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511623, false));
			}
		}
	}

	private void RefreshRoomList()
	{
		List<RoomUiInfo> curRoomInfos = HuntManager.Instance.CurRoomInfos;
		if (curRoomInfos != null)
		{
			for (int i = 0; i < this.mRoomList.get_Count(); i++)
			{
				this.mRoomList.get_Item(i).SetUnused();
			}
			for (int j = 0; j < curRoomInfos.get_Count(); j++)
			{
				RoomUiInfo roomUiInfo = curRoomInfos.get_Item(j);
				if (roomUiInfo.teamFlag != 0)
				{
					this.CreateRoom(roomUiInfo);
				}
			}
			for (int k = 0; k < curRoomInfos.get_Count(); k++)
			{
				RoomUiInfo roomUiInfo = curRoomInfos.get_Item(k);
				if (roomUiInfo.teamFlag == 0)
				{
					this.CreateRoom(roomUiInfo);
				}
			}
		}
	}

	private void CreateRoom(RoomUiInfo info)
	{
		if (info != null)
		{
			HuntRoomItem huntRoomItem = this.mRoomList.Find((HuntRoomItem e) => e.get_gameObject().get_name() == "Unused");
			if (huntRoomItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("HuntRoomItem");
				UGUITools.SetParent(this.mRoomPanel, instantiate2Prefab, false);
				huntRoomItem = instantiate2Prefab.GetComponent<HuntRoomItem>();
				huntRoomItem.EventHandler = new Action<HuntRoomItem>(this.OnClickRoom);
				this.mRoomList.Add(huntRoomItem);
			}
			huntRoomItem.SetData(info);
			huntRoomItem.get_gameObject().set_name("Room_" + info.roomId);
			huntRoomItem.get_gameObject().SetActive(true);
		}
	}

	private void StartRefreshButton()
	{
		this.mIsCD = true;
		this.mIsShowTips = false;
		this.mDeltaTime = (float)(HuntManager.Instance.RefreshTime - HuntManager.Instance.CurRoomCD);
	}

	private void RefreshButton()
	{
		if (this.mIsCD)
		{
			this.mDeltaTime += Time.get_deltaTime();
			this.mRefreshButtonFg.set_fillAmount(this.mDeltaTime / (float)HuntManager.Instance.RefreshTime);
			if (this.mDeltaTime > (float)HuntManager.Instance.RefreshTime)
			{
				this.mIsCD = false;
				this.mDeltaTime = 0f;
				this.mRefreshButtonFg.set_fillAmount(1f);
			}
		}
	}
}
