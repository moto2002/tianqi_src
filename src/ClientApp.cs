using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using XEngine.AssetLoader;
using XNetwork;

public class ClientApp : MonoBehaviour
{
	protected static ClientApp instance;

	public bool isShowFightLog;

	public bool IsApplicationInited;

	private static readonly string WORD_FILTER_NAME = "Word.txt";

	private float unScaleEscapeTime;

	private float tryAddUnscaleEscapeTime;

	private int deltaUnscaleEscapeTime;

	private float scaleEscapeTime;

	private float tryAddScaleEscapeTime;

	private int deltaScaleEscapeTime;

	private float lastCheckExitApplicationTime;

	private float m_alpha;

	private float fadeTime = 1f;

	private bool isFadeInAndOut;

	public static ClientApp Instance
	{
		get
		{
			return ClientApp.instance;
		}
	}

	private void Awake()
	{
		Debug.Log("==>clientapp.Awake");
		ClientApp.instance = this;
		TimeManager.Instance.StartTimeRun();
		AppConst.GlobalTimeScale = Time.get_timeScale();
		BaseServiceMgr.InitGameSettingManagers();
		this.InitBeginning();
		Debug.Log("==>clientapp.SystemConfig.Init");
		SystemConfig.Init();
		Debug.Log("==>clientapp.InitWordFilter");
	}

	private void Start()
	{
		Debug.Log("==>clientapp.start");
		base.StartCoroutine(this.AsyncInit(true));
	}

	public void ReInit()
	{
		this.Release();
		NativeCallManager.QueryUpdate();
		base.StartCoroutine(this.AsyncInit(false));
	}

	public static void QuitApp()
	{
		if (Application.get_platform() != 8)
		{
			Application.Quit();
		}
	}

	protected void Release()
	{
		NetworkManager.Instance.ShutDownAllServer();
		NetworkService.Instance.Release();
		BaseServiceMgr.ReleaseManagers();
		UIManagerControl.Instance.HideAll();
		UIManagerControl.Instance.HideUI("CloseServerTips");
		CityInstance.Instance.HasEnteredCityBefore = false;
		MySceneManager.Instance.CurSceneID = 0;
		CameraGlobal.DestroyCamera();
		XInputManager.EnabledLogic = true;
	}

	private void InitBeginning()
	{
		if (SystemInfo.get_graphicsDeviceVersion().StartsWith("Metal"))
		{
			Debuger.Error("==>Metal Initialized", new object[0]);
		}
		else
		{
			Debuger.Error(SystemInfo.get_graphicsDeviceVersion(), new object[0]);
		}
		this.isShowFightLog = false;
	}

	[DebuggerHidden]
	private IEnumerator AsyncInit(bool InitIsFirstTime)
	{
		ClientApp.<AsyncInit>c__Iterator29 <AsyncInit>c__Iterator = new ClientApp.<AsyncInit>c__Iterator29();
		<AsyncInit>c__Iterator.InitIsFirstTime = InitIsFirstTime;
		<AsyncInit>c__Iterator.<$>InitIsFirstTime = InitIsFirstTime;
		<AsyncInit>c__Iterator.<>f__this = this;
		return <AsyncInit>c__Iterator;
	}

	private void OnUpdateEnd(bool InitIsFirstTime)
	{
		base.StartCoroutine(this.AfterUpdate(InitIsFirstTime));
	}

	[DebuggerHidden]
	private IEnumerator AfterUpdate(bool InitIsFirstTime)
	{
		ClientApp.<AfterUpdate>c__Iterator2A <AfterUpdate>c__Iterator2A = new ClientApp.<AfterUpdate>c__Iterator2A();
		<AfterUpdate>c__Iterator2A.InitIsFirstTime = InitIsFirstTime;
		<AfterUpdate>c__Iterator2A.<$>InitIsFirstTime = InitIsFirstTime;
		<AfterUpdate>c__Iterator2A.<>f__this = this;
		return <AfterUpdate>c__Iterator2A;
	}

	[DebuggerHidden]
	private IEnumerator InitWordFilter()
	{
		return new ClientApp.<InitWordFilter>c__Iterator2B();
	}

	private void InitBase()
	{
		this.InitSubgroup();
		this.InitSetting();
		Debuger.Info("==>clientapp  start", new object[0]);
		TimerHeap.AddCheatCheckHandler(delegate
		{
			Debuger.Info(" ==>found the accelerator cheating ", new object[0]);
		});
	}

	private void InitSubgroup()
	{
		base.get_gameObject().AddComponent<XInputManager>();
		if (SystemConfig.IsDebugInfoOn)
		{
			base.get_gameObject().AddUniqueComponent<DebugInfoUIViewManager>();
		}
		if (SystemConfig.IsDebugPing)
		{
			base.get_gameObject().AddUniqueComponent<PingDebug>();
		}
		Object.DontDestroyOnLoad(UINodesManager.UIRoot);
	}

	private void InitSetting()
	{
		UIUtils.SwitchAudioVolume();
		new CameraSetting();
		CameraSetting.InitManagers();
	}

	private void InitManager()
	{
		NetworkService.Instance.Init();
		SceneLoadedUIManager.Instance.Init();
		BaseServiceMgr.InitManagers();
	}

	private void Update()
	{
		InputManager.UpdateInputManager();
		TimeManager.Instance.TimeUpdate(Time.get_realtimeSinceStartup());
		this.CheckExitApplication();
		this.tryAddUnscaleEscapeTime = this.unScaleEscapeTime + Time.get_unscaledDeltaTime();
		this.deltaUnscaleEscapeTime = (int)this.tryAddUnscaleEscapeTime - (int)this.unScaleEscapeTime;
		if (this.deltaUnscaleEscapeTime > 0)
		{
			for (int i = 0; i < this.deltaUnscaleEscapeTime; i++)
			{
				EventDispatcher.Broadcast("TimeManagerProtectedEvent.UnscaleSecondUpdate");
			}
		}
		this.unScaleEscapeTime += Time.get_unscaledDeltaTime();
		this.tryAddScaleEscapeTime = this.scaleEscapeTime + Time.get_deltaTime();
		this.deltaScaleEscapeTime = (int)this.tryAddScaleEscapeTime - (int)this.scaleEscapeTime;
		if (this.deltaScaleEscapeTime > 0)
		{
			for (int j = 0; j < this.deltaScaleEscapeTime; j++)
			{
				EventDispatcher.Broadcast("TimeManagerProtectedEvent.ScaleSecondUpdate");
			}
		}
		this.scaleEscapeTime += Time.get_deltaTime();
	}

	protected void FixedUpdate()
	{
		if (!this.IsApplicationInited)
		{
			return;
		}
		NetworkManager.Instance.Update(Time.get_fixedDeltaTime());
	}

	private void LateUpdate()
	{
		if (!this.IsApplicationInited)
		{
			return;
		}
		OrganHandler.Instance.Update();
		LocalAgent.Update(Time.get_deltaTime());
	}

	private void CheckExitApplication()
	{
		if (Application.get_platform() == 11 && Input.GetKey(27))
		{
			if (Time.get_realtimeSinceStartup() - this.lastCheckExitApplicationTime <= 0.1f)
			{
				return;
			}
			this.lastCheckExitApplicationTime = Time.get_realtimeSinceStartup();
			if (SDKManager.Instance.ApplicationQuit())
			{
				return;
			}
			DialogExitUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(505136, false), GameDataUtils.GetChineseContent(505137, false), delegate
			{
				ClientApp.QuitApp();
			}, delegate
			{
			}, GameDataUtils.GetChineseContent(505138, false), GameDataUtils.GetChineseContent(505139, false), "button_orange_1", "button_yellow_1", null);
		}
	}

	private void OnApplicationQuit()
	{
		PushNotificationManager.DoOnApplicationQuit();
		TimeManager.Instance.StopTimeRun();
		NetworkManager.Instance.ShutDownAllServer();
		NetBufferPool.Instance.Release();
		if (Downloader.Instance != null)
		{
			Downloader.Instance.Stop();
		}
		StopwatchLog.Release();
		AssetLoader.ReleaseLoader();
	}

	public void DelayAction(float time, Action act)
	{
		base.StartCoroutine(this.DelayTime(time, act));
	}

	[DebuggerHidden]
	private IEnumerator DelayTime(float t, Action action)
	{
		ClientApp.<DelayTime>c__Iterator2C <DelayTime>c__Iterator2C = new ClientApp.<DelayTime>c__Iterator2C();
		<DelayTime>c__Iterator2C.t = t;
		<DelayTime>c__Iterator2C.action = action;
		<DelayTime>c__Iterator2C.<$>t = t;
		<DelayTime>c__Iterator2C.<$>action = action;
		return <DelayTime>c__Iterator2C;
	}

	private void OnApplicationPause(bool bPause)
	{
		this.FixedRT();
		if (Application.get_platform() == 11)
		{
			if (!bPause)
			{
				UIUtils.SetHardwareResolution();
			}
		}
		else if (Application.get_platform() == 8 && !bPause && EntityWorld.Instance.EntSelf != null && !ClientGMManager.Instance.NetSwitch00)
		{
			EventDispatcher.Broadcast(HeartbeatManagerEvent.ForceSendHeartbeat);
		}
		if (!bPause && !Application.get_isEditor() && File.Exists(AppConst.LogOnFilePath))
		{
			SystemConfig.LogSetting(true);
		}
	}

	private void FixedRT()
	{
		if (RTManager.Instance != null)
		{
			RTManager.Instance.FixedCamera2RTCommon();
		}
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (RTManager.Instance != null)
			{
				RTManager.Instance.FixedCamera2RTCommon();
			}
		});
	}

	public void PlayCGMovie(Action ac)
	{
		base.StartCoroutine(this.OnPlayCGMovie(ac));
	}

	[DebuggerHidden]
	private IEnumerator OnPlayCGMovie(Action ac)
	{
		ClientApp.<OnPlayCGMovie>c__Iterator2D <OnPlayCGMovie>c__Iterator2D = new ClientApp.<OnPlayCGMovie>c__Iterator2D();
		<OnPlayCGMovie>c__Iterator2D.ac = ac;
		<OnPlayCGMovie>c__Iterator2D.<$>ac = ac;
		<OnPlayCGMovie>c__Iterator2D.<>f__this = this;
		return <OnPlayCGMovie>c__Iterator2D;
	}

	private void OnGUI()
	{
		if (this.isFadeInAndOut)
		{
			GUI.set_color(new Color(GUI.get_color().r, GUI.get_color().g, GUI.get_color().b, Mathf.Clamp(this.m_alpha, 0f, 1f)));
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), Texture2D.get_blackTexture());
		}
	}
}
