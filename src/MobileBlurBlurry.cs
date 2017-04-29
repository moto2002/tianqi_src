using System;
using UnityEngine;

public class MobileBlurBlurry : PostProcessBase
{
	[Range(0f, 20f)]
	public float Amount = 3f;

	[Range(1f, 8f)]
	public int FastFilter = 1;

	private Material m_matBlurBlurry;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/MobileBlurBlurry"))
		{
			this.m_shaderNames.Add("Hidden/MobileBlurBlurry");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matBlurBlurry = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.m_matBlurBlurry != null)
		{
			int fastFilter = this.FastFilter;
			this.m_matBlurBlurry.SetFloat(ShaderPIDManager._Amount, this.Amount);
			this.m_matBlurBlurry.SetVector(ShaderPIDManager._ScreenResolution, new Vector2((float)(Screen.get_width() / fastFilter), (float)(Screen.get_height() / fastFilter)));
			int num = sourceTexture.get_width() / fastFilter;
			int num2 = sourceTexture.get_height() / fastFilter;
			if (this.FastFilter > 1)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
				temporary.set_filterMode(2);
				Graphics.Blit(sourceTexture, temporary, this.m_matBlurBlurry);
				Graphics.Blit(temporary, destTexture);
				RenderTexture.ReleaseTemporary(temporary);
			}
			else
			{
				Graphics.Blit(sourceTexture, destTexture, this.m_matBlurBlurry);
			}
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
}
