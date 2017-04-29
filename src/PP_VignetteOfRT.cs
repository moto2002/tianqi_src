using System;
using UnityEngine;

public sealed class PP_VignetteOfRT : PostProcessBase
{
	public float radius = 10f;

	public float darkness = 0.3f;

	private Material m_matVignette;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/VignetteOfRT"))
		{
			this.m_shaderNames.Add("Hidden/VignetteOfRT");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matVignette = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
		if (this.IsInitSuccessed)
		{
			this.m_matVignette.SetFloat(ShaderPIDManager._Radius, this.radius);
			this.m_matVignette.SetFloat(ShaderPIDManager._Darkness, this.darkness);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.RenderImage(source, destination);
	}

	public void RenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matVignette == null)
		{
			base.set_enabled(false);
			return;
		}
		this.m_matVignette.SetFloat(ShaderPIDManager._Radius, this.radius);
		this.m_matVignette.SetFloat(ShaderPIDManager._Darkness, this.darkness);
		Graphics.Blit(source, destination, this.m_matVignette);
	}
}
