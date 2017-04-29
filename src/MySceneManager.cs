using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using XEngine.AssetLoader;
using XNetwork;

public class MySceneManager : BaseSubSystemManager
{
	private class SwitchMapException : Exception
	{
		public SwitchMapException(string msg, Exception ex) : base(msg, ex)
		{
		}
	}

	public enum SceneMarkType
	{
		City = 1,
		CityWild,
		Instance
	}

	public const float FloorHeight = 30f;

	public const float Upslope = 15f;

	protected bool isSceneExist;

	protected int curSceneID;

	protected SceneType curSceneType;

	protected SceneType lastSceneType;

	private static MySceneManager instance;

	public bool IsSceneExist
	{
		get
		{
			return this.isSceneExist;
		}
		set
		{
			if (!this.isSceneExist && value)
			{
				this.isSceneExist = value;
				for (int i = 0; i < EntityWorld.Instance.AllEntities.Values.get_Count(); i++)
				{
					if (EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor)
					{
						EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.SetNavMeshAgent();
						EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.LoadEndResetPoistion();
					}
				}
				for (int j = 0; j < EntityWorld.Instance.AllCityPets.Values.get_Count(); j++)
				{
					if (EntityWorld.Instance.AllCityPets.Values.get_Item(j).Actor)
					{
						EntityWorld.Instance.AllCityPets.Values.get_Item(j).Actor.Init();
					}
				}
				for (int k = 0; k < EntityWorld.Instance.AllCityMonsters.Values.get_Count(); k++)
				{
					if (EntityWorld.Instance.AllCityMonsters.Values.get_Item(k).Actor)
					{
						EntityWorld.Instance.AllCityMonsters.Values.get_Item(k).Actor.LoadEndResetPoistion();
					}
				}
				if (EntityWorld.Instance.EntSelf != null)
				{
					EntityWorld.Instance.EntSelf.ApplySwitchScene();
				}
				for (int l = 0; l < NPCManager.Instance.NPCLogicList.get_Count(); l++)
				{
					NPCManager.Instance.NPCLogicList.get_Item(l).ApplyDefaultState();
				}
				InstanceManager.SelfPositionUpdated();
			}
			else
			{
				this.isSceneExist = value;
			}
		}
	}

	public int CurSceneID
	{
		get
		{
			return this.curSceneID;
		}
		set
		{
			this.curSceneID = value;
		}
	}

	public SceneType CurSceneType
	{
		get
		{
			return this.curSceneType;
		}
	}

	public SceneType LastSceneType
	{
		get
		{
			return this.lastSceneType;
		}
	}

	public bool IsCurrentGuildFieldScene
	{
		get
		{
			return MySceneManager.IsGuildFieldScene(this.CurSceneID);
		}
	}

	public bool IsCurrentGuildWarCityScene
	{
		get
		{
			return MySceneManager.IsGuildWarCityScene(this.CurSceneID);
		}
	}

	public static MySceneManager Instance
	{
		get
		{
			if (MySceneManager.instance == null)
			{
				MySceneManager.instance = new MySceneManager();
			}
			return MySceneManager.instance;
		}
	}

	private MySceneManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.isSceneExist = false;
		this.CurSceneID = 0;
		this.lastSceneType = SceneType.None;
		this.curSceneType = SceneType.None;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<SwitchMapRes>(new NetCallBackMethod<SwitchMapRes>(this.SwitchMapResp));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.SceneRelatedUnload, new Callback<int, int>(this.OnSceneRelatedUnload));
	}

	private void OnUnloadScene(int lastId, int nextId)
	{
		FXManager.Instance.DeleteFXs();
		FXPool.Instance.Clear();
		MonsterPool.Instance.Clear();
		PetPool.Instance.Clear();
		PlayerPool.Instance.Clear();
		CityPlayerPool.Instance.Clear();
		AvatarPool.Instance.Clear();
		ModelPool.Instance.Clear();
		NPCPool.Instance.Clear();
		GameObjectLoader.Instance.Clear();
		CityMonsterPool.Instance.Clear();
		FXSpineManager.Instance.DeleteAllSpine();
		FXSpinePool.Instance.Clear();
		AssetManager.AssetOfSpineManager.Clear();
		AssetManager.AssetOfControllerManager.Clear();
		AudioAssetPool.ClearAll();
		AssetManager.ClearEquipCustomizationAssets();
		AssetManager.AssetOfTPManager.ReleaseNoRef();
		AssetManager.ReleaseNoRef(false);
		GC.Collect();
		ActorVisibleManager.Instance.ClearAVC();
		TransactionNPCManager.Instance.Clear();
	}

	private void OnSceneRelatedUnload(int lastId, int nextId)
	{
		if (MySceneManager.IsMainScene(nextId))
		{
			ReleaseResOfUI.ReleaseResInInstance();
		}
		else
		{
			ReleaseResOfUI.ReleaseResInCity();
		}
		AssetManager.AssetOfTPManager.ReleaseNoRef();
		AssetManager.ReleaseNoRef(true);
	}

	public void SwitchMapResp(short state, SwitchMapRes down = null)
	{
		try
		{
			Debug.Log("切换地图: " + down.newMapId);
			if (state != 0)
			{
				StateManager.Instance.StateShow(state, 0);
				throw new Exception("state :" + state);
			}
			if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.ActSelf)
			{
				EntityWorld.Instance.AddPosRecord(EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position(), -1);
			}
			EventDispatcher.Broadcast(SceneManagerEvent.ClearSceneDependentLogic);
			if (MySceneManager.IsCityWildScene(down.oldMapId) || MySceneManager.IsCityWildScene(down.newMapId))
			{
				down.selfObj.pos = null;
			}
			if (EntityWorld.Instance.EntSelf != null)
			{
				EntityWorld.Instance.EntSelf.SetDataByMapObjInfo(down.selfObj, false, down.transformId);
			}
			if (down.oldMapId != down.newMapId || (down.oldMapId == down.newMapId && MySceneManager.IsMainScene(down.newMapId)))
			{
				this.IsSceneExist = false;
				this.lastSceneType = this.curSceneType;
				this.curSceneType = ((!MySceneManager.IsMainScene(down.newMapId)) ? SceneType.Battle : SceneType.City);
				Loading.Instance.OnStartLoad(down.newMapId, down.otherObjs);
			}
			else
			{
				AOIService.Instance.SetMapArrivedObj(down.otherObjs);
			}
		}
		catch (Exception ex)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm("错误", string.Format("切换地图时出错 :{0} {3} {1} version: {2}", new object[]
			{
				DateTime.get_Now().ToString("yyyy/MM/dd HH:mm:ss"),
				down.newMapId,
				GameManager.Instance.GetLocalVersionsString(),
				state
			}), null, "确定", "button_orange_1", UINodesManager.T4RootOfSpecial);
			throw new MySceneManager.SwitchMapException(string.Concat(new object[]
			{
				"mapID:",
				down.newMapId,
				" state:",
				state
			}), ex);
		}
	}

	public static bool IsMainScene(int sceneID)
	{
		return CityManager.Instance.IsCityScene(sceneID);
	}

	public static bool IsCityWildScene(int sceneID)
	{
		return sceneID != 0 && DataReader<Scene>.Contains(sceneID) && DataReader<Scene>.Get(sceneID).sceneType == 2;
	}

	public static bool IsGuildFieldScene(int sceneID)
	{
		return CityManager.Instance.IsGuildFieldScene(sceneID);
	}

	public static bool IsGuildWarCityScene(int sceneID)
	{
		return CityManager.Instance.IsGuildWarCityScene(sceneID);
	}

	public static bool IsSceneResourceAvailable(int sceneID)
	{
		return AssetBundleLoader.Instance.ResToABName(DataReader<Scene>.Get(sceneID).path) != null;
	}

	public static Vector3 GetTerrainPoint(float x, float z, float curHeight)
	{
		return XUtility.GetTerrainPoint(x, z, curHeight);
	}

	public static bool GetTerrainPoint(float x, float z, float curHeight, out Vector3 result)
	{
		if (!MySceneManager.Instance.IsSceneExist)
		{
			result = new Vector3(x, curHeight, z);
			return false;
		}
		return XUtility.GetTerrainPoint(x, z, curHeight, out result);
	}

	public static Vector3 GetTerrainBornPoint(float curHeight)
	{
		if (DataReader<Scene>.Contains(MySceneManager.Instance.CurSceneID))
		{
			Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID).pointId);
			return MySceneManager.GetTerrainPoint(point.x * 0.01f, point.y * 0.01f, curHeight);
		}
		return Vector3.get_zero();
	}

	public static bool GetTerrainBornPoint(float curHeight, out Vector3 result)
	{
		if (!DataReader<Scene>.Contains(MySceneManager.Instance.CurSceneID))
		{
			result = Vector3.get_zero();
			return false;
		}
		Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID).pointId);
		return MySceneManager.GetTerrainPoint(point.x * 0.01f, point.y * 0.01f, curHeight, out result);
	}

	public void PlayBGM()
	{
		if (this.CurSceneID == 0)
		{
			return;
		}
		Scene scene = DataReader<Scene>.Get(this.CurSceneID);
		if (scene == null)
		{
			return;
		}
		SoundManager.Instance.PlayBGMByID(scene.music);
	}

	public void Test()
	{
		SceneManager.LoadScene(3);
	}

	public void Test1()
	{
		SceneManager.UnloadScene(36);
		SceneManager.LoadScene(35, 1);
	}

	public void Test2()
	{
		SceneManager.UnloadScene(35);
		SceneManager.LoadSceneAsync(36, 1);
	}
}
