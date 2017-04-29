using GameData;
using System;
using UnityEngine;

public class EntityCityPet
{
	protected long id;

	protected int typeID;

	protected int rank;

	protected int modelID;

	protected long ownerID;

	protected EntityParent owner;

	protected float moveSpeed;

	protected float actionSpeed;

	protected int asyncLoadID;

	protected ActorCityPet actor;

	protected bool isFollowing;

	protected uint talkTimer;

	protected int talkInterval = 3000;

	protected float talkRate = 0.5f;

	public long ID
	{
		get
		{
			return this.id;
		}
		set
		{
			this.id = value;
		}
	}

	public int TypeID
	{
		get
		{
			return this.typeID;
		}
		set
		{
			this.typeID = value;
		}
	}

	public int Rank
	{
		get
		{
			return this.rank;
		}
		set
		{
			this.rank = value;
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

	public long OwnerID
	{
		get
		{
			return this.ownerID;
		}
		set
		{
			this.ownerID = value;
		}
	}

	public EntityParent Owner
	{
		get
		{
			return this.owner;
		}
		set
		{
			this.owner = value;
		}
	}

	public float MoveSpeed
	{
		get
		{
			return this.moveSpeed;
		}
		set
		{
			this.moveSpeed = value;
		}
	}

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

	public int AsyncLoadID
	{
		get
		{
			return this.asyncLoadID;
		}
		set
		{
			this.asyncLoadID = value;
		}
	}

	public ActorCityPet Actor
	{
		get
		{
			return this.actor;
		}
		set
		{
			this.actor = value;
		}
	}

	public bool IsFollowing
	{
		get
		{
			return this.isFollowing;
		}
		set
		{
			this.isFollowing = value;
		}
	}

	public void SetData(long theID, int theTypeID, int theRank, EntityParent theOwner)
	{
		this.ID = theID;
		this.TypeID = theTypeID;
		this.Rank = theRank;
		this.Owner = theOwner;
		this.MoveSpeed = (float)theOwner.MoveSpeed;
		this.ModelID = PetManagerBase.GetPlayerPetModel(this.TypeID, this.Rank);
		this.OwnerID = theOwner.ID;
		this.ActionSpeed = 1f;
		this.InitEntityState();
	}

	public void OnEnterField()
	{
		EntityWorld.Instance.AddCityPet(this);
	}

	public void OnLeaveField()
	{
		this.Hide();
		if (this.Actor)
		{
			this.Actor.OnCallToDestroy();
		}
		else
		{
			EntityWorld.Instance.CancelGetCityPetActorAsync(this.AsyncLoadID);
		}
		EntityWorld.Instance.RemoveCityPet(this);
	}

	public void CreateActor()
	{
		this.AsyncLoadID = EntityWorld.Instance.GetCityPetActorAsync(this.ModelID, delegate(ActorCityPet actorCityPet)
		{
			Pet dataPet = DataReader<Pet>.Get(this.TypeID);
			this.Actor = actorCityPet;
			actorCityPet.Entity = this;
			this.Actor.FixGameObject.set_name(this.ID.ToString());
			this.Actor.SetScale(PetManagerBase.GetCityPetModelZoom(dataPet, this.ModelID));
			ShadowController.ShowShadow(this.ID, this.Actor.FixTransform, false, this.ModelID);
			ActorVisibleManager.Instance.Add(this.ID, this.Actor.FixTransform, 22, this.OwnerID);
			this.Show();
		});
	}

	protected void InitEntityState()
	{
		Pet pet = DataReader<Pet>.Get(this.TypeID);
		this.talkInterval = pet.interval2 * 1000;
		this.talkRate = pet.wordProbability;
	}

	protected void InitActorState()
	{
		if (EntityWorld.Instance.EntSelf != null && this.OwnerID == EntityWorld.Instance.EntSelf.ID)
		{
			this.talkTimer = TimerHeap.AddTimer((uint)this.talkInterval, this.talkInterval, new Action(this.CheckTalk));
		}
	}

	public void Show()
	{
		this.Actor.FixGameObject.SetActive(true);
		this.Actor.Init();
		this.Actor.CastAction("idle");
		this.InitActorState();
	}

	public void Hide()
	{
		TimerHeap.DelTimer(this.talkTimer);
		if (this.Actor)
		{
			BubbleDialogueManager.Instance.RemoveBubbleDialogue(this.ID, this.Actor.FixTransform);
			this.Actor.FixGameObject.SetActive(false);
		}
	}

	protected void CheckTalk()
	{
		if (this.IsFollowing)
		{
			return;
		}
		if (Random.get_value() > this.talkRate)
		{
			return;
		}
		this.Talk();
	}

	protected void Talk()
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.ModelID);
		Pet pet = DataReader<Pet>.Get(this.TypeID);
		if (BubbleDialogueManager.Instance.AddBubbleDialogueLimit(this.Actor.FixTransform, (float)avatarModel.height_HP, this.ID, 0))
		{
			BubbleDialogueManager.Instance.SetContentsByRandom(this.ID, pet.word, pet.duration);
		}
	}
}
