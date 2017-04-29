using System;
using UnityEngine;

public interface IOrgan
{
	void EnterPlatform(Collider other);

	void LeavePlatform(Collider other);

	void StayPlatform(Collider other);
}
