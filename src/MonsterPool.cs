using System;
using XEngineActor;

public class MonsterPool : ActorPool<ActorMonster>
{
	public static readonly MonsterPool Instance = new MonsterPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
