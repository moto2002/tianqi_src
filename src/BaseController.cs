using System;
using UnityEngine;
using XEngineActor;

public class BaseController : MonoBehaviour
{
	private ActorParent actor;

	protected ActorParent Actor
	{
		get
		{
			if (this.actor == null)
			{
				this.actor = base.get_transform().GetComponent<ActorParent>();
				if (this.actor == null)
				{
					Debuger.Error("获取不到Controller的Actor", new object[0]);
				}
			}
			return this.actor;
		}
	}

	protected virtual void Awake()
	{
		BaseController[] components = base.get_transform().GetComponents<BaseController>();
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i] != this)
			{
				components[i].Destroy();
			}
		}
	}
}
