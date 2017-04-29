using System;
using UnityEngine;

[Serializable]
public class AutoScrollUv2DirCfg
{
	public string texName = "_MainTex";

	public Vector2 uvInitOffset;

	public Vector2 Direction;

	public float speed;

	private Vector2 _dir;

	public Vector2 dir
	{
		get
		{
			return this._dir;
		}
		set
		{
			this._dir = value;
		}
	}
}
