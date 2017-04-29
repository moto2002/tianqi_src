using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class DebugUIView : UIBase
{
	public const string SCENE_ROOT = "SceneSystem";

	public const string SceneNoStaticBatching = "SceneNoStaticBatching";

	public const string SceneStaticBatching = "SceneStaticBatching";

	public const string SCENE_FX = "SceneFX";

	private const float OUTPUT_HEIGHT = 270f;

	public static DebugUIView Instance;

	private RectTransform RegionOutput;

	private Text m_lblOutputContent;

	private RectTransform RegionTips;

	private RectTransform RegionButtons;

	private Transform ButtonSROfFun;

	private Transform ButtonSROfFun2;

	private Transform ButtonSROfGM;

	private Transform ButtonSROfProfile;

	private InputField m_InputField;

	private bool _IsRegionOutputHide = true;

	private bool _IsRegionButtonsHide = true;

	private bool _IsRegionTipsHide;

	private bool IsShowScene = true;

	private bool IsShowFXOfScene = true;

	private bool IsShowModel = true;

	private bool IsShowLight = true;

	private bool IsShowUI = true;

	private GameObject goSceneSystem;

	private GameObject goSceneFX;

	private List<GameObject> listModelOfNoBattle = new List<GameObject>();

	private List<GameObject> listModelOfAll = new List<GameObject>();

	private bool IsFound;

	private bool IsOutputLock;

	private Vector2 output_anchoredPosition = Vector2.get_zero();

	private static string LogString = string.Empty;

	public bool IsRegionOutputHide
	{
		get
		{
			return this._IsRegionOutputHide;
		}
		set
		{
			this._IsRegionOutputHide = value;
			if (value)
			{
				this.RegionOutput.set_anchoredPosition(new Vector2(0f, 280f));
			}
			else
			{
				this.RegionOutput.set_anchoredPosition(new Vector2(0f, 0f));
			}
		}
	}

	public bool IsRegionButtonsHide
	{
		get
		{
			return this._IsRegionButtonsHide;
		}
		set
		{
			this._IsRegionButtonsHide = value;
			if (value)
			{
				this.RegionButtons.set_anchoredPosition(new Vector2(0f, -370f));
			}
			else
			{
				this.RegionButtons.set_anchoredPosition(new Vector2(0f, 0f));
			}
		}
	}

	public bool IsRegionTipsHide
	{
		get
		{
			return this._IsRegionTipsHide;
		}
		set
		{
			this._IsRegionTipsHide = value;
			if (value)
			{
				this.RegionTips.set_anchoredPosition(new Vector2(-304f, 100f));
			}
			else
			{
				this.RegionTips.set_anchoredPosition(new Vector2(0f, 100f));
			}
		}
	}

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		DebugUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.RegionOutput = (base.FindTransform("RegionOutput") as RectTransform);
		this.m_lblOutputContent = base.FindTransform("OutputContent").GetComponent<Text>();
		this.RegionTips = (base.FindTransform("RegionTips") as RectTransform);
		this.RegionButtons = (base.FindTransform("RegionButtons") as RectTransform);
		this.ButtonSROfFun = base.FindTransform("ButtonSROfFun");
		this.ButtonSROfFun2 = base.FindTransform("ButtonSROfFun2");
		this.ButtonSROfGM = base.FindTransform("ButtonSROfGM");
		this.ButtonSROfProfile = base.FindTransform("ButtonSROfProfile");
		this.m_InputField = base.FindTransform("InputField").GetComponent<InputField>();
		this.IsRegionOutputHide = this.IsRegionOutputHide;
		this.IsRegionButtonsHide = this.IsRegionButtonsHide;
		EventTriggerListener.Get(base.FindTransform("BtnClose").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnClose);
		EventTriggerListener.Get(base.FindTransform("BtnOutputOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnOutputOn);
		EventTriggerListener.Get(base.FindTransform("BtnOutputClear").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnOutputClearOn);
		EventTriggerListener.Get(base.FindTransform("BtnOutputLock").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnOutputLockOn);
		EventTriggerListener.Get(base.FindTransform("BtnTipsOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnTipsOn);
		EventTriggerListener.Get(base.FindTransform("BtnButtonsOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnButtonsOn);
		EventTriggerListener.Get(base.FindTransform("BtnFunOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnFunOn);
		EventTriggerListener.Get(base.FindTransform("BtnFun2On").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnFun2On);
		EventTriggerListener.Get(base.FindTransform("BtnGMOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnGMOn);
		EventTriggerListener.Get(base.FindTransform("BtnProfileOn").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnProfileOn);
		EventTriggerListener.Get(base.FindTransform("Fun_GM").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_GM);
		EventTriggerListener.Get(base.FindTransform("Fun_FX").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_FX);
		EventTriggerListener.Get(base.FindTransform("Fun_UIFX").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_UIFX);
		EventTriggerListener.Get(base.FindTransform("Fun_PostProcess").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_PostProcess);
		EventTriggerListener.Get(base.FindTransform("Fun_Scene").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_Scene);
		EventTriggerListener.Get(base.FindTransform("Fun_Light").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_Light);
		EventTriggerListener.Get(base.FindTransform("Fun_UI").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_UI);
		EventTriggerListener.Get(base.FindTransform("Fun_UISystem").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_UISystem);
		EventTriggerListener.Get(base.FindTransform("Fun_ActorModel").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_ActorModel);
		EventTriggerListener.Get(base.FindTransform("Fun_ReleaseResource").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_ReleaseResource);
		EventTriggerListener.Get(base.FindTransform("Fun_Billboard").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_Billboard);
		EventTriggerListener.Get(base.FindTransform("Fun_ImageRes").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_ImageRes);
		EventTriggerListener.Get(base.FindTransform("Fun_Frame").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_Frame);
		EventTriggerListener.Get(base.FindTransform("Fun_Effect").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_Effect);
		EventTriggerListener.Get(base.FindTransform("Fun_ActorAnim").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_ActorAnim);
		EventTriggerListener.Get(base.FindTransform("Fun_MainCamera").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_MainCamera);
		EventTriggerListener.Get(base.FindTransform("Fun_UICamera").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_UICamera);
		EventTriggerListener.Get(base.FindTransform("Fun_FXOfScene").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnFun_FXOfScene);
		EventTriggerListener.Get(base.FindTransform("GM_Excute").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_Excute);
		EventTriggerListener.Get(base.FindTransform("GM_stopallai").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_stopallai);
		EventTriggerListener.Get(base.FindTransform("GM_KillAllMonsters").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_KillAllMonsters);
		EventTriggerListener.Get(base.FindTransform("GM_PassAllInstance").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_PassAllInstance);
		EventTriggerListener.Get(base.FindTransform("GM_CameraOcclusion").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_CameraOcclusion);
		EventTriggerListener.Get(base.FindTransform("GM_AIPause").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_AIPause);
		EventTriggerListener.Get(base.FindTransform("GM_AIResume").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_AIResume);
		EventTriggerListener.Get(base.FindTransform("GM_Fog").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_Fog);
		EventTriggerListener.Get(base.FindTransform("GM_BattleGMButton").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnGM_BattleGMButton);
		EventTriggerListener.Get(base.FindTransform("Profile_Memory").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnProfile_MemoryOn);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.IsOutputLock = false;
		this.DoPrintLogCache();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnSceneLoadEnd));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnSceneLoadEnd));
	}

	private void OnSceneLoadEnd(int sceneId)
	{
		this.IsFound = false;
	}

	private void OnBtnClose(GameObject sender)
	{
		this.Show(false);
	}

	private void OnBtnOutputOn(GameObject sender)
	{
		this.IsRegionOutputHide = !this.IsRegionOutputHide;
	}

	private void OnBtnOutputClearOn(GameObject sender)
	{
		DebugUIView.LogString = string.Empty;
		this.SetOutput(DebugUIView.LogString);
	}

	private void OnBtnOutputLockOn(GameObject sender)
	{
		this.IsOutputLock = !this.IsOutputLock;
		this.DoPrintLogCache();
	}

	private void OnBtnTipsOn(GameObject sender)
	{
		this.IsRegionTipsHide = !this.IsRegionTipsHide;
	}

	private void OnBtnButtonsOn(GameObject sender)
	{
		this.IsRegionButtonsHide = !this.IsRegionButtonsHide;
	}

	private void OnBtnFunOn(GameObject sender)
	{
		this.HideAllRegionButtons();
		this.ButtonSROfFun.get_gameObject().SetActive(true);
	}

	private void OnBtnFun2On(GameObject sender)
	{
		this.HideAllRegionButtons();
		this.ButtonSROfFun2.get_gameObject().SetActive(true);
	}

	private void OnBtnGMOn(GameObject sender)
	{
		this.HideAllRegionButtons();
		this.ButtonSROfGM.get_gameObject().SetActive(true);
	}

	private void OnBtnProfileOn(GameObject sender)
	{
		this.HideAllRegionButtons();
		this.ButtonSROfProfile.get_gameObject().SetActive(true);
	}

	private void OnFun_GM(GameObject sender)
	{
		if (ClientGMManager.Instance.GMOpen)
		{
			ClientGMManager.Instance.GMOpen = false;
		}
		else
		{
			ClientGMManager.Instance.GMOpen = true;
		}
	}

	private void OnFun_FX(GameObject sender)
	{
		SystemConfig.IsFXOn = !SystemConfig.IsFXOn;
		if (!SystemConfig.IsFXOn)
		{
			ActorFX[] componentsInChildren = FXPool.Instance.root.GetComponentsInChildren<ActorFX>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].get_gameObject().SetActive(false);
			}
		}
	}

	private void OnFun_UIFX(GameObject sender)
	{
		SystemConfig.IsUIFXOn = !SystemConfig.IsUIFXOn;
	}

	private void OnFun_PostProcess(GameObject sender)
	{
		SystemConfig.IsPostProcessOn = !SystemConfig.IsPostProcessOn;
		SystemConfig.IsShaderEffectOn = !SystemConfig.IsShaderEffectOn;
		if (!SystemConfig.IsPostProcessOn)
		{
			EventDispatcher.Broadcast(ShaderEffectEvent.PostProcessOff);
		}
	}

	private void OnFun_Scene(GameObject sender)
	{
		this.Find();
		this.IsShowScene = !this.IsShowScene;
		if (this.goSceneSystem != null)
		{
			this.goSceneSystem.SetActive(this.IsShowScene);
		}
	}

	private void OnFun_FXOfScene(GameObject sender)
	{
		this.Find();
		this.IsShowFXOfScene = !this.IsShowFXOfScene;
		if (this.goSceneFX != null)
		{
			this.goSceneFX.SetActive(this.IsShowFXOfScene);
		}
	}

	private void OnFun_Light(GameObject sender)
	{
		this.IsShowLight = !this.IsShowLight;
		Utils.EnableRoleLight(this.IsShowLight);
	}

	private void OnFun_UI(GameObject sender)
	{
		this.IsShowUI = !this.IsShowUI;
		UINodesManager.NoEventsUIRoot.get_gameObject().SetActive(this.IsShowUI);
		UINodesManager.NormalUIRoot.get_gameObject().SetActive(this.IsShowUI);
		UINodesManager.MiddleUIRoot.get_gameObject().SetActive(this.IsShowUI);
	}

	private void OnFun_UISystem(GameObject sender)
	{
		UINodesManager.UIRoot.get_gameObject().SetActive(false);
		UIManagerControl.Instance.FakeHideAllUI(true, 7);
	}

	private void OnFun_ActorModel(GameObject sender)
	{
		this.Find();
		this.IsShowModel = !this.IsShowModel;
		for (int i = 0; i < this.listModelOfAll.get_Count(); i++)
		{
			Renderer[] componentsInChildren = this.listModelOfAll.get_Item(i).GetComponentsInChildren<Renderer>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (!LayerSystem.IsSpecialEffectLayers(componentsInChildren[j].get_gameObject().get_layer()))
				{
					componentsInChildren[j].get_gameObject().SetActive(this.IsShowModel);
					componentsInChildren[j].set_enabled(this.IsShowModel);
				}
			}
			this.listModelOfAll.get_Item(i).SetActive(this.IsShowModel);
		}
	}

	private void OnFun_ReleaseResource(GameObject sender)
	{
		SystemConfig.IsReleaseResourceOn = !SystemConfig.IsReleaseResourceOn;
	}

	private void OnFun_Billboard(GameObject sender)
	{
		SystemConfig.IsBillboardOn = !SystemConfig.IsBillboardOn;
		BillboardManager.Instance.SwitchBillboards(SystemConfig.IsBillboardOn);
	}

	private void OnFun_ImageRes(GameObject sender)
	{
		SystemConfig.IsReadUIImageOn = !SystemConfig.IsReadUIImageOn;
	}

	private void OnFun_Frame(GameObject sender)
	{
		FPSManager.Instance.FPSLimitOff();
		FPSManager.Instance.vSyncOff();
	}

	private void OnFun_Effect(GameObject sender)
	{
		SystemConfig.IsEffectOn = !SystemConfig.IsEffectOn;
	}

	private void OnFun_ActorAnim(GameObject sender)
	{
		InstanceManager.IsActorAnimatorOn = !InstanceManager.IsActorAnimatorOn;
		this.Find();
		for (int i = 0; i < this.listModelOfNoBattle.get_Count(); i++)
		{
			Animator[] componentsInChildren = this.listModelOfNoBattle.get_Item(i).GetComponentsInChildren<Animator>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (!LayerSystem.IsSpecialEffectLayers(componentsInChildren[j].get_gameObject().get_layer()))
				{
					componentsInChildren[j].set_enabled(InstanceManager.IsActorAnimatorOn);
				}
			}
		}
	}

	private void OnFun_MainCamera(GameObject sender)
	{
		if (CamerasMgr.CameraMain != null)
		{
			CamerasMgr.CameraMain.set_enabled(!CamerasMgr.CameraMain.get_enabled());
		}
	}

	private void OnFun_UICamera(GameObject sender)
	{
		if (CamerasMgr.CameraUI != null)
		{
			CamerasMgr.CameraUI.set_enabled(!CamerasMgr.CameraUI.get_enabled());
		}
	}

	private void OnGM_Excute(GameObject sender)
	{
		string text = this.m_InputField.get_text();
		this.m_InputField.set_text(string.Empty);
		string gMResult = ClientGMManager.Instance.GetGMResult(text);
		this.SetOutput(gMResult);
	}

	private void OnGM_stopallai(GameObject sender)
	{
		ClientGMManager.Stopallai();
	}

	private void OnGM_KillAllMonsters(GameObject sender)
	{
		EntityWorld.Instance.KillAllMonsters();
	}

	private void OnGM_PassAllInstance(GameObject sender)
	{
		ChatManager.Instance.SendGMCommand(0, "#passall ");
	}

	private void OnGM_CameraOcclusion(GameObject sender)
	{
		CamerasMgr.CameraMain.set_useOcclusionCulling(!CamerasMgr.CameraMain.get_useOcclusionCulling());
		if (CamerasMgr.CameraMain.get_useOcclusionCulling())
		{
			base.FindTransform("GM_CameraOcclusion").GetComponentInChildren<Text>().set_text("主相机遮挡剔除\n状态:开");
		}
		else
		{
			base.FindTransform("GM_CameraOcclusion").GetComponentInChildren<Text>().set_text("主相机遮挡剔除\n状态:关");
		}
	}

	private void OnGM_AIPause(GameObject sender)
	{
		InstanceManager.PauseAllClientAI();
	}

	private void OnGM_AIResume(GameObject sender)
	{
		InstanceManager.ResumeAllClientAI();
	}

	private void OnGM_Fog(GameObject sender)
	{
		RenderSettings.set_fog(!RenderSettings.get_fog());
	}

	private void OnGM_BattleGMButton(GameObject sender)
	{
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("BattleUI");
		if (uIIfExist != null)
		{
			(uIIfExist as BattleUI).ShowButtonGM(true);
		}
	}

	private void OnProfile_MemoryOn(GameObject sender)
	{
		Singleton<ResourcesMemoryInfoFile>.S.Begin();
		Singleton<ResourcesMemoryTracker>.S.Sample();
		Singleton<ResourcesMemoryInfoFile>.S.End();
	}

	private void Find()
	{
		if (this.IsFound)
		{
			for (int i = 0; i < this.listModelOfAll.get_Count(); i++)
			{
				if (this.listModelOfAll.get_Item(i) == null)
				{
					this.IsFound = false;
					break;
				}
			}
		}
		if (this.IsFound)
		{
			for (int j = 0; j < this.listModelOfNoBattle.get_Count(); j++)
			{
				if (this.listModelOfNoBattle.get_Item(j) == null)
				{
					this.IsFound = false;
					break;
				}
			}
		}
		if (!this.IsFound)
		{
			this.IsFound = true;
			this.goSceneSystem = GameObject.Find("SceneSystem");
			this.goSceneFX = GameObject.Find("SceneFX");
			this.listModelOfAll.Clear();
			this.listModelOfNoBattle.Clear();
			if (NPCPool.Instance != null)
			{
				this.listModelOfAll.Add(NPCPool.Instance.root.get_gameObject());
				this.listModelOfNoBattle.Add(NPCPool.Instance.root.get_gameObject());
			}
			if (AvatarPool.Instance != null)
			{
				this.listModelOfAll.Add(AvatarPool.Instance.root.get_gameObject());
			}
			if (CityPlayerPool.Instance != null)
			{
				this.listModelOfAll.Add(CityPlayerPool.Instance.root.get_gameObject());
			}
			if (PlayerPool.Instance != null)
			{
				this.listModelOfAll.Add(PlayerPool.Instance.root.get_gameObject());
			}
			if (PetPool.Instance != null)
			{
				this.listModelOfAll.Add(PetPool.Instance.root.get_gameObject());
			}
			if (MonsterPool.Instance != null)
			{
				this.listModelOfAll.Add(MonsterPool.Instance.root.get_gameObject());
			}
			if (CityMonsterPool.Instance != null)
			{
				this.listModelOfAll.Add(CityMonsterPool.Instance.root.get_gameObject());
			}
		}
	}

	public void SetOutput(string output)
	{
		RectTransform rectTransform = this.m_lblOutputContent.get_transform() as RectTransform;
		this.output_anchoredPosition = rectTransform.get_anchoredPosition();
		this.m_lblOutputContent.set_text(output);
		float preferredHeight = this.m_lblOutputContent.get_preferredHeight();
		rectTransform.set_sizeDelta(new Vector2(rectTransform.get_sizeDelta().x, preferredHeight));
		if (preferredHeight > 270f)
		{
			rectTransform.set_anchoredPosition(new Vector2(rectTransform.get_anchoredPosition().x, preferredHeight - 135f));
		}
	}

	public static void PrintLogCache()
	{
		if (DebugUIView.Instance == null)
		{
			return;
		}
		if (!Debug.get_logger().get_logEnabled())
		{
			return;
		}
		DebugUIView.Instance.DoPrintLogCache();
	}

	private void DoPrintLogCache()
	{
		if (!Debug.get_logger().get_logEnabled())
		{
			return;
		}
		if (this.IsOutputLock)
		{
			return;
		}
		DebugUIView.LogString = string.Empty;
		LogUnit[] array = RemoteLogSender.Instance.LogCacheUnits.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			LogUnit logUnit = array[i];
			DebugUIView.AddToLogString(logUnit.logString, logUnit.stackTrace, logUnit.logType);
		}
		this.SetOutput(DebugUIView.LogString);
	}

	private static void AddToLogString(string logString, string stackTrace, LogType type)
	{
		switch (type)
		{
		case 0:
		case 4:
			logString = TextColorMgr.GetColor(logString, "FF0000", string.Empty);
			break;
		case 2:
			logString = TextColorMgr.GetColor(logString, "FFE200", string.Empty);
			break;
		}
		if (string.IsNullOrEmpty(DebugUIView.LogString))
		{
			DebugUIView.LogString += logString;
		}
		else
		{
			DebugUIView.LogString = DebugUIView.LogString + "\n" + logString;
		}
	}

	private void Update()
	{
		if (RemoteLogSender.IsLogCacheUnitsChanged)
		{
			RemoteLogSender.IsLogCacheUnitsChanged = false;
			DebugUIView.PrintLogCache();
		}
	}

	private void HideAllRegionButtons()
	{
		this.ButtonSROfFun.get_gameObject().SetActive(false);
		this.ButtonSROfFun2.get_gameObject().SetActive(false);
		this.ButtonSROfGM.get_gameObject().SetActive(false);
		this.ButtonSROfProfile.get_gameObject().SetActive(false);
	}
}
