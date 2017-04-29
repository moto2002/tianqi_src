using System;
using UnityEngine;

public interface IPlatformRider
{
	bool BoardPlatform(Platform platform);

	bool LeavePlatform(Platform platform);

	void UpdatePlatform(Vector3 platformDelta, bool isEqual = false);
}
