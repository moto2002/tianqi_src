using GameData;
using Package;
using System;
using System.Collections;
using UnityEngine;

public class WildMonsterBehavior : NPCBehavior
{
	public static readonly string OnEnterNPC = "WildMonsterBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "WildMonsterBehavior.OnExitNPC";

	protected int monsterDataID;

	protected Pos originPos;

	public ArrayList pointData;

	public override bool EnableUpdate
	{
		get
		{
			return false;
		}
	}

	public WildMonsterBehavior(WildSceneMonsterInfo mData)
	{
		this.monsterDataID = mData.id;
		ArrayList pointDataByGroupKey = MapDataManager.Instance.GetPointDataByGroupKey(mData.sceneId, mData.bornPoint);
		this.pointData = pointDataByGroupKey;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		this.id = 1000000 + theID;
		this.transform = root;
		this.ApplyDefaultState();
		if (DataReader<Monster>.Get(this.monsterDataID) == null)
		{
			return;
		}
		this.SetModel(root, modelID);
	}

	protected void SetModel(Transform root, int modelID)
	{
		base.GetAsyncModel(root, modelID, delegate
		{
			Monster monster = DataReader<Monster>.Get(this.monsterDataID);
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
			CharacterController[] componentsInChildren = root.GetComponentsInChildren<CharacterController>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].set_enabled(avatarModel.collideOff == 0);
				}
			}
			this.animator = root.GetComponentInChildren<Animator>();
			BillboardManager.Instance.AddBillboardsInfo(25, root, (float)avatarModel.height_HP, (long)this.id, false, true, true);
			HeadInfoManager.Instance.SetName(25, (long)this.id, GameDataUtils.GetChineseContent(monster.name, false));
			ShadowController.ShowShadow((long)this.id, root, false, modelID);
			ActorVisibleManager.Instance.Add((long)this.id, root, 25, 0L);
		});
	}

	public override void ApplyDefaultState()
	{
		Vector3 spawnPosition = this.GetSpawnPosition();
		this.transform.set_position(MySceneManager.GetTerrainPoint(spawnPosition.x, spawnPosition.z, 0f));
		this.transform.set_eulerAngles(new Vector3(0f, (float)Random.Range(0, 360), 0f));
	}

	public override void Release()
	{
		base.Release();
	}

	public override void Update()
	{
	}

	public override void UpdateHeadInfoState()
	{
	}

	public override void OnEnter()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int, int>(WildMonsterBehavior.OnEnterNPC, this.id, this.monsterDataID);
		}
	}

	public override void OnExit()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int>(WildMonsterBehavior.OnExitNPC, this.id);
		}
	}

	public override void Born()
	{
	}

	public override void Die()
	{
	}

	public override void UpdateState(object state)
	{
	}

	public override int GetState()
	{
		return -1;
	}

	public Vector3 GetSpawnPosition()
	{
		Vector3 zero = Vector3.get_zero();
		if (this.pointData == null)
		{
			return zero;
		}
		int num = Random.Range(0, this.pointData.get_Count());
		if (this.pointData.get_Item(num) != null)
		{
			Hashtable hashtable = (Hashtable)this.pointData.get_Item(num);
			double num2 = (double)hashtable.get_Item("x") * 0.01;
			double num3 = (double)hashtable.get_Item("y") * 0.01;
			zero = new Vector3((float)num2, 0f, (float)num3);
		}
		return zero;
	}
}
