using System;
using System.IO;
using UnityEngine;

public class NativeCallTools
{
	private static readonly string CLASS_MAINACTIVITY = "com.unity3d.player.UnityPlayer";

	private static AndroidJavaObject mAndroidJO;

	private static byte[] cache;

	private static byte[] cache2;

	private static AndroidJavaObject mPackageManager;

	public static AndroidJavaObject AndroidJO
	{
		get
		{
			if (NativeCallTools.mAndroidJO == null)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass(NativeCallTools.CLASS_MAINACTIVITY);
				NativeCallTools.mAndroidJO = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			return NativeCallTools.mAndroidJO;
		}
	}

	public static AndroidJavaObject PackageManager
	{
		get
		{
			if (NativeCallTools.mPackageManager == null)
			{
				NativeCallTools.mPackageManager = NativeCallTools.AndroidJO.Call<AndroidJavaObject>("getPackageManager", new object[0]);
			}
			return NativeCallTools.mPackageManager;
		}
	}

	public static byte[] GetMemoryOfAb(string path)
	{
		NativeCallTools.cache = NativeCallTools.AndroidJO.CallStatic<byte[]>("getBytes", new object[]
		{
			Path.Combine(ABUtility.GetPlatformFolderForAssetBundles(), path)
		});
		return NativeCallTools.cache;
	}

	public static byte[] GetMemoryOfPath(string path)
	{
		NativeCallTools.cache2 = NativeCallTools.AndroidJO.CallStatic<byte[]>("getBytes", new object[]
		{
			path
		});
		return NativeCallTools.cache2;
	}

	public static void InstallAPK(string path)
	{
		NativeCallTools.AndroidJO.Call("installApk", new object[]
		{
			path
		});
	}

	public static bool IsWifiOn()
	{
		return NativeCallTools.AndroidJO.Call<bool>("isWifiOn", new object[0]);
	}

	public static int GetNetworkRssi()
	{
		return NativeCallTools.AndroidJO.Call<int>("getNetworkRssi", new object[0]);
	}

	public static void RegisterPush(string account)
	{
		NativeCallTools.AndroidJO.Call("registerPush", new object[]
		{
			account
		});
	}

	public static int GetPushID()
	{
		return NativeCallTools.AndroidJO.Call<int>("getPushID", new object[0]);
	}

	public static void SetPushTag(string tag)
	{
		NativeCallTools.AndroidJO.Call("setPushTag", new object[]
		{
			tag
		});
	}

	public static void DeletePushTag(string tag)
	{
		NativeCallTools.AndroidJO.Call("deletePushTag", new object[]
		{
			tag
		});
	}

	public static void QueryUpdate()
	{
		NativeCallTools.AndroidJO.Call("queryUpdate", new object[0]);
	}

	public static void Restart()
	{
		NativeCallTools.AndroidJO.Call("restart", new object[0]);
	}
}
