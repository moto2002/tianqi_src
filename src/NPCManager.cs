using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : BaseSubSystemManager
{
	protected static NPCManager instance;

	protected List<ActorNPC> npcLogicList = new List<ActorNPC>();

	public static NPCManager Instance
	{
		get
		{
			if (NPCManager.instance == null)
			{
				NPCManager.instance = new NPCManager();
			}
			return NPCManager.instance;
		}
	}

	public List<ActorNPC> NPCLogicList
	{
		get
		{
			return this.npcLogicList;
		}
	}

	protected NPCManager()
	{
	}

	public override void Release()
	{
		this.ClearAllNPCs();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(CityManagerEvent.EnteredCity, new Callback(this.EnteredCity));
	}

	protected void RemoveListener()
	{
		EventDispatcher.RemoveListener(CityManagerEvent.EnteredCity, new Callback(this.EnteredCity));
	}

	public void EnteredCity()
	{
		if (MySceneManager.IsMainScene(MySceneManager.Instance.CurSceneID))
		{
			TaskNPCManager.Instance.InitNPC();
			HearthNPCManager.Instance.InitNPC();
			CollectionNPCManager.Instance.InitNPC();
			if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
			{
				GuildWarMineNPCManager.Instance.InitNPC();
			}
		}
	}

	public ActorNPC CreateNPC(int id, int modelID, NPCBehavior npcEvent)
	{
		GameObject gameObject = new GameObject();
		UGUITools.ResetTransform(gameObject.get_transform(), NPCPool.Instance.root.get_transform());
		ActorNPC actorNPC = gameObject.AddUniqueComponent<ActorNPC>();
		actorNPC.Init(id, modelID, npcEvent);
		this.npcLogicList.Add(actorNPC);
		return actorNPC;
	}

	public void RemoveNPC(ActorNPC npc)
	{
		if (npc == null)
		{
			return;
		}
		this.npcLogicList.Remove(npc);
		npc.Release();
	}

	public void ClearAllNPCs()
	{
		TaskNPCManager.Instance.ClearNPC();
		WildBossNPCManager.Instance.ClearNPC();
		HearthNPCManager.Instance.ClearNPC();
		CollectionNPCManager.Instance.ClearNPC();
		GuildWarMineNPCManager.Instance.ClearNPC();
		for (int i = 0; i < this.npcLogicList.get_Count(); i++)
		{
			ActorNPC actorNPC = this.npcLogicList.get_Item(i);
			actorNPC.Release();
		}
		this.npcLogicList.Clear();
	}
}
