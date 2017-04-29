using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class FriendManager : BaseSubSystemManager
{
	private List<BuddyInfo> _AllFriends = new List<BuddyInfo>();

	private List<BuddyInfo> _Asks = new List<BuddyInfo>();

	private bool _IsAsksTipOn;

	private List<BuddyInfo> m_lookUpBuddys;

	private static FriendManager instance;

	public List<BuddyInfo> m_recommendInfo;

	private List<BuddyInfo> AllFriends
	{
		get
		{
			return this._AllFriends;
		}
		set
		{
			this._AllFriends = value;
		}
	}

	private List<BuddyInfo> Asks
	{
		get
		{
			return this._Asks;
		}
		set
		{
			this._Asks = value;
			this.SetIsAsksTipOn();
		}
	}

	public bool IsAsksTipOn
	{
		get
		{
			return this._IsAsksTipOn;
		}
		set
		{
			this._IsAsksTipOn = value;
			EventDispatcher.Broadcast<bool>("FriendManagerEvents.IsAsksTipOn", value);
			EventDispatcher.Broadcast<bool>("FriendManagerEvents.FriendBadgeTip", value);
		}
	}

	public static FriendManager Instance
	{
		get
		{
			if (FriendManager.instance == null)
			{
				FriendManager.instance = new FriendManager();
			}
			return FriendManager.instance;
		}
	}

	private FriendManager()
	{
	}

	public List<BuddyInfo> GetFriends()
	{
		List<BuddyInfo> list = new List<BuddyInfo>();
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			BuddyInfo buddyInfo = this.AllFriends.get_Item(i);
			if (buddyInfo.relation == BuddyRelation.BR.Buddy)
			{
				list.Add(buddyInfo);
			}
		}
		return list;
	}

	public List<BuddyInfo> GetBlacks()
	{
		List<BuddyInfo> list = new List<BuddyInfo>();
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			BuddyInfo buddyInfo = this.AllFriends.get_Item(i);
			if (buddyInfo.relation == BuddyRelation.BR.BuddyBlack || buddyInfo.relation == BuddyRelation.BR.StrangerBlack)
			{
				list.Add(buddyInfo);
			}
		}
		return list;
	}

	public List<BuddyInfo> GetAsks()
	{
		return this.Asks;
	}

	private void AddBuddy(long uid)
	{
		if (this.m_lookUpBuddys == null)
		{
			return;
		}
		for (int i = 0; i < this.m_lookUpBuddys.get_Count(); i++)
		{
			if (this.m_lookUpBuddys.get_Item(i).id == uid)
			{
				this.m_lookUpBuddys.RemoveAt(i);
			}
		}
		if (FriendUIViewModel.Instance != null && FriendUIViewModel.Instance.DelAnimOfFindFriends(uid))
		{
			TimerHeap.AddTimer(350u, 0, delegate
			{
				if (FriendUIViewModel.Instance != null)
				{
					FriendUIViewModel.Instance.UpdateFindFriends(this.m_lookUpBuddys);
				}
			});
		}
	}

	public override void Init()
	{
		base.Init();
		this.SetIsAsksTipOn();
	}

	public override void Release()
	{
		this.AllFriends.Clear();
		this.Asks.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<BuddyInfos>(new NetCallBackMethod<BuddyInfos>(this.OnBuddyInfosRes));
		NetworkManager.AddListenEvent<LookUpBuddyInfoRes>(new NetCallBackMethod<LookUpBuddyInfoRes>(this.OnLookUpBuddyInfoRes));
		NetworkManager.AddListenEvent<DelBuddyRes>(new NetCallBackMethod<DelBuddyRes>(this.OnDelBuddyRes));
		NetworkManager.AddListenEvent<MoveToBlackListRes>(new NetCallBackMethod<MoveToBlackListRes>(this.OnMoveToBlackListRes));
		NetworkManager.AddListenEvent<ExcludeFromBlackListRes>(new NetCallBackMethod<ExcludeFromBlackListRes>(this.OnExcludeFromBlackListRes));
		NetworkManager.AddListenEvent<AddBuddyRes>(new NetCallBackMethod<AddBuddyRes>(this.OnAddBuddyRes));
		NetworkManager.AddListenEvent<MakeFriendsNotify>(new NetCallBackMethod<MakeFriendsNotify>(this.OnMakeFriendsNotifyRes));
		NetworkManager.AddListenEvent<ApproveApplicantRes>(new NetCallBackMethod<ApproveApplicantRes>(this.OnApproveApplicantRes));
		NetworkManager.AddListenEvent<ApprovalNotice>(new NetCallBackMethod<ApprovalNotice>(this.OnApprovalNoticeRes));
		NetworkManager.AddListenEvent<IgnoreApplicantRes>(new NetCallBackMethod<IgnoreApplicantRes>(this.OnIgnoreApplicantRes));
		NetworkManager.AddListenEvent<BuddyUpdateNotice>(new NetCallBackMethod<BuddyUpdateNotice>(this.OnBuddyUpdateNoticeRes));
		NetworkManager.AddListenEvent<InviteInfos>(new NetCallBackMethod<InviteInfos>(this.OnInviteInfosRes));
		NetworkManager.AddListenEvent<InviteAgreeInfos>(new NetCallBackMethod<InviteAgreeInfos>(this.OnInviteAgreeInfosRes));
		NetworkManager.AddListenEvent<FindBuddyRes>(new NetCallBackMethod<FindBuddyRes>(this.OnFindBuddyRes));
		NetworkManager.AddListenEvent<RefreshBuddyInfoRes>(new NetCallBackMethod<RefreshBuddyInfoRes>(this.OnRefreshBuddyInfoRes));
		NetworkManager.AddListenEvent<GetRecommendFriendListRes>(new NetCallBackMethod<GetRecommendFriendListRes>(this.OnGetRecommendFriendListRes));
	}

	public void SendLookUpBuddyInfo(string nameBlur)
	{
		NetworkManager.Send(new LookUpBuddyInfoReq
		{
			name = nameBlur
		}, ServerType.Data);
	}

	public void SendDelBuddy(long friendUId)
	{
		NetworkManager.Send(new DelBuddyReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendMoveToBlackList(long friendUId)
	{
		NetworkManager.Send(new MoveToBlackListReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendExcludeFromBlackList(long friendUId)
	{
		NetworkManager.Send(new ExcludeFromBlackListReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendAddBuddy(long friendId, bool newBuddy = false)
	{
		if (this.IsRelationOfBuddy(friendId))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502069, false));
		}
		else if (this.IsRelationOfBlack(friendId))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502072, false));
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("请求发送成功");
			this.AddBuddy(friendId);
			NetworkManager.Send(new AddBuddyReq
			{
				id = friendId,
				newBuddy = newBuddy
			}, ServerType.Data);
		}
	}

	public void SendApproveApplicant(long friendUId)
	{
		NetworkManager.Send(new ApproveApplicantReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendIgnoreApplicant(long friendUId)
	{
		NetworkManager.Send(new IgnoreApplicantReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendFindBuddy(long friendUId)
	{
		NetworkManager.Send(new FindBuddyReq
		{
			id = friendUId
		}, ServerType.Data);
	}

	public void SendRefreshBuddyInfo()
	{
		NetworkManager.Send(new RefreshBuddyInfoReq(), ServerType.Data);
	}

	public void SendRecommendsInfo(int num)
	{
		NetworkManager.Send(new GetRecommendFriendListReq
		{
			num = num
		}, ServerType.Data);
	}

	private void OnBuddyInfosRes(short state, BuddyInfos down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.applicants.get_Count(); i++)
		{
			this.Add2Asks(down.applicants.get_Item(i));
		}
		this.AllFriends.Clear();
		for (int j = 0; j < down.buddies.get_Count(); j++)
		{
			this.AllFriends.Add(down.buddies.get_Item(j));
			this.Remove2Asks(down.buddies.get_Item(j).id);
		}
		for (int k = 0; k < down.blackLists.get_Count(); k++)
		{
			this.AllFriends.Add(down.blackLists.get_Item(k));
		}
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnLookUpBuddyInfoRes(short state, LookUpBuddyInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.m_lookUpBuddys = down.buddyInfos;
		if (FriendUIViewModel.Instance != null)
		{
			FriendUIViewModel.Instance.UpdateFindFriends(this.m_lookUpBuddys);
		}
	}

	private void OnDelBuddyRes(short state, DelBuddyRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnMoveToBlackListRes(short state, MoveToBlackListRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.relation == BuddyRelation.BR.BuddyBlack || down.relation == BuddyRelation.BR.StrangerBlack)
		{
			this.ChangeRelation2AllFirends(down.id, down.relation);
		}
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnExcludeFromBlackListRes(short state, ExcludeFromBlackListRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.relation == BuddyRelation.BR.Buddy)
		{
			this.ChangeRelation2AllFirends(down.id, BuddyRelation.BR.Buddy);
		}
		else if (down.relation == BuddyRelation.BR.Stranger)
		{
			this.ChangeRelation2AllFirends(down.id, BuddyRelation.BR.Stranger);
		}
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnAddBuddyRes(short state, AddBuddyRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnMakeFriendsNotifyRes(short state, MakeFriendsNotify down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.Add2Asks(down.info);
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnApproveApplicantRes(short state, ApproveApplicantRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.Remove2Asks(down.id);
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnApprovalNoticeRes(short state, ApprovalNotice down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		FloatTextAddManager.Instance.AddFloatText("[" + down.name + "]成为你的好友", Color.get_white());
	}

	private void OnIgnoreApplicantRes(short state, IgnoreApplicantRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.Remove2Asks(down.id);
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
	}

	private void OnBuddyUpdateNoticeRes(short state, BuddyUpdateNotice down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.infos.get_Count(); i++)
		{
			BuddyUpdateInfo buddyUpdateInfo = down.infos.get_Item(i);
			if (buddyUpdateInfo.type == BuddyUpdateType.BUT.Add)
			{
				this.Add2AllFriends(buddyUpdateInfo.buddies);
				for (int j = 0; j < buddyUpdateInfo.buddies.get_Count(); j++)
				{
					this.Remove2Asks(buddyUpdateInfo.buddies.get_Item(j).id);
				}
			}
			else if (buddyUpdateInfo.type == BuddyUpdateType.BUT.Del)
			{
				this.Remove2AllFriends(buddyUpdateInfo.buddies);
			}
			else if (buddyUpdateInfo.type == BuddyUpdateType.BUT.Update)
			{
				this.Update2AllFriends(buddyUpdateInfo.buddies);
			}
		}
		FriendUIViewModel.UIStatus = FriendUIViewModel.UIStatus;
		EventDispatcher.Broadcast(EventNames.UpdateFriendList);
	}

	private void OnInviteInfosRes(short state, InviteInfos down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.otherInviteInfos.get_Count(); i++)
		{
			this.Add2Asks(down.otherInviteInfos.get_Item(i));
		}
	}

	private void OnInviteAgreeInfosRes(short state, InviteAgreeInfos down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnFindBuddyRes(short state, FindBuddyRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.HideUI("ChatUI");
			UIManagerControl.Instance.OpenUI("RoleShowUI", UINodesManager.NormalUIRoot, true, UIType.FullScreen);
			RoleShowUIViewModel.Instance.TextLv = down.info.lv.ToString();
			RoleShowUIViewModel.Instance.TextName = down.info.name;
			RoleShowUIViewModel.Instance.TextPower = down.info.fighting.ToString();
			RoleShowUIViewModel.Instance.ShowRoleModel(down.info.career, down.equipInfos, down.fashionList, down.wearWing);
			RoleShowUIViewModel.Instance.SetEquipItems(down.equipInfos);
			ActorPropertyUI actorPropertyUI = UIManagerControl.Instance.OpenUI("ActorPropertyUI", RoleShowUIView.Instance.PanelProperty, false, UIType.NonPush) as ActorPropertyUI;
			actorPropertyUI.RefreshUI(down);
			RoleShowUIViewModel.Instance.level = down.info.lv;
			RoleShowUIViewModel.Instance.BuddyPetFormations = down.petFormation;
		}
	}

	private void OnRefreshBuddyInfoRes(short state, RefreshBuddyInfoRes down = null)
	{
	}

	private void OnGetRecommendFriendListRes(short state, GetRecommendFriendListRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_recommendInfo = down.Info;
			if (FriendUIViewModel.Instance != null)
			{
				FriendUIViewModel.Instance.RandomShowRecommendsFriends();
			}
		}
	}

	public bool IsRelationOfBuddy(long uid)
	{
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			BuddyInfo buddyInfo = this.AllFriends.get_Item(i);
			if (buddyInfo.id == uid)
			{
				return buddyInfo.relation == BuddyRelation.BR.Buddy;
			}
		}
		return false;
	}

	public bool IsRelationOfBlack(long uid)
	{
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			BuddyInfo buddyInfo = this.AllFriends.get_Item(i);
			if (buddyInfo.id == uid)
			{
				return buddyInfo.relation == BuddyRelation.BR.BuddyBlack || buddyInfo.relation == BuddyRelation.BR.StrangerBlack;
			}
		}
		return false;
	}

	private void Add2AllFriends(BuddyInfo buddyInfo)
	{
		this.Remove2AllFriends(buddyInfo);
		this.AllFriends.Add(buddyInfo);
	}

	private void Add2AllFriends(List<BuddyInfo> buddies)
	{
		for (int i = 0; i < buddies.get_Count(); i++)
		{
			this.Add2AllFriends(buddies.get_Item(i));
		}
	}

	private void Remove2AllFriends(BuddyInfo buddyInfo)
	{
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			if (this.AllFriends.get_Item(i).id == buddyInfo.id)
			{
				this.AllFriends.Remove(this.AllFriends.get_Item(i));
				break;
			}
		}
	}

	private void Remove2AllFriends(List<BuddyInfo> buddies)
	{
		for (int i = 0; i < buddies.get_Count(); i++)
		{
			this.Remove2AllFriends(buddies.get_Item(i));
		}
	}

	private void Update2AllFriends(BuddyInfo buddyInfo)
	{
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			if (this.AllFriends.get_Item(i).id == buddyInfo.id)
			{
				BuddyRelation.BR relation = this.AllFriends.get_Item(i).relation;
				this.AllFriends.set_Item(i, buddyInfo);
				this.AllFriends.get_Item(i).relation = relation;
			}
		}
	}

	private void Update2AllFriends(List<BuddyInfo> buddies)
	{
		for (int i = 0; i < buddies.get_Count(); i++)
		{
			this.Update2AllFriends(buddies.get_Item(i));
		}
	}

	private void ChangeRelation2AllFirends(long uid, BuddyRelation.BR relation)
	{
		for (int i = 0; i < this.AllFriends.get_Count(); i++)
		{
			BuddyInfo buddyInfo = this.AllFriends.get_Item(i);
			if (buddyInfo.id == uid)
			{
				buddyInfo.relation = relation;
				break;
			}
		}
	}

	private void Add2Asks(BuddyInfo buddyInfo)
	{
		this.Remove2Asks(buddyInfo.id);
		this.Asks.Add(buddyInfo);
		this.SetIsAsksTipOn();
	}

	private void Remove2Asks(BuddyInfo buddyInfo)
	{
		this.Remove2Asks(buddyInfo.id);
	}

	private void Remove2Asks(long uid)
	{
		for (int i = 0; i < this.Asks.get_Count(); i++)
		{
			if (this.Asks.get_Item(i).id == uid)
			{
				this.Asks.Remove(this.Asks.get_Item(i));
				this.SetIsAsksTipOn();
				return;
			}
		}
	}

	private void SetIsAsksTipOn()
	{
		this.IsAsksTipOn = (this.Asks.get_Count() > 0);
	}
}
