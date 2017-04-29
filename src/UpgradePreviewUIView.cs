using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;

public class UpgradePreviewUIView : UIBase
{
	private UpgradePreviewUnit m_preview01;

	private UpgradePreviewUnit m_preview02;

	private UpgradePreviewUnit m_preview03;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_preview01 = this.Create(base.FindTransform("Preview01"));
		this.m_preview01.Init(0, GameDataUtils.GetChineseContent(400006, false), 0.6f);
		this.m_preview02 = this.Create(base.FindTransform("Preview02"));
		this.m_preview02.Init(1, GameDataUtils.GetChineseContent(400007, false), 0.8f);
		this.m_preview03 = this.Create(base.FindTransform("Preview03"));
		this.m_preview03.Init(2, GameDataUtils.GetChineseContent(400008, false), 1f);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		ModelDisplayManager.Instance.DeleteModel();
		Pet pet = DataReader<Pet>.Get(PetBasicUIViewModel.PetID);
		if (pet != null && pet.modelPreview.get_Count() >= 3)
		{
			this.m_preview01.SetModel(pet.modelPreview.get_Item(0));
			this.m_preview02.SetModel(pet.modelPreview.get_Item(1));
			this.m_preview03.SetModel(pet.modelPreview.get_Item(2));
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (PetBasicUIViewModel.Instance != null)
		{
			PetBasicUIViewModel.Instance.ShowPetModel();
		}
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	private UpgradePreviewUnit Create(Transform parent)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("UpgradePreviewUnit");
		UGUITools.SetParent(parent.get_gameObject(), instantiate2Prefab, true);
		return instantiate2Prefab.GetComponent<UpgradePreviewUnit>();
	}
}
