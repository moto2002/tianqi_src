using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public static class WingGlobal
{
	public static List<GameObject> cameras = new List<GameObject>();

	public static List<GameObject> models = new List<GameObject>();

	public static bool IsWingWearAndNoHidden(int wingId)
	{
		return EntityWorld.Instance.EntSelf.Decorations.wingId == wingId && !EntityWorld.Instance.EntSelf.Decorations.wingHidden;
	}

	public static void ResetRawImage()
	{
		using (List<GameObject>.Enumerator enumerator = WingGlobal.cameras.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject current = enumerator.get_Current();
				Object.Destroy(current.get_gameObject());
			}
		}
		WingGlobal.cameras.Clear();
		using (List<GameObject>.Enumerator enumerator2 = WingGlobal.models.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				GameObject current2 = enumerator2.get_Current();
				Object.Destroy(current2.get_gameObject());
			}
		}
		WingGlobal.models.Clear();
	}

	public static void SetDefaultTransform(Transform aTransform)
	{
		aTransform.set_localPosition(Vector3.get_zero());
		aTransform.set_localRotation(Quaternion.get_identity());
		aTransform.set_localScale(Vector3.get_one());
	}

	public static void FindNodeRecursive(Transform parent, ref Transform node, string name)
	{
		if (node != null)
		{
			return;
		}
		if (parent == null)
		{
			return;
		}
		if (parent.get_name().TrimEnd(new char[]
		{
			' '
		}) == name)
		{
			node = parent;
			return;
		}
		for (int i = 0; i < parent.get_childCount(); i++)
		{
			Transform child = parent.GetChild(i);
			WingGlobal.FindNodeRecursive(child, ref node, name);
		}
	}

	public static ActorModel GetModel(int wingModelId)
	{
		ActorModel actorModel = ModelPool.Instance.Get(EntityWorld.Instance.EntSelf.ModelID);
		actorModel.ModelType = ActorModelType.UI;
		actorModel.ModelLayer = "NPC";
		float num = (float)(-1000 * (WingGlobal.models.get_Count() + 1));
		actorModel.get_transform().set_position(new Vector3(num, 0f));
		actorModel.EquipOn(EntityWorld.Instance.EntSelf.EquipCustomizationer.GetIdOfWeapon(), 0);
		actorModel.EquipOn(EntityWorld.Instance.EntSelf.EquipCustomizationer.GetIdOfClothes(), 0);
		actorModel.EquipWingOn(wingModelId);
		LayerSystem.SetGameObjectLayer(actorModel.get_gameObject(), "NPC", 1);
		actorModel.ShowSelf(true);
		return actorModel;
	}

	public static GameObject SetRawImage(RawImage rawImage, int wingModelId)
	{
		RenderTexture renderTexture = null;
		RTManager.CreateRenderTexture(ref renderTexture, "temp_render_texture");
		rawImage.GetComponent<RectTransform>().set_sizeDelta(new Vector2((float)renderTexture.get_width(), (float)renderTexture.get_height()));
		RTManager.SetRT(rawImage, renderTexture);
		Camera camera = WingGlobal.CreateCameraToModel(renderTexture);
		ActorModel model = WingGlobal.GetModel(wingModelId);
		WingGlobal.cameras.Add(camera.get_gameObject());
		WingGlobal.models.Add(model.get_gameObject());
		camera.get_transform().set_rotation(model.get_transform().get_rotation() * Quaternion.AngleAxis(180f, Vector3.get_up()));
		Vector3 vector = model.get_transform().get_up() * 0.9f;
		Vector3 vector2 = model.get_transform().get_forward() * 2.95f;
		camera.get_transform().set_position(model.get_transform().get_position() + vector + vector2);
		return model.get_gameObject();
	}

	public static Camera CreateCameraToModel(RenderTexture renderTexture)
	{
		GameObject gameObject = new GameObject("ui_temp_camera");
		Camera camera = gameObject.AddComponent<Camera>();
		camera.set_targetTexture(renderTexture);
		camera.set_cullingMask(1 << LayerMask.NameToLayer("NPC"));
		camera.set_orthographic(true);
		camera.set_orthographicSize(1.6f);
		camera.set_useOcclusionCulling(false);
		return camera;
	}

	public static WingPreviewCell GetOneWingPreview(Transform parent)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("WingPreviewCell");
		instantiate2Prefab.get_transform().SetParent(parent, false);
		instantiate2Prefab.SetActive(true);
		return instantiate2Prefab.GetComponent<WingPreviewCell>();
	}

	public static long GetCurrentWingFightingValue()
	{
		long num = 0L;
		if (WingManager.wingInfoDict == null)
		{
			return num;
		}
		using (Dictionary<int, WingInfo>.Enumerator enumerator = WingManager.wingInfoDict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, WingInfo> current = enumerator.get_Current();
				int cfgId = current.get_Value().cfgId;
				int lv = current.get_Value().lv;
				if (cfgId > 0)
				{
					wingLv wingLvInfo = WingManager.GetWingLvInfo(cfgId, lv);
					if (wingLvInfo != null)
					{
						int templateId = wingLvInfo.templateId;
						Attrs attrs = DataReader<Attrs>.Get(templateId);
						if (attrs != null)
						{
							num += EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, attrs.values);
						}
					}
				}
			}
		}
		return num;
	}
}
