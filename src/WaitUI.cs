using Foundation.Core.Databinding;
using System;

public class WaitUI : UIBase
{
	public const uint WAITING_MAX_NETWORK_DATA = 3000u;

	public const uint WAITING_MAX_UIOPNE = 3000u;

	public const uint WAITING_MAX = 30000u;

	public static WaitUI Instance;

	private static uint timer_id;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
		this.isInterruptStick = false;
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		WaitUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	public static void OpenUI(uint millsecond_to_auto_close = 0u)
	{
		TimerHeap.DelTimer(WaitUI.timer_id);
		uint start = 30000u;
		if (millsecond_to_auto_close > 0u && millsecond_to_auto_close < 30000u)
		{
			start = millsecond_to_auto_close;
		}
		WaitUI.timer_id = TimerHeap.AddTimer(start, 0, delegate
		{
			WaitUI.CloseUINow();
		});
		UIManagerControl.Instance.OpenUI("WaitUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush);
	}

	public static void CloseUI(uint millsecond_to_auto_close = 0u)
	{
		if (millsecond_to_auto_close > 0u)
		{
			TimerHeap.DelTimer(WaitUI.timer_id);
			WaitUI.timer_id = TimerHeap.AddTimer(millsecond_to_auto_close, 0, delegate
			{
				WaitUI.CloseUINow();
			});
		}
		else
		{
			WaitUI.CloseUINow();
		}
	}

	public static void CloseUINow()
	{
		TimerHeap.DelTimer(WaitUI.timer_id);
		UIManagerControl.Instance.HideUI("WaitUI");
	}

	public static void TestOpenUI01()
	{
		WaitUI.OpenUI(3000u);
		WaitUI.Instance.FindTransform("Wait").get_gameObject().SetActive(true);
		WaitUI.Instance.FindTransform("Sequence").get_gameObject().SetActive(false);
	}

	public static void TestOpenUI02()
	{
		WaitUI.OpenUI(3000u);
		WaitUI.Instance.FindTransform("Wait").get_gameObject().SetActive(false);
		WaitUI.Instance.FindTransform("Sequence").get_gameObject().SetActive(true);
	}
}
