using System;
using UnityEngine;
using XEngineActor;
using XEngineCommand;

public class ActorWildMonster : ActorParentContainer<EntityCityMonster>
{
	protected NPCBehavior npcEvent;

	public void Init(int id, int modelID, NPCBehavior theNPCEvent)
	{
		this.npcEvent = theNPCEvent;
		base.get_gameObject().SetActive(true);
		base.get_gameObject().set_name(id.ToString());
		if (this.npcEvent != null)
		{
			this.npcEvent.Init(id, modelID, base.get_transform());
		}
	}

	public void ApplyDefaultState()
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.ApplyDefaultState();
		}
	}

	public void Release()
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.Release();
			this.npcEvent = null;
		}
		Object.Destroy(this);
	}

	protected override void Update()
	{
		if (this.GetEntity() == null)
		{
			return;
		}
		this.MoveProcess();
		if (base.NextRotateCountDown > 0f)
		{
			base.NextRotateCountDown -= Time.get_deltaTime();
		}
		else
		{
			base.UpdateSight();
		}
	}

	public override void ResetController()
	{
		AssetManager.AssetOfControllerManager.SetController(base.FixAnimator, this.GetEntity().FixModelID, true);
	}

	public override void OnNotifyPropChanged(NotifyPropChangedCmd cmd)
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnNotifyPropChanged(cmd);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (ActorWildMonster.IsColliderActorSelf(other))
		{
			this.OnActSelfTriggerEnter(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (ActorWildMonster.IsColliderActorSelf(other))
		{
			this.OnActSelfTriggerExit(other);
		}
	}

	private void OnTriggerStay(Collider other)
	{
	}

	protected void OnActSelfTriggerEnter(Collider other)
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnEnter();
		}
	}

	protected void OnActSelfTriggerExit(Collider other)
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnExit();
		}
	}

	public static bool IsColliderActorSelf(Collider collider)
	{
		ActorSelf component = collider.GetComponent<ActorSelf>();
		return component != null;
	}

	public int GetState()
	{
		return (this.npcEvent != null) ? this.npcEvent.GetState() : 0;
	}

	public void UpdateState(object state)
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.UpdateState(state);
		}
	}

	public override void UpdateLayer()
	{
	}
}
