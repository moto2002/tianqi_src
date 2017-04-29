using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using XUPorterJSON;

public static class Utils
{
	public class CullingMaskType
	{
		public const int Everything = 1;

		public const int Nothing = 2;

		public const int LayersToBack = 3;

		public const int LayersToFront = 4;

		public const int Win = 5;

		public const int Timeline = 6;

		public const int PetUltraSkill = 7;

		public const int NoCameraRange = 8;

		public const int CameraRange = 9;
	}

	public class CullingMaskValue
	{
		public const int Mask_Everything = -1;

		public const int Mask_Nothing = 0;
	}

	private static string[] background_layers = new string[]
	{
		"Default"
	};

	private static string[] win_layers = new string[]
	{
		"Default",
		"CameraRange",
		"Terrian",
		"FX"
	};

	private static string[] timeline_layers = new string[]
	{
		"Default",
		"CameraRange",
		"FX",
		"FX_Distortion",
		"Terrian"
	};

	public static string[] camerarange_layers = new string[]
	{
		"CameraRange"
	};

	private static string[] pet_ultra_skill_no_layers = new string[]
	{
		"UI"
	};

	public static string[] no_camerarange_layers = new string[]
	{
		"CameraRange"
	};

	private static Vector3 RoleLightInitAngles = Vector3.get_zero();

	private static Vector3 tempMain2Screenpos = Vector3.get_zero();

	private static Vector3 tempUI2Worldpos = Vector3.get_zero();

	private static Light RoleLight
	{
		get
		{
			return null;
		}
	}

	public static Material GetShareMaterial(Renderer render)
	{
		if (render == null)
		{
			return null;
		}
		if (Utils.IsEditor() && Application.get_isPlaying())
		{
			return render.get_material();
		}
		return render.get_sharedMaterial();
	}

	public static Material[] GetShareMaterials(Renderer render)
	{
		if (render == null)
		{
			return null;
		}
		if (Utils.IsEditor() && Application.get_isPlaying())
		{
			return render.get_materials();
		}
		return render.get_sharedMaterials();
	}

	public static void SetShareMaterial(Renderer render, Material mat)
	{
		if (render == null)
		{
			return;
		}
		if (Utils.IsEditor() && Application.get_isPlaying())
		{
			render.set_material(mat);
		}
		else
		{
			render.set_sharedMaterial(mat);
		}
	}

	public static void SetShareMaterials(Renderer render, Material[] mats)
	{
		if (render == null)
		{
			return;
		}
		if (Utils.IsEditor() && Application.get_isPlaying())
		{
			render.set_materials(mats);
		}
		else
		{
			render.set_sharedMaterials(mats);
		}
	}

	public static void SetShareMaterials(Renderer render, Material commonMat)
	{
		if (render == null)
		{
			return;
		}
		if (Utils.IsEditor() && Application.get_isPlaying())
		{
			if (render.get_materials() == null)
			{
				return;
			}
			Material[] array = new Material[render.get_materials().Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = commonMat;
			}
			render.set_materials(array);
		}
		else
		{
			if (render.get_sharedMaterials() == null)
			{
				return;
			}
			Material[] array2 = new Material[render.get_sharedMaterials().Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = commonMat;
			}
			render.set_sharedMaterials(array2);
		}
	}

	private static bool IsEditor()
	{
		return !Application.get_isEditor();
	}

	public static void SetCameraCullingMask(Camera camera, int classify)
	{
		if (camera != null)
		{
			camera.set_cullingMask(Utils.GetCullingMask(classify));
		}
	}

	public static int GetCullingMask(int classify)
	{
		int num = 0;
		switch (classify)
		{
		case 1:
			num = -1;
			break;
		case 2:
			num = 0;
			break;
		case 3:
			num = LayerSystem.GetMask(Utils.background_layers);
			break;
		case 4:
			num = -1;
			for (int i = 0; i < Utils.background_layers.Length; i++)
			{
				num &= ~(1 << LayerSystem.NameToLayer(Utils.background_layers[i]));
			}
			break;
		case 5:
			num = LayerSystem.GetMask(Utils.win_layers);
			break;
		case 6:
			num = LayerSystem.GetMask(Utils.timeline_layers);
			break;
		case 7:
			num = -1;
			for (int j = 0; j < Utils.pet_ultra_skill_no_layers.Length; j++)
			{
				num &= ~(1 << LayerSystem.NameToLayer(Utils.pet_ultra_skill_no_layers[j]));
			}
			break;
		case 8:
			num = -1;
			for (int k = 0; k < Utils.no_camerarange_layers.Length; k++)
			{
				num &= ~(1 << LayerSystem.NameToLayer(Utils.no_camerarange_layers[k]));
			}
			break;
		case 9:
			num = LayerSystem.GetMask(Utils.camerarange_layers);
			break;
		}
		return num;
	}

	public static void EnableRoleLight(bool isShow)
	{
		if (Utils.RoleLight != null)
		{
			Utils.RoleLight.set_enabled(isShow);
			Utils.RoleLight.set_enabled(false);
		}
	}

	public static void SetRoleLightRotation(bool isReset, float rotateY = 0f)
	{
		if (Utils.RoleLight != null)
		{
			Utils.RoleLight.get_transform().set_localEulerAngles(Utils.RoleLightInitAngles);
			if (!isReset)
			{
				Transform expr_34 = Utils.RoleLight.get_transform();
				expr_34.set_localEulerAngles(expr_34.get_localEulerAngles() + new Vector3(0f, rotateY, 0f));
			}
		}
	}

	public static string ReplaceFirst(this string input, string oldValue, string newValue, int startAt = 0)
	{
		int num = input.IndexOf(oldValue, startAt);
		if (num < 0)
		{
			return input;
		}
		return input.Substring(0, num) + newValue + input.Substring(num + oldValue.get_Length());
	}

	public static Vector3 WorldToUIPos(Vector3 point)
	{
		Utils.tempMain2Screenpos = CamerasMgr.CameraMain.WorldToScreenPoint(point);
		Utils.tempUI2Worldpos = CamerasMgr.CameraUI.ScreenToWorldPoint(Utils.tempMain2Screenpos);
		return Utils.tempUI2Worldpos;
	}

	public static Vector3 WorldToUIPos_NoZ(Vector3 point)
	{
		Utils.tempMain2Screenpos = CamerasMgr.CameraMain.WorldToScreenPoint(point);
		if (Utils.tempMain2Screenpos.z > 0f)
		{
			Utils.tempUI2Worldpos = CamerasMgr.CameraUI.ScreenToWorldPoint(Utils.tempMain2Screenpos);
			return Utils.tempUI2Worldpos;
		}
		return Vector3.get_zero();
	}

	public static Vector2 GetVectorNoY(this Vector3 t)
	{
		return new Vector2(t.x, t.z);
	}

	public static Vector3 GetVectorWithY(this Vector2 t)
	{
		return new Vector3(t.x, 0f, t.y);
	}

	public static Vector3 AssignXZero(this Vector3 t)
	{
		return new Vector3(0f, t.y, t.z);
	}

	public static Vector3 AssignYZero(this Vector3 t)
	{
		return new Vector3(t.x, 0f, t.z);
	}

	public static Vector3 AssignX(this Vector3 t, float x)
	{
		return new Vector3(x, t.y, t.z);
	}

	public static Vector3 AssignY(this Vector3 t, float y)
	{
		return new Vector3(t.x, y, t.z);
	}

	public static void LookAtByPlane(this Transform t, Vector2 pos, float height)
	{
		t.LookAt(new Vector3(pos.x, height, pos.y));
	}

	public static void SetSelfPositionByPlane(this Transform t, Vector2 pos, float height)
	{
		t.set_position(new Vector3(pos.x, height, pos.y));
	}

	public static float ComputeAngle(Quaternion a, Quaternion b)
	{
		Vector3 vector = a * Vector3.get_forward();
		Vector3 vector2 = b * Vector3.get_forward();
		float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		float num2 = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
		return Mathf.DeltaAngle(num, num2);
	}

	public static Hashtable ReadFromJson(string absPath)
	{
		StreamReader streamReader = new StreamReader(absPath);
		string text = streamReader.ReadLine();
		Hashtable result = Utils.ReadFromMemory(text);
		streamReader.Close();
		streamReader.Dispose();
		return result;
	}

	public static Hashtable ReadFromMemory(string text)
	{
		Hashtable hashtable = new Hashtable();
		return MiniJSON.jsonDecode(text) as Hashtable;
	}

	public static void SetTransformZOn(Transform target, bool isOn)
	{
		if (target != null)
		{
			if (isOn)
			{
				if (!target.get_gameObject().get_activeSelf())
				{
					target.get_gameObject().SetActive(true);
				}
			}
			else if (target.get_gameObject().get_activeSelf())
			{
				target.get_gameObject().SetActive(false);
			}
		}
	}

	public static void WinSetting(bool win)
	{
		if (win)
		{
			PetManager.Instance.DeleteScreenFXOfBattles();
			EntityWorld.Instance.ActSelf.IsDisplayingByLayer = true;
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 5);
			UINodesManager.SetNoEventsUIRootByIsShow(false);
		}
		else
		{
			EntityWorld.Instance.ActSelf.IsDisplayingByLayer = false;
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
			UINodesManager.SetNoEventsUIRootByIsShow(true);
		}
	}

	public static T GetComponent<T>(GameObject go)
	{
		object obj = go.GetComponent(typeof(T));
		if (obj == null)
		{
			obj = go.AddComponent(typeof(T));
		}
		return (T)((object)obj);
	}

	public static T GetComponent<T>(Component component)
	{
		return Utils.GetComponent<T>(component.get_gameObject());
	}

	public static void SetInstanceLock(bool instanceLock)
	{
		if (instanceLock)
		{
			EventDispatcher.Broadcast("ControlStick.InterruptStick");
			InstanceManager.IsActorAnimatorOn = false;
			InstanceManager.PauseAllClientAI();
		}
		else
		{
			InstanceManager.IsActorAnimatorOn = true;
			InstanceManager.ResumeAllClientAI();
		}
	}

	public static void PauseAI(bool instanceLock)
	{
		if (instanceLock)
		{
			EventDispatcher.Broadcast("ControlStick.InterruptStick");
			InstanceManager.PauseAllClientAI();
		}
		else
		{
			InstanceManager.ResumeAllClientAI();
		}
	}

	public static bool HasCHZN(string inputData)
	{
		Regex regex = new Regex("[一-龥]");
		Match match = regex.Match(inputData);
		return match.get_Success();
	}

	public static Type GetType(string TypeName)
	{
		Type type = Type.GetType(TypeName);
		if (type != null)
		{
			return type;
		}
		Assembly assembly = Assembly.LoadFrom("hsh.dll");
		if (assembly == null)
		{
			return null;
		}
		return assembly.GetType(TypeName);
	}

	public static void PauseGame(bool isPause)
	{
		if (isPause)
		{
			Time.set_timeScale(0f);
			TimerHeap.Pause(isPause);
		}
		else
		{
			Time.set_timeScale(AppConst.GlobalTimeScale);
			TimerHeap.Pause(isPause);
		}
	}

	public static string GetItemNum(int templateId, long value)
	{
		Items item = BackpackManager.Instance.GetItem(templateId);
		if (item != null)
		{
			if (item.secondType == 15)
			{
				return Utils.GetGoldValueStr(value);
			}
			if (item.secondType == 16)
			{
				return Utils.GetExpValueStr(value);
			}
		}
		return value.ToString();
	}

	private static string GetExpValueStr(long value)
	{
		return AttrUtility.GetAttrValueDisplay(AttrType.Exp, value);
	}

	private static string GetGoldValueStr(long value)
	{
		return AttrUtility.GetAttrValueDisplay(AttrType.Gold, value);
	}

	public static string SwitchChineseNumber(long number, int type = 0)
	{
		string text = number.ToString();
		if (type == 1 && text.get_Length() > 16)
		{
			return Utils.FloorNumber(text, 16, GameDataUtils.GetChineseContent(330027, false) + GameDataUtils.GetChineseContent(330027, false));
		}
		if (type == 1 && text.get_Length() > 15)
		{
			return Utils.FloorNumber(text, 15, GameDataUtils.GetChineseContent(330030, false) + GameDataUtils.GetChineseContent(330028, false) + GameDataUtils.GetChineseContent(330027, false));
		}
		if (text.get_Length() > 12)
		{
			return Utils.FloorNumber(text, 12, GameDataUtils.GetChineseContent(330028, false) + GameDataUtils.GetChineseContent(330027, false));
		}
		if (type == 1 && text.get_Length() > 11)
		{
			return Utils.FloorNumber(text, 11, GameDataUtils.GetChineseContent(330030, false) + GameDataUtils.GetChineseContent(330027, false));
		}
		if (text.get_Length() > 8)
		{
			return Utils.FloorNumber(text, 8, GameDataUtils.GetChineseContent(330027, false));
		}
		if (type == 1 && text.get_Length() > 7)
		{
			return Utils.FloorNumber(text, 7, GameDataUtils.GetChineseContent(330030, false) + GameDataUtils.GetChineseContent(330028, false));
		}
		if (type == 0 && text.get_Length() > 5)
		{
			return Utils.FloorNumber(text, 4, GameDataUtils.GetChineseContent(330028, false));
		}
		if (type == 1 && text.get_Length() > 4)
		{
			return Utils.FloorNumber(text, 4, GameDataUtils.GetChineseContent(330028, false));
		}
		if (type == 1 && text.get_Length() > 3)
		{
			return Utils.FloorNumber(text, 3, GameDataUtils.GetChineseContent(330030, false));
		}
		return text;
	}

	private static string FloorNumber(string strNum, int figure, string postfix)
	{
		int num = strNum.get_Length() - figure;
		if (strNum.get_Chars(num) != '0')
		{
			return string.Concat(new object[]
			{
				strNum.Substring(0, num),
				".",
				strNum.get_Chars(num),
				postfix
			});
		}
		return strNum.Substring(0, num) + postfix;
	}

	public static void RemoveNull<T>(List<T> list) where T : MonoBehaviour
	{
		int count = list.get_Count();
		int i = 0;
		while (i < count)
		{
			if (!(list.get_Item(i) == null))
			{
				T t = list.get_Item(i);
				if (!(t.get_transform() == null))
				{
					i++;
					continue;
				}
			}
			int num = i++;
			while (i < count)
			{
				if (list.get_Item(i) != null)
				{
					list.set_Item(num++, list.get_Item(i));
				}
				i++;
			}
			list.RemoveRange(num, count - num);
			break;
		}
	}
}
