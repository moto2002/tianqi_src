using System;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
	public const string animName = "animOfUI";

	private Action AnimCallBack;

	protected void EventEnd()
	{
		if (this.AnimCallBack != null)
		{
			this.AnimCallBack.Invoke();
		}
	}

	public static void DoAnim(Transform root, int animId, Action callback = null)
	{
		PanelSwitcher panelSwitcher = root.get_gameObject().AddUniqueComponent<PanelSwitcher>();
		panelSwitcher.AnimCallBack = callback;
		switch (animId)
		{
		case 1:
			panelSwitcher.DoVerticalOpen();
			break;
		case 2:
			panelSwitcher.DoVerticalClose();
			break;
		case 3:
			panelSwitcher.DoHorizonalVerticalOpen();
			break;
		case 4:
			panelSwitcher.DoHorizonalVerticalClose();
			break;
		case 5:
			panelSwitcher.DoVerticalOpen_a();
			break;
		case 6:
			panelSwitcher.DoVerticalClose_a();
			break;
		case 7:
			panelSwitcher.DoHorizonalVerticalOpen_a();
			break;
		case 8:
			panelSwitcher.DoHorizonalVerticalClose_a();
			break;
		}
	}

	private void DoVerticalOpen()
	{
		UIUtils.animPlay(base.get_gameObject(), "UI2VerticalOpen");
	}

	private void DoVerticalClose()
	{
		UIUtils.animPlay(base.get_gameObject(), "UI2VerticalClose");
	}

	private void DoVerticalOpen_a()
	{
		base.get_gameObject().AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(base.get_gameObject(), "UI2VerticalOpen_a");
	}

	private void DoVerticalClose_a()
	{
		base.get_gameObject().AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(base.get_gameObject(), "UI2VerticalClose_a");
	}

	private void DoHorizonalVerticalOpen()
	{
		UIUtils.animPlay(base.get_gameObject(), "UI2HorizonalVerticalOpen");
	}

	private void DoHorizonalVerticalClose()
	{
		UIUtils.animPlay(base.get_gameObject(), "UI2HorizonalVerticalClose");
	}

	private void DoHorizonalVerticalOpen_a()
	{
		base.get_gameObject().AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(base.get_gameObject(), "UI2HorizonalVerticalOpen_a");
	}

	private void DoHorizonalVerticalClose_a()
	{
		base.get_gameObject().AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(base.get_gameObject(), "UI2HorizonalVerticalClose_a");
	}

	private void ApplyScale()
	{
	}

	private void ApplyRotation()
	{
	}

	private void ApplyMove()
	{
	}

	private void ApplyFade()
	{
	}
}
