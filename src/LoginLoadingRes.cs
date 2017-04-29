using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class LoginLoadingRes : MonoBehaviour
{
	private enum LoadType
	{
		Data,
		Resource,
		WarmUpShader,
		Entity
	}

	private class GameDataLoadWrap
	{
		public Type GameDataType;

		public byte[] GameDataContent;

		public MethodInfo UnloadMethod;

		public void InitData(object[] param)
		{
			MethodInfo method = this.GameDataType.GetMethod("InitData", 24);
			param[0] = this.GameDataContent;
			try
			{
				method.Invoke(null, param);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			MethodInfo method2 = this.GameDataType.GetMethod("UnloadAsset", 24);
			this.UnloadMethod = method2;
		}
	}

	private enum LoadEntityType
	{
		None,
		CityPlayer
	}

	private float[] LoadProgressWeight;

	private float[] LoadProgress;

	public int LoadEndFlag;

	public int AllEndFlag;

	private int[] LoadFlag;

	private bool IsProgressChange;

	private List<LoginLoadingRes.GameDataLoadWrap> LoadWrapList = new List<LoginLoadingRes.GameDataLoadWrap>();

	private object LoadingLock = new object();

	private volatile int LoadingIndex;

	private volatile int LoadingIndexDescend;

	private volatile int LoadedCount;

	private volatile bool IsAbortLoading;

	private List<Exception> LastLoadDataException = new List<Exception>();

	private int MultThreadCount = 1;

	private int loadEntityThreadCount = 10;

	private int allEntityCount;

	private int currentAllEntityCount;

	private int entityCityPlayerCount = 100;

	private object loadEntityLock = new object();

	public int total_res_count;

	private int total_res_count_loaded;

	public static LoginLoadingRes Instance
	{
		get;
		private set;
	}

	public static void Init(GameObject root)
	{
		if (LoginLoadingRes.Instance != null)
		{
			throw new Exception("LoginLoadingRes 已经存在");
		}
		LoginLoadingRes.Instance = root.AddComponent<LoginLoadingRes>();
	}

	public static void Uninit()
	{
		if (LoginLoadingRes.Instance == null)
		{
			throw new Exception("LoginLoadingRes 单例不存在");
		}
		Object.Destroy(LoginLoadingRes.Instance);
		LoginLoadingRes.Instance = null;
	}

	private void Awake()
	{
		this.InitProgress();
		this.PreloadGameData();
		this.StartLoadGameData();
		this.StartWarmUpShader();
		this.LoadEntity();
	}

	public bool IsPreloadEnd()
	{
		return this.AllEndFlag == this.LoadEndFlag;
	}

	private void InitProgress()
	{
		this.LoadEndFlag = 0;
		int length = Enum.GetValues(typeof(LoginLoadingRes.LoadType)).get_Length();
		this.LoadProgressWeight = new float[length];
		this.LoadProgress = new float[length];
		this.LoadFlag = new int[length];
		this.SetWeight(LoginLoadingRes.LoadType.Data, 0.6f);
		this.SetWeight(LoginLoadingRes.LoadType.Resource, 0.3f);
		this.SetWeight(LoginLoadingRes.LoadType.WarmUpShader, 0.05f);
		this.SetWeight(LoginLoadingRes.LoadType.Entity, 0.05f);
		float num = 0f;
		for (int i = 0; i < length; i++)
		{
			num += this.LoadProgressWeight[i];
			this.LoadProgress[i] = 0f;
			this.LoadFlag[i] = 1 << i;
			this.AllEndFlag |= this.LoadFlag[i];
		}
		if (num != 1f)
		{
			Debug.LogError("权重总和必须等于1");
		}
	}

	private void SetWeight(LoginLoadingRes.LoadType type, float weight)
	{
		this.LoadProgressWeight[(int)type] = weight;
	}

	private void SetProgress(LoginLoadingRes.LoadType type, float progress)
	{
		float num = this.LoadProgress[(int)type];
		if (num != progress)
		{
			this.IsProgressChange = true;
			this.LoadProgress[(int)type] = progress;
		}
	}

	private void SetLoadEnd(LoginLoadingRes.LoadType type)
	{
		this.LoadEndFlag |= this.LoadFlag[(int)type];
	}

	private bool IsLoadEnd(LoginLoadingRes.LoadType type)
	{
		return (this.LoadEndFlag & this.LoadFlag[(int)type]) != 0;
	}

	private void RegisterGameData<T>() where T : class, new()
	{
		Type t = typeof(DataReader<T>);
		MethodInfo method = t.GetMethod("LoadData", 24);
		if (this.LoadWrapList.Exists((LoginLoadingRes.GameDataLoadWrap x) => x.GameDataType == t))
		{
			Debug.LogErrorFormat("注册了重复的表格: {0}", new object[]
			{
				typeof(T).get_Name()
			});
		}
		else
		{
			TextAsset textAsset = (TextAsset)method.Invoke(null, null);
			if (textAsset != null)
			{
				LoginLoadingRes.GameDataLoadWrap gameDataLoadWrap = new LoginLoadingRes.GameDataLoadWrap
				{
					GameDataType = t,
					GameDataContent = textAsset.get_bytes()
				};
				this.LoadWrapList.Add(gameDataLoadWrap);
			}
		}
	}

	private void PreloadGameData()
	{
		this.LoadWrapList.Clear();
		this.LoadedCount = 0;
		this.IsAbortLoading = false;
		if (Application.get_platform() == 8)
		{
			this.MultThreadCount = 1;
		}
		else
		{
			this.MultThreadCount = Math.Max(1, SystemInfo.get_processorCount());
		}
		this.RegisterGameData<ReleaseResWhiteLists>();
		this.RegisterGameData<FXSpine>();
		this.RegisterGameData<Fx>();
		this.RegisterGameData<UINameTable>();
		this.RegisterGameData<MonsterRefresh>();
		this.RegisterGameData<BoCiBiao>();
		this.RegisterGameData<Monster>();
		this.RegisterGameData<AvatarModel>();
		this.RegisterGameData<JuQingZhiYinBuZou>();
		this.RegisterGameData<RoleAi>();
		this.RegisterGameData<Condition>();
		this.RegisterGameData<Effect>();
		this.RegisterGameData<CameraAnimation>();
		this.RegisterGameData<Buff>();
		this.RegisterGameData<Skill>();
		this.RegisterGameData<ActionFuse>();
		this.RegisterGameData<FuBenJiChuPeiZhi>();
		this.RegisterGameData<ZhuoYueShuXing>();
		this.RegisterGameData<Audio>();
		this.RegisterGameData<ChuanSongMenNPC>();
		this.RegisterGameData<YeWaiGuaiWu>();
		this.RegisterGameData<zZhuangBeiSheZhi>();
		this.RegisterGameData<FuMoDaoJuShuXing>();
		this.RegisterGameData<wingLv>();
		this.RegisterGameData<wings>();
		this.RegisterGameData<SShenBingPeiZhi>();
		this.RegisterGameData<VipDengJi>();
		this.RegisterGameData<VipXiaoGuo>();
		this.RegisterGameData<HuoDongZhongXin>();
		this.RegisterGameData<ZhuanZhiJiChuPeiZhi>();
		this.RegisterGameData<Audio2UI>();
		this.RegisterGameData<UIWidgetTable>();
		this.RegisterGameData<MonsterAttr>();
		this.RegisterGameData<DiaoLuo>();
		this.LoadWrapList = Enumerable.ToList<LoginLoadingRes.GameDataLoadWrap>(Enumerable.OrderBy<LoginLoadingRes.GameDataLoadWrap, int>(this.LoadWrapList, (LoginLoadingRes.GameDataLoadWrap x) => x.GameDataContent.Length));
		this.LoadingIndex = -1;
		this.LoadingIndexDescend = this.LoadWrapList.get_Count();
	}

	private void StartLoadGameData()
	{
		Thread thread = new Thread(new ThreadStart(this.DoLoadGameData));
		thread.set_Priority(4);
		thread.Start();
	}

	private void DoLoadGameData()
	{
		try
		{
			for (int i = 0; i < this.LoadWrapList.get_Count(); i++)
			{
				LoginLoadingRes.GameDataLoadWrap gameDataLoadWrap = this.LoadWrapList.get_Item(i);
				MethodInfo method = gameDataLoadWrap.GameDataType.GetMethod("PrepareSerializer", 24);
				method.Invoke(null, null);
			}
			for (int j = 0; j < this.MultThreadCount - 1; j++)
			{
				Thread thread = new Thread(new ParameterizedThreadStart(this.MultThreadLoadDataByDescend));
				thread.set_Priority(4);
				thread.Start(j);
			}
			this.MultThreadLoadData();
		}
		catch (Exception ex)
		{
			this.AddThreadEx(ex);
		}
	}

	private void StartWarmUpShader()
	{
		Debug.Log("==>StartWarmUpShader");
		ShaderManager.BeginInit(new Action(this.OnShaderManagerInited));
	}

	private void OnShaderManagerInited()
	{
		Debug.Log("==>OnShaderManagerInited01");
		base.StartCoroutine(ShaderManager.Instance.WarmUp(delegate(int loaded, int count)
		{
			this.SetProgress(LoginLoadingRes.LoadType.WarmUpShader, (float)loaded / (float)count);
		}, delegate
		{
			this.SetLoadEnd(LoginLoadingRes.LoadType.WarmUpShader);
		}));
		Debug.Log("==>OnShaderManagerInited02");
	}

	private void LoadEntity()
	{
		this.allEntityCount = this.entityCityPlayerCount;
		for (int i = 0; i < this.loadEntityThreadCount; i++)
		{
			Thread thread = new Thread(new ThreadStart(this.CreateEntityCityPlayer));
			thread.set_Priority(4);
			thread.Start(i);
		}
		this.SetProgress(LoginLoadingRes.LoadType.Entity, 0.05f);
		this.SetLoadEnd(LoginLoadingRes.LoadType.Entity);
	}

	private void CreateEntityCityPlayer()
	{
		LoginLoadingRes.LoadEntityType type = this.GetNextLoadType();
		while (type != LoginLoadingRes.LoadEntityType.None)
		{
			type = this.LoadEntity(type);
			this.currentAllEntityCount++;
			this.SetProgress(LoginLoadingRes.LoadType.Entity, (float)this.currentAllEntityCount / (float)this.allEntityCount);
		}
		object obj = this.loadEntityLock;
		lock (obj)
		{
			if (this.currentAllEntityCount > 0 && this.currentAllEntityCount == this.allEntityCount)
			{
				this.currentAllEntityCount = -1;
				this.SetLoadEnd(LoginLoadingRes.LoadType.Entity);
			}
		}
	}

	private LoginLoadingRes.LoadEntityType GetNextLoadType()
	{
		object obj = this.loadEntityLock;
		LoginLoadingRes.LoadEntityType result;
		lock (obj)
		{
			if (EntityWorld.Instance.CityPlayerEntityPool.get_Count() < this.entityCityPlayerCount)
			{
				result = LoginLoadingRes.LoadEntityType.CityPlayer;
			}
			else
			{
				result = LoginLoadingRes.LoadEntityType.None;
			}
		}
		return result;
	}

	private LoginLoadingRes.LoadEntityType LoadEntity(LoginLoadingRes.LoadEntityType type)
	{
		object obj = this.loadEntityLock;
		LoginLoadingRes.LoadEntityType result;
		lock (obj)
		{
			if (type != LoginLoadingRes.LoadEntityType.CityPlayer)
			{
				result = LoginLoadingRes.LoadEntityType.None;
			}
			else
			{
				EntityWorld.Instance.CityPlayerEntityPool.Add(new EntityCityPlayer());
				if (EntityWorld.Instance.CityPlayerEntityPool.get_Count() < this.entityCityPlayerCount)
				{
					result = LoginLoadingRes.LoadEntityType.CityPlayer;
				}
				else
				{
					result = LoginLoadingRes.LoadEntityType.None;
				}
			}
		}
		return result;
	}

	private void AddThreadEx(Exception ex)
	{
		List<Exception> lastLoadDataException = this.LastLoadDataException;
		lock (lastLoadDataException)
		{
			this.LastLoadDataException.Add(ex);
		}
	}

	private void MultThreadLoadData()
	{
		try
		{
			LoginLoadingRes.GameDataLoadWrap next = this.GetNext();
			object[] param = new object[1];
			int num = 0;
			while (next != null)
			{
				num++;
				next.InitData(param);
				next = this.GetNext();
				this.LoadedCount++;
			}
		}
		catch (Exception ex)
		{
			this.AddThreadEx(ex);
		}
	}

	private void MultThreadLoadDataByDescend(object threadIdx)
	{
		try
		{
			LoginLoadingRes.GameDataLoadWrap nextDescend = this.GetNextDescend();
			object[] param = new object[1];
			int num = 0;
			while (nextDescend != null)
			{
				num++;
				nextDescend.InitData(param);
				nextDescend = this.GetNextDescend();
				this.LoadedCount++;
			}
		}
		catch (Exception ex)
		{
			this.AddThreadEx(ex);
		}
	}

	private LoginLoadingRes.GameDataLoadWrap GetNext()
	{
		LoginLoadingRes.GameDataLoadWrap result = null;
		object loadingLock = this.LoadingLock;
		lock (loadingLock)
		{
			this.LoadingIndex++;
			if (this.LoadingIndex < this.LoadingIndexDescend)
			{
				result = this.LoadWrapList.get_Item(this.LoadingIndex);
			}
		}
		return result;
	}

	private LoginLoadingRes.GameDataLoadWrap GetNextDescend()
	{
		LoginLoadingRes.GameDataLoadWrap result = null;
		object loadingLock = this.LoadingLock;
		lock (loadingLock)
		{
			this.LoadingIndexDescend--;
			if (this.LoadingIndex < this.LoadingIndexDescend)
			{
				result = this.LoadWrapList.get_Item(this.LoadingIndexDescend);
			}
		}
		return result;
	}

	private bool CheckIsGameDataLoaded()
	{
		this.IsAbortLoading = !Application.get_isPlaying();
		List<Exception> lastLoadDataException = this.LastLoadDataException;
		lock (lastLoadDataException)
		{
			if (this.LastLoadDataException.get_Count() != 0)
			{
				for (int i = 0; i < this.LastLoadDataException.get_Count(); i++)
				{
					Debug.LogException(this.LastLoadDataException.get_Item(i));
				}
				this.LastLoadDataException.Clear();
				bool result = true;
				return result;
			}
		}
		this.UpdateProgressToData();
		if (this.LoadedCount == this.LoadWrapList.get_Count())
		{
			for (int j = 0; j < this.LoadWrapList.get_Count(); j++)
			{
				MethodInfo unloadMethod = this.LoadWrapList.get_Item(j).UnloadMethod;
				unloadMethod.Invoke(null, null);
			}
			this.LoadWrapList.Clear();
			GC.Collect();
			this.SetLoadEnd(LoginLoadingRes.LoadType.Data);
			this.StartLoadResource();
			return true;
		}
		return false;
	}

	private void StartLoadResource()
	{
		UIManagerControl.Instance.OpenUI_Async("LoginUI", null, null);
		ResourceRegulation.Init();
	}

	public void PreloadResourceFinish()
	{
		this.total_res_count_loaded++;
		if (this.total_res_count_loaded >= this.total_res_count)
		{
			this.SetLoadEnd(LoginLoadingRes.LoadType.Resource);
		}
		this.UpdateProgressToResource();
	}

	private void Update()
	{
		if (!this.IsLoadEnd(LoginLoadingRes.LoadType.Data))
		{
			this.CheckIsGameDataLoaded();
		}
		this.UpdateProgress();
	}

	private void UpdateProgressToData()
	{
		if (this.LoadWrapList.get_Count() != 0)
		{
			float progress = (float)this.LoadedCount / (float)this.LoadWrapList.get_Count();
			this.SetProgress(LoginLoadingRes.LoadType.Data, progress);
		}
	}

	private void UpdateProgressToResource()
	{
		float progress = (float)this.total_res_count_loaded / (float)this.total_res_count;
		this.SetProgress(LoginLoadingRes.LoadType.Resource, progress);
	}

	private void UpdateProgress()
	{
		if (this.IsProgressChange)
		{
			float num = 0f;
			for (int i = 0; i < this.LoadProgressWeight.Length; i++)
			{
				num += this.LoadProgressWeight[i] * this.LoadProgress[i];
			}
			PreloadingUIView.SetProgressInSmooth(num);
			this.IsProgressChange = false;
		}
	}

	public void PreAwake()
	{
		int spine_uid01 = FXSpineManager.Instance.PlaySpine(3501, UINodesManager.NormalUIRoot, string.Empty, 2001, null, "CameraRange", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		TimerHeap.AddTimer(5000u, 0, delegate
		{
			FXSpineManager.Instance.DeleteSpine(spine_uid01, true);
		});
		UIManagerControl.Instance.InstantiateUI("BattleUI", UINodesManager.NormalUIRoot, delegate(GameObject go)
		{
			BattleUI component = go.GetComponent<BattleUI>();
			component.Awake();
			component.get_transform().SetAsFirstSibling();
			component.get_gameObject().SetActive(true);
			component.get_gameObject().SetActive(false);
		});
	}
}
