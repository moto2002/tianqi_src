using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace LuaFramework
{
	public class Util
	{
		private static List<string> luaPaths = new List<string>();

		public static string DataPath
		{
			get
			{
				if (Application.get_platform() == 2 || Application.get_platform() == 1)
				{
					return Application.get_streamingAssetsPath() + "/";
				}
				return Application.get_persistentDataPath() + "/";
			}
		}

		public static bool NetAvailable
		{
			get
			{
				return Application.get_internetReachability() != 0;
			}
		}

		public static bool IsWifi
		{
			get
			{
				return Application.get_internetReachability() == 2;
			}
		}

		public static int Int(object o)
		{
			return Convert.ToInt32(o);
		}

		public static float Float(object o)
		{
			return (float)Math.Round((double)Convert.ToSingle(o), 2);
		}

		public static long Long(object o)
		{
			return Convert.ToInt64(o);
		}

		public static int Random(int min, int max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		public static float Random(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		public static string Uid(string uid)
		{
			int num = uid.LastIndexOf('_');
			return uid.Remove(0, num + 1);
		}

		public static long GetTime()
		{
			long arg_27_0 = DateTime.get_UtcNow().get_Ticks();
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
			TimeSpan timeSpan = new TimeSpan(arg_27_0 - dateTime.get_Ticks());
			return (long)timeSpan.get_TotalMilliseconds();
		}

		public static T Get<T>(GameObject go, string subnode) where T : Component
		{
			if (go != null)
			{
				Transform transform = go.get_transform().FindChild(subnode);
				if (transform != null)
				{
					return transform.GetComponent<T>();
				}
			}
			return (T)((object)null);
		}

		public static T Get<T>(Transform go, string subnode) where T : Component
		{
			if (go != null)
			{
				Transform transform = go.FindChild(subnode);
				if (transform != null)
				{
					return transform.GetComponent<T>();
				}
			}
			return (T)((object)null);
		}

		public static T Get<T>(Component go, string subnode) where T : Component
		{
			return go.get_transform().FindChild(subnode).GetComponent<T>();
		}

		public static T Add<T>(GameObject go) where T : Component
		{
			if (go != null)
			{
				T[] components = go.GetComponents<T>();
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i] != null)
					{
						Object.Destroy(components[i]);
					}
				}
				return go.get_gameObject().AddComponent<T>();
			}
			return (T)((object)null);
		}

		public static T Add<T>(Transform go) where T : Component
		{
			return Util.Add<T>(go.get_gameObject());
		}

		public static GameObject Child(GameObject go, string subnode)
		{
			return Util.Child(go.get_transform(), subnode);
		}

		public static GameObject Child(Transform go, string subnode)
		{
			Transform transform = go.FindChild(subnode);
			if (transform == null)
			{
				return null;
			}
			return transform.get_gameObject();
		}

		public static GameObject Peer(GameObject go, string subnode)
		{
			return Util.Peer(go.get_transform(), subnode);
		}

		public static GameObject Peer(Transform go, string subnode)
		{
			Transform transform = go.get_parent().FindChild(subnode);
			if (transform == null)
			{
				return null;
			}
			return transform.get_gameObject();
		}

		public static string md5(string source)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.get_UTF8().GetBytes(source);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes, 0, bytes.Length);
			mD5CryptoServiceProvider.Clear();
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				text += Convert.ToString(array[i], 16).PadLeft(2, '0');
			}
			return text.PadLeft(32, '0');
		}

		public static string md5file(string file)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(file, 3, 1, 1);
				MD5 mD = new MD5CryptoServiceProvider();
				byte[] array = mD.ComputeHash(fileStream);
				fileStream.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Debug.LogFormat("md5file ex :{0}", new object[]
				{
					ex
				});
				throw new Exception("md5file() fail, error:" + ex.get_Message());
			}
			return result;
		}

		public static void ClearChild(Transform go)
		{
			if (go == null)
			{
				return;
			}
			for (int i = go.get_childCount() - 1; i >= 0; i--)
			{
				Object.Destroy(go.GetChild(i).get_gameObject());
			}
		}

		public static void ClearMemory()
		{
		}

		public static string GetRelativePath()
		{
			if (Application.get_isEditor())
			{
				return "file://" + Environment.get_CurrentDirectory().Replace("\\", "/") + "/Assets/StreamingAssets/";
			}
			if (Application.get_isMobilePlatform() || Application.get_isConsolePlatform())
			{
				return "file:///" + Util.DataPath;
			}
			return "file://" + Application.get_streamingAssetsPath() + "/";
		}

		public static string AppContentPath()
		{
			string result = string.Empty;
			switch (Application.get_platform())
			{
			case 8:
				result = Application.get_dataPath() + "/Raw/";
				return result;
			case 11:
				result = "jar:file://" + Application.get_dataPath() + "!/assets/";
				return result;
			}
			result = Application.get_streamingAssetsPath() + "/";
			return result;
		}

		public static string GetIndexName()
		{
			return "files.txt";
		}

		public static string GetRuntimeFolderName(RuntimePlatform platform)
		{
			switch (platform)
			{
			case 0:
			case 1:
				return "osx";
			case 2:
			case 7:
				return "windows";
			case 3:
			case 5:
				return "webplayer";
			case 8:
				return "ios";
			case 11:
				return "android";
			}
			Debug.Log("Target not implemented.");
			return null;
		}

		public static string GetValidAssetBundlePath(string resName)
		{
			resName = resName.ToLower();
			string text = Path.Combine(AppConst.ResourcePath, Path.Combine(Util.GetRuntimeFolderName(Application.get_platform()).ToLower(), resName));
			if (File.Exists(text))
			{
				return text;
			}
			return Path.Combine(Application.get_dataPath() + "/assets", Path.Combine(Util.GetRuntimeFolderName(Application.get_platform()).ToLower(), resName));
		}

		public static string GetFileText(string path)
		{
			return File.ReadAllText(path);
		}

		public static void Log(string str)
		{
			Debug.Log(str);
		}

		public static void LogWarning(string str)
		{
			Debug.LogWarning(str);
		}

		public static void LogError(string str)
		{
			Debug.LogError(str);
		}

		public static int CheckRuntimeFile()
		{
			if (!Application.get_isEditor())
			{
				return 0;
			}
			string text = Application.get_dataPath() + "/StreamingAssets/";
			if (!Directory.Exists(text))
			{
				return -1;
			}
			string[] files = Directory.GetFiles(text);
			if (files.Length == 0)
			{
				return -1;
			}
			if (!File.Exists(text + Util.GetIndexName()))
			{
				return -1;
			}
			string text2 = AppConst.FrameworkRoot + "/ToLua/Source/Generate/";
			if (!Directory.Exists(text2))
			{
				return -2;
			}
			string[] files2 = Directory.GetFiles(text2);
			if (files2.Length == 0)
			{
				return -2;
			}
			return 0;
		}

		public static object[] CallMethod(string module, string func, params object[] args)
		{
			LuaManager manager = AppFacade.Instance.GetManager<LuaManager>("LuaManager");
			if (manager == null)
			{
				return null;
			}
			return manager.CallFunction(module + "." + func, args);
		}

		public static bool CheckEnvironment()
		{
			return true;
		}
	}
}
