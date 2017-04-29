using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCustom_Scale : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	private Animator animator;

	public ButtonCustom.VoidDelegateObj onClickCustom;

	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.animator != null)
		{
			this.animator.PlayInFixedTime("ButtonScaleOnPointerUp");
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.animator != null)
		{
			this.animator.PlayInFixedTime("ButtonScaleOnPointerDown");
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		if (this.onClickCustom != null)
		{
			SoundManager.PlayUI(base.get_gameObject());
			this.onClickCustom(base.get_gameObject());
		}
		UIStateSystem.LockOfClickInterval(0u);
	}
}
