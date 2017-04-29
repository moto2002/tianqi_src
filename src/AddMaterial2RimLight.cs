using System;
using System.Collections.Generic;
using UnityEngine;

public class AddMaterial2RimLight : AddMaterialBase
{
	public Color TintColor;

	public Vector2 BaseTiling;

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
		}
	}
}
