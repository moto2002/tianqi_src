using System;
using UnityEngine;

public class FloatCoder : ValueCoder<float>
{
	public FloatCoder()
	{
		this.mask = (float)((int)Math.Round((double)(Random.Range(0f, 7f) * 100f))) / 100f;
	}

	public override float Encode(float origin)
	{
		return origin + this.mask;
	}

	public override float Decode(float current)
	{
		return current - this.mask;
	}
}
