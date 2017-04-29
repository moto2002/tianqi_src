using System;
using UnityEngine;
using XEngineCommand;

public class FXEventReceiver : MonoBehaviour
{
	private void OnActionStart(AnimationEvent e)
	{
		base.SendMessageUpwards("OnAllActionStart", e, 1);
	}

	private void OnActionEnd(AnimationEvent e)
	{
		base.SendMessageUpwards("OnAllActionEnd", e, 1);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<ActorCollider>())
		{
			base.SendMessageUpwards("OnAllTriggerEnter", other.GetComponent<ActorCollider>(), 1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<ActorCollider>())
		{
			base.SendMessageUpwards("OnAllTriggerExit", other.GetComponent<ActorCollider>(), 1);
		}
	}

	private void OnImmune(AnimationEvent e)
	{
		base.SendMessageUpwards("OnAllImmune", e, 1);
	}

	private void OnAudio(AnimationEvent e)
	{
		base.SendMessageUpwards("OnAudioEvent", new AudioEventCmd
		{
			args = e
		}, 1);
	}
}
