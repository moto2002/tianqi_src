using System;
using UnityEngine;

public class MobileBloom : PostProcessBase
{
	public enum BloomType
	{
		None,
		Lightning
	}

	public MobileBloom.BloomType m_bloomType;

	public float m_fInitIntensity = 0.5f;

	public float m_fAddIntensityMax = 1f;

	public float m_fAddIntensity;

	public Color m_colorMix = Color.get_white();

	public float m_colorMixBlend = 0.25f;

	private Material m_matBloom;

	public float m_fLightningRandom = 10f;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/MobileBloom"))
		{
			this.m_shaderNames.Add("Hidden/MobileBloom");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matBloom = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public void Initialization(MobileBloom.BloomType bloomType)
	{
		this.m_bloomType = bloomType;
		if (this.m_bloomType == MobileBloom.BloomType.None)
		{
			base.set_enabled(false);
			return;
		}
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.InitValue(0f, 0.25f);
		}
	}

	public void InitValue(float intensity = 0f, float blend = 0.25f)
	{
		this.m_fInitIntensity = intensity;
		this.m_colorMixBlend = blend;
	}

	private void Update()
	{
		this.LightningUpdate();
	}

	private void OnRenderImage(RenderTexture srcRt, RenderTexture dstRt)
	{
		if (this.m_materials == null)
		{
			base.set_enabled(false);
			return;
		}
		MobileBloom.BloomType bloomType = this.m_bloomType;
		if (bloomType != MobileBloom.BloomType.Lightning)
		{
			base.set_enabled(false);
		}
		else
		{
			this.Lightning(srcRt, dstRt);
		}
	}

	private void Lightning(RenderTexture srcRt, RenderTexture dstRt)
	{
		this.m_fAddIntensity = Mathf.Clamp01(this.m_fAddIntensity - Time.get_deltaTime() * 2.75f);
		RenderTexture temporary = RenderTexture.GetTemporary(srcRt.get_width() / 4, srcRt.get_height() / 4, 0, PostProcessBase.RTFormat);
		RenderTexture temporary2 = RenderTexture.GetTemporary(srcRt.get_width() / 4, srcRt.get_height() / 4, 0, PostProcessBase.RTFormat);
		this.m_matBloom.SetColor(ShaderPIDManager._ColorMix, this.m_colorMix);
		this.m_matBloom.SetVector(ShaderPIDManager._Parameter, new Vector4(this.m_colorMixBlend * 0.25f, 0f, 0f, 1f - this.m_fInitIntensity - this.m_fAddIntensity));
		Graphics.Blit(srcRt, temporary, this.m_matBloom, (this.m_fAddIntensity >= 0.5f) ? 5 : 1);
		Graphics.Blit(temporary, temporary2, this.m_matBloom, 2);
		Graphics.Blit(temporary2, temporary, this.m_matBloom, 3);
		this.m_matBloom.SetTexture("_Bloom", temporary);
		Graphics.Blit(srcRt, dstRt, this.m_matBloom, (!GameLevelManager.IsPostProcessReachQuality(250)) ? 0 : 4);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	private void LightningUpdate()
	{
		if (base.IsTime1Over())
		{
			base.Circle1 = Random.Range(0f, this.m_fLightningRandom);
			this.m_fAddIntensity = this.m_fAddIntensityMax;
		}
	}
}
