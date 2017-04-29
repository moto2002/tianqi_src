using System;
using UnityEngine;

public class VisibleCollider : BaseUnit
{
	private Collider c;

	public override void OnEnter()
	{
		this.c = base.GetComponent<Collider>();
		this.c.set_enabled(true);
	}

	public override void OnExit()
	{
		this.c = base.GetComponent<Collider>();
		this.c.set_enabled(false);
	}
}
