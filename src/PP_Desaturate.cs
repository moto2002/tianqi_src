using System;
using UnityEngine;

public sealed class PP_Desaturate : PostProcessBase
{
	public float amount = 0.5f;

	private Material m_matDesaturate;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/Desaturate"))
		{
			this.m_shaderNames.Add("Hidden/Desaturate");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matDesaturate = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.m_matDesaturate.SetFloat(ShaderPIDManager._Amount, this.amount);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.m_matDesaturate.SetFloat(ShaderPIDManager._Amount, this.amount);
		Graphics.Blit(source, destination, this.m_matDesaturate);
	}
}
