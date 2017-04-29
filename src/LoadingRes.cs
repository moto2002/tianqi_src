using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

public class LoadingRes
{
	private static LoadingRes instance;

	protected static HashSet<int> m_load_fxIds = new HashSet<int>();

	protected static HashSet<int> m_load_modelIds = new HashSet<int>();

	public static HashSet<string> m_load_uiprefabs = new HashSet<string>();

	public static HashSet<string> m_load_uiatlas = new HashSet<string>();

	public static HashSet<string> m_load_spines = new HashSet<string>();

	public static int total_resource_num = 0;

	public static int finish_resource_num = 0;

	private static bool IsLoadAtlasOfAllUi = false;

	private List<Action> m_preload_resource_actions = new List<Action>();

	public static LoadingRes Instance
	{
		get
		{
			if (LoadingRes.instance == null)
			{
				LoadingRes.instance = new LoadingRes();
			}
			return LoadingRes.instance;
		}
	}

	public static void AddPreloadFxID(int id)
	{
		if (id != 0)
		{
			LoadingRes.m_load_fxIds.Add(id);
		}
	}

	public static void AddPreloadModelID(int id)
	{
		LoadingRes.m_load_modelIds.Add(id);
	}

	public void ClearResourcesMap()
	{
		LoadingRes.m_load_fxIds.Clear();
		LoadingRes.m_load_modelIds.Clear();
		LoadingRes.m_load_uiprefabs.Clear();
		LoadingRes.m_load_uiatlas.Clear();
		LoadingRes.m_load_spines.Clear();
	}

	private static void ResetResourceCounter()
	{
		LoadingRes.total_resource_num = 0;
		LoadingRes.finish_resource_num = 0;
	}

	public void ResetLoadDatas()
	{
		DataReader<Scene>.Init();
		DataReader<Action>.Init();
		DataReader<ActionBoss>.Init();
		DataReader<ActionMonster>.Init();
		DataReader<MonsterAttr>.Init();
		this.ClearResourcesMap();
	}

	public static void CalTotalNum()
	{
		ResWeight.total_model += LoadingRes.m_load_modelIds.get_Count() * 50;
		LoadingRes.total_resource_num += ResWeight.total_model;
		ResWeight.total_fx = LoadingRes.m_load_fxIds.get_Count() * 10;
		LoadingRes.total_resource_num += ResWeight.total_fx;
		ResWeight.total_uiprefab = LoadingRes.m_load_uiprefabs.get_Count() * 20;
		LoadingRes.total_resource_num += ResWeight.total_uiprefab;
		ResWeight.total_uiatlas = LoadingRes.m_load_uiatlas.get_Count() * 20;
		LoadingRes.total_resource_num += ResWeight.total_uiatlas;
		ResWeight.total_spine = LoadingRes.m_load_spines.get_Count() * 20;
		LoadingRes.total_resource_num += ResWeight.total_spine;
	}

	public static void ExtractInstanceUiPrefab()
	{
		LoadingRes.m_load_uiprefabs.Clear();
		LoadingRes.m_load_uiprefabs.Add("BattleUI");
		LoadingRes.m_load_uiprefabs.Add("BattlePassUI");
		LoadingRes.m_load_uiprefabs.Add("BattleLoseUI");
	}

	public static void ExtractInstanceAtlasOfAllUi()
	{
		if (!SystemConfig.IsPreloadAllUiAtlas)
		{
			return;
		}
		if (LoadingRes.IsLoadAtlasOfAllUi)
		{
			return;
		}
		LoadingRes.IsLoadAtlasOfAllUi = true;
		LoadingRes.m_load_uiatlas = PreviousLoadingRes.GetAllAtlas();
	}

	public static void ExtractInstanceFXSpines()
	{
		if (Application.get_platform() == 8)
		{
			return;
		}
		List<FXSpine> dataList = DataReader<FXSpine>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if ((dataList.get_Item(i).id >= 201 && dataList.get_Item(i).id <= 299) || (dataList.get_Item(i).id >= 401 && dataList.get_Item(i).id <= 499) || (dataList.get_Item(i).id >= 501 && dataList.get_Item(i).id <= 599))
			{
				if (!LoadingRes.m_load_spines.Contains(dataList.get_Item(i).name))
				{
					LoadingRes.m_load_spines.Add(dataList.get_Item(i).name);
				}
			}
		}
	}

	public static void ExtractCityUiPrefab()
	{
		LoadingRes.m_load_uiprefabs.Clear();
		LoadingRes.m_load_uiprefabs.Add("TownUI");
		LoadingRes.m_load_uiprefabs.Add("CurrenciesUI");
		LoadingRes.m_load_uiprefabs.Add("RoleUI");
		LoadingRes.m_load_uiprefabs.Add("BattleTypeUI");
	}

	public static void ExtractCityFXSpines()
	{
	}

	public void InitResourceCounter()
	{
		LoadingRes.ResetResourceCounter();
		this.ClearIllegalData();
		ResWeight.ResetAll();
		LoadingRes.CalTotalNum();
	}

	private void ClearIllegalData()
	{
		bool flag = LoadingRes.m_load_fxIds.Remove(0);
		while (flag)
		{
			flag = LoadingRes.m_load_fxIds.Remove(0);
		}
	}

	public void PreloadAllResource()
	{
		if (LoadingRes.total_resource_num == 0)
		{
			Loading.Instance.PreloadFinish(0);
			return;
		}
		this.m_preload_resource_actions.Clear();
		this.m_preload_resource_actions.Add(delegate
		{
			this.PreloadModel(ResWeight.ResType.model);
		});
		this.m_preload_resource_actions.Add(delegate
		{
			this.PreloadFX(ResWeight.ResType.fx);
		});
		this.m_preload_resource_actions.Add(delegate
		{
			this.PreloadUiPrefab(ResWeight.ResType.uiprefab);
		});
		this.m_preload_resource_actions.Add(delegate
		{
			this.PreloadSpines(ResWeight.ResType.spine);
		});
		this.m_preload_resource_actions.Add(delegate
		{
			this.PreloadUiAtlas(ResWeight.ResType.uiatlas);
		});
		this.JustLoadNextResource(ResWeight.ResType.none);
	}

	private void JustLoadNextResource(ResWeight.ResType finish_type)
	{
		if (finish_type != ResWeight.ResType.none)
		{
			Debug.Log("=====================>load resource finish, type = " + finish_type);
		}
		if (this.m_preload_resource_actions.get_Count() > 0)
		{
			Action action = this.m_preload_resource_actions.get_Item(0);
			this.m_preload_resource_actions.RemoveAt(0);
			action.Invoke();
		}
	}

	private void PreloadModel(ResWeight.ResType type)
	{
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(ResWeight.ResType.none);
			return;
		}
		using (HashSet<int>.Enumerator enumerator = LoadingRes.m_load_modelIds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				AvatarModel avatarModel = DataReader<AvatarModel>.Get(current);
				if (avatarModel == null)
				{
					this.LoadSuccessWithCheck(type);
				}
				else
				{
					AssetManager.LoadAssetWithPool(avatarModel.path, delegate(bool isSuccess)
					{
						this.LoadSuccessWithCheck(type);
					});
				}
			}
		}
	}

	private void PreloadFX(ResWeight.ResType type)
	{
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(ResWeight.ResType.none);
			return;
		}
		using (HashSet<int>.Enumerator enumerator = LoadingRes.m_load_fxIds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				Fx fx = DataReader<Fx>.Get(current);
				if (fx == null)
				{
					this.LoadSuccessWithCheck(type);
				}
				else
				{
					AssetManager.LoadAssetWithPool(fx.path, delegate(bool isSuccess)
					{
						this.LoadSuccessWithCheck(type);
					});
				}
			}
		}
	}

	private void PreloadUiPrefab(ResWeight.ResType type)
	{
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(ResWeight.ResType.none);
			return;
		}
		using (HashSet<string>.Enumerator enumerator = LoadingRes.m_load_uiprefabs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				AssetManager.LoadAssetOfUI(current, delegate(bool isSuccess)
				{
					this.LoadSuccessWithCheck(type);
				});
			}
		}
	}

	private void PreloadUiAtlas(ResWeight.ResType type)
	{
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(ResWeight.ResType.none);
			return;
		}
		using (HashSet<string>.Enumerator enumerator = LoadingRes.m_load_uiatlas.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				AssetManager.AssetOfTPManager.LoadAtlas(current, delegate(bool isSuccess)
				{
					this.LoadSuccessWithCheck(type);
				});
			}
		}
	}

	private void PreloadSpines(ResWeight.ResType type)
	{
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(ResWeight.ResType.none);
			return;
		}
		using (HashSet<string>.Enumerator enumerator = LoadingRes.m_load_spines.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				AssetManager.AssetOfSpineManager.LoadAssetWithPool(FileSystem.GetPathOfSpine(current), delegate(bool isSuccess)
				{
					this.LoadSuccessWithCheck(type);
				});
			}
		}
	}

	private void LoadSuccessWithCheck(ResWeight.ResType type)
	{
		Loading.Instance.PreloadFinish(ResWeight.GetWeight(type));
		ResWeight.LoadSuccess(type);
		if (ResWeight.IsLoadFinished(type))
		{
			this.JustLoadNextResource(type);
		}
	}
}
