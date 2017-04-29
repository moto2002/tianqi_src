using System;
using UnityEngine;

public class FrostEffect : PostProcessBase
{
	public float m_fFrostAmount = 0.5f;

	public float m_fEdgeSharpness = 1f;

	public float m_fMinFrost;

	public float m_fMaxFrost = 1f;

	public float m_fSeethroughness = 0.2f;

	public float m_fDistortion = 0.1f;

	private Material m_matFrost;

	private string FrostName = "FrostIce";

	private string FrostNormalsName = "FrostIce_N";

	private string FrostBrokenName = "FrostIce_Broken";

	private Texture2D m_texFrost;

	private Texture2D m_texFrostNormals;

	private int m_iBroken = 1;

	private Texture2D m_texFrostBroken;

	private Texture2D Frost
	{
		get
		{
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texFrost, this.FrostName);
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

	private Texture2D FrostBroken
	{
		get
		{
			this.m_texFrostBroken = null;
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texFrostBroken, this.FrostBrokenName + this.m_iBroken);
			return this.m_texFrostBroken;
		}
	}

	private void Awake()
	{
		EventDispatcher.AddListener<int>(ShaderEffectEvent.FROST_SET_BROKEN, new Callback<int>(this.SwitchFrostBroken));
	}

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/FrostImageBlend"))
		{
			this.m_shaderNames.Add("Hidden/FrostImageBlend");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matFrost = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.InitVariables();
			this.m_matFrost.SetTexture("_BlendTex", this.Frost);
			this.m_matFrost.SetTexture("_BumpMap", this.FrostNormals);
			this.SwitchFrostBroken(1);
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

	private void SwitchFrostBroken(int iChoose = 1)
	{
		if (this.m_matFrost == null)
		{
			this.Initialization();
		}
		this.m_iBroken = iChoose;
		if (this.m_matFrost.HasProperty("_BrokenText"))
		{
			this.m_matFrost.SetTexture("_BrokenText", this.FrostBroken);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matFrost == null)
		{
			base.set_enabled(false);
			return;
		}
		this.m_matFrost.SetFloat(ShaderPIDManager._BlendAmount, Mathf.Clamp01(Mathf.Clamp01(this.m_fFrostAmount) * (this.m_fMaxFrost - this.m_fMinFrost) + this.m_fMinFrost));
		this.m_matFrost.SetFloat(ShaderPIDManager._EdgeSharpness, this.m_fEdgeSharpness);
		this.m_matFrost.SetFloat(ShaderPIDManager._SeeThroughness, this.m_fSeethroughness);
		this.m_matFrost.SetFloat(ShaderPIDManager._Distortion, this.m_fDistortion);
		Graphics.Blit(source, destination, this.m_matFrost);
	}
}
