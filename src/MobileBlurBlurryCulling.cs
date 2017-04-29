using System;
using UnityEngine;

public class MobileBlurBlurryCulling : PostProcessBase
{
	[Range(0f, 20f)]
	public float AmountFinal = 3f;

	public float AmountNow;

	public float AmountToShader;

	public float AmountRate_N2F = 2.8f;

	public float AmountRate_F2N = 4.5f;

	[Range(1f, 8f)]
	public int FastFilter = 1;

	public LayerMask ExcludeLayers = 0;

	public bool EnableSlide = true;

	private Material m_matBlurBlurryCulling;

	private bool mBlurSwitch;

	public float AngleEffectByDistance;

	public static float AngleCameraBlurThreshold = 0.2f;

	public bool BlurSwitch
	{
		get
		{
			return this.mBlurSwitch;
		}
		set
		{
			this.mBlurSwitch = value;
		}
	}

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/MobileBlurBlurry_Culling"))
		{
			this.m_shaderNames.Add("Hidden/MobileBlurBlurry_Culling");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matBlurBlurryCulling = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		this.IsDestoryOnDisable = false;
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			EventDispatcher.Broadcast<bool, RTManagerOfPostProcess.PostProcessType>("RTManager.ENABLE_POSTPROCESS_TYPE", true, RTManagerOfPostProcess.PostProcessType.MobileBlurBlurry);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ResetAmountNow();
		EventDispatcher.Broadcast<bool, RTManagerOfPostProcess.PostProcessType>("RTManager.ENABLE_POSTPROCESS_TYPE", false, RTManagerOfPostProcess.PostProcessType.MobileBlurBlurry);
	}

	private void ResetAmountNow()
	{
		this.AmountNow = 0f;
	}

	public void BlurOnOff(bool bSwitch, float angleEffect)
	{
		this.AngleEffectByDistance = angleEffect;
		this.BlurSwitch = bSwitch;
	}

	public static float CalAngleEffect(float angle, float distanceCamera2Lp)
	{
		return angle;
	}

	private float CalAmount()
	{
		if (!this.EnableSlide)
		{
			return this.AmountFinal;
		}
		float num = this.AmountNow * this.AngleEffectByDistance;
		num = Mathf.Clamp(num, 0.5f, 3.5f);
		this.AmountToShader = Mathf.Lerp(this.AmountToShader, num, 0.2f);
		return this.AmountToShader;
	}

	private void Update()
	{
		if (!this.EnableSlide)
		{
			return;
		}
		if (this.BlurSwitch)
		{
			this.AmountNow += Time.get_deltaTime() * this.AmountRate_N2F;
			this.AmountNow = Mathf.Min(this.AmountNow, this.AmountFinal);
			if (this.AmountNow == this.AmountFinal)
			{
			}
		}
		else
		{
			this.AmountNow -= Time.get_deltaTime() * this.AmountRate_F2N;
			if (this.AmountNow <= 0f)
			{
				base.set_enabled(false);
			}
		}
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.m_matBlurBlurryCulling != null)
		{
			this.m_matBlurBlurryCulling.SetTexture(ShaderPIDManager._CullingTex, RTManagerOfPostProcess.Instance.RTPostProcess);
			int fastFilter = this.FastFilter;
			this.m_matBlurBlurryCulling.SetFloat(ShaderPIDManager._Amount, this.CalAmount());
			this.m_matBlurBlurryCulling.SetVector(ShaderPIDManager._ScreenResolution, new Vector2((float)(Screen.get_width() / fastFilter), (float)(Screen.get_height() / fastFilter)));
			int num = sourceTexture.get_width() / fastFilter;
			int num2 = sourceTexture.get_height() / fastFilter;
			if (this.FastFilter > 1)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, PostProcessBase.RTFormat);
				temporary.set_filterMode(2);
				Graphics.Blit(sourceTexture, temporary, this.m_matBlurBlurryCulling);
				Graphics.Blit(temporary, destTexture);
				RenderTexture.ReleaseTemporary(temporary);
			}
			else
			{
				Graphics.Blit(sourceTexture, destTexture, this.m_matBlurBlurryCulling);
			}
		}
	}
}
