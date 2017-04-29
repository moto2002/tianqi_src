using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class TitleManager : BaseSubSystemManager
{
	public Dictionary<int, TitleInfo> OwnTitleMap = new Dictionary<int, TitleInfo>();

	public Dictionary<int, int> idProcessMap = new Dictionary<int, int>();

	public List<TitleInfo> TitleList = new List<TitleInfo>();

	public List<TitleInfo> TitleListOwn = new List<TitleInfo>();

	private int _OwnCurrId;

	private static TitleManager instance;

	public int OwnCurrId
	{
		get
		{
			return this._OwnCurrId;
		}
		set
		{
			this._OwnCurrId = value;
			if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
			{
				EventDispatcher.Broadcast<long, int>("BillboardManager.Title", EntityWorld.Instance.EntSelf.ID, value);
			}
		}
	}

	public static TitleManager Instance
	{
		get
		{
			if (TitleManager.instance == null)
			{
				TitleManager.instance = new TitleManager();
			}
			return TitleManager.instance;
		}
	}

	private TitleManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(EventNames.OpenTitleUI, new Callback(this.OpenTitleUI));
		NetworkManager.AddListenEvent<TitleInfoLoginPush>(new NetCallBackMethod<TitleInfoLoginPush>(this.OnTitleInfoLoginPush));
		NetworkManager.AddListenEvent<TitleChangeNty>(new NetCallBackMethod<TitleChangeNty>(this.OnTitleChangeNty));
		NetworkManager.AddListenEvent<ReplaceCurrTitleRes>(new NetCallBackMethod<ReplaceCurrTitleRes>(this.OnReplaceCurrTitleRes));
		NetworkManager.AddListenEvent<LookTitleRes>(new NetCallBackMethod<LookTitleRes>(this.OnLookTitleRes));
		NetworkManager.AddListenEvent<SvrRemoveNty>(new NetCallBackMethod<SvrRemoveNty>(this.OnSvrRemoveNty));
		NetworkManager.AddListenEvent<OpenTitleRes>(new NetCallBackMethod<OpenTitleRes>(this.OnOpenTitleRes));
		NetworkManager.AddListenEvent<TitleProgressLoginPush>(new NetCallBackMethod<TitleProgressLoginPush>(this.OnTitleProgressLoginPush));
		NetworkManager.AddListenEvent<TitleProgressNty>(new NetCallBackMethod<TitleProgressNty>(this.OnTitleProgressNty));
	}

	private void OpenTitleUI()
	{
		NetworkManager.Send(new OpenTitleReq(), ServerType.Data);
	}

	public void SendReplaceCurrTiltle(int _titleId)
	{
		if (_titleId > 0)
		{
			NetworkManager.Send(new ReplaceCurrTitleReq
			{
				titleId = _titleId
			}, ServerType.Data);
		}
	}

	private void OnTitleInfoLoginPush(short state, TitleInfoLoginPush down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OwnCurrId = down.currId;
		this.OwnTitleMap.Clear();
		using (List<TitleInfo>.Enumerator enumerator = down.infos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				current.remainTime = (int)(TimeManager.Instance.PreciseServerTime.AddSeconds((double)current.remainTime) - TimeManager.Instance.CalculateLocalServerTimeBySecond(0)).get_TotalSeconds();
				this.OwnTitleMap.Add(current.titleId, current);
			}
		}
		this.SortTitle();
		this.BroadcastRefreshEvent();
	}

	private void OnTitleProgressLoginPush(short state, TitleProgressLoginPush down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.idProcessMap.Clear();
		using (List<TitleProgress>.Enumerator enumerator = down.progress.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleProgress current = enumerator.get_Current();
				this.idProcessMap.Add(current.titleId, current.curProgress);
			}
		}
		this.BroadcastRefreshEvent();
	}

	private void OnTitleProgressNty(short state, TitleProgressNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		using (List<TitleProgress>.Enumerator enumerator = down.progress.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleProgress current = enumerator.get_Current();
				this.idProcessMap.Remove(current.titleId);
				this.idProcessMap.Add(current.titleId, current.curProgress);
			}
		}
		this.BroadcastRefreshEvent();
	}

	public void OnOpenTitleRes(short state, OpenTitleRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OwnCurrId = down.currId;
		this.OwnTitleMap.Clear();
		using (List<TitleInfo>.Enumerator enumerator = down.infos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				current.remainTime = (int)(TimeManager.Instance.PreciseServerTime.AddSeconds((double)current.remainTime) - TimeManager.Instance.CalculateLocalServerTimeBySecond(0)).get_TotalSeconds();
				this.OwnTitleMap.Add(current.titleId, current);
			}
		}
		this.SortTitle();
		UIManagerControl.Instance.OpenUI("TitleUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
	}

	private void OnTitleChangeNty(short state, TitleChangeNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OwnCurrId = down.currId;
		using (List<TitleInfo>.Enumerator enumerator = down.infos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				current.remainTime = (int)(TimeManager.Instance.PreciseServerTime.AddSeconds((double)current.remainTime) - TimeManager.Instance.CalculateLocalServerTimeBySecond(0)).get_TotalSeconds();
				this.OwnTitleMap.Remove(current.titleId);
				this.OwnTitleMap.Add(current.titleId, current);
				GotTitleUI gotTitleUI = UIManagerControl.Instance.OpenUI("GotTitleUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as GotTitleUI;
				gotTitleUI.setTitle(current.titleId);
			}
		}
		this.SortTitle();
		this.BroadcastRefreshEvent();
	}

	public void OnSvrRemoveNty(short state, SvrRemoveNty down = null)
	{
		this.OwnCurrId = down.currId;
		this.OwnTitleMap.Remove(down.titleId);
		this.SortTitle();
		this.BroadcastRefreshEvent();
	}

	private void OnReplaceCurrTitleRes(short state, ReplaceCurrTitleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OwnCurrId = down.currId;
		EventDispatcher.Broadcast<int>(EventNames.UpdateWearInfo, this.OwnCurrId);
		EventDispatcher.Broadcast(EventNames.RefreshTitleButtonStateInActorUI);
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsActorUiTitle, this.HasNewTitle());
	}

	public void OnLookTitleRes(short state, LookTitleRes down = null)
	{
		if (down != null && state == 0)
		{
			using (List<TitleId>.Enumerator enumerator = down.ids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TitleId current = enumerator.get_Current();
					TitleInfo titleInfo = this.OwnTitleMap.get_Item(current.titleId);
					if (titleInfo != null)
					{
						titleInfo.lookFlag = true;
					}
					using (List<TitleInfo>.Enumerator enumerator2 = this.TitleListOwn.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							TitleInfo current2 = enumerator2.get_Current();
							if (current2.titleId == current.titleId)
							{
								current2.lookFlag = true;
							}
						}
					}
				}
			}
			EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsActorUiTitle, this.HasNewTitle());
		}
	}

	public void BroadcastRefreshEvent()
	{
		EventDispatcher.Broadcast(EventNames.RefreshTitleInfo);
		EventDispatcher.Broadcast(EventNames.RefreshTitleButtonStateInActorUI);
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsActorUiTitle, this.HasNewTitle());
	}

	public float GetVerticalNormalizedPosition()
	{
		for (int i = 0; i < this.TitleListOwn.get_Count(); i++)
		{
			if (this.TitleListOwn.get_Item(i).titleId == this.OwnCurrId)
			{
				return 1f - 1f * (float)i / (float)(this.TitleListOwn.get_Count() - 1);
			}
		}
		return 1f;
	}

	private void SortTitle()
	{
		if (EntityWorld.Instance == null || EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		this.TitleList.Clear();
		this.TitleListOwn.Clear();
		SortedList<int, TitleInfo> sortedList = new SortedList<int, TitleInfo>();
		List<int> list = new List<int>();
		int num = 2147483647;
		using (Dictionary<int, TitleInfo>.ValueCollection.Enumerator enumerator = this.OwnTitleMap.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				ChengHao chengHao = DataReader<ChengHao>.Get(current.titleId);
				if (chengHao.id > 0 && (chengHao.belong == EntityWorld.Instance.EntSelf.TypeID || chengHao.belong == 3))
				{
					sortedList.Add(DataReader<ChengHao>.Get(current.titleId).sort, current);
				}
				if (DataReader<ChengHao>.Get(current.titleId).duration > 0)
				{
					if (current.remainTime > 0)
					{
						if (num > current.remainTime)
						{
							num = current.remainTime;
						}
					}
					else
					{
						list.Add(current.titleId);
					}
				}
			}
		}
		using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				int current2 = enumerator2.get_Current();
				sortedList.Remove(current2);
				this.OwnTitleMap.Remove(current2);
			}
		}
		using (IEnumerator<KeyValuePair<int, TitleInfo>> enumerator3 = sortedList.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<int, TitleInfo> current3 = enumerator3.get_Current();
				this.TitleListOwn.Add(current3.get_Value());
			}
		}
		sortedList = new SortedList<int, TitleInfo>();
		using (List<ChengHao>.Enumerator enumerator4 = DataReader<ChengHao>.DataList.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				ChengHao current4 = enumerator4.get_Current();
				if (!this.OwnTitleMap.ContainsKey(current4.id) && current4.id > 0 && (current4.belong == EntityWorld.Instance.EntSelf.TypeID || current4.belong == 3))
				{
					bool flag = true;
					for (int i = 0; i < this.TitleListOwn.get_Count(); i++)
					{
						ChengHao chengHao2 = DataReader<ChengHao>.Get(this.TitleListOwn.get_Item(i).titleId);
						if (chengHao2 != null && chengHao2.type != 0 && chengHao2.type == current4.type && chengHao2.replaceable < current4.replaceable)
						{
							flag = false;
						}
					}
					if (flag)
					{
						TitleInfo titleInfo = new TitleInfo();
						titleInfo.titleId = current4.id;
						titleInfo.lookFlag = true;
						titleInfo.remainTime = current4.duration;
						sortedList.Add(current4.sort, titleInfo);
					}
				}
			}
		}
		using (IEnumerator<KeyValuePair<int, TitleInfo>> enumerator5 = sortedList.GetEnumerator())
		{
			while (enumerator5.MoveNext())
			{
				KeyValuePair<int, TitleInfo> current5 = enumerator5.get_Current();
				this.TitleList.Add(current5.get_Value());
			}
		}
	}

	public TitleInfo GetTitleInfoById(int id)
	{
		using (List<TitleInfo>.Enumerator enumerator = this.TitleList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				if (current.titleId == id)
				{
					TitleInfo result = current;
					return result;
				}
			}
		}
		using (List<TitleInfo>.Enumerator enumerator2 = this.TitleListOwn.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				TitleInfo current2 = enumerator2.get_Current();
				if (current2.titleId == id)
				{
					TitleInfo result = current2;
					return result;
				}
			}
		}
		return null;
	}

	public bool Contain(int id)
	{
		return this.OwnTitleMap.ContainsKey(id);
	}

	public void RefreshButtonStateInEquipShowUI(UIBase btnParent)
	{
	}

	public bool HasNewTitle()
	{
		bool flag = false;
		using (List<TitleInfo>.Enumerator enumerator = this.TitleList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				if (!current.lookFlag)
				{
					flag = true;
				}
			}
		}
		using (List<TitleInfo>.Enumerator enumerator2 = this.TitleListOwn.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				TitleInfo current2 = enumerator2.get_Current();
				if (!current2.lookFlag)
				{
					flag = true;
				}
			}
		}
		if (!flag && this._OwnCurrId == 0 && this.OwnTitleMap.get_Count() > 0)
		{
			flag = true;
		}
		return flag;
	}
}
