using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class ShadowController
{
	public static bool IsPreviousRealTimeShadowEnable;

	public static void RefreshShadows()
	{
		if (ShadowController.IsPreviousRealTimeShadowEnable != GameLevelManager.IsRealTimeShadowOn())
		{
			XDict<long, EntityParent> allEntities = EntityWorld.Instance.AllEntities;
			using (List<EntityParent>.Enumerator enumerator = allEntities.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityParent current = enumerator.get_Current();
					if (current != null & current.Actor)
					{
						if (ShadowController.IsPreviousRealTimeShadowEnable)
						{
							ShadowController.SetShadowModelRender(current.Actor.FixTransform, true);
						}
						else
						{
							ShadowProjector.ShowShadowProjector(current.Actor.FixTransform, true);
						}
						ShadowController.ShowShadow(current.ID, current.Actor.FixTransform, false, 0);
					}
				}
			}
		}
		ShadowController.IsPreviousRealTimeShadowEnable = GameLevelManager.IsRealTimeShadowOn();
	}

	public static void ShowShadow(long uuid, Transform actorTarget, bool bHideShadow, int modelId = 0)
	{
		ShadowController.ShowShadow(uuid, actorTarget, null, bHideShadow, modelId);
	}

	public static void ShowShadow(long uuid, ActorParent actorParent, bool bHideShadow, int modelId = 0)
	{
		if (actorParent is ActorMonster && (actorParent as ActorMonster).GetEntity().IsLogicBoss)
		{
			ShadowController.ShowShadow(uuid, actorParent.FixTransform, actorParent, bHideShadow, modelId);
		}
		else
		{
			ShadowController.ShowShadow(uuid, actorParent.FixTransform, null, bHideShadow, modelId);
		}
	}

	private static void ShowShadow(long uuid, Transform actorTarget, ActorParent actorParent, bool bHideShadow, int modelId = 0)
	{
		if (GameLevelManager.IsRealTimeShadowOn())
		{
			ShadowController.SetShadowModelRender(actorTarget, bHideShadow);
		}
		else
		{
			float scale = 1f;
			if (modelId > 0)
			{
				AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
				if (avatarModel != null)
				{
					scale = avatarModel.projectorScale;
				}
			}
			ShadowSlicePlaneMgr.ShowShadowSlicePlane(uuid, actorTarget, actorParent, bHideShadow, scale);
		}
	}

	public static void SetShadowModelRender(Transform actorTarget, bool bHideShadow = false)
	{
		if (actorTarget == null)
		{
			return;
		}
		Renderer[] componentsInChildren = actorTarget.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			SynY2Shadow synY2Shadow = componentsInChildren[i].get_gameObject().AddMissingComponent<SynY2Shadow>();
			synY2Shadow.Init(actorTarget, bHideShadow);
		}
	}

	public static void SetShadowModelRender2Child(Transform actorTarget, Transform node, bool bHideShadow = false)
	{
		if (!GameLevelManager.IsRealTimeShadowOn())
		{
			return;
		}
		if (actorTarget == null || node == null)
		{
			return;
		}
		Renderer[] components = node.GetComponents<Renderer>();
		for (int i = 0; i < components.Length; i++)
		{
			SynY2Shadow synY2Shadow = components[i].get_gameObject().AddMissingComponent<SynY2Shadow>();
			synY2Shadow.Init(actorTarget, bHideShadow);
		}
	}
}
