using System;
using UnityEngine;

public class LongCoder : ValueCoder<long>
{
	public LongCoder()
	{
		this.mask = (long)Random.Range(0, 10000);
	}

	public override long Encode(long origin)
	{
		return origin + this.mask;
	}

	public override long Decode(long current)
	{
		return current - this.mask;
	}
}
