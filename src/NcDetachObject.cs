using System;
using UnityEngine;

public class NcDetachObject : NcEffectBehaviour
{
	public GameObject m_LinkGameObject;

	public static void Create(GameObject parentObj, GameObject linkObject)
	{
		NcDetachObject ncDetachObject = parentObj.AddComponent<NcDetachObject>();
		ncDetachObject.m_LinkGameObject = linkObject;
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		if (bRuntime)
		{
			NcEffectBehaviour.AdjustSpeedRuntime(this.m_LinkGameObject, fSpeedRate);
		}
	}
}
