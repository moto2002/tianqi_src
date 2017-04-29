using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PetChooseUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_PetChooses = "PetChooses";

		public const string Attr_PetChooseLines = "PetChooseLines";

		public const string Attr_ShiftType = "ShiftType";
	}

	public static PetChooseUIViewModel Instance;

	private Vector3 _ShiftType;

	public ObservableCollection<OOPetChooseUnit> PetChooses = new ObservableCollection<OOPetChooseUnit>();

	public ObservableCollection<OOPetChooseItems> PetChooseLines = new ObservableCollection<OOPetChooseItems>();

	public int PetSelectedIndex;

	private List<int> uniqueIds = new List<int>();

	private List<OOPetChooseUnit> tempPetChooses = new List<OOPetChooseUnit>();

	public List<Goods> PetFragmentGoods
	{
		get
		{
			List<Goods> list = new List<Goods>();
			List<Goods> bag = BackpackManager.Instance.Bag;
			for (int i = 0; i < bag.get_Count(); i++)
			{
				Items item = BackpackManager.Instance.GetItem(bag.get_Item(i).GetItemId());
				if (item != null && item.tab == 4 && item.secondType == 6)
				{
					list.Add(bag.get_Item(i));
				}
			}
			return list;
		}
	}

	public Vector3 ShiftType
	{
		get
		{
			return this._ShiftType;
		}
		set
		{
			this._ShiftType = value;
			base.NotifyProperty("ShiftType", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PetChooseUIViewModel.Instance = this;
	}

	private void OnEnable()
	{
		this.RefreshPets(true);
	}

	private void OnDisable()
	{
		this.PetChooseLines.Clear();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		this.PetChooses.Clear();
		this.PetChooses = null;
		this.PetChooseLines.Clear();
		this.PetChooseLines = null;
		this.tempPetChooses.Clear();
		this.tempPetChooses = null;
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener("PetManager.RefreshPets", new Callback(this.OnRefreshPets));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener("PetManager.RefreshPets", new Callback(this.OnRefreshPets));
	}

	private void OnRefreshPets()
	{
		this.RefreshPets(false);
	}

	public void SetSelectedByID(int petId, bool refreshRightNow = true)
	{
		int indexByID = this.GetIndexByID(petId);
		if (refreshRightNow)
		{
			this.SetSelectedByIndex(indexByID);
		}
		else if (this.PetSelectedIndex != indexByID)
		{
			this.SetSelectedByIndex(indexByID);
		}
	}

	private void SetSelectedByIndex(int selectedIndex)
	{
		for (int i = 0; i < this.PetChooses.Count; i++)
		{
			OOPetChooseUnit item = this.PetChooses.GetItem(i);
			if (item != null && i == selectedIndex)
			{
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo(item.PetUID, item.PetId);
				this.PetSelectedIndex = i;
			}
		}
	}

	private int GetIndexByID(int petId)
	{
		for (int i = 0; i < this.PetChooses.Count; i++)
		{
			OOPetChooseUnit item = this.PetChooses.GetItem(i);
			if (item != null && item.PetId == petId)
			{
				return i;
			}
		}
		return 0;
	}

	private void SortPetChooses(List<OOPetChooseUnit> tempPetChooses)
	{
		this.PetChooses.Clear();
		PetManager.PetActiveIds.Clear();
		if (tempPetChooses == null)
		{
			return;
		}
		tempPetChooses.Sort(new Comparison<OOPetChooseUnit>(PetChooseUIViewModel.SortCompare));
		for (int i = 0; i < tempPetChooses.get_Count(); i++)
		{
			this.PetChooses.Add(tempPetChooses.get_Item(i));
			if (PetManager.Instance.GetPetInfoById(tempPetChooses.get_Item(i).PetId) != null)
			{
				PetManager.PetActiveIds.Add(tempPetChooses.get_Item(i).PetUID);
			}
		}
	}

	private static int SortCompare(OOPetChooseUnit AF1, OOPetChooseUnit AF2)
	{
		int result = 0;
		if (AF1.PetUpgradeLevel > AF2.PetUpgradeLevel)
		{
			result = -1;
		}
		else if (AF1.PetUpgradeLevel < AF2.PetUpgradeLevel)
		{
			result = 1;
		}
		else if (AF1.BattleFighting > AF2.BattleFighting)
		{
			result = -1;
		}
		else if (AF1.BattleFighting < AF2.BattleFighting)
		{
			result = 1;
		}
		else if (AF1.PetId < AF2.PetId)
		{
			result = -1;
		}
		else if (AF1.PetId > AF2.PetId)
		{
			result = 1;
		}
		return result;
	}

	private void RefreshPets(bool reSort)
	{
		if (reSort)
		{
			this.uniqueIds.Clear();
			this.tempPetChooses.Clear();
			using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = PetManager.Instance.MaplistPet.get_Values().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PetInfo current = enumerator.get_Current();
					OOPetChooseUnit oOPetChooseUnit = new OOPetChooseUnit
					{
						PetUID = current.id,
						PetId = current.petId
					};
					this.tempPetChooses.Add(oOPetChooseUnit);
					this.uniqueIds.Add(current.petId);
				}
			}
			List<Pet> levelPassPets = PetManager.Instance.GetLevelPassPets();
			for (int i = 0; i < levelPassPets.get_Count(); i++)
			{
				int id = (int)levelPassPets.get_Item(i).id;
				if (!this.uniqueIds.Contains(id))
				{
					OOPetChooseUnit oOPetChooseUnit2 = new OOPetChooseUnit
					{
						PetId = id
					};
					this.tempPetChooses.Add(oOPetChooseUnit2);
					this.uniqueIds.Add(id);
				}
			}
			this.RefreshActivatedPets(this.tempPetChooses);
			this.RefreshNotActivatedPets(this.tempPetChooses);
			this.SortPetChooses(this.tempPetChooses);
		}
		this.RefreshActivatedPets(this.PetChooses.OClist);
		this.RefreshNotActivatedPets(this.PetChooses.OClist);
		this.SetListView();
	}

	public void RefreshActivatedPets(List<OOPetChooseUnit> ooPets)
	{
		for (int i = 0; i < ooPets.get_Count(); i++)
		{
			PetInfo petInfoById = PetManager.Instance.GetPetInfoById(ooPets.get_Item(i).PetId);
			if (petInfoById != null)
			{
				OOPetChooseUnit dataUnit = ooPets.get_Item(i);
				this.RefreshActivatedPet(petInfoById, dataUnit);
			}
		}
	}

	public void RefreshActivatedPet(PetInfo petInfo, OOPetChooseUnit dataUnit)
	{
		if (dataUnit == null)
		{
			return;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return;
		}
		dataUnit.PetUID = petInfo.id;
		dataUnit.PetId = petInfo.petId;
		dataUnit.BadgeTip = PetManager.Instance.CheckPetBadge(petInfo);
		dataUnit.PetUpgradeLevel = petInfo.star;
		dataUnit.PetIconHSV = 0;
		dataUnit.BattleFighting = petInfo.publicBaseInfo.simpleInfo.Fighting;
		dataUnit.Level = "Lv" + petInfo.lv;
		dataUnit.PetName = PetManager.GetPetName(pet, false);
		dataUnit.InFormation = PetManager.Instance.IsInFormation(petInfo.id);
		if (pet != null && pet.needFragment.get_Count() > 0)
		{
			int num = petInfo.star + 1;
			if (num <= pet.needFragment.get_Count())
			{
				long num2 = BackpackManager.Instance.OnGetGoodCount(pet.fragmentId);
				dataUnit.MatNum = num2 + "/" + pet.needFragment.get_Item(num - 1);
				if (num2 >= (long)pet.needFragment.get_Item(num - 1))
				{
					dataUnit.PetStatus = OOPetChooseUnit.Status.HaveActivation_StarEnough;
				}
				else
				{
					dataUnit.PetStatus = OOPetChooseUnit.Status.HaveActivation_StarNoEnough;
				}
			}
			else
			{
				dataUnit.PetStatus = OOPetChooseUnit.Status.HaveActivation_StarTop;
			}
		}
	}

	public void RefreshNotActivatedPets(List<OOPetChooseUnit> ooPets)
	{
		for (int i = 0; i < ooPets.get_Count(); i++)
		{
			if (PetManager.Instance.GetPetInfoById(ooPets.get_Item(i).PetId) == null)
			{
				OOPetChooseUnit oOPetChooseUnit = ooPets.get_Item(i);
				this.RefreshNotActivatedPet(oOPetChooseUnit.PetId, oOPetChooseUnit);
			}
		}
		if (PetBasicUIViewModel.Instance != null)
		{
			PetBasicUIViewModel.Instance.SubFormationBadge = PetManager.Instance.CheckAllCanFormation();
		}
	}

	private void RefreshNotActivatedPet(int petId, OOPetChooseUnit dataUnit)
	{
		if (dataUnit == null)
		{
			return;
		}
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet != null && pet.needFragment.get_Count() > 0)
		{
			dataUnit.PetUID = 0L;
			dataUnit.PetId = petId;
			dataUnit.BadgeTip = false;
			dataUnit.PetUpgradeLevel = pet.initStar;
			dataUnit.Level = string.Empty;
			dataUnit.PetName = PetManager.GetPetName(pet, false);
			dataUnit.InFormation = false;
			dataUnit.BattleFighting = 0L;
			dataUnit.PetIconHSV = 6;
			dataUnit.PetStatus = OOPetChooseUnit.Status.NoActivation;
			dataUnit.MatNum = GameDataUtils.GetChineseContent(pet.getTip, false);
		}
	}

	private void SetListView()
	{
		this.PetChooseLines.Clear();
		OOPetChooseItems oOPetChooseItems = null;
		if (PetChooseUIView.Instance.m_listView.m_listSpacing == null)
		{
			PetChooseUIView.Instance.m_listView.m_listSpacing = new List<float>();
		}
		PetChooseUIView.Instance.m_listView.m_listSpacing.Clear();
		int num = 0;
		for (int i = 0; i < this.PetChooses.Count; i++)
		{
			if (PetManager.Instance.GetPetInfoById(this.PetChooses[i].PetId) != null)
			{
				if (num % 3 == 0)
				{
					oOPetChooseItems = new OOPetChooseItems();
					oOPetChooseItems.LineRegion = false;
					this.PetChooseLines.Add(oOPetChooseItems);
					PetChooseUIView.Instance.m_listView.m_listSpacing.Add(180f);
				}
				oOPetChooseItems.Items.Add(this.PetChooses[i]);
				num++;
			}
		}
		if (this.PetChooses.Count == num)
		{
			return;
		}
		oOPetChooseItems = new OOPetChooseItems();
		oOPetChooseItems.LineRegion = true;
		this.PetChooseLines.Add(oOPetChooseItems);
		PetChooseUIView.Instance.m_listView.m_listSpacing.Add(120f);
		int num2 = 0;
		for (int j = 0; j < this.PetChooses.Count; j++)
		{
			if (PetManager.Instance.GetPetInfoById(this.PetChooses[j].PetId) == null)
			{
				if (num2 % 3 == 0)
				{
					oOPetChooseItems = new OOPetChooseItems();
					oOPetChooseItems.LineRegion = false;
					this.PetChooseLines.Add(oOPetChooseItems);
					PetChooseUIView.Instance.m_listView.m_listSpacing.Add(180f);
				}
				oOPetChooseItems.Items.Add(this.PetChooses[j]);
				num2++;
			}
		}
	}
}
