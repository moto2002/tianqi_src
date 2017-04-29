using System;
using UnityEngine;

public sealed class PP_Holywood : PostProcessBase
{
	public float lumThreshold = 0.3f;

	private Material m_matHolywood;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/Holywood"))
		{
			this.m_shaderNames.Add("Hidden/Holywood");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matHolywood = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
		if (this.IsInitSuccessed)
		{
			Matrix4x4 zero = Matrix4x4.get_zero();
			zero.m00 = 0.5149f;
			zero.m01 = 0.3244f;
			zero.m02 = 0.1607f;
			zero.m03 = 0f;
			zero.m10 = 0.2654f;
			zero.m11 = 0.6704f;
			zero.m12 = 0.0642f;
			zero.m13 = 0f;
			zero.m20 = 0.0248f;
			zero.m21 = 0.1248f;
			zero.m22 = 0.8504f;
			zero.m23 = 0f;
			zero.m30 = 0f;
			zero.m31 = 0f;
			zero.m32 = 0f;
			zero.m33 = 0f;
			this.m_matHolywood.SetMatrix(ShaderPIDManager._MtxColor, zero);
			this.m_matHolywood.SetFloat(ShaderPIDManager._LumThreshold, this.lumThreshold);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.RenderImage(source, destination);
	}

	public void RenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matHolywood == null)
		{
			base.set_enabled(false);
			return;
		}
		this.m_matHolywood.SetFloat("_LumThreshold", this.lumThreshold);
		Graphics.Blit(source, destination, this.m_matHolywood);
	}
}
