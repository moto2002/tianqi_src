using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections;
using UnityEngine;

public class EntityCityMonster : EntityParent
{
	protected float actionSpeed;

	protected string aiType = string.Empty;

	protected uint talkTimer;

	protected int talkInterval = 3000;

	protected float talkRate = 0.5f;

	protected ArrayList pointData;

	protected BattleBaseAttrs battleBaseAttrs = new BattleBaseAttrs();

	public float ActionSpeed
	{
		get
		{
			return this.actionSpeed;
		}
		set
		{
			this.actionSpeed = value;
		}
	}

	public string AIType
	{
		get
		{
			return this.aiType;
		}
		set
		{
			this.aiType = value;
		}
	}

	public override BattleBaseAttrs BattleBaseAttrs
	{
		get
		{
			return this.battleBaseAttrs;
		}
	}

	public override int RealMoveSpeed
	{
		get
		{
			return this.BattleBaseAttrs.RealMoveSpeed;
		}
		set
		{
			this.BattleBaseAttrs.RealMoveSpeed = value;
		}
	}

	public override int RealActionSpeed
	{
		get
		{
			return this.BattleBaseAttrs.RealActionSpeed;
		}
		set
		{
			this.BattleBaseAttrs.RealActionSpeed = value;
		}
	}

	public override int ActSpeed
	{
		get
		{
			return this.BattleBaseAttrs.ActSpeed;
		}
		set
		{
			this.BattleBaseAttrs.ActSpeed = value;
		}
	}

	public void SetData(long theID, WildSceneMonsterInfo mData)
	{
		base.ID = theID;
		this.TypeID = mData.id;
		this.ModelID = mData.model;
		this.ActionSpeed = 1f;
		this.AIType = mData.aiId;
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		Vector2 monsterFixBornDirection = InstanceManager.GetMonsterFixBornDirection(monster.monsterBornDirection, base.Pos, base.OwnerID, monster.scenePoint);
		base.Dir = new Vector3(monsterFixBornDirection.x, 0f, monsterFixBornDirection.y);
		this.RealMoveSpeed = DataReader<AvatarModel>.Get(this.ModelID).speed;
		ArrayList pointDataByGroupKey = MapDataManager.Instance.GetPointDataByGroupKey(mData.sceneId, mData.bornPoint);
		this.pointData = pointDataByGroupKey;
		base.Pos = this.GetSpawnPosition();
	}

	public override void OnLeaveField()
	{
		this.Hide();
		if (base.Actor)
		{
			base.Actor.Destroy();
		}
		else
		{
			EntityWorld.Instance.CancelGetCityMonsterActorAsync(base.AsyncLoadID);
		}
		EntityWorld.Instance.RemoveCityMonster(this);
		base.OnLeaveField();
	}

	public override void CreateActor()
	{
		base.AsyncLoadID = EntityWorld.Instance.GetCityMonsterActorAsync(this.ModelID, delegate(ActorWildMonster actorMonster)
		{
			Monster monster = DataReader<Monster>.Get(this.TypeID);
			base.Actor = actorMonster;
			base.Actor.InitActionPriorityTable();
			actorMonster.theEntity = this;
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.ModelID);
			BillboardManager.Instance.AddBillboardsInfo(25, base.Actor.FixTransform, (float)avatarModel.height_HP, base.ID, false, true, true);
			ShadowController.ShowShadow(base.ID, base.Actor.FixTransform, false, this.modelID);
			LayerSystem.SetGameObjectLayer(base.Actor.FixGameObject, "CityPlayer", 2);
			ActorVisibleManager.Instance.Add(base.ID, base.Actor.FixTransform, 25, 0L);
			base.Actor.SetAllCollider(true);
			this.Show();
		});
	}

	protected override void InitEntityState()
	{
		base.InitEntityState();
	}

	public override void InitActorState()
	{
		base.SetPos(base.Pos);
		base.SetDir(base.Dir);
		base.SetMoveSpeed((long)this.RealMoveSpeed);
		base.SetDefaultActionSpeed((long)this.ActSpeed);
		base.SetRunActionSpeed((long)this.RealActionSpeed);
		this.ActiveMonster();
	}

	public void Show()
	{
		this.InitActorState();
	}

	public void Hide()
	{
		TimerHeap.DelTimer(this.talkTimer);
	}

	protected void CheckTalk()
	{
		if (Random.get_value() > this.talkRate)
		{
			return;
		}
		this.Talk();
	}

	protected void Talk()
	{
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

	protected override void InitManager()
	{
		this.m_subSystems.Add("CityMonsterAI", new CityMonsterAIManager());
		this.m_subSystems.Add("CityMonsterCondition", new CityMonsterConditionManager());
		base.InitManager();
	}

	protected void ActiveMonster()
	{
		if (base.GetAIManager() != null)
		{
			base.GetAIManager().Deactive();
			base.GetAIManager().AIType = this.AIType;
			base.GetAIManager().Active();
		}
	}

	protected void DeactiveMonster()
	{
		if (base.GetAIManager() != null)
		{
			base.GetAIManager().Deactive();
		}
	}
}
