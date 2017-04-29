using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

public class UIManagerControl
{
	public class EventNames
	{
		public const string HideTipsUI = "UIManagerControl.HideTipsUI";

		public const string ResortDepth = "UIManagerControl.ResortDepth";

		public const string UIOpenOfSuccess = "UIManagerControl.UIOpenOfSuccess";

		public const string HidePopButtonsAdjustUI = "UIManagerControl.HidePopButtonsAdjustUI";

		public const string ResetSpineRendering = "UIManagerControl.ResetSpineRendering";

		public const string FakeHideAllUI = "UIManagerControl.FakeHideAllUI";
	}

	private static UIManagerControl instance;

	private List<UIBase> listBaseUI = new List<UIBase>();

	private Hashtable mPrefabMap = new Hashtable();

	private List<UIBase> m_listOpenUI = new List<UIBase>();

	public bool IsFakeHideAllUI;

	public static UIManagerControl Instance
	{
		get
		{
			if (UIManagerControl.instance == null)
			{
				UIManagerControl.instance = new UIManagerControl();
			}
			return UIManagerControl.instance;
		}
	}

	public UIBase OpenUI(string prefabName, Transform parent = null, bool hideTheVisible = false, UIType uitp = UIType.NonPush)
	{
		if (parent == null)
		{
			parent = UINodesManager.NormalUIRoot;
		}
		if (this.IsOpen(prefabName))
		{
			UIBase uIIfExist = this.GetUIIfExist(prefabName);
			uIIfExist.get_transform().SetParent(parent);
			uIIfExist.get_transform().set_localPosition(Vector3.get_zero());
			uIIfExist.get_transform().set_localRotation(Quaternion.get_identity());
			uIIfExist.get_transform().set_localScale(Vector3.get_one());
			this.RefreshlistOpenUI(uIIfExist, true);
			return uIIfExist;
		}
		if (!UIManagerControl.IsSpecialUI(prefabName))
		{
			UIStateSystem.LockOfWaitOpenUI(prefabName);
		}
		if (hideTheVisible || uitp == UIType.FullScreen)
		{
			this.HideAllInNormalUIRoot(prefabName);
		}
		UIBase uIBase = this.LoadUINow(prefabName, parent, uitp);
		if (uIBase == null)
		{
			if (!UIManagerControl.IsSpecialUI(prefabName))
			{
				EventDispatcher.Broadcast<string>("UIManagerControl.UIOpenOfSuccess", prefabName);
			}
			Debug.LogError("加载失败 : " + prefabName);
			return null;
		}
		if (!UIManagerControl.IsSpecialUI(prefabName))
		{
			GuideManager.Instance.CheckQueue(false, false);
		}
		this.RefreshlistOpenUI(uIBase, true);
		uIBase.Show(true);
		if (!UIManagerControl.IsSpecialUI(prefabName))
		{
			GuideManager.Instance.CheckQueue(true, false);
			EventDispatcher.Broadcast("GuideManager.LevelNow");
		}
		return uIBase;
	}

	public void OpenUI_Async(string prefabName, Action<UIBase> finish_callback = null, Transform parent = null)
	{
		int iD = UIID.GetID(prefabName);
		if (iD <= 0)
		{
			Debug.LogError("ui id is illegal, prefab name = " + prefabName);
			return;
		}
		WaitUI.OpenUI(0u);
		UINameTable dataNT = DataReader<UINameTable>.Get(iD);
		if (parent == null)
		{
			parent = WidgetSystem.GetRoot(dataNT.parent);
		}
		bool flag = false;
		if (dataNT.hideTheVisible == 1)
		{
			flag = true;
		}
		UIType type = (UIType)dataNT.type;
		if (this.IsOpen(dataNT.name))
		{
			UIBase uIIfExist = this.GetUIIfExist(dataNT.name);
			uIIfExist.get_transform().SetParent(parent);
			uIIfExist.get_transform().set_localPosition(Vector3.get_zero());
			uIIfExist.get_transform().set_localRotation(Quaternion.get_identity());
			uIIfExist.get_transform().set_localScale(Vector3.get_one());
			this.RefreshlistOpenUI(uIIfExist, true);
			WaitUI.CloseUI(0u);
			if (finish_callback != null)
			{
				finish_callback.Invoke(uIIfExist);
			}
			return;
		}
		if (!UIManagerControl.IsSpecialUI(dataNT.name))
		{
			UIStateSystem.LockOfWaitOpenUI(dataNT.name);
		}
		if (flag || type == UIType.FullScreen)
		{
			this.HideAllInNormalUIRoot(dataNT.name);
		}
		AssetManager.AssetOfTPManager.LoadAtlas(PreloadUIBaseSystem.GetPreloads(dataNT.name), delegate
		{
			this.LoadUI(dataNT.name, parent, type, delegate(UIBase ub)
			{
				if (ub == null)
				{
					if (!UIManagerControl.IsSpecialUI(dataNT.name))
					{
						EventDispatcher.Broadcast<string>("UIManagerControl.UIOpenOfSuccess", dataNT.name);
					}
					Debug.LogError("加载失败 : " + dataNT.name);
					WaitUI.CloseUI(0u);
					if (finish_callback != null)
					{
						finish_callback.Invoke(null);
					}
					return;
				}
				if (!UIManagerControl.IsSpecialUI(dataNT.name))
				{
					GuideManager.Instance.CheckQueue(false, false);
				}
				this.RefreshlistOpenUI(ub, true);
				ub.Show(true);
				if (!UIManagerControl.IsSpecialUI(dataNT.name))
				{
					GuideManager.Instance.CheckQueue(true, false);
					EventDispatcher.Broadcast("GuideManager.LevelNow");
				}
				WaitUI.CloseUI(0u);
				if (finish_callback != null)
				{
					finish_callback.Invoke(ub);
				}
			});
		}, 0);
	}

	public static bool IsSpecialUI(string prefabName)
	{
		return prefabName == "WaitingUI" || prefabName == "WaitUI" || prefabName == "GuideUI" || prefabName == "PreloadingUI" || prefabName == "NewContinueUI";
	}

	public void UnLoadUIPrefab(string prefabName)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			return;
		}
		if (this.mPrefabMap.Contains(prefabName))
		{
			GameObject gameObject = this.mPrefabMap.get_Item(prefabName) as GameObject;
			this.mPrefabMap.Remove(prefabName);
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
			for (int i = this.listBaseUI.get_Count() - 1; i >= 0; i--)
			{
				if (this.listBaseUI.get_Item(i) != null && this.listBaseUI.get_Item(i).prefabName == prefabName)
				{
					this.listBaseUI.RemoveAt(i);
					break;
				}
			}
		}
	}

	public void HideAll()
	{
		for (int i = this.listBaseUI.get_Count() - 1; i >= 0; i--)
		{
			UIBase uIBase = this.listBaseUI.get_Item(i);
			if (uIBase != null && uIBase.isVisible)
			{
				if (uIBase.get_transform() == null)
				{
					uIBase.Show(false);
				}
				else if (UINodesManager.CheckParentIsCanvas(uIBase.get_transform().get_parent()))
				{
					uIBase.Show(false);
				}
				else if (uIBase.prefabName == "LoadingUI" || uIBase.prefabName == "BattleLoading")
				{
					uIBase.Show(false);
				}
			}
		}
	}

	public void HideAllExcept(params string[] exceptPrefabNames)
	{
		List<string> list;
		if (exceptPrefabNames != null && exceptPrefabNames.Length > 0)
		{
			list = new List<string>(exceptPrefabNames);
		}
		else
		{
			list = new List<string>();
		}
		for (int i = this.listBaseUI.get_Count() - 1; i >= 0; i--)
		{
			UIBase uIBase = this.listBaseUI.get_Item(i);
			if (uIBase != null && uIBase.isVisible && !list.Contains(uIBase.prefabName))
			{
				if (uIBase.get_transform() == null)
				{
					uIBase.Show(false);
				}
				else if (UINodesManager.CheckParentIsCanvas(uIBase.get_transform().get_parent()))
				{
					uIBase.Show(false);
				}
				else if (uIBase.prefabName == "LoadingUI" || uIBase.prefabName == "BattleLoading")
				{
					uIBase.Show(false);
				}
			}
		}
	}

	public void HideUI(string prefabName)
	{
		UIBase uIIfExist = this.GetUIIfExist(prefabName);
		if (uIIfExist == null)
		{
			return;
		}
		if (uIIfExist.isVisible || uIIfExist.get_gameObject().get_activeSelf())
		{
			uIIfExist.Show(false);
		}
	}

	private void HideAllInNormalUIRoot(string prefabName)
	{
		for (int i = 0; i < UINodesManager.NormalUIRoot.get_childCount(); i++)
		{
			Transform child = UINodesManager.NormalUIRoot.GetChild(i);
			if (child.get_gameObject().get_activeSelf())
			{
				UIBase component = child.GetComponent<UIBase>();
				if (component != null)
				{
					if (string.IsNullOrEmpty(component.prefabName) || !component.prefabName.Equals(prefabName))
					{
						if (component.isVisible)
						{
							component.isBeenPushed = true;
							component.Show(false);
						}
					}
				}
			}
		}
	}

	public UIBase GetUIIfExist(string prefabName)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			return null;
		}
		for (int i = this.listBaseUI.get_Count() - 1; i >= 0; i--)
		{
			if (this.listBaseUI.get_Item(i).prefabName == prefabName)
			{
				return this.listBaseUI.get_Item(i);
			}
		}
		return null;
	}

	public bool IsOpen(string prefabName)
	{
		UIBase uIIfExist = this.GetUIIfExist(prefabName);
		return uIIfExist != null && uIIfExist.get_gameObject().get_activeInHierarchy();
	}

	private UIBase LoadUINow(string prefabName, Transform parent, UIType uitp)
	{
		GameObject uiprefab = this.InstantiateUINow(prefabName, parent);
		return this.InitLoadUI(prefabName, parent, uitp, uiprefab);
	}

	public void LoadUI(string prefabName, Transform parent, UIType uitp, Action<UIBase> finish_callback)
	{
		this.InstantiateUI(prefabName, parent, delegate(GameObject uiprefab)
		{
			UIBase uIBase = this.InitLoadUI(prefabName, parent, uitp, uiprefab);
			if (finish_callback != null)
			{
				finish_callback.Invoke(uIBase);
			}
		});
	}

	private UIBase InitLoadUI(string prefabName, Transform parent, UIType uitp, GameObject uiprefab)
	{
		if (uiprefab == null)
		{
			return null;
		}
		UIBase component = uiprefab.GetComponent<UIBase>();
		if (component == null)
		{
			return null;
		}
		component.uiType = uitp;
		if (!component.inited)
		{
			component.Init(prefabName);
			for (int i = this.listBaseUI.get_Count() - 1; i >= 0; i--)
			{
				if (this.listBaseUI.get_Item(i).prefabName == prefabName)
				{
					this.listBaseUI.RemoveAt(i);
				}
			}
			this.listBaseUI.Add(component);
		}
		UIStackManager.Instance.PushUI(component);
		return component;
	}

	private GameObject InstantiateUINow(string prefabName, Transform parent)
	{
		GameObject fromPrefabMap = this.GetFromPrefabMap(prefabName, parent);
		if (fromPrefabMap != null)
		{
			return fromPrefabMap;
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(prefabName);
		this.Add2PrefabMap(prefabName, parent, instantiate2Prefab);
		return instantiate2Prefab;
	}

	public void InstantiateUI(string prefabName, Transform parent, Action<GameObject> finish_callback)
	{
		GameObject fromPrefabMap = this.GetFromPrefabMap(prefabName, parent);
		if (fromPrefabMap != null)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(fromPrefabMap);
			}
			return;
		}
		AssetManager.LoadAssetOfUI(prefabName, delegate(bool isSuccess)
		{
			if (finish_callback == null)
			{
				return;
			}
			if (isSuccess)
			{
				finish_callback.Invoke(this.InstantiateUINow(prefabName, parent));
			}
			else
			{
				finish_callback.Invoke(null);
			}
		});
	}

	private GameObject GetFromPrefabMap(string prefabName, Transform parent)
	{
		if (!this.mPrefabMap.Contains(prefabName))
		{
			return null;
		}
		if (this.mPrefabMap.get_Item(prefabName) != null && this.mPrefabMap.get_Item(prefabName) as GameObject != null)
		{
			GameObject gameObject = this.mPrefabMap.get_Item(prefabName) as GameObject;
			gameObject.get_transform().SetParent(parent);
			gameObject.get_transform().set_localPosition(Vector3.get_zero());
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			gameObject.get_transform().set_localScale(Vector3.get_one());
			return gameObject;
		}
		this.mPrefabMap.Remove(prefabName);
		return null;
	}

	private void Add2PrefabMap(string prefabName, Transform parent, GameObject uiPrefab)
	{
		if (uiPrefab != null)
		{
			uiPrefab.set_name(prefabName);
			if (parent != null)
			{
				uiPrefab.get_transform().SetParent(parent, false);
				uiPrefab.get_transform().set_localPosition(Vector3.get_zero());
				uiPrefab.get_transform().set_localRotation(Quaternion.get_identity());
				uiPrefab.get_transform().set_localScale(Vector3.get_one());
			}
			this.mPrefabMap.Add(prefabName, uiPrefab);
		}
	}

	public void RefreshlistOpenUI(UIBase uibase0, bool isOpen)
	{
		if (uibase0 == null)
		{
			return;
		}
		if (uibase0.get_transform() == null)
		{
			return;
		}
		if (uibase0.get_transform().get_parent() != null && !UINodesManager.Is2DCanvasRoot(uibase0.get_transform().get_parent()))
		{
			return;
		}
		string prefabName = uibase0.prefabName;
		if (uibase0.uiDepth >= 14000)
		{
			return;
		}
		this.m_listOpenUI.Remove(uibase0);
		if (isOpen)
		{
			this.m_listOpenUI.Add(uibase0);
		}
		if (uibase0.GetIsIgnoreToSpine())
		{
			return;
		}
		EventDispatcher.Broadcast("UIManagerControl.ResetSpineRendering");
	}

	public bool IsShowSpine(string uibase)
	{
		UIBase uIIfExist = this.GetUIIfExist(uibase);
		if (uIIfExist == null)
		{
			return true;
		}
		if (uIIfExist.get_transform() == null || !uIIfExist.get_gameObject().get_activeSelf())
		{
			return false;
		}
		for (int i = this.m_listOpenUI.get_Count() - 1; i >= 0; i--)
		{
			UIBase uIBase = this.m_listOpenUI.get_Item(i);
			if (!(uIBase == null) && !(uIBase.get_transform() == null))
			{
				if (uIBase.prefabName == uibase)
				{
					return true;
				}
				if (!uIBase.GetIsIgnoreToSpine())
				{
					if (uIBase != null && uIBase.uiDepth == uIIfExist.uiDepth)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public void AddFloatText(string text, Color textColor)
	{
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		FloatTextAddManager.Instance.AddFloatText(text, textColor);
	}

	public void ShowToastText(string text, Color textColor, float duration, float delay)
	{
		ToastUI toastUI = UIManagerControl.Instance.OpenUI("ToastUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as ToastUI;
		toastUI.ShowText(text, textColor, duration, delay);
	}

	public void ShowToastText(string text)
	{
		this.ShowToastText(text, 1f, 1f);
	}

	public ToastUI ShowToastText(string text, float duration, float delay)
	{
		ToastUI toastUI = UIManagerControl.Instance.OpenUI("ToastUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as ToastUI;
		toastUI.ShowText(text, duration, delay);
		return toastUI;
	}

	public void ShowBattleToastText(string text, float dismissDelay = 2f)
	{
		BattleToastUI battleToastUI = UIManagerControl.Instance.OpenUI("BattleToastUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as BattleToastUI;
		battleToastUI.ShowText(text, dismissDelay);
	}

	public void OpenSourceReferenceUI(int itemID, Action action = null)
	{
		string text = GameDataUtils.GetChineseContent(508022, false);
		string itemName = GameDataUtils.GetItemName(itemID, true, 0L);
		text = string.Format(text, itemName);
		UIManagerControl.Instance.ShowToastText(text);
	}

	public void SetMainCameraEnable()
	{
		bool flag = true;
		for (int i = 0; i < this.listBaseUI.get_Count(); i++)
		{
			if (this.listBaseUI.get_Item(i) != null && this.listBaseUI.get_Item(i).isVisible && this.listBaseUI.get_Item(i).get_gameObject().get_activeInHierarchy() && this.listBaseUI.get_Item(i).hideMainCamera)
			{
				flag = false;
				break;
			}
		}
		CamerasMgr.EnableCamera2Main(flag);
		SoundManager.TurnOnOff2Player(flag);
		UINodesManager.SetNoEventsUIRootByMainCamera(flag);
	}

	public void FakeHideAllUI(bool hide, int hide_nodes_2D = 7)
	{
		this.IsFakeHideAllUI = hide;
		UINodesManager.Show2DUI(!hide, hide_nodes_2D);
		UINodesManager.Show3DUI(!hide);
		EventDispatcher.Broadcast("UIManagerControl.FakeHideAllUI");
		EventDispatcher.Broadcast<bool>("ControlStick.ForbiddenStick", hide);
	}

	public void PrintMessage()
	{
		for (int i = 0; i < this.listBaseUI.get_Count(); i++)
		{
			if (this.listBaseUI.get_Item(i) != null)
			{
				Debug.LogError("=>listBaseUI, PrefabName = " + this.listBaseUI.get_Item(i).prefabName);
			}
		}
		IEnumerator enumerator = this.mPrefabMap.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.get_Current();
				Debug.LogError("=>mPrefabMap, PrefabName = " + current);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	public void ClearAllUI()
	{
		this.listBaseUI.Clear();
		this.mPrefabMap.Clear();
	}
}
