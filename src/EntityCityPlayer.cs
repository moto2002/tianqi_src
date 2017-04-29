using GameData;
using Package;
using System;
using System.Text;
using UnityEngine;
using XEngineActor;

public class EntityCityPlayer : EntityParent, ISimpleBaseAttr
{
	private SimpleBaseAttrs simpleBaseAttrs = new SimpleBaseAttrs();

	protected EquipCustomization equipCustomizationer;

	protected ExteriorArithmeticUnit exteriorUnit;

	protected int appearFxID;

	protected int levelUpFxID;

	public override SimpleBaseAttrs SimpleBaseAttrs
	{
		get
		{
			return this.simpleBaseAttrs;
		}
	}

	public override int MoveSpeed
	{
		get
		{
			return this.SimpleBaseAttrs.MoveSpeed;
		}
		set
		{
			this.SimpleBaseAttrs.MoveSpeed = value;
		}
	}

	public override int ActSpeed
	{
		get
		{
			return this.SimpleBaseAttrs.ActSpeed;
		}
		set
		{
			this.SimpleBaseAttrs.ActSpeed = value;
		}
	}

	public override int RealMoveSpeed
	{
		get
		{
			return this.SimpleBaseAttrs.RealMoveSpeed;
		}
		set
		{
			this.SimpleBaseAttrs.RealMoveSpeed = value;
		}
	}

	public override int RealActionSpeed
	{
		get
		{
			return this.SimpleBaseAttrs.RealActionSpeed;
		}
		set
		{
			this.SimpleBaseAttrs.RealActionSpeed = value;
		}
	}

	public override int Lv
	{
		get
		{
			return this.SimpleBaseAttrs.Lv;
		}
		set
		{
			this.SimpleBaseAttrs.Lv = value;
		}
	}

	public override long Fighting
	{
		get
		{
			return this.SimpleBaseAttrs.Fighting;
		}
		set
		{
			this.SimpleBaseAttrs.Fighting = value;
		}
	}

	public override int VipLv
	{
		get
		{
			return this.SimpleBaseAttrs.VipLv;
		}
		set
		{
			this.SimpleBaseAttrs.VipLv = value;
		}
	}

	public override int TypeID
	{
		get
		{
			return base.TypeID;
		}
		set
		{
			base.TypeID = value;
			this.ExteriorUnit.SetType(this.TypeID);
		}
	}

	public override int ModelID
	{
		get
		{
			return base.ModelID;
		}
		set
		{
			base.ModelID = value;
			this.ExteriorUnit.ServerModelID = value;
		}
	}

	public override MapObjDecorations Decorations
	{
		get
		{
			return base.Decorations;
		}
		set
		{
			if (value == null)
			{
				this.decorations.career = 0;
				this.decorations.equipIds.Clear();
				this.decorations.petUUId = 0L;
				this.decorations.petId = 0;
				this.decorations.wingId = 0;
				this.decorations.wingHidden = true;
				this.decorations.wingLv = 0;
				this.decorations.gogokNum = 0;
				this.decorations.fashions.Clear();
				return;
			}
			bool flag = this.decorations.petUUId == value.petUUId && this.decorations.petId == value.petId && this.decorations.petStar == value.petStar;
			long petUUId = this.decorations.petUUId;
			this.decorations.career = value.career;
			this.decorations.modelId = value.modelId;
			this.decorations.equipIds.Clear();
			if (value.equipIds != null)
			{
				this.decorations.equipIds.AddRange(value.equipIds);
			}
			this.decorations.petUUId = value.petUUId;
			this.decorations.petId = value.petId;
			this.decorations.petStar = value.petStar;
			this.decorations.wingId = value.wingId;
			this.decorations.wingLv = value.wingLv;
			this.decorations.gogokNum = value.gogokNum;
			this.decorations.wingHidden = value.wingHidden;
			this.decorations.fashions.Clear();
			if (value.fashions != null)
			{
				this.decorations.fashions.AddRange(value.fashions);
			}
			this.ExteriorUnit.WrapSetData(delegate
			{
				this.TypeID = this.decorations.career;
				this.ModelID = this.decorations.modelId;
				this.ExteriorUnit.EquipIDs = this.decorations.equipIds;
				this.ExteriorUnit.WingID = WingManager.GetWingModel(this.decorations.wingId, this.decorations.wingLv);
				this.ExteriorUnit.IsHideWing = this.decorations.wingHidden;
				this.ExteriorUnit.FashionIDs = this.decorations.fashions;
				this.ExteriorUnit.Gogok = this.decorations.gogokNum;
			});
			if (!flag && base.Actor)
			{
				if (petUUId != 0L)
				{
					this.RemoveFollowPet(petUUId);
				}
				if (this.decorations.petUUId != 0L && this.decorations.petId != 0)
				{
					this.AddFollowPet(this.decorations.petUUId, this.decorations.petId, this.decorations.petStar);
				}
			}
		}
	}

	public override bool IsEntityCityPlayerType
	{
		get
		{
			return true;
		}
	}

	public override int FixModelID
	{
		get
		{
			return this.ExteriorUnit.FinalModelID;
		}
	}

	public EquipCustomization EquipCustomizationer
	{
		get
		{
			if (this.equipCustomizationer == null)
			{
				this.equipCustomizationer = new EquipCustomization();
			}
			return this.equipCustomizationer;
		}
	}

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(new Action<Action>(this.UpdateActor), new Action(this.UpdateWeapon), new Action(this.UpdateClothes), new Action(this.UpdateWing));
			}
			return this.exteriorUnit;
		}
	}

	public EntityCityPlayer()
	{
		this.simpleBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
	}

	public override void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		base.IsClientCreate = isClientCreate;
		base.ObjType = (int)info.objType;
		base.ID = info.id;
		this.TypeID = info.typeId;
		this.ModelID = info.modelId;
		this.Name = info.name;
		this.TypeRank = info.rankValue;
		this.TitleID = info.titleId;
		this.GuildTitle = HeadInfoManager.GetGuildTitle(info.guildInfo);
		base.Floor = info.mapLayer;
		base.Pos = PosDirUtility.ToTerrainPoint(info.pos, base.CurFloorStandardHeight);
		base.Dir = new Vector3(info.vector.x, 0f, info.vector.y);
		this.SetMapObjSimpleInfo(info.otherInfo);
		this.Decorations = info.decorations;
	}

	public override void CreateActor()
	{
		this.ExteriorUnit.IsAutoUpdateExterior = true;
	}

	public override void OnLeaveField()
	{
		if (base.Actor)
		{
			FXManager.Instance.PlayFX(922, null, base.Actor.FixTransform.get_position(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
		}
		else
		{
			EntityWorld.Instance.CancelGetCityPlayerActorAsync(base.AsyncLoadID);
		}
		FXManager.Instance.DeleteFX(this.appearFxID);
		FXManager.Instance.DeleteFX(this.levelUpFxID);
		this.EquipCustomizationer.RemoveWeaponFX();
		base.OnLeaveField();
	}

	protected override void LeaveField()
	{
		this.ResetEntity();
		EntityWorld.Instance.ReuseCityPlayer(this);
	}

	protected override void ResetEntity()
	{
		this.simpleBaseAttrs.ResetAllAttrs();
		this.equipCustomizationer = null;
		this.appearFxID = 0;
		this.levelUpFxID = 0;
	}

	protected override void AddNetworkListener()
	{
		base.AddNetworkListener();
		base.AddNetworkMoveRotateTeleportGoUpAndDownListener();
		this.AddNetworkDecorationChangedListener();
	}

	protected override void RemoveNetworkListener()
	{
		base.RemoveNetworkListener();
		base.RemoveNetworkMoveRotateTeleportGoUpAndDownListener();
		this.RemoveNetworkDecorationChangedListener();
	}

	public override void InitActorState()
	{
		base.InitActorState();
		this.ExteriorUnit.IsAutoUpdateExterior = true;
		this.AddFollowPet(this.Decorations.petUUId, this.Decorations.petId, this.Decorations.petStar);
	}

	public override void OnAttrChanged(GameData.AttrType attrType, long oldValue, long newValue)
	{
		if (attrType == GameData.AttrType.Lv)
		{
			this.OnChangeLv(oldValue, newValue);
		}
	}

	protected override void OnChangeLv(long oldValue, long newValue)
	{
		if (oldValue != newValue)
		{
			HeadInfoManager.Instance.SetName(21, base.ID, (int)newValue, this.Name);
			if (base.Actor && ActorVisibleManager.Instance.IsShow(base.ID))
			{
				this.levelUpFxID = FXManager.Instance.PlayFXIfNOExist(this.levelUpFxID, 903, base.Actor.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f);
			}
		}
	}

	protected override void ChangeDecoration(short state, MapObjDecorationChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (base.ID != down.objId)
		{
			return;
		}
		this.Decorations = down.decorations;
	}

	protected void UpdateActor(Action logicCallback)
	{
		this.DestoryOldCityPlayerActor();
		base.AsyncLoadID = EntityWorld.Instance.GetCityPlayerActorAsync(this.FixModelID, delegate(ActorCityPlayer newActor)
		{
			this.SetCityPlayerActor(newActor, logicCallback);
		});
	}

	protected void DestoryOldCityPlayerActor()
	{
		this.ExteriorUnit.IsAutoUpdateExterior = false;
		if (base.Actor)
		{
			ActorParent actor = base.Actor;
			base.Pos = actor.FixTransform.get_position();
			base.Dir = actor.FixTransform.get_forward();
			actor.DestroyScript();
		}
		else
		{
			EntityWorld.Instance.CancelGetCityPlayerActorAsync(base.AsyncLoadID);
		}
	}

	protected void SetCityPlayerActor(ActorCityPlayer actorCityPlayer, Action logicCallback)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		base.Actor = actorCityPlayer;
		actorCityPlayer.theEntity = this;
		base.Actor.FixGameObject.SetActive(true);
		base.Actor.FixGameObject.set_name(base.ID.ToString());
		base.Actor.InitActionPriorityTable();
		base.Actor.CanAnimatorApplyMotion = false;
		ShadowController.ShowShadow(base.ID, base.Actor, false, this.FixModelID);
		ShaderEffectUtils.InitShaderRenderers(base.Actor.FixTransform, this.shaderRenderers, ref this.shadowRenderer, ref this.shadowSlicePlane);
		ShaderEffectUtils.InitTransparencys(this.shaderRenderers, this.alphaControls);
		this.InitBillboard((float)avatarModel.height_HP);
		LayerSystem.SetGameObjectLayer(base.Actor.FixGameObject, "CityPlayer", 2);
		ActorVisibleManager.Instance.Add(base.ID, base.Actor.FixTransform, 21, 0L);
		base.Actor.CastAction("idle", true, 1f, 0, 0, string.Empty);
		if (logicCallback != null)
		{
			logicCallback.Invoke();
		}
		this.InitActorState();
	}

	protected void UpdateWeapon()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipGogokNum = this.ExteriorUnit.FinalWeaponGogok;
		this.EquipCustomizationer.EquipOn(this.ExteriorUnit.FinalWeaponID);
	}

	protected void UpdateClothes()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipOn(this.ExteriorUnit.FinalClothesID);
	}

	protected void UpdateWing()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipWingOn(this.ExteriorUnit.FinalWingID);
	}

	public void CheckWeaponSlot()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.CheckWeaponSlot("weapon_slot");
	}

	public void ShowWeapon(bool isShow)
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.ShowWeapon(isShow);
	}

	public override void EquipSuccess()
	{
		ShaderEffectUtils.InitShaderRenderers(base.Actor.FixTransform, this.shaderRenderers, ref this.shadowRenderer, ref this.shadowSlicePlane);
		ShaderEffectUtils.InitTransparencys(this.shaderRenderers, this.alphaControls);
	}

	public override void EquipWingSuccess(Animator wing_tor)
	{
		base.EquipWingSuccess(wing_tor);
		this.EquipSuccess();
	}

	public override void ShowEquipFX(bool isShow)
	{
		this.EquipCustomizationer.ShowEquipFX(isShow);
	}

	protected void AddFollowPet(long petID, int typeID, int rank)
	{
		if (petID == 0L || typeID == 0)
		{
			return;
		}
		EntityWorld.Instance.CreateEntityCityPet(petID, typeID, rank, this);
	}

	protected void RemoveFollowPet(long petID)
	{
		EntityWorld.Instance.RemoveEntityCityPet(petID);
	}

	protected void InitBillboard(float height)
	{
		BillboardManager.Instance.AddBillboardsInfo(21, base.Actor, height, base.ID, false, true, !this.IsDead);
		HeadInfoManager.Instance.SetName(21, base.ID, this.Lv, this.Name);
		HeadInfoManager.Instance.SetTitle(base.ID, this.TitleID);
		HeadInfoManager.Instance.SetGuildTitle(base.ID, this.GuildTitle);
	}

	protected override void GetInfoString(StringBuilder result)
	{
		base.GetInfoString(result);
		this.ExteriorUnit.GetInfoString(result);
	}
}
