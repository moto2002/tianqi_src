using System;
using UnityEngine;
using XEngineActor;

public class ActorCollider : MonoBehaviour
{
	public ActorParent Actor;

	public BoxCollider detectCollisionTrigger;

	public void SetCollider(ActorParent theActor, CharacterController controller, float height)
	{
		this.Actor = theActor;
		this.detectCollisionTrigger = base.get_gameObject().AddMissingComponent<BoxCollider>();
		this.detectCollisionTrigger.set_isTrigger(false);
		this.detectCollisionTrigger.set_center(new Vector3(0f, height * 0.5f, 0f));
		this.detectCollisionTrigger.set_size(new Vector3(controller.get_radius() * Mathf.Sqrt(2f), height, controller.get_radius() * Mathf.Sqrt(2f)));
	}

	private void OnEnable()
	{
		if (this.detectCollisionTrigger)
		{
			this.detectCollisionTrigger.set_enabled(true);
		}
	}

	private void OnDisable()
	{
		if (this.detectCollisionTrigger)
		{
			this.detectCollisionTrigger.set_enabled(false);
		}
	}
}
