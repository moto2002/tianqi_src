using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class AOIService
{
	protected static AOIService instance;

	public static int DefaultMaxCounter = 99;

	public static int MaxCounter = 99;

	public int counter;

	public static AOIService Instance
	{
		get
		{
			if (AOIService.instance == null)
			{
				AOIService.instance = new AOIService();
			}
			return AOIService.instance;
		}
	}

	protected AOIService()
	{
	}

	public void Init()
	{
		NetworkManager.AddListenEvent<MapObjEnterMapNty>(new NetCallBackMethod<MapObjEnterMapNty>(this.OnAOIEnterMap));
		NetworkManager.AddListenEvent<MapObjLeaveMapNty>(new NetCallBackMethod<MapObjLeaveMapNty>(this.OnAOILeaveMap));
	}

	public void Release()
	{
		NetworkManager.RemoveListenEvent<MapObjEnterMapNty>(new NetCallBackMethod<MapObjEnterMapNty>(this.OnAOIEnterMap));
		NetworkManager.RemoveListenEvent<MapObjLeaveMapNty>(new NetCallBackMethod<MapObjLeaveMapNty>(this.OnAOILeaveMap));
		this.counter = 0;
	}

	protected void OnAOIEnterMap(short state, MapObjEnterMapNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.mapObjs.get_Count(); i++)
		{
			MapObjInfo mapObjInfo = down.mapObjs.get_Item(i);
			if (mapObjInfo.objType == GameObjectType.ENUM.Role)
			{
				if (this.counter > AOIService.MaxCounter)
				{
					break;
				}
				this.counter++;
			}
			this.CreateEntity(mapObjInfo, false);
		}
	}

	protected void OnAOILeaveMap(short state, MapObjLeaveMapNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.objIds.get_Count(); i++)
		{
			if (EntityWorld.Instance.AllEntities.ContainsKey(down.objIds.get_Item(i)) && EntityWorld.Instance.AllEntities[down.objIds.get_Item(i)].IsEntityCityPlayerType)
			{
				this.counter--;
			}
			EntityWorld.Instance.RemoveEntityByID(down.objIds.get_Item(i));
		}
	}

	public void SetMapArrivedObj(List<MapObjInfo> mapObjs)
	{
		this.counter = 0;
		if (mapObjs == null)
		{
			return;
		}
		List<MapObjInfo> list = new List<MapObjInfo>();
		List<MapObjInfo> list2 = new List<MapObjInfo>();
		List<MapObjInfo> list3 = new List<MapObjInfo>();
		for (int i = 0; i < mapObjs.get_Count(); i++)
		{
			MapObjInfo mapObjInfo = mapObjs.get_Item(i);
			if (mapObjInfo.objType == GameObjectType.ENUM.Role)
			{
				list.Add(mapObjInfo);
			}
			else if (mapObjInfo.battleInfo != null)
			{
				switch (mapObjInfo.battleInfo.wrapType)
				{
				case GameObjectType.ENUM.Role:
					list.Add(mapObjInfo);
					break;
				case GameObjectType.ENUM.Monster:
					list3.Add(mapObjInfo);
					break;
				case GameObjectType.ENUM.Pet:
					list2.Add(mapObjInfo);
					break;
				}
			}
		}
		List<MapObjInfo> list4 = new List<MapObjInfo>();
		for (int j = 0; j < list.get_Count(); j++)
		{
			MapObjInfo mapObjInfo2 = list.get_Item(j);
			if (mapObjInfo2.objType == GameObjectType.ENUM.Role)
			{
				if (this.counter > AOIService.MaxCounter)
				{
					break;
				}
				this.counter++;
			}
			list4.Add(mapObjInfo2);
		}
		list4.AddRange(list2);
		list4.AddRange(list3);
		for (int k = 0; k < list4.get_Count(); k++)
		{
			MapObjInfo item = list4.get_Item(k);
			this.CreateEntity(item, false);
		}
	}

	public void CreateEntity(MapObjInfo item, bool isClientCreate)
	{
		switch (item.objType)
		{
		case GameObjectType.ENUM.Role:
			this.CreateCityEntity(item);
			break;
		case GameObjectType.ENUM.Soldier:
			this.CreateBattleEntity(item, isClientCreate);
			break;
		}
	}

	protected void CreateCityEntity(MapObjInfo item)
	{
		EntityWorld.Instance.CreateCityOtherPlayer(item);
	}

	protected void CreateBattleEntity(MapObjInfo item, bool isClientCreate)
	{
		switch (item.battleInfo.wrapType)
		{
		case GameObjectType.ENUM.Role:
			EntityWorld.Instance.CreateOtherPlayer(item);
			break;
		case GameObjectType.ENUM.Monster:
			if (isClientCreate)
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					EntityWorld.Instance.CreateMonster(item, isClientCreate, 0L);
				}, 0.1f);
			}
			else
			{
				EntityWorld.Instance.CreateMonster(item, isClientCreate, 0L);
			}
			break;
		case GameObjectType.ENUM.Pet:
			EntityWorld.Instance.CreatePet(item, isClientCreate);
			break;
		}
	}
}
