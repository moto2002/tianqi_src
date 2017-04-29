using System;

public class CauseDamageEffectAnnouncer
{
	protected static ConditionType type = ConditionType.CauseDamageEffect;

	public static void Announce(EntityParent announcer, EntityParent target, int effectID)
	{
		CauseDamageEffectMessage causeDamageEffectMessage = new CauseDamageEffectMessage();
		causeDamageEffectMessage.type = CauseDamageEffectAnnouncer.type;
		causeDamageEffectMessage.announcer = announcer;
		causeDamageEffectMessage.target = target;
		causeDamageEffectMessage.effectID = effectID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, causeDamageEffectMessage);
	}
}
