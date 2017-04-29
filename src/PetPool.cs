using System;
using XEngineActor;

public class PetPool : ActorPool<ActorPet>
{
	public static readonly PetPool Instance = new PetPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
