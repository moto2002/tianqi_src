using System;
using XEngineActor;

public class CityPlayerPool : ActorPool<ActorCityPlayer>
{
	public static readonly CityPlayerPool Instance = new CityPlayerPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
