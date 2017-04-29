using System;
using UnityEngine;

public class ScrollUv : MonoBehaviour
{
	public UVScrollCfg[] _uvScrolls;

	public Material _dstMat;

	private Vector2[] _uvPos;

	private void Awake()
	{
		if (this._dstMat == null)
		{
			this._dstMat = base.GetComponent<Renderer>().get_material();
		}
		this._uvPos = new Vector2[this._uvScrolls.Length];
	}

	private void Update()
	{
		for (int i = 0; i < this._uvScrolls.Length; i++)
		{
			this._uvPos[i] += this._uvScrolls[i].uvOffset * Time.get_smoothDeltaTime();
			if (this._dstMat.HasProperty(this._uvScrolls[i].texName))
			{
				this._dstMat.SetTextureOffset(this._uvScrolls[i].texName, this._uvPos[i]);
			}
		}
	}
}
