using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;
using XUPorterJSON;

public class MapDataManager
{
	private Dictionary<int, ArrayList> m_sceneBornPointData = new Dictionary<int, ArrayList>();

	private static MapDataManager instance;

	public static MapDataManager Instance
	{
		get
		{
			if (MapDataManager.instance == null)
			{
				MapDataManager.instance = new MapDataManager();
			}
			return MapDataManager.instance;
		}
	}

	public ArrayList GetPointDataBySceneID(int sceneID)
	{
		if (this.m_sceneBornPointData.ContainsKey(sceneID))
		{
			return this.m_sceneBornPointData.get_Item(sceneID);
		}
		TextAsset textAsset = AssetManager.AssetOfNoPool.LoadAssetNowNoAB("MapPointData/pointdata_" + sceneID.ToString(), typeof(Object)) as TextAsset;
		if (textAsset == null)
		{
			EntityWorld.Instance.ForceOut("没有数据", string.Format("找不到 sceneID = {0} 的出生点文件", sceneID), null);
		}
		string text = textAsset.get_text();
		ArrayList arrayList = (ArrayList)MiniJSON.jsonDecode(text);
		AssetLoader.UnloadAsset("MapPointData/pointdata_" + sceneID.ToString(), null);
		this.m_sceneBornPointData.Add(sceneID, arrayList);
		return arrayList;
	}

	public ArrayList GetPointDataByGroupKey(int sceneID, int bornPoint)
	{
		ArrayList pointDataBySceneID = this.GetPointDataBySceneID(sceneID);
		for (int i = 0; i < pointDataBySceneID.get_Count(); i++)
		{
			Hashtable hashtable = (Hashtable)pointDataBySceneID.get_Item(i);
			double num = (double)hashtable.get_Item("point");
			if ((int)num == bornPoint)
			{
				return (ArrayList)hashtable.get_Item("child_point");
			}
		}
		return null;
	}

	public bool GetPathPointByBornPoint(int sceneID, int bornPoint, out Vector2 position)
	{
		ArrayList pointDataBySceneID = this.GetPointDataBySceneID(sceneID);
		for (int i = 0; i < pointDataBySceneID.get_Count(); i++)
		{
			Hashtable hashtable = (Hashtable)pointDataBySceneID.get_Item(i);
			double num = (double)hashtable.get_Item("point");
			if ((int)num == bornPoint)
			{
				position = new Vector2((float)((double)hashtable.get_Item("x") * 0.01), (float)((double)hashtable.get_Item("y") * 0.01));
				return true;
			}
		}
		position = Vector2.get_zero();
		return false;
	}

	public Vector2 GetPoint(int sceneID, int point)
	{
		ArrayList pointDataBySceneID = this.GetPointDataBySceneID(sceneID);
		for (int i = 0; i < pointDataBySceneID.get_Count(); i++)
		{
			Hashtable hashtable = (Hashtable)pointDataBySceneID.get_Item(i);
			double num = (double)hashtable.get_Item("point");
			if ((int)num == point)
			{
				return new Vector2
				{
					x = (float)((double)hashtable.get_Item("x")),
					y = (float)((double)hashtable.get_Item("y"))
				};
			}
		}
		return Vector2.get_zero();
	}

	public Vector2 GetActorPoint(int sceneID)
	{
		ArrayList pointDataBySceneID = this.GetPointDataBySceneID(sceneID);
		for (int i = 0; i < pointDataBySceneID.get_Count(); i++)
		{
			Hashtable hashtable = (Hashtable)pointDataBySceneID.get_Item(i);
			double num = (double)hashtable.get_Item("point");
			if ((int)num == 6)
			{
				return new Vector2
				{
					x = (float)((double)hashtable.get_Item("x")),
					y = (float)((double)hashtable.get_Item("y"))
				};
			}
		}
		return Vector2.get_zero();
	}

	public Vector2 GetPetPoint(int sceneID)
	{
		ArrayList pointDataBySceneID = this.GetPointDataBySceneID(sceneID);
		for (int i = 0; i < pointDataBySceneID.get_Count(); i++)
		{
			Hashtable hashtable = (Hashtable)pointDataBySceneID.get_Item(i);
			double num = (double)hashtable.get_Item("point");
			if ((int)num == 7)
			{
				return new Vector2
				{
					x = (float)((double)hashtable.get_Item("x")),
					y = (float)((double)hashtable.get_Item("y"))
				};
			}
		}
		return Vector2.get_zero();
	}
}
