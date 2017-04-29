using System;
using UnityEngine;

public class FadeInOutShaderColor : MonoBehaviour
{
	public string ShaderColorName = "_Color";

	public float StartDelay;

	public float FadeInSpeed;

	public float FadeOutDelay;

	public float FadeOutSpeed;

	public bool UseSharedMaterial;

	public bool FadeOutAfterCollision;

	public bool UseHideStatus;

	private Material mat;

	private Color oldColor;

	private Color currentColor;

	private float oldAlpha;

	private float alpha;

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

	public void UpdateMaterial(Material instanceMaterial)
	{
		this.mat = instanceMaterial;
		this.InitMaterial();
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
		if (this.ShaderColorName == ShaderPIDManager.Name_Color)
		{
			this.oldColor = this.mat.GetColor(ShaderPIDManager._Color);
		}
		else
		{
			this.oldColor = this.mat.GetColor(this.ShaderColorName);
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
		this.oldAlpha = 0f;
		this.alpha = 0f;
		this.canStart = false;
		this.currentColor = this.oldColor;
		if (this.isIn)
		{
			this.currentColor.a = 0f;
		}
		if (this.ShaderColorName == ShaderPIDManager.Name_Color)
		{
			this.mat.SetColor(ShaderPIDManager._Color, this.currentColor);
		}
		else
		{
			this.mat.SetColor(this.ShaderColorName, this.currentColor);
		}
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
			this.oldAlpha = this.oldColor.a;
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
		this.alpha = this.oldAlpha + Time.get_deltaTime() / this.FadeInSpeed;
		if (this.alpha >= this.oldColor.a)
		{
			this.fadeInComplited = true;
			if (!this.isOut)
			{
				this.allComplited = true;
			}
			this.alpha = this.oldColor.a;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.currentColor.a = this.alpha;
		if (this.ShaderColorName == ShaderPIDManager.Name_Color)
		{
			this.mat.SetColor(ShaderPIDManager._Color, this.currentColor);
		}
		else
		{
			this.mat.SetColor(this.ShaderColorName, this.currentColor);
		}
		this.oldAlpha = this.alpha;
	}

	private void FadeOut()
	{
		this.alpha = this.oldAlpha - Time.get_deltaTime() / this.FadeOutSpeed;
		if (this.alpha <= 0f)
		{
			this.alpha = 0f;
			this.fadeOutComplited = true;
			this.allComplited = true;
		}
		this.currentColor.a = this.alpha;
		if (this.ShaderColorName == ShaderPIDManager.Name_Color)
		{
			this.mat.SetColor(ShaderPIDManager._Color, this.currentColor);
		}
		else
		{
			this.mat.SetColor(this.ShaderColorName, this.currentColor);
		}
		this.oldAlpha = this.alpha;
	}
}
