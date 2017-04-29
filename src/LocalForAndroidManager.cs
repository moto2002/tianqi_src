using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalForAndroidManager : MonoBehaviour
{
	public enum NotificationExecuteMode
	{
		Inexact,
		Exact,
		ExactAndAllowWhileIdle
	}

	private static LocalForAndroidManager instance;

	private static string MainActivityClassName = "com.unity3d.player.UnityPlayerNativeActivity";

	private static readonly string CLASS_Notification = "com.hsh.XProject.UnityNotificationManager";

	private static AndroidJavaClass mAndroidJC;

	public static LocalForAndroidManager Instance
	{
		get
		{
			if (LocalForAndroidManager.instance == null)
			{
				LocalForAndroidManager.instance = NativeCallManager.UnityNativeBridgeRoot.AddComponent<LocalForAndroidManager>();
			}
			return LocalForAndroidManager.instance;
		}
	}

	public static AndroidJavaClass AndroidJC
	{
		get
		{
			if (LocalForAndroidManager.mAndroidJC == null)
			{
				LocalForAndroidManager.mAndroidJC = new AndroidJavaClass(LocalForAndroidManager.CLASS_Notification);
			}
			return LocalForAndroidManager.mAndroidJC;
		}
	}

	public void Init()
	{
	}

	public static void NotificationMessage(int push_id, string message, int hour, int minute, NotificationRepeatInterval repeatInterval)
	{
		int year = DateTime.get_Now().get_Year();
		int month = DateTime.get_Now().get_Month();
		int day = DateTime.get_Now().get_Day();
		DateTime dst_date = new DateTime(year, month, day, hour, minute, 0);
		LocalForAndroidManager.NotificationMessage(push_id, message, dst_date, repeatInterval);
	}

	public static void NotificationMessage(int push_id, string message, DateTime dst_date, NotificationRepeatInterval repeatInterval)
	{
		if (repeatInterval == NotificationRepeatInterval.None && dst_date <= DateTime.get_Now())
		{
			return;
		}
		long timeInSecond = LocalForAndroidManager.GetTimeInSecond(dst_date);
		if (repeatInterval == NotificationRepeatInterval.None)
		{
			LocalForAndroidManager.SendNotification(push_id, timeInSecond, message, true, true, true, "app_icon", LocalForAndroidManager.NotificationExecuteMode.Inexact);
		}
		else if (repeatInterval == NotificationRepeatInterval.Day)
		{
			LocalForAndroidManager.SendRepeatingNotification(push_id, timeInSecond, 86400L, message, true, true, true, "app_icon");
		}
		else if (repeatInterval == NotificationRepeatInterval.Week)
		{
			LocalForAndroidManager.SendRepeatingNotification(push_id, timeInSecond, 604800L, message, true, true, true, "app_icon");
		}
		else if (repeatInterval == NotificationRepeatInterval.ForTest)
		{
			LocalForAndroidManager.SendRepeatingNotification(push_id, timeInSecond, 5L, message, true, true, true, "app_icon");
		}
		else if (repeatInterval == NotificationRepeatInterval.ForTest2)
		{
			LocalForAndroidManager.SendRepeatingNotification(push_id, timeInSecond, 60L, message, true, true, true, "app_icon");
		}
	}

	private static void SendNotification(int push_id, long delay_second, string message, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "app_icon", LocalForAndroidManager.NotificationExecuteMode executeMode = LocalForAndroidManager.NotificationExecuteMode.Inexact)
	{
		Color32 color = new Color32(255, 68, 68, 255);
		LocalForAndroidManager.AndroidJC.CallStatic("SetNotification", new object[]
		{
			push_id,
			delay_second * 1000L,
			"天启之门",
			message,
			message,
			(!sound) ? 0 : 1,
			(!vibrate) ? 0 : 1,
			(!lights) ? 0 : 1,
			bigIcon,
			"notify_icon_small",
			(int)color.r * 65536 + (int)color.g * 256 + (int)color.b,
			(int)executeMode,
			LocalForAndroidManager.MainActivityClassName
		});
	}

	private static void SendRepeatingNotification(int push_id, long delay_second, long interval, string message, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "app_icon")
	{
		Color32 color = new Color32(255, 68, 68, 255);
		LocalForAndroidManager.AndroidJC.CallStatic("SetRepeatingNotification", new object[]
		{
			push_id,
			delay_second * 1000L,
			"天启之门",
			message,
			message,
			interval * 1000L,
			(!sound) ? 0 : 1,
			(!vibrate) ? 0 : 1,
			(!lights) ? 0 : 1,
			bigIcon,
			"notify_icon_small",
			(int)color.r * 65536 + (int)color.g * 256 + (int)color.b,
			LocalForAndroidManager.MainActivityClassName
		});
	}

	public static void CancelNotification(int id)
	{
		LocalForAndroidManager.AndroidJC.CallStatic("CancelNotification", new object[]
		{
			id
		});
	}

	public static void CleanNotification()
	{
		List<int> pushIDs = NativeCallManager.GetPushIDs();
		for (int i = 0; i < pushIDs.get_Count(); i++)
		{
			LocalForAndroidManager.CancelNotification(pushIDs.get_Item(i));
		}
		LocalForAndroidManager.AndroidJC.CallStatic("CancelAll", new object[0]);
	}

	private void Awake()
	{
		LocalForAndroidManager.CleanNotification();
	}

	public static long GetTimeInSecond(DateTime dst_date)
	{
		if (dst_date <= DateTime.get_Now())
		{
			return 0L;
		}
		return (long)(dst_date - DateTime.get_Now()).get_TotalSeconds();
	}
}
