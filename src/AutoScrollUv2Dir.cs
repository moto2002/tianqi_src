using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollUv2Dir : MonoBehaviour
{
	public AutoScrollUv2DirCfg[] _uvScrolls;

	private Material _selfMat;

	private void Awake()
	{
		Image component = base.GetComponent<Image>();
		if (component != null)
		{
			this._selfMat = component.get_material();
		}
		else
		{
			RawImage component2 = base.GetComponent<RawImage>();
			if (component2 != null)
			{
				this._selfMat = component2.get_material();
			}
		}
	}

	private void Update()
	{
		if (this._selfMat == null)
		{
			return;
		}
		for (int i = 0; i < this._uvScrolls.Length; i++)
		{
			this._selfMat.SetTextureOffset(this._uvScrolls[i].texName, this._uvScrolls[i].dir += new Vector2(Time.get_deltaTime() * this._uvScrolls[i].speed * this._uvScrolls[i].Direction.x, Time.get_deltaTime() * this._uvScrolls[i].speed * this._uvScrolls[i].Direction.y));
			if (this._uvScrolls[i].dir.x > 2f)
			{
				this._uvScrolls[i].dir = Vector2.get_zero();
			}
		}
	}
}
