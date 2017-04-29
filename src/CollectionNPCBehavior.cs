using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineCommand;

public class CollectionNPCBehavior : NPCBehavior
{
	public enum CollectionNPCStateType
	{
		Normal,
		Collecting,
		Collected
	}

	public static readonly string OnEnterNPC = "CollectionNPCBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "CollectionNPCBehavior.OnExitNPC";

	public static readonly string OnNPCDieEnd = "CollectionNPCBehavior.OnNPCDieEnd";

	protected int collectionDataID;

	protected CollectionNPCBehavior.CollectionNPCStateType collectionState;

	protected uint deleteCollectingFxTimer;

	protected int collectingFxID;

	protected uint collectedDeleteTimer;

	protected Collider notTriggerCollider;

	protected Collider triggerCollider;

	public override bool EnableUpdate
	{
		get
		{
			return false;
		}
	}

	public CollectionNPCBehavior(int theCollectionDataID, int state)
	{
		this.collectionDataID = theCollectionDataID;
		this.collectionState = (CollectionNPCBehavior.CollectionNPCStateType)state;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		this.id = theID;
		this.transform = root;
		CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
		if (caiJiPeiZhi == null)
		{
			return;
		}
		this.SetCollider(root, caiJiPeiZhi.touchRange, caiJiPeiZhi.triggeredRange);
		this.ApplyDefaultState();
		this.SetModel(root, modelID);
	}

	protected void SetModel(Transform root, int modelID)
	{
		base.GetAsyncModel(root, modelID, delegate
		{
			CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
			this.animator = root.GetComponentInChildren<Animator>();
			if (this.animator)
			{
				AssetManager.AssetOfControllerManager.SetController(this.animator, modelID, false);
				this.ChangeAnimationSpeed();
			}
			this.SetCharacterController(root);
			BillboardManager.Instance.AddBillboardsInfo(31, root, (float)avatarModel.height_HP, (long)this.id, false, true, true);
			if (caiJiPeiZhi.name != 0)
			{
				HeadInfoManager.Instance.SetName(31, (long)this.id, GameDataUtils.GetChineseContent(caiJiPeiZhi.name, false));
			}
			ShadowController.ShowShadow((long)this.id, root, false, modelID);
		});
	}

	protected void SetCharacterController(Transform root)
	{
		CharacterController[] componentsInChildren = root.GetComponentsInChildren<CharacterController>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_enabled(false);
			}
		}
	}

	protected void SetCollider(Transform root, List<int> touchRange, List<int> triggeredRange)
	{
		GameObject gameObject = new GameObject("notTriggerAgent");
		UGUITools.ResetTransform(gameObject.get_transform(), root);
		this.notTriggerCollider = base.CreateCollider(gameObject, touchRange);
		if (this.notTriggerCollider)
		{
			this.notTriggerCollider.set_isTrigger(false);
		}
		GameObject gameObject2 = new GameObject("triggerAgent");
		UGUITools.ResetTransform(gameObject2.get_transform(), root);
		gameObject2.AddComponent<NPCTriggerReceiver>();
		this.triggerCollider = base.CreateCollider(gameObject2, triggeredRange);
		if (this.triggerCollider)
		{
			this.triggerCollider.set_isTrigger(true);
		}
	}

	protected void EnableCollider()
	{
		if (this.notTriggerCollider && !this.notTriggerCollider.get_enabled())
		{
			this.notTriggerCollider.set_enabled(true);
		}
		if (this.triggerCollider && !this.triggerCollider.get_enabled())
		{
			this.triggerCollider.set_enabled(MySceneManager.Instance.IsSceneExist);
		}
	}

	protected void DisableCollider()
	{
		if (this.notTriggerCollider && this.notTriggerCollider.get_enabled())
		{
			this.notTriggerCollider.set_enabled(false);
		}
		if (this.triggerCollider && this.triggerCollider.get_enabled())
		{
			this.triggerCollider.set_enabled(false);
			this.OnExit();
		}
	}

	protected void SetPositionAndRotation()
	{
		CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
		if (caiJiPeiZhi == null)
		{
			return;
		}
		if (caiJiPeiZhi.position.get_Count() >= 3)
		{
			this.transform.set_position(PosDirUtility.ToTerrainPoint(caiJiPeiZhi.position));
		}
		if (caiJiPeiZhi.face.get_Count() >= 3)
		{
			this.transform.set_eulerAngles(PosDirUtility.ToEulerAnglesFromErrorFormatData(caiJiPeiZhi.face));
		}
	}

	public override void ApplyDefaultState()
	{
		this.SetPositionAndRotation();
		this.UpdateCollider();
		this.ApplyState();
	}

	public override void Release()
	{
		this.collectionDataID = 0;
		this.notTriggerCollider = null;
		this.triggerCollider = null;
		TimerHeap.DelTimer(this.collectedDeleteTimer);
		base.Release();
	}

	public override void Update()
	{
	}

	public override void UpdateHeadInfoState()
	{
	}

	public override void Born()
	{
	}

	public override void Die()
	{
	}

	public override void OnActionStatusExit(ActionStatusExitCmd cmd)
	{
		base.OnActionStatusExit(cmd);
	}

	public override void OnEnter()
	{
		if (this.collectionDataID != 0)
		{
			EventDispatcher.Broadcast<int>(CollectionNPCBehavior.OnEnterNPC, this.collectionDataID);
		}
	}

	public override void OnExit()
	{
		if (this.collectionState != CollectionNPCBehavior.CollectionNPCStateType.Collected)
		{
			FXManager.Instance.DeleteFX(this.collectingFxID);
		}
		if (this.collectionDataID != 0)
		{
			EventDispatcher.Broadcast<int>(CollectionNPCBehavior.OnExitNPC, this.collectionDataID);
		}
	}

	public override int GetState()
	{
		return (int)this.collectionState;
	}

	public override void UpdateState(object state)
	{
		if (this.collectionState == CollectionNPCBehavior.CollectionNPCStateType.Collected)
		{
			return;
		}
		if (this.collectionState == (CollectionNPCBehavior.CollectionNPCStateType)((int)state))
		{
			return;
		}
		this.collectionState = (CollectionNPCBehavior.CollectionNPCStateType)((int)state);
		this.UpdateCollider();
		this.ApplyState();
	}

	protected void ApplyState()
	{
		switch (this.collectionState)
		{
		case CollectionNPCBehavior.CollectionNPCStateType.Normal:
			this.SetNormalState();
			break;
		case CollectionNPCBehavior.CollectionNPCStateType.Collecting:
			this.SetCollectingState();
			break;
		case CollectionNPCBehavior.CollectionNPCStateType.Collected:
			this.SetCollectedState();
			break;
		}
	}

	protected void SetNormalState()
	{
		base.CastAction(base.DefaultIdleActionStatus);
		FXManager.Instance.DeleteFX(this.collectingFxID);
	}

	protected void SetCollectingState()
	{
		CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
		if (caiJiPeiZhi == null)
		{
			return;
		}
		if (caiJiPeiZhi.position2.get_Count() > 2)
		{
			string[] array = caiJiPeiZhi.specialEffects.Split(new char[]
			{
				';'
			});
			if (array.Length > 1)
			{
				int templateId = int.Parse(array[0]);
				this.collectingFxID = FXManager.Instance.PlayFX(templateId, null, new Vector3((float)caiJiPeiZhi.position2.get_Item(0) * 0.01f, (float)caiJiPeiZhi.position2.get_Item(1) * 0.01f, (float)caiJiPeiZhi.position2.get_Item(2) * 0.01f), this.transform.get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
			}
		}
		if (caiJiPeiZhi.LockLookPoint.get_Count() > 2 && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Actor)
		{
			EntityWorld.Instance.EntSelf.Actor.TurnToPos(new Vector3((float)int.Parse(caiJiPeiZhi.LockLookPoint.get_Item(0)) * 0.01f, (float)int.Parse(caiJiPeiZhi.LockLookPoint.get_Item(1)) * 0.01f, (float)int.Parse(caiJiPeiZhi.LockLookPoint.get_Item(2)) * 0.01f));
		}
	}

	protected void SetCollectedState()
	{
		CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
		if (caiJiPeiZhi == null)
		{
			return;
		}
		if (this.animator && this.animator.HasAction(caiJiPeiZhi.action2))
		{
			base.CastAction(caiJiPeiZhi.action2);
			string[] array = caiJiPeiZhi.specialEffects.Split(new char[]
			{
				';'
			});
			if (array.Length > 1)
			{
				int start = int.Parse(array[1]);
				this.deleteCollectingFxTimer = TimerHeap.AddTimer((uint)start, 0, delegate
				{
					FXManager.Instance.DeleteFX(this.collectingFxID);
				});
			}
		}
		else
		{
			this.CollectedActionEnd();
		}
	}

	protected void UpdateCollider()
	{
		switch (this.collectionState)
		{
		case CollectionNPCBehavior.CollectionNPCStateType.Normal:
		case CollectionNPCBehavior.CollectionNPCStateType.Collecting:
			this.EnableCollider();
			break;
		case CollectionNPCBehavior.CollectionNPCStateType.Collected:
			this.DisableCollider();
			break;
		}
	}

	public override void OnAnimationEnd(AnimationEndCmd cmd)
	{
		base.OnAnimationEnd(cmd);
		CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(this.collectionDataID);
		if (caiJiPeiZhi == null)
		{
			return;
		}
		if (this.collectionState == CollectionNPCBehavior.CollectionNPCStateType.Collected && cmd.actName == caiJiPeiZhi.action2)
		{
			this.CollectedActionEnd();
		}
	}

	protected void CollectedActionEnd()
	{
		this.collectedDeleteTimer = TimerHeap.AddTimer(20u, 0, delegate
		{
			if (this.collectionDataID != 0)
			{
				EventDispatcher.Broadcast<int>(CollectionNPCBehavior.OnNPCDieEnd, this.collectionDataID);
			}
		});
	}
}
