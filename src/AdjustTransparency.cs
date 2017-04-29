using System;
using UnityEngine;

public class AdjustTransparency : MonoBehaviour
{
	private Renderer m_renderer;

	private Material[] _rendererMaterials;

	private ShaderManager.ShaderType m_shaderType;

	private bool m_isSettingAction;

	private bool m_bNeedAdjust;

	private bool m_bAdd;

	public bool HideOutline;

	public float m_fDstTransparentcy = 1f;

	public float m_fSrcTransparentcy = 1f;

	public bool InFade;

	public Action actionFadeFinish;

	private Material matFirst;

	private Color outlineColor = Color.get_white();

	private Material[] rendererMaterials
	{
		get
		{
			if (this.m_renderer == null)
			{
				return null;
			}
			if (this._rendererMaterials == null)
			{
				this._rendererMaterials = this.m_renderer.get_materials();
				if (this._rendererMaterials == null || this._rendererMaterials.Length == 0)
				{
					base.set_enabled(false);
					return null;
				}
				this.SetShaderType();
				this.SetSettingAction();
			}
			return this._rendererMaterials;
		}
	}

	private void OnEnable()
	{
		this.m_fSrcTransparentcy = 1f;
		this.SetTransparencyValue();
	}

	private void OnDestroy()
	{
		this.m_fDstTransparentcy = 1f;
		this.ResetAll();
	}

	private void ResetAll()
	{
		this.m_fSrcTransparentcy = 1f;
		this.m_bNeedAdjust = false;
		this.SetTransparencyValue();
	}

	public void Init(Renderer renderer)
	{
		this.m_renderer = renderer;
	}

	public bool IsSettingAction()
	{
		return base.get_enabled() && this.rendererMaterials != null && this.rendererMaterials.Length != 0 && !(this.rendererMaterials[0] == null) && this.m_isSettingAction;
	}

	public void SetIsNearCamera(bool isHide)
	{
		if (this.InFade)
		{
			return;
		}
		this.SwitchShader(true);
		this.m_fDstTransparentcy = ((!isHide) ? 1f : 0.25f);
		this.SetTransparency(isHide);
	}

	public void SetFade(bool isHide)
	{
		this.SwitchShader(true);
		this.m_fDstTransparentcy = ((!isHide) ? 1f : 0f);
		this.SetTransparency(isHide);
	}

	public void SetFadeRightNow(bool isHide)
	{
		this.m_fSrcTransparentcy = ((!isHide) ? 1f : 0f);
		this.m_fDstTransparentcy = ((!isHide) ? 1f : 0f);
		this.m_bNeedAdjust = false;
		this.SetTransparencyValue();
	}

	private void Update()
	{
		if (this.rendererMaterials == null)
		{
			return;
		}
		if (this.m_bNeedAdjust)
		{
			if (this.m_bAdd)
			{
				this.m_fSrcTransparentcy += Time.get_deltaTime() * this.GetTimeFactor();
				if (this.m_fSrcTransparentcy >= this.m_fDstTransparentcy)
				{
					this.m_fSrcTransparentcy = this.m_fDstTransparentcy;
					this.m_bNeedAdjust = false;
					this.FadeFinished();
				}
			}
			else
			{
				this.m_fSrcTransparentcy = Mathf.Clamp01(this.m_fSrcTransparentcy - Time.get_deltaTime() * this.GetTimeFactor());
				if (this.m_fSrcTransparentcy <= this.m_fDstTransparentcy)
				{
					this.m_fSrcTransparentcy = this.m_fDstTransparentcy;
					this.m_bNeedAdjust = false;
					this.FadeFinished();
				}
			}
			this.SetTransparencyValue();
		}
	}

	private float GetTimeFactor()
	{
		if (!this.InFade)
		{
			return 2.27f;
		}
		return 1f;
	}

	private void SetTransparency(bool bHideStatus)
	{
		this.m_bNeedAdjust = false;
		this.matFirst = this.GetMaterialWithProperty(ShaderPIDManager._Transparency);
		if (this.matFirst == null)
		{
			this.FadeFinished();
			return;
		}
		for (int i = 0; i < this.rendererMaterials.Length; i++)
		{
			this.SetOutline(this.rendererMaterials[i], bHideStatus);
		}
		this.m_fSrcTransparentcy = this.matFirst.GetFloat(ShaderPIDManager._Transparency);
		if (this.m_fDstTransparentcy > this.m_fSrcTransparentcy)
		{
			this.m_bAdd = true;
			this.m_bNeedAdjust = true;
		}
		else if (this.m_fDstTransparentcy < this.m_fSrcTransparentcy)
		{
			this.m_bAdd = false;
			this.m_bNeedAdjust = true;
		}
		else
		{
			this.FadeFinished();
		}
	}

	private Material GetMaterialWithProperty(int property)
	{
		if (this.rendererMaterials != null)
		{
			for (int i = 0; i < this.rendererMaterials.Length; i++)
			{
				if (this.rendererMaterials[i] != null && this.rendererMaterials[i].HasProperty(property))
				{
					return this.rendererMaterials[i];
				}
			}
		}
		return null;
	}

	private void SetOutline(Material mat, bool bHide)
	{
		this.HideOutline = bHide;
		if (!GameLevelManager.IsLuminousOutlineOn())
		{
			return;
		}
		if (!bHide && !ShaderEffectUtils.OutlineIncludeNormal)
		{
			return;
		}
		if (mat != null && mat.HasProperty(ShaderPIDManager._OutlineColor))
		{
			this.outlineColor = mat.GetColor(ShaderPIDManager._OutlineColor);
			this.outlineColor.a = (float)((!bHide) ? 1 : 0);
			mat.SetColor(ShaderPIDManager._OutlineColor, this.outlineColor);
		}
	}

	private void SetTransparencyValue()
	{
		if (this.rendererMaterials != null)
		{
			for (int i = 0; i < this.rendererMaterials.Length; i++)
			{
				if (this.rendererMaterials[i] != null && this.rendererMaterials[i].HasProperty(ShaderPIDManager._Transparency))
				{
					this.rendererMaterials[i].SetFloat(ShaderPIDManager._Transparency, this.m_fSrcTransparentcy);
				}
			}
		}
	}

	private void FadeFinished()
	{
		this.SwitchShader(false);
		this.InFade = false;
		if (this.actionFadeFinish != null)
		{
			this.actionFadeFinish.Invoke();
			this.actionFadeFinish = null;
		}
	}

	private void SetShaderType()
	{
		if (this.rendererMaterials == null && this.rendererMaterials.Length <= 0)
		{
			return;
		}
		if (this.rendererMaterials.Length == 1 && this.rendererMaterials[0] != null && this.rendererMaterials[0].get_shader() != null)
		{
			this.m_shaderType = ShaderManager.GetShaderType(this.rendererMaterials[0].get_shader().get_name());
			return;
		}
		this.m_shaderType = ShaderManager.ShaderType.None;
	}

	private void SetSettingAction()
	{
		if (this.rendererMaterials == null && this.rendererMaterials.Length <= 0)
		{
			return;
		}
		if (this.rendererMaterials[0] != null && this.rendererMaterials[0].get_shader() != null)
		{
			this.m_isSettingAction = this.rendererMaterials[0].HasProperty(ShaderPIDManager._Transparency);
		}
	}

	private void SwitchShader(bool transparent)
	{
		if (!transparent)
		{
			this.ResetAll();
		}
	}
}
