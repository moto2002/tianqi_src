using System;
using XEngineActor;

public class AvatarPool : ActorPool<ActorSelf>
{
	public static readonly AvatarPool Instance = new AvatarPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}

	public override void Clear()
	{
	}
}
