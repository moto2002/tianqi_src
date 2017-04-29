using LuaFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class AssetBundleLoader
	{
		private const string AssetHeader = "Assets/";

		private const string ResourceHeader = "Assets/Resources/";

		public static readonly AssetBundleLoader Instance = new AssetBundleLoader();

		private AssetBundleManifest m_manifest;

		private SingleLoader m_coroutineQueue;

		public XDict<string, AssetBundleInfo> m_assetName2abInfoMap;

		public Dictionary<string, Object> m_alreadyLoaded = new Dictionary<string, Object>();

		private bool m_startToRecord;

		public List<string> m_resources = new List<string>();

		private Action<string> LoadFailed;

		private int m_loadedNum;

		public int maxConcurrencyNum = 10;

		private Dictionary<string, List<string>> m_dependencies = new Dictionary<string, List<string>>();

		private XDict<string, int> m_loadedReferenceCount = new XDict<string, int>();

		private XDict<string, List<string>> m_tracks = new XDict<string, List<string>>();

		private LogFileWriter FileRecord;

		private HashSet<string> ResNameSet;

		public SingleLoader CoroutineQueue
		{
			get
			{
				if (this.m_coroutineQueue == null)
				{
					this.m_coroutineQueue = new SingleLoader(new Func<IEnumerator, Coroutine>(GameManager.Instance.StartCoroutine));
				}
				return this.m_coroutineQueue;
			}
		}

		public bool startToRecord
		{
			get
			{
				return this.m_startToRecord;
			}
			set
			{
				if (value)
				{
					this.m_resources.Clear();
				}
				else
				{
					using (FileStream fileStream = new FileStream(Application.get_dataPath() + "/res.txt", 4, 2))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream))
						{
							this.m_resources.Sort();
							using (List<string>.Enumerator enumerator = this.m_resources.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									string current = enumerator.get_Current();
									streamWriter.WriteLine(current);
								}
							}
							streamWriter.Flush();
						}
					}
				}
				this.m_startToRecord = value;
			}
		}

		public void Initialize(Action<string> loadFailed = null)
		{
			AssetLoader.useDynamicLoader = true;
			this.LoadFailed = loadFailed;
			if (Application.get_isEditor())
			{
				if (Directory.Exists(PathSystem.GetEditorDataDir()))
				{
					AppConst.ResourcePath = PathSystem.GetEditorDataDir();
				}
				this.InitPackageFileList();
			}
			if (this.m_assetName2abInfoMap == null)
			{
				this.m_assetName2abInfoMap = SerializeUtility.DeserializeXDictFromMemory<string, AssetBundleInfo>(XUtility.GetConfigTxt("abmap", ".txt"));
			}
			this.m_manifest = (AssetLoader.LoadAssetNow("AssetBundleManifest", typeof(AssetBundleManifest)) as AssetBundleManifest);
			if (this.m_manifest == null)
			{
				Debug.LogError("manifest加载失败");
			}
		}

		public AssetBundle GetAssetBundle(string resName)
		{
			if (this.m_alreadyLoaded.ContainsKey(this.ResToABName(resName)))
			{
				return this.m_alreadyLoaded.get_Item(this.ResToABName(resName)) as AssetBundle;
			}
			Debug.LogError("缺失 " + resName);
			return null;
		}

		public string ResToABName(string resName)
		{
			if (resName.Equals("AssetBundleManifest"))
			{
				return Util.GetRuntimeFolderName(Application.get_platform()) + ".unity3d";
			}
			if (!resName.StartsWith("Assets/"))
			{
				resName = "Assets/Resources/" + resName.Replace("\\", "/");
			}
			else
			{
				resName = resName.Replace("\\", "/");
			}
			if (this.m_assetName2abInfoMap.ContainsKey(resName))
			{
				return this.m_assetName2abInfoMap[resName].filename;
			}
			Debug.LogError("找不到 " + resName);
			return string.Empty;
		}

		public void AsyncLoadAB(string resName, Action<bool> totalCallback, Action<float> progressCallback = null)
		{
			if (this.m_startToRecord)
			{
				this.m_resources.Add(resName);
			}
			string text = this.ResToABName(resName);
			if (string.IsNullOrEmpty(text))
			{
				totalCallback.Invoke(false);
				return;
			}
			this.CoroutineQueue.Enqueue(this.AsyncOnLoadAB(text, totalCallback, progressCallback));
		}

		public void UnloadUnusedAssets(Action callback)
		{
			this.CoroutineQueue.Enqueue(this.AsyncOnDoing(Resources.UnloadUnusedAssets(), callback));
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnDoing(AsyncOperation async, Action callback)
		{
			AssetBundleLoader.<AsyncOnDoing>c__Iterator1B <AsyncOnDoing>c__Iterator1B = new AssetBundleLoader.<AsyncOnDoing>c__Iterator1B();
			<AsyncOnDoing>c__Iterator1B.async = async;
			<AsyncOnDoing>c__Iterator1B.callback = callback;
			<AsyncOnDoing>c__Iterator1B.<$>async = async;
			<AsyncOnDoing>c__Iterator1B.<$>callback = callback;
			return <AsyncOnDoing>c__Iterator1B;
		}

		private bool SyncOnLoadAB(string abName)
		{
			List<string> dependencies = this.GetDependencies(abName);
			bool result = true;
			using (List<string>.Enumerator enumerator = dependencies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.get_Current();
					if (!this.m_alreadyLoaded.ContainsKey(current))
					{
						this.OnPreAssetBundleLoad(current);
						AssetBundle assetBundle = AssetBundle.LoadFromFile(Util.GetValidAssetBundlePath(current));
						if (!(assetBundle != null))
						{
							result = false;
							Debug.LogError("无法加载 " + current);
							if (this.LoadFailed != null)
							{
								this.LoadFailed.Invoke(current);
							}
							break;
						}
						this.m_alreadyLoaded.Add(current, assetBundle);
					}
					this.AddReference(current);
				}
			}
			return result;
		}

		[DebuggerHidden]
		private IEnumerator LoadFromFileAsync(string abName, Action<string, AssetBundle> callback)
		{
			AssetBundleLoader.<LoadFromFileAsync>c__Iterator1C <LoadFromFileAsync>c__Iterator1C = new AssetBundleLoader.<LoadFromFileAsync>c__Iterator1C();
			<LoadFromFileAsync>c__Iterator1C.abName = abName;
			<LoadFromFileAsync>c__Iterator1C.callback = callback;
			<LoadFromFileAsync>c__Iterator1C.<$>abName = abName;
			<LoadFromFileAsync>c__Iterator1C.<$>callback = callback;
			<LoadFromFileAsync>c__Iterator1C.<>f__this = this;
			return <LoadFromFileAsync>c__Iterator1C;
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnLoadAB(string abName, Action<bool> totalCallback, Action<float> progressCallback)
		{
			AssetBundleLoader.<AsyncOnLoadAB>c__Iterator1D <AsyncOnLoadAB>c__Iterator1D = new AssetBundleLoader.<AsyncOnLoadAB>c__Iterator1D();
			<AsyncOnLoadAB>c__Iterator1D.abName = abName;
			<AsyncOnLoadAB>c__Iterator1D.progressCallback = progressCallback;
			<AsyncOnLoadAB>c__Iterator1D.totalCallback = totalCallback;
			<AsyncOnLoadAB>c__Iterator1D.<$>abName = abName;
			<AsyncOnLoadAB>c__Iterator1D.<$>progressCallback = progressCallback;
			<AsyncOnLoadAB>c__Iterator1D.<$>totalCallback = totalCallback;
			<AsyncOnLoadAB>c__Iterator1D.<>f__this = this;
			return <AsyncOnLoadAB>c__Iterator1D;
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnUnloadAB(string abName, Action<bool> totalCallback, Action<float> progressCallback)
		{
			AssetBundleLoader.<AsyncOnUnloadAB>c__Iterator1E <AsyncOnUnloadAB>c__Iterator1E = new AssetBundleLoader.<AsyncOnUnloadAB>c__Iterator1E();
			<AsyncOnUnloadAB>c__Iterator1E.abName = abName;
			<AsyncOnUnloadAB>c__Iterator1E.progressCallback = progressCallback;
			<AsyncOnUnloadAB>c__Iterator1E.totalCallback = totalCallback;
			<AsyncOnUnloadAB>c__Iterator1E.<$>abName = abName;
			<AsyncOnUnloadAB>c__Iterator1E.<$>progressCallback = progressCallback;
			<AsyncOnUnloadAB>c__Iterator1E.<$>totalCallback = totalCallback;
			<AsyncOnUnloadAB>c__Iterator1E.<>f__this = this;
			return <AsyncOnUnloadAB>c__Iterator1E;
		}

		public void CheckABCountZero()
		{
			for (int i = 0; i < this.m_loadedReferenceCount.Count; i++)
			{
				string text = this.m_loadedReferenceCount.ElementKeyAt(i);
				if (this.m_loadedReferenceCount[text] <= 0 && this.m_alreadyLoaded.ContainsKey(text))
				{
					this.m_loadedReferenceCount.Remove(text);
					this.ClearTracks(text);
					(this.m_alreadyLoaded.get_Item(text) as AssetBundle).Unload(false);
					this.m_alreadyLoaded.Remove(text);
				}
			}
		}

		private void SyncUnloadAB(string abName)
		{
			if (this.IsInWhiltelist(abName))
			{
				return;
			}
			if (!this.m_loadedReferenceCount.ContainsKey(abName))
			{
				Debug.LogWarning("Error尚无引用计数存在 " + abName);
				return;
			}
			if (this.m_loadedReferenceCount[abName] <= 0)
			{
				Debug.LogWarning("Error引用计数错误 " + abName);
				return;
			}
			List<string> dependencies = this.GetDependencies(abName);
			int num;
			for (int i = 0; i < dependencies.get_Count(); i++)
			{
				if (this.m_loadedReferenceCount.ContainsKey(dependencies.get_Item(i)))
				{
					XDict<string, int> loadedReferenceCount;
					XDict<string, int> expr_7E = loadedReferenceCount = this.m_loadedReferenceCount;
					string key;
					string expr_87 = key = dependencies.get_Item(i);
					num = loadedReferenceCount[key];
					expr_7E[expr_87] = num - 1;
				}
			}
			XDict<string, int> loadedReferenceCount2;
			XDict<string, int> expr_B1 = loadedReferenceCount2 = this.m_loadedReferenceCount;
			num = loadedReferenceCount2[abName];
			expr_B1[abName] = num - 1;
			this.CheckABCountZero();
		}

		public void UnloadAB(string resName, Action<bool> totalCallback, Action<float> loadingPercent = null)
		{
			string text = this.ResToABName(resName);
			if (string.IsNullOrEmpty(text))
			{
				if (totalCallback != null)
				{
					totalCallback.Invoke(false);
				}
				return;
			}
			this.CoroutineQueue.Enqueue(this.AsyncOnUnloadAB(text, totalCallback, loadingPercent));
		}

		public bool SyncLoadAB(string resName)
		{
			if (this.CoroutineQueue.isBusy())
			{
				Debug.LogWarning("==>an asynchronous task is executing!!!");
			}
			if (this.m_startToRecord)
			{
				this.m_resources.Add(resName);
			}
			string text = this.ResToABName(resName);
			return !string.IsNullOrEmpty(text) && this.SyncOnLoadAB(text);
		}

		private bool isAllLoaded(string[] dependencies)
		{
			for (int i = 0; i < dependencies.Length; i++)
			{
				if (!string.IsNullOrEmpty(dependencies[i]) && !this.m_alreadyLoaded.ContainsKey(dependencies[i]))
				{
					return false;
				}
			}
			return true;
		}

		private List<string> GetDependencies(string abName)
		{
			if (this.m_manifest == null)
			{
				List<string> list = new List<string>();
				list.Add(abName);
				return list;
			}
			if (!this.m_dependencies.ContainsKey(abName))
			{
				this.m_dependencies.set_Item(abName, new List<string>(this.m_manifest.GetAllDependencies(abName)));
				this.m_dependencies.get_Item(abName).Add(abName);
			}
			return this.m_dependencies.get_Item(abName);
		}

		private void MakeTracks(string abName)
		{
		}

		private void ClearTracks(string abName)
		{
		}

		public void OutputStackTrace(string ab_name)
		{
			if (!this.m_tracks.ContainsKey(ab_name))
			{
				Debug.LogError("assetbundle cache no contains ab, assetbundle name = " + ab_name);
				return;
			}
			using (List<string>.Enumerator enumerator = this.m_tracks[ab_name].GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.get_Current();
					Debug.LogError(current);
				}
			}
		}

		public int GetLoadedReferenceCount(string resName)
		{
			if (!this.m_loadedReferenceCount.ContainsKey(this.ResToABName(resName)))
			{
				return 0;
			}
			return this.m_loadedReferenceCount[this.ResToABName(resName)];
		}

		public bool isLoaded(string resName)
		{
			return this.m_alreadyLoaded.ContainsKey(this.ResToABName(resName));
		}

		private void AddReference(string abName)
		{
			if (!this.m_loadedReferenceCount.ContainsKey(abName))
			{
				this.m_loadedReferenceCount.Add(abName, 0);
			}
			XDict<string, int> loadedReferenceCount;
			XDict<string, int> expr_24 = loadedReferenceCount = this.m_loadedReferenceCount;
			int num = loadedReferenceCount[abName];
			expr_24[abName] = num + 1;
			this.MakeTracks(abName);
		}

		private bool IsInWhiltelist(string abName)
		{
			return abName.Contains("shader/shader") || abName.Contains("camera.unity3d") || abName.Contains("sszh.unity3d");
		}

		private void InitPackageFileList()
		{
			if (Singleton<EditorConfig>.S.Data.IsLoadRecordEnable)
			{
				string subPackageInfoPath = PathSystem.GetSubPackageInfoPath(PathSystem.SubPackageInfoFile.CoreList);
				if (File.Exists(subPackageInfoPath))
				{
					this.ResNameSet = new HashSet<string>(File.ReadAllLines(subPackageInfoPath));
				}
				else
				{
					this.ResNameSet = new HashSet<string>();
				}
				this.FileRecord = new LogFileWriter();
				this.FileRecord.Init(subPackageInfoPath, 6);
			}
		}

		private void OnPreAssetBundleLoad(string resName)
		{
			if (this.FileRecord != null)
			{
				resName = resName.ToLower();
				if (this.ResNameSet.Contains(resName))
				{
					return;
				}
				this.FileRecord.WriteLog(resName);
			}
		}

		public void Release()
		{
			if (this.FileRecord != null)
			{
				this.FileRecord.Release();
				this.FileRecord = null;
			}
		}
	}
}
