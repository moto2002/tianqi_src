using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;

public class FakeDrop : GearParent
{
	public enum FakeDropState
	{
		None,
		FlyToWait,
		Wait,
		FlyToEntity
	}

	protected static GameObject dropPool;

	protected static List<FakeDrop> allFakeDrop = new List<FakeDrop>();

	public FakeDrop.FakeDropState currentState;

	public Animator animator;

	public Vector3 waitPosition;

	public static readonly float FlyToWaitPositionDefaultTime = 1f;

	public Vector3 flyToWaitPositionSpeed;

	public float flyToWaitPositionTime;

	public int waitFxModelID;

	public int waitFxID;

	public BoxCollider waitCollider;

	public float WaitDefaultTime = 3f;

	public float waitTime;

	public int flyToEntityFxModelID;

	public int flyToEntityFxID;

	public static readonly int FlyToEntityDefaultSpeed = 10;

	protected float deltaTime;

	public static GameObject DropPool
	{
		get
		{
			if (FakeDrop.dropPool == null)
			{
				FakeDrop.dropPool = new GameObject("FakeDropPool");
				Object.DontDestroyOnLoad(FakeDrop.dropPool);
				FakeDrop.dropPool.get_transform().set_localPosition(Vector3.get_zero());
				FakeDrop.dropPool.get_transform().set_localScale(Vector3.get_one());
			}
			return FakeDrop.dropPool;
		}
	}

	public static FakeDrop CreateFakeDrop(int modelID, Vector3 originPoint, Vector3 waitPoint, int waitFxID, int flyToEntityFxID)
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
		FakeDrop fakeDrop = gameObject.AddUniqueComponent<FakeDrop>();
		fakeDrop.animator = fakeDrop.GetComponentInChildren<Animator>();
		fakeDrop.get_gameObject().set_layer(LayerSystem.NameToLayer("Gear"));
		fakeDrop.get_transform().set_parent(FakeDrop.DropPool.get_transform());
		fakeDrop.get_transform().set_position(originPoint);
		if (originPoint.x == waitPoint.x && originPoint.z == waitPoint.z)
		{
			fakeDrop.currentState = FakeDrop.FakeDropState.Wait;
			fakeDrop.waitPosition = originPoint;
			fakeDrop.flyToWaitPositionSpeed = Vector3.get_zero();
		}
		else
		{
			fakeDrop.currentState = FakeDrop.FakeDropState.FlyToWait;
			fakeDrop.waitPosition = MySceneManager.GetTerrainPoint(waitPoint.x, waitPoint.z, waitPoint.y);
			fakeDrop.flyToWaitPositionSpeed = (fakeDrop.waitPosition - originPoint) / FakeDrop.FlyToWaitPositionDefaultTime;
		}
		fakeDrop.waitFxModelID = waitFxID;
		fakeDrop.waitCollider = fakeDrop.GetComponent<BoxCollider>();
		fakeDrop.waitCollider.set_enabled(false);
		fakeDrop.flyToEntityFxModelID = flyToEntityFxID;
		FakeDrop.allFakeDrop.Add(fakeDrop);
		return fakeDrop;
	}

	public static void DeleteFakeDrop(FakeDrop drop)
	{
		FakeDrop.allFakeDrop.Remove(drop);
		drop.Delete();
	}

	public static void DeleteAllFakeDrop()
	{
		for (int i = 0; i < FakeDrop.allFakeDrop.get_Count(); i++)
		{
			FakeDrop.allFakeDrop.get_Item(i).Delete();
		}
		FakeDrop.allFakeDrop.Clear();
	}

	private void Awake()
	{
		this.WaitDefaultTime = float.Parse(DataReader<GlobalParams>.Get("flopDwellTime").value) * 0.001f;
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
		FakeDrop.FakeDropState fakeDropState = this.currentState;
		if (fakeDropState != FakeDrop.FakeDropState.FlyToWait)
		{
			if (fakeDropState == FakeDrop.FakeDropState.Wait)
			{
				this.Waiting();
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
		if (this.flyToWaitPositionTime < FakeDrop.FlyToWaitPositionDefaultTime)
		{
			base.get_transform().Translate(this.flyToWaitPositionSpeed * this.deltaTime);
		}
		else
		{
			this.currentState = FakeDrop.FakeDropState.Wait;
			base.get_transform().set_position(this.waitPosition);
			this.animator.Play("idle");
			this.waitFxID = FXManager.Instance.PlayFX(this.waitFxModelID, base.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			this.waitCollider.set_enabled(true);
		}
	}

	protected void Waiting()
	{
		this.waitTime += this.deltaTime;
		if (this.waitTime < this.WaitDefaultTime)
		{
			return;
		}
		this.currentState = FakeDrop.FakeDropState.FlyToEntity;
		if (EntityWorld.Instance.ActSelf)
		{
			this.flyToEntityFxID = FXManager.Instance.PlayFXOfFollow(this.flyToEntityFxModelID, base.get_transform().get_position(), EntityWorld.Instance.ActSelf.FixTransform, (float)FakeDrop.FlyToEntityDefaultSpeed, 0.2f, 0f, delegate
			{
				if (EntityWorld.Instance.EntSelf != null)
				{
					EntityWorld.Instance.EntSelf.GetFakeDrop();
				}
				FakeDrop.DeleteFakeDrop(this);
			}, FXClassification.Normal);
		}
		this.Hide();
	}

	protected void Hide()
	{
		FXManager.Instance.DeleteFX(this.waitFxID);
		base.get_gameObject().SetActive(false);
	}

	protected void OnSelfEnter()
	{
		this.currentState = FakeDrop.FakeDropState.None;
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.GetFakeDrop();
		}
		FakeDrop.DeleteFakeDrop(this);
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
			FXManager.Instance.DeleteFX(this.flyToEntityFxID);
		}
		if (null != this)
		{
			if (null != base.get_gameObject())
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
