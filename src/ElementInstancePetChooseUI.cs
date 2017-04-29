using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstancePetChooseUI : UIBase
{
	private GridLayoutGroup GridChoosePet;

	private ButtonCustom BtnConfirm;

	private Transform NoPets;

	private List<PetInfo> mPetInfos = new List<PetInfo>();

	private List<GameObject> listPetsGameObject = new List<GameObject>();

	private PetInfo petSelectedInfo;

	private GameObject petSelectedGameObj;

	public string currentBlockID = string.Empty;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.GridChoosePet = base.FindTransform("GridChoosePet").GetComponent<GridLayoutGroup>();
		this.BtnConfirm = base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>();
		this.NoPets = base.FindTransform("NoPets");
		this.BtnConfirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnConfirm);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
		base.GetComponent<RectTransform>().SetAsLastSibling();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetSelectPetToMiningRes, new Callback(this.OnGetSelectPetToMiningRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetSelectPetToMiningRes, new Callback(this.OnGetSelectPetToMiningRes));
	}

	public void RefreshUI()
	{
		this.ResetPetInfos();
		this.ResetChoosePetUI();
	}

	private void ResetPetInfos()
	{
		this.mPetInfos.Clear();
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = PetManager.Instance.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				this.mPetInfos.Add(current);
			}
		}
		for (int i = 0; i < this.mPetInfos.get_Count(); i++)
		{
			int num = i;
			for (int j = i + 1; j < this.mPetInfos.get_Count(); j++)
			{
				Pet pet = DataReader<Pet>.Get(this.mPetInfos.get_Item(num).petId);
				Pet pet2 = DataReader<Pet>.Get(this.mPetInfos.get_Item(j).petId);
				if (pet.summonEnergy < pet2.summonEnergy)
				{
					num = j;
				}
			}
			if (num != i)
			{
				XUtility.ListExchange<PetInfo>(this.mPetInfos, num, i);
			}
		}
	}

	private void ResetChoosePetUI()
	{
		if (this.mPetInfos == null)
		{
			return;
		}
		this.petSelectedInfo = null;
		this.petSelectedGameObj = null;
		for (int i = 0; i < this.listPetsGameObject.get_Count(); i++)
		{
			GameObject gameObject = this.listPetsGameObject.get_Item(i);
			Object.Destroy(gameObject);
		}
		this.listPetsGameObject.Clear();
		bool flag = false;
		for (int j = 0; j < this.mPetInfos.get_Count(); j++)
		{
			PetInfo petinfo = this.mPetInfos.get_Item(j);
			if (ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.Find((MinePetInfo a) => a.petId == petinfo.id) == null)
			{
				flag = true;
				Pet dataPet = DataReader<Pet>.Get(petinfo.petId);
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
				instantiate2Prefab.set_name("PetChooseItem_" + j);
				PetID component = instantiate2Prefab.GetComponent<PetID>();
				component.petInfo = petinfo;
				instantiate2Prefab.SetActive(true);
				instantiate2Prefab.get_transform().SetParent(this.GridChoosePet.get_transform());
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPetChoose);
				component.SetItem(dataPet, petinfo);
				this.listPetsGameObject.Add(instantiate2Prefab);
			}
		}
		if (flag)
		{
			this.NoPets.get_gameObject().SetActive(false);
		}
		else
		{
			this.NoPets.get_gameObject().SetActive(true);
		}
	}

	private void OnGetSelectPetToMiningRes()
	{
		this.Show(false);
	}

	private void OnClickPetChoose(GameObject sender)
	{
		PetInfo petInfo = sender.GetComponent<PetID>().petInfo;
		GameObject gameObject = sender.get_transform().FindChild("ImageSelect").get_gameObject();
		if (this.petSelectedGameObj == sender)
		{
			this.petSelectedInfo = null;
			this.petSelectedGameObj = null;
			gameObject.get_gameObject().SetActive(false);
		}
		else
		{
			this.petSelectedInfo = petInfo;
			gameObject.get_gameObject().SetActive(true);
			if (this.petSelectedGameObj != null)
			{
				GameObject gameObject2 = this.petSelectedGameObj.get_transform().FindChild("ImageSelect").get_gameObject();
				gameObject2.get_gameObject().SetActive(false);
			}
			this.petSelectedGameObj = sender;
		}
	}

	private void OnClickBtnConfirm(GameObject sender)
	{
		if (this.petSelectedInfo != null)
		{
			ElementInstanceManager.Instance.SendSelectPetToMiningReq(this.currentBlockID, this.petSelectedInfo.id);
		}
		else
		{
			this.Show(false);
		}
	}
}
