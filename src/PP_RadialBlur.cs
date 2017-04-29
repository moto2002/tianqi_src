using System;
using UnityEngine;

public sealed class PP_RadialBlur : PostProcessBase
{
	public float m_fCenterX = 0.5f;

	public float m_fCenterY = 0.5f;

	public float m_fStrength = 0.2f;

	private Material m_matRadialBlur;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hidden/RadialBlur"))
		{
			this.m_shaderNames.Add("Hidden/RadialBlur");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matRadialBlur = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public void Initialization(Vector3 pos)
	{
		base.Initialization();
		this.IsDestoryOnDisable = false;
		if (this.IsInitSuccessed)
		{
			this.m_fStrength = 0.12f;
			this.SetCenter(pos);
			this.m_matRadialBlur.SetFloat(ShaderPIDManager._CenterX, this.m_fCenterX);
			this.m_matRadialBlur.SetFloat(ShaderPIDManager._CenterY, this.m_fCenterY);
			this.m_matRadialBlur.SetFloat(ShaderPIDManager._Strength, this.m_fStrength);
		}
	}

	private void SetCenter(Vector3 pos)
	{
		Vector3 vector = CamerasMgr.CameraMain.WorldToScreenPoint(pos);
		this.m_fCenterX = Mathf.Max(vector.x / (float)Screen.get_width(), 0.01f);
		this.m_fCenterY = Mathf.Max(vector.y / (float)Screen.get_height(), 0.01f);
	}

	private void Update()
	{
		this.m_fStrength = Mathf.Clamp01(this.m_fStrength - Time.get_deltaTime() * 0.5f);
		if (this.m_fStrength <= 0f)
		{
			base.set_enabled(false);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_matRadialBlur == null)
		{
			base.set_enabled(false);
			return;
		}
		this.m_matRadialBlur.SetFloat(ShaderPIDManager._CenterX, this.m_fCenterX);
		this.m_matRadialBlur.SetFloat(ShaderPIDManager._CenterY, this.m_fCenterY);
		this.m_matRadialBlur.SetFloat(ShaderPIDManager._Strength, this.m_fStrength);
		Graphics.Blit(source, destination, this.m_matRadialBlur);
	}
}
