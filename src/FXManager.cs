using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class FXManager
{
	private const int SCREEN_BATTLE_ID = 9999;

	private const int CHANGE_SCENE_ID = 1107;

	public static readonly FXManager Instance = new FXManager();

	private Dictionary<int, ActorFX> m_fxs = new Dictionary<int, ActorFX>();

	private Dictionary<int, float> m_fxOfspeed = new Dictionary<int, float>();

	private Dictionary<int, float> m_fxOfResize = new Dictionary<int, float>();

	private int m_fxOfcounter;

	public static bool IsPetBorn = false;

	private List<int> m_deleteIds = new List<int>();

	private int mScreenBattleUID;

	private int mChangeSceneUID;

	public int PlayFX(int templateId, Transform host, Vector3 position, Quaternion rotation, float speed = 1f, float scale = 1f, int time = 0, bool syncNode = false, int bulletLife = 0, Action<Actor, XPoint, ActorParent> bulletCallback = null, Action<ActorFX> loadCallback = null, float rate = 1f, FXClassification fxClassification = FXClassification.Normal)
	{
		return this.AsyncPlayfxOnScene(templateId, host, position, rotation, speed, scale, time, syncNode, bulletLife, bulletCallback, null, loadCallback, true, rate, fxClassification);
	}

	public int PlayFXIfNOExist(int uid, int templateId, Transform host, Vector3 position, Quaternion rotation, float speed = 1f, float scale = 1f, int time = 0, bool syncNode = false, int bulletLife = 0, Action<Actor, XPoint, ActorParent> bulletCallback = null, Action<ActorFX> loadCallback = null, float rate = 1f)
	{
		if (this.IsExist(uid))
		{
			return uid;
		}
		return this.PlayFX(templateId, host, position, rotation, 1f, 1f, 0, syncNode, bulletLife, bulletCallback, loadCallback, rate, FXClassification.Normal);
	}

	public int PlayFXOfFollow(int templateId, Vector3 position, Transform targetFollow, float speed, float lessDistance, float offset = 0f, Action finishCallback = null, FXClassification fxClassification = FXClassification.Normal)
	{
		if (!SystemConfig.IsFXOn)
		{
			return 0;
		}
		if (templateId <= 0)
		{
			return 0;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return 0;
		}
		int uid = ++this.m_fxOfcounter;
		this.m_fxs.set_Item(uid, null);
		AssetManager.LoadAssetWithPool(FXPool.GetPathWithLOD(fx.path, fxClassification == FXClassification.LowLod), delegate(bool isSuccess)
		{
			if (this.m_fxs.ContainsKey(uid))
			{
				this.JustPlayFXOfFollow(uid, templateId, position, targetFollow, speed, lessDistance, offset, finishCallback, fxClassification);
			}
		});
		return uid;
	}

	public int PlayFXOfDisplay(int templateId, Transform host, Vector3 position, Quaternion rotation, float speed = 1f, float scale = 1f, int time = 0, bool syncNode = false, Action finishCallback = null, Action<ActorFX> loadCallback = null)
	{
		return this.AsyncPlayfxOnScene(templateId, host, position, rotation, speed, scale, time, syncNode, 0, null, finishCallback, loadCallback, false, 1f, FXClassification.Normal);
	}

	public int PlayFXOfUI(int templateId, Transform host, int time = 0, int depthValue = 2001, Action finishCallback = null)
	{
		if (!SystemConfig.IsFXOn)
		{
			return 0;
		}
		if (templateId <= 0)
		{
			return 0;
		}
		if (host == null)
		{
			return 0;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return 0;
		}
		int uid = ++this.m_fxOfcounter;
		this.m_fxs.set_Item(uid, null);
		AssetManager.LoadAssetWithPool(FXPool.GetPathWithLOD(fx.path, false), delegate(bool isSuccess)
		{
			if (this.m_fxs.ContainsKey(uid))
			{
				this.JustPlayfxOnUI(uid, templateId, host, time, depthValue, finishCallback);
			}
		});
		return uid;
	}

	public ActorFX GetActorByID(int uid)
	{
		if (this.m_fxs.ContainsKey(uid))
		{
			return this.m_fxs.get_Item(uid);
		}
		return null;
	}

	public bool IsExist(int uid)
	{
		return this.m_fxs.ContainsKey(uid);
	}

	public void DeleteFX(int uid)
	{
		if (this.m_fxs.ContainsKey(uid))
		{
			if (this.m_fxs.get_Item(uid) != null)
			{
				this.m_fxs.get_Item(uid).FXFinished();
			}
			this.m_fxs.Remove(uid);
		}
	}

	public void DeleteFXs()
	{
		this.m_deleteIds.Clear();
		using (Dictionary<int, ActorFX>.Enumerator enumerator = this.m_fxs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActorFX> current = enumerator.get_Current();
				this.m_deleteIds.Add(current.get_Key());
			}
		}
		for (int i = 0; i < this.m_deleteIds.get_Count(); i++)
		{
			this.DeleteFX(this.m_deleteIds.get_Item(i));
		}
		this.m_deleteIds.Clear();
		this.m_fxs.Clear();
	}

	public void Resize(int uid, float scale)
	{
		if (this.m_fxs.ContainsKey(uid))
		{
			ActorFX actorFX = this.m_fxs.get_Item(uid);
			if (actorFX != null)
			{
				actorFX.Scale = scale;
			}
			else
			{
				this.m_fxOfResize.set_Item(uid, scale);
			}
		}
	}

	private void ResizeIfNeed(int uid)
	{
		if (this.m_fxOfResize.ContainsKey(uid))
		{
			this.Resize(uid, this.m_fxOfResize.get_Item(uid));
			this.m_fxOfResize.Remove(uid);
		}
	}

	public void SetSpeed(int uid, float rate)
	{
		if (this.m_fxs.ContainsKey(uid))
		{
			ActorFX actorFX = this.m_fxs.get_Item(uid);
			if (actorFX != null)
			{
				this.m_fxs.get_Item(uid).SetFrameRate(rate, false);
			}
			else
			{
				this.m_fxOfspeed.set_Item(uid, rate);
			}
		}
	}

	private void SetSpeedIfNeed(int uid)
	{
		if (this.m_fxOfspeed.ContainsKey(uid))
		{
			this.SetSpeed(uid, this.m_fxOfspeed.get_Item(uid));
			this.m_fxOfspeed.Remove(uid);
		}
	}

	public void Preload(int templateId, Action callbackSuccess)
	{
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			if (callbackSuccess != null)
			{
				callbackSuccess.Invoke();
			}
			return;
		}
		AssetManager.LoadAssetWithPool(FXPool.GetPathWithLOD(fx.path, false), delegate(bool isSuccess)
		{
			if (callbackSuccess != null)
			{
				callbackSuccess.Invoke();
			}
		});
	}

	private int AsyncPlayfxOnScene(int templateId, Transform host, Vector3 position, Quaternion rotation, float speed = 1f, float scale = 1f, int time = 0, bool syncNode = false, int bulletLife = 0, Action<Actor, XPoint, ActorParent> bulletCallback = null, Action finishCallback = null, Action<ActorFX> loadCallback = null, bool sound3D = true, float rate = 1f, FXClassification fxClassification = FXClassification.Normal)
	{
		if (!SystemConfig.IsFXOn)
		{
			return 0;
		}
		if (templateId == ClientGMManager.fxout_id)
		{
			return 0;
		}
		if (templateId <= 0)
		{
			return 0;
		}
		if (fxClassification == FXClassification.NoPlay)
		{
			return 0;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return 0;
		}
		int uid = ++this.m_fxOfcounter;
		this.m_fxs.set_Item(uid, null);
		AssetManager.LoadAssetWithPool(FXPool.GetPathWithLOD(fx.path, fxClassification == FXClassification.LowLod), delegate(bool isSuccess)
		{
			if (this.m_fxs.ContainsKey(uid))
			{
				this.JustPlayfxOnScene(uid, templateId, host, position, rotation, speed, scale, time, syncNode, bulletLife, bulletCallback, finishCallback, loadCallback, sound3D, rate, fxClassification);
			}
		});
		return uid;
	}

	private void JustPlayfxOnScene(int uid, int templateId, Transform host, Vector3 position, Quaternion rotation, float speed, float scale, int time, bool syncNode, int bulletLife, Action<Actor, XPoint, ActorParent> bulletCallback, Action finishCallback, Action<ActorFX> loadCallback = null, bool sound3D = true, float rate = 1f, FXClassification fxClassification = FXClassification.Normal)
	{
		if (templateId == 900)
		{
			Debug.Log("JustPlayfxOnScene: " + DateTime.get_Now());
		}
		ActorFX actorFX = FXPool.Instance.Get(templateId, fxClassification == FXClassification.LowLod);
		if (actorFX == null)
		{
			return;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return;
		}
		this.m_fxs.set_Item(uid, actorFX);
		actorFX.IsLodLow = (fxClassification == FXClassification.LowLod);
		actorFX.AwakeSelf();
		actorFX.uid = uid;
		if (host != null)
		{
			string text = fx.hook;
			if (fx.hook == "weapon_slot" && EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
			{
				text += "_city";
			}
			if (fx.hook == "weapon_slot2" && EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
			{
				text = "weapon_slot_city2";
			}
			if (syncNode)
			{
				if (string.IsNullOrEmpty(text))
				{
					actorFX.Host = host;
				}
				else
				{
					Transform transform = XUtility.RecursiveFindTransform(host, text);
					if (transform == null)
					{
						Debug.LogError("mountPoint is null, mountPoint name = " + text);
					}
					actorFX.Host = transform;
				}
			}
			else if (string.IsNullOrEmpty(text))
			{
				if (fx.type1 == 3)
				{
					actorFX.get_transform().set_position(host.get_position());
				}
				else
				{
					if (actorFX.get_transform().get_parent() != host)
					{
						actorFX.get_transform().set_parent(host);
					}
					actorFX.get_transform().set_localPosition(position);
					actorFX.get_transform().set_localRotation(rotation);
				}
			}
			else
			{
				Transform transform2 = XUtility.RecursiveFindTransform(host, text);
				if (fx.type1 == 3)
				{
					actorFX.get_transform().set_position(transform2.get_position());
				}
				else
				{
					if (actorFX.get_transform().get_parent() != transform2)
					{
						actorFX.get_transform().set_parent(transform2);
					}
					actorFX.get_transform().set_localPosition(position);
					actorFX.get_transform().set_localRotation(rotation);
				}
			}
			actorFX.IsSyncNode = syncNode;
			actorFX.HostRoot = host;
		}
		else
		{
			actorFX.get_transform().set_position(position);
			actorFX.get_transform().set_rotation(rotation);
		}
		actorFX.Scale = scale;
		actorFX.Speed = speed;
		actorFX.SetFrameRate(rate, false);
		Delay component = actorFX.get_gameObject().GetComponent<Delay>();
		if (component != null)
		{
			component.TriggerDelay();
		}
		else
		{
			actorFX.get_gameObject().SetActive(true);
		}
		actorFX.finishCallback = finishCallback;
		if (bulletCallback != null)
		{
			actorFX.bulletLife = bulletLife;
			actorFX.bulletCallback = bulletCallback;
		}
		if (time <= 0)
		{
			time = fx.time;
		}
		if (time > 0)
		{
			actorFX.AutoDestroy = false;
			actorFX.SetTimer(time);
		}
		if (SoundManager.Instance != null)
		{
			if (sound3D)
			{
				AudioPlayer audioPlayer = actorFX.get_gameObject().AddUniqueComponent<AudioPlayer>();
				audioPlayer.RoleId = (long)actorFX.InstanceID;
				SoundManager.Instance.PlayPlayer(audioPlayer, fx.audio);
			}
			else
			{
				SoundManager.PlayUI(fx.audio, false);
			}
		}
		this.ResizeIfNeed(uid);
		this.SetSpeedIfNeed(uid);
		if (loadCallback != null)
		{
			loadCallback.Invoke(actorFX);
		}
		if (FXManager.IsPetBorn)
		{
			LayerSystem.SetGameObjectLayer(actorFX.get_gameObject(), "CameraRange", 1);
		}
	}

	private void JustPlayFXOfFollow(int uid, int templateId, Vector3 position, Transform targetFollow, float speed, float lessDistance, float offset, Action finishCallback, FXClassification fxClassification = FXClassification.Normal)
	{
		ActorFX actorFX = FXPool.Instance.Get(templateId, fxClassification == FXClassification.LowLod);
		if (actorFX == null)
		{
			return;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return;
		}
		this.m_fxs.set_Item(uid, actorFX);
		actorFX.IsLodLow = (fxClassification == FXClassification.LowLod);
		actorFX.AwakeSelf();
		FXFollow fXFollow = actorFX.get_gameObject().AddUniqueComponent<FXFollow>();
		fXFollow.set_enabled(true);
		fXFollow.targetFollow = targetFollow;
		fXFollow.speed = speed;
		fXFollow.lessDistance = lessDistance;
		fXFollow.offset = offset;
		actorFX.get_transform().set_position(position + new Vector3(0f, offset, 0f));
		actorFX.get_transform().set_rotation(Quaternion.get_identity());
		actorFX.Scale = 1f;
		Delay component = actorFX.get_gameObject().GetComponent<Delay>();
		if (component != null)
		{
			component.TriggerDelay();
		}
		else
		{
			actorFX.get_gameObject().SetActive(true);
		}
		actorFX.finishCallback = finishCallback;
		AudioPlayer audioPlayer = actorFX.get_gameObject().AddUniqueComponent<AudioPlayer>();
		audioPlayer.RoleId = (long)actorFX.InstanceID;
		if (SoundManager.Instance != null)
		{
			SoundManager.Instance.PlayPlayer(audioPlayer, fx.audio);
		}
		this.ResizeIfNeed(uid);
		this.SetSpeedIfNeed(uid);
	}

	private void JustPlayfxOnUI(int uid, int templateId, Transform host, int time, int depthValue, Action finishCallback = null)
	{
		ActorFX actorFX = FXPool.Instance.Get(templateId, false);
		if (actorFX == null)
		{
			return;
		}
		Fx fx = DataReader<Fx>.Get(templateId);
		if (fx == null || string.IsNullOrEmpty(fx.path))
		{
			return;
		}
		this.m_fxs.set_Item(uid, actorFX);
		actorFX.uid = uid;
		actorFX.AwakeSelf();
		actorFX.get_transform().set_parent(host);
		actorFX.get_transform().set_localPosition(Vector3.get_zero());
		actorFX.get_transform().set_localRotation(Quaternion.get_identity());
		actorFX.get_gameObject().SetActive(true);
		actorFX.Scale = 1f;
		actorFX.finishCallback = finishCallback;
		SoundManager.PlayUI(fx.audio, false);
		LayerSystem.SetGameObjectLayer(actorFX.get_gameObject(), "UI", 1);
		DepthOfFX depthOfFX = actorFX.get_gameObject().AddMissingComponent<DepthOfFX>();
		depthOfFX.SortingOrder = depthValue;
		if (time <= 0)
		{
			time = fx.time;
		}
		if (time > 0)
		{
			actorFX.AutoDestroy = false;
			actorFX.SetTimer(time);
		}
	}

	public int ScreenBattlePlay()
	{
		if (this.mScreenBattleUID != 0)
		{
			this.DeleteFX(this.mScreenBattleUID);
		}
		if (CamerasMgr.MainCameraRoot == null)
		{
			return 0;
		}
		CamerasMgr.CameraMain.set_cullingMask(Utils.GetCullingMask(8));
		CamerasMgr.Camera2BattleFX.get_gameObject().SetActive(true);
		this.mScreenBattleUID = FXManager.Instance.PlayFX(9999, CamerasMgr.Camera2BattleFX.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, delegate(ActorFX obj)
		{
			LayerSystem.SetGameObjectLayer(obj.get_gameObject(), "CameraRange", 1);
			obj.get_transform().set_localPosition(new Vector3(0f, -3.12f, 7.25f));
			obj.get_transform().set_localRotation(Quaternion.get_identity());
		}, 1f, FXClassification.Normal);
		return this.mScreenBattleUID;
	}

	public void ScreenBattleDelete()
	{
		this.DeleteFX(this.mScreenBattleUID);
		this.mScreenBattleUID = 0;
		if (CamerasMgr.Camera2BattleFX != null)
		{
			CamerasMgr.Camera2BattleFX.get_gameObject().SetActive(false);
		}
	}

	public int ChangeScenePlay()
	{
		if (this.mChangeSceneUID != 0)
		{
			this.DeleteFX(this.mChangeSceneUID);
		}
		if (CamerasMgr.MainCameraRoot == null)
		{
			return 0;
		}
		CamerasMgr.CameraMain.set_cullingMask(Utils.GetCullingMask(8));
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_BG_BLUR_FADEIN, true);
		CamerasMgr.Camera2BattleFX.get_gameObject().SetActive(true);
		this.mChangeSceneUID = FXManager.Instance.PlayFXOfDisplay(1107, CamerasMgr.Camera2BattleFX.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, delegate
		{
			this.ChangeSceneDelete();
		}, delegate(ActorFX obj)
		{
			LayerSystem.SetGameObjectLayer(obj.get_gameObject(), "CameraRange", 1);
			obj.get_transform().set_localPosition(new Vector3(0f, -5f, 7.25f));
			obj.get_transform().set_localRotation(Quaternion.get_identity());
		});
		return this.mChangeSceneUID;
	}

	public void ChangeSceneDelete()
	{
		this.DeleteFX(this.mChangeSceneUID);
		this.mChangeSceneUID = 0;
		if (CamerasMgr.Camera2BattleFX != null)
		{
			CamerasMgr.Camera2BattleFX.get_gameObject().SetActive(false);
		}
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_BG_BLUR_FADEIN, false);
	}
}
