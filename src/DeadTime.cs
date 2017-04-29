using System;
using UnityEngine;

public class DeadTime : MonoBehaviour
{
	public float deadTime = 1.5f;

	public bool destroyRoot;

	private void Awake()
	{
		Object.Destroy(this.destroyRoot ? base.get_transform().get_root().get_gameObject() : base.get_gameObject(), this.deadTime);
	}
}
