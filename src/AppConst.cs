using System;
using System.IO;
using UnityEngine;

public class AppConst
{
	public const string ClientVersion = "1.7.0";

	public const bool AsyncLoad = true;

	public const bool UpdateMode = false;

	public const int TimerInterval = 1;

	public const int GameFrameRateSleep = 20;

	public const int GameFrameRateSleepSecond = 300;

	public const string AppName = "LuaFramework";

	public const string LuaTempDir = "Lua/";

	public const string AppPrefix = "LuaFramework_";

	public const string ExtName = ".unity3d";

	public const string AssetDir = "StreamingAssets";

	public const string InstallPackageUrl = "http://swejackies.com.cn/Android/ceshixiazai.apk";

	public const string CodeName = "ProX.dll";

	public static bool UseAssetBundle = true;

	private static bool defaultUseLan = false;

	public static bool LocalResource = true;

	public static bool LuaByteMode = false;

	public static bool LuaBundleMode = false;

	public static float GlobalTimeScale = 1f;

	public static int GameFrameRate = 30;

	public static string ResourcePath = Application.get_persistentDataPath();

	public static string UserId = string.Empty;

	public static int SocketPort = 0;

	public static string SocketAddress = string.Empty;

	public static bool UseLAN
	{
		get
		{
			bool result = AppConst.defaultUseLan;
			if (!Application.get_isEditor() && File.Exists(AppConst.UseLanFilePath))
			{
				result = true;
			}
			return result;
		}
		set
		{
			AppConst.defaultUseLan = value;
		}
	}

	public static string UseLanFilePath
	{
		get
		{
			return AppConst.GetPath("UseLan");
		}
	}

	public static string LogOnFilePath
	{
		get
		{
			return AppConst.GetPath("LogOn");
		}
	}

	public static string IsDebugFilePath
	{
		get
		{
			return AppConst.GetPath("IsDebug");
		}
	}

	public static char[] NewLineSymbol
	{
		get
		{
			return Environment.get_NewLine().ToCharArray();
		}
	}

	public static string WebUrl
	{
		get
		{
			if (AppConst.LocalResource)
			{
				return "http://172.19.1.5/Resources/{0}/bundles/{1}/{2}/";
			}
			return "http://tqzm.453e.com/Client_Apk/tqzm/Resources/{0}/bundles/{1}/{2}/";
		}
	}

	public static string ZipUrl
	{
		get
		{
			if (AppConst.LocalResource)
			{
				return "http://172.19.1.5/Resources/{0}/{1}/";
			}
			return "http://tqzm.453e.com/Client_Apk/tqzm/Resources/{0}/{1}/";
		}
	}

	public static string ServerUrl
	{
		get
		{
			if (AppConst.LocalResource)
			{
				return "http://172.19.1.5/Resources/{0}/server_version.txt";
			}
			return "http://tqzm.453e.com/Client_Apk/tqzm/Resources/{0}/server_version.txt";
		}
	}

	public static string ServerListUrl
	{
		get
		{
			if (AppConst.UseLAN)
			{
				return "http://172.19.8.101:80/server_list/0.1.9999/hsv_ios_tq/server.csv";
			}
			return "http://tqzm.453e.com:8080/server_list/0.1.9999/hsv_ios_tq/server.csv";
		}
	}

	public static string ServerAnnoucementUrl
	{
		get
		{
			if (AppConst.UseLAN)
			{
				return "http://172.19.8.101:80/server_list/0.1.9999/hsv_ios_tq/notice.csv";
			}
			return "http://tqzm.453e.com:8080/server_list/0.1.9999/hsv_ios_tq/notice.csv";
		}
	}

	public static string FrameworkRoot
	{
		get
		{
			return string.Join("/", new string[]
			{
				Application.get_dataPath(),
				"Plugins",
				"LuaFramework"
			});
		}
	}

	private static string GetPath(string fileName)
	{
		return Path.Combine(Application.get_persistentDataPath(), fileName);
	}

	public static string GetRemoteFilePath(string place, string file, int channel = 0)
	{
		if (AppConst.UseLAN)
		{
			return string.Format("http://172.19.8.101/server_list2/{0}/{1}", place, file);
		}
		if (channel == 1)
		{
			return string.Format("http://122.226.199.218:8080/server_list2/{0}/{1}", place, file);
		}
		if (channel != 2)
		{
			return string.Format("http://tqzm.453e.com:8080/server_list2/{0}/{1}", place, file);
		}
		return string.Format("http://123.58.145.26:8080/server_list2/{0}/{1}", place, file);
	}
}
