using System;
using System.Collections.Generic;
using UnityEngine;

public class NativeCallManager : MonoBehaviour
{
	private const string PUSH_ID_NAME = "LOCAL_PUSH_ID";

	private static GameObject _UnityNativeBridgeRoot;

	private static NativeCallManager instance;

	private static List<int> m_allIds;

	public static bool m_isTest = false;

	private static string ApkMd5 = string.Empty;

	public static GameObject UnityNativeBridgeRoot
	{
		get
		{
			if (NativeCallManager._UnityNativeBridgeRoot == null)
			{
				NativeCallManager._UnityNativeBridgeRoot = new GameObject("UnityNativeBridgeRoot");
				Object.DontDestroyOnLoad(NativeCallManager._UnityNativeBridgeRoot);
			}
			return NativeCallManager._UnityNativeBridgeRoot;
		}
	}

	public static NativeCallManager Instance
	{
		get
		{
			if (NativeCallManager.instance == null)
			{
				NativeCallManager.instance = NativeCallManager.UnityNativeBridgeRoot.AddComponent<NativeCallManager>();
				LocalForAndroidManager.Instance.Init();
			}
			return NativeCallManager.instance;
		}
	}

	public void Init()
	{
	}

	public static void InstallPackage(string path)
	{
		Debug.Log("call enjoy's code");
		NativeCallTools.InstallAPK(path);
	}

	public static bool Native_CheckPermissionRecordAudio()
	{
		return NativeCallTools.AndroidJO.Call<bool>("checkPermissionRecordAudio", new object[0]);
	}

	public static void Native_StartRecording(string filePath)
	{
		NativeCallTools.AndroidJO.CallStatic("startRecording", new object[]
		{
			filePath
		});
	}

	public static void Native_StopRecording()
	{
		NativeCallTools.AndroidJO.CallStatic("stopRecording", new object[0]);
	}

	public static void Native_CancelRecording()
	{
		NativeCallTools.AndroidJO.CallStatic("cancelRecording", new object[0]);
	}

	public static bool Native_RecordFinished()
	{
		return NativeCallTools.AndroidJO.CallStatic<bool>("recordFinished", new object[0]);
	}

	public static void Native_PlayRecording(string filePath)
	{
		if (NativeCallManager.m_isTest)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				@static.CallStatic("playRecording", new object[]
				{
					filePath
				});
			}
			return;
		}
		NativeCallTools.AndroidJO.CallStatic("playRecording", new object[]
		{
			filePath
		});
	}

	public static void Native_StopPlayRecording()
	{
		if (NativeCallManager.m_isTest)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				@static.CallStatic("stopPlayRecording", new object[0]);
			}
			return;
		}
		NativeCallTools.AndroidJO.CallStatic("stopPlayRecording", new object[0]);
	}

	public static bool Native_IsPlayRecordingFinished()
	{
		return NativeCallTools.AndroidJO.CallStatic<bool>("playFinished", new object[0]);
	}

	public static double Native_GetVolume()
	{
		return NativeCallTools.AndroidJO.CallStatic<double>("getVolume", new object[0]);
	}

	public static bool IsWifiOn()
	{
		return NativeCallTools.IsWifiOn();
	}

	public static int GetNetworkRssi()
	{
		return NativeCallTools.GetNetworkRssi();
	}

	public static void RegisterPush(string account)
	{
		NativeCallTools.RegisterPush(account);
	}

	public static int GetPushID()
	{
		Debug.Log("===============>GetPushID : " + NativeCallTools.GetPushID());
		return NativeCallTools.GetPushID();
	}

	public void ReceiveDeviceToken(string deviceToken)
	{
		Debug.Log("===============>deviceToken : " + deviceToken);
		PushNotificationManager.Instance.SendUpdateXGToken(deviceToken);
	}

	public void ReceiveRegisterSuccess(string arg01)
	{
		Debug.Log("===============>ReceiveRegisterSuccess");
		PushNotificationManager.Instance.RefreshAllPushTag();
	}

	public static void SetPushTag(string tag)
	{
		NativeCallTools.SetPushTag(tag);
	}

	public static void DeletePushTag(string tag)
	{
		NativeCallTools.DeletePushTag(tag);
	}

	private static void SavePushIDs(int push_id)
	{
		if (NativeCallManager.m_allIds == null)
		{
			NativeCallManager.m_allIds = new List<int>();
		}
		if (!NativeCallManager.m_allIds.Contains(push_id))
		{
			NativeCallManager.m_allIds.Add(push_id);
		}
	}

	public static void SavePushIDs()
	{
		if (NativeCallManager.m_allIds == null)
		{
			return;
		}
		string text = string.Empty;
		for (int i = 0; i < NativeCallManager.m_allIds.get_Count(); i++)
		{
			if (i == 0)
			{
				text += NativeCallManager.m_allIds.get_Item(i).ToString();
			}
			else
			{
				text = text + ";" + NativeCallManager.m_allIds.get_Item(i).ToString();
			}
		}
		PlayerPrefs.SetString("LOCAL_PUSH_ID", text);
	}

	public static List<int> GetPushIDs()
	{
		List<int> list = new List<int>();
		string @string = PlayerPrefs.GetString("LOCAL_PUSH_ID", string.Empty);
		if (!string.IsNullOrEmpty(@string))
		{
			string[] array = @string.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(int.Parse(array[i]));
			}
		}
		return list;
	}

	public static void NotificationMessage(int push_id, string message, int hour, int minute, NotificationRepeatInterval repeatInterval)
	{
		NativeCallManager.SavePushIDs(push_id);
		LocalForAndroidManager.NotificationMessage(push_id, message, hour, minute, repeatInterval);
	}

	public static void NotificationMessage(int push_id, string message, DateTime newDate, NotificationRepeatInterval repeatInterval)
	{
		NativeCallManager.SavePushIDs(push_id);
		LocalForAndroidManager.NotificationMessage(push_id, message, newDate, repeatInterval);
	}

	public void ReceiveNativeLog(string logString)
	{
		Debug.LogError("===============>ReceiveNativeLog: " + logString);
	}

	public static bool ContainsInAssets(string dir, string fileName)
	{
		return NativeCallManager.CallSdkApi<bool>("containsInAssets", new object[]
		{
			dir,
			fileName
		});
	}

	public static byte[] getFromAssets(string fileName)
	{
		return NativeCallManager.CallSdkApi<byte[]>("getFromAssets", new object[]
		{
			fileName
		});
	}

	public static string GetDeviceIMEI()
	{
		return NativeCallManager.CallSdkApi<string>("getDeviceID", new object[0]);
	}

	public static void CallSdkApi(string apiName, params object[] args)
	{
		if (NativeCallManager.m_isTest)
		{
			Debug.Log("Unity3d " + apiName + " calling...");
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				@static.Call(apiName, args);
			}
		}
		else
		{
			Debug.Log("Unity3d " + apiName + " calling...");
			NativeCallTools.AndroidJO.Call(apiName, args);
		}
	}

	public static T CallSdkApi<T>(string apiName, params object[] args)
	{
		if (NativeCallManager.m_isTest)
		{
			Debug.Log("Unity3d " + apiName + " calling...");
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				return @static.Call<T>(apiName, args);
			}
		}
		return NativeCallTools.AndroidJO.Call<T>(apiName, args);
	}

	public static int GetSDKType()
	{
		if (NativeCallManager.m_isTest)
		{
			Debug.Log("Unity3d getSDKType calling...");
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				return @static.Call<int>("getSDKType", new object[0]);
			}
		}
		return NativeCallTools.AndroidJO.Call<int>("getSDKType", new object[0]);
	}

	public static bool GetIsLogin()
	{
		return NativeCallTools.AndroidJO.Call<bool>("isLogin", new object[0]);
	}

	public static bool ApplicationQuitSDK()
	{
		return NativeCallTools.AndroidJO.Call<bool>("applicationQuit", new object[0]);
	}

	public static string GetAndroidPackageName()
	{
		string empty = string.Empty;
		AndroidJavaObject androidJavaObject = NativeCallTools.AndroidJO.Call<AndroidJavaObject>("getPackageName", new object[0]);
		return androidJavaObject.Call<string>("toString", new object[0]);
	}

	public static byte[] GetAndroidSignatures()
	{
		byte[] result = null;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.pm.PackageManager");
			int @static = androidJavaClass.GetStatic<int>("GET_SIGNATURES");
			AndroidJavaObject androidJavaObject = NativeCallTools.PackageManager.Call<AndroidJavaObject>("getPackageInfo", new object[]
			{
				NativeCallManager.GetAndroidPackageName(),
				@static
			});
			AndroidJavaObject[] array = androidJavaObject.Get<AndroidJavaObject[]>("signatures");
			if (array != null && array.Length > 0)
			{
				result = array[0].Call<byte[]>("toByteArray", new object[0]);
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			return null;
		}
		return result;
	}

	public static string GetApkPath()
	{
		return PathSystem.DataPath;
	}

	public static void GetApkMd5Async(Action<string> OnGetCallback)
	{
		if (string.IsNullOrEmpty(NativeCallManager.ApkMd5))
		{
			Debug.Log("GetApkMd5Async begin");
			string dataPath = NativeCallManager.GetApkPath();
			Debug.LogFormat("dataPath :{0}", new object[]
			{
				dataPath
			});
			Loom.Current.RunAsync(delegate
			{
				NativeCallManager.ApkMd5 = MD5Util.EncryptFile(dataPath);
				Debug.LogFormat("apk md5 :{0}", new object[]
				{
					NativeCallManager.ApkMd5
				});
				if (OnGetCallback != null)
				{
					OnGetCallback.Invoke(NativeCallManager.ApkMd5);
					Debug.Log("GetApkMd5Async end");
				}
			});
		}
		else if (OnGetCallback != null)
		{
			OnGetCallback.Invoke(NativeCallManager.ApkMd5);
		}
	}

	public static string GetApkMd5()
	{
		Debug.Log("GetApkMd5 begin");
		if (string.IsNullOrEmpty(NativeCallManager.ApkMd5))
		{
			NativeCallManager.ApkMd5 = MD5Util.EncryptFile(PathSystem.DataPath);
		}
		Debug.Log("GetApkMd5 end");
		return NativeCallManager.ApkMd5;
	}

	public static int GetVersionCode()
	{
		int result = 0;
		try
		{
			AndroidJavaObject androidJavaObject = NativeCallTools.PackageManager.Call<AndroidJavaObject>("getPackageInfo", new object[]
			{
				NativeCallManager.GetAndroidPackageName(),
				0
			});
			result = androidJavaObject.Get<int>("versionCode");
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			return 0;
		}
		return result;
	}

	public static void QueryUpdate()
	{
		NativeCallTools.QueryUpdate();
	}

	public static void Restart()
	{
		NativeCallTools.Restart();
	}
}
