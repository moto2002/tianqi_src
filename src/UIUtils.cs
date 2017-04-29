using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUtils
{
	private static int HardwareResolutionOfWidth;

	private static int HardwareResolutionOfHeight;

	public static float UIScaleFactor;

	private static Transform m_AdvancedFPS;

	public static Transform AdvancedFPS
	{
		get
		{
			if (UIUtils.m_AdvancedFPS == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("AdvancedFPSCounter");
				UGUITools.SetParent(null, instantiate2Prefab, false, "AdvancedFPSCounter");
				UIUtils.m_AdvancedFPS = instantiate2Prefab.get_transform();
			}
			return UIUtils.m_AdvancedFPS;
		}
	}

	private static void InitHardwareResolution()
	{
		if (UIUtils.HardwareResolutionOfWidth == 0)
		{
			UIUtils.HardwareResolutionOfWidth = Screen.get_currentResolution().get_width();
			UIUtils.HardwareResolutionOfHeight = Screen.get_currentResolution().get_height();
		}
	}

	public static void SetHardwareResolution()
	{
		UIUtils.InitHardwareResolution();
		if (SystemConfig.IsSetHardwareResolutionOn)
		{
			if (Application.get_platform() == 11 || Application.get_platform() == 8)
			{
				UIUtils.SetHardwareResolution(SystemConfig.RESOLUTION_WIDTH);
			}
		}
		else if (Application.get_platform() == 11 || Application.get_platform() == 8)
		{
			UIUtils.UnitySetResolution(UIUtils.HardwareResolutionOfWidth, UIUtils.HardwareResolutionOfHeight, true);
		}
	}

	public static void SetHardwareResolution(int width_pixel)
	{
		UIUtils.InitHardwareResolution();
		if (UIUtils.HardwareResolutionOfWidth > width_pixel)
		{
			UIUtils.UnitySetResolution(width_pixel, width_pixel * UIUtils.HardwareResolutionOfHeight / UIUtils.HardwareResolutionOfWidth, true);
		}
		else
		{
			UIUtils.UnitySetResolution(UIUtils.HardwareResolutionOfWidth, UIUtils.HardwareResolutionOfHeight, true);
		}
	}

	private static void UnitySetResolution(int width, int height, bool fullscreen)
	{
		if (SystemInfoTools.IsDeviceModelInBlacklist())
		{
			return;
		}
		Screen.SetResolution(width, height, fullscreen);
	}

	public static void DrawGizmosRect(Rect drawRect)
	{
		Camera cameraUI = CamerasMgr.CameraUI;
		if (cameraUI != null)
		{
			Vector3 vector = new Vector3(drawRect.get_x(), drawRect.get_y(), 0f);
			Vector3 vector2 = new Vector3(drawRect.get_x() + drawRect.get_width(), drawRect.get_y(), 0f);
			Vector3 vector3 = new Vector3(drawRect.get_x() + drawRect.get_width(), drawRect.get_y() + drawRect.get_height(), 0f);
			Vector3 vector4 = new Vector3(drawRect.get_x(), drawRect.get_y() + drawRect.get_height(), 0f);
			Gizmos.DrawLine(cameraUI.ScreenToWorldPoint(vector), cameraUI.ScreenToWorldPoint(vector2));
			Gizmos.DrawLine(cameraUI.ScreenToWorldPoint(vector2), cameraUI.ScreenToWorldPoint(vector3));
			Gizmos.DrawLine(cameraUI.ScreenToWorldPoint(vector3), cameraUI.ScreenToWorldPoint(vector4));
			Gizmos.DrawLine(cameraUI.ScreenToWorldPoint(vector4), cameraUI.ScreenToWorldPoint(vector));
		}
	}

	public static void SwitchAudioVolume()
	{
		if (!SystemConfig.IsAudioOn)
		{
			AudioSource component = CamerasMgr.MainCameraRoot.GetComponent<AudioSource>();
			if (component != null)
			{
				component.set_volume(0f);
			}
			AudioSource component2 = ClientApp.Instance.get_gameObject().GetComponent<AudioSource>();
			if (component2 != null)
			{
				component2.set_volume(0f);
			}
		}
	}

	public static void ShowImageText(Transform parent, int num, string imagePrefix)
	{
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.get_Current();
				Object.Destroy(transform.get_gameObject());
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
		int length = num.ToString().get_Length();
		int num2 = num;
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<Image>();
		for (int i = 1; i < length; i++)
		{
			int num3 = (int)((double)num2 / Math.Pow(10.0, (double)(length - i)));
			num2 = (int)((double)num2 % Math.Pow(10.0, (double)(length - i)));
			GameObject gameObject2 = UGUITools.AddChild(parent.get_gameObject(), gameObject, false);
			gameObject2.set_name(num3.ToString());
			Image component = gameObject2.GetComponent<Image>();
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(imagePrefix + num3));
			component.SetNativeSize();
		}
		GameObject gameObject3 = UGUITools.AddChild(parent.get_gameObject(), gameObject, false);
		ResourceManager.SetSprite(gameObject3.GetComponent<Image>(), ResourceManager.GetIconSprite(imagePrefix + num2));
		gameObject3.set_name(num2.ToString());
		Object.Destroy(gameObject);
	}

	public static void ShowImageText(Transform parent, long num, string imagePrefix, string notDestroyChildName)
	{
		Transform transform = null;
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform2 = (Transform)enumerator.get_Current();
				if (transform2.get_name() == notDestroyChildName && notDestroyChildName != string.Empty)
				{
					transform = transform2;
				}
				else
				{
					Object.Destroy(transform2.get_gameObject());
				}
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
		int length = num.ToString().get_Length();
		long num2 = num;
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<Image>();
		for (int i = 1; i < length; i++)
		{
			int num3 = (int)((double)num2 / Math.Pow(10.0, (double)(length - i)));
			num2 = (long)((int)((double)num2 % Math.Pow(10.0, (double)(length - i))));
			GameObject gameObject2 = UGUITools.AddChild(parent.get_gameObject(), gameObject, false);
			gameObject2.set_name(num3.ToString());
			Image component = gameObject2.GetComponent<Image>();
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(imagePrefix + num3));
			component.SetNativeSize();
		}
		GameObject gameObject3 = UGUITools.AddChild(parent.get_gameObject(), gameObject, false);
		ResourceManager.SetSprite(gameObject3.GetComponent<Image>(), ResourceManager.GetIconSprite(imagePrefix + num2));
		gameObject3.set_name(num2.ToString());
		Object.Destroy(gameObject);
		if (transform != null)
		{
			transform.SetAsLastSibling();
		}
	}

	public static void AdjustScale(RectTransform target)
	{
		target.set_localScale(new Vector3(UIUtils.UIScaleFactor, UIUtils.UIScaleFactor, 1f));
		target.set_sizeDelta(new Vector2(target.get_sizeDelta().x / UIUtils.UIScaleFactor, target.get_sizeDelta().y / UIUtils.UIScaleFactor));
	}

	public static void GetBound(ref float x, ref float y, float width_targettran, float height_targettran, Vector2 pivot)
	{
		int num;
		int num2;
		UIConst.GetRealScreenSize(out num, out num2);
		if (pivot.x == 0f)
		{
			if (x + width_targettran > (float)(num / 2))
			{
				x = (float)(num / 2) - width_targettran;
			}
			if (x < (float)(-(float)(num / 2)))
			{
				x = (float)(-(float)(num / 2));
			}
		}
		else if (pivot.x == 0.5f)
		{
			if (x + width_targettran / 2f > (float)(num / 2))
			{
				x = (float)(num / 2) - width_targettran / 2f;
			}
			if (x - width_targettran / 2f < (float)(-(float)(num / 2)))
			{
				x = (float)(-(float)(num / 2)) + width_targettran / 2f;
			}
		}
		else if (pivot.x == 1f)
		{
			if (x > (float)(num / 2))
			{
				x = (float)(num / 2);
			}
			if (x - width_targettran < (float)(-(float)(num / 2)))
			{
				x = (float)(-(float)(num / 2)) + width_targettran;
			}
		}
		if (pivot.y == 0f)
		{
			if (y + height_targettran > (float)(num2 / 2))
			{
				y = (float)(num2 / 2) - height_targettran;
			}
			if (y < (float)(-(float)(num2 / 2)))
			{
				y = (float)(-(float)(num2 / 2));
			}
		}
		else if (pivot.y == 0.5f)
		{
			if (y + height_targettran / 2f > (float)(num2 / 2))
			{
				y = (float)(num2 / 2) - height_targettran / 2f;
			}
			if (y - height_targettran / 2f < (float)(-(float)(num2 / 2)))
			{
				y = (float)(-(float)(num2 / 2)) + height_targettran / 2f;
			}
		}
		else if (pivot.y == 1f)
		{
			if (y > (float)(num2 / 2))
			{
				y = (float)(num2 / 2);
			}
			if (y - height_targettran < (float)(-(float)(num2 / 2)))
			{
				y = (float)(-(float)(num2 / 2)) + height_targettran;
			}
		}
	}

	public static SpriteRenderer GetRoleHeadIcon(int profession)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return GameDataUtils.GetIcon(zhuanZhiJiChuPeiZhi.jobPic1);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetRoleSelfBodyImage()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return GameDataUtils.GetIcon(zhuanZhiJiChuPeiZhi.jobPic);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetRoleSelfChangeCareerImage()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return ResourceManager.GetIconSprite(zhuanZhiJiChuPeiZhi.jobPic5);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static string GetRolePVPImage(int profession)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return zhuanZhiJiChuPeiZhi.jobPic3;
		}
		return string.Empty;
	}

	public static SpriteRenderer GetRoleSelfNameBg()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return ResourceManager.GetIconSprite(zhuanZhiJiChuPeiZhi.jobNameBg);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetRoleSelfName()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return ResourceManager.GetIconSprite(zhuanZhiJiChuPeiZhi.jobNameImage);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetRoleSmallIcon(int profession)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return ResourceManager.GetIconSprite(zhuanZhiJiChuPeiZhi.jobPic4);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetRoleSelfMenuIcon()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return ResourceManager.GetIconSprite(zhuanZhiJiChuPeiZhi.jobPic2);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static string GetRoleName(int profession)
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(profession);
		if (zhuanZhiJiChuPeiZhi != null)
		{
			return GameDataUtils.GetChineseContent(zhuanZhiJiChuPeiZhi.jobName, false);
		}
		return string.Empty;
	}

	public static void animToFadeOut(GameObject node)
	{
		UIAnimatorEventReceiver uIAnimatorEventReceiver = node.AddUniqueComponent<UIAnimatorEventReceiver>();
		uIAnimatorEventReceiver.CallBackOfEnd = delegate
		{
			UIUtils.animToFadeOutEnd(node);
		};
		node.AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(node, "UI2Close_a");
	}

	public static void animToFadeOutEnd(GameObject node)
	{
		if (node != null)
		{
			Animator component = node.GetComponent<Animator>();
			if (component != null)
			{
				component.set_enabled(false);
			}
			CanvasGroup canvasGroup = node.AddMissingComponent<CanvasGroup>();
			canvasGroup.set_alpha(0f);
		}
	}

	public static void animToFadeIn(GameObject node)
	{
		UIAnimatorEventReceiver uIAnimatorEventReceiver = node.AddUniqueComponent<UIAnimatorEventReceiver>();
		uIAnimatorEventReceiver.CallBackOfEnd = delegate
		{
			UIUtils.animToFadeInEnd(node);
		};
		node.AddMissingComponent<CanvasGroup>();
		UIUtils.animPlay(node, "UI2Open_a");
	}

	public static void animToFadeInEnd(GameObject node)
	{
		if (node != null)
		{
			Animator component = node.GetComponent<Animator>();
			if (component != null)
			{
				component.set_enabled(false);
			}
			CanvasGroup canvasGroup = node.AddMissingComponent<CanvasGroup>();
			canvasGroup.set_alpha(1f);
		}
	}

	public static void animPlay(GameObject node, string controller)
	{
		Animator animator = node.AddUniqueComponent<Animator>();
		animator.set_enabled(true);
		Object asset2UIAnim = ResourceManager.Instance.GetAsset2UIAnim(controller);
		if (asset2UIAnim == null)
		{
			return;
		}
		animator.set_runtimeAnimatorController(asset2UIAnim as RuntimeAnimatorController);
		animator.set_speed(1f);
		animator.Play("animOfUI", 0, 0f);
	}

	public static void animReset(GameObject node, string controller)
	{
		Animator animator = node.AddUniqueComponent<Animator>();
		animator.set_enabled(true);
		Object asset2UIAnim = ResourceManager.Instance.GetAsset2UIAnim(controller);
		if (asset2UIAnim == null)
		{
			return;
		}
		animator.set_runtimeAnimatorController(asset2UIAnim as RuntimeAnimatorController);
		if (animator.get_runtimeAnimatorController() != null)
		{
			animator.Play("animOfUI", 0, 0f);
		}
	}

	public static void SetHSV(Material material, List<float> colour)
	{
		if (colour != null && colour.get_Count() >= 3)
		{
			material.SetFloat(ShaderPIDManager._HueShift, colour.get_Item(0));
			material.SetFloat(ShaderPIDManager._Sat, colour.get_Item(1));
			material.SetFloat(ShaderPIDManager._Val, colour.get_Item(2));
		}
		else
		{
			material.SetFloat(ShaderPIDManager._HueShift, UIConst.HSV_DEFAULT.x);
			material.SetFloat(ShaderPIDManager._Sat, UIConst.HSV_DEFAULT.y);
			material.SetFloat(ShaderPIDManager._Val, UIConst.HSV_DEFAULT.z);
		}
	}
}
