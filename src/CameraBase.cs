using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineCommand;

[RequireComponent(typeof(ShakeCamera))]
public abstract class CameraBase : MonoBehaviour
{
	protected Transform role;

	public float defaultRoleHeight;

	public float defaultCameraHeight;

	public float defaultRoleCameraDistance;

	protected Vector3 rolePositionLast;

	protected Transform newBoss;

	private bool IsAddListeners;

	protected bool isPetUltraSKill;

	protected Quaternion PetUltraSKillEndRotationFrom;

	protected Quaternion PetUltraSKillEndRotationTo;

	protected Vector3 petUltraSkillEndPositionFrom;

	protected Vector3 petUltraSkillEndPositionTo;

	protected Vector3 petUltraSkillEndRolePositionFrom;

	protected Vector3 petUltraSkillEndCameraForwardFrom;

	protected float petUltraSkillEndPassedTime;

	protected float closeUpTotalTime;

	protected float[] partTimeTotal = new float[3];

	protected float[] partTimeElapsed = new float[3];

	private List<long> monsterIds = new List<long>();

	protected abstract void Init();

	protected abstract void InitCameraArgs();

	protected abstract void InitCamera();

	protected abstract void Refresh();

	protected virtual void InitListeners()
	{
		this.IsAddListeners = true;
		EventDispatcher.AddListener(EventNames.TraSelfChanged, new Callback(this.OnTraSelfChanged));
		EventDispatcher.AddListener<Transform>(CameraEvent.BossBornEnd, new Callback<Transform>(this.OnBossBornEnd));
		EventDispatcher.AddListener<EntityParent>(CameraEvent.PetUltraSKillEnd, new Callback<EntityParent>(this.OnPetUltraSKillEnd));
	}

	public virtual void RemoveListeners()
	{
		if (!this.IsAddListeners)
		{
			return;
		}
		this.IsAddListeners = false;
		EventDispatcher.RemoveListener(EventNames.TraSelfChanged, new Callback(this.OnTraSelfChanged));
		EventDispatcher.RemoveListener<Transform>(CameraEvent.BossBornEnd, new Callback<Transform>(this.OnBossBornEnd));
		EventDispatcher.RemoveListener<EntityParent>(CameraEvent.PetUltraSKillEnd, new Callback<EntityParent>(this.OnPetUltraSKillEnd));
	}

	protected virtual void OnTraSelfChanged()
	{
		this.role = EntityWorld.Instance.TraSelf;
	}

	protected virtual void OnBossBornEnd(Transform newBoss)
	{
		this.newBoss = newBoss;
		CameraGlobal.cameraType = CameraType.Follow;
	}

	protected void SetRolePositionLast()
	{
		this.rolePositionLast = this.role.get_position();
	}

	protected bool IsRoleStand()
	{
		return this.role.get_position().AssignYZero() == this.rolePositionLast.AssignYZero();
	}

	protected Transform GetPlayerRole()
	{
		return EntityWorld.Instance.TraSelf;
	}

	protected void OnPetUltraSKillEnd(EntityParent pet)
	{
		Debug.LogError("OnPetUltraSKillEnd");
		this.isPetUltraSKill = true;
		this.petUltraSkillEndPassedTime = 0f;
		this.closeUpTotalTime = 0f;
		Pet pet2 = DataReader<Pet>.Get(pet.TypeID);
		List<float> camManualSkill = pet2.camManualSkill;
		float num = camManualSkill.get_Item(0);
		float num2 = camManualSkill.get_Item(1);
		float num3 = camManualSkill.get_Item(2);
		for (int i = 0; i < this.partTimeTotal.Length; i++)
		{
			this.partTimeTotal[i] = camManualSkill.get_Item(i + 3);
			this.partTimeElapsed[i] = 0f;
		}
		this.PetUltraSKillEndRotationFrom = Quaternion.LookRotation(base.get_transform().get_forward());
		this.petUltraSkillEndPositionFrom = base.get_transform().get_position();
		this.petUltraSkillEndRolePositionFrom = this.role.get_position();
		Vector3 vector = -base.get_transform().get_forward().AssignYZero();
		Vector3 vector2 = vector * num3;
		Vector3 vector3 = new Vector3(0f, num2, 0f);
		Debug.LogError("OnPetUltraSKillEnd=" + pet.Actor.FixTransform.get_position());
		this.petUltraSkillEndPositionTo = pet.Actor.FixTransform.get_position() + vector2 + vector3;
		Vector3 vector4 = pet.Actor.FixTransform.get_position() + new Vector3(0f, num, 0f);
		this.PetUltraSKillEndRotationTo = Quaternion.LookRotation(vector4 - this.petUltraSkillEndPositionTo);
		CommandCenter.ExecuteCommand(pet.Actor.FixTransform, new ChangeAllFXLayerCmd());
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 7);
	}

	protected void PetUltraSKill()
	{
		this.closeUpTotalTime += Time.get_deltaTime();
		float num;
		if (this.closeUpTotalTime <= this.partTimeTotal[0])
		{
			this.partTimeElapsed[0] += Time.get_deltaTime();
			num = this.partTimeElapsed[0] / this.partTimeTotal[0];
		}
		else if (this.closeUpTotalTime <= this.partTimeTotal[0] + this.partTimeTotal[1])
		{
			num = 1f;
		}
		else if (this.closeUpTotalTime <= this.partTimeTotal[0] + this.partTimeTotal[1] + this.partTimeTotal[2])
		{
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
			this.partTimeElapsed[2] += Time.get_deltaTime();
			num = this.partTimeElapsed[2] / this.partTimeTotal[2];
			num = Mathf.Max(0f, 1f - num);
		}
		else
		{
			Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
			this.isPetUltraSKill = false;
			num = 0f;
		}
		Vector3 position = Vector3.Lerp(this.petUltraSkillEndPositionFrom, this.petUltraSkillEndPositionTo, num);
		Quaternion rotation = Quaternion.Lerp(this.PetUltraSKillEndRotationFrom, this.PetUltraSKillEndRotationTo, num);
		base.get_transform().set_position(position);
		base.get_transform().set_rotation(rotation);
	}

	private void DealOcclusion(EntityParent pet)
	{
		Transform fixTransform = pet.Actor.FixTransform;
		Vector3 normalized = (new Vector3(base.get_transform().get_position().x, 0f, base.get_transform().get_position().z) - new Vector3(fixTransform.get_position().x, 0f, fixTransform.get_position().z)).get_normalized();
		Vector3 vector = fixTransform.get_position() + normalized * 0.01f + new Vector3(0f, 0.5f, 0f);
		Vector3 normalized2 = (base.get_transform().get_position() - vector).get_normalized();
		float num = Vector3.Distance(vector, base.get_transform().get_position());
		RaycastHit[] array = Physics.RaycastAll(vector, normalized2, num);
		for (int i = 0; i < array.Length; i++)
		{
			ActorCollider component = array[i].get_collider().get_transform().GetComponent<ActorCollider>();
			Debug.LogError("ac=" + component);
			if (component != null && component.Actor != null && component.Actor.GetEntity() != null)
			{
				Debug.LogError("ac.Actor=" + component.Actor);
				Debug.LogError("ac.Actor.GetEntity()=" + component.Actor.GetEntity());
				long iD = component.Actor.GetEntity().ID;
				this.monsterIds.Add(iD);
				ShaderEffectUtils.SetIsNearCamera(array[i].get_collider().get_transform(), true);
			}
		}
	}
}
