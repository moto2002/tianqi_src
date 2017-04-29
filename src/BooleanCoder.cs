using System;
using UnityEngine;

public class BooleanCoder : ValueCoder<bool>
{
	public BooleanCoder()
	{
		this.mask = (Random.Range(0, 2) == 0);
	}

	public override bool Encode(bool origin)
	{
		return (!this.mask) ? (!origin) : origin;
	}

	public override bool Decode(bool current)
	{
		return (!this.mask) ? (!current) : current;
	}
}
