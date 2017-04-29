using System;

public class CityPetPool : ActorPool<ActorCityPet>
{
	public static readonly CityPetPool Instance = new CityPetPool();

	protected override void SetPipeline()
	{
		this.pipeline = new ModelPipeline();
	}
}
