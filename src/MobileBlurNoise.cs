using System;
using UnityEngine;

public class MobileBlurNoise : PostProcessBase
{
	private float TimeX = 1f;

	private Vector4 ScreenResolution;

	[Range(2f, 16f)]
	public int Level = 16;

	public Vector2 Distance = new Vector2(30f, 0f);

	private Material m_matBlurNoise;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/MobileBlur_Noise"))
		{
			this.m_shaderNames.Add("Hidden/MobileBlur_Noise");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matBlurNoise = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.m_matBlurNoise != null)
		{
			this.TimeX += Time.get_deltaTime();
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.m_matBlurNoise.SetFloat(ShaderPIDManager._TimeX, this.TimeX);
			this.m_matBlurNoise.SetFloat(ShaderPIDManager._Level, (float)this.Level);
			this.m_matBlurNoise.SetVector(ShaderPIDManager._Distance, this.Distance);
			this.m_matBlurNoise.SetVector(ShaderPIDManager._ScreenResolution, new Vector4((float)sourceTexture.get_width(), (float)sourceTexture.get_height(), 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.m_matBlurNoise);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
}
