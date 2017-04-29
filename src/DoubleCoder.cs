using System;
using UnityEngine;

public class DoubleCoder : ValueCoder<double>
{
	public DoubleCoder()
	{
		this.mask = (double)((float)((int)Math.Round((double)(Random.Range(0f, 7f) * 100f))) / 100f);
	}

	public override double Encode(double origin)
	{
		return origin + this.mask;
	}

	public override double Decode(double current)
	{
		return current - this.mask;
	}
}
