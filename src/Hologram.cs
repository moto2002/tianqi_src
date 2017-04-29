using System;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : PostProcessBase
{
	public float m_fClipVariance = 5f;

	public float m_fRimVariance = 0.5f;

	public float m_fClippower = 100f;

	public float m_fRimpower = 1f;

	private List<Material> HoloMaterials = new List<Material>();

	private Material m_matHologram;

	protected override void SetShaders()
	{
		if (!this.m_shaderNames.Contains("Hsh(Mobile)/Character/Hologram"))
		{
			this.m_shaderNames.Add("Hsh(Mobile)/Character/Hologram");
		}
	}

	protected override void GetMaterials()
	{
		this.m_matHologram = this.m_materials.get_Item(this.m_shaderNames.get_Item(0));
	}

	public void Initialization(Transform tran)
	{
		base.Initialization();
		if (this.IsInitSuccessed)
		{
			this.HoloMaterials.Clear();
			Renderer[] componentsInChildren = base.get_transform().GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Renderer renderer = componentsInChildren[i];
				this.m_matHologram.SetTexture(ShaderPIDManager._MainTex, renderer.get_material().GetTexture(ShaderPIDManager._MainTex));
				this.m_matHologram.SetFloat(ShaderPIDManager._RimPower, 1f);
				this.m_matHologram.SetFloat(ShaderPIDManager._DiffuseAmount, 0.2f);
				renderer.set_material(this.m_matHologram);
				this.HoloMaterials.Add(renderer.get_material());
			}
		}
	}

	private void Update()
	{
		float num = this.m_fClippower - this.m_fClipVariance / 2f + Random.get_value() * this.m_fClipVariance;
		float num2 = this.m_fRimpower - this.m_fRimVariance / 2f;
		num = Mathf.Max(0f, num);
		num2 = Mathf.Max(0f, num2);
		for (int i = 0; i < this.HoloMaterials.get_Count(); i++)
		{
			this.HoloMaterials.get_Item(i).SetFloat(ShaderPIDManager._RimPower, num2);
			this.HoloMaterials.get_Item(i).SetFloat(ShaderPIDManager._ClipPower, num);
		}
	}
}
