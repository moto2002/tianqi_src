using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BossNPCBehavior : NPCBehavior
{
	protected enum BossNPCState
	{
		Born = 1,
		InChallenge,
		Die
	}

	public static readonly string OnEnterNPC = "BossNPCBehavior.OnEnterNPC";

	public static readonly string OnExitNPC = "BossNPCBehavior.OnExitNPC";

	public static readonly string OnSeleteNPC = "BossNPCBehavior.OnSeleteNPC";

	public static readonly int InChallengeIconID = 6001;

	protected int monsterDataID;

	protected bool isMultiBoss;

	protected int monsterLv;

	protected Pos originPos;

	protected List<int> originDir = new List<int>();

	protected int monsterState = -1;

	public override bool EnableUpdate
	{
		get
		{
			return false;
		}
	}

	public BossNPCBehavior(int dataID, bool isMulti, int lv, Pos pos, List<int> dir, int state)
	{
		this.monsterDataID = dataID;
		this.isMultiBoss = isMulti;
		this.monsterLv = lv;
		this.originPos = pos;
		this.originDir.AddRange(dir);
		this.monsterState = state;
	}

	public override void Init(int theID, int modelID, Transform root)
	{
		this.id = theID;
		this.transform = root;
		this.ApplyDefaultState();
		if (DataReader<Monster>.Get(this.monsterDataID) == null)
		{
			return;
		}
		if (DataReader<AvatarModel>.Get(modelID) == null)
		{
			return;
		}
		GameObject gameObject = new GameObject("additionalCollider");
		UGUITools.ResetTransform(gameObject.get_transform(), root);
		gameObject.AddComponent<NPCTriggerReceiver>();
		string value = DataReader<YeWaiBOSS>.Get("touch_range").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		List<int> list = new List<int>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(int.Parse(array[i]));
		}
		Collider collider = base.CreateCollider(gameObject, list);
		if (collider != null)
		{
			collider.set_isTrigger(true);
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
			BillboardManager.Instance.AddBillboardsInfo(23, root, (float)avatarModel.height_HP, (long)this.id, false, true, true);
			HeadInfoManager.Instance.SetName(23, (long)this.id, TextColorMgr.GetColorByID(string.Format(GameDataUtils.GetChineseContent((!this.isMultiBoss) ? 505163 : 505164, false), this.monsterLv, GameDataUtils.GetChineseContent(monster.name, false), (!this.isMultiBoss) ? DataReader<YeWaiBOSSJieJi>.Get(this.monsterLv).SingleRank : DataReader<YeWaiBOSSJieJi>.Get(this.monsterLv).ManyRank), (!this.isMultiBoss) ? 1000007 : 1000009));
			ShadowController.ShowShadow((long)this.id, root, false, modelID);
			ActorVisibleManager.Instance.Add((long)this.id, root, 23, 0L);
			this.AppState();
		});
	}

	public override void ApplyDefaultState()
	{
		this.transform.set_position(PosDirUtility.ToTerrainPoint(this.originPos, 0f));
		if (this.originDir.get_Count() >= 3)
		{
			this.transform.set_eulerAngles(PosDirUtility.ToEulerAnglesFromErrorFormatData(this.originDir));
		}
		else
		{
			this.transform.set_eulerAngles(new Vector3(0f, (float)Random.Range(0, 360), 0f));
		}
	}

	public override void Release()
	{
		this.monsterDataID = 0;
		this.monsterState = -1;
		this.monsterLv = 0;
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
		HeadInfoManager.Instance.SetCommonIcon((long)this.id, 0);
		FXManager.Instance.PlayFX(1109, null, this.transform.get_position(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		WildBossManager.Instance.RemoveBossNPC(this.id);
	}

	public override void OnEnter()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int, int>(BossNPCBehavior.OnEnterNPC, this.id, this.monsterDataID);
		}
	}

	public override void OnExit()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int>(BossNPCBehavior.OnExitNPC, this.id);
		}
	}

	public override void OnSeleted()
	{
		if (this.id != 0)
		{
			EventDispatcher.Broadcast<int, int>(BossNPCBehavior.OnSeleteNPC, this.id, this.monsterDataID);
		}
	}

	public override int GetState()
	{
		return this.monsterState;
	}

	public override void UpdateState(object state)
	{
		int num = 0;
		if (!int.TryParse(state.ToString(), ref num))
		{
			return;
		}
		if (this.monsterState == num)
		{
			return;
		}
		this.monsterState = num;
		this.AppState();
	}

	protected void AppState()
	{
		switch (this.monsterState)
		{
		case 1:
			this.Born();
			break;
		case 2:
			this.InChallenge();
			break;
		case 3:
			this.Die();
			break;
		}
	}

	protected void InChallenge()
	{
		HeadInfoManager.Instance.SetCommonIcon((long)this.id, BossNPCBehavior.InChallengeIconID);
	}
}
