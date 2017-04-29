using GameData;
using Package;
using System;
using System.Collections.Generic;

public class WildBossNPCManager : BaseSubSystemManager
{
	protected static WildBossNPCManager instance;

	protected XDict<int, ActorNPC> wildBossNPCData = new XDict<int, ActorNPC>();

	public static WildBossNPCManager Instance
	{
		get
		{
			if (WildBossNPCManager.instance == null)
			{
				WildBossNPCManager.instance = new WildBossNPCManager();
			}
			return WildBossNPCManager.instance;
		}
	}

	protected WildBossNPCManager()
	{
	}

	public override void Release()
	{
		this.wildBossNPCData.Clear();
	}

	protected override void AddListener()
	{
	}

	public void CreateNPC(int id, int monsterID, bool isMultiBoss, int lv, Pos pos, List<int> dir, int state)
	{
		if (this.wildBossNPCData.ContainsKey(id))
		{
			return;
		}
		Monster monster = DataReader<Monster>.Get(monsterID);
		ActorNPC value = NPCManager.Instance.CreateNPC(id, monster.model, new BossNPCBehavior(monsterID, isMultiBoss, lv, pos, dir, state));
		this.wildBossNPCData.Add(id, value);
	}

	public void RemoveNPC(int id)
	{
		if (!this.wildBossNPCData.ContainsKey(id))
		{
			return;
		}
		NPCManager.Instance.RemoveNPC(this.wildBossNPCData[id]);
		this.wildBossNPCData.Remove(id);
	}

	public void ClearNPC()
	{
		for (int i = 0; i < this.wildBossNPCData.Count; i++)
		{
			NPCManager.Instance.RemoveNPC(this.wildBossNPCData.ElementValueAt(i));
		}
		this.wildBossNPCData.Clear();
	}

	public void UpdateNPC(int id, int state)
	{
		if (!this.wildBossNPCData.ContainsKey(id))
		{
			return;
		}
		this.wildBossNPCData[id].UpdateState(state);
	}

	public int GetNPCState(int id)
	{
		if (!this.wildBossNPCData.ContainsKey(id))
		{
			return -1;
		}
		return this.wildBossNPCData[id].GetState();
	}
}
