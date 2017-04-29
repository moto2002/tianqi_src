using System;
using UnityEngine;

public class CamerasMgr
{
	public const float FIELD_OF_VIEW = 50f;

	private static Camera m_cameraUI;

	public static Transform MainCameraRoot;

	public static Camera CameraMain;

	private static Camera _Camera2Storyboard;

	private static Camera _Camera2RTCommon;

	private static string[] battle_fx_layerss = new string[]
	{
		"CameraRange"
	};

	private static Camera _Camera2BattleFX;

	private static string[] battle_spine_layerss = new string[]
	{
		"UI"
	};

	public static Transform _canvasOfBattleSpine;

	public static Camera CameraOfBattleSpine;

	private static float RT_fieldOfView = 0f;

	private static float RT_ClippingPlanesFar = 0f;

	public static Camera CameraUI
	{
		get
		{
			if (CamerasMgr.m_cameraUI == null && UINodesManager.UIRoot != null)
			{
				Transform transform = UINodesManager.UIRoot.FindChild("UICamera");
				if (transform != null)
				{
					CamerasMgr.m_cameraUI = transform.GetComponent<Camera>();
					CamerasMgr.m_cameraUI.set_useOcclusionCulling(false);
				}
			}
			return CamerasMgr.m_cameraUI;
		}
	}

	public static Camera Camera2Storyboard
	{
		get
		{
			if (CamerasMgr._Camera2Storyboard == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("Camera2Storyboard");
				gameObject.get_transform().set_parent(CamerasMgr.MainCameraRoot.get_transform());
				gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
				gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				gameObject.get_transform().set_localRotation(Quaternion.get_identity());
				CamerasMgr._Camera2Storyboard = gameObject.AddComponent<Camera>();
				CamerasMgr._Camera2Storyboard.set_enabled(false);
			}
			return CamerasMgr._Camera2Storyboard;
		}
	}

	public static Camera Camera2RTCommon
	{
		get
		{
			if (CamerasMgr._Camera2RTCommon == null && CamerasMgr.MainCameraRoot != null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("Camera2RTCommon");
				if (CamerasMgr.MainCameraRoot != null)
				{
					gameObject.get_transform().set_parent(CamerasMgr.MainCameraRoot.get_transform());
				}
				else
				{
					Object.DontDestroyOnLoad(gameObject);
				}
				gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
				gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				gameObject.get_transform().set_localRotation(Quaternion.get_identity());
				CamerasMgr._Camera2RTCommon = gameObject.AddComponent<Camera>();
				CamerasMgr._Camera2RTCommon.set_cullingMask(0);
				CamerasMgr._Camera2RTCommon.set_enabled(false);
			}
			return CamerasMgr._Camera2RTCommon;
		}
	}

	public static Camera Camera2BattleFX
	{
		get
		{
			if (CamerasMgr._Camera2BattleFX == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("Camera2BattleFXScreen");
				gameObject.get_transform().set_parent(CamerasMgr.MainCameraRoot.get_transform());
				gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
				gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				gameObject.get_transform().set_localRotation(Quaternion.get_identity());
				CamerasMgr._Camera2BattleFX = gameObject.AddComponent<Camera>();
				CamerasMgr._Camera2BattleFX.set_cullingMask(LayerSystem.GetMask(CamerasMgr.battle_fx_layerss));
				CamerasMgr._Camera2BattleFX.set_clearFlags(3);
				CamerasMgr._Camera2BattleFX.set_orthographic(false);
				CamerasMgr._Camera2BattleFX.set_farClipPlane(CamerasMgr.CameraMain.get_farClipPlane());
				CamerasMgr._Camera2BattleFX.set_nearClipPlane(CamerasMgr.CameraMain.get_nearClipPlane());
				CamerasMgr._Camera2BattleFX.set_fieldOfView(50f);
				CamerasMgr._Camera2BattleFX.set_depth(CamerasMgr.CameraMain.get_depth() + 1f);
				CamerasMgr._Camera2BattleFX.set_useOcclusionCulling(false);
				CamerasMgr._Camera2BattleFX.set_enabled(true);
			}
			return CamerasMgr._Camera2BattleFX;
		}
	}

	public static void InitMainCamera()
	{
		if (CamerasMgr.MainCameraRoot != null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject == null)
		{
			return;
		}
		CamerasMgr.MainCameraRoot = gameObject.get_transform();
		Object.DontDestroyOnLoad(CamerasMgr.MainCameraRoot);
		if (CamerasMgr.MainCameraRoot != null)
		{
			CamerasMgr.CameraMain = CamerasMgr.MainCameraRoot.GetComponent<Camera>();
			CamerasMgr.CameraMain.set_useOcclusionCulling(false);
			CamerasMgr.CameraMain.set_backgroundColor(Color.get_black());
		}
	}

	public static bool IsCamera2RTCommonTargetNull()
	{
		return CamerasMgr._Camera2RTCommon != null && CamerasMgr._Camera2RTCommon.get_targetTexture() == null;
	}

	public static void CreateCameraOfBattleSpine()
	{
		if (CamerasMgr.CameraOfBattleSpine != null && CamerasMgr.CameraOfBattleSpine.get_transform() != null)
		{
			CamerasMgr.CameraOfBattleSpine.get_gameObject().SetActive(true);
		}
		else
		{
			GameObject gameObject = new GameObject();
			gameObject.set_name("Camera2BattleSpineScreen");
			gameObject.get_transform().set_parent(null);
			CamerasMgr.CameraOfBattleSpine = gameObject.AddComponent<Camera>();
			CamerasMgr.CameraOfBattleSpine.set_clearFlags(1);
			CamerasMgr.CameraOfBattleSpine.set_cullingMask(LayerSystem.GetMask(CamerasMgr.battle_spine_layerss));
			CamerasMgr.CameraOfBattleSpine.set_depth(CamerasMgr.CameraMain.get_depth() - 1f);
			GameObject gameObject2 = AssetManager.AssetOfNoPool.LoadAssetNowNoAB("UGUI/Prefabs/GlobalUI/UICanvasNoEvents", typeof(Object)) as GameObject;
			if (gameObject2 == null)
			{
				return;
			}
			CamerasMgr._canvasOfBattleSpine = UGUITools.AddChild(CamerasMgr.CameraOfBattleSpine.get_gameObject(), gameObject2, false, "_canvasOfBattleSpine").get_transform();
			Canvas component = CamerasMgr._canvasOfBattleSpine.GetComponent<Canvas>();
			if (component != null)
			{
				component.set_worldCamera(CamerasMgr.CameraOfBattleSpine);
				component.get_gameObject().SetActive(true);
				component.set_planeDistance(100f);
				component.set_sortingOrder(1000);
			}
		}
	}

	public static void EnableCamera2Main(bool bEnable)
	{
		if (CamerasMgr.CameraMain != null)
		{
			CamerasMgr.CameraMain.set_enabled(bEnable);
		}
	}

	public static void EnableRTC(bool bEnable)
	{
		if (CamerasMgr.Camera2RTCommon != null)
		{
			CamerasMgr.Camera2RTCommon.set_enabled(bEnable);
			if (bEnable)
			{
				CamerasMgr.ResetRTC();
			}
			else
			{
				RTManager.Instance.RTCommon.Release();
			}
		}
	}

	public static void SetRTCCullingMask(int mask)
	{
		if (CamerasMgr._Camera2RTCommon == null)
		{
			CamerasMgr._Camera2RTCommon = CamerasMgr.Camera2RTCommon;
		}
		if (CamerasMgr._Camera2RTCommon != null)
		{
			CamerasMgr._Camera2RTCommon.set_cullingMask(mask);
		}
	}

	private static void ResetRTC()
	{
		if (CamerasMgr.Camera2RTCommon != null && CamerasMgr.CameraMain != null)
		{
			CamerasMgr.Camera2RTCommon.set_clearFlags(2);
			CamerasMgr.Camera2RTCommon.set_backgroundColor(Vector4.get_zero());
			CamerasMgr.Camera2RTCommon.set_orthographic(false);
			CamerasMgr.Camera2RTCommon.set_farClipPlane((CamerasMgr.RT_ClippingPlanesFar <= 0f) ? CamerasMgr.CameraMain.get_farClipPlane() : CamerasMgr.RT_ClippingPlanesFar);
			CamerasMgr.Camera2RTCommon.set_nearClipPlane(CamerasMgr.CameraMain.get_nearClipPlane());
			CamerasMgr.Camera2RTCommon.set_fieldOfView((CamerasMgr.RT_fieldOfView <= 0f) ? CamerasMgr.CameraMain.get_fieldOfView() : CamerasMgr.RT_fieldOfView);
			CamerasMgr.Camera2RTCommon.set_depth(CamerasMgr.CameraMain.get_depth() - 1f);
			CamerasMgr.Camera2RTCommon.set_useOcclusionCulling(false);
		}
	}

	public static void SetRTCFieldOfView(float fieldOfView = 0f)
	{
		CamerasMgr.RT_fieldOfView = fieldOfView;
		if (CamerasMgr.Camera2RTCommon != null && CamerasMgr.CameraMain != null)
		{
			CamerasMgr.Camera2RTCommon.set_fieldOfView((CamerasMgr.RT_fieldOfView <= 0f) ? CamerasMgr.CameraMain.get_fieldOfView() : CamerasMgr.RT_fieldOfView);
		}
	}

	public static void SetRTCClippingPlanes(float far)
	{
		CamerasMgr.RT_ClippingPlanesFar = far;
		if (CamerasMgr.Camera2RTCommon != null && CamerasMgr.CameraMain != null)
		{
			CamerasMgr.Camera2RTCommon.set_farClipPlane((CamerasMgr.RT_ClippingPlanesFar <= 0f) ? CamerasMgr.CameraMain.get_farClipPlane() : CamerasMgr.RT_ClippingPlanesFar);
		}
	}

	private static void SetCameraStoryboard(Camera camera)
	{
		if (camera != null)
		{
			camera.set_clearFlags(3);
			camera.set_orthographic(false);
			camera.set_farClipPlane(CamerasMgr.CameraMain.get_farClipPlane());
			camera.set_nearClipPlane(CamerasMgr.CameraMain.get_nearClipPlane());
			camera.set_fieldOfView(CamerasMgr.CameraMain.get_fieldOfView());
			camera.set_depth(CamerasMgr.CameraMain.get_depth() + 1f);
			camera.set_useOcclusionCulling(false);
		}
	}

	public static void SetCameraToStoryboard(bool bStoryboard)
	{
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.CAMERA_IN_STORY, bStoryboard);
		if (bStoryboard)
		{
			UINodesManager.SetUICanvas3D(CamerasMgr.Camera2Storyboard);
			CamerasMgr.SetCameraStoryboard(CamerasMgr.Camera2Storyboard);
			CamerasMgr.Camera2Storyboard.set_enabled(true);
			Utils.SetCameraCullingMask(CamerasMgr._Camera2Storyboard, 4);
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 3);
		}
		else
		{
			UINodesManager.SetUICanvas3D(CamerasMgr.CameraMain);
			if (CamerasMgr._Camera2Storyboard != null)
			{
				CamerasMgr._Camera2Storyboard.set_enabled(false);
			}
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
		}
	}

	public static void OpenEye()
	{
		if (CamerasMgr.CameraMain != null)
		{
			CamerasMgr.CameraMain.get_gameObject().AddMissingComponent<HeightDepthofField>();
		}
	}
}
