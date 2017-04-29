using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class AchievementManager : BaseSubSystemManager
{
	private Dictionary<int, AchievementItemInfo> OwnIdMap = new Dictionary<int, AchievementItemInfo>();

	public Dictionary<int, AchievementItemInfo> AllIdList = new Dictionary<int, AchievementItemInfo>();

	private static AchievementManager instance;

	public static AchievementManager Instance
	{
		get
		{
			if (AchievementManager.instance == null)
			{
				AchievementManager.instance = new AchievementManager();
			}
			return AchievementManager.instance;
		}
	}

	private AchievementManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.RefreshAchievementInfo();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<AchievementLoginPush>(new NetCallBackMethod<AchievementLoginPush>(this.OnAchievementLoginPush));
		NetworkManager.AddListenEvent<AchievementItemChangedNty>(new NetCallBackMethod<AchievementItemChangedNty>(this.OnAchievementItemChangedNty));
		NetworkManager.AddListenEvent<acceptAchievementAwardRes>(new NetCallBackMethod<acceptAchievementAwardRes>(this.OnAcceptAchievementAwardRes));
	}

	private void OnAchievementLoginPush(short state, AchievementLoginPush down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OwnIdMap.Clear();
		using (List<AchievementItemInfo>.Enumerator enumerator = down.achievementItemInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AchievementItemInfo current = enumerator.get_Current();
				this.OwnIdMap.Add(current.achievementId, current);
			}
		}
		this.RefreshAchievementInfo();
		this.BroadcastTipsEvent();
	}

	private void OnAchievementItemChangedNty(short state, AchievementItemChangedNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		using (List<AchievementItemInfo>.Enumerator enumerator = down.achievementItemInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AchievementItemInfo current = enumerator.get_Current();
				this.OwnIdMap.Remove(current.achievementId);
				this.OwnIdMap.Add(current.achievementId, current);
			}
		}
		this.RefreshAchievementInfo();
		this.BroadcastTipsEvent();
	}

	private void OnAcceptAchievementAwardRes(short state, acceptAchievementAwardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513103, false), 1f, 2f);
		this.OwnIdMap.Remove(down.achievementId);
		if (down.achievementItemInfo.achievementId != 0)
		{
			this.OwnIdMap.Add(down.achievementItemInfo.achievementId, down.achievementItemInfo);
		}
		this.RefreshAchievementInfo();
		this.BroadcastTipsEvent();
	}

	private void RefreshAchievementInfo()
	{
		this.AllIdList.Clear();
		SortedList<int, AchievementItemInfo> sortedList = new SortedList<int, AchievementItemInfo>();
		SortedList<int, AchievementItemInfo> sortedList2 = new SortedList<int, AchievementItemInfo>();
		SortedList<int, AchievementItemInfo> sortedList3 = new SortedList<int, AchievementItemInfo>();
		using (Dictionary<int, AchievementItemInfo>.ValueCollection.Enumerator enumerator = this.OwnIdMap.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AchievementItemInfo current = enumerator.get_Current();
				Achievement achievement = DataReader<Achievement>.Get(current.achievementId);
				if (achievement == null)
				{
					Debug.LogError("GameData.Achievement no find id = " + current.achievementId);
				}
				else if (current.isAccept == 1)
				{
					sortedList.Remove(achievement.sort);
					sortedList.Add(achievement.sort, current);
				}
				else if (current.isAccept == 0)
				{
					sortedList2.Remove(achievement.sort);
					sortedList2.Add(achievement.sort, current);
				}
				else if (current.isAccept == 2)
				{
					sortedList3.Remove(achievement.sort);
					sortedList3.Add(achievement.sort, current);
				}
			}
		}
		using (IEnumerator<KeyValuePair<int, AchievementItemInfo>> enumerator2 = sortedList.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<int, AchievementItemInfo> current2 = enumerator2.get_Current();
				this.AllIdList.Add(current2.get_Value().achievementId, current2.get_Value());
			}
		}
		using (IEnumerator<KeyValuePair<int, AchievementItemInfo>> enumerator3 = sortedList2.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<int, AchievementItemInfo> current3 = enumerator3.get_Current();
				this.AllIdList.Add(current3.get_Value().achievementId, current3.get_Value());
			}
		}
		using (IEnumerator<KeyValuePair<int, AchievementItemInfo>> enumerator4 = sortedList3.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				KeyValuePair<int, AchievementItemInfo> current4 = enumerator4.get_Current();
				this.AllIdList.Add(current4.get_Value().achievementId, current4.get_Value());
			}
		}
		EventDispatcher.Broadcast(EventNames.RefreshAchievementInfo);
	}

	public void BroadcastTipsEvent()
	{
		bool arg = false;
		using (Dictionary<int, AchievementItemInfo>.ValueCollection.Enumerator enumerator = this.AllIdList.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AchievementItemInfo current = enumerator.get_Current();
				if (current.isAccept == 1)
				{
					arg = true;
					break;
				}
			}
		}
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsModuleAchievement, arg);
	}
}
