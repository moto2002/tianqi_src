using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuideFocusWidget : MonoBehaviour
{
	private EventTriggerListener m_eventlistener;

	private ButtonCustom m_buttoncustom;

	private ButtonCustom_Scale m_buttoncustomscale;

	private Button m_button;

	private Toggle m_toggle;

	private void OnEnable()
	{
		if (GuideManager.Instance.IsNeedChangeGuideLayer)
		{
			this.SetDepthOfGuide();
		}
		this.AddListeners();
		EventDispatcher.AddListener("GuideManager.BroadOfEndGuide", new Callback(this.StopGuide));
		EventDispatcher.AddListener<bool>("GuideManager.PauseGuideSystem", new Callback<bool>(this.PauseGuideSystem));
	}

	private void OnDisable()
	{
		this.RemoveListeners();
		EventDispatcher.RemoveListener("GuideManager.BroadOfEndGuide", new Callback(this.StopGuide));
		EventDispatcher.RemoveListener<bool>("GuideManager.PauseGuideSystem", new Callback<bool>(this.PauseGuideSystem));
	}

	private void SetDepthOfGuide()
	{
		DepthOfGuide depthOfGuide = base.get_gameObject().GetComponent<DepthOfGuide>();
		if (depthOfGuide != null)
		{
			depthOfGuide.set_enabled(false);
			depthOfGuide.set_enabled(true);
			depthOfGuide.GuideOn();
		}
		else
		{
			depthOfGuide = base.get_gameObject().AddComponent<DepthOfGuide>();
			depthOfGuide.GuideOn();
		}
	}

	private void AddListeners()
	{
		this.m_eventlistener = base.get_gameObject().GetComponent<EventTriggerListener>();
		if (this.m_eventlistener != null)
		{
			if (this.m_eventlistener.onClick != null)
			{
				EventTriggerListener expr_38 = this.m_eventlistener;
				expr_38.onClick = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_38.onClick, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlClick));
			}
			if (this.m_eventlistener.onUp != null)
			{
				EventTriggerListener expr_6F = this.m_eventlistener;
				expr_6F.onUp = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_6F.onUp, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlPressUp));
			}
			else if (this.m_eventlistener.onDown != null)
			{
				EventTriggerListener expr_AB = this.m_eventlistener;
				expr_AB.onDown = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_AB.onDown, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlPressDown));
			}
			return;
		}
		this.m_buttoncustom = base.get_gameObject().GetComponent<ButtonCustom>();
		if (this.m_buttoncustom != null && this.m_buttoncustom.onClickCustom != null)
		{
			ButtonCustom expr_105 = this.m_buttoncustom;
			expr_105.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_105.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnButtonCustomClick));
			return;
		}
		this.m_buttoncustomscale = base.get_gameObject().GetComponent<ButtonCustom_Scale>();
		if (this.m_buttoncustomscale != null && this.m_buttoncustomscale.onClickCustom != null)
		{
			ButtonCustom_Scale expr_15F = this.m_buttoncustomscale;
			expr_15F.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_15F.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnButtonCustomClick));
			return;
		}
		this.m_button = base.get_gameObject().GetComponent<Button>();
		if (this.m_button != null && this.m_button.get_onClick() != null)
		{
			this.m_button.get_onClick().AddListener(new UnityAction(this.OnButtonClick));
			return;
		}
		this.m_toggle = base.get_gameObject().GetComponent<Toggle>();
		if (this.m_toggle != null)
		{
			if (this.m_toggle.onValueChanged != null)
			{
				this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChange));
			}
			return;
		}
	}

	private void RemoveListeners()
	{
		if (this.m_eventlistener != null)
		{
			EventTriggerListener expr_17 = this.m_eventlistener;
			expr_17.onClick = (EventTriggerListener.VoidDelegateGameObject)Delegate.Remove(expr_17.onClick, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlClick));
			EventTriggerListener expr_3E = this.m_eventlistener;
			expr_3E.onDown = (EventTriggerListener.VoidDelegateGameObject)Delegate.Remove(expr_3E.onDown, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlPressDown));
			EventTriggerListener expr_65 = this.m_eventlistener;
			expr_65.onUp = (EventTriggerListener.VoidDelegateGameObject)Delegate.Remove(expr_65.onUp, new EventTriggerListener.VoidDelegateGameObject(this.OnEtlPressUp));
		}
		if (this.m_buttoncustom != null)
		{
			ButtonCustom expr_9D = this.m_buttoncustom;
			expr_9D.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Remove(expr_9D.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnButtonCustomClick));
		}
		if (this.m_buttoncustomscale != null)
		{
			ButtonCustom_Scale expr_D5 = this.m_buttoncustomscale;
			expr_D5.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Remove(expr_D5.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnButtonCustomClick));
		}
		if (this.m_button != null)
		{
			this.m_button.get_onClick().RemoveListener(new UnityAction(this.OnButtonClick));
		}
		if (this.m_toggle != null)
		{
			this.m_toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnToggleValueChange));
		}
	}

	private void OnButtonClick()
	{
		this.TriggerFocusWidget();
	}

	private void OnButtonCustomClick(GameObject go)
	{
		this.TriggerFocusWidget();
	}

	private void OnEtlClick(GameObject go)
	{
		this.TriggerFocusWidget();
	}

	private void OnEtlPressDown(GameObject go)
	{
		this.TriggerFocusWidget();
	}

	private void OnEtlPressUp(GameObject go)
	{
		this.TriggerFocusWidget();
	}

	private void OnToggleValueChange(bool value)
	{
		this.TriggerFocusWidget();
	}

	private void TriggerFocusWidget()
	{
		if (GuideManager.Instance.finger_move_lock)
		{
			return;
		}
		base.set_enabled(false);
		this.RemoveFocus();
	}

	private void StopGuide()
	{
		if (base.get_enabled())
		{
			base.set_enabled(false);
			this.RemoveFocus();
		}
	}

	private void PauseGuideSystem(bool isPause)
	{
		DepthOfGuide component = base.get_gameObject().GetComponent<DepthOfGuide>();
		if (component != null)
		{
			component.PauseGuideSystem(isPause);
		}
	}

	private void RemoveFocus()
	{
		EventDispatcher.Broadcast("GuideManager.ResetGuideUI");
		if (!InstanceManager.IsActorAnimatorOn)
		{
			InstanceManager.IsActorAnimatorOn = true;
		}
		DepthOfGuide component = base.get_gameObject().GetComponent<DepthOfGuide>();
		if (component != null)
		{
			component.GuideOff();
		}
		EventDispatcher.Broadcast("GuideManager.TriggerFocusWidget");
	}
}
