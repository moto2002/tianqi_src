using System;
using UnityEngine;
using XEngineActor;
using XEngineCommand;

public class ActorNPC : Actor, IActorVisible
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

	protected override void OnDestroy()
	{
		ActorVisibleManager.Instance.Remove(base.get_transform());
		Object.Destroy(base.get_gameObject());
	}

	private void Update()
	{
		if (this.npcEvent != null && this.npcEvent.EnableUpdate)
		{
			this.npcEvent.Update();
		}
	}

	public override void OnAnimationEnd(AnimationEndCmd cmd)
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnAnimationEnd(cmd);
		}
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
		if (ActorNPC.IsColliderActorSelf(other))
		{
			this.OnActSelfTriggerEnter(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (ActorNPC.IsColliderActorSelf(other))
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

	public void OnSeleted()
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnSeleted();
		}
	}

	public void OnAnimatorBecameVisiable()
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.OnAnimatorBecameVisiable();
		}
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

	public void UpdateHeadInfoState()
	{
		if (this.npcEvent != null)
		{
			this.npcEvent.UpdateHeadInfoState();
		}
	}
}
