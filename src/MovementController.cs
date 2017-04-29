using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public Mover mover;

	private void Awake()
	{
		if (this.mover == null)
		{
			this.mover = base.GetComponent<Mover>();
		}
	}

	protected virtual void OnEnable()
	{
		this.mover.ApplyPosition = false;
	}

	protected virtual void OnDisable()
	{
		this.mover.ApplyPosition = true;
	}
}
