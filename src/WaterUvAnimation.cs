using System;
using UnityEngine;

public class WaterUvAnimation : MonoBehaviour
{
	public bool IsReverse;

	public float Speed = 1f;

	public int MaterialNomber;

	private Material mat;

	private float deltaFps;

	private bool isVisible;

	private bool isCorutineStarted;

	private float offset;

	private float delta;

	private void Awake()
	{
		this.mat = base.GetComponent<Renderer>().get_materials()[this.MaterialNomber];
	}

	private void Update()
	{
		if (this.IsReverse)
		{
			this.offset -= Time.get_deltaTime() * this.Speed;
			if (this.offset < 0f)
			{
				this.offset = 1f;
			}
		}
		else
		{
			this.offset += Time.get_deltaTime() * this.Speed;
			if (this.offset > 1f)
			{
				this.offset = 0f;
			}
		}
		Vector2 vector = new Vector2(0f, this.offset);
		this.mat.SetTextureOffset("_BumpMap", vector);
		this.mat.SetFloat(ShaderPIDManager._OffsetYHeightMap, this.offset);
	}
}
