using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
	private const float kExitReCheckDelay = 0.3f;

	private List<PlatformRider> passengers = new List<PlatformRider>();

	private Vector3 lastPosition;

	private Collider[] triggers;

	private void OnEnable()
	{
		this.lastPosition = base.get_transform().get_position();
		this.triggers = Enumerable.ToArray<Collider>(Enumerable.Where<Collider>(base.GetComponentsInChildren<Collider>(), (Collider collider) => collider.get_isTrigger()));
	}

	public void BoardPlatform(PlatformRider passenger)
	{
		this.passengers.Add(passenger);
	}

	public void LeavePlatform(PlatformRider passenger)
	{
		this.passengers.Remove(passenger);
	}

	private void OnTriggerStay(Collider other)
	{
		PlatformRider componentInChildren = other.get_transform().get_root().GetComponentInChildren<PlatformRider>();
		if (componentInChildren == null || this.passengers.Contains(componentInChildren))
		{
			return;
		}
		if (componentInChildren.BoardPlatform(this))
		{
			this.BoardPlatform(componentInChildren);
		}
	}

	private bool IntersectingTrigger(Collider other)
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			Collider collider = this.triggers[i];
			if (collider.get_bounds().Intersects(other.get_bounds()))
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
			Collider collider = this.triggers[i];
			if (collider.get_bounds().Contains(point))
			{
				return true;
			}
		}
		return false;
	}

	private void LateUpdate()
	{
		Vector3 vector = base.get_transform().get_position() - this.lastPosition;
		if (vector != Vector3.get_zero())
		{
			int i = 0;
			while (i < this.passengers.get_Count())
			{
				PlatformRider platformRider = this.passengers.get_Item(i);
				bool flag = false;
				Collider[] componentsInChildren = platformRider.get_transform().get_root().GetComponentsInChildren<Collider>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					Collider other = componentsInChildren[j];
					if (this.ContainsPoint(platformRider.get_transform().get_position()) || (Vector3.Dot(platformRider.get_transform().get_position() - base.get_transform().get_position(), base.get_transform().get_up()) > 0f && this.IntersectingTrigger(other)))
					{
						platformRider.UpdatePlatform(vector);
						flag = true;
						break;
					}
				}
				if (!flag && platformRider.LeavePlatform(this))
				{
					this.LeavePlatform(platformRider);
				}
				else
				{
					i++;
				}
			}
		}
		this.lastPosition = base.get_transform().get_position();
	}
}
