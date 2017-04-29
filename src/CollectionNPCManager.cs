using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionNPCManager : BaseSubSystemManager
{
	protected static CollectionNPCManager instance;

	protected XDict<int, ActorNPC> collectionNPCData = new XDict<int, ActorNPC>();

	public static CollectionNPCManager Instance
	{
		get
		{
			if (CollectionNPCManager.instance == null)
			{
				CollectionNPCManager.instance = new CollectionNPCManager();
			}
			return CollectionNPCManager.instance;
		}
	}

	protected CollectionNPCManager()
	{
	}

	public override void Release()
	{
		this.collectionNPCData.Clear();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(EventNames.RefreshCollect, new Callback(this.InitNPC));
		EventDispatcher.AddListener(EventNames.StartCollecting, new Callback(this.InitNPC));
		EventDispatcher.AddListener(EventNames.StopCollecting, new Callback(this.InitNPC));
		EventDispatcher.AddListener(EventNames.FinishCollecting, new Callback(this.InitNPC));
		EventDispatcher.AddListener<int>(CollectionNPCBehavior.OnNPCDieEnd, new Callback<int>(this.RemoveNPC));
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
		XDict<int, int> goodsModels = MainTaskManager.Instance.GoodsModels;
		XDict<int, int> xDict = new XDict<int, int>();
		XDict<int, int> xDict2 = new XDict<int, int>();
		List<int> list = new List<int>();
		for (int i = 0; i < goodsModels.Count; i++)
		{
			Debug.Log("采集模型:" + goodsModels.ElementKeyAt(i));
			if (this.collectionNPCData.ContainsKey(goodsModels.ElementKeyAt(i)))
			{
				xDict2.Add(goodsModels.ElementKeyAt(i), goodsModels.ElementValueAt(i));
			}
			else
			{
				xDict.Add(goodsModels.ElementKeyAt(i), goodsModels.ElementValueAt(i));
			}
		}
		for (int j = 0; j < this.collectionNPCData.Count; j++)
		{
			if (!goodsModels.ContainsKey(this.collectionNPCData.ElementKeyAt(j)) && this.collectionNPCData.ElementValueAt(j).GetState() != 2)
			{
				list.Add(this.collectionNPCData.ElementKeyAt(j));
			}
		}
		for (int k = 0; k < xDict.Count; k++)
		{
			this.CreateNPC(xDict.ElementKeyAt(k), xDict.ElementValueAt(k));
		}
		for (int l = 0; l < xDict2.Count; l++)
		{
			this.UpdateNPC(xDict2.ElementKeyAt(l), xDict2.ElementValueAt(l));
		}
		for (int m = 0; m < list.get_Count(); m++)
		{
			this.RemoveNPC(list.get_Item(m));
		}
	}

	public void CreateNPC(int collectionDataID, int state)
	{
		this.RemoveNPC(collectionDataID);
		ActorNPC value = NPCManager.Instance.CreateNPC(collectionDataID, DataReader<CaiJiPeiZhi>.Get(collectionDataID).model, new CollectionNPCBehavior(collectionDataID, state));
		this.collectionNPCData.Add(collectionDataID, value);
	}

	public void RemoveNPC(int npcDataID)
	{
		if (!this.collectionNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		NPCManager.Instance.RemoveNPC(this.collectionNPCData[npcDataID]);
		this.collectionNPCData.Remove(npcDataID);
	}

	public void ClearNPC()
	{
		for (int i = 0; i < this.collectionNPCData.Count; i++)
		{
			NPCManager.Instance.RemoveNPC(this.collectionNPCData.ElementValueAt(i));
		}
		this.collectionNPCData.Clear();
	}

	public void UpdateNPC(int npcDataID, int state)
	{
		if (!this.collectionNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		this.collectionNPCData[npcDataID].UpdateState(state);
	}
}
