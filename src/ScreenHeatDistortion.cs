using System;
using UnityEngine;

public class ScreenHeatDistortion : PostProcessBase
{
	public float Speed = 50f;

	public float Range = 80f;

	private Material m_matHeatDistortion;

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
		if (!this.m_shaderNames.Contains("Hidden/HeatDistortion"))
		{
			this.m_shaderNames.Add("Hidden/HeatDistortion");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matHeatDistortion = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.m_matHeatDistortion.SetTexture("_OffsetTex", this.TexOffset);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matHeatDistortion != null)
		{
			this.m_matHeatDistortion.SetFloat("_Speed", this.Speed);
			this.m_matHeatDistortion.SetFloat("_Range", this.Range);
			Graphics.Blit(source, destination, this.m_matHeatDistortion);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}
}
