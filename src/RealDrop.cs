using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;

public class RealDrop : GearParent
{
	public enum RealDropState
	{
		None,
		FlyToWait,
		Wait,
		Picking
	}

	protected static GameObject dropPool;

	protected static List<RealDrop> allRealDrop = new List<RealDrop>();

	protected int index;

	protected int modelID;

	protected string itemName;

	public RealDrop.RealDropState currentState;

	public Animator animator;

	public Vector3 waitPosition;

	public static readonly float FlyToWaitPositionDefaultTime = 1f;

	public Vector3 flyToWaitPositionSpeed;

	public float flyToWaitPositionTime;

	public int waitFxModelID;

	public int waitFxID;

	public BoxCollider waitCollider;

	protected float deltaTime;

	protected int collectTextID;

	public static GameObject DropPool
	{
		get
		{
			if (RealDrop.dropPool == null)
			{
				RealDrop.dropPool = new GameObject("RealDropPool");
				Object.DontDestroyOnLoad(RealDrop.dropPool);
				RealDrop.dropPool.get_transform().set_localPosition(Vector3.get_zero());
				RealDrop.dropPool.get_transform().set_localScale(Vector3.get_one());
			}
			return RealDrop.dropPool;
		}
	}

	public int Index
	{
		get
		{
			return this.index;
		}
		set
		{
			this.index = value;
		}
	}

	public int ModelID
	{
		get
		{
			return this.modelID;
		}
		set
		{
			this.modelID = value;
		}
	}

	public string ItemName
	{
		get
		{
			return this.itemName;
		}
		set
		{
			this.itemName = value;
		}
	}

	public static RealDrop CreateRealDrop(int theIndex, int theModelID, string theItemName, Vector3 originPoint, Vector3 waitPoint, int waitFxID, int collectTextID)
	{
		if (!DataReader<DiaoLuoMoXingBiao>.Contains(theModelID))
		{
			return null;
		}
		DiaoLuoMoXingBiao diaoLuoMoXingBiao = DataReader<DiaoLuoMoXingBiao>.Get(theModelID);
		GameObject gameObject = GameObjectLoader.Instance.Get(diaoLuoMoXingBiao.path);
		if (gameObject == null)
		{
			return null;
		}
		RealDrop realDrop = gameObject.AddUniqueComponent<RealDrop>();
		realDrop.Index = theIndex;
		realDrop.ModelID = theModelID;
		realDrop.ItemName = theItemName;
		realDrop.animator = realDrop.GetComponentInChildren<Animator>();
		realDrop.get_gameObject().set_layer(LayerSystem.NameToLayer("Gear"));
		realDrop.get_transform().set_parent(RealDrop.DropPool.get_transform());
		realDrop.get_transform().set_position(originPoint);
		if (originPoint.x == waitPoint.x && originPoint.z == waitPoint.z)
		{
			realDrop.currentState = RealDrop.RealDropState.Wait;
			realDrop.waitPosition = originPoint;
			realDrop.flyToWaitPositionSpeed = Vector3.get_zero();
		}
		else
		{
			realDrop.currentState = RealDrop.RealDropState.FlyToWait;
			realDrop.waitPosition = MySceneManager.GetTerrainPoint(waitPoint.x, waitPoint.z, waitPoint.y);
			realDrop.flyToWaitPositionSpeed = (realDrop.waitPosition - originPoint) / RealDrop.FlyToWaitPositionDefaultTime;
		}
		realDrop.waitFxModelID = waitFxID;
		realDrop.waitCollider = realDrop.GetComponent<BoxCollider>();
		realDrop.waitCollider.set_enabled(false);
		RealDrop.allRealDrop.Add(realDrop);
		return realDrop;
	}

	public static void DeleteRealDrop(List<int> IDList)
	{
		if (IDList.get_Count() == 0)
		{
			return;
		}
		if (IDList.get_Count() == 1)
		{
			RealDrop.DeleteRealDrop(IDList.get_Item(0));
		}
		else
		{
			List<RealDrop> list = new List<RealDrop>();
			for (int i = 0; i < RealDrop.allRealDrop.get_Count(); i++)
			{
				if (IDList.Contains(RealDrop.allRealDrop.get_Item(i).Index))
				{
					list.Add(RealDrop.allRealDrop.get_Item(i));
				}
			}
			for (int j = 0; j < list.get_Count(); j++)
			{
				RealDrop.DeleteRealDrop(list.get_Item(j));
			}
		}
	}

	public static void DeleteRealDrop(int ID)
	{
		for (int i = 0; i < RealDrop.allRealDrop.get_Count(); i++)
		{
			if (RealDrop.allRealDrop.get_Item(i).Index == ID)
			{
				RealDrop.DeleteRealDrop(RealDrop.allRealDrop.get_Item(i));
				break;
			}
		}
	}

	public static void DeleteRealDrop(RealDrop drop)
	{
		RealDrop.allRealDrop.Remove(drop);
		if (drop)
		{
			drop.Delete();
		}
	}

	public static void DeleteAllRealDrop()
	{
		for (int i = 0; i < RealDrop.allRealDrop.get_Count(); i++)
		{
			RealDrop.allRealDrop.get_Item(i).Delete();
		}
		RealDrop.allRealDrop.Clear();
	}

	private void Awake()
	{
		this.AddListeners();
	}

	private void Start()
	{
		this.animator.Play("born");
		if (DataReader<DiaoLuoMoXingBiao>.Contains(this.ModelID))
		{
			DiaoLuoMoXingBiao diaoLuoMoXingBiao = DataReader<DiaoLuoMoXingBiao>.Get(this.ModelID);
			BillboardManager.Instance.AddBillboardsInfo(101, base.get_transform(), (float)DataReader<DiaoLuoMoXingBiao>.Get(this.ModelID).height_HP, (long)this.Index, false, true, true);
			HeadInfoManager.Instance.SetName(101, (long)this.Index, this.ItemName);
		}
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
		BillboardManager.Instance.RemoveBillboardsInfo((long)this.Index, base.get_transform());
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

	private void OnTriggerExit(Collider other)
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
			this.OnSelfExit();
		}
	}

	private void Update()
	{
		this.deltaTime = Time.get_deltaTime();
		RealDrop.RealDropState realDropState = this.currentState;
		if (realDropState == RealDrop.RealDropState.FlyToWait)
		{
			this.FlyingToWaitPosition();
		}
	}

	protected void FlyingToWaitPosition()
	{
		this.flyToWaitPositionTime += this.deltaTime;
		if (this.flyToWaitPositionTime < RealDrop.FlyToWaitPositionDefaultTime)
		{
			base.get_transform().Translate(this.flyToWaitPositionSpeed * this.deltaTime);
		}
		else
		{
			this.currentState = RealDrop.RealDropState.Wait;
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
		this.currentState = RealDrop.RealDropState.Picking;
		BattleProgressBarUI battleProgressBarUI = UIManagerControl.Instance.OpenUI("BattleProgressBarUI", null, false, UIType.NonPush) as BattleProgressBarUI;
		if (battleProgressBarUI)
		{
			battleProgressBarUI.SetData(GameDataUtils.GetChineseContent(this.collectTextID, false), InstanceManager.CurrentCollectDropTime, InstanceManager.CurrentCollectDropTime, delegate
			{
				InstanceManager.CollectDrop(this.index);
				if (EntityWorld.Instance.EntSelf != null)
				{
					EntityWorld.Instance.EntSelf.GetRealDrop();
				}
				RealDrop.DeleteRealDrop(this);
			});
		}
		else
		{
			InstanceManager.CollectDrop(this.index);
		}
	}

	protected void OnSelfExit()
	{
		this.currentState = RealDrop.RealDropState.Wait;
		if (UIManagerControl.Instance.IsOpen("BattleProgressBarUI"))
		{
			UIManagerControl.Instance.HideUI("BattleProgressBarUI");
		}
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
			if (this.currentState == RealDrop.RealDropState.Picking)
			{
				this.OnSelfExit();
			}
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
