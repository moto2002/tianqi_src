using System;
using UnityEngine;
using XEngineActor;

public class ShadowSlicePlaneMgr
{
	private const string SHADOW_PREFABNAME = "ShadowSlicePlane";

	public const string SHADOW_NAME = "SP";

	public const float yOffset = 0.03f;

	public static bool IsShadow(GameObject go)
	{
		return go == null || go.get_name() == "SP";
	}

	public static void ShowShadowSlicePlane(long uuid, Transform actorTarget, ActorParent actorParent, bool bHideShadow, float scale)
	{
		if (!bHideShadow)
		{
			Transform transform = actorTarget.FindChild("SP");
			GameObject gameObject = (!(transform != null)) ? null : transform.get_gameObject();
			if (gameObject == null)
			{
				gameObject = ResourceManager.GetInstantiate2Prefab("ShadowSlicePlane");
				UGUITools.SetParent(actorTarget.get_gameObject(), gameObject, false, "SP");
				Transform expr_5A = gameObject.get_transform();
				expr_5A.set_localPosition(expr_5A.get_localPosition() + new Vector3(0f, 0.03f, 0f));
				gameObject.get_transform().set_localEulerAngles(new Vector3(90f, 0f, 0f));
				gameObject.get_transform().set_localScale(new Vector3(scale, scale, 1f));
				ShadowSlicePlane shadowSlicePlane = gameObject.AddComponent<ShadowSlicePlane>();
				shadowSlicePlane.uuid = uuid;
				shadowSlicePlane.m_root = actorTarget;
				shadowSlicePlane.m_actorParent = actorParent;
				shadowSlicePlane.m_meshRenderer = gameObject.GetComponent<MeshRenderer>();
				shadowSlicePlane.SetShadowY();
			}
			else
			{
				ShadowSlicePlane shadowSlicePlane = gameObject.GetComponent<ShadowSlicePlane>();
			}
			gameObject.set_layer(actorTarget.get_gameObject().get_layer());
			gameObject.SetActive(true);
		}
		else
		{
			Transform transform2 = actorTarget.FindChild("SP");
			if (transform2 != null)
			{
				transform2.get_gameObject().SetActive(false);
			}
		}
	}
}
