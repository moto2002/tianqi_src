using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial2Dissolve : ChangeMaterialBase
{
	private const float MaxTime = 1.5f;

	public float Amount;

	public float StartAmount = 0.1f;

	public Color DissColor = new Color(1f, 1f, 1f, 1f);

	public Vector4 ColorAnimate = new Vector4(1f, 1f, 1f, 1f);

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
							material.SetFloat(ShaderPIDManager._Amount, this.Amount);
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
			using (Dictionary<int, Material[]>.Enumerator enumerator = this.m_dst_matmap.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Material[]> current = enumerator.get_Current();
					Material[] value = current.get_Value();
					Material[] array = null;
					if (this.m_src_matmap.ContainsKey(current.get_Key()))
					{
						array = this.m_src_matmap.get_Item(current.get_Key());
					}
					if (value != null && array != null)
					{
						for (int i = 0; i < value.Length; i++)
						{
							Material material = value[i];
							if (i < array.Length)
							{
								material.SetTexture(ShaderPIDManager._MainTex, array[i].GetTexture(ShaderPIDManager._MainTex));
							}
							material.SetFloat(ShaderPIDManager._Amount, this.Amount);
							material.SetFloat(ShaderPIDManager._StartAmount, this.StartAmount);
							material.SetColor(ShaderPIDManager._DissColor, this.DissColor);
							material.SetVector(ShaderPIDManager._ColorAnimate, this.ColorAnimate);
						}
					}
				}
			}
		}
	}
}
