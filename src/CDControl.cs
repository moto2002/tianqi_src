using System;
using UnityEngine;
using UnityEngine.UI;

public class CDControl : MonoBehaviour
{
	protected bool hasInit;

	protected Image cdImage;

	protected bool isCDing;

	protected float cdTotalTime;

	protected float cdLeftTime;

	protected Action cdEndAction;

	protected void Init()
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.cdImage = base.GetComponent<Image>();
	}

	public void SetCD(float time, float percentage, Action endAct)
	{
		this.Init();
		if (time == 0f || percentage == 0f)
		{
			return;
		}
		if (this.cdLeftTime > time * percentage)
		{
			return;
		}
		this.isCDing = true;
		this.cdTotalTime = time;
		this.cdLeftTime = time * percentage;
		this.cdEndAction = endAct;
		this.cdImage.set_fillAmount(percentage);
		Utils.SetTransformZOn(base.get_transform(), true);
	}

	public void ResetCD()
	{
		this.Init();
		this.isCDing = false;
		this.cdTotalTime = 0f;
		this.cdLeftTime = 0f;
		this.cdEndAction = null;
		this.cdImage.set_fillAmount(1f);
		Utils.SetTransformZOn(base.get_transform(), false);
	}

	private void Update()
	{
		if (!this.hasInit)
		{
			return;
		}
		if (!this.isCDing)
		{
			return;
		}
		this.UpdateCD(Time.get_deltaTime());
	}

	protected void UpdateCD(float deltaTime)
	{
		this.cdLeftTime -= deltaTime;
		if (this.cdLeftTime > 0f)
		{
			this.cdImage.set_fillAmount(this.cdLeftTime / this.cdTotalTime);
		}
		else
		{
			if (this.cdEndAction != null)
			{
				this.cdEndAction.Invoke();
				this.cdEndAction = null;
			}
			this.ResetCD();
		}
	}
}
