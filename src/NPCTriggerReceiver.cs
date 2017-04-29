using System;
using UnityEngine;

public class NPCTriggerReceiver : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (ActorNPC.IsColliderActorSelf(other))
		{
			base.SendMessageUpwards("OnActSelfTriggerEnter", other, 1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (ActorNPC.IsColliderActorSelf(other))
		{
			base.SendMessageUpwards("OnActSelfTriggerExit", other, 1);
		}
	}

	private void OnTriggerStay(Collider other)
	{
	}
}
