using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class CameraPathGlobal
{
	public static CameraPathGlobal instance;

	public int plotId;

	public int plotIndex;

	public Action plotCallback;

	public List<ActorModel> plotNpcs = new List<ActorModel>();

	public CameraPathGlobal(int id, int index, Action callback)
	{
		CameraPathGlobal.instance = this;
		this.plotId = id;
		this.plotIndex = index;
		this.plotCallback = callback;
		string path = string.Concat(new object[]
		{
			"CameraPath/jq_",
			id,
			"_",
			index + 1
		});
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(path, typeof(Object));
		if (@object == null)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(@object as GameObject);
		gameObject.GetComponent<CameraPathAnimator>().animationObject = CamerasMgr.MainCameraRoot;
		gameObject.AddComponent<CameraPathListener>();
		gameObject.SetActive(true);
	}

	public ActorModel GetNpc(int npcId, int nameId)
	{
		for (int i = 0; i < this.plotNpcs.get_Count(); i++)
		{
			if (this.plotNpcs.get_Item(i).resGUID == npcId && this.plotNpcs.get_Item(i).nameId == nameId)
			{
				return this.plotNpcs.get_Item(i);
			}
		}
		ActorModel actorModel = ModelPool.Instance.Get(npcId);
		actorModel.nameId = nameId;
		actorModel.ModelType = ActorModelType.CG;
		actorModel.ShowShadow(true, npcId);
		this.plotNpcs.Add(actorModel);
		return actorModel;
	}

	public void ClearNpcs()
	{
		for (int i = 0; i < this.plotNpcs.get_Count(); i++)
		{
			Object.Destroy(this.plotNpcs.get_Item(i));
		}
		this.plotNpcs.Clear();
	}

	public void SetEntityVisible(bool visible)
	{
		this.SetRoleVisible(visible);
		this.SetPetVisible(visible);
		UIManagerControl.Instance.FakeHideAllUI(!visible, 7);
	}

	private void SetRoleVisible(bool visible)
	{
		EntityWorld.Instance.EntSelf.ShowSelf(visible);
	}

	private void SetPetVisible(bool visible)
	{
		using (Dictionary<long, EntityPet>.Enumerator enumerator = EntityWorld.Instance.EntCurPet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, EntityPet> current = enumerator.get_Current();
				current.get_Value().IsVisible = visible;
			}
		}
	}

	public void DelNpc(int npcId, int nameId)
	{
		for (int i = 0; i < this.plotNpcs.get_Count(); i++)
		{
			if (this.plotNpcs.get_Item(i).resGUID == npcId && this.plotNpcs.get_Item(i).nameId == nameId)
			{
				Object.Destroy(this.plotNpcs.get_Item(i));
				break;
			}
		}
	}

	public static List<int> GetPreloadNpcs(int plotId)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 2; i++)
		{
			string path = string.Concat(new object[]
			{
				"CameraPath/jq_",
				plotId,
				"_",
				i + 1
			});
			Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(path, typeof(Object));
			if (!(@object == null))
			{
				GameObject gameObject = @object as GameObject;
				CameraPathEventList eventList = gameObject.GetComponent<CameraPath>().eventList;
				for (int j = 0; j < eventList.realNumberOfPoints; j++)
				{
					CameraPathEvent cameraPathEvent = eventList[j];
					string[] array = cameraPathEvent.eventName.Split(new char[]
					{
						';'
					});
					string[] array2 = array;
					for (int k = 0; k < array2.Length; k++)
					{
						string text = array2[k];
						JuQingShiJian juQingShiJian = DataReader<JuQingShiJian>.Get(int.Parse(text));
						if (juQingShiJian != null)
						{
							if (juQingShiJian.eventType == 2)
							{
								list.Add(juQingShiJian.modelId);
							}
						}
					}
				}
			}
		}
		for (int l = 0; l < list.get_Count(); l++)
		{
			Debug.LogError("preload=" + list.get_Item(l));
		}
		return list;
	}
}
