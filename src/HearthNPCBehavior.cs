using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HearthNPCBehavior : NPCBehavior
{
	public static readonly string OnEnterNPC = "HearthNPCBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "HearthNPCBehavior.OnExitNPC";

	protected int hearthDataID;

	protected Collider triggerCollider;

	public override bool EnableUpdate
	{
		get
		{
			return false;
		}
	}

	public HearthNPCBehavior(int dataID)
	{
		this.hearthDataID = dataID;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		this.id = theID;
		this.transform = root;
		ChuanSongMenNPC chuanSongMenNPC = DataReader<ChuanSongMenNPC>.Get(this.hearthDataID);
		if (chuanSongMenNPC == null)
		{
			return;
		}
		this.SetCollider(root, chuanSongMenNPC.triggeredRange);
		this.ApplyDefaultState();
		this.SetModel(root, modelID);
	}

	protected void SetModel(Transform root, int modelID)
	{
		base.GetAsyncModel(root, modelID, delegate
		{
		});
	}

	protected void SetCollider(Transform root, List<int> triggeredRange)
	{
		GameObject gameObject = new GameObject("triggerAgent");
		UGUITools.ResetTransform(gameObject.get_transform(), root);
		gameObject.AddComponent<NPCTriggerReceiver>();
		this.triggerCollider = base.CreateCollider(gameObject, triggeredRange);
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
		ChuanSongMenNPC chuanSongMenNPC = DataReader<ChuanSongMenNPC>.Get(this.hearthDataID);
		if (chuanSongMenNPC == null)
		{
			return;
		}
		if (chuanSongMenNPC.position.get_Count() >= 3)
		{
			this.transform.set_position(PosDirUtility.ToTerrainPoint(chuanSongMenNPC.position));
		}
		if (chuanSongMenNPC.face.get_Count() >= 3)
		{
			this.transform.set_eulerAngles(PosDirUtility.ToEulerAnglesFromErrorFormatData(chuanSongMenNPC.face));
		}
	}

	public override void ApplyDefaultState()
	{
		this.SetPositionAndRotation();
		this.EnableCollider();
	}

	public override void Release()
	{
		this.hearthDataID = 0;
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
	}

	public override void Die()
	{
	}

	public override void OnEnter()
	{
		if (this.hearthDataID != 0)
		{
			EventDispatcher.Broadcast<int>(HearthNPCBehavior.OnEnterNPC, this.hearthDataID);
		}
	}

	public override void OnExit()
	{
		if (this.hearthDataID != 0)
		{
			EventDispatcher.Broadcast<int>(HearthNPCBehavior.OnExitNPC, this.hearthDataID);
		}
	}

	public override int GetState()
	{
		return 0;
	}

	public override void UpdateState(object state)
	{
	}
}
