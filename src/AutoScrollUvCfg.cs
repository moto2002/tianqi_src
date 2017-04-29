using System;
using UnityEngine;

[Serializable]
public class AutoScrollUvCfg
{
	public string texName = "_MainTex";

	public Vector2 uvOffset;

	private Vector2 _uvPos;

	public Vector2 uvPos
	{
		get
		{
			return this._uvPos;
		}
		set
		{
			this._uvPos = value;
		}
	}
}
