using System;
using UnityEngine;

public class TransparencyCulling : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		ShaderEffectUtils.SetIsNearCamera(other.get_transform(), true);
	}

	private void OnTriggerExit(Collider other)
	{
		ShaderEffectUtils.SetIsNearCamera(other.get_transform(), false);
	}
}
