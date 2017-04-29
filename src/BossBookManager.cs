using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

internal class BossBookManager : BaseSubSystemManager
{
	public Dictionary<int, BossItemInfo> BossDictionary = new Dictionary<int, BossItemInfo>();

	public Dictionary<int, List<int>> PageDictionary = new Dictionary<int, List<int>>();

	private int AfterTeleportNavToBossId;

	private bool IsShowBossComeOutTip;

	private Dictionary<int, int> TrackBossComeOutUtcDic;

	public bool isFirstLogin = true;

	public int CurrentUITabIndex;

	private static BossBookManager instance;

	public bool IsTrack;

	public int comeOutTipCDRemainTime;

	private TimeCountDown bossBookCD;

	public static BossBookManager Instance
	{
		get
		{
			if (BossBookManager.instance == null)
			{
				BossBookManager.instance = new BossBookManager();
			}
			return BossBookManager.instance;
		}
	}

	private BossBookManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.BossDictionary.Clear();
		this.PageDictionary.Clear();
		List<BossBiaoQian> dataList = DataReader<BossBiaoQian>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			BossBiaoQian bossBiaoQian = dataList.get_Item(i);
			if (bossBiaoQian != null)
			{
				int page = bossBiaoQian.page;
				List<int> list;
				this.PageDictionary.TryGetValue(page, ref list);
				if (list == null)
				{
					list = new List<int>();
					this.PageDictionary.Add(page, list);
				}
				list.Add(bossBiaoQian.key);
				if (!this.BossDictionary.ContainsKey(bossBiaoQian.key))
				{
					this.BossDictionary.Add(bossBiaoQian.key, new BossItemInfo
					{
						bossId = bossBiaoQian.key
					});
				}
			}
		}
		using (Dictionary<int, List<int>>.ValueCollection.Enumerator enumerator = this.PageDictionary.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				List<int> current = enumerator.get_Current();
				current.Sort((int a, int b) => a.CompareTo(b));
			}
		}
	}

	public override void Release()
	{
		using (Dictionary<int, BossItemInfo>.ValueCollection.Enumerator enumerator = this.BossDictionary.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BossItemInfo current = enumerator.get_Current();
				current.ClearInfo();
			}
		}
		this.BossDictionary.Clear();
		this.PageDictionary.Clear();
		this.AfterTeleportNavToBossId = 0;
		this.TrackBossComeOutUtcDic = null;
		this.RemoveBossBookCountDown();
		this.isFirstLogin = true;
		this.CurrentUITabIndex = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<BossLabelInfoNty>(new NetCallBackMethod<BossLabelInfoNty>(this.OnBossLabelInfoNty));
		NetworkManager.AddListenEvent<GetBossLabelInfoRes>(new NetCallBackMethod<GetBossLabelInfoRes>(this.OnGetBossLabelInfoRes));
		NetworkManager.AddListenEvent<GetBossKilledLogRes>(new NetCallBackMethod<GetBossKilledLogRes>(this.OnGetBossKilledLogRes));
		NetworkManager.AddListenEvent<GetBossDropLogRes>(new NetCallBackMethod<GetBossDropLogRes>(this.OnGetBossDropLogRes));
		NetworkManager.AddListenEvent<TraceBossRes>(new NetCallBackMethod<TraceBossRes>(this.OnTraceBossRes));
		NetworkManager.AddListenEvent<GetBossPageInfoRes>(new NetCallBackMethod<GetBossPageInfoRes>(this.OnGetBossPageInfoRes));
		NetworkManager.AddListenEvent<CondMainCityEnterRes>(new NetCallBackMethod<CondMainCityEnterRes>(this.OnCondMainCityEnterRes));
		NetworkManager.AddListenEvent<CondMainCityLeaveRes>(new NetCallBackMethod<CondMainCityLeaveRes>(this.OnCondMainCityLeaveRes));
		NetworkManager.AddListenEvent<TraceBossLabelInfoPush>(new NetCallBackMethod<TraceBossLabelInfoPush>(this.OnTraceBossLabelInfoPush));
	}

	public void SendGetBossLabelInfoReq(int bossId)
	{
		NetworkManager.Send(new GetBossLabelInfoReq
		{
			labelId = bossId
		}, ServerType.Data);
	}

	public void SendGetBossPageInfoReq(int page)
	{
		NetworkManager.Send(new GetBossPageInfoReq
		{
			pageId = page
		}, ServerType.Data);
	}

	public void SendGetBossKilledLogReq(int bossId)
	{
		NetworkManager.Send(new GetBossKilledLogReq
		{
			labelId = bossId
		}, ServerType.Data);
	}

	public void SendGetBossDropLogReq(int page)
	{
		NetworkManager.Send(new GetBossDropLogReq
		{
			pageId = page
		}, ServerType.Data);
	}

	public void SendTraceBossReq(int bossId, bool isTrack)
	{
		this.IsTrack = isTrack;
		NetworkManager.Send(new TraceBossReq
		{
			labelId = bossId,
			opFlag = isTrack
		}, ServerType.Data);
	}

	public void SendCondMainCityEnterReq(int mapId)
	{
		NetworkManager.Send(new CondMainCityEnterReq
		{
			condCityType = 2,
			mapId = mapId
		}, ServerType.Data);
	}

	public void SendCondMainCityLeaveReq()
	{
		NetworkManager.Send(new CondMainCityLeaveReq(), ServerType.Data);
	}

	public void OnBossLabelInfoNty(short state, BossLabelInfoNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		EventDispatcher.Broadcast<List<int>>(EventNames.BossBookBossDataUpdate, msg.labelId);
	}

	public void OnGetBossLabelInfoRes(short state, GetBossLabelInfoRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		BossItemInfo bossItemInfo = this.GetBossItemInfo(msg.labelId);
		if (bossItemInfo != null)
		{
			bossItemInfo.UpdateInfo(msg.bossLabelInfo);
		}
		EventDispatcher.Broadcast<int>(EventNames.BossBookItemUpdate, msg.labelId);
	}

	public void OnGetBossPageInfoRes(short state, GetBossPageInfoRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < msg.bossLabelInfo.get_Count(); i++)
		{
			BossLabelInfo bossLabelInfo = msg.bossLabelInfo.get_Item(i);
			if (DataReader<BossBiaoQian>.Get(bossLabelInfo.labelId) != null)
			{
				BossItemInfo bossItemInfo = this.GetBossItemInfo(bossLabelInfo.labelId);
				if (bossItemInfo != null)
				{
					bossItemInfo.UpdateInfo(bossLabelInfo);
					list.Add(bossLabelInfo.labelId);
				}
			}
		}
		EventDispatcher.Broadcast<List<int>>(EventNames.BossBookPageUpdate, list);
	}

	public void OnGetBossKilledLogRes(short state, GetBossKilledLogRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		EventDispatcher.Broadcast<List<BossKilledLog>>(EventNames.BossSlayLogUpdate, msg.bossKilledLog);
	}

	public void OnGetBossDropLogRes(short state, GetBossDropLogRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		EventDispatcher.Broadcast<List<BossDropLog>>(EventNames.BossDropLogUpdate, msg.bossDropLog);
	}

	public void OnTraceBossRes(short state, TraceBossRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		if (!this.IsTrack && this.TrackBossComeOutUtcDic != null && this.TrackBossComeOutUtcDic.ContainsKey(msg.labelId))
		{
			this.TrackBossComeOutUtcDic.Remove(msg.labelId);
		}
	}

	public void OnCondMainCityEnterRes(short state, CondMainCityEnterRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
	}

	public void OnCondMainCityLeaveRes(short state, CondMainCityLeaveRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
	}

	public void OnTraceBossLabelInfoPush(short state, TraceBossLabelInfoPush msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		if (this.TrackBossComeOutUtcDic == null)
		{
			this.TrackBossComeOutUtcDic = new Dictionary<int, int>();
		}
		for (int i = 0; i < msg.info.get_Count(); i++)
		{
			int labelId = msg.info.get_Item(i).labelId;
			int nextTime = msg.info.get_Item(i).nextTime;
			if (this.isFirstLogin || nextTime > 0)
			{
				if (!this.TrackBossComeOutUtcDic.ContainsKey(labelId))
				{
					this.TrackBossComeOutUtcDic.Add(labelId, nextTime);
				}
				else
				{
					this.TrackBossComeOutUtcDic.set_Item(labelId, nextTime);
				}
			}
		}
		this.isFirstLogin = false;
		EventDispatcher.Broadcast(EventNames.BossBookComeOutTipUpdate);
	}

	public static DateTime StampToDateTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
		long num = long.Parse(timeStamp + "0000000");
		TimeSpan timeSpan = new TimeSpan(num);
		return dateTime.Add(timeSpan);
	}

	public static int ToTimeStamp(DateTime dateTime)
	{
		DateTime dateTime2 = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
		return (int)(dateTime - dateTime2).get_TotalSeconds();
	}

	public List<int> GetPageDictionary(int page)
	{
		List<int> result;
		this.PageDictionary.TryGetValue(page, ref result);
		return result;
	}

	public BossItemInfo GetBossItemInfo(int bossId)
	{
		BossItemInfo result;
		this.BossDictionary.TryGetValue(bossId, ref result);
		return result;
	}

	public bool TeleportAndNavToBoss(int bossId)
	{
		BossItemInfo bossItemInfo = this.GetBossItemInfo(bossId);
		if (bossItemInfo == null)
		{
			return false;
		}
		BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(bossId);
		if (bossBiaoQian == null)
		{
			return false;
		}
		bool result = false;
		int scene = bossBiaoQian.scene;
		if (CityManager.Instance.CurrentCityID == scene)
		{
			if (bossItemInfo.pos.get_Count() > 0)
			{
				Pos pos;
				if (bossItemInfo.pos.get_Count() > 1)
				{
					int num = Random.Range(0, bossItemInfo.pos.get_Count());
					pos = bossItemInfo.pos.get_Item(num);
				}
				else
				{
					pos = bossItemInfo.pos.get_Item(0);
				}
				float pointX = pos.x * 0.01f;
				float pointZ = pos.y * 0.01f;
				result = true;
				EventDispatcher.Broadcast(EventNames.BeginNav);
				EntityWorld.Instance.EntSelf.NavToSameScenePoint(pointX, pointZ, 0f, delegate
				{
					EventDispatcher.Broadcast(EventNames.EndNav);
				});
			}
		}
		else if (!this.IsTeleportOn(scene))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(517001, false));
		}
		else
		{
			RadarManager.Instance.StopNav();
			ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(scene);
			if (zhuChengPeiZhi != null)
			{
				this.AfterTeleportNavToBossId = bossId;
				result = true;
				if (zhuChengPeiZhi.mapType == 3)
				{
					this.SendCondMainCityEnterReq(scene);
				}
				else
				{
					EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, scene);
				}
			}
		}
		return result;
	}

	public void ContinueNavToBoss()
	{
		if (this.AfterTeleportNavToBossId > 0)
		{
			BossItemInfo bossItemInfo = this.GetBossItemInfo(this.AfterTeleportNavToBossId);
			if (bossItemInfo == null)
			{
				return;
			}
			BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(this.AfterTeleportNavToBossId);
			if (bossBiaoQian == null)
			{
				return;
			}
			if (CityManager.Instance.CurrentCityID != bossBiaoQian.scene)
			{
				return;
			}
			if (bossItemInfo.pos.get_Count() > 0)
			{
				Pos pos;
				if (bossItemInfo.pos.get_Count() > 1)
				{
					int num = Random.Range(0, bossItemInfo.pos.get_Count());
					pos = bossItemInfo.pos.get_Item(num);
				}
				else
				{
					pos = bossItemInfo.pos.get_Item(0);
				}
				float x = pos.x * 0.01f;
				float z = pos.y * 0.01f;
				TimerHeap.AddTimer(500u, 0, delegate
				{
					EventDispatcher.Broadcast(EventNames.BeginNav);
					EntityWorld.Instance.EntSelf.NavToSameScenePoint(x, z, 0f, delegate
					{
						EventDispatcher.Broadcast(EventNames.EndNav);
					});
				});
			}
			this.AfterTeleportNavToBossId = 0;
		}
	}

	public void SavePushSettings()
	{
		using (Dictionary<int, BossItemInfo>.KeyCollection.Enumerator enumerator = this.BossDictionary.get_Keys().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				if (DataReader<TuiSongTongZhi>.Contains(current))
				{
					PushNotificationManager.Instance.SetItemSetting(current, this.BossDictionary.get_Item(current).trackFlag);
				}
			}
		}
		PushNotificationManager.Instance.SendSetMessagePush();
	}

	public void RefreshBossPushTag()
	{
		List<TuiSongTongZhi> dataList = DataReader<TuiSongTongZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(dataList.get_Item(i).id);
			if (tuiSongTongZhi != null)
			{
				PushNotificationManager.Instance.RefreshPushTag(tuiSongTongZhi.id, tuiSongTongZhi.tab);
			}
		}
	}

	private bool IsTeleportOn(int cityID)
	{
		ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(cityID);
		return zhuChengPeiZhi != null && zhuChengPeiZhi.teleport == 1;
	}

	public bool CheckBossBookComeOutTip()
	{
		this.RemoveBossBookCountDown();
		if (this.IsShowBossComeOutTip)
		{
			return true;
		}
		int num = 0;
		this.comeOutTipCDRemainTime = 0;
		if (this.TrackBossComeOutUtcDic == null)
		{
			return false;
		}
		using (Dictionary<int, int>.Enumerator enumerator = this.TrackBossComeOutUtcDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				int num2 = current.get_Value() - TimeManager.Instance.PreciseServerSecond;
				if (num2 <= 300)
				{
					this.IsShowBossComeOutTip = true;
				}
				else
				{
					if (num <= 0)
					{
						this.comeOutTipCDRemainTime = num2;
					}
					if (this.comeOutTipCDRemainTime < num2)
					{
						this.comeOutTipCDRemainTime = num2;
					}
				}
			}
		}
		if (this.comeOutTipCDRemainTime > 0)
		{
			this.AddBossBookCountDown(this.comeOutTipCDRemainTime);
		}
		return this.IsShowBossComeOutTip;
	}

	public void HideBossBookComeOutTip()
	{
		this.RemoveBossBookCountDown();
		this.IsShowBossComeOutTip = false;
		List<int> list = new List<int>();
		int num = 0;
		this.comeOutTipCDRemainTime = 0;
		if (this.TrackBossComeOutUtcDic == null)
		{
			return;
		}
		using (Dictionary<int, int>.Enumerator enumerator = this.TrackBossComeOutUtcDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				int num2 = current.get_Value() - TimeManager.Instance.PreciseServerSecond;
				if (num2 <= 300)
				{
					list.Add(current.get_Key());
				}
				else
				{
					if (num <= 0)
					{
						this.comeOutTipCDRemainTime = num2;
					}
					if (this.comeOutTipCDRemainTime < num2)
					{
						this.comeOutTipCDRemainTime = num2;
					}
				}
			}
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			int num3 = list.get_Item(i);
			if (this.TrackBossComeOutUtcDic.ContainsKey(num3))
			{
				this.TrackBossComeOutUtcDic.Remove(num3);
			}
		}
		if (this.comeOutTipCDRemainTime > 0)
		{
			this.AddBossBookCountDown(this.comeOutTipCDRemainTime);
		}
	}

	private void AddBossBookCountDown(int remianTime)
	{
		this.RemoveBossBookCountDown();
		if (this.IsShowBossComeOutTip)
		{
			return;
		}
		this.bossBookCD = new TimeCountDown(remianTime, TimeFormat.SECOND, null, delegate
		{
			this.RemoveBossBookCountDown();
			this.IsShowBossComeOutTip = true;
			EventDispatcher.Broadcast(EventNames.BossBookComeOutTipUpdate);
		}, true);
	}

	public void RemoveBossBookCountDown()
	{
		if (this.bossBookCD != null)
		{
			this.bossBookCD.Dispose();
			this.bossBookCD = null;
		}
	}
}
