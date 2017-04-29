using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XEngine.AssetLoader;

public class UINodesManager
{
	private static Transform m_NoEventsUIRoot;

	private static Transform m_NormalUIRoot;

	private static Transform m_MiddleUIRoot;

	private static Transform m_TopUIRoot;

	private static Transform m_T2Root;

	private static Transform m_T3Root;

	private static Transform m_T4Root;

	private static Canvas cs_NoEventsUIRoot;

	private static Canvas cs_NormalUIRoot;

	private static Canvas cs_MiddleUIRoot;

	private static Canvas cs_TopUIRoot;

	private static Canvas cs_T2RootOfSpecial;

	private static Canvas cs_T3RootOfSpecial;

	private static Canvas cs_T4RootOfSpecial;

	private static Transform m_UIRoot;

	private static EventSystem m_UIEventSystem;

	private static Transform m_UICanvas3D;

	private static CanvasScaler.ScreenMatchMode UIScreenMatchMode = 1;

	private static bool NoEventsUIRoot_MainCamera = true;

	private static bool NoEventsUIRoot_IsShow = true;

	public static Transform NoEventsUIRoot
	{
		get
		{
			return UINodesManager.m_NoEventsUIRoot;
		}
	}

	public static Transform NormalUIRoot
	{
		get
		{
			if (UINodesManager.m_NormalUIRoot == null && UINodesManager.UIRoot != null)
			{
				UINodesManager.m_NormalUIRoot = UINodesManager.UIRoot.FindChild("UICanvas");
			}
			return UINodesManager.m_NormalUIRoot;
		}
	}

	public static Transform MiddleUIRoot
	{
		get
		{
			return UINodesManager.m_MiddleUIRoot;
		}
	}

	public static Transform TopUIRoot
	{
		get
		{
			return UINodesManager.m_TopUIRoot;
		}
	}

	public static Transform T2RootOfSpecial
	{
		get
		{
			return UINodesManager.m_T2Root;
		}
	}

	public static Transform T3RootOfSpecial
	{
		get
		{
			return UINodesManager.m_T3Root;
		}
	}

	public static Transform T4RootOfSpecial
	{
		get
		{
			return UINodesManager.m_T4Root;
		}
	}

	public static Transform UIRoot
	{
		get
		{
			if (UINodesManager.m_UIRoot == null)
			{
				GameObject gameObject = GameObject.Find("UGUI Root");
				if (gameObject != null)
				{
					UINodesManager.m_UIRoot = gameObject.get_transform();
				}
			}
			return UINodesManager.m_UIRoot;
		}
	}

	public static EventSystem UIEventSystem
	{
		get
		{
			if (UINodesManager.m_UIEventSystem == null && UINodesManager.UIRoot != null)
			{
				UINodesManager.m_UIEventSystem = UINodesManager.UIRoot.FindChild("EventSystem").GetComponent<EventSystem>();
			}
			return UINodesManager.m_UIEventSystem;
		}
	}

	public static Transform UICanvas3D
	{
		get
		{
			if (UINodesManager.m_UICanvas3D == null && CamerasMgr.MainCameraRoot != null)
			{
				UINodesManager.m_UICanvas3D = CamerasMgr.MainCameraRoot.FindChild("UICanvas3D");
			}
			return UINodesManager.m_UICanvas3D;
		}
	}

	public static bool Is2DCanvasRoot(Transform target)
	{
		return target == UINodesManager.NoEventsUIRoot || target == UINodesManager.NormalUIRoot || target == UINodesManager.MiddleUIRoot || target == UINodesManager.TopUIRoot || target == UINodesManager.T2RootOfSpecial || target == UINodesManager.T3RootOfSpecial || target == UINodesManager.T4RootOfSpecial;
	}

	public static void InitUICanvas()
	{
		if (UINodesManager.UIRoot == null)
		{
			return;
		}
		if (UINodesManager.m_NoEventsUIRoot != null)
		{
			return;
		}
		GameObject gameObject = AssetLoader.LoadAssetNow("Reserved/UICanvasNoEvents", typeof(Object)) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("prefabNoEvents初始化失败");
			return;
		}
		UINodesManager.m_NoEventsUIRoot = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject, false, "UICanvasNoEvents").get_transform();
		UINodesManager.m_NoEventsUIRoot.SetSiblingIndex(UINodesManager.NormalUIRoot.GetSiblingIndex());
		GameObject gameObject2 = AssetLoader.LoadAssetNow("Reserved/UICanvasEvents", typeof(Object)) as GameObject;
		if (gameObject2 == null)
		{
			Debug.LogError("prefabEvents初始化失败");
			return;
		}
		UINodesManager.m_MiddleUIRoot = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject2, false, "UICanvasMiddle").get_transform();
		UINodesManager.m_TopUIRoot = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject2, false, "UICanvasTop").get_transform();
		UINodesManager.m_T2Root = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject2, false, "UICanvasT2OfSpeical").get_transform();
		UINodesManager.m_T3Root = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject2, false, "UICanvasT3OfSpecial").get_transform();
		UINodesManager.m_T4Root = UGUITools.AddChild(UINodesManager.UIRoot.get_gameObject(), gameObject2, false, "UICanvasT4OfSpecial").get_transform();
		UINodesManager.SetUICanvas2Ds();
		UINodesManager.SetUICanvas3D(CamerasMgr.CameraMain);
		UINodesManager.Show3DUI(true);
		UINodesManager.SetUICamera();
	}

	private static void SetUICanvas2Ds()
	{
		UINodesManager.cs_NoEventsUIRoot = UINodesManager.NoEventsUIRoot.GetComponent<Canvas>();
		if (UINodesManager.cs_NoEventsUIRoot != null)
		{
			UINodesManager.cs_NoEventsUIRoot.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_NoEventsUIRoot.get_gameObject().SetActive(true);
			UINodesManager.cs_NoEventsUIRoot.set_planeDistance(100f);
			UINodesManager.cs_NoEventsUIRoot.set_sortingOrder(1000);
		}
		CanvasScaler component = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component != null)
		{
			component.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_NormalUIRoot = UINodesManager.NormalUIRoot.GetComponent<Canvas>();
		if (UINodesManager.cs_NormalUIRoot != null)
		{
			UINodesManager.cs_NormalUIRoot.get_gameObject().SetActive(true);
			UINodesManager.cs_NormalUIRoot.set_planeDistance(200f);
			UINodesManager.cs_NormalUIRoot.set_sortingOrder(2000);
		}
		CanvasScaler component2 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component2 != null)
		{
			component2.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_MiddleUIRoot = UINodesManager.MiddleUIRoot.GetComponent<Canvas>();
		if (UINodesManager.cs_MiddleUIRoot != null)
		{
			UINodesManager.cs_MiddleUIRoot.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_MiddleUIRoot.get_gameObject().SetActive(true);
			UINodesManager.cs_MiddleUIRoot.set_planeDistance(300f);
			UINodesManager.cs_MiddleUIRoot.set_sortingOrder(3000);
		}
		CanvasScaler component3 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component3 != null)
		{
			component3.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_TopUIRoot = UINodesManager.TopUIRoot.GetComponent<Canvas>();
		if (UINodesManager.cs_TopUIRoot != null)
		{
			UINodesManager.cs_TopUIRoot.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_TopUIRoot.get_gameObject().SetActive(true);
			UINodesManager.cs_TopUIRoot.set_planeDistance(400f);
			UINodesManager.cs_TopUIRoot.set_sortingOrder(14000);
		}
		CanvasScaler component4 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component4 != null)
		{
			component4.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_T2RootOfSpecial = UINodesManager.T2RootOfSpecial.GetComponent<Canvas>();
		if (UINodesManager.cs_T2RootOfSpecial != null)
		{
			UINodesManager.cs_T2RootOfSpecial.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_T2RootOfSpecial.get_gameObject().SetActive(true);
			UINodesManager.cs_T2RootOfSpecial.set_planeDistance(500f);
			UINodesManager.cs_T2RootOfSpecial.set_sortingOrder(15000);
		}
		CanvasScaler component5 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component5 != null)
		{
			component5.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_T3RootOfSpecial = UINodesManager.T3RootOfSpecial.GetComponent<Canvas>();
		if (UINodesManager.cs_T3RootOfSpecial != null)
		{
			UINodesManager.cs_T3RootOfSpecial.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_T3RootOfSpecial.get_gameObject().SetActive(true);
			UINodesManager.cs_T3RootOfSpecial.set_planeDistance(600f);
			UINodesManager.cs_T3RootOfSpecial.set_sortingOrder(16000);
		}
		CanvasScaler component6 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component6 != null)
		{
			component6.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
		UINodesManager.cs_T4RootOfSpecial = UINodesManager.T4RootOfSpecial.GetComponent<Canvas>();
		if (UINodesManager.cs_T4RootOfSpecial != null)
		{
			UINodesManager.cs_T4RootOfSpecial.set_worldCamera(CamerasMgr.CameraUI);
			UINodesManager.cs_T4RootOfSpecial.get_gameObject().SetActive(true);
			UINodesManager.cs_T4RootOfSpecial.set_planeDistance(600f);
			UINodesManager.cs_T4RootOfSpecial.set_sortingOrder(17000);
		}
		CanvasScaler component7 = UINodesManager.NoEventsUIRoot.GetComponent<CanvasScaler>();
		if (component7 != null)
		{
			component7.set_screenMatchMode(UINodesManager.UIScreenMatchMode);
		}
	}

	public static void SetUICanvas3D(Camera cameraOfRender)
	{
		if (UINodesManager.UICanvas3D != null)
		{
			Canvas component = UINodesManager.UICanvas3D.GetComponent<Canvas>();
			component.set_worldCamera(cameraOfRender);
			component.get_gameObject().SetActive(true);
			component.set_planeDistance(5f);
			component.set_sortingOrder(10);
			component.set_renderMode(1);
		}
	}

	private static void SetUICamera()
	{
		float num = (float)Screen.get_width() / UIConst.UI_SIZE_WIDTH;
		float num2 = (float)Screen.get_height() / UIConst.UI_SIZE_HEIGHT;
		UIUtils.UIScaleFactor = Mathf.Min(num, num2);
		CamerasMgr.CameraUI.set_orthographicSize(1f);
		CamerasMgr.CameraUI.set_nearClipPlane(-2000f);
		CamerasMgr.CameraUI.set_farClipPlane(2000f);
	}

	public static bool CheckParentIsCanvas(Transform parent)
	{
		return parent == UINodesManager.NormalUIRoot || parent == UINodesManager.MiddleUIRoot || parent == UINodesManager.TopUIRoot || parent == UINodesManager.T2RootOfSpecial;
	}

	public static void Show3DUI(bool isShow)
	{
		UINodesManager.UICanvas3D.get_gameObject().SetActive(isShow);
		if (BubbleDialogueManager.Pool2BubbleDialogue != null)
		{
			BubbleDialogueManager.Pool2BubbleDialogue.get_gameObject().SetActive(isShow);
		}
	}

	public static void Show2DUI(bool isShow, int hide_nodes = 0)
	{
		UINodesManager.NoEventsUIRoot_IsShow = isShow;
		if (isShow)
		{
			UINodesManager.SetNoEventsUIRootByIsShow(true);
			UINodesManager.cs_NormalUIRoot.set_enabled(true);
			UINodesManager.cs_MiddleUIRoot.set_enabled(true);
			UINodesManager.cs_TopUIRoot.set_enabled(true);
			UINodesManager.cs_T2RootOfSpecial.set_enabled(true);
			UINodesManager.cs_T3RootOfSpecial.set_enabled(true);
		}
		else
		{
			if (UINodeBit.IsContainNode(hide_nodes, 1))
			{
				UINodesManager.SetNoEventsUIRootByIsShow(false);
			}
			if (UINodeBit.IsContainNode(hide_nodes, 2))
			{
				UINodesManager.cs_NormalUIRoot.set_enabled(false);
			}
			if (UINodeBit.IsContainNode(hide_nodes, 4))
			{
				UINodesManager.cs_MiddleUIRoot.set_enabled(false);
			}
			if (UINodeBit.IsContainNode(hide_nodes, 8))
			{
				UINodesManager.cs_TopUIRoot.set_enabled(false);
			}
			if (UINodeBit.IsContainNode(hide_nodes, 16))
			{
				UINodesManager.cs_T2RootOfSpecial.set_enabled(false);
			}
			if (UINodeBit.IsContainNode(hide_nodes, 32))
			{
				UINodesManager.cs_T3RootOfSpecial.set_enabled(false);
			}
		}
	}

	public static void SetNoEventsUIRootByMainCamera(bool isShow)
	{
		UINodesManager.NoEventsUIRoot_MainCamera = isShow;
		UINodesManager.SetNoEventsUIRoot();
	}

	public static void SetNoEventsUIRootByIsShow(bool isShow)
	{
		UINodesManager.NoEventsUIRoot_IsShow = isShow;
		UINodesManager.SetNoEventsUIRoot();
	}

	private static void SetNoEventsUIRoot()
	{
		if (UINodesManager.NoEventsUIRoot_MainCamera && UINodesManager.NoEventsUIRoot_IsShow)
		{
			if (UINodesManager.cs_NoEventsUIRoot != null)
			{
				UINodesManager.cs_NoEventsUIRoot.set_enabled(true);
			}
			UINodesManager.ShowCanvasInNoEvent(true);
		}
		else
		{
			if (UINodesManager.cs_NoEventsUIRoot != null)
			{
				UINodesManager.cs_NoEventsUIRoot.set_enabled(false);
			}
			UINodesManager.ShowCanvasInNoEvent(false);
		}
	}

	private static void ShowCanvasInNoEvent(bool isShow)
	{
	}
}
