using System;

public class UnderDamageEffectAnnouncer
{
	protected static ConditionType type = ConditionType.UnderDamageEffect;

	public static void Announce(EntityParent announcer, EntityParent caster, int effectID)
	{
		UnderDamageEffectConditionMessage underDamageEffectConditionMessage = new UnderDamageEffectConditionMessage();
		underDamageEffectConditionMessage.type = UnderDamageEffectAnnouncer.type;
		underDamageEffectConditionMessage.announcer = announcer;
		underDamageEffectConditionMessage.caster = caster;
		underDamageEffectConditionMessage.effectID = effectID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, underDamageEffectConditionMessage);
	}
}
