using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using XEngineActor;

public class EntityWorld
{
	protected static EntityWorld instance;

	protected XDict<Type, XDict<long, EntityParent>> subEntities = new XDict<Type, XDict<long, EntityParent>>();

	protected XDict<long, EntityParent> allEntities = new XDict<long, EntityParent>();

	protected EntitySelf entSelf;

	protected ActorSelf actSelf;

	protected Transform traSelf;

	protected Dictionary<long, EntityPet> entCurPet = new Dictionary<long, EntityPet>();

	protected XDict<long, EntityCityPet> allCityPets = new XDict<long, EntityCityPet>();

	protected XDict<long, EntityCityMonster> allCityMonsters = new XDict<long, EntityCityMonster>();

	protected EntityParent lockOnTarget;

	protected List<EntityCityPlayer> cityPlayerEntityPool = new List<EntityCityPlayer>();

	protected object cityPlayerEntityPoolLock = new object();

	protected List<PosRecord> allPosRecord = new List<PosRecord>();

	protected int maxPosRecord = 500;

	public static EntityWorld Instance
	{
		get
		{
			if (EntityWorld.instance == null)
			{
				EntityWorld.instance = new EntityWorld();
			}
			return EntityWorld.instance;
		}
	}

	public XDict<long, EntityParent> AllEntities
	{
		get
		{
			return this.allEntities;
		}
		set
		{
			this.allEntities = value;
		}
	}

	public EntitySelf EntSelf
	{
		get
		{
			return this.entSelf;
		}
		set
		{
			this.entSelf = value;
		}
	}

	public ActorSelf ActSelf
	{
		get
		{
			return this.actSelf;
		}
		set
		{
			this.actSelf = value;
			EventDispatcher.Broadcast(EventNames.ActSelfChanged);
		}
	}

	public Transform TraSelf
	{
		get
		{
			return this.traSelf;
		}
		set
		{
			this.traSelf = value;
			EventDispatcher.Broadcast(EventNames.TraSelfChanged);
		}
	}

	public bool IsEntitySelfInBattle
	{
		get
		{
			return this.EntSelf != null && this.EntSelf.IsInBattle;
		}
	}

	public Dictionary<long, EntityPet> EntCurPet
	{
		get
		{
			return this.entCurPet;
		}
		set
		{
			this.entCurPet = value;
		}
	}

	public XDict<long, EntityCityPet> AllCityPets
	{
		get
		{
			return this.allCityPets;
		}
		set
		{
			this.allCityPets = value;
		}
	}

	public XDict<long, EntityCityMonster> AllCityMonsters
	{
		get
		{
			return this.allCityMonsters;
		}
		set
		{
			this.allCityMonsters = value;
		}
	}

	public EntityParent LockOnTarget
	{
		get
		{
			return this.lockOnTarget;
		}
		set
		{
			this.lockOnTarget = value;
		}
	}

	public List<EntityCityPlayer> CityPlayerEntityPool
	{
		get
		{
			return this.cityPlayerEntityPool;
		}
	}

	protected EntityWorld()
	{
	}

	public void Init()
	{
		EventDispatcher.AddListener<BattleAction_Relive, bool>(BattleActionEvent.Relive, new Callback<BattleAction_Relive, bool>(this.CheckEntityRelive));
		EventDispatcher.AddListener<BattleAction_NewBatchNty, bool>(BattleActionEvent.NewBatchNty, new Callback<BattleAction_NewBatchNty, bool>(this.GetNewBatch));
		EventDispatcher.AddListener<BattleAction_AllLoadDoneNty, bool>(BattleActionEvent.AllLoadDoneNty, new Callback<BattleAction_AllLoadDoneNty, bool>(this.GetAllClientLoadDone));
	}

	public void Release()
	{
		EventDispatcher.RemoveListener<BattleAction_Relive, bool>(BattleActionEvent.Relive, new Callback<BattleAction_Relive, bool>(this.CheckEntityRelive));
		EventDispatcher.RemoveListener<BattleAction_NewBatchNty, bool>(BattleActionEvent.NewBatchNty, new Callback<BattleAction_NewBatchNty, bool>(this.GetNewBatch));
		EventDispatcher.RemoveListener<BattleAction_AllLoadDoneNty, bool>(BattleActionEvent.AllLoadDoneNty, new Callback<BattleAction_AllLoadDoneNty, bool>(this.GetAllClientLoadDone));
		EntityWorld.Instance.ClearAllMapObjects();
		EntityWorld.Instance.ReleaseSelf();
		for (int i = 0; i < this.subEntities.Count; i++)
		{
			this.subEntities.ElementValueAt(i).Clear();
		}
		this.subEntities.Clear();
		this.allEntities.Clear();
		this.entSelf = null;
		this.actSelf = null;
		this.traSelf = null;
		this.entCurPet.Clear();
		this.allCityPets.Clear();
		this.allCityMonsters.Clear();
	}

	public void AddEntity<T>(T entity) where T : EntityParent
	{
		if (!this.subEntities.ContainsKey(entity.GetType()))
		{
			this.subEntities.Add(entity.GetType(), new XDict<long, EntityParent>());
		}
		this.allEntities.Add(entity.ID, entity);
		this.subEntities[entity.GetType()].Add(entity.ID, entity);
	}

	public void RemoveEntity<T>(T entity) where T : EntityParent
	{
		this.allEntities.Remove(entity.ID);
		Type type = entity.GetType();
		if (this.subEntities.ContainsKey(type))
		{
			this.subEntities[type].Remove(entity.ID);
		}
		entity = (T)((object)null);
	}

	public T GetEntity<T>(long entityId) where T : EntityParent
	{
		if (this.allEntities.ContainsKey(entityId))
		{
			return this.allEntities[entityId] as T;
		}
		return (T)((object)null);
	}

	public void CreateSelf(RoleInfo info)
	{
		EntitySelf entitySelf = new EntitySelf();
		entitySelf.SetDataByRoleInfo(info);
		this.EntSelf = entitySelf;
		entitySelf.OnEnterField();
		entitySelf.CreateActor();
	}

	public void CreateCityOtherPlayer(MapObjInfo info)
	{
		EntityCityPlayer aCityPlayer = this.GetACityPlayer();
		aCityPlayer.OnCreate(info, false);
		aCityPlayer.OnEnterField();
		aCityPlayer.CreateActor();
	}

	public void CreateOtherPlayer(MapObjInfo info)
	{
		EntityPlayer entityPlayer = new EntityPlayer();
		entityPlayer.OnCreate(info, false);
		entityPlayer.OnEnterField();
		entityPlayer.CreateActor();
	}

	public EntityMonster CreateMonster(MapObjInfo info, bool isClient, long noumenonID = 0L)
	{
		if (info == null)
		{
			return null;
		}
		if (info.battleInfo == null)
		{
			return null;
		}
		if (info.battleInfo.battleBaseAttr == null)
		{
			return null;
		}
		if (info.battleInfo.battleBaseAttr.Hp <= 0L)
		{
			return null;
		}
		EntityMonster entityMonster = new EntityMonster();
		entityMonster.OnCreate(info, isClient, noumenonID);
		entityMonster.OnEnterField();
		entityMonster.CreateActor();
		return entityMonster;
	}

	public EntityPet CreatePet(MapObjInfo info, bool isClient)
	{
		if (info == null)
		{
			return null;
		}
		if (info.battleInfo == null)
		{
			return null;
		}
		if (info.battleInfo.battleBaseAttr == null)
		{
			return null;
		}
		if (info.battleInfo.battleBaseAttr.Hp <= 0L)
		{
			return null;
		}
		EntityPet entityPet = new EntityPet();
		entityPet.OnCreate(info, isClient);
		entityPet.OnEnterField();
		entityPet.CreateActor();
		return entityPet;
	}

	public ActorSelf GetSelfActor(int modelID)
	{
		return AvatarPool.Instance.Get(modelID);
	}

	public int GetSelfActorAsync(int modelID, Action<ActorSelf> callback)
	{
		return AvatarPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetSelfActorAsync(int asyncID)
	{
		AvatarPool.Instance.DestroyById(asyncID, null);
	}

	public ActorPlayer GetPlayerActor(int modelID)
	{
		return PlayerPool.Instance.Get(modelID);
	}

	public int GetPlayerActorAsync(int modelID, Action<ActorPlayer> callback)
	{
		return PlayerPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetPlayerActorAsync(int asyncID)
	{
		PlayerPool.Instance.DestroyById(asyncID, null);
	}

	public ActorCityPlayer GetCityPlayerActor(int modelID)
	{
		return CityPlayerPool.Instance.Get(modelID);
	}

	public int GetCityPlayerActorAsync(int modelID, Action<ActorCityPlayer> callback)
	{
		return CityPlayerPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetCityPlayerActorAsync(int asyncID)
	{
		CityPlayerPool.Instance.DestroyById(asyncID, null);
	}

	public ActorPet GetPetActor(int modelID)
	{
		return PetPool.Instance.Get(modelID);
	}

	public int GetPetActorAsync(int modelID, Action<ActorPet> callback)
	{
		return PetPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetPetActorAsync(int asyncID)
	{
		PetPool.Instance.DestroyById(asyncID, null);
	}

	public ActorMonster GetMonsterActor(int modelID)
	{
		return MonsterPool.Instance.Get(modelID);
	}

	public int GetMonsterActorAsync(int modelID, Action<ActorMonster> callback)
	{
		return MonsterPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetMonsterActorAsync(int asyncID)
	{
		MonsterPool.Instance.DestroyById(asyncID, null);
	}

	public ActorMonster MonsterHaunt(int golemID, int modelID)
	{
		Golem[] array = Object.FindObjectsOfType<Golem>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].ID == golemID)
			{
				Actor component = array[i].get_gameObject().GetComponent<Actor>();
				if (component)
				{
					Object.Destroy(component);
				}
				ActorMonster actorMonster = array[i].get_gameObject().AddComponent<ActorMonster>();
				actorMonster.resGUID = modelID;
				actorMonster.InstanceID = actorMonster.GetInstanceID();
				actorMonster.GameObjectID = array[i].get_gameObject().GetInstanceID();
				AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
				actorMonster.IsLockModelDir = (avatarModel.lockDirection == 1);
				actorMonster.FrameLayerState = actorMonster.FrameLayerState;
				return actorMonster;
			}
		}
		return null;
	}

	public void AddCityMonster(EntityCityMonster cityMonster)
	{
		if (this.allCityMonsters.ContainsKey(cityMonster.ID))
		{
			this.RemoveEntityCityMonster(cityMonster.ID);
		}
		this.allCityMonsters.Add(cityMonster.ID, cityMonster);
	}

	public void RemoveEntityCityMonster(long id)
	{
		if (this.allCityMonsters.ContainsKey(id))
		{
			this.allCityMonsters[id].OnLeaveField();
		}
	}

	public void RemoveCityMonster(EntityCityMonster cityMonster)
	{
		this.allCityMonsters.Remove(cityMonster.ID);
		cityMonster = null;
	}

	public ActorWildMonster GetCityMonsterActor(int modelID)
	{
		return CityMonsterPool.Instance.Get(modelID);
	}

	public int GetCityMonsterActorAsync(int modelID, Action<ActorWildMonster> callback)
	{
		return CityMonsterPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetCityMonsterActorAsync(int asyncID)
	{
		CityMonsterPool.Instance.DestroyById(asyncID, null);
	}

	public ActorSelf GetSelfActor(GameObject root, string mountPointName)
	{
		return this.GetSelfActor(XUtility.RecursiveFindGameObject(root, mountPointName));
	}

	public ActorPlayer GetPlayerActor(GameObject root, string mountPointName)
	{
		return this.GetPlayerActor(XUtility.RecursiveFindGameObject(root, mountPointName));
	}

	public ActorPet GetPetActor(GameObject root, string mountPointName)
	{
		return this.GetPetActor(XUtility.RecursiveFindGameObject(root, mountPointName));
	}

	public ActorMonster GetMonsterActor(GameObject root, string mountPointName, int way)
	{
		GameObject gameObject = XUtility.RecursiveFindGameObject(root, mountPointName);
		if (way == 0)
		{
			return this.GetMonsterActor(gameObject, gameObject);
		}
		return this.GetMonsterActor(root, gameObject);
	}

	protected ActorSelf GetSelfActor(GameObject parent)
	{
		if (!parent)
		{
			return null;
		}
		GameObject gameObject = new GameObject();
		gameObject.get_transform().set_parent(parent.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero());
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		ActorSelf actorSelf = gameObject.AddUniqueComponent<ActorSelf>();
		actorSelf.resGUID = 0;
		actorSelf.InstanceID = actorSelf.GetInstanceID();
		actorSelf.GameObjectID = gameObject.GetInstanceID();
		return actorSelf;
	}

	protected ActorPlayer GetPlayerActor(GameObject parent)
	{
		if (!parent)
		{
			return null;
		}
		GameObject gameObject = new GameObject();
		gameObject.get_transform().set_parent(parent.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero());
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		ActorPlayer actorPlayer = gameObject.AddUniqueComponent<ActorPlayer>();
		actorPlayer.resGUID = 0;
		actorPlayer.InstanceID = actorPlayer.GetInstanceID();
		actorPlayer.GameObjectID = gameObject.GetInstanceID();
		return actorPlayer;
	}

	protected ActorPet GetPetActor(GameObject parent)
	{
		if (!parent)
		{
			return null;
		}
		GameObject gameObject = new GameObject();
		gameObject.get_transform().set_parent(parent.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero());
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		ActorPet actorPet = gameObject.AddUniqueComponent<ActorPet>();
		actorPet.resGUID = 0;
		actorPet.InstanceID = actorPet.GetInstanceID();
		actorPet.GameObjectID = gameObject.GetInstanceID();
		return actorPet;
	}

	protected ActorMonster GetMonsterActor(GameObject parent, GameObject mountPoint)
	{
		if (!parent)
		{
			return null;
		}
		if (!mountPoint)
		{
			return null;
		}
		GameObject gameObject = new GameObject();
		gameObject.get_transform().set_parent(parent.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero());
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		IEnumerator enumerator = mountPoint.get_transform().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.get_Current();
				if (transform.get_name() == "head_fx_node" || transform.get_name() == "body_fx_node" || transform.get_name() == "foot_fx_node")
				{
					GameObject gameObject2 = new GameObject();
					gameObject2.set_name(transform.get_name());
					gameObject2.get_transform().set_parent(gameObject.get_transform());
					gameObject2.get_transform().set_localPosition(transform.get_localPosition());
					gameObject2.get_transform().set_localRotation(transform.get_localRotation());
					gameObject2.get_transform().set_localScale(transform.get_localScale());
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		CharacterController component = mountPoint.GetComponent<CharacterController>();
		if (component)
		{
			CharacterController characterController = gameObject.AddUniqueComponent<CharacterController>();
			characterController.set_slopeLimit(component.get_slopeLimit());
			characterController.set_stepOffset(component.get_stepOffset());
			characterController.set_center(component.get_center());
			characterController.set_radius(component.get_radius());
			characterController.set_height(component.get_height());
			characterController.set_contactOffset(component.get_contactOffset());
			Object.Destroy(component);
		}
		ActorMonster actorMonster = gameObject.AddUniqueComponent<ActorMonster>();
		actorMonster.resGUID = 0;
		actorMonster.InstanceID = actorMonster.GetInstanceID();
		actorMonster.GameObjectID = gameObject.GetInstanceID();
		actorMonster.MountGameObject = mountPoint;
		return actorMonster;
	}

	public void RemoveEntityByID(long id)
	{
		EntityParent entityByID = this.GetEntityByID(id);
		if (entityByID == null)
		{
			return;
		}
		if (entityByID.ObjType == 0)
		{
			this.RemoveCityOtherPlayer(id);
		}
		else
		{
			switch (entityByID.WrapType)
			{
			case 0:
				this.RemovePlayer(id);
				break;
			case 1:
				this.RemoveMonster(id);
				break;
			case 2:
				this.RemovePet(id);
				break;
			}
		}
	}

	public void RemoveCityOtherPlayer(long id)
	{
		this.GetEntities<EntityCityPlayer>()[id].OnLeaveField();
	}

	public void RemovePlayer(long id)
	{
		EntityParent entityByID = this.GetEntityByID(id);
		if (entityByID != null)
		{
			(entityByID as EntityPlayer).ReceiveServerRemove();
		}
	}

	public void RemovePet(long id)
	{
		this.GetEntities<EntityPet>()[id].OnLeaveField();
	}

	public void RemoveMonster(long id)
	{
		this.GetEntities<EntityMonster>()[id].OnLeaveField();
	}

	public void ClearAllMapObjects()
	{
		this.EntCurPet.Clear();
		this.ClearEntities<EntityCityPlayer>();
		this.ClearEntities<EntityPlayer>();
		this.ClearEntities<EntityMonster>();
		this.ClearEntities<EntityPet>();
		this.ClearEntityCityPet();
		this.ClearEntities<EntityCityMonster>();
	}

	protected void ClearEntities<T>() where T : EntityParent
	{
		List<long> list = new List<long>(this.GetEntities<T>().Keys);
		XDict<long, EntityParent> entities = this.GetEntities<T>();
		for (int i = 0; i < list.get_Count(); i++)
		{
			entities[list.get_Item(i)].OnLeaveField();
		}
	}

	public void ReleaseSelf()
	{
		if (this.EntSelf != null)
		{
			this.EntSelf.OnLeaveField();
		}
		this.EntSelf = null;
	}

	public void KillAllMonsters()
	{
		List<EntityParent> values = this.GetEntities<EntityMonster>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			EntityMonster entityMonster = values.get_Item(i) as EntityMonster;
			if (values.get_Item(i) != null)
			{
				if (!entityMonster.IsPlayerMate && !entityMonster.IsComponont)
				{
					entityMonster.Hp = 0L;
				}
			}
		}
	}

	public void AddCityPet(EntityCityPet cityPet)
	{
		if (this.allCityPets.ContainsKey(cityPet.ID))
		{
			this.RemoveEntityCityPet(cityPet.ID);
		}
		this.allCityPets.Add(cityPet.ID, cityPet);
	}

	public void RemoveCityPet(EntityCityPet cityPet)
	{
		this.allCityPets.Remove(cityPet.ID);
		cityPet = null;
	}

	public void CreateEntityCityPet(long id, int typeID, int rank, EntityParent owner)
	{
		if (owner == null)
		{
			return;
		}
		EntityCityPet entityCityPet = new EntityCityPet();
		entityCityPet.SetData(id, typeID, rank, owner);
		entityCityPet.OnEnterField();
		entityCityPet.CreateActor();
	}

	public void RemoveEntityCityPet(long id)
	{
		if (this.allCityPets.ContainsKey(id))
		{
			this.allCityPets[id].OnLeaveField();
		}
	}

	public void ClearEntityCityPet()
	{
		List<long> list = new List<long>(this.allCityPets.Keys);
		for (int i = 0; i < list.get_Count(); i++)
		{
			this.allCityPets[list.get_Item(i)].OnLeaveField();
		}
		this.allCityPets.Clear();
	}

	public ActorCityPet GetCityPetActor(int modelID)
	{
		return CityPetPool.Instance.Get(modelID);
	}

	public int GetCityPetActorAsync(int modelID, Action<ActorCityPet> callback)
	{
		return CityPetPool.Instance.GetAsync(modelID, callback);
	}

	public void CancelGetCityPetActorAsync(int asyncID)
	{
		CityPetPool.Instance.DestroyById(asyncID, null);
	}

	protected EntityCityPlayer GetACityPlayer()
	{
		object obj = this.cityPlayerEntityPoolLock;
		EntityCityPlayer result;
		lock (obj)
		{
			if (this.CityPlayerEntityPool.get_Count() > 0)
			{
				EntityCityPlayer entityCityPlayer = this.CityPlayerEntityPool.get_Item(0);
				this.CityPlayerEntityPool.RemoveAt(0);
				result = entityCityPlayer;
			}
			else
			{
				result = new EntityCityPlayer();
			}
		}
		return result;
	}

	public void ReuseCityPlayer(EntityCityPlayer entity)
	{
		this.allEntities.Remove(entity.ID);
		Type type = entity.GetType();
		if (this.subEntities.ContainsKey(type))
		{
			this.subEntities[type].Remove(entity.ID);
		}
		object obj = this.cityPlayerEntityPoolLock;
		lock (obj)
		{
			this.CityPlayerEntityPool.Add(entity);
		}
	}

	public XDict<long, EntityParent> GetEntities<T>() where T : EntityParent
	{
		Type typeFromHandle = typeof(T);
		if (this.subEntities.ContainsKey(typeFromHandle) && this.subEntities[typeFromHandle] != null)
		{
			return this.subEntities[typeFromHandle];
		}
		return new XDict<long, EntityParent>();
	}

	public XDict<long, T> GetEntitiesByCamp<T>(int camp) where T : EntityParent
	{
		XDict<long, T> xDict = new XDict<long, T>();
		Type typeFromHandle = typeof(T);
		if (this.subEntities.ContainsKey(typeFromHandle) && this.subEntities[typeFromHandle] != null)
		{
			XDict<long, EntityParent> xDict2 = this.subEntities[typeFromHandle];
			for (int i = 0; i < xDict2.Count; i++)
			{
				EntityParent entityParent = xDict2.ElementValueAt(i);
				if (entityParent.Camp == camp)
				{
					xDict.Add(xDict2.ElementKeyAt(i), entityParent as T);
				}
			}
			return xDict;
		}
		return new XDict<long, T>();
	}

	public XDict<long, T> GetEntitiesByTypeID<T>(int typeID) where T : EntityParent
	{
		XDict<long, T> xDict = new XDict<long, T>();
		Type typeFromHandle = typeof(T);
		if (this.subEntities.ContainsKey(typeFromHandle) && this.subEntities[typeFromHandle] != null)
		{
			XDict<long, EntityParent> xDict2 = this.subEntities[typeFromHandle];
			for (int i = 0; i < xDict2.Count; i++)
			{
				EntityParent entityParent = xDict2.ElementValueAt(i);
				if (entityParent.TypeID == typeID)
				{
					xDict.Add(xDict2.ElementKeyAt(i), entityParent as T);
				}
			}
			return xDict;
		}
		return new XDict<long, T>();
	}

	public T GetAnEntityByTypeID<T>(int typeID) where T : EntityParent
	{
		Type typeFromHandle = typeof(T);
		if (this.subEntities.ContainsKey(typeFromHandle) && this.subEntities[typeFromHandle] != null)
		{
			XDict<long, EntityParent> xDict = this.subEntities[typeFromHandle];
			for (int i = 0; i < xDict.Count; i++)
			{
				EntityParent entityParent = xDict.ElementValueAt(i);
				if (entityParent.TypeID == typeID)
				{
					return entityParent as T;
				}
			}
			return (T)((object)null);
		}
		return (T)((object)null);
	}

	public EntityParent GetEntityByID(long id)
	{
		if (this.allEntities.ContainsKey(id))
		{
			return this.allEntities[id];
		}
		return null;
	}

	public EntityParent GetOneSkillTarget(EntityParent caster, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, int camp, bool isSameCamp, bool isContainsSelf, List<int> comparers)
	{
		EntityParent result = null;
		this.GetOneTargetFromEntityCollection<EntityParent, EntityParent>(this.GetEntities<EntitySelf>(), caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, camp, isSameCamp, isContainsSelf, comparers, ref result);
		this.GetOneTargetFromEntityCollection<EntityParent, EntityParent>(this.GetEntities<EntityPlayer>(), caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, camp, isSameCamp, isContainsSelf, comparers, ref result);
		this.GetOneTargetFromEntityCollection<EntityParent, EntityParent>(this.GetEntities<EntityPet>(), caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, camp, isSameCamp, isContainsSelf, comparers, ref result);
		this.GetOneTargetFromEntityCollection<EntityParent, EntityParent>(this.GetEntities<EntityMonster>(), caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, camp, isSameCamp, isContainsSelf, comparers, ref result);
		return result;
	}

	public EntityParent GetOneSkillTarget<T>(XDict<long, T> entityCollection, EntityParent caster, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, int camp, bool isSameCamp, bool isContainsSelf, List<int> comparers) where T : EntityParent
	{
		EntityParent result = null;
		this.GetOneTargetFromEntityCollection<T, EntityParent>(entityCollection, caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, camp, isSameCamp, isContainsSelf, comparers, ref result);
		return result;
	}

	public EntityParent GetOneSkillTarget<T>(XDict<long, T> entityCollection, EntityParent caster, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, List<int> comparers) where T : EntityParent
	{
		EntityParent result = null;
		this.GetOneTargetFromEntityCollection<T, EntityParent>(entityCollection, caster, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers, ref result);
		return result;
	}

	public EntityParent GetOneTargetFromEntityCollection<T, U>(XDict<long, T> entityCollection, U caster, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, int camp, bool isSameCamp, bool isContainsSelf, List<int> comparers, ref EntityParent result) where T : EntityParent where U : EntityParent
	{
		List<T> values = entityCollection.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (this.StateFilter<T>(values.get_Item(i), false))
			{
				if (this.CampFilter<T>(values.get_Item(i), caster, camp, isSameCamp, isContainsSelf))
				{
					if (this.AltitudeFilter<T>(values.get_Item(i), altitude))
					{
						if (caster != null && !(caster.Actor == null) && !(caster.Actor.FixTransform == null))
						{
							if (values.get_Item(i) != null)
							{
								T t = values.get_Item(i);
								if (!(t.Actor == null))
								{
									T t2 = values.get_Item(i);
									if (!(t2.Actor.FixTransform == null))
									{
										Vector3 position = caster.Actor.FixTransform.get_position();
										T t3 = values.get_Item(i);
										float arg_177_1 = t3.Actor.FixTransform.get_position().x;
										float arg_177_2 = position.y;
										T t4 = values.get_Item(i);
										Vector3 vector = new Vector3(arg_177_1, arg_177_2, t4.Actor.FixTransform.get_position().z);
										Vector3 vector2 = (forwardFixAngle != 0) ? (Quaternion.Euler(caster.Actor.FixTransform.get_rotation().get_eulerAngles().x, caster.Actor.FixTransform.get_rotation().get_eulerAngles().y + (float)forwardFixAngle, caster.Actor.FixTransform.get_rotation().get_eulerAngles().z) * Vector3.get_forward()) : caster.Actor.FixTransform.get_forward();
										Vector3 casterForward = new Vector3(vector2.x, 0f, vector2.z);
										T t5 = values.get_Item(i);
										float hitRadius = XUtility.GetHitRadius(t5.Actor.FixTransform);
										if (this.RangeAndAngleFilter(outerDistance, innerDistance, angle, position, vector, hitRadius, casterForward))
										{
											if (!this.JudgeDefaultState(result, values.get_Item(i), out result))
											{
												Vector3 vector3 = new Vector3(result.Actor.FixTransform.get_position().x, position.y, result.Actor.FixTransform.get_position().z);
												for (int j = 0; j < comparers.get_Count(); j++)
												{
													bool flag = false;
													switch (comparers.get_Item(j))
													{
													case 1:
														flag = this.JudgeDistance(result, values.get_Item(i), position, vector3, vector, out result);
														break;
													case 2:
														flag = this.JudgeAngle(result, values.get_Item(i), casterForward, vector3 - position, vector - position, out result);
														break;
													case 3:
														flag = this.JudgeTauntState(result, values.get_Item(i), out result);
														break;
													case 4:
														flag = this.JudgeLeastHp(result, values.get_Item(i), out result);
														break;
													case 5:
														flag = this.JudgeLeastHpPercentage(result, values.get_Item(i), out result);
														break;
													case 6:
														flag = this.JudgeBossTag(result, values.get_Item(i), out result);
														break;
													case 7:
														flag = this.JudgeLockOn(result, values.get_Item(i), out result);
														break;
													}
													if (flag)
													{
														break;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	public EntityParent GetOneTargetFromEntityCollection<T, U>(XDict<long, T> entityCollection, U caster, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, List<int> comparers, ref EntityParent result) where T : EntityParent where U : EntityParent
	{
		List<T> values = entityCollection.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (this.StateFilter<T>(values.get_Item(i), false))
			{
				if (this.AltitudeFilter<T>(values.get_Item(i), altitude))
				{
					if (caster != null && !(caster.Actor == null) && !(caster.Actor.FixTransform == null))
					{
						if (values.get_Item(i) != null)
						{
							T t = values.get_Item(i);
							if (!(t.Actor == null))
							{
								T t2 = values.get_Item(i);
								if (!(t2.Actor.FixTransform == null))
								{
									Vector3 position = caster.Actor.FixTransform.get_position();
									T t3 = values.get_Item(i);
									float arg_154_1 = t3.Actor.FixTransform.get_position().x;
									float arg_154_2 = position.y;
									T t4 = values.get_Item(i);
									Vector3 vector = new Vector3(arg_154_1, arg_154_2, t4.Actor.FixTransform.get_position().z);
									Vector3 vector2 = (forwardFixAngle != 0) ? (Quaternion.Euler(caster.Actor.FixTransform.get_rotation().get_eulerAngles().x, caster.Actor.FixTransform.get_rotation().get_eulerAngles().y + (float)forwardFixAngle, caster.Actor.FixTransform.get_rotation().get_eulerAngles().z) * Vector3.get_forward()) : caster.Actor.FixTransform.get_forward();
									Vector3 casterForward = new Vector3(vector2.x, 0f, vector2.z);
									T t5 = values.get_Item(i);
									float hitRadius = XUtility.GetHitRadius(t5.Actor.FixTransform);
									if (this.RangeAndAngleFilter(outerDistance, innerDistance, angle, position, vector, hitRadius, casterForward))
									{
										if (!this.JudgeDefaultState(result, values.get_Item(i), out result))
										{
											Vector3 vector3 = new Vector3(result.Actor.FixTransform.get_position().x, position.y, result.Actor.FixTransform.get_position().z);
											for (int j = 0; j < comparers.get_Count(); j++)
											{
												bool flag = false;
												switch (comparers.get_Item(j))
												{
												case 1:
													flag = this.JudgeDistance(result, values.get_Item(i), position, vector3, vector, out result);
													break;
												case 2:
													flag = this.JudgeAngle(result, values.get_Item(i), casterForward, vector3 - position, vector - position, out result);
													break;
												case 3:
													flag = this.JudgeTauntState(result, values.get_Item(i), out result);
													break;
												case 4:
													flag = this.JudgeLeastHp(result, values.get_Item(i), out result);
													break;
												case 5:
													flag = this.JudgeLeastHpPercentage(result, values.get_Item(i), out result);
													break;
												case 6:
													flag = this.JudgeBossTag(result, values.get_Item(i), out result);
													break;
												case 7:
													flag = this.JudgeLockOn(result, values.get_Item(i), out result);
													break;
												}
												if (flag)
												{
													break;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	protected bool JudgeDefaultState(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		bool result2 = false;
		result = currentEntity;
		if (currentEntity == null)
		{
			result2 = true;
			result = candidateEntity;
		}
		return result2;
	}

	protected bool JudgeTauntState(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		result = ((currentEntity.IsTaunt || !candidateEntity.IsTaunt) ? currentEntity : candidateEntity);
		return currentEntity.IsTaunt != candidateEntity.IsTaunt;
	}

	protected bool JudgeDistance(EntityParent currentEntity, EntityParent candidateEntity, Vector3 casterPosition, Vector3 currentEntityPosition, Vector3 candidateEntityPosition, out EntityParent result)
	{
		float num = Vector3.Distance(casterPosition, currentEntityPosition);
		float num2 = Vector3.Distance(casterPosition, candidateEntityPosition);
		result = ((num2 >= num) ? currentEntity : candidateEntity);
		return num != num2;
	}

	protected bool JudgeAngle(EntityParent currentEntity, EntityParent candidateEntity, Vector3 casterForward, Vector3 currentEntityToCaster, Vector3 candidateEntityToCaster, out EntityParent result)
	{
		float num = Vector3.Angle(casterForward, currentEntityToCaster);
		float num2 = Vector3.Angle(casterForward, candidateEntityToCaster);
		result = ((num2 >= num) ? currentEntity : candidateEntity);
		return num != num2;
	}

	protected bool JudgeLeastHp(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		result = ((candidateEntity.Hp >= currentEntity.Hp) ? currentEntity : candidateEntity);
		return candidateEntity.Hp != currentEntity.Hp;
	}

	protected bool JudgeLeastHpPercentage(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		float num = (float)currentEntity.Hp / (float)currentEntity.RealHpLmt;
		float num2 = (float)candidateEntity.Hp / (float)candidateEntity.RealHpLmt;
		result = ((num2 >= num) ? currentEntity : candidateEntity);
		return num != num2;
	}

	protected bool JudgeBossTag(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		result = ((currentEntity.IsLogicBoss || !candidateEntity.IsLogicBoss) ? currentEntity : candidateEntity);
		return currentEntity.IsLogicBoss != candidateEntity.IsLogicBoss;
	}

	protected bool JudgeLockOn(EntityParent currentEntity, EntityParent candidateEntity, out EntityParent result)
	{
		result = ((this.LockOnTarget.ID != candidateEntity.ID) ? currentEntity : candidateEntity);
		return (this.LockOnTarget.ID == currentEntity.ID && this.LockOnTarget.ID != candidateEntity.ID) || (this.LockOnTarget.ID != currentEntity.ID && this.LockOnTarget.ID == candidateEntity.ID);
	}

	public bool CheckOneTargetFromEntityCollection<T, U>(T entity, U caster, float outerDistance, float innerDistance, int angle, int altitude, int camp, bool isSameCamp, bool isContainsSelf) where T : EntityParent where U : EntityParent
	{
		if (!this.StateFilter<T>(entity, false))
		{
			return false;
		}
		if (!this.CampFilter<T>(entity, caster, camp, isSameCamp, isContainsSelf))
		{
			return false;
		}
		if (!this.AltitudeFilter<T>(entity, altitude))
		{
			return false;
		}
		Vector3 position = caster.Actor.FixTransform.get_position();
		Vector3 entityPosition = new Vector3(entity.Actor.FixTransform.get_position().x, position.y, entity.Actor.FixTransform.get_position().z);
		float hitRadius = XUtility.GetHitRadius(entity.Actor.FixTransform);
		Vector3 forward = caster.Actor.FixTransform.get_forward();
		return this.RangeAndAngleFilter(outerDistance, innerDistance, angle, position, entityPosition, hitRadius, forward);
	}

	public void GetAllEffectTarget(EntityParent caster, bool isIgnoreUnconspicuous, int altitude, int camp, bool isSame, bool isContainsSelf, List<EntityParent> result)
	{
		this.GetCampTransform<EntityParent>(this.AllEntities, caster, isIgnoreUnconspicuous, altitude, camp, isSame, isContainsSelf, result);
	}

	protected void GetCampTransform<T>(XDict<long, T> entityCollection, EntityParent caster, bool isIgnoreUnconspicuous, int altitude, int camp, bool isSameCamp, bool isContainsSelf, List<EntityParent> result) where T : EntityParent
	{
		for (int i = 0; i < entityCollection.Values.get_Count(); i++)
		{
			if (this.StateFilter<T>(entityCollection.Values.get_Item(i), isIgnoreUnconspicuous) && this.CampFilter<T>(entityCollection.Values.get_Item(i), caster, camp, isSameCamp, isContainsSelf) && this.AltitudeFilter<T>(entityCollection.Values.get_Item(i), altitude))
			{
				result.Add(entityCollection.Values.get_Item(i));
			}
		}
	}

	public bool StateFilter<T>(T entity, bool isIgnoreUnconspicuous = false) where T : EntityParent
	{
		return entity != null && entity.Actor && entity.IsFighting && !entity.IsDead && (isIgnoreUnconspicuous || !entity.IsUnconspicuous);
	}

	protected bool CampFilter<T>(T entity, EntityParent caster, int camp, bool isSameCamp, bool isContainsSelf) where T : EntityParent
	{
		return (camp == -1 || entity.Camp == camp || !isSameCamp) && (camp == -1 || entity.Camp != camp || isSameCamp) && (isContainsSelf || entity.ID != caster.ID);
	}

	public bool AltitudeFilter<T>(T entity, int altitude) where T : EntityParent
	{
		return entity.Actor && entity.Actor.ModelHeight <= (float)altitude;
	}

	public bool RangeAndAngleFilter(float outerDistance, float innerDistance, int angle, Vector3 casterPosition, Vector3 entityPosition, float entityRadius, Vector3 casterForward)
	{
		Vector3 vector = entityPosition - casterPosition;
		return (outerDistance == -1f || (Vector3.Distance(casterPosition, entityPosition) <= outerDistance + entityRadius && Vector3.Distance(casterPosition, entityPosition) >= innerDistance - entityRadius)) && (angle == -1 || Vector3.Angle(casterForward, vector) <= (float)angle * 0.5f || !(vector != Vector3.get_zero()));
	}

	public bool TargetFilter<T>(T entity, EntityParent caster, int targetDataID) where T : EntityParent
	{
		if (entity == null)
		{
			return false;
		}
		if (caster == null)
		{
			return false;
		}
		Target target = DataReader<Target>.Get(targetDataID);
		if (target == null)
		{
			return false;
		}
		if (!this.TargetTypeFilter<T>(entity, caster, target.type))
		{
			return false;
		}
		if (entity.IsEntityMonsterType)
		{
			int rank = target.rank;
			if (rank != 1)
			{
				if (rank == 2)
				{
					if (entity.IsLogicBoss)
					{
						return false;
					}
				}
			}
			else if (!entity.IsLogicBoss)
			{
				return false;
			}
		}
		if ((entity.IsEntitySelfType || entity.IsEntityPlayerType) && target.profession != 0 && entity.TypeID != target.profession)
		{
			return false;
		}
		if (entity.IsEntityPetType)
		{
			if (target.element != 0 && entity.Element != target.element)
			{
				return false;
			}
			if (target.function != 0 && entity.Function != target.function)
			{
				return false;
			}
		}
		return this.CompareFilter(target.HPpercentage, (float)(entity.Hp / entity.RealHpLmt));
	}

	public bool TargetTypeFilter<T>(T entity, EntityParent caster, int type) where T : EntityParent
	{
		switch (type)
		{
		case 1:
			return entity.Camp != caster.Camp;
		case 2:
			return entity.Camp == caster.Camp && entity.ID != caster.ID;
		case 3:
			return entity.ID == caster.ID;
		case 4:
			return entity.Camp == caster.Camp;
		case 5:
			return caster.IsEntityPetType && (caster as EntityPet).OwnerID == entity.ID;
		case 6:
			return true;
		case 7:
			return caster.IsEntitySelfType && (caster as EntitySelf).OwnedIDs.Contains(entity.ID);
		case 8:
			return caster.DamageSourceID == entity.ID;
		case 9:
			return EntityWorld.Instance.EntSelf.ID == entity.ID;
		default:
			return false;
		}
	}

	public bool CompareFilter(string comparison, float attr)
	{
		if (string.IsNullOrEmpty(comparison))
		{
			return true;
		}
		bool result = true;
		if (Regex.Match(comparison, "==[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("==", string.Empty));
			if (attr != num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, "<=[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("<=", string.Empty));
			if (attr > num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, ">=[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace(">=", string.Empty));
			if (attr < num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, "<[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("<", string.Empty));
			if (attr >= num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, ">[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace(">", string.Empty));
			if (attr <= num)
			{
				return false;
			}
		}
		return result;
	}

	public bool CompareFilter(string comparison, double attr)
	{
		if (string.IsNullOrEmpty(comparison))
		{
			return true;
		}
		bool result = true;
		if (Regex.Match(comparison, "==[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("==", string.Empty));
			if (attr != (double)num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, "<=[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("<=", string.Empty));
			if (attr > (double)num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, ">=[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace(">=", string.Empty));
			if (attr < (double)num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, "<[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace("<", string.Empty));
			if (attr >= (double)num)
			{
				return false;
			}
		}
		else if (Regex.Match(comparison, ">[/-]*[0-9]+").get_Success())
		{
			float num = float.Parse(comparison.Replace(">", string.Empty));
			if (attr <= (double)num)
			{
				return false;
			}
		}
		return result;
	}

	public bool IsContainOtherLogicBoss(long id)
	{
		List<EntityParent> values = this.GetEntities<EntityMonster>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i).IsLogicBoss && values.get_Item(i).ID != id)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsContainOtherDisplayBoss(long id)
	{
		List<EntityParent> values = this.GetEntities<EntityMonster>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i).IsDisplayBoss && values.get_Item(i).ID != id)
			{
				return true;
			}
		}
		return false;
	}

	protected void CheckEntityRelive(BattleAction_Relive data, bool isClient)
	{
		if (data == null)
		{
			return;
		}
		if (data.soldierInfo == null)
		{
			return;
		}
		if (this.AllEntities.ContainsKey(data.soldierInfo.id))
		{
			return;
		}
		AOIService.Instance.CreateEntity(data.soldierInfo, isClient);
	}

	protected void GetNewBatch(BattleAction_NewBatchNty data, bool isClient)
	{
		InstanceManager.ServerBatch = data.batch;
	}

	protected void GetAllClientLoadDone(BattleAction_AllLoadDoneNty data, bool isClient)
	{
		EventDispatcher.Broadcast(InstanceManagerEvent.GetAllClientLoadDone);
	}

	public void AddPosRecord(long theID, Vector3 thePos, int theWay)
	{
		if (this.allPosRecord.get_Count() > this.maxPosRecord)
		{
			this.allPosRecord.RemoveRange(0, this.allPosRecord.get_Count() - this.maxPosRecord + 1);
		}
		this.allPosRecord.Add(new PosRecord
		{
			id = theID,
			pos = thePos,
			sceneID = MySceneManager.Instance.CurSceneID,
			way = theWay
		});
	}

	public string ShowAllPosState()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < this.allPosRecord.get_Count(); i++)
		{
			stringBuilder.Append(this.allPosRecord.get_Item(i).ToString());
		}
		stringBuilder.Append("\n");
		return stringBuilder.ToString();
	}

	public void ForceOut(string title, string text, Action callback = null)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(title, text, delegate
		{
			if (callback != null)
			{
				callback.Invoke();
			}
			TimerHeap.AddTimer(1000u, 0, delegate
			{
				this.ForceOut(title, text, callback);
			});
		}, delegate
		{
			if (callback != null)
			{
				callback.Invoke();
			}
			TimerHeap.AddTimer(1000u, 0, delegate
			{
				this.ForceOut(title, text, callback);
			});
		}, "点我死循环", "报复社会", "button_orange_1", "button_yellow_1", null, true, true);
	}
}
