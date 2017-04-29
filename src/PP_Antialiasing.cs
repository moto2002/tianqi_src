using System;
using UnityEngine;

public class PP_Antialiasing : PostProcessBase
{
	public enum AAMode
	{
		SSAA = 1,
		NFAA,
		DLAA,
		FXAA3Console
	}

	public PP_Antialiasing.AAMode mode = PP_Antialiasing.AAMode.SSAA;

	public float edgeThresholdMin = 0.05f;

	public float edgeThreshold = 0.2f;

	public float edgeSharpness = 4f;

	private Material m_matSSAA;

	private Material m_matFXAAIII;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/SSAA"))
		{
			this.m_shaderNames.Add("Hidden/SSAA");
		}
		if (!this.m_shaderNames.Contains("Hidden/FXAA III (Console)"))
		{
			this.m_shaderNames.Add("Hidden/FXAA III (Console)");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matSSAA = this.m_materials.get_Item("Hidden/SSAA");
		this.m_matFXAAIII = this.m_materials.get_Item("Hidden/FXAA III (Console)");
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.mode == PP_Antialiasing.AAMode.SSAA && this.m_matSSAA != null)
		{
			Graphics.Blit(source, destination, this.m_matSSAA);
		}
		if (this.mode == PP_Antialiasing.AAMode.FXAA3Console && this.m_matFXAAIII != null)
		{
			this.m_matFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
			this.m_matFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
			this.m_matFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
			Graphics.Blit(source, destination, this.m_matFXAAIII);
		}
	}
}
