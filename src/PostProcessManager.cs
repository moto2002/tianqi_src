using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PostProcessManager : TimerInterval
{
	private enum Weather
	{
		Rain = 1,
		Lightning,
		YingHua
	}

	public enum CameraAnimationEffect
	{
		Distortion = 1,
		MotionBlur
	}

	public const string SPLIT_KEY_VALUE = ":";

	public const string SPLIT_ELEMENT = ";";

	private static PostProcessManager m_instance;

	private Transform m_myTransform;

	private List<uint> m_weatherTimerIds = new List<uint>();

	private RenderManager m_RenderManager;

	private MobileBloom m_mobileBloom;

	private PP_RadialBlur m_PP_RadialBlur;

	private MobileBlurBlurry m_mobileBlurBlurry;

	private MobileBlurBlurryFade m_mobileBlurBlurryFade;

	private MobileBlurBlurryCulling m_MobileBlurBlurryCulling;

	private DepthOfField m_SRDepthOfField;

	private MobileBlurBlurry m_SRMobileBlurBlurry;

	private FullScreenGray m_fullScreenGray;

	private FrostEffect m_frostEffect;

	private FogEffect m_fogEffect;

	private WaterEffect m_waterEffect;

	private static bool IsRoleConversionUseTwoCameraOn = true;

	private PP_Holywood m_PP_Holywood;

	private PP_Vignette m_PP_Vignette;

	private PP_HolywoodOfRT m_PP_HolywoodOfRT;

	private PP_VignetteOfRT m_VignetteOfRT;

	private ScreenHeatDistortion m_ScreenHeatDistortion;

	private LocalHeatDistortion m_LocalHeatDistortion;

	private BloomOptimized m_BloomOptimized;

	public static PostProcessManager Instance
	{
		get
		{
			return PostProcessManager.m_instance;
		}
	}

	private void Awake()
	{
		this.m_myTransform = base.get_transform();
		PostProcessManager.m_instance = this;
		this.Initialize();
	}

	private void Initialize()
	{
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnLoadSceneEnd));
		EventDispatcher.AddListener<string>(LocalInstanceEvent.DirectlyRefreshBegin, new Callback<string>(this.ControlWeatherByRefreshMonsterEnd));
		EventDispatcher.AddListener<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, new Callback<bool>(this.EnableRoleConversion));
		EventDispatcher.AddListener<bool>(ShaderEffectEvent.PLAYER_DEAD, new Callback<bool>(this.EnableFullScreenGray));
		EventDispatcher.AddListener<bool, Vector3>(ShaderEffectEvent.PLAYER_ASSAULT, new Callback<bool, Vector3>(this.EnableMotionBlur));
		EventDispatcher.AddListener<int, bool>(ShaderEffectEvent.CAMERA_ANIMATION_EFFECT, new Callback<int, bool>(this.EnableCameraAnimationEffect));
		EventDispatcher.AddListener<bool, float, float>(ShaderEffectEvent.ENABLE_BG_BLUR, new Callback<bool, float, float>(this.EnableBGBlur));
		EventDispatcher.AddListener<bool>(ShaderEffectEvent.ENABLE_BG_BLUR_FADEIN, new Callback<bool>(this.EnableBGBlurFadeIn));
		EventDispatcher.AddListener<bool, float, float>(ShaderEffectEvent.ENABLE_CAMERA_ROTATE_BLUR, new Callback<bool, float, float>(this.EnableCameraRotateBlur));
		EventDispatcher.AddListener(ShaderEffectEvent.LODChanged, new Callback(this.LODChanged));
	}

	private void OnUnloadScene(int oldId, int newId)
	{
		this.DelWeatherTimerIds();
	}

	private void OnLoadSceneEnd(int sceneId)
	{
		this.ControlWeatherBySceneLoaded();
		this.SetCameraRotateBlur(sceneId);
		this.ResetPostProcess();
	}

	private void LODChanged()
	{
		this.ResetPostProcess();
	}

	private void ResetPostProcess()
	{
		if (MySceneManager.Instance.CurSceneID > 0)
		{
			this.EnableBloom(true);
		}
	}

	private void Update()
	{
		if (!Input.GetKey(304))
		{
			return;
		}
		if (Input.GetKeyDown(282))
		{
			LinkNavigationManager.OpenPetTaskUI();
		}
		if (Input.GetKeyDown(286))
		{
			ClientApp.Instance.ReInit();
		}
	}

	private void ControlWeatherBySceneLoaded()
	{
		Scene scene = DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID);
	}

	private void ControlWeatherByRefreshMonsterEnd(string args)
	{
		if (!string.IsNullOrEmpty(args))
		{
			this.ControlWeather(args);
		}
	}

	private void ControlWeather(string args)
	{
		string[] array = args.Split(";".ToCharArray());
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(":".ToCharArray());
			int weatherId = int.Parse(array2[0]);
			int num = int.Parse(array2[1]);
			string arg3 = string.Empty;
			if (array2.Length >= 3)
			{
				arg3 = array2[2];
			}
			if (num > 0)
			{
				this.m_weatherTimerIds.Add(TimerHeap.AddTimer((uint)(num * 1000), 0, delegate
				{
					this.ControlWeather(weatherId, arg3);
				}));
			}
			else
			{
				this.ControlWeather(weatherId, arg3);
			}
		}
	}

	private void ControlWeather(int weatherId, string arg3)
	{
		switch (weatherId)
		{
		case 1:
			this.EnableRain(true);
			break;
		case 2:
			if (!string.IsNullOrEmpty(arg3))
			{
				float fLightningRandom = float.Parse(arg3);
				this.EnableLightning(true, fLightningRandom);
			}
			else
			{
				this.EnableLightning(true, 10f);
			}
			break;
		case 3:
			this.EnableYingHua(true);
			break;
		}
	}

	private void DelWeatherTimerIds()
	{
		for (int i = 0; i < this.m_weatherTimerIds.get_Count(); i++)
		{
			TimerHeap.DelTimer(this.m_weatherTimerIds.get_Item(i));
		}
		this.m_weatherTimerIds.Clear();
	}

	private void EnableYingHua(bool bEnable)
	{
		GameObject gameObject = GameObject.Find("YingHua");
		if (gameObject != null)
		{
			Transform transform = gameObject.get_transform().FindChild("yinghua");
			if (transform != null)
			{
				transform.get_gameObject().SetActive(bEnable);
			}
		}
	}

	private void EnableCameraAnimationEffect(int effectId, bool bEnable)
	{
		if (effectId != 1)
		{
			if (effectId == 2)
			{
				if (EntityWorld.Instance.EntSelf.AITarget != null && EntityWorld.Instance.EntSelf.AITarget.Actor)
				{
					this.EnableMotionBlur(bEnable, EntityWorld.Instance.EntSelf.AITarget.Actor.FixTransform.get_position());
				}
			}
		}
		else
		{
			this.EnableScreenHeatDistortion(bEnable);
		}
	}

	public void EnableRenderManager(bool bEnable)
	{
		if (bEnable)
		{
			if (CamerasMgr.MainCameraRoot == null)
			{
				return;
			}
			this.m_RenderManager = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<RenderManager>();
			this.m_RenderManager.set_enabled(bEnable);
			this.m_RenderManager.Init();
		}
		else if (this.m_RenderManager != null)
		{
			this.m_RenderManager.set_enabled(false);
		}
	}

	private void EnableRain(bool bEnable)
	{
		if (GameLevelManager.IsPostProcessReachQuality(250) && RainEffectManager.Instance != null)
		{
			RainEffectManager.Instance.EnableRain(bEnable);
		}
	}

	private void EnableLightning(bool isOn, float fLightningRandom = 10f)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		int lod = 250;
		if (Application.get_platform() == 11)
		{
			lod = 300;
		}
		if (isOn && GameLevelManager.IsPostProcessReachQuality(lod))
		{
			this.m_mobileBloom = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<MobileBloom>();
			this.m_mobileBloom.set_enabled(true);
			this.m_mobileBloom.m_fLightningRandom = fLightningRandom;
			this.m_mobileBloom.Initialization(MobileBloom.BloomType.Lightning);
		}
		else if (this.m_mobileBloom != null)
		{
			this.m_mobileBloom.set_enabled(false);
		}
	}

	private void EnableMotionBlur(bool bEnable, Vector3 pos)
	{
		this.EnableMotionBlurSimple(bEnable, pos);
	}

	private void EnableMotionBlurSimple(bool isOn, Vector3 pos)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		if (SystemInfo.get_deviceModel().Contains("M3"))
		{
			return;
		}
		if (isOn && SystemConfig.PP_MotionBlurOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (this.m_PP_RadialBlur == null)
				{
					this.m_PP_RadialBlur = CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<PP_RadialBlur>();
				}
				this.m_PP_RadialBlur.set_enabled(isOn);
				this.m_PP_RadialBlur.Initialization(pos);
			}
		}
		else if (this.m_PP_RadialBlur != null)
		{
			this.m_PP_RadialBlur.set_enabled(false);
		}
	}

	private void EnableBGBlur(bool bEnable, float vecDistanceX, float vecDistanceY)
	{
		this.EnableBlurBlurry(bEnable);
	}

	private void EnableBlurBlurry(bool bEnable)
	{
		if (bEnable)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_mobileBlurBlurry = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<MobileBlurBlurry>();
				this.m_mobileBlurBlurry.set_enabled(bEnable);
				this.m_mobileBlurBlurry.Initialization();
			}
		}
		else if (this.m_mobileBlurBlurry != null)
		{
			this.m_mobileBlurBlurry.set_enabled(false);
		}
	}

	private void EnableBGBlurFadeIn(bool bEnable)
	{
		if (bEnable)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_mobileBlurBlurryFade = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<MobileBlurBlurryFade>();
				this.m_mobileBlurBlurryFade.set_enabled(bEnable);
				this.m_mobileBlurBlurryFade.Initialization();
			}
		}
		else if (this.m_mobileBlurBlurryFade != null)
		{
			this.m_mobileBlurBlurryFade.set_enabled(false);
		}
	}

	private void SetCameraRotateBlur(int sceneId)
	{
		if (MySceneManager.IsMainScene(sceneId) && this.m_MobileBlurBlurryCulling != null)
		{
			this.m_MobileBlurBlurryCulling.set_enabled(false);
		}
	}

	private void EnableCameraRotateBlur(bool isOn, float angle = 0f, float disCamera2lookpoint = 0f)
	{
		float num = MobileBlurBlurryCulling.CalAngleEffect(angle, disCamera2lookpoint);
		if (num >= MobileBlurBlurryCulling.AngleCameraBlurThreshold)
		{
			this.EnableCameraRotateBlurCulling(true, num);
		}
		else
		{
			this.EnableCameraRotateBlurCulling(false, num);
		}
	}

	private void EnableCameraRotateBlurCulling(bool isOn, float angleEffect)
	{
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				if (this.m_MobileBlurBlurryCulling == null)
				{
					this.m_MobileBlurBlurryCulling = CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<MobileBlurBlurryCulling>();
				}
				this.m_MobileBlurBlurryCulling.Initialization();
				this.m_MobileBlurBlurryCulling.set_enabled(true);
				this.m_MobileBlurBlurryCulling.BlurOnOff(true, angleEffect);
			}
		}
		else if (this.m_MobileBlurBlurryCulling != null)
		{
			this.m_MobileBlurBlurryCulling.BlurOnOff(false, angleEffect);
		}
	}

	public void EnablePostProcessToSelectRole(bool isOn)
	{
		if (isOn)
		{
			if (this.EnableSRDepthOfField(true))
			{
				PPToSelectRoleSetting.SelectRoleSettingOn();
				return;
			}
			if (this.EnableSRBlurBlurry(true))
			{
				PPToSelectRoleSetting.SelectRoleSettingOn();
				return;
			}
		}
		else
		{
			PPToSelectRoleSetting.SelectRoleSettingOff();
			this.EnableSRDepthOfField(false);
			this.EnableSRBlurBlurry(false);
		}
	}

	private bool EnableSRDepthOfField(bool isOn)
	{
		return !GameLevelManager.IsPostProcessReachQuality(1000) && false;
	}

	private bool EnableSRBlurBlurry(bool isOn)
	{
		if (isOn)
		{
			if (CamerasMgr.MainCameraRoot == null)
			{
				return false;
			}
			this.m_SRMobileBlurBlurry = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<MobileBlurBlurry>();
			this.m_SRMobileBlurBlurry.set_enabled(isOn);
			this.m_SRMobileBlurBlurry.Initialization();
		}
		else if (this.m_SRMobileBlurBlurry != null)
		{
			this.m_SRMobileBlurBlurry.set_enabled(false);
		}
		return true;
	}

	private void EnableFullScreenGray(bool isOn)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				this.m_fullScreenGray = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<FullScreenGray>();
				this.m_fullScreenGray.set_enabled(true);
				this.m_fullScreenGray.Initialization();
			}
		}
		else if (this.m_fullScreenGray != null)
		{
			this.m_fullScreenGray.set_enabled(false);
		}
	}

	private void DisableScreenEffect()
	{
		this.EnableScreenFrost(false, false);
		this.EnableScreenFog(false, false);
		this.EnableScreenWater(false, false);
	}

	private void EnableScreenFrost(bool isOn, bool bDisableFirst = true)
	{
		if (bDisableFirst)
		{
			this.DisableScreenEffect();
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(300))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_frostEffect = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<FrostEffect>();
				this.m_frostEffect.Initialization();
				this.m_frostEffect.set_enabled(true);
			}
		}
		else if (this.m_frostEffect != null)
		{
			this.m_frostEffect.set_enabled(false);
		}
	}

	private void EnableScreenFog(bool isOn, bool bDisableFirst = true)
	{
		if (bDisableFirst)
		{
			this.DisableScreenEffect();
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(300))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_fogEffect = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<FogEffect>();
				this.m_fogEffect.Initialization();
				this.m_fogEffect.set_enabled(true);
			}
		}
		else if (this.m_fogEffect != null)
		{
			this.m_fogEffect.set_enabled(false);
		}
	}

	private void EnableScreenWater(bool isOn, bool bDisableFirst = true)
	{
		if (bDisableFirst)
		{
			this.DisableScreenEffect();
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(300))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_waterEffect = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<WaterEffect>();
				this.m_waterEffect.Initialization();
				this.m_waterEffect.set_enabled(true);
			}
		}
		else if (this.m_waterEffect != null)
		{
			this.m_waterEffect.set_enabled(false);
		}
	}

	private bool IsRoleConversionOn()
	{
		return InstanceManager.CurrentInstanceType != InstanceType.Experience;
	}

	private void EnableRoleConversion(bool isOn)
	{
		if (!this.IsRoleConversionOn())
		{
			return;
		}
		if (PostProcessManager.IsRoleConversionUseTwoCameraOn)
		{
			CamerasMgr.SetCameraToStoryboard(isOn);
			this.EnableHolywood(isOn);
			this.EnableVignette(isOn);
		}
		else
		{
			this.EnableHolywoodOfRT(isOn);
			this.EnableVignetteOfRT(isOn);
		}
	}

	private void EnableHolywood(bool isOn)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (this.m_PP_Holywood == null)
				{
					this.m_PP_Holywood = CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<PP_Holywood>();
				}
				this.m_PP_Holywood.set_enabled(isOn);
				this.m_PP_Holywood.Initialization();
			}
		}
		else if (this.m_PP_Holywood != null)
		{
			this.m_PP_Holywood.set_enabled(false);
		}
	}

	private void EnableVignette(bool isOn)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (this.m_PP_Vignette == null)
				{
					this.m_PP_Vignette = CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<PP_Vignette>();
				}
				this.m_PP_Vignette.set_enabled(isOn);
				this.m_PP_Vignette.Initialization();
			}
		}
		else if (this.m_PP_Vignette != null)
		{
			this.m_PP_Vignette.set_enabled(false);
		}
	}

	private void EnableHolywoodOfRT(bool bEnable)
	{
		if (bEnable)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_PP_HolywoodOfRT = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<PP_HolywoodOfRT>();
				this.m_PP_HolywoodOfRT.set_enabled(bEnable);
				this.m_PP_HolywoodOfRT.Initialization();
			}
		}
		else if (this.m_PP_HolywoodOfRT != null)
		{
			this.m_PP_HolywoodOfRT.set_enabled(false);
		}
	}

	private void EnableVignetteOfRT(bool bEnable)
	{
		if (bEnable)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_VignetteOfRT = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<PP_VignetteOfRT>();
				this.m_VignetteOfRT.set_enabled(bEnable);
				this.m_VignetteOfRT.Initialization();
			}
		}
		else if (this.m_VignetteOfRT != null)
		{
			this.m_VignetteOfRT.set_enabled(false);
		}
	}

	private void EnableScreenHeatDistortion(bool isOn)
	{
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				this.m_ScreenHeatDistortion = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<ScreenHeatDistortion>();
				this.m_ScreenHeatDistortion.set_enabled(true);
				this.m_ScreenHeatDistortion.Initialization();
			}
		}
		else if (this.m_ScreenHeatDistortion != null)
		{
			this.m_ScreenHeatDistortion.set_enabled(false);
		}
	}

	public void EnableLocalHeatDistortion(bool isOn)
	{
		if (isOn)
		{
			if (GameLevelManager.IsPostProcessReachQuality(250))
			{
				if (CamerasMgr.MainCameraRoot == null)
				{
					return;
				}
				EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.LocalHeatDistortion);
				if (this.m_LocalHeatDistortion == null)
				{
					this.m_LocalHeatDistortion = CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<LocalHeatDistortion>();
					this.m_LocalHeatDistortion.Initialization();
					this.m_LocalHeatDistortion.set_enabled(isOn);
				}
			}
		}
		else
		{
			EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.LocalHeatDistortion);
			if (this.m_LocalHeatDistortion != null)
			{
				this.m_LocalHeatDistortion.set_enabled(false);
			}
		}
	}

	private void EnableBloom(bool isOn)
	{
		if (CamerasMgr.MainCameraRoot == null)
		{
			return;
		}
		int lod = 250;
		if (Application.get_platform() == 11)
		{
			lod = 300;
		}
		else if (Application.get_platform() == 8)
		{
			lod = 300;
		}
		if (isOn && GameLevelManager.IsPostProcessReachQuality(lod) && SystemConfig.PP_BloomOn)
		{
			if (this.m_BloomOptimized == null)
			{
				this.m_BloomOptimized = CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<BloomOptimized>();
			}
			this.m_BloomOptimized.set_enabled(isOn);
			this.m_BloomOptimized.Initialization();
		}
		else if (this.m_BloomOptimized != null)
		{
			this.m_BloomOptimized.set_enabled(false);
		}
	}
}
