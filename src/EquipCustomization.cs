using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class EquipCustomization
{
	public enum EquipType
	{
		Equip,
		Clothes,
		Wing
	}

	private const string city_suffix = "_city";

	private const string wing_node = "wing_fx_node";

	private int mWeaponId;

	private int mClothesId;

	private int mWingId;

	private int mCacheWeaponId;

	private int mCacheClothesId;

	private int mCacheWingId;

	private List<GameObject> m_goWeapons;

	private GameObject m_goWing;

	private Actor m_actorTarget;

	private int _equipGogokNum;

	private string CurrentSlot = string.Empty;

	private int mEquipModelFxID1;

	private int mEquipModelFxID2;

	public Actor ActorTarget
	{
		get
		{
			return this.m_actorTarget;
		}
		set
		{
			this.m_actorTarget = value;
		}
	}

	public int EquipGogokNum
	{
		get
		{
			return this._equipGogokNum;
		}
		set
		{
			this._equipGogokNum = value;
		}
	}

	public EquipCustomization()
	{
		this.m_goWeapons = new List<GameObject>();
	}

	public int GetIdOfWeapon()
	{
		if (this.mCacheWeaponId > 0)
		{
			return this.mCacheWeaponId;
		}
		return this.mWeaponId;
	}

	public int GetIdOfClothes()
	{
		if (this.mCacheClothesId > 0)
		{
			return this.mCacheClothesId;
		}
		return this.mClothesId;
	}

	public int GetModelIdOfWing()
	{
		if (this.mCacheWingId > 0)
		{
			return this.mCacheWingId;
		}
		return this.mWingId;
	}

	public void RefreshEquips()
	{
		this.EquipOn(this.GetIdOfWeapon());
		this.EquipOn(this.GetIdOfClothes());
		this.EquipWingOn(this.GetModelIdOfWing());
	}

	public void EquipOn(int id)
	{
		if (id <= 0)
		{
			return;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(id);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			Debug.LogError("zZhuangBeiPeiZhiBiao no find, equipId = " + id);
			return;
		}
		if (zZhuangBeiPeiZhiBiao.model <= 0)
		{
			return;
		}
		EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
		if (equipBody == null)
		{
			return;
		}
		this.DoEquipOn(id, equipBody);
	}

	public void EquipWingOn(int wingModelId)
	{
		if (wingModelId <= 0)
		{
			this.ClearWing();
			return;
		}
		WingBody wingBody = DataReader<WingBody>.Get(wingModelId);
		if (wingBody == null)
		{
			Debug.LogError("GameData.WingBody no find, wingId = " + wingModelId);
			return;
		}
		this.DoEquipWingOn(wingModelId, wingBody);
	}

	public void RefreshEquipFX()
	{
		this.CheckShowEquipFX(this.mWeaponId);
	}

	public void CheckWeaponSlot(string slot)
	{
		slot = this.GetWeaponSlotRealName(slot);
		if (this.CurrentSlot.Equals(slot))
		{
			return;
		}
		this.SetWeapon2Slot(slot, true);
		this.CheckShowEquipFX(this.GetIdOfWeapon());
	}

	public void ShowWeapon(bool isShow)
	{
		for (int i = 0; i < this.m_goWeapons.get_Count(); i++)
		{
			GameObject gameObject = this.m_goWeapons.get_Item(i);
			if (gameObject != null)
			{
				gameObject.SetActive(isShow);
			}
		}
	}

	public void Release()
	{
		this.ClearWeapons();
		this.ClearWing();
		if (this.mWeaponId > 0)
		{
			this.mCacheWeaponId = this.mWeaponId;
		}
		if (this.mClothesId > 0)
		{
			this.mCacheClothesId = this.mClothesId;
		}
		if (this.mWingId > 0)
		{
			this.mCacheWingId = this.mWingId;
		}
		this.mWeaponId = 0;
		this.mClothesId = 0;
		this.mWingId = 0;
	}

	private void EquipSuccess(int newEquipID = 0)
	{
		if (this.ActorTarget is ActorParent)
		{
			EntityParent entity = (this.ActorTarget as ActorParent).GetEntity();
			entity.EquipSuccess();
			ActorVisibleManager.Instance.RefreshRenderers(entity.ID);
		}
		else if (this.ActorTarget is ActorModel)
		{
			(this.ActorTarget as ActorModel).EquipSuccess();
		}
	}

	private void EquipWingSuccess()
	{
		Animator component = this.m_goWing.GetComponent<Animator>();
		if (this.ActorTarget is ActorParent)
		{
			EntityParent entity = (this.ActorTarget as ActorParent).GetEntity();
			entity.EquipWingSuccess(component);
			ActorVisibleManager.Instance.RefreshRenderers(entity.ID);
		}
		else if (this.ActorTarget is ActorModel)
		{
			(this.ActorTarget as ActorModel).EquipWingSuccess(component);
		}
	}

	private void DoEquipOn(int newEquipId, EquipBody dataEB)
	{
		if (dataEB.putOnMethod == 1)
		{
			if (this.ActorTarget == null)
			{
				this.mWeaponId = 0;
				this.mCacheWeaponId = newEquipId;
				return;
			}
			this.mWeaponId = newEquipId;
			this.mCacheWeaponId = 0;
			this.DoEquipWeapons(newEquipId, dataEB, delegate(bool isSuccess)
			{
				if (isSuccess)
				{
					EventDispatcher.Broadcast("EquipCustomization.ACTSELF_CHANGE_WEAPON");
					this.EquipSuccess(newEquipId);
					this.CheckShowEquipFX(newEquipId);
				}
			});
		}
		else if (dataEB.putOnMethod == 2)
		{
			if (this.ActorTarget == null)
			{
				this.mClothesId = 0;
				this.mCacheClothesId = newEquipId;
				return;
			}
			this.mClothesId = newEquipId;
			this.mCacheClothesId = 0;
			this.DoEquipClothes(newEquipId, dataEB, delegate(bool isSuccess)
			{
				if (isSuccess)
				{
					this.EquipSuccess(0);
				}
			});
		}
		else
		{
			Debug.LogError(string.Concat(new object[]
			{
				"GameData.EquipBody id = ",
				dataEB.id,
				", putOnMethod is illegal = ",
				dataEB.putOnMethod
			}));
		}
	}

	private void DoEquipWeapons(int equipId, EquipBody dataEB, Action<bool> finished)
	{
		this.InstantiateEquipWeapon(equipId, dataEB, delegate(GameObject weapon1, GameObject weapon2)
		{
			if (this.ActorTarget == null)
			{
				this.ClearWeaponTemp(weapon1, weapon2);
				finished.Invoke(false);
				return;
			}
			if (!this.VerifyWeapon(equipId))
			{
				this.ClearWeaponTemp(weapon1, weapon2);
				finished.Invoke(false);
				return;
			}
			if (weapon1 == null)
			{
				this.ClearWeaponTemp(weapon1, weapon2);
				finished.Invoke(false);
				return;
			}
			this.ClearWeapons();
			if (weapon1 != null)
			{
				EquipCustomizationTool.SetLayer(weapon1, this.ActorTarget);
				ShadowController.SetShadowModelRender2Child(this.ActorTarget.get_transform(), weapon1.get_transform(), false);
				this.m_goWeapons.Add(weapon1);
				this.SetWeapon2Slot(this.GetWeaponSlotRealName(dataEB.slot), true);
			}
			if (weapon2 != null)
			{
				EquipCustomizationTool.SetLayer(weapon2, this.ActorTarget);
				ShadowController.SetShadowModelRender2Child(this.ActorTarget.get_transform(), weapon2.get_transform(), false);
				this.m_goWeapons.Add(weapon2);
				this.SetWeapon2Slot(this.GetWeaponSlotRealName(dataEB.slot), true);
			}
			if (finished != null)
			{
				finished.Invoke(weapon1 != null);
			}
		});
	}

	private void InstantiateEquipWeapon(int equipId, EquipBody dataEB, Action<GameObject, GameObject> loaded)
	{
		if (string.IsNullOrEmpty(dataEB.prefabPath))
		{
			if (loaded != null)
			{
				loaded.Invoke(null, null);
			}
			return;
		}
		this.LoadAsset(equipId, dataEB.prefabPath, EquipCustomization.EquipType.Equip, delegate(bool isSuccess)
		{
			if (!isSuccess)
			{
				if (loaded != null)
				{
					loaded.Invoke(null, null);
				}
				return;
			}
			if (string.IsNullOrEmpty(dataEB.prefabPath2))
			{
				if (loaded != null)
				{
					loaded.Invoke(EquipCustomizationTool.GetInstantiate(dataEB.prefabPath), null);
				}
				return;
			}
			this.LoadAsset(equipId, dataEB.prefabPath2, EquipCustomization.EquipType.Equip, delegate(bool isSuccess2)
			{
				if (!isSuccess2)
				{
					if (loaded != null)
					{
						loaded.Invoke(EquipCustomizationTool.GetInstantiate(dataEB.prefabPath), null);
					}
					return;
				}
				if (loaded != null)
				{
					loaded.Invoke(EquipCustomizationTool.GetInstantiate(dataEB.prefabPath), EquipCustomizationTool.GetInstantiate(dataEB.prefabPath2));
				}
			});
		});
	}

	private bool VerifyWeapon(int equipId)
	{
		return equipId == this.mWeaponId;
	}

	public void ChangeWeaponSlot(string slot_name, bool changeSlot, bool resetlocal)
	{
		if (changeSlot)
		{
			this.SetWeapon2Slot(this.GetWeaponSlotRealName(slot_name), resetlocal);
		}
		else
		{
			slot_name = this.GetWeaponSlot(this.mWeaponId);
			this.SetWeapon2Slot(this.GetWeaponSlotRealName(slot_name), resetlocal);
		}
	}

	private string GetWeaponSlot(int equipId)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			Debug.LogError("zZhuangBeiPeiZhiBiao no find, equipId = " + equipId);
			return string.Empty;
		}
		if (zZhuangBeiPeiZhiBiao.model <= 0)
		{
			return string.Empty;
		}
		EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
		if (equipBody == null)
		{
			return string.Empty;
		}
		return equipBody.slot;
	}

	private string GetWeaponSlotRealName(string slot)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return string.Empty;
		}
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return slot;
		}
		return slot + "_city";
	}

	private void ClearWeapons()
	{
		for (int i = 0; i < this.m_goWeapons.get_Count(); i++)
		{
			if (this.m_goWeapons.get_Item(i) != null)
			{
				this.m_goWeapons.get_Item(i).SetActive(false);
				Object.Destroy(this.m_goWeapons.get_Item(i));
			}
		}
		this.m_goWeapons.Clear();
	}

	private void ClearWeaponTemp(GameObject weapon1, GameObject weapon2)
	{
		Object.Destroy(weapon1);
		Object.Destroy(weapon2);
	}

	private void SetWeapon2Slot(string realSlot, bool resetlocal)
	{
		if (string.IsNullOrEmpty(realSlot))
		{
			return;
		}
		this.CurrentSlot = realSlot;
		if (this.m_goWeapons.get_Count() >= 1)
		{
			Transform transform = null;
			if (realSlot != string.Empty)
			{
				transform = XUtility.RecursiveFindTransform(this.ActorTarget.get_transform(), realSlot);
				if (transform == null)
				{
					return;
				}
			}
			GameObject gameObject = this.m_goWeapons.get_Item(0);
			if (gameObject != null)
			{
				Vector3 localScale = gameObject.get_transform().get_localScale();
				Vector3 position = gameObject.get_transform().get_position();
				gameObject.get_transform().set_parent(transform);
				gameObject.get_transform().set_position(position);
				if (resetlocal)
				{
					gameObject.get_transform().set_localPosition(Vector3.get_zero());
					gameObject.get_transform().set_localEulerAngles(Vector3.get_zero());
					gameObject.get_transform().set_localScale(localScale);
				}
			}
		}
		if (this.m_goWeapons.get_Count() >= 2)
		{
			Transform transform2 = null;
			if (realSlot != string.Empty)
			{
				transform2 = XUtility.RecursiveFindTransform(this.ActorTarget.get_transform(), realSlot + "2");
				if (transform2 == null)
				{
					return;
				}
			}
			GameObject gameObject2 = this.m_goWeapons.get_Item(1);
			if (gameObject2 != null)
			{
				Vector3 localScale2 = gameObject2.get_transform().get_localScale();
				Vector3 position2 = gameObject2.get_transform().get_position();
				gameObject2.get_transform().set_parent(transform2);
				gameObject2.get_transform().set_position(position2);
				if (resetlocal)
				{
					gameObject2.get_transform().set_localPosition(Vector3.get_zero());
					gameObject2.get_transform().set_localEulerAngles(Vector3.get_zero());
					gameObject2.get_transform().set_localScale(localScale2);
				}
			}
		}
	}

	private void DoEquipClothes(int equipId, EquipBody dataEB, Action<bool> finished)
	{
		EquipCustomizationTool.GetInstantiateClothes(dataEB, delegate(SkinnedMeshRenderer renderer)
		{
			if (this.ActorTarget == null)
			{
				finished.Invoke(false);
				return;
			}
			if (equipId != this.mClothesId)
			{
				finished.Invoke(false);
				return;
			}
			Transform transform = XUtility.RecursiveFindTransform(this.ActorTarget.get_transform(), dataEB.slot);
			if (transform == null)
			{
				Debug.LogError("slot is no find, GameData.EquipBody id = " + dataEB.id);
				finished.Invoke(false);
				return;
			}
			SkinnedMeshRenderer component = transform.GetComponent<SkinnedMeshRenderer>();
			if (renderer != null)
			{
				component.set_sharedMesh(renderer.get_sharedMesh());
				List<Transform> list = new List<Transform>();
				for (int i = 0; i < renderer.get_bones().Length; i++)
				{
					Transform transform2 = XUtility.RecursiveFindTransform(this.ActorTarget.get_transform(), renderer.get_bones()[i].get_name());
					list.Add(transform2);
				}
				component.set_bones(list.ToArray());
				component.set_shadowCastingMode(0);
				component.set_receiveShadows(false);
				component.set_useLightProbes(false);
				component.set_material(renderer.get_sharedMaterial());
			}
			if (finished != null)
			{
				finished.Invoke(true);
			}
		});
	}

	private void DoEquipWingOn(int wingId, WingBody dataWing)
	{
		if (this.ActorTarget == null)
		{
			this.mWingId = 0;
			this.mCacheWingId = wingId;
			return;
		}
		this.mWingId = wingId;
		this.mCacheWingId = 0;
		this.InstantiateEquipWing(wingId, dataWing, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				EventDispatcher.Broadcast("EquipCustomization.ACTSELF_CHANGE_WEAPON");
				this.EquipWingSuccess();
			}
		});
	}

	private void InstantiateEquipWing(int wingId, WingBody dataWing, Action<bool> finished)
	{
		this.GetInstantiateWing(wingId, dataWing, delegate(GameObject goInstantiate1)
		{
			if (this.ActorTarget == null)
			{
				finished.Invoke(false);
				return;
			}
			if (!this.VerifyWing(wingId))
			{
				if (goInstantiate1 != null)
				{
					Object.Destroy(goInstantiate1);
				}
				finished.Invoke(false);
				return;
			}
			if (goInstantiate1 == null)
			{
				finished.Invoke(false);
				return;
			}
			this.ClearWing();
			if (goInstantiate1 != null)
			{
				EquipCustomizationTool.SetLayer(goInstantiate1, this.ActorTarget);
				ShadowController.SetShadowModelRender2Child(this.ActorTarget.get_transform(), goInstantiate1.get_transform(), false);
				this.m_goWing = goInstantiate1;
				this.SetWing2Slot();
			}
			if (finished != null)
			{
				finished.Invoke(goInstantiate1 != null);
			}
		});
	}

	private void GetInstantiateWing(int wingId, WingBody dataWing, Action<GameObject> loaded)
	{
		if (string.IsNullOrEmpty(dataWing.prefabPath))
		{
			if (loaded != null)
			{
				loaded.Invoke(null);
			}
			return;
		}
		this.LoadAsset(wingId, dataWing.prefabPath, EquipCustomization.EquipType.Wing, delegate(bool isSuccess)
		{
			if (!isSuccess)
			{
				if (loaded != null)
				{
					loaded.Invoke(null);
				}
				return;
			}
			if (loaded != null)
			{
				loaded.Invoke(EquipCustomizationTool.GetInstantiate(dataWing.prefabPath));
			}
		});
	}

	private bool VerifyWing(int wingId)
	{
		return wingId == this.mWingId;
	}

	private void ClearWing()
	{
		Object.Destroy(this.m_goWing);
	}

	private string GetWingSlotRealName()
	{
		return "wing_fx_node";
	}

	private void SetWing2Slot()
	{
		Transform transform = XUtility.RecursiveFindTransform(this.ActorTarget.get_transform(), this.GetWingSlotRealName());
		if (transform == null)
		{
			return;
		}
		if (this.m_goWing != null)
		{
			Vector3 localScale = this.m_goWing.get_transform().get_localScale();
			Vector3 position = this.m_goWing.get_transform().get_position();
			this.m_goWing.get_transform().set_parent(transform);
			this.m_goWing.get_transform().set_position(position);
			this.m_goWing.get_transform().set_localPosition(Vector3.get_zero());
			this.m_goWing.get_transform().set_localEulerAngles(Vector3.get_zero());
			this.m_goWing.get_transform().set_localScale(localScale);
		}
	}

	private void LoadAsset(int id, string path, EquipCustomization.EquipType type, Action<bool> loaded)
	{
		path = path.Replace("\\", "/");
		AssetManager.LoadAssetWithPool(path, delegate(bool isSuccess)
		{
			if (!isSuccess)
			{
				loaded.Invoke(false);
				return;
			}
			if (this.ActorTarget == null)
			{
				loaded.Invoke(false);
				return;
			}
			if (type == EquipCustomization.EquipType.Equip)
			{
				if (!this.VerifyWeapon(id))
				{
					loaded.Invoke(false);
					return;
				}
			}
			else if (type == EquipCustomization.EquipType.Wing && !this.VerifyWing(id))
			{
				loaded.Invoke(false);
				return;
			}
			if (loaded != null)
			{
				loaded.Invoke(true);
			}
		});
	}

	public void CheckShowEquipFX(int equipCfgID)
	{
		if (EquipGlobal.CheckCanShowEquipModel(equipCfgID))
		{
			this.RemoveWeaponFX();
			if (EquipGlobal.CheckCanShowEquipFX(equipCfgID))
			{
				int equipModelFXID = EquipGlobal.GetEquipModelFXID(equipCfgID, this.EquipGogokNum);
				if (equipModelFXID > 0 && this.ActorTarget != null)
				{
					this.mEquipModelFxID1 = FXManager.Instance.PlayFX(equipModelFXID, this.ActorTarget.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, delegate(ActorFX actorFX)
					{
						if (this.ActorTarget != null)
						{
							if (actorFX != null && actorFX.get_gameObject() != null && this.ActorTarget is ActorParent)
							{
								EntityParent entity = (this.ActorTarget as ActorParent).GetEntity();
								actorFX.get_gameObject().SetActive(ActorVisibleManager.Instance.IsShow(entity.ID));
							}
						}
						else
						{
							FXManager.Instance.DeleteFX(this.mEquipModelFxID1);
						}
					}, 1f, FXClassification.Normal);
				}
				int equipModelFXID2 = EquipGlobal.GetEquipModelFXID2(equipCfgID, this.EquipGogokNum);
				if (equipModelFXID2 > 0 && this.ActorTarget != null)
				{
					this.mEquipModelFxID2 = FXManager.Instance.PlayFX(equipModelFXID2, this.ActorTarget.get_transform(), Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, delegate(ActorFX actorFX)
					{
						if (this.ActorTarget != null)
						{
							if (actorFX != null && actorFX.get_gameObject() != null && this.ActorTarget is ActorParent)
							{
								EntityParent entity = (this.ActorTarget as ActorParent).GetEntity();
								actorFX.get_gameObject().SetActive(ActorVisibleManager.Instance.IsShow(entity.ID));
							}
						}
						else
						{
							FXManager.Instance.DeleteFX(this.mEquipModelFxID2);
						}
					}, 1f, FXClassification.Normal);
				}
			}
		}
	}

	public void RemoveWeaponFX()
	{
		if (this.mEquipModelFxID1 > 0)
		{
			FXManager.Instance.DeleteFX(this.mEquipModelFxID1);
			this.mEquipModelFxID1 = 0;
		}
		if (this.mEquipModelFxID2 > 0)
		{
			FXManager.Instance.DeleteFX(this.mEquipModelFxID2);
			this.mEquipModelFxID2 = 0;
		}
	}

	public void ShowEquipFX(bool isShow)
	{
		if (this.mEquipModelFxID1 > 0)
		{
			ActorFX actorByID = FXManager.Instance.GetActorByID(this.mEquipModelFxID1);
			if (actorByID != null && actorByID.get_gameObject() != null)
			{
				actorByID.get_gameObject().SetActive(isShow);
			}
		}
		if (this.mEquipModelFxID2 > 0)
		{
			ActorFX actorByID2 = FXManager.Instance.GetActorByID(this.mEquipModelFxID2);
			if (actorByID2 != null && actorByID2.get_gameObject() != null)
			{
				actorByID2.get_gameObject().SetActive(isShow);
			}
		}
	}
}
