using System;
using XEngineActor;

public class ModelPool : ActorPool<ActorModel>
{
	public static readonly ModelPool Instance = new ModelPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
