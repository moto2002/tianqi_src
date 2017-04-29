using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;

public class PetTaskResultUIView : UIBase
{
	public static PetTaskResultUIView Instance;

	private PetTaskResultModel m_preview01;

	private PetTaskResultModel m_preview02;

	private Transform SpineRoot;

	private int spineId1;

	private int spineId2;

	private bool IsModel01;

	private bool IsModel02;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		PetTaskResultUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_preview01 = this.Create(base.FindTransform("Preview01"));
		this.m_preview01.Init(0);
		this.m_preview02 = this.Create(base.FindTransform("Preview02"));
		this.m_preview02.Init(1);
		this.SpineRoot = base.FindTransform("SpineRoot");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		ModelDisplayManager.Instance.DeleteModel();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.RemoveSpine();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			PetTaskResultUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	public void ShowAsSuccess(int petId, int monster_modelId, Action action)
	{
		this.ResetSelf();
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet == null)
		{
			return;
		}
		if (pet.model.get_Count() == 0)
		{
			return;
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById(petId);
		if (petInfoById == null)
		{
			return;
		}
		int pet_modelId = PetManagerBase.GetPlayerPetModel(pet, petInfoById.star);
		this.m_preview01.SetModelWithAction(pet_modelId, "idle", null);
		this.m_preview02.SetModelWithAction(monster_modelId, "idle", null);
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			this.m_preview01.SetModelWithSkill(pet_modelId, PetManager.Instance.GetPetSkill(petId, 1), delegate
			{
				this.IsModel01 = true;
				this.ModelFinish(action);
			});
			TimerHeap.AddTimer(1000u, 0, delegate
			{
				this.m_preview02.SetModelWithAction(monster_modelId, "die", delegate
				{
					this.IsModel02 = true;
					this.m_preview02.get_gameObject().SetActive(false);
					if (this.IsModel01 && this.IsModel02)
					{
						this.ModelFinish(action);
					}
				});
			});
		});
	}

	public void ShowSuccessSpine()
	{
		this.RemoveSpine();
		this.spineId1 = FXSpineManager.Instance.PlaySpine(1801, this.SpineRoot, "PetTaskResultUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.spineId2 = FXSpineManager.Instance.PlaySpine(1802, this.SpineRoot, "PetTaskResultUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void ShowAsFail(int petId, int monster_modelId, Action action)
	{
		this.ResetSelf();
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet == null)
		{
			return;
		}
		if (pet.model.get_Count() == 0)
		{
			return;
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById(petId);
		if (petInfoById == null)
		{
			return;
		}
		int pet_modelId = PetManagerBase.GetPlayerPetModel(pet, petInfoById.star);
		this.m_preview01.SetModelWithAction(pet_modelId, "idle", null);
		this.m_preview02.SetModelWithAction(monster_modelId, "idle", null);
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			this.m_preview01.SetModelWithSkill(pet_modelId, PetManager.Instance.GetPetSkill(petId, 1), delegate
			{
				this.IsModel01 = true;
				this.ModelFinish(action);
			});
		});
	}

	public void ShowFailSpine()
	{
		this.RemoveSpine();
		this.spineId1 = FXSpineManager.Instance.PlaySpine(1851, this.SpineRoot, "PetTaskResultUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.spineId2 = FXSpineManager.Instance.PlaySpine(1852, this.SpineRoot, "PetTaskResultUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void ResetSelf()
	{
		this.m_preview01.get_gameObject().SetActive(true);
		this.m_preview02.get_gameObject().SetActive(true);
		this.IsModel01 = false;
		this.IsModel02 = false;
	}

	private void ModelFinish(Action action)
	{
		this.m_preview01.get_gameObject().SetActive(false);
		this.m_preview02.get_gameObject().SetActive(false);
		if (action != null)
		{
			action.Invoke();
			action = null;
		}
	}

	private PetTaskResultModel Create(Transform parent)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("PetTaskResultModel");
		UGUITools.SetParent(parent.get_gameObject(), instantiate2Prefab, true);
		return instantiate2Prefab.GetComponent<PetTaskResultModel>();
	}

	private void RemoveSpine()
	{
		FXSpineManager.Instance.DeleteSpine(this.spineId1, true);
		FXSpineManager.Instance.DeleteSpine(this.spineId2, true);
	}
}
