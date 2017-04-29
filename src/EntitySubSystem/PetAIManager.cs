using GameData;
using System;

namespace EntitySubSystem
{
	public class PetAIManager : AIManager
	{
		protected override void GetAIDataByType()
		{
			if (string.IsNullOrEmpty(this.AIType) || this.ThinkInterval == 0)
			{
				Pet pet = DataReader<Pet>.Get(this.owner.TypeID);
				this.AIType = pet.aiId;
				this.ThinkInterval = pet.interval;
			}
		}
	}
}
