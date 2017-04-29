using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial2Freeze : ChangeMaterialBase
{
	public Vector2 BaseTiling;

	public Vector2 Offset;

	public Color TintColor;

	public Color RimColor = Color.get_white();

	public float RimPower = 1f;

	public float RimBrightnessRatio = 1f;

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
							Material material = current[i];
							material.SetTextureScale("_MainTex", this.BaseTiling);
							material.SetTextureOffset("_MainTex", this.Offset);
							material.SetColor(ShaderPIDManager._TintColor, this.TintColor);
							material.SetColor(ShaderPIDManager._RimColor, this.RimColor);
							material.SetFloat(ShaderPIDManager._RimPower, this.RimPower);
							material.SetFloat(ShaderPIDManager._RimBrightnessRatio, this.RimBrightnessRatio);
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
			this.BaseTiling = this.mat.GetTextureScale("_MainTex");
			this.Offset = this.mat.GetTextureOffset("_MainTex");
			this.TintColor = this.mat.GetColor("_TintColor");
			if (this.mat.HasProperty("_RimColor"))
			{
				this.RimColor = this.mat.GetColor("_RimColor");
			}
			if (this.mat.HasProperty("_RimPower"))
			{
				this.RimPower = this.mat.GetFloat("_RimPower");
			}
			if (this.mat.HasProperty("_RimBrightnessRatio"))
			{
				this.RimBrightnessRatio = this.mat.GetFloat("_RimBrightnessRatio");
			}
		}
	}
}
