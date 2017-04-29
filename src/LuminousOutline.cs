using System;
using UnityEngine;

public class LuminousOutline : MonoBehaviour
{
	public enum Status
	{
		Normal,
		Selected,
		Taunt
	}

	private const float DoublePI = 6.28318548f;

	private Transform m_myTransform;

	private float m_fFlashingFreq = 1.5f;

	private Color m_colFlashingMin = new Color(1f, 0f, 0f, 0.2f);

	private Color m_colFlashingMax = new Color(1f, 0f, 0f, 0.9f);

	private Color m_colTauntMin = new Color(0f, 1f, 0f, 0.2f);

	private Color m_colTauntMax = new Color(0f, 1f, 0f, 0.9f);

	private Color m_colConstantOn = new Color(0f, 0f, 0f, 1f);

	private Color m_colConstantOff = new Color(0f, 0f, 0f, 0f);

	private Material m_material;

	private LuminousOutline.Status m_status;

	private AdjustTransparency m_adjustTransparency;

	public void SetStatus(ref Material mat, LuminousOutline.Status status)
	{
		this.m_myTransform = base.get_transform();
		this.m_material = mat;
		this.m_status = status;
		if (status == LuminousOutline.Status.Normal)
		{
			base.set_enabled(false);
		}
		else
		{
			base.set_enabled(true);
		}
		this.SetOutlineColor();
	}

	private void SetOutlineColor()
	{
		if (this.m_material != null)
		{
			switch (this.m_status)
			{
			case LuminousOutline.Status.Normal:
				if (ShaderEffectUtils.OutlineIncludeNormal)
				{
					this.m_material.SetColor(ShaderPIDManager._OutlineColor, this.m_colConstantOn);
					this.m_material.SetFloat(ShaderPIDManager._Outline, 0.005f);
				}
				else
				{
					this.m_material.SetColor(ShaderPIDManager._OutlineColor, this.m_colConstantOff);
					this.m_material.SetFloat(ShaderPIDManager._Outline, 0f);
				}
				break;
			case LuminousOutline.Status.Selected:
			{
				Color color = Color.Lerp(this.m_colFlashingMin, this.m_colFlashingMax, 0.5f * Mathf.Sin(Time.get_realtimeSinceStartup() * this.m_fFlashingFreq * 6.28318548f) + 0.5f);
				this.CheckHideOutline(ref color);
				this.m_material.SetColor(ShaderPIDManager._OutlineColor, color);
				this.m_material.SetFloat(ShaderPIDManager._Outline, 0.03f);
				break;
			}
			case LuminousOutline.Status.Taunt:
			{
				Color color2 = Color.Lerp(this.m_colTauntMin, this.m_colTauntMax, 0.5f * Mathf.Sin(Time.get_realtimeSinceStartup() * this.m_fFlashingFreq * 6.28318548f) + 0.5f);
				this.CheckHideOutline(ref color2);
				this.m_material.SetColor(ShaderPIDManager._OutlineColor, color2);
				this.m_material.SetFloat(ShaderPIDManager._Outline, 0.03f);
				break;
			}
			}
		}
	}

	private void CheckHideOutline(ref Color c)
	{
		if (this.m_myTransform != null)
		{
			if (this.m_adjustTransparency == null)
			{
				this.m_adjustTransparency = this.m_myTransform.GetComponent<AdjustTransparency>();
			}
			if (this.m_adjustTransparency != null && this.m_adjustTransparency.HideOutline)
			{
				c = this.m_colConstantOff;
			}
		}
	}

	private void Update()
	{
		this.SetOutlineColor();
	}
}
