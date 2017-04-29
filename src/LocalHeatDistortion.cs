using System;
using UnityEngine;

public class LocalHeatDistortion : PostProcessBase
{
	public float Speed = 15f;

	public float Range = 50f;

	public float OffsetPixel = 0.03f;

	private Material m_matLHD;

	private string OffsetTexName = "Distortion_Water";

	private Texture2D m_texOffset;

	private Texture2D TexOffset
	{
		get
		{
			ShaderEffectUtils.SafeCreateTexture(ref this.m_texOffset, this.OffsetTexName);
			return this.m_texOffset;
		}
	}

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/LocalHeatDistortion"))
		{
			this.m_shaderNames.Add("Hidden/LocalHeatDistortion");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matLHD = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		this.IsDestoryOnDisable = false;
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.m_matLHD.SetTexture(ShaderPIDManager._ClipTex, RTManager.Instance.RTCommon);
			this.m_matLHD.SetTexture(ShaderPIDManager._OffsetTex, this.TexOffset);
			this.m_matLHD.SetFloat(ShaderPIDManager._Speed, this.Speed);
			this.m_matLHD.SetFloat(ShaderPIDManager._Range, this.Range);
			this.m_matLHD.SetFloat(ShaderPIDManager._OffsetPixel, this.OffsetPixel);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, this.m_matLHD);
	}
}
