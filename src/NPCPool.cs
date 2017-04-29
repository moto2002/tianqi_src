using System;

public class NPCPool : ActorPool<ActorNPC>
{
	public static readonly NPCPool Instance = new NPCPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
