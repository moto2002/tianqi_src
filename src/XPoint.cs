using System;
using System.Collections.Generic;
using UnityEngine;

public class XPoint
{
	public Vector3 position;

	public Quaternion rotation;

	public XPoint(Transform trans)
	{
		this.position = trans.get_position();
		this.rotation = trans.get_rotation();
	}

	public XPoint()
	{
		this.position = Vector3.get_zero();
		this.rotation = Quaternion.get_identity();
	}

	public XPoint ApplyOffset(List<int> offset)
	{
		if (offset == null)
		{
			return this;
		}
		if (offset.get_Count() < 1)
		{
			return this;
		}
		float num = 0f;
		float num2 = (float)offset.get_Item(0) * 0.01f;
		if (offset.get_Count() > 1)
		{
			num = (float)offset.get_Item(1) * 0.01f;
		}
		return new XPoint
		{
			position = this.position + this.rotation * Vector3.get_forward() * num + this.rotation * Vector3.get_left() * num2,
			rotation = this.rotation
		};
	}

	public XPoint ApplyForwardFix(int forwardFixAngle)
	{
		return new XPoint
		{
			position = this.position,
			rotation = (forwardFixAngle != 0) ? Quaternion.Euler(this.rotation.get_eulerAngles().x, this.rotation.get_eulerAngles().y + (float)forwardFixAngle, this.rotation.get_eulerAngles().z) : this.rotation
		};
	}
}
