using System;
using XEngineActor;

public class PlayerPool : ActorPool<ActorPlayer>
{
	public static readonly PlayerPool Instance = new PlayerPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
