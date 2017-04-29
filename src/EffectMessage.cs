using GameData;
using System;
using XEngineActor;

public class EffectMessage
{
	public int UID;

	public EntityParent caster;

	public Actor casterActor;

	public Skill skillData;

	public Effect effectData;

	public XPoint basePoint;

	public bool isClientHandle;

	public EffectMessage()
	{
	}

	public EffectMessage(EffectMessage copy)
	{
		this.caster = copy.caster;
		this.casterActor = copy.casterActor;
		this.skillData = copy.skillData;
		this.effectData = copy.effectData;
		this.basePoint = copy.basePoint;
		this.UID = copy.UID;
		this.isClientHandle = copy.isClientHandle;
	}
}
