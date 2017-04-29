using System;
using UnityEngine;

public class ShaderPropertySetting2 : MonoBehaviour
{
	public int _materialIndex;

	private Material _material;

	public bool On_Amount;

	public bool On_StartAmount;

	public bool On_Illuminate;

	public bool On_Tile;

	public bool On_ColorAnimate;

	public bool On_Transparency;

	public bool On_TintColor;

	public float _Amount;

	public float _StartAmount;

	public float _Illuminate;

	public float _Tile;

	public Color _ColorAnimate;

	public float _Transparency;

	public Color _TintColor;

	private void Awake()
	{
		Renderer component = base.get_gameObject().GetComponent<Renderer>();
		if (component != null && component.get_materials() != null && this._materialIndex < component.get_materials().Length)
		{
			this._material = component.get_materials()[this._materialIndex];
			if (this.On_Amount && this._material.HasProperty(ShaderPIDManager._Amount))
			{
				this._Amount = this._material.GetFloat(ShaderPIDManager._Amount);
			}
			if (this.On_StartAmount && this._material.HasProperty(ShaderPIDManager._StartAmount))
			{
				this._StartAmount = this._material.GetFloat(ShaderPIDManager._StartAmount);
			}
			if (this.On_Illuminate && this._material.HasProperty(ShaderPIDManager._Illuminate))
			{
				this._Illuminate = this._material.GetFloat(ShaderPIDManager._Illuminate);
			}
			if (this.On_Tile && this._material.HasProperty(ShaderPIDManager._Tile))
			{
				this._Tile = this._material.GetFloat(ShaderPIDManager._Tile);
			}
			if (this.On_ColorAnimate && this._material.HasProperty(ShaderPIDManager._ColorAnimate))
			{
				this._ColorAnimate = this._material.GetColor(ShaderPIDManager._ColorAnimate);
			}
			if (this.On_Transparency && this._material.HasProperty(ShaderPIDManager._Transparency))
			{
				this._Transparency = this._material.GetFloat(ShaderPIDManager._Transparency);
			}
			if (this.On_TintColor && this._material.HasProperty(ShaderPIDManager._TintColor))
			{
				this._TintColor = this._material.GetColor(ShaderPIDManager._TintColor);
			}
			return;
		}
	}

	private void Update()
	{
		if (this._material == null)
		{
			return;
		}
		if (this.On_Amount && this._material.HasProperty(ShaderPIDManager._Amount))
		{
			this._material.SetFloat(ShaderPIDManager._Amount, this._Amount);
		}
		if (this.On_StartAmount && this._material.HasProperty(ShaderPIDManager._StartAmount))
		{
			this._material.SetFloat(ShaderPIDManager._StartAmount, this._StartAmount);
		}
		if (this.On_Illuminate && this._material.HasProperty(ShaderPIDManager._Illuminate))
		{
			this._material.SetFloat(ShaderPIDManager._Illuminate, this._Illuminate);
		}
		if (this.On_Tile && this._material.HasProperty(ShaderPIDManager._Tile))
		{
			this._material.SetFloat(ShaderPIDManager._Tile, this._Tile);
		}
		if (this.On_ColorAnimate && this._material.HasProperty(ShaderPIDManager._ColorAnimate))
		{
			this._material.SetColor(ShaderPIDManager._ColorAnimate, this._ColorAnimate);
		}
		if (this.On_Transparency && this._material.HasProperty(ShaderPIDManager._Transparency))
		{
			this._material.SetFloat(ShaderPIDManager._Transparency, this._Transparency);
		}
		if (this.On_TintColor && this._material.HasProperty(ShaderPIDManager._TintColor))
		{
			this._material.SetColor(ShaderPIDManager._TintColor, this._TintColor);
		}
	}
}
