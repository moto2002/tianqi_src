using System;
using UnityEngine;
using UnityEngine.UI;

public class PetCDControl : MonoBehaviour
{
	protected bool hasInit;

	protected Image petCDImage;

	protected bool isPetCDing;

	protected float petCDTotalTime;

	protected float petCDLeftTime;

	protected Action petCDEndAction;

	protected void Init()
	{
		if (this.hasInit)
		{
			return;
		}
		this.hasInit = true;
		this.petCDImage = base.GetComponent<Image>();
	}

	public void SetPetCD(float time, float percentage, Action endAct)
	{
		this.Init();
		if (time == 0f || percentage <= 0f)
		{
			return;
		}
		if (this.petCDLeftTime > time * percentage)
		{
			return;
		}
		this.isPetCDing = true;
		this.petCDTotalTime = time;
		this.petCDLeftTime = time * percentage;
		this.petCDEndAction = endAct;
		this.petCDImage.set_fillAmount(percentage);
		Utils.SetTransformZOn(base.get_transform(), true);
	}

	public void ResetPetCD()
	{
		this.Init();
		this.isPetCDing = false;
		this.petCDTotalTime = 0f;
		this.petCDLeftTime = 0f;
		this.petCDEndAction = null;
		this.petCDImage.set_fillAmount(1f);
		Utils.SetTransformZOn(base.get_transform(), false);
	}

	private void Update()
	{
		if (!this.hasInit)
		{
			return;
		}
		if (!this.isPetCDing)
		{
			return;
		}
		this.UpdatePetCD(Time.get_deltaTime());
	}

	private void UpdatePetCD(float deltaTime)
	{
		this.petCDLeftTime -= deltaTime;
		if (this.petCDLeftTime > 0f)
		{
			this.petCDImage.set_fillAmount(this.petCDLeftTime / this.petCDTotalTime);
		}
		else
		{
			this.ResetPetCD();
			if (this.petCDEndAction != null)
			{
				this.petCDEndAction.Invoke();
				this.petCDEndAction = null;
			}
		}
	}
}
