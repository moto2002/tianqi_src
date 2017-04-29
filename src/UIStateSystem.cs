using System;

public class UIStateSystem
{
	public class EventNames
	{
		public const string ResetFPSSleep = "UIStateSystem.ResetFPSSleep";
	}

	private static uint click_interval_lock_timerId;

	private static bool m_is_click_interval_lock;

	private static bool is_click_interval_lock
	{
		get
		{
			return UIStateSystem.m_is_click_interval_lock;
		}
		set
		{
			UIStateSystem.m_is_click_interval_lock = value;
		}
	}

	public static bool IsEventSystemLock(bool lockOfInterval = false)
	{
		EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
		return UIStateSystem.IsEventSystemLockOfMotionSystem() || UIStateSystem.IsEventSystemLockOfMintime() || UIStateSystem.IsEventSystemLockOfMoveFinger() || UIStateSystem.is_click_interval_lock;
	}

	public static void LockOfWaitOpenUI(string uiNameOfWaiting)
	{
		if (UINodesManager.T2RootOfSpecial == null)
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("WaitingUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush);
		WaitingUIView.Instance.LockOnOfWaitOpenUI(uiNameOfWaiting);
	}

	public static void LockOfWaitNextGuide()
	{
		if (UINodesManager.T2RootOfSpecial == null)
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("WaitingUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush);
		WaitingUIView.Instance.LockOnOfWaitNextGuide();
	}

	public static void LockOfClickInterval(uint lock_time = 0u)
	{
		if (lock_time > 0u)
		{
			UIStateSystem.is_click_interval_lock = true;
			TimerHeap.DelTimer(UIStateSystem.click_interval_lock_timerId);
			UIStateSystem.click_interval_lock_timerId = TimerHeap.AddTimer(lock_time, 0, delegate
			{
				UIStateSystem.is_click_interval_lock = false;
			});
		}
	}

	private static bool IsEventSystemLockOfMotionSystem()
	{
		return UIMotionSystem.IsLock;
	}

	private static bool IsEventSystemLockOfMoveFinger()
	{
		return GuideManager.Instance.finger_move_lock;
	}

	private static bool IsEventSystemLockOfMintime()
	{
		return GuideManager.Instance.mintime_lock;
	}

	public static string PrintMessage()
	{
		return string.Concat(new object[]
		{
			"IsEventSystemLockOfMotionSystem = ",
			UIStateSystem.IsEventSystemLockOfMotionSystem(),
			"\nIsEventSystemLockOfMintime = ",
			UIStateSystem.IsEventSystemLockOfMintime(),
			"\nIsEventSystemLockOfMoveFinger = ",
			UIStateSystem.IsEventSystemLockOfMoveFinger(),
			"\nis_click_interval_lock = ",
			UIStateSystem.is_click_interval_lock
		});
	}
}
