using System;
using UnityEngine;

public class GraduallyHidden : MonoBehaviour
{
	private Material m_material;

	private bool m_bHiding;

	private float m_fHideY = 100f;

	public void SetHidden(ref Material mat, bool bHide, float hideY = 100f)
	{
		this.m_bHiding = bHide;
		this.m_material = mat;
		this.m_fHideY = hideY;
		this.m_material.SetFloat(ShaderPIDManager._HideY, this.m_fHideY);
	}

	private void Update()
	{
		if (this.m_material == null)
		{
			return;
		}
		if (this.m_bHiding)
		{
			this.m_fHideY -= 0.01f;
			if (this.m_material.HasProperty(ShaderPIDManager._HideY))
			{
				this.m_material.SetFloat(ShaderPIDManager._HideY, this.m_fHideY);
			}
			if (this.m_fHideY <= 0f)
			{
				this.m_bHiding = false;
			}
		}
	}
}
