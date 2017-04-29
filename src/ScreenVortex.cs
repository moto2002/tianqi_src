using System;
using UnityEngine;

public class ScreenVortex : PostProcessBase
{
	public Vector2 m_vec2Center = new Vector2(0.5f, 0.5f);

	public Vector2 m_vec2Radius = new Vector2(0.4f, 0.4f);

	public float m_fAngle = 50f;

	private Material m_matVortex;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/Vortex"))
		{
			this.m_shaderNames.Add("Hidden/Vortex");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matVortex = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public override void Initialization()
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.InitVariables();
		}
	}

	private void InitVariables()
	{
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matVortex == null)
		{
			return;
		}
		bool flag = source.get_texelSize().y < 0f;
		if (flag)
		{
			this.m_vec2Center.y = 1f - this.m_vec2Center.y;
			this.m_fAngle = -this.m_fAngle;
		}
		Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.get_zero(), Quaternion.Euler(0f, 0f, this.m_fAngle), Vector3.get_one());
		this.m_matVortex.SetMatrix(ShaderPIDManager._RotationMatrix, matrix4x);
		this.m_matVortex.SetVector(ShaderPIDManager._CenterRadius, new Vector4(this.m_vec2Center.x, this.m_vec2Center.y, this.m_vec2Radius.x, this.m_vec2Radius.y));
		this.m_matVortex.SetFloat(ShaderPIDManager._Angle, this.m_fAngle * 0.0174532924f);
		Graphics.Blit(source, destination, this.m_matVortex);
	}
}
