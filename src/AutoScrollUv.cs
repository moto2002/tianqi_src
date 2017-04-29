using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollUv : MonoBehaviour
{
	public AutoScrollUvCfg[] _uvScrolls;

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
			this._uvScrolls[i].uvPos += this._uvScrolls[i].uvOffset * Time.get_smoothDeltaTime();
			this._selfMat.SetTextureOffset(this._uvScrolls[i].texName, this._uvScrolls[i].uvPos);
			if (this._uvScrolls[i].uvPos.x >= 1f)
			{
				this._uvScrolls[i].uvPos = new Vector2(0f, this._uvScrolls[i].uvPos.y);
			}
			if (this._uvScrolls[i].uvPos.y >= 1f)
			{
				this._uvScrolls[i].uvPos = new Vector2(this._uvScrolls[i].uvPos.x, 0f);
			}
		}
	}
}
