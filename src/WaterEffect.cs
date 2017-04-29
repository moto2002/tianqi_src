using System;
using UnityEngine;

public class WaterEffect : PostProcessBase
{
	public float m_fFrostAmount = 0.5f;

	public float m_fEdgeSharpness = 1f;

	public float m_fMinFrost;

	public float m_fMaxFrost = 1f;

	public float m_fSeethroughness = 0.2f;

	public float m_fDistortion = 0.1f;

	private Material m_matImageBlend;

	private string FrostNormalsName = "FrostIce_N";

	private string ScreenWaterName = "ScreenWater";

	private Texture2D m_texFrost;

	private Texture2D m_texFrostNormals;

	private Texture2D Frost
	{
		get
		{
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texFrost, this.ScreenWaterName);
			return this.m_texFrost;
		}
	}

	private Texture2D FrostNormals
	{
		get
		{
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texFrostNormals, this.FrostNormalsName);
			return this.m_texFrost;
		}
	}

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/ImageBlend"))
		{
			this.m_shaderNames.Add("Hidden/ImageBlend");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matImageBlend = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.InitVariables();
			this.m_matImageBlend.SetTexture("_BlendTex", this.Frost);
			this.m_matImageBlend.SetTexture("_BumpMap", this.FrostNormals);
		}
	}

	private void InitVariables()
	{
		this.m_fFrostAmount = 1.02f;
		this.m_fEdgeSharpness = 1.85f;
		this.m_fMinFrost = 0f;
		this.m_fMaxFrost = 1f;
		this.m_fSeethroughness = 0.39f;
		this.m_fDistortion = 0.1f;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matImageBlend == null)
		{
			base.set_enabled(false);
			return;
		}
		this.m_matImageBlend.SetFloat(ShaderPIDManager._BlendAmount, Mathf.Clamp01(Mathf.Clamp01(this.m_fFrostAmount) * (this.m_fMaxFrost - this.m_fMinFrost) + this.m_fMinFrost));
		this.m_matImageBlend.SetFloat(ShaderPIDManager._EdgeSharpness, this.m_fEdgeSharpness);
		this.m_matImageBlend.SetFloat(ShaderPIDManager._SeeThroughness, this.m_fSeethroughness);
		this.m_matImageBlend.SetFloat(ShaderPIDManager._Distortion, this.m_fDistortion);
		Graphics.Blit(source, destination, this.m_matImageBlend);
	}
}
