using System;
using UnityEngine;

public class ActorHitEffect : ActorHitEffectBase
{
	public override void Init(Renderer hitRenderer)
	{
		this.SrcPower = 1f;
		this.DstPower = 6f;
		this.Rate = 15f;
		base.Init(hitRenderer);
	}
}
