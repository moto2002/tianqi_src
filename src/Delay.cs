using System;
using UnityEngine;

public class Delay : MonoBehaviour
{
	public float delayTime = 1f;

	private bool takeEffect;

	private void OnEnable()
	{
		this.TriggerDelay();
	}

	private void OnDisable()
	{
		this.takeEffect = false;
	}

	public void TriggerDelay()
	{
		if (!this.takeEffect)
		{
			base.Invoke("DelayFunc", this.delayTime);
			base.get_gameObject().SetActive(false);
			this.takeEffect = true;
		}
	}

	private void DelayFunc()
	{
		base.get_gameObject().SetActive(true);
	}
}
