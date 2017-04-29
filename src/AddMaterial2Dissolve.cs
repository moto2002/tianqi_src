using System;
using System.Collections.Generic;
using UnityEngine;

public class AddMaterial2Dissolve : AddMaterialBase
{
	public Color TintColor;

	public Vector2 BaseTiling;

	public float RimBrightnessRatio = 1f;

	public float Amount = 0.5f;

	public float StartAmount = 0.1f;

	public float Illuminate = 0.5f;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void Update()
	{
		base.Update();
		if (this.m_dst_matmap.get_Count() == 0)
		{
			return;
		}
		this.InitValue();
		if (this.m_isInitValue)
		{
			using (Dictionary<int, Material[]>.ValueCollection.Enumerator enumerator = this.m_dst_matmap.get_Values().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Material[] current = enumerator.get_Current();
					if (current != null)
					{
						for (int i = 0; i < current.Length; i++)
						{
							if (i != 0)
							{
								Material material = current[i];
								material.SetColor(ShaderPIDManager._TintColor, this.TintColor);
								material.SetTextureScale("_MainTex", this.BaseTiling);
								material.SetFloat(ShaderPIDManager._RimBrightnessRatio, this.RimBrightnessRatio);
								material.SetFloat(ShaderPIDManager._Amount, this.Amount);
								material.SetFloat(ShaderPIDManager._StartAmount, this.StartAmount);
								material.SetFloat(ShaderPIDManager._Illuminate, this.Illuminate);
							}
						}
					}
				}
			}
		}
	}

	private void InitValue()
	{
		if (this.mat != null && !this.m_isInitValue)
		{
			this.m_isInitValue = true;
			this.TintColor = this.mat.GetColor("_TintColor");
			this.BaseTiling = this.mat.GetTextureScale("_MainTex");
			if (this.mat.HasProperty("_RimBrightnessRatio"))
			{
				this.RimBrightnessRatio = this.mat.GetFloat("_RimBrightnessRatio");
			}
			if (this.mat.HasProperty("_Amount"))
			{
				this.Amount = this.mat.GetFloat("_Amount");
			}
			if (this.mat.HasProperty("_StartAmount"))
			{
				this.StartAmount = this.mat.GetFloat("_StartAmount");
			}
			if (this.mat.HasProperty("_Illuminate"))
			{
				this.Illuminate = this.mat.GetFloat("_Illuminate");
			}
		}
	}
}
