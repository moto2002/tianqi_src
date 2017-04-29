using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HearthNPCManager : BaseSubSystemManager
{
	protected static HearthNPCManager instance;

	protected XDict<int, ActorNPC> hearthNPCData = new XDict<int, ActorNPC>();

	public static HearthNPCManager Instance
	{
		get
		{
			if (HearthNPCManager.instance == null)
			{
				HearthNPCManager.instance = new HearthNPCManager();
			}
			return HearthNPCManager.instance;
		}
	}

	protected HearthNPCManager()
	{
	}

	public override void Release()
	{
		this.ClearNPC();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<int>(EventNames.UpdateTaskId, new Callback<int>(this.OnUpdateTaskData));
	}

	protected void RemoveListener()
	{
		EventDispatcher.RemoveListener<int>(EventNames.UpdateTaskId, new Callback<int>(this.OnUpdateTaskData));
	}

	protected void OnUpdateTaskData(int newTaskID)
	{
		this.InitNPC();
	}

	public void InitNPC()
	{
		if (MySceneManager.Instance.CurSceneID == 0)
		{
			return;
		}
		if (MainTaskManager.Instance.MainTaskId == 0)
		{
			return;
		}
		int mainTaskId = MainTaskManager.Instance.MainTaskId;
		List<ChuanSongMenNPC> dataList = DataReader<ChuanSongMenNPC>.DataList;
		List<int> list = new List<int>();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).scene == MySceneManager.Instance.CurSceneID && (dataList.get_Item(i).frontMainTask == 0 || dataList.get_Item(i).frontMainTask < mainTaskId) && dataList.get_Item(i).position.get_Count() == 3)
			{
				list.Add(dataList.get_Item(i).id);
			}
		}
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (!this.hearthNPCData.ContainsKey(list.get_Item(j)))
			{
				list2.Add(list.get_Item(j));
			}
		}
		for (int k = 0; k < this.hearthNPCData.Count; k++)
		{
			if (!list.Contains(this.hearthNPCData.ElementKeyAt(k)))
			{
				list3.Add(this.hearthNPCData.ElementKeyAt(k));
			}
		}
		for (int l = 0; l < list2.get_Count(); l++)
		{
			this.CreateNPC(list2.get_Item(l));
		}
		for (int m = 0; m < list3.get_Count(); m++)
		{
			this.RemoveNPC(list3.get_Item(m));
		}
	}

	public void CreateNPC(int hearthDataID)
	{
		this.RemoveNPC(hearthDataID);
		ActorNPC value = NPCManager.Instance.CreateNPC(hearthDataID, DataReader<ChuanSongMenNPC>.Get(hearthDataID).model, new HearthNPCBehavior(hearthDataID));
		this.hearthNPCData.Add(hearthDataID, value);
	}

	public void RemoveNPC(int hearthDataID)
	{
		if (!this.hearthNPCData.ContainsKey(hearthDataID))
		{
			return;
		}
		NPCManager.Instance.RemoveNPC(this.hearthNPCData[hearthDataID]);
		this.hearthNPCData.Remove(hearthDataID);
	}

	public void ClearNPC()
	{
		for (int i = 0; i < this.hearthNPCData.Count; i++)
		{
			NPCManager.Instance.RemoveNPC(this.hearthNPCData.ElementValueAt(i));
		}
		this.hearthNPCData.Clear();
	}

	public Vector3 GetPosition(int hearthDataID)
	{
		if (!this.hearthNPCData.ContainsKey(hearthDataID))
		{
			return Vector3.get_zero();
		}
		return this.hearthNPCData[hearthDataID].get_transform().get_position();
	}
}
