using System;

public class SummonPetAnnouncer
{
	protected static ConditionType type = ConditionType.SummonPet;

	public static void Announce(EntityParent announcer, int petType)
	{
		SummonPetConditionMessage summonPetConditionMessage = new SummonPetConditionMessage();
		summonPetConditionMessage.type = SummonPetAnnouncer.type;
		summonPetConditionMessage.announcer = announcer;
		summonPetConditionMessage.petType = petType;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, summonPetConditionMessage);
	}
}
