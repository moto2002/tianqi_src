using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TramcarBeInviteUI : UIBase
{
	private GameObject mInvitePanel;

	private Toggle mDontShowAgain;

	private List<TramcarFriendItem> mInviteList;

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
		this.mInviteList = new List<TramcarFriendItem>();
		this.mInvitePanel = base.FindTransform("Grid").get_gameObject();
		this.mDontShowAgain = base.FindTransform("DontShowAgain").GetComponent<Toggle>();
		this.mDontShowAgain.onValueChanged.AddListener(new UnityAction<bool>(this.OnDontShowAgain));
		base.FindTransform("BtnAllReject").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAllReject);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.RefreshInviteList();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.TramcarInviteFriendNty, new Callback(this.RefreshInviteList));
		EventDispatcher.AddListener<bool>(EventNames.TramcarInviteAnswerRes, new Callback<bool>(this.OnTramcarInviteAnswerRes));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.TramcarInviteFriendNty, new Callback(this.RefreshInviteList));
		EventDispatcher.RemoveListener<bool>(EventNames.TramcarInviteAnswerRes, new Callback<bool>(this.OnTramcarInviteAnswerRes));
	}

	private void OnClickItem(bool isAgree, TramcarFriendItem item)
	{
		if (item != null && item.Info != null)
		{
			TramcarManager.Instance.SendFriendProtectAnswerReq(item.Info.id, isAgree, item.Info.name);
			if (!isAgree)
			{
				item.SetUnused();
				TramcarManager.Instance.InviteMessage.Remove(TramcarManager.Instance.InviteMessage.Find((InviteProtectNty e) => e.roleId == item.Info.id));
				this.RefreshInviteList();
			}
		}
	}

	private void OnClickAllReject(GameObject go)
	{
		TramcarManager.Instance.InviteMessage.Clear();
		this.Show(false);
	}

	private void OnDontShowAgain(bool value)
	{
		TramcarManager.Instance.IsDontShowAgainBeInvite = value;
	}

	private void RefreshInviteList()
	{
		TramcarBeInviteUI.<RefreshInviteList>c__AnonStorey306 <RefreshInviteList>c__AnonStorey = new TramcarBeInviteUI.<RefreshInviteList>c__AnonStorey306();
		this.ClearInviteList();
		List<BuddyInfo> friends = FriendManager.Instance.GetFriends();
		<RefreshInviteList>c__AnonStorey.list = TramcarManager.Instance.InviteMessage;
		int i;
		for (i = <RefreshInviteList>c__AnonStorey.list.get_Count() - 1; i > -1; i--)
		{
			this.CreateInvite(<RefreshInviteList>c__AnonStorey.list.get_Item(i).quality, friends.Find((BuddyInfo e) => e.id == <RefreshInviteList>c__AnonStorey.list.get_Item(i).roleId));
		}
	}

	private int SortLootList(TramcarRoomInfo a, TramcarRoomInfo b)
	{
		if (a.quality > b.quality)
		{
			return 1;
		}
		if (a.quality < b.quality)
		{
			return -1;
		}
		return 0;
	}

	private void ClearInviteList()
	{
		for (int i = 0; i < this.mInviteList.get_Count(); i++)
		{
			this.mInviteList.get_Item(i).SetUnused();
		}
	}

	private void CreateInvite(int quality, BuddyInfo info)
	{
		if (info != null)
		{
			TramcarFriendItem tramcarFriendItem = this.mInviteList.Find((TramcarFriendItem e) => e.get_gameObject().get_name() == "Unused");
			if (tramcarFriendItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TramcarFriendItem");
				UGUITools.SetParent(this.mInvitePanel, instantiate2Prefab, false);
				tramcarFriendItem = instantiate2Prefab.GetComponent<TramcarFriendItem>();
				tramcarFriendItem.InviteEventHandler = new Action<bool, TramcarFriendItem>(this.OnClickItem);
				this.mInviteList.Add(tramcarFriendItem);
			}
			tramcarFriendItem.SetData(quality, info);
			tramcarFriendItem.get_gameObject().set_name("Invite_" + info.id);
			tramcarFriendItem.get_gameObject().SetActive(true);
		}
	}

	private void OnTramcarInviteAnswerRes(bool isAgree)
	{
		if (isAgree)
		{
			this.Show(false);
		}
	}
}
