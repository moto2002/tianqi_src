using GameData;
using System;
using System.Collections.Generic;

public class WildMonsterManager : BaseSubSystemManager
{
	protected long addIdValue = 1000000L;

	protected static WildMonsterManager instance;

	protected XDict<int, WildSceneMonsterInfo> wildMonsterInfo = new XDict<int, WildSceneMonsterInfo>();

	public static WildMonsterManager Instance
	{
		get
		{
			if (WildMonsterManager.instance == null)
			{
				WildMonsterManager.instance = new WildMonsterManager();
			}
			return WildMonsterManager.instance;
		}
	}

	public XDict<int, WildSceneMonsterInfo> WildMonsterInfo
	{
		get
		{
			return this.wildMonsterInfo;
		}
	}

	protected WildMonsterManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.wildMonsterInfo.Clear();
		EntityWorld.Instance.AllCityMonsters.Clear();
	}

	public override void Release()
	{
		this.wildMonsterInfo.Clear();
		EntityWorld.Instance.AllCityMonsters.Clear();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
	}

	protected void RemoveListener()
	{
		EventDispatcher.RemoveListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
	}

	public void LoadSceneEnd(int sceneID)
	{
		if (MySceneManager.IsMainScene(sceneID))
		{
			this.CreateSceneMonsterNPC();
		}
	}

	public void CreateSceneMonsterNPC()
	{
		XDict<int, WildSceneMonsterInfo> monsterDataBySceneId = this.GetMonsterDataBySceneId();
		if (monsterDataBySceneId == null)
		{
			return;
		}
		EntityWorld.Instance.AllCityMonsters.Clear();
		int num = 0;
		for (int i = 0; i < monsterDataBySceneId.Count; i++)
		{
			for (int j = 0; j < monsterDataBySceneId[i].num; j++)
			{
				this.CreateEntityCityMonster(num, monsterDataBySceneId[i]);
				num++;
			}
		}
	}

	private XDict<int, WildSceneMonsterInfo> GetMonsterDataBySceneId()
	{
		this.wildMonsterInfo.Clear();
		int curSceneID = MySceneManager.Instance.CurSceneID;
		List<YeWaiGuaiWu> dataList = DataReader<YeWaiGuaiWu>.DataList;
		int num = 0;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (curSceneID == dataList.get_Item(i).sceneId)
			{
				MonsterRefresh monsterRefresh = DataReader<MonsterRefresh>.Get(dataList.get_Item(i).monsterRefreshId);
				if (monsterRefresh != null)
				{
					Monster monster = DataReader<Monster>.Get(monsterRefresh.monster);
					this.wildMonsterInfo.Add(num, new WildSceneMonsterInfo
					{
						sceneId = curSceneID,
						monsterRefreshId = dataList.get_Item(i).monsterRefreshId,
						bornPoint = monsterRefresh.bornPoint,
						num = monsterRefresh.num,
						aiId = monsterRefresh.aiId,
						id = monsterRefresh.monster,
						name = monster.name,
						model = monster.model
					});
					num++;
				}
			}
		}
		return this.wildMonsterInfo;
	}

	public void CreateEntityCityMonster(int id, WildSceneMonsterInfo mData)
	{
		if (EntityWorld.Instance.AllCityMonsters.ContainsKey(this.addIdValue + (long)id))
		{
			return;
		}
		EntityCityMonster entityCityMonster = new EntityCityMonster();
		entityCityMonster.SetData(this.addIdValue + (long)id, mData);
		entityCityMonster.OnEnterField();
		entityCityMonster.CreateActor();
		EntityWorld.Instance.AddCityMonster(entityCityMonster);
	}
}
