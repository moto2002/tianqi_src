using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XEngineActor;

public class Organ : BaseOrgan
{
	private List<ActorParent> passengers = new List<ActorParent>();

	private Collider[] triggers;

	private void OnEnable()
	{
		this.triggers = Enumerable.ToArray<Collider>(Enumerable.Where<Collider>(base.GetComponentsInChildren<Collider>(), (Collider collider) => collider.get_isTrigger()));
	}

	public override void EnterPlatform(Collider other)
	{
		ActorParent componentInChildren = other.get_transform().get_root().GetComponentInChildren<ActorParent>();
		if (componentInChildren == null || this.passengers.Contains(componentInChildren))
		{
			return;
		}
		Debuger.Error("add", new object[0]);
		this.passengers.Add(componentInChildren);
		componentInChildren.EnterPlatformArea();
		base.AddStartPlatformEvent(new Action(componentInChildren.StartPlatformTrip));
		base.AddEndPlatformEvent(new Action(componentInChildren.FinishPlatformTrip));
	}

	public override void LeavePlatform(Collider other)
	{
		ActorParent componentInChildren = other.get_transform().get_root().GetComponentInChildren<ActorParent>();
		if (componentInChildren != null && this.passengers.Contains(componentInChildren))
		{
			this.passengers.Remove(componentInChildren);
			componentInChildren.ExitPlatformArea();
			base.RemoveStartPlatformEvent(new Action(componentInChildren.StartPlatformTrip));
			base.RemoveEndPlatformEvent(new Action(componentInChildren.FinishPlatformTrip));
		}
	}

	public override void StayPlatform(Collider other)
	{
	}

	private bool IntersectingTrigger(Collider other)
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			if (this.triggers[i].get_bounds().Intersects(other.get_bounds()))
			{
				return true;
			}
		}
		return false;
	}

	private bool ContainsPoint(Vector3 point)
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			if (this.triggers[i].get_bounds().Contains(point))
			{
				return true;
			}
		}
		return false;
	}

	public override void UpdateActor(Vector3 delta, bool isEqual)
	{
		if (delta != Vector3.get_zero())
		{
			for (int i = 0; i < this.passengers.get_Count(); i++)
			{
				ActorParent actorParent = this.passengers.get_Item(i);
				Collider[] componentsInChildren = actorParent.get_transform().get_root().GetComponentsInChildren<Collider>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (this.ContainsPoint(actorParent.get_transform().get_position()) || (Vector3.Dot(actorParent.get_transform().get_position() - base.get_transform().get_position(), base.get_transform().get_up()) > 0f && this.IntersectingTrigger(componentsInChildren[j])))
					{
						actorParent.UpdatePlatform(delta, false);
					}
				}
			}
		}
	}
}
