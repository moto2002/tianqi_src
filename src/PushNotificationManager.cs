using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class PushNotificationManager : BaseSubSystemManager
{
	public enum PushWay
	{
		Client,
		ServerToXG,
		ServerToClient
	}

	public class PushTag
	{
		public const string USER_L_DARK_TRAIN = "darkTrain";

		public const string USER_L_GANG_FIGHT = "gangFight";

		public const string USER_L_GUILD_QUESTION = "guildQuestion";

		public const string USER_L_GUILD_BOSS = "guildBoss";

		public const string USER_L_HIT_MOUSE = "hitMouse";
	}

	public class PushID
	{
		public const int ID_OFFLINE_HOOK = 1;

		public const int ID_GUILD_BOSS = 3;

		public const int ID_GUILD_QUESTION = 4;

		public const int ID_DARK_TRAIN = 5;

		public const int ID_GANG_FIGHT = 6;

		public const int ID_HIT_MOUSE = 7;

		public const int ID_OFFLINE_CALL = 8;
	}

	public const string PUSH_ID_KEY = "tqzm_push";

	public const string PUSH_TITLE = "天启之门";

	private List<ItemSetting> mItemSettings = new List<ItemSetting>();

	private static PushNotificationManager instance;

	private bool IsHasOpenPushSystem;

	public static PushNotificationManager Instance
	{
		get
		{
			if (PushNotificationManager.instance == null)
			{
				PushNotificationManager.instance = new PushNotificationManager();
			}
			return PushNotificationManager.instance;
		}
	}

	private PushNotificationManager()
	{
	}

	public bool GetItemSetting(int pushId)
	{
		for (int i = 0; i < this.mItemSettings.get_Count(); i++)
		{
			if (this.mItemSettings.get_Item(i).sType == pushId)
			{
				return this.mItemSettings.get_Item(i).bOpen;
			}
		}
		return true;
	}

	public void SetItemSetting(int pushId, bool isOn)
	{
		for (int i = 0; i < this.mItemSettings.get_Count(); i++)
		{
			if (this.mItemSettings.get_Item(i).sType == pushId)
			{
				this.mItemSettings.get_Item(i).bOpen = isOn;
				return;
			}
		}
		ItemSetting itemSetting = new ItemSetting();
		itemSetting.sType = pushId;
		itemSetting.bOpen = isOn;
		this.mItemSettings.Add(itemSetting);
	}

	public static bool IsNotNull()
	{
		return PushNotificationManager.instance != null;
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
		NetworkManager.AddListenEvent<MessagePushSettingPush>(new NetCallBackMethod<MessagePushSettingPush>(this.OnMessagePushSettingPush));
		NetworkManager.AddListenEvent<UpdateXGTokenRes>(new NetCallBackMethod<UpdateXGTokenRes>(this.OnUpdateXGTokenRes));
		NetworkManager.AddListenEvent<SetMessagePushRes>(new NetCallBackMethod<SetMessagePushRes>(this.OnSetMessagePushRes));
	}

	public void SendUpdateXGToken(string token)
	{
		NetworkManager.Send(new UpdateXGTokenReq
		{
			xgToken = token
		}, ServerType.Data);
	}

	public void SendSetMessagePush()
	{
		this.RefreshAllPushTag();
		SetMessagePushReq setMessagePushReq = new SetMessagePushReq();
		for (int i = 0; i < this.mItemSettings.get_Count(); i++)
		{
			setMessagePushReq.setting.Add(this.mItemSettings.get_Item(i));
		}
		NetworkManager.Send(setMessagePushReq, ServerType.Data);
	}

	private void OnMessagePushSettingPush(short state, MessagePushSettingPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mItemSettings = down.setting;
		}
	}

	private void OnUpdateXGTokenRes(short state, UpdateXGTokenRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnSetMessagePushRes(short state, SetMessagePushRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public bool OpenPushSystem()
	{
		if (this.IsHasOpenPushSystem)
		{
			return false;
		}
		this.IsHasOpenPushSystem = true;
		int pushID = NativeCallManager.GetPushID();
		if (pushID > 0)
		{
			GuideManager.Instance.out_system_lock = true;
			LinkNavigationManager.SystemLink(pushID, false, delegate
			{
				GuideManager.Instance.out_system_lock = false;
			});
			return true;
		}
		return false;
	}

	public static void SetPushTag(string tag)
	{
		NativeCallManager.SetPushTag(tag);
	}

	public static void DeletePushTag(string tag)
	{
		NativeCallManager.DeletePushTag(tag);
	}

	public void RefreshAllPushTag()
	{
		this.RefreshPushTag(5, "darkTrain");
		this.RefreshPushTag(6, "gangFight");
		this.RefreshPushTag(4, "guildQuestion");
		this.RefreshPushTag(3, "guildBoss");
		this.RefreshPushTag(7, "hitMouse");
		BossBookManager.Instance.RefreshBossPushTag();
	}

	public void RefreshPushTag(int pushId, string tag)
	{
		if (this.GetItemSetting(pushId) && this.IsPushOn(pushId) && this.IsSystemOn(pushId))
		{
			PushNotificationManager.SetPushTag(tag);
		}
		else
		{
			PushNotificationManager.DeletePushTag(tag);
		}
	}

	public bool IsLocalPushOn(int pushId)
	{
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(pushId);
		return tuiSongTongZhi != null && tuiSongTongZhi.open == 1 && tuiSongTongZhi.serverPush == 0;
	}

	public bool IsPushOn(int pushId)
	{
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(pushId);
		return tuiSongTongZhi != null && tuiSongTongZhi.open == 1;
	}

	public bool IsSystemOn(int pushId)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return false;
		}
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(pushId);
		return tuiSongTongZhi != null && SystemOpenManager.IsSystemOn(tuiSongTongZhi.sysId);
	}

	public void NotificationOffline(int seconds)
	{
		int num = 1;
		if (!this.IsLocalPushOn(num))
		{
			return;
		}
		if (!this.IsSystemOn(num))
		{
			return;
		}
		Debug.Log("[离线挂机], seconds = " + seconds);
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(num);
		if (tuiSongTongZhi != null)
		{
			NativeCallManager.NotificationMessage(tuiSongTongZhi.id, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), DateTime.get_Now().AddSeconds((double)seconds), NotificationRepeatInterval.None);
		}
	}

	public void NotificationOfflineCall()
	{
		int num = 8;
		if (!this.IsLocalPushOn(num))
		{
			return;
		}
		if (this.IsSystemOn(num))
		{
			return;
		}
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(64);
		if (systemOpen == null)
		{
			return;
		}
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(num);
		if (tuiSongTongZhi != null)
		{
			Debug.Log("[离线召回] time = " + tuiSongTongZhi.early);
			NativeCallManager.NotificationMessage(tuiSongTongZhi.id, string.Format(GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), systemOpen.level), DateTime.get_Now().AddSeconds((double)(tuiSongTongZhi.early * 60)), NotificationRepeatInterval.None);
		}
	}

	public void NotificationActivity(int pushId)
	{
		if (!this.IsLocalPushOn(pushId))
		{
			return;
		}
		if (!this.IsSystemOn(pushId))
		{
			return;
		}
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(pushId);
		if (tuiSongTongZhi != null && tuiSongTongZhi.activityid > 0)
		{
			HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(tuiSongTongZhi.activityid);
			if (huoDongZhongXin != null)
			{
				if (huoDongZhongXin.date.get_Count() == 7)
				{
					int num = 0;
					for (int i = 0; i < huoDongZhongXin.starttime.get_Count(); i++)
					{
						DateTime dateTime = this.GetDateTime(0, huoDongZhongXin.starttime.get_Item(i));
						int notificationID = this.GetNotificationID(tuiSongTongZhi, num++);
						NativeCallManager.NotificationMessage(notificationID, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), dateTime, NotificationRepeatInterval.Day);
					}
				}
				else
				{
					int num2 = 0;
					for (int j = 0; j < huoDongZhongXin.date.get_Count(); j++)
					{
						for (int k = 0; k < huoDongZhongXin.starttime.get_Count(); k++)
						{
							DateTime dateTime2 = this.GetDateTime(huoDongZhongXin.date.get_Item(j), huoDongZhongXin.starttime.get_Item(k));
							int notificationID2 = this.GetNotificationID(tuiSongTongZhi, num2++);
							NativeCallManager.NotificationMessage(notificationID2, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), dateTime2, NotificationRepeatInterval.Week);
						}
					}
				}
			}
		}
	}

	public void Notification_GuildBoss()
	{
		if (GuildManager.Instance.GetGuildId() <= 0L)
		{
			return;
		}
		int num = 3;
		if (!this.IsLocalPushOn(num))
		{
			return;
		}
		if (!this.IsSystemOn(num))
		{
			return;
		}
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(num);
		if (tuiSongTongZhi != null)
		{
			string value = DataReader<GongHuiXinXi>.Get("QuestionTime").value;
			string[] array = value.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				DateTime dateTime = this.GetDateTime(0, array[0]);
				int id = tuiSongTongZhi.id;
				NativeCallManager.NotificationMessage(id, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), dateTime, NotificationRepeatInterval.Day);
			}
		}
	}

	public void Notification_GuildQuestion()
	{
		if (GuildManager.Instance.GetGuildId() <= 0L)
		{
			return;
		}
		int num = 4;
		if (!this.IsLocalPushOn(num))
		{
			return;
		}
		if (!this.IsSystemOn(num))
		{
			return;
		}
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(num);
		if (tuiSongTongZhi != null)
		{
			string value = DataReader<GongHuiXinXi>.Get("QuestionTime").value;
			string[] array = value.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				DateTime dateTime = this.GetDateTime(0, array[0]);
				int id = tuiSongTongZhi.id;
				NativeCallManager.NotificationMessage(id, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), dateTime, NotificationRepeatInterval.Day);
			}
		}
	}

	public static void DoOnApplicationQuit()
	{
		if (PushNotificationManager.IsNotNull())
		{
			PushNotificationManager.Instance.RefreshAllPushTag();
		}
		if (OffHookManager.IsNotNull() && OffHookManager.Instance.GetOffHookHasTime() > 0)
		{
			PushNotificationManager.Instance.NotificationOffline(OffHookManager.Instance.GetOffHookHasTime());
		}
		PushNotificationManager.Instance.NotificationOfflineCall();
		PushNotificationManager.Instance.NotificationActivity(5);
		PushNotificationManager.Instance.NotificationActivity(6);
		PushNotificationManager.Instance.NotificationActivity(7);
		PushNotificationManager.Instance.Notification_GuildBoss();
		PushNotificationManager.Instance.Notification_GuildQuestion();
		NativeCallManager.SavePushIDs();
	}

	private int GetNotificationID(TuiSongTongZhi dataTS, int offset)
	{
		return dataTS.id * 100 + offset;
	}

	private DateTime GetDateTime(int dayOfWeek, string time)
	{
		int year = DateTime.get_Now().get_Year();
		int month = DateTime.get_Now().get_Month();
		int day = DateTime.get_Now().get_Day();
		string[] array = time.Split(new char[]
		{
			':'
		});
		int num = 0;
		int num2 = 0;
		if (array.Length >= 1)
		{
			num = int.Parse(array[0]);
		}
		if (array.Length >= 2)
		{
			num2 = int.Parse(array[1]);
		}
		DateTime dateTime = new DateTime(year, month, day, num, num2, 0);
		if (dayOfWeek != 0)
		{
			int i = 0;
			while (i < 7)
			{
				DateTime dateTime2 = dateTime.AddSeconds((double)(i * 86400));
				if (dateTime2.get_DayOfWeek() == this.GetDayOfWeek(dayOfWeek))
				{
					if (dateTime2 <= DateTime.get_Now())
					{
						return dateTime2.AddSeconds(604800.0);
					}
					return dateTime2;
				}
				else
				{
					i++;
				}
			}
			return default(DateTime);
		}
		if (dateTime <= DateTime.get_Now())
		{
			return dateTime.AddSeconds(86400.0);
		}
		return dateTime;
	}

	private DayOfWeek GetDayOfWeek(int dayOfWeek)
	{
		switch (dayOfWeek)
		{
		case 1:
			return 1;
		case 2:
			return 2;
		case 3:
			return 3;
		case 4:
			return 4;
		case 5:
			return 5;
		case 6:
			return 6;
		case 7:
			return 0;
		default:
			return 1;
		}
	}
}
