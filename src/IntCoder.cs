using System;
using UnityEngine;

public class IntCoder : ValueCoder<int>
{
	public IntCoder()
	{
		this.mask = Random.Range(0, 10000);
	}

	public override int Encode(int origin)
	{
		return origin + this.mask;
	}

	public override int Decode(int current)
	{
		return current - this.mask;
	}
}
