using System;
using UnityEngine;

public class PlatformRider : MonoBehaviour
{
	public virtual bool BoardPlatform(Platform platform)
	{
		return true;
	}

	public virtual bool LeavePlatform(Platform platform)
	{
		return true;
	}

	public virtual void UpdatePlatform(Vector3 platformDelta)
	{
		Transform expr_06 = base.get_transform();
		expr_06.set_position(expr_06.get_position() + platformDelta);
	}
}
