using System;
using UnityEngine;

public class FadeInOutShaderFloat : MonoBehaviour
{
	public string PropertyName = "_CutOut";

	public float MaxFloat = 1f;

	public float StartDelay;

	public float FadeInSpeed;

	public float FadeOutDelay;

	public float FadeOutSpeed;

	public bool FadeOutAfterCollision;

	public bool UseHideStatus;

	private Material OwnMaterial;

	private Material mat;

	private float oldFloat;

	private float currentFloat;

	private float startFloat;

	private bool canStart;

	private bool canStartFadeOut;

	private bool fadeInComplited;

	private bool fadeOutComplited;

	private bool isCollisionEnter;

	private bool allComplited;

	private bool isStartDelay;

	private bool isIn;

	private bool isOut;

	private EffectSettings effectSettings;

	private bool isInitialized;

	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.get_parent();
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.get_transform());
			}
		}
	}

	private void Start()
	{
		this.GetEffectSettingsComponent(base.get_transform());
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += new EventHandler<CollisionInfo>(this.prefabSettings_CollisionEnter);
		}
		this.InitMaterial();
	}

	public void UpdateMaterial(Material instanceMaterial)
	{
		this.mat = instanceMaterial;
		this.InitMaterial();
	}

	private void InitMaterial()
	{
		if (this.isInitialized)
		{
			return;
		}
		if (base.GetComponent<Renderer>() != null)
		{
			this.mat = base.GetComponent<Renderer>().get_material();
		}
		else if (this.mat == null)
		{
			return;
		}
		this.isStartDelay = (this.StartDelay > 0.001f);
		this.isIn = (this.FadeInSpeed > 0.001f);
		this.isOut = (this.FadeOutSpeed > 0.001f);
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	private void InitDefaultVariables()
	{
		this.fadeInComplited = false;
		this.fadeOutComplited = false;
		this.allComplited = false;
		this.canStartFadeOut = false;
		this.isCollisionEnter = false;
		this.oldFloat = 0f;
		this.currentFloat = this.MaxFloat;
		if (this.isIn)
		{
			this.currentFloat = 0f;
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
		if (this.isStartDelay)
		{
			base.Invoke("SetupStartDelay", this.StartDelay);
		}
		else
		{
			this.canStart = true;
		}
		if (!this.isIn)
		{
			if (!this.FadeOutAfterCollision)
			{
				base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
			}
			this.oldFloat = this.MaxFloat;
		}
	}

	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		if (!this.isIn && this.FadeOutAfterCollision)
		{
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
	}

	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	private void SetupStartDelay()
	{
		this.canStart = true;
	}

	private void SetupFadeOutDelay()
	{
		this.canStartFadeOut = true;
	}

	private void Update()
	{
		if (!this.canStart)
		{
			return;
		}
		if (this.effectSettings != null && this.UseHideStatus && this.allComplited && this.effectSettings.IsVisible)
		{
			this.allComplited = false;
			this.fadeInComplited = false;
			this.fadeOutComplited = false;
			this.InitDefaultVariables();
		}
		if (this.isIn && !this.fadeInComplited)
		{
			if (this.effectSettings == null)
			{
				this.FadeIn();
			}
			else if ((this.UseHideStatus && this.effectSettings.IsVisible) || !this.UseHideStatus)
			{
				this.FadeIn();
			}
		}
		if (!this.isOut || this.fadeOutComplited || !this.canStartFadeOut)
		{
			return;
		}
		if (this.effectSettings == null || (!this.UseHideStatus && !this.FadeOutAfterCollision))
		{
			this.FadeOut();
		}
		else if ((this.UseHideStatus && !this.effectSettings.IsVisible) || this.isCollisionEnter)
		{
			this.FadeOut();
		}
	}

	private void FadeIn()
	{
		this.currentFloat = this.oldFloat + Time.get_deltaTime() / this.FadeInSpeed * this.MaxFloat;
		if (this.currentFloat >= this.MaxFloat)
		{
			this.fadeInComplited = true;
			if (!this.isOut)
			{
				this.allComplited = true;
			}
			this.currentFloat = this.MaxFloat;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
		this.oldFloat = this.currentFloat;
	}

	private void FadeOut()
	{
		this.currentFloat = this.oldFloat - Time.get_deltaTime() / this.FadeOutSpeed * this.MaxFloat;
		if (this.currentFloat <= 0f)
		{
			this.currentFloat = 0f;
			this.fadeOutComplited = true;
			this.allComplited = true;
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
		this.oldFloat = this.currentFloat;
	}
}
