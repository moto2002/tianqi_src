using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
	public delegate void VoidDelegateGameObject(GameObject go);

	public delegate void VoidDelegateData(PointerEventData eventData);

	public EventTriggerListener.VoidDelegateGameObject onClick;

	public EventTriggerListener.VoidDelegateGameObject onDown;

	public EventTriggerListener.VoidDelegateGameObject onEnter;

	public EventTriggerListener.VoidDelegateGameObject onExit;

	public EventTriggerListener.VoidDelegateGameObject onUp;

	public EventTriggerListener.VoidDelegateGameObject onSelect;

	public EventTriggerListener.VoidDelegateGameObject onUpdateSelect;

	public EventTriggerListener.VoidDelegateData onDrag;

	public EventTriggerListener.VoidDelegateData onBeginDrag;

	public EventTriggerListener.VoidDelegateData onEndDrag;

	public EventTriggerListener.VoidDelegateData onClickData;

	public EventTriggerListener.VoidDelegateData onPointerEnter;

	public EventTriggerListener.VoidDelegateData onPointerExit;

	public EventTriggerListener.VoidDelegateData onPointerDown;

	public EventTriggerListener.VoidDelegateData onPointerUp;

	public static EventTriggerListener Get(GameObject go)
	{
		EventTriggerListener eventTriggerListener = go.GetComponent<EventTriggerListener>();
		if (eventTriggerListener == null)
		{
			eventTriggerListener = go.AddComponent<EventTriggerListener>();
		}
		return eventTriggerListener;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		if (this.onClickData != null)
		{
			this.onClickData(eventData);
		}
		if (this.onClick != null)
		{
			SoundManager.PlayUI(base.get_gameObject());
			this.onClick(base.get_gameObject());
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		if (this.onDown != null)
		{
			this.onDown(base.get_gameObject());
		}
		if (this.onPointerDown != null)
		{
			this.onPointerDown(eventData);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		if (this.onUp != null)
		{
			this.onUp(base.get_gameObject());
		}
		if (this.onPointerUp != null)
		{
			this.onPointerUp(eventData);
		}
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		if (this.onBeginDrag != null)
		{
			this.onBeginDrag(eventData);
		}
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		if (this.onEndDrag != null)
		{
			this.onEndDrag(eventData);
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (this.onPointerEnter != null)
		{
			this.onPointerEnter(eventData);
		}
	}

	public override void OnDrag(PointerEventData eventData)
	{
		if (this.onDrag != null)
		{
			this.onDrag(eventData);
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if (this.onExit != null)
		{
			this.onExit(base.get_gameObject());
		}
		if (this.onPointerExit != null)
		{
			this.onPointerExit(eventData);
		}
	}

	public override void OnSelect(BaseEventData eventData)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.get_gameObject());
		}
	}

	public override void OnUpdateSelected(BaseEventData eventData)
	{
		if (this.onUpdateSelect != null)
		{
			this.onUpdateSelect(base.get_gameObject());
		}
	}
}
