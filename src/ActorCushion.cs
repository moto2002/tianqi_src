using System;
using UnityEngine;
using XEngineActor;

public class ActorCushion : MonoBehaviour
{
	public ActorParent Actor;

	public void SetCushion(ActorParent theActor, CharacterController controller)
	{
		this.Actor = theActor;
		BoxCollider boxCollider = base.get_gameObject().AddMissingComponent<BoxCollider>();
		boxCollider.set_isTrigger(true);
		boxCollider.set_center(new Vector3(0f, controller.get_center().y, (controller.get_radius() + 0.5f) / 2f));
		boxCollider.set_size(new Vector3(controller.get_radius() * 2f, controller.get_height(), controller.get_radius() + 0.5f));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Default") && other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Terrian"))
		{
			ActorParent component = other.get_transform().GetComponent<ActorParent>();
			if (component && this.Actor && component != this.Actor && component.FixGameObject.get_layer() != this.Actor.FixGameObject.get_layer())
			{
				this.Actor.OnCushionEnter(component);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Default") && other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Terrian"))
		{
			ActorParent component = other.get_transform().GetComponent<ActorParent>();
			if (component && this.Actor && component != this.Actor && component.FixGameObject.get_layer() != this.Actor.FixGameObject.get_layer() && XUtility.DistanceNoY(other.get_transform().get_position(), base.get_transform().get_position()) < XUtility.GetTriggerRadius(other.get_transform()) + XUtility.GetTriggerRadius(base.get_transform()))
			{
				this.Actor.OnCushionStay(component);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Default") && other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Terrian"))
		{
			ActorParent component = other.get_transform().GetComponent<ActorParent>();
			if (component && this.Actor && component != this.Actor && component.FixGameObject.get_layer() != this.Actor.FixGameObject.get_layer())
			{
				this.Actor.OnCushionExit(component);
			}
		}
	}
}
