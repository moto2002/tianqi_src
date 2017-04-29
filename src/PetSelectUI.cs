using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetSelectUI : UIBase
{
	public enum Mode
	{
		Instance,
		PetTask
	}

	public static PetSelectUI Instance;

	private Transform NoPets;

	private GridLayoutGroup GridChoosePet;

	private ButtonCustom BtnConfirm;

	private List<GameObject> listPetItems = new List<GameObject>();

	private List<PetInfo> listPetsShow = new List<PetInfo>();

	private List<long> listPetsSelected = new List<long>();

	private int m_maxPetsCount = 3;

	private int m_formationId;

	private PetSelectUI.Mode mMode;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		PetSelectUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.NoPets = base.FindTransform("NoPets");
		this.GridChoosePet = base.FindTransform("GridChoosePet").GetComponent<GridLayoutGroup>();
		this.BtnConfirm = base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>();
		this.BtnConfirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnConfirm);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PetSelectUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("PetManager.PetFormationHaveChange", new Callback(this.PetFormationHaveChange));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("PetManager.PetFormationHaveChange", new Callback(this.PetFormationHaveChange));
	}

	private void PetFormationHaveChange()
	{
		this.Show(false);
	}

	private void OnClickBtnConfirm(GameObject sender)
	{
		this.ConfirmIsInstance();
		this.ConfirmIsPetTask();
	}

	private void OnClickPetChoose(GameObject sender)
	{
		this.ChooseIsInstance(sender);
		this.ChooseIsPetTask(sender);
	}

	private void RefreshUI()
	{
		this.ClearPetItems();
		if (this.listPetsShow.get_Count() == 0)
		{
			this.NoPets.get_gameObject().SetActive(true);
			return;
		}
		this.NoPets.get_gameObject().SetActive(false);
		for (int i = 0; i < this.listPetsShow.get_Count(); i++)
		{
			PetInfo petInfo = this.listPetsShow.get_Item(i);
			Pet pet = DataReader<Pet>.Get(petInfo.petId);
			if (pet != null)
			{
				this.listPetItems.Add(this.GetItem(petInfo, pet, this.IsSelected(petInfo.id)));
			}
		}
	}

	private static int SortCompare(PetInfo AF1, PetInfo AF2)
	{
		int result = 0;
		if (PetSelectUI.Instance.mMode == PetSelectUI.Mode.PetTask)
		{
			if (PetTaskFormationUIView.Instance.IsRecommend(AF1.petId) && !PetTaskFormationUIView.Instance.IsRecommend(AF2.petId))
			{
				return -1;
			}
			if (!PetTaskFormationUIView.Instance.IsRecommend(AF1.petId) && PetTaskFormationUIView.Instance.IsRecommend(AF2.petId))
			{
				return 1;
			}
		}
		if (AF1.publicBaseInfo.simpleInfo.Fighting > AF2.publicBaseInfo.simpleInfo.Fighting)
		{
			return -1;
		}
		if (AF1.publicBaseInfo.simpleInfo.Fighting < AF2.publicBaseInfo.simpleInfo.Fighting)
		{
			return 1;
		}
		return result;
	}

	private void ClearPetItems()
	{
		for (int i = 0; i < this.listPetItems.get_Count(); i++)
		{
			GameObject gameObject = this.listPetItems.get_Item(i);
			Object.Destroy(gameObject);
		}
		this.listPetItems.Clear();
	}

	private bool IsSelected(long uuid)
	{
		for (int i = 0; i < this.listPetsSelected.get_Count(); i++)
		{
			if (this.listPetsSelected.get_Item(i) == uuid)
			{
				return true;
			}
		}
		return false;
	}

	private GameObject GetItem(PetInfo petinfo, Pet dataPet, bool isSelected)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
		instantiate2Prefab.set_name("Item_" + petinfo.petId);
		PetID component = instantiate2Prefab.GetComponent<PetID>();
		component.petInfo = petinfo;
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.get_transform().SetParent(this.GridChoosePet.get_transform());
		instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
		instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPetChoose);
		component.SetItem(dataPet, petinfo);
		component.ShowLimit(PetManager.Instance.IsFormationFromInstance && PetManagerBase.IsPetLimit(petinfo.petId));
		component.ShowSelect(isSelected);
		if (this.mMode != PetSelectUI.Mode.PetTask)
		{
			component.SetRecommend(false);
		}
		else
		{
			component.SetRecommend(PetTaskFormationUIView.Instance.IsRecommend((int)dataPet.id));
		}
		return instantiate2Prefab;
	}

	public void RefreshUIIsInstance(int formationId)
	{
		this.mMode = PetSelectUI.Mode.Instance;
		this.m_maxPetsCount = 3;
		this.m_formationId = formationId;
		this.FilterPetsIsInstance();
		this.PetSelectsIsInstance();
		this.RefreshUI();
	}

	private void FilterPetsIsInstance()
	{
		this.listPetsShow.Clear();
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = PetManager.Instance.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				this.listPetsShow.Add(current);
			}
		}
		this.listPetsShow.Sort(new Comparison<PetInfo>(PetSelectUI.SortCompare));
	}

	private void PetSelectsIsInstance()
	{
		if (this.mMode != PetSelectUI.Mode.Instance)
		{
			return;
		}
		this.listPetsSelected.Clear();
		PetFormation formationData = PetManager.Instance.GetFormationData(this.m_formationId);
		if (formationData == null)
		{
			return;
		}
		if (formationData != null && formationData.petFormationArr != null && formationData.petFormationArr.Int64Array != null)
		{
			List<long> list = new List<long>();
			for (int i = 0; i < formationData.petFormationArr.Int64Array.get_Count(); i++)
			{
				for (int j = 0; j < formationData.petFormationArr.Int64Array.get_Count(); j++)
				{
					Int64IndexValue int64IndexValue = formationData.petFormationArr.Int64Array.get_Item(j);
					if (int64IndexValue.index == i)
					{
						list.Add(int64IndexValue.value);
						break;
					}
				}
			}
			for (int k = 0; k < list.get_Count(); k++)
			{
				long num = list.get_Item(k);
				for (int l = 0; l < this.listPetsShow.get_Count(); l++)
				{
					PetInfo petInfo = this.listPetsShow.get_Item(l);
					if (petInfo.id == num)
					{
						this.listPetsSelected.Add(petInfo.id);
					}
				}
			}
		}
	}

	private void ConfirmIsInstance()
	{
		if (this.mMode != PetSelectUI.Mode.Instance)
		{
			return;
		}
		if (PetManager.Instance.IsFormationFromInstance)
		{
			for (int i = 0; i < this.listPetsSelected.get_Count(); i++)
			{
				if (PetManagerBase.IsPetLimit(this.listPetsSelected.get_Item(i)))
				{
					InstanceManagerUI.ShowLimitMessage();
					return;
				}
			}
		}
		if (this.listPetsSelected.get_Count() < 3 && this.listPetsShow.get_Count() >= 3)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(510101, false), GameDataUtils.GetChineseContent(510095, false), null, delegate
			{
				PetManager.Instance.SendMsgPetFormation(this.m_formationId, this.listPetsSelected);
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		PetManager.Instance.SendMsgPetFormation(this.m_formationId, this.listPetsSelected);
	}

	private void ChooseIsInstance(GameObject sender)
	{
		if (this.mMode != PetSelectUI.Mode.Instance)
		{
			return;
		}
		PetID component = sender.GetComponent<PetID>();
		if (component == null)
		{
			return;
		}
		string[] array = sender.get_name().Split("_".ToCharArray());
		int num = int.Parse(array[1]);
		GameObject gameObject = sender.get_transform().FindChild("ImageSelect").get_gameObject();
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById(num);
		if (petInfoById == null)
		{
			Debug.LogError("宠物不存在, petId = " + num);
			return;
		}
		Pet pet = DataReader<Pet>.Get(num);
		if (pet.fightRoleLv > EntityWorld.Instance.EntSelf.Lv)
		{
			string text = GameDataUtils.GetChineseContent(510112, false);
			text = text.Replace("{s1}", pet.fightRoleLv.ToString());
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (gameObject.get_activeSelf())
		{
			component.ShowSelect(false);
			component.ShowLimit(PetManager.Instance.IsFormationFromInstance && PetManagerBase.IsPetLimit(petInfoById.petId));
			for (int i = 0; i < this.listPetsSelected.get_Count(); i++)
			{
				if (this.listPetsSelected.get_Item(i) == petInfoById.id)
				{
					this.listPetsSelected.RemoveAt(i);
					break;
				}
			}
		}
		else
		{
			if (this.listPetsSelected.get_Count() == this.m_maxPetsCount)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505132, false));
				return;
			}
			if (PetManager.Instance.IsFormationFromInstance && PetManagerBase.IsPetLimit(petInfoById.petId))
			{
				InstanceManagerUI.ShowLimitMessage();
				return;
			}
			component.ShowSelect(true);
			this.listPetsSelected.Add(petInfoById.id);
		}
	}

	public void RefreshUIIsPetTask(List<int> listPets, int quality)
	{
		this.mMode = PetSelectUI.Mode.PetTask;
		this.m_maxPetsCount = 3;
		this.FilterPetsIsPetTask(quality);
		this.PetSelectsIsPetTask(listPets);
		this.RefreshUI();
	}

	private void FilterPetsIsPetTask(int quality)
	{
		this.listPetsShow.Clear();
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = PetManager.Instance.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				if (!PetTaskManager.Instance.IsInFormation(current.petId))
				{
					this.listPetsShow.Add(current);
				}
			}
		}
		this.listPetsShow.Sort(new Comparison<PetInfo>(PetSelectUI.SortCompare));
	}

	private void PetSelectsIsPetTask(List<int> listPets)
	{
		if (this.mMode != PetSelectUI.Mode.PetTask)
		{
			return;
		}
		this.listPetsSelected.Clear();
		for (int i = 0; i < listPets.get_Count(); i++)
		{
			PetInfo petInfoById = PetManager.Instance.GetPetInfoById(listPets.get_Item(i));
			if (petInfoById != null)
			{
				this.listPetsSelected.Add(petInfoById.id);
			}
		}
	}

	private void ConfirmIsPetTask()
	{
		if (this.mMode != PetSelectUI.Mode.PetTask)
		{
			return;
		}
		PetTaskFormationUIView.Instance.SendPetFormation(this.listPetsSelected);
	}

	private void ChooseIsPetTask(GameObject sender)
	{
		if (this.mMode != PetSelectUI.Mode.PetTask)
		{
			return;
		}
		PetID component = sender.GetComponent<PetID>();
		if (component == null)
		{
			return;
		}
		string[] array = sender.get_name().Split("_".ToCharArray());
		int num = int.Parse(array[1]);
		GameObject gameObject = sender.get_transform().FindChild("ImageSelect").get_gameObject();
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById(num);
		if (petInfoById == null)
		{
			Debug.LogError("宠物不存在, petId = " + num);
			return;
		}
		Pet pet = DataReader<Pet>.Get(num);
		if (pet.fightRoleLv > EntityWorld.Instance.EntSelf.Lv)
		{
			string text = GameDataUtils.GetChineseContent(510112, false);
			text = text.Replace("{s1}", pet.fightRoleLv.ToString());
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (gameObject.get_activeSelf())
		{
			component.ShowSelect(false);
			component.ShowLimit(PetManager.Instance.IsFormationFromInstance && PetManagerBase.IsPetLimit(petInfoById.petId));
			for (int i = 0; i < this.listPetsSelected.get_Count(); i++)
			{
				if (this.listPetsSelected.get_Item(i) == petInfoById.id)
				{
					this.listPetsSelected.RemoveAt(i);
					break;
				}
			}
		}
		else
		{
			if (this.listPetsSelected.get_Count() == this.m_maxPetsCount)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513702, false));
				return;
			}
			component.ShowSelect(true);
			this.listPetsSelected.Add(petInfoById.id);
		}
	}
}
