using System;
using UnityEngine;

public abstract class BaseEventListener : MonoBehaviour
{
	protected bool IsAddListenersSuccess;

	private void Awake()
	{
		this.AddListenersWhenAwake();
	}

	protected void AddListenersWhenAwake()
	{
		if (!this.IsAddListenersSuccess)
		{
			this.AddListeners();
			this.IsAddListenersSuccess = true;
		}
	}

	protected virtual void OnDestroy()
	{
		if (this.IsAddListenersSuccess)
		{
			this.RemoveListeners();
			this.IsAddListenersSuccess = false;
		}
	}

	protected virtual void AddListeners()
	{
	}

	protected virtual void RemoveListeners()
	{
	}
}
