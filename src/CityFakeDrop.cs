using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;

public class CityFakeDrop : GearParent
{
	public enum CityFakeDropState
	{
		None,
		FlyToWait,
		Wait
	}

	protected static GameObject cityDropPool;

	protected static List<CityFakeDrop> allCityFakeDrop = new List<CityFakeDrop>();

	public CityFakeDrop.CityFakeDropState currentState;

	public Animator animator;

	public Vector3 waitPosition;

	public static readonly float FlyToWaitPositionDefaultTime = 1f;

	public Vector3 flyToWaitPositionSpeed;

	public float flyToWaitPositionTime;

	public int waitFxModelID;

	public int waitFxID;

	public BoxCollider waitCollider;

	protected float deltaTime;

	public static GameObject CityDropPool
	{
		get
		{
			if (CityFakeDrop.cityDropPool == null)
			{
				CityFakeDrop.cityDropPool = new GameObject("CityFakeDropPool");
				Object.DontDestroyOnLoad(CityFakeDrop.cityDropPool);
				CityFakeDrop.cityDropPool.get_transform().set_localPosition(Vector3.get_zero());
				CityFakeDrop.cityDropPool.get_transform().set_localScale(Vector3.get_one());
			}
			return CityFakeDrop.cityDropPool;
		}
	}

	public static CityFakeDrop CreateCityFakeDrop(int modelID, Vector3 originPoint, Vector3 waitPoint, int waitFxID)
	{
		DiaoLuoMoXingBiao diaoLuoMoXingBiao = DataReader<DiaoLuoMoXingBiao>.Get(modelID);
		if (diaoLuoMoXingBiao == null)
		{
			return null;
		}
		GameObject gameObject = GameObjectLoader.Instance.Get(diaoLuoMoXingBiao.path);
		if (gameObject == null)
		{
			return null;
		}
		CityFakeDrop cityFakeDrop = gameObject.AddUniqueComponent<CityFakeDrop>();
		cityFakeDrop.animator = cityFakeDrop.GetComponentInChildren<Animator>();
		cityFakeDrop.get_gameObject().set_layer(LayerSystem.NameToLayer("Gear"));
		cityFakeDrop.get_transform().set_parent(CityFakeDrop.CityDropPool.get_transform());
		cityFakeDrop.get_transform().set_position(originPoint);
		if (originPoint.x == waitPoint.x && originPoint.z == waitPoint.z)
		{
			cityFakeDrop.currentState = CityFakeDrop.CityFakeDropState.Wait;
			cityFakeDrop.waitPosition = originPoint;
			cityFakeDrop.flyToWaitPositionSpeed = Vector3.get_zero();
		}
		else
		{
			cityFakeDrop.currentState = CityFakeDrop.CityFakeDropState.FlyToWait;
			cityFakeDrop.waitPosition = MySceneManager.GetTerrainPoint(waitPoint.x, waitPoint.z, waitPoint.y);
			cityFakeDrop.flyToWaitPositionSpeed = (cityFakeDrop.waitPosition - originPoint) / CityFakeDrop.FlyToWaitPositionDefaultTime;
		}
		cityFakeDrop.waitFxModelID = waitFxID;
		cityFakeDrop.waitCollider = cityFakeDrop.GetComponent<BoxCollider>();
		cityFakeDrop.waitCollider.set_enabled(false);
		CityFakeDrop.allCityFakeDrop.Add(cityFakeDrop);
		return cityFakeDrop;
	}

	public static void DeleteCityFakeDrop(CityFakeDrop drop)
	{
		CityFakeDrop.allCityFakeDrop.Remove(drop);
		drop.Delete();
	}

	public static void DeleteAllCityFakeDrop()
	{
		for (int i = 0; i < CityFakeDrop.allCityFakeDrop.get_Count(); i++)
		{
			CityFakeDrop.allCityFakeDrop.get_Item(i).Delete();
		}
		CityFakeDrop.allCityFakeDrop.Clear();
	}

	private void Awake()
	{
		this.AddListeners();
	}

	private void Start()
	{
		this.animator.Play("born");
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Default") && other.get_gameObject().get_layer() != LayerSystem.NameToLayer("Terrian"))
		{
			ActorCollider component = other.GetComponent<ActorCollider>();
			if (!component)
			{
				return;
			}
			if (!component.Actor)
			{
				return;
			}
			if (component.Actor.GetEntity() == null)
			{
				return;
			}
			if (!component.Actor.GetEntity().IsEntitySelfType)
			{
				return;
			}
			this.OnSelfEnter();
		}
	}

	private void Update()
	{
		this.deltaTime = Time.get_deltaTime();
		CityFakeDrop.CityFakeDropState cityFakeDropState = this.currentState;
		if (cityFakeDropState != CityFakeDrop.CityFakeDropState.FlyToWait)
		{
			if (cityFakeDropState != CityFakeDrop.CityFakeDropState.Wait)
			{
			}
		}
		else
		{
			this.FlyingToWaitPosition();
		}
	}

	protected void FlyingToWaitPosition()
	{
		this.flyToWaitPositionTime += this.deltaTime;
		if (this.flyToWaitPositionTime < CityFakeDrop.FlyToWaitPositionDefaultTime)
		{
			base.get_transform().Translate(this.flyToWaitPositionSpeed * this.deltaTime);
		}
		else
		{
			this.currentState = CityFakeDrop.CityFakeDropState.Wait;
			base.get_transform().set_position(this.waitPosition);
			this.animator.Play("idle");
			this.waitFxID = FXManager.Instance.PlayFX(this.waitFxModelID, base.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			this.waitCollider.set_enabled(true);
		}
	}

	protected void Hide()
	{
		FXManager.Instance.DeleteFX(this.waitFxID);
		base.get_gameObject().SetActive(false);
	}

	protected void OnSelfEnter()
	{
		this.currentState = CityFakeDrop.CityFakeDropState.None;
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.GetFakeDrop();
		}
		EventDispatcher.Broadcast(WildBossManagerEvent.GetCityFakeDrop);
		CityFakeDrop.DeleteCityFakeDrop(this);
	}

	public void Delete()
	{
		if (FXManager.Instance == null)
		{
			Debug.LogError("FXManager.Instance == null，找洪总");
		}
		else
		{
			FXManager.Instance.DeleteFX(this.waitFxID);
		}
		if (this)
		{
			if (base.get_gameObject())
			{
				if (GameObjectLoader.Instance == null)
				{
					Debug.LogError("GameObjectLoader.Instance == null，找左总");
				}
				else
				{
					GameObjectLoader.Instance.PoolRecycle(base.get_gameObject());
				}
			}
			Object.Destroy(this);
		}
	}
}
