using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLightmapData : MonoBehaviour
{
	[SerializeField]
	public List<RendererInfo> rendererInfo = new List<RendererInfo>();

	private void Awake()
	{
		this.UpdateLightmapRenderInfos();
		this.StaticBatching();
		GameLevelManager.SetSceneFX(GameLevelManager.GameLevelVariable.GetRealLODLEVEL(), true);
	}

	private void UpdateLightmapRenderInfos()
	{
		for (int i = 0; i < this.rendererInfo.get_Count(); i++)
		{
			this.rendererInfo.get_Item(i).UpdateLightmap();
		}
	}

	private void StaticBatching()
	{
		Transform transform = base.get_transform().FindChild("SceneNoStaticBatching");
		if (transform == null)
		{
			StaticBatchingUtility.Combine(base.get_gameObject());
		}
		else
		{
			Transform transform2 = base.get_transform().FindChild("SceneStaticBatching");
			if (transform2 != null)
			{
				StaticBatchingUtility.Combine(transform2.get_gameObject());
			}
		}
	}
}
