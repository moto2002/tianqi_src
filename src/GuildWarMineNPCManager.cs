using GameData;
using Package;
using System;
using System.Collections.Generic;

public class GuildWarMineNPCManager : BaseSubSystemManager
{
	protected static GuildWarMineNPCManager instance;

	protected XDict<int, ActorNPC> guildWarNPCList = new XDict<int, ActorNPC>();

	public static GuildWarMineNPCManager Instance
	{
		get
		{
			if (GuildWarMineNPCManager.instance == null)
			{
				GuildWarMineNPCManager.instance = new GuildWarMineNPCManager();
			}
			return GuildWarMineNPCManager.instance;
		}
	}

	protected GuildWarMineNPCManager()
	{
	}

	public override void Release()
	{
		this.ClearNPC();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(GuildWarManagerEvent.UpdateAllMineLiveData, new Callback(this.UpdateNPC));
	}

	public void InitNPC()
	{
		if (MySceneManager.Instance.CurSceneID == 0)
		{
			return;
		}
		if (!MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			return;
		}
		List<JunTuanZhanCaiJi> dataList = DataReader<JunTuanZhanCaiJi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			this.CreateNPC(dataList.get_Item(i).CollectionId, GuildWarManager.Instance.GetMineLiveData(dataList.get_Item(i).CollectionId));
		}
	}

	public void CreateNPC(int id, List<string> mineLiveData)
	{
		if (this.guildWarNPCList.ContainsKey(id))
		{
			return;
		}
		JunTuanZhanCaiJi junTuanZhanCaiJi = DataReader<JunTuanZhanCaiJi>.Get(id);
		if (junTuanZhanCaiJi == null)
		{
			return;
		}
		ActorNPC value = NPCManager.Instance.CreateNPC(id, junTuanZhanCaiJi.ModelId, new GuildWarMineNPCBehavior(id, mineLiveData));
		this.guildWarNPCList.Add(id, value);
	}

	public void RemoveNPC(int id)
	{
		if (!this.guildWarNPCList.ContainsKey(id))
		{
			return;
		}
		NPCManager.Instance.RemoveNPC(this.guildWarNPCList[id]);
		this.guildWarNPCList.Remove(id);
	}

	public void ClearNPC()
	{
		for (int i = 0; i < this.guildWarNPCList.Count; i++)
		{
			NPCManager.Instance.RemoveNPC(this.guildWarNPCList.ElementValueAt(i));
		}
		this.guildWarNPCList.Clear();
	}

	public void UpdateNPC()
	{
		List<string> list = new List<string>();
		List<GuildWarResBriefNty.GuildWarResBrief> values = GuildWarManager.Instance.GuildWarResourceBriefDic.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (!this.guildWarNPCList.ContainsKey(values.get_Item(i).resourceId))
			{
				return;
			}
			this.guildWarNPCList[values.get_Item(i).resourceId].UpdateState(GuildWarManager.Instance.GetMineLiveData(values.get_Item(i).resourceId));
		}
	}
}
