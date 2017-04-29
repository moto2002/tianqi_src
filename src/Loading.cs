using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;

public class Loading
{
	private static Loading instance;

	private bool isLoading;

	private bool IsPreloadResourceOn = true;

	private Action action_allResource_loaded;

	public bool isCurrentHasLoading = true;

	private int scene_loaded_num;

	private int async_release_num;

	private uint action_allResource_loaded_timer_id;

	private List<Action> actions_need_deal = new List<Action>();

	public static Loading Instance
	{
		get
		{
			if (Loading.instance == null)
			{
				Loading.instance = new Loading();
			}
			return Loading.instance;
		}
	}

	public bool IsLoading
	{
		get
		{
			return this.isLoading;
		}
	}

	public void OnStartLoad(int sceneID, List<MapObjInfo> otherObjs)
	{
		if (this.action_allResource_loaded_timer_id > 0u)
		{
			TimerHeap.DelTimer(this.action_allResource_loaded_timer_id);
			this.action_allResource_loaded_timer_id = 0u;
		}
		SoundManager.Instance.StopBGM(null);
		EntityWorld.Instance.ClearAllMapObjects();
		NPCManager.Instance.ClearAllNPCs();
		EventDispatcher.Broadcast<int, int>(SceneManagerEvent.UnloadScene, MySceneManager.Instance.CurSceneID, sceneID);
		if (!this.IsWorldMapSwitch(sceneID))
		{
			if (InstanceManager.CurrentInstanceType == InstanceType.Arena)
			{
				UIManagerControl.Instance.HideAllExcept(new string[]
				{
					"LoadingUI",
					"PVPVSUI"
				});
			}
			else
			{
				UIManagerControl.Instance.HideAllExcept(new string[]
				{
					"LoadingUI"
				});
			}
			LoadingUIView.Open(false);
			this.isCurrentHasLoading = true;
			this.TrulyStartLoad(sceneID, otherObjs);
		}
		else
		{
			this.isCurrentHasLoading = false;
			FXSpineManager.Instance.ShowBattleStart1(sceneID, delegate
			{
				AssetLoader.UnloadUnusedAssets(delegate
				{
					this.TrulyStartLoad(sceneID, otherObjs);
				});
			});
		}
	}

	private void TrulyStartLoad(int sceneID, List<MapObjInfo> otherObjs)
	{
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.CheckWeaponSlot();
			if (!EntityWorld.Instance.EntSelf.IsInBattle)
			{
				InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
			}
		}
		if (EntityWorld.Instance.ActSelf != null)
		{
			EntityWorld.Instance.ActSelf.ResetController();
		}
		EventDispatcher.Broadcast<int, int>(SceneManagerEvent.LoadSceneStart, MySceneManager.Instance.CurSceneID, sceneID);
		AOIService.Instance.SetMapArrivedObj(otherObjs);
		this.isLoading = true;
		this.EnterPreloadResource(sceneID);
	}

	public bool IsWorldMapSwitch(int sceneID)
	{
		return MySceneManager.IsCityWildScene(sceneID) || MySceneManager.IsCityWildScene(MySceneManager.Instance.CurSceneID);
	}

	private void OnLoadEnd(int sceneID)
	{
		Debug.LogFormat("OnLoadEnd sceneID: {0}", new object[]
		{
			sceneID
		});
		MySceneManager.Instance.CurSceneID = sceneID;
		MySceneManager.Instance.IsSceneExist = true;
		this.isLoading = false;
		EventDispatcher.Broadcast<int>(SceneManagerEvent.LoadSceneEnd, MySceneManager.Instance.CurSceneID);
		Light[] array = Object.FindObjectsOfType<Light>();
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].get_name().Equals("scenelight"))
			{
				array[i].set_enabled(false);
			}
		}
		WaveBloodManager.Instance.PreInstantiate();
	}

	private void EnterPreloadResource(int sceneID)
	{
		LoadingRes.Instance.ResetLoadDatas();
		LoadingRes.ExtractInstanceAtlasOfAllUi();
		if (this.IsPreloadResourceOn && !this.IsWorldMapSwitch(sceneID))
		{
			InstanceManager.PreLoadData(sceneID);
		}
		LoadingRes.ExtractInstanceUiPrefab();
		LoadingRes.ExtractInstanceFXSpines();
		LoadingRes.Instance.InitResourceCounter();
		this.loadmode1_scene_action_allresource(sceneID);
	}

	private void loadmode1_scene_action_allresource(int sceneID)
	{
		this.action_allResource_loaded = delegate
		{
			Debug.Log("=>预加载完成(allResources)");
			this.action_allResource_loaded = null;
			this.OnLoadEnd(sceneID);
		};
		LoadingRes.total_resource_num += 10;
		this.LoadScene(sceneID, delegate
		{
			if (!this.IsWorldMapSwitch(sceneID))
			{
				CameraGlobal.DestroyCamera();
			}
			this.DealActionsBeforePreloadRes();
			this.PreloadFinish(10);
			if (this.IsPreloadResourceOn && !this.IsWorldMapSwitch(sceneID))
			{
				LoadingRes.Instance.PreloadAllResource();
			}
			else if (this.action_allResource_loaded != null)
			{
				this.action_allResource_loaded.Invoke();
			}
		});
	}

	private void LoadScene(int sceneID, Action callback)
	{
		Debug.LogFormat("LoadScene ID: {0}", new object[]
		{
			sceneID
		});
		Scene scene = DataReader<Scene>.Get(sceneID);
		if (scene == null)
		{
			Debug.LogError("GameData.Scene no exist, id = " + sceneID);
			return;
		}
		string newSceneName = scene.path;
		string newSubSceneName = scene.navPath;
		if (MySceneManager.Instance.CurSceneID == 0)
		{
			LoadingRes.total_resource_num += 600;
			this.scene_loaded_num = 0;
			this.LoadMainScene(sceneID, newSceneName, delegate
			{
				this.LoadSubScene(newSubSceneName, delegate
				{
					callback.Invoke();
				});
			});
		}
		else
		{
			Scene scene2 = DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID);
			if (scene2 == null)
			{
				Debug.LogError("GameData.Scene no exist, id = " + MySceneManager.Instance.CurSceneID);
				return;
			}
			string navPath = scene2.navPath;
			string oldSceneName = scene2.path;
			if (newSceneName != oldSceneName)
			{
				LoadingRes.total_resource_num += 600;
				LoadingRes.total_resource_num += 100;
				this.scene_loaded_num = 0;
				this.async_release_num = 0;
				this.UnloadSubScene(navPath, delegate
				{
					this.UnloadMainScene(sceneID, oldSceneName, delegate
					{
						this.LoadMainScene(sceneID, newSceneName, delegate
						{
							this.LoadSubScene(newSubSceneName, delegate
							{
								callback.Invoke();
							});
						});
					});
				});
			}
			else if (navPath != newSubSceneName)
			{
				this.UnloadSubScene(navPath, delegate
				{
					this.LoadSubScene(newSubSceneName, delegate
					{
						callback.Invoke();
					});
				});
			}
			else
			{
				callback.Invoke();
			}
		}
	}

	protected void UnloadMainScene(int sceneID, string name, Action callback)
	{
		AssetLoader.UnloadScene(name, delegate(bool isSuccess)
		{
			EventDispatcher.Broadcast<int, int>(SceneManagerEvent.SceneRelatedUnload, MySceneManager.Instance.CurSceneID, sceneID);
			AssetLoader.UnloadUnusedAssets(delegate
			{
				this.PreloadAsyncReleaseChange(1f);
				callback.Invoke();
			});
		}, false, delegate(float release_percent)
		{
			this.PreloadAsyncReleaseChange(release_percent);
		});
	}

	protected void UnloadSubScene(string name, Action callback)
	{
		if (string.IsNullOrEmpty(name))
		{
			callback.Invoke();
		}
		else
		{
			AssetLoader.UnloadScene(name, delegate(bool isSuccess)
			{
				AssetLoader.UnloadUnusedAssets(delegate
				{
					callback.Invoke();
				});
			}, true, null);
		}
	}

	protected void LoadMainScene(int sceneID, string name, Action callback)
	{
		EventDispatcher.Broadcast<int, int>(SceneManagerEvent.SceneRelatedLoad, MySceneManager.Instance.CurSceneID, sceneID);
		AssetLoader.LoadScene(name, delegate(bool isFinished)
		{
			this.PreloadSceneChange(1f);
			LightmapManager.UpdateLightmap(sceneID);
			callback.Invoke();
		}, false, delegate(float loaded_percent)
		{
			this.PreloadSceneChange(loaded_percent);
		});
	}

	protected void LoadSubScene(string name, Action callback)
	{
		if (string.IsNullOrEmpty(name))
		{
			callback.Invoke();
		}
		else
		{
			AssetLoader.LoadScene(name, delegate(bool isFinished)
			{
				callback.Invoke();
			}, true, null);
		}
	}

	public void PreloadFinish(int count)
	{
		LoadingRes.finish_resource_num += count;
		this.PreloadFinishUpdate();
	}

	public void PreloadSceneChange(float loaded_precent)
	{
		int num = (int)(loaded_precent * 600f);
		if (num > this.scene_loaded_num)
		{
			LoadingRes.finish_resource_num += num - this.scene_loaded_num;
			this.scene_loaded_num = num;
			this.PreloadFinishUpdate();
		}
	}

	public void PreloadAsyncReleaseChange(float release_percent)
	{
		int num = (int)(release_percent * 100f);
		if (num > this.async_release_num)
		{
			LoadingRes.finish_resource_num += num - this.async_release_num;
			this.async_release_num = num;
			this.PreloadFinishUpdate();
		}
	}

	private void PreloadFinishUpdate()
	{
		float progress = 1f;
		if (LoadingRes.total_resource_num > 0)
		{
			progress = (float)LoadingRes.finish_resource_num / (float)LoadingRes.total_resource_num;
		}
		this.UpdateProgressBar(progress);
		if (this.IsPreloadResourceOn && LoadingRes.finish_resource_num >= LoadingRes.total_resource_num)
		{
			TimerHeap.DelTimer(this.action_allResource_loaded_timer_id);
			this.action_allResource_loaded_timer_id = TimerHeap.AddTimer(100u, 0, delegate
			{
				this.action_allResource_loaded_timer_id = 0u;
				if (this.action_allResource_loaded != null)
				{
					this.action_allResource_loaded.Invoke();
				}
			});
		}
	}

	protected void UpdateProgressBar(float progress)
	{
		this.SetLoadingPercentage(progress);
	}

	private void SetLoadingPercentage(float percent)
	{
		LoadingUIView.SetProgressInSmooth(percent);
	}

	public void AddPreLoadAction(Action action)
	{
		this.actions_need_deal.Add(action);
	}

	private void DealActionsBeforePreloadRes()
	{
		using (List<Action>.Enumerator enumerator = this.actions_need_deal.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Action current = enumerator.get_Current();
				current.Invoke();
			}
		}
		this.actions_need_deal.Clear();
	}
}
