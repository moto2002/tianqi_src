using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TramcarFriendUI : UIBase
{
	private GameObject mFriendPanel;

	private List<TramcarFriendItem> mFriendList;

	private KuangCheDiTuPeiZhi mData;

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
		this.mFriendList = new List<TramcarFriendItem>();
		this.mFriendPanel = base.FindTransform("Grid").get_gameObject();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.mData = DataReader<KuangCheDiTuPeiZhi>.Get(TramcarUI.LastSelectMapId);
		FriendManager.Instance.SendRefreshBuddyInfo();
		this.RefreshFriendList();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.UpdateFriendList, new Callback(this.RefreshFriendList));
		EventDispatcher.AddListener(EventNames.OpenTramcarFriendList, new Callback(this.RefreshFriendList));
		EventDispatcher.AddListener(EventNames.InviteFriendSuccess, new Callback(this.OnInviteFriendSuccess));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OpenTramcarFriendList, new Callback(this.RefreshFriendList));
		EventDispatcher.RemoveListener(EventNames.InviteFriendSuccess, new Callback(this.OnInviteFriendSuccess));
	}

	private void OnClickInvite(TramcarFriendItem item)
	{
		if (item != null && item.Info != null)
		{
			TramcarManager.Instance.SendInviteFriendReq(item.Info.id);
		}
	}

	private void RefreshFriendList()
	{
		TramcarFriendUI.<RefreshFriendList>c__AnonStorey308 <RefreshFriendList>c__AnonStorey = new TramcarFriendUI.<RefreshFriendList>c__AnonStorey308();
		this.ClearFriendList();
		<RefreshFriendList>c__AnonStorey.friends = FriendManager.Instance.GetFriends();
		<RefreshFriendList>c__AnonStorey.friends.Sort(new Comparison<BuddyInfo>(this.SortFriendList));
		List<FriendProtectFightInfo> tramcarFriendInfos = TramcarManager.Instance.TramcarFriendInfos;
		List<BuddyInfo> list = new List<BuddyInfo>();
		List<BuddyInfo> list2 = new List<BuddyInfo>();
		int i;
		for (i = 0; i < <RefreshFriendList>c__AnonStorey.friends.get_Count(); i++)
		{
			if (tramcarFriendInfos != null)
			{
				FriendProtectFightInfo friendProtectFightInfo = tramcarFriendInfos.Find((FriendProtectFightInfo e) => e.roleId == <RefreshFriendList>c__AnonStorey.friends.get_Item(i).id);
				if (friendProtectFightInfo != null)
				{
					if (friendProtectFightInfo.todayHelpProtectTimes > 0)
					{
						this.CreateFriendItem(<RefreshFriendList>c__AnonStorey.friends.get_Item(i), friendProtectFightInfo.todayHelpProtectTimes);
					}
					else
					{
						list.Add(<RefreshFriendList>c__AnonStorey.friends.get_Item(i));
					}
				}
				else
				{
					list2.Add(<RefreshFriendList>c__AnonStorey.friends.get_Item(i));
				}
			}
		}
		for (int k = 0; k < list2.get_Count(); k++)
		{
			this.CreateFriendItem(list2.get_Item(k), -1);
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			this.CreateFriendItem(list.get_Item(j), 0);
		}
	}

	private int SortFriendList(BuddyInfo a, BuddyInfo b)
	{
		if (a.fighting > b.fighting)
		{
			return -1;
		}
		if (a.fighting < b.fighting)
		{
			return 1;
		}
		return 0;
	}

	private void ClearFriendList()
	{
		for (int i = 0; i < this.mFriendList.get_Count(); i++)
		{
			this.mFriendList.get_Item(i).SetUnused();
		}
	}

	private void CreateFriendItem(BuddyInfo info, int protectTimes)
	{
		if (info != null)
		{
			TramcarFriendItem tramcarFriendItem = this.mFriendList.Find((TramcarFriendItem e) => e.get_gameObject().get_name() == "Unused");
			if (tramcarFriendItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TramcarFriendItem");
				UGUITools.SetParent(this.mFriendPanel, instantiate2Prefab, false);
				tramcarFriendItem = instantiate2Prefab.GetComponent<TramcarFriendItem>();
				tramcarFriendItem.EventHandler = new Action<TramcarFriendItem>(this.OnClickInvite);
				this.mFriendList.Add(tramcarFriendItem);
			}
			if (this.mData != null)
			{
				tramcarFriendItem.SetData(info, protectTimes, this.mData.minLv, this.mData.maxLv);
			}
			else
			{
				tramcarFriendItem.SetData(info, protectTimes, 0, 0);
			}
			tramcarFriendItem.get_gameObject().set_name(info.id.ToString());
			tramcarFriendItem.get_gameObject().SetActive(true);
		}
	}

	private void OnInviteFriendSuccess()
	{
		this.Show(false);
	}
}
