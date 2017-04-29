using System;
using UnityEngine;
using XEngineActor;

public class FXFollow : MonoBehaviour
{
	public Transform targetFollow;

	public float speed;

	public float lessDistance;

	public float offset;

	private void Update()
	{
		if (this.targetFollow == null)
		{
			this.FXFinished();
			return;
		}
		if (this == null || base.get_transform() == null)
		{
			this.FXFinished();
			return;
		}
		if (Vector3.Distance(base.get_transform().get_position(), this.targetFollow.get_position() + new Vector3(0f, this.offset, 0f)) <= this.lessDistance)
		{
			this.FXFinished();
			return;
		}
		base.get_transform().set_position(Vector3.MoveTowards(base.get_transform().get_position(), this.targetFollow.get_position() + new Vector3(0f, this.offset, 0f), this.speed * Time.get_deltaTime()));
	}

	private void OnDisable()
	{
		base.set_enabled(false);
	}

	public void FXFinished()
	{
		ActorFX component = base.get_transform().GetComponent<ActorFX>();
		if (component != null)
		{
			component.FXFinished();
		}
	}
}
