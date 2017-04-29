using System;

public class CityMonsterPool : ActorPool<ActorWildMonster>
{
	public static readonly CityMonsterPool Instance = new CityMonsterPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
