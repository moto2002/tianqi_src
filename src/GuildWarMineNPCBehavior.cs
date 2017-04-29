using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildWarMineNPCBehavior : NPCBehavior
{
	public static readonly string OnEnterNPC = "GuildWarNPCBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "GuildWarNPCBehavior.OnExitNPC";

	protected int guildWarMineNPCDataID;

	protected List<string> mineLiveData;

	protected Collider notTriggerCollider;

	protected Collider triggerCollider;

	public override bool EnableUpdate
	{
		get
		{
			return false;
		}
	}

	public GuildWarMineNPCBehavior(int dataID, List<string> theMineLiveData)
	{
		this.guildWarMineNPCDataID = dataID;
		this.mineLiveData = theMineLiveData;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		this.id = theID;
		this.transform = root;
		JunTuanZhanCaiJi junTuanZhanCaiJi = DataReader<JunTuanZhanCaiJi>.Get(this.guildWarMineNPCDataID);
		if (junTuanZhanCaiJi == null)
		{
			return;
		}
		this.SetCollider(root, junTuanZhanCaiJi.TouchRange, junTuanZhanCaiJi.CollectionRange);
		this.ApplyDefaultState();
		this.SetModel(root, modelID);
	}

	protected void SetModel(Transform root, int modelID)
	{
		base.GetAsyncModel(root, modelID, delegate
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
			if (avatarModel == null)
			{
				return;
			}
			BillboardManager.Instance.AddBillboardsInfo(23, root, (float)avatarModel.height_HP, (long)this.id, false, false, true);
			HeadInfoManager.Instance.SetName(23, (long)this.id, this.GetName());
			ShadowController.ShowShadow((long)this.id, root, false, modelID);
		});
	}

	protected string GetName()
	{
		if (this.mineLiveData == null || this.mineLiveData.get_Count() < 3)
		{
			return GameDataUtils.GetChineseContent(515082, false);
		}
		return TextColorMgr.GetColor(string.Format(GameDataUtils.GetChineseContent(515081, false), new object[]
		{
			GameDataUtils.GetChineseContent(DataReader<JunTuanZhanCaiJi>.Get(this.guildWarMineNPCDataID).Name, false),
			TextColorMgr.GetColor(this.mineLiveData.get_Item(0), "6adc32", string.Empty),
			TextColorMgr.GetColor(this.mineLiveData.get_Item(1), "ff4040", string.Empty),
			this.mineLiveData.get_Item(2)
		}), "f1e5b7", string.Empty);
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
		if (this.triggerCollider)
		{
			this.triggerCollider.set_enabled(MySceneManager.Instance.IsSceneExist);
		}
	}

	protected void SetPositionAndRotation()
	{
		JunTuanZhanCaiJi junTuanZhanCaiJi = DataReader<JunTuanZhanCaiJi>.Get(this.guildWarMineNPCDataID);
		if (junTuanZhanCaiJi == null)
		{
			return;
		}
		if (junTuanZhanCaiJi.CoordinatePoint.get_Count() >= 3)
		{
			this.transform.set_position(PosDirUtility.ToTerrainPoint(junTuanZhanCaiJi.CoordinatePoint));
		}
	}

	public override void ApplyDefaultState()
	{
		this.SetPositionAndRotation();
		this.EnableCollider();
	}

	public override void Release()
	{
		this.guildWarMineNPCDataID = 0;
		this.triggerCollider = null;
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
		HeadInfoManager.Instance.SetCommonIcon((long)this.id, 0);
		if (this.animator)
		{
			this.animator.Play("idle");
		}
	}

	public override void Die()
	{
	}

	public override void OnEnter()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int>(GuildWarMineNPCBehavior.OnEnterNPC, this.guildWarMineNPCDataID);
		}
	}

	public override void OnExit()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int>(GuildWarMineNPCBehavior.OnExitNPC, this.guildWarMineNPCDataID);
		}
	}

	public override int GetState()
	{
		return 0;
	}

	public override void UpdateState(object state)
	{
		List<string> list = state as List<string>;
		if (list == null)
		{
			return;
		}
		this.mineLiveData.Clear();
		if (list.get_Count() < 3)
		{
			return;
		}
		this.mineLiveData.AddRange(list);
		this.AppState();
	}

	protected void AppState()
	{
		HeadInfoManager.Instance.SetName(23, (long)this.id, this.GetName());
	}
}
