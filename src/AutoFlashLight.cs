using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoFlashLight : MonoBehaviour
{
	public bool Inited;

	public bool Loop;

	public float TimeOnce = 0.4f;

	public float Interval = 5f;

	private uint timerId;

	public void ShowEffect(bool isShow)
	{
		if (isShow)
		{
			this.Inited = true;
			if (base.get_gameObject().get_activeInHierarchy() && base.get_enabled())
			{
				this.OnEnable();
			}
		}
		else
		{
			this.Inited = false;
			this.SwitchNormal();
		}
	}

	private void OnEnable()
	{
		if (!this.Inited)
		{
			return;
		}
		TimerHeap.DelTimer(this.timerId);
		this.SetFlashLight(true);
		uint start = (uint)(this.Interval * 1000f);
		if (!this.Loop)
		{
			this.timerId = TimerHeap.AddTimer(start, 0, delegate
			{
				this.SetFlashLight(false);
			});
		}
	}

	private void SwitchNormal()
	{
		TimerHeap.DelTimer(this.timerId);
		this.SetFlashLight(false);
	}

	private void SetFlashLight(bool flash)
	{
		Material material;
		if (flash)
		{
			material = ShaderEffectUtils.CreateAutoFlashLightMat(this.TimeOnce, this.Interval);
		}
		else
		{
			material = null;
		}
		Image component = base.GetComponent<Image>();
		if (component != null)
		{
			component.set_material(material);
		}
		else
		{
			RawImage component2 = base.GetComponent<RawImage>();
			if (component2 != null)
			{
				component2.set_material(material);
			}
		}
	}
}
