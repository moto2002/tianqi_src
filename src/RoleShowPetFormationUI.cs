using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoleShowPetFormationUI : UIBase
{
	private List<PetFormationUnit> m_petFormation = new List<PetFormationUnit>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_petFormation.Clear();
		for (int i = 1; i <= 3; i++)
		{
			PetFormationUnit component = base.FindTransform("Formation" + i).GetComponent<PetFormationUnit>();
			component.SetAction(new Action<int>(this.OnClickFormation), new Action<int>(this.OnClickFormation), i - 1);
			this.m_petFormation.Add(component);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.ShowDetailInfos(false, 0);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ShowDetailInfos(false, 0);
	}

	private void OnClickFormation(int index)
	{
		this.SetCurrentSelectFormationID(index);
	}

	private void OnClickMask(GameObject sender)
	{
		this.ShowDetailInfos(false, 0);
	}

	public void RefreshUI(int indexFormation)
	{
		this.RefreshOpen();
		for (int i = 0; i < this.m_petFormation.get_Count(); i++)
		{
			this.SetPetItems(this.m_petFormation.get_Item(i), i);
		}
		this.SetCurrentSelectFormationID(indexFormation);
		this.RefreshDetailInfos(indexFormation);
	}

	private void RefreshOpen()
	{
		string chineseContent = GameDataUtils.GetChineseContent(510092, false);
		List<int> formationLvs = PetManager.Instance.GetFormationLvs();
		int num = 0;
		while (num < formationLvs.get_Count() && num < this.m_petFormation.get_Count())
		{
			bool flag = EntityWorld.Instance.EntSelf.Lv >= formationLvs.get_Item(num);
			if (flag)
			{
				this.m_petFormation.get_Item(num).ShowOpen(true);
			}
			else
			{
				this.m_petFormation.get_Item(num).ShowOpen(false);
				this.m_petFormation.get_Item(num).SetNoOpen(chineseContent.Replace("{s1}", formationLvs.get_Item(num).ToString()));
			}
			num++;
		}
	}

	private void SetPetItems(PetFormationUnit pfu, int indexFormation)
	{
		List<PetInfo> pets = RoleShowUIViewModel.Instance.GetPets(indexFormation);
		if (pets == null || pets.get_Count() == 0)
		{
			for (int i = 0; i < 3; i++)
			{
				pfu.pets.get_Item(i).SetItem(null, null, false);
			}
		}
		else
		{
			for (int j = 0; j < 3; j++)
			{
				if (j < pets.get_Count())
				{
					PetInfo petInfo = pets.get_Item(j);
					Pet dataPet = DataReader<Pet>.Get(petInfo.petId);
					pfu.pets.get_Item(j).SetItem(petInfo, dataPet, false);
				}
				else
				{
					pfu.pets.get_Item(j).SetItem(null, null, false);
				}
			}
		}
	}

	private void SetCurrentSelectFormationID(int indexFormation)
	{
		for (int i = 0; i < this.m_petFormation.get_Count(); i++)
		{
			if (indexFormation == i)
			{
				this.m_petFormation.get_Item(i).SetSelect(true);
			}
			else
			{
				this.m_petFormation.get_Item(i).SetSelect(false);
			}
		}
	}

	private void ShowDetailInfos(bool isShow, int indexFormation = 0)
	{
		List<PetInfo> pets = RoleShowUIViewModel.Instance.GetPets(indexFormation);
		if (pets == null || pets.get_Count() == 0)
		{
			isShow = false;
		}
		if (isShow)
		{
			this.RefreshDetailInfos(indexFormation);
		}
	}

	private void RefreshDetailInfos(int indexFormation)
	{
	}
}
