using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePetChooseUI : UIBase
{
	private Transform UIs;

	private List<ChangePetChooseUnit> PetUnitList;

	private ButtonCustom BtnChangeFormation;

	private Text TextFormationSelect;

	private ButtonCustom PetsBar;

	private Transform FormationSelector;

	private PetFormationType.FORMATION_TYPE currentType;

	private Transform currentTransfromAbove;

	private int currentSubType;

	private Vector3 posPetsBarWhenSelectorRight = new Vector3(-175f, 0f, 0f);

	private Vector3 posSelectorWhenSelectorRight = new Vector3(10f, 0f, 0f);

	private float uisScaleRight = 1f;

	private Vector3 posPetsBarWhenSelectorLeft = new Vector3(-45f, 0f, 0f);

	private Vector3 posSelectorWhenSelectorLeft = new Vector3(-330f, 0f, 0f);

	private float uisScaleLeft = 1f;

	private Vector3 posPetsBarWhenSelectorDefendFight = new Vector3(-210f, 0f, 0f);

	private Vector3 posSelectorWhenSelectorDefendFight = new Vector3(-30f, 0f, 0f);

	private float uisScaleDefendFight = 1f;

	private Vector3 posPetsBarWhenSelectorDarkTrial = new Vector3(575f, 0f, 0f);

	private Vector3 posSelectorWhenSelectorDarkTrial = new Vector3(755f, 0f, 0f);

	private float uisScaleDarkTrial = 0.9f;

	public static bool limit_pet;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.UIs = base.FindTransform("UIs");
		this.PetUnitList = new List<ChangePetChooseUnit>();
		for (int i = 1; i < 4; i++)
		{
			ChangePetChooseUnit component = base.FindTransform("Pet" + i).GetComponent<ChangePetChooseUnit>();
			if (component != null)
			{
				this.PetUnitList.Add(component);
			}
		}
		this.BtnChangeFormation = base.FindTransform("BtnChangeFormation").GetComponent<ButtonCustom>();
		this.TextFormationSelect = base.FindTransform("TextFormationSelect").GetComponent<Text>();
		this.PetsBar = base.FindTransform("PetsBar").GetComponent<ButtonCustom>();
		this.FormationSelector = base.FindTransform("FormationSelector");
		this.BtnChangeFormation.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnChangeFormation);
		this.PetsBar.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPetsBar);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetSetCurFormationIdRes, new Callback(this.OnGetSetFormationIdByTypeRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetSetCurFormationIdRes, new Callback(this.OnGetSetFormationIdByTypeRes));
	}

	private void OnGetSetFormationIdByTypeRes()
	{
		this.RefreshUI(this.currentType, this.currentTransfromAbove, this.currentSubType);
	}

	private void OnClickPetsBar(GameObject sender)
	{
		if (MultiPlayerInstance.Instance.isMulti)
		{
			PetManager.Instance.Jump2Formation(true);
			return;
		}
		EventDispatcher.Broadcast(EventNames.ShouldShowInstanceDetailUI);
		PetManager.Instance.Jump2Formation(true);
	}

	private void OnClickBtnChangeFormation(GameObject sender)
	{
		List<int> formationLvs = PetManager.Instance.GetFormationLvs();
		bool flag = false;
		int changeFormationID = 1;
		if (PetManager.Instance.CurrentFormationID == 1 && EntityWorld.Instance.EntSelf.Lv >= formationLvs.get_Item(1))
		{
			PetManager.Instance.SendSetCurFormationIdReq(2);
			changeFormationID = 2;
		}
		else if (PetManager.Instance.CurrentFormationID == 1 && EntityWorld.Instance.EntSelf.Lv < formationLvs.get_Item(1))
		{
			string chineseContent = GameDataUtils.GetChineseContent(510092, false);
			UIManagerControl.Instance.ShowToastText(chineseContent.Replace("{s1}", formationLvs.get_Item(1).ToString()));
			flag = true;
		}
		else if (PetManager.Instance.CurrentFormationID == 2 && EntityWorld.Instance.EntSelf.Lv >= formationLvs.get_Item(2))
		{
			PetManager.Instance.SendSetCurFormationIdReq(3);
			changeFormationID = 3;
		}
		else if (PetManager.Instance.CurrentFormationID == 2 && EntityWorld.Instance.EntSelf.Lv < formationLvs.get_Item(2))
		{
			PetManager.Instance.SendSetCurFormationIdReq(1);
			changeFormationID = 1;
		}
		else if (PetManager.Instance.CurrentFormationID == 3)
		{
			PetManager.Instance.SendSetCurFormationIdReq(1);
			changeFormationID = 1;
		}
		if (!flag)
		{
			bool flag2 = true;
			int i;
			for (i = 1; i <= 3; i++)
			{
				PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == i);
				if (petFormation != null && petFormation.petFormationArr != null && petFormation.petFormationArr.Int64Array != null && petFormation.petFormationArr.Int64Array.get_Count() != 0)
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510093, false));
			}
			else
			{
				PetFormation petFormation2 = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == changeFormationID);
				if (petFormation2 == null || petFormation2.petFormationArr == null || petFormation2.petFormationArr.Int64Array == null || petFormation2.petFormationArr.Int64Array.get_Count() == 0)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510094, false));
				}
			}
		}
	}

	public void RefreshUI(PetFormationType.FORMATION_TYPE type, Transform transfromAbove, int subType = 0)
	{
		this.currentTransfromAbove = transfromAbove;
		this.currentType = type;
		this.currentSubType = subType;
		base.SetSiblingIndex(transfromAbove.GetSiblingIndex() + 1);
		this.TextFormationSelect.set_text("阵容" + PetManager.Instance.CurrentFormationID);
		this.ResetUICoordinates(type, subType);
		this.RefreshPos(type);
		this.RefreshPetInfo();
	}

	private void RefreshPetInfo()
	{
		ChangePetChooseUI.limit_pet = false;
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		for (int i = 0; i < this.PetUnitList.get_Count(); i++)
		{
			this.PetUnitList.get_Item(i).SetHavePet(false);
			this.PetUnitList.get_Item(i).SetImageTip(false);
		}
		if (petFormation == null || petFormation.petFormationArr == null || petFormation.petFormationArr.Int64Array == null)
		{
			for (int j = 0; j < PetManager.Instance.MaplistPet.get_Values().get_Count(); j++)
			{
				if (j >= this.PetUnitList.get_Count())
				{
					break;
				}
				this.PetUnitList.get_Item(j).SetImageTip(true);
			}
		}
		else
		{
			int k;
			for (k = 0; k < petFormation.petFormationArr.Int64Array.get_Count(); k++)
			{
				Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(k);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
				Pet dataPet = DataReader<Pet>.Get(petInfo.petId);
				if (k >= this.PetUnitList.get_Count())
				{
					break;
				}
				this.SetPet(this.PetUnitList.get_Item(k), dataPet, petInfo);
				this.PetUnitList.get_Item(k).SetHavePet(true);
			}
			for (int l = k; l < PetManager.Instance.MaplistPet.get_Keys().get_Count(); l++)
			{
				if (l >= this.PetUnitList.get_Count())
				{
					break;
				}
				if (this.CheckCanShowChangePetTip() < l)
				{
					break;
				}
				this.PetUnitList.get_Item(l).SetImageTip(true);
			}
		}
	}

	private int CheckCanShowChangePetTip()
	{
		int num = 0;
		using (Dictionary<long, PetInfo>.Enumerator enumerator = PetManager.Instance.MaplistPet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, PetInfo> current = enumerator.get_Current();
				Pet pet = DataReader<Pet>.Get(current.get_Value().petId);
				if (pet.fightRoleLv <= EntityWorld.Instance.EntSelf.Lv)
				{
					num++;
				}
			}
		}
		return num;
	}

	private void SetPet(ChangePetChooseUnit unit, Pet dataPet, PetInfo petInfo)
	{
		unit.SetItem(dataPet, petInfo);
		if (PetManagerBase.IsPetLimit(dataPet))
		{
			unit.SetImageLimit(true);
			ChangePetChooseUI.limit_pet = true;
		}
		else
		{
			unit.SetImageLimit(false);
		}
	}

	private void RefreshPos(PetFormationType.FORMATION_TYPE type)
	{
		Vector3 vector = this.posSelectorWhenSelectorRight;
		Vector3 vector2 = this.posPetsBarWhenSelectorRight;
		float num = this.uisScaleRight;
		switch (type)
		{
		case PetFormationType.FORMATION_TYPE.Arena:
		case PetFormationType.FORMATION_TYPE.Gang:
		case PetFormationType.FORMATION_TYPE.PVP:
		case PetFormationType.FORMATION_TYPE.Bounty:
			vector = this.posSelectorWhenSelectorLeft;
			vector2 = this.posPetsBarWhenSelectorLeft;
			num = this.uisScaleLeft;
			goto IL_A2;
		case PetFormationType.FORMATION_TYPE.PVE:
		case PetFormationType.FORMATION_TYPE.GuildWar:
		case PetFormationType.FORMATION_TYPE.Survival:
		case PetFormationType.FORMATION_TYPE.ElementCopy:
		case PetFormationType.FORMATION_TYPE.CreateFight:
			IL_47:
			if (type != PetFormationType.FORMATION_TYPE.MultiPve)
			{
				goto IL_A2;
			}
			vector = this.posSelectorWhenSelectorDarkTrial;
			vector2 = this.posPetsBarWhenSelectorDarkTrial;
			num = this.uisScaleDarkTrial;
			goto IL_A2;
		case PetFormationType.FORMATION_TYPE.Defend:
			vector = this.posSelectorWhenSelectorDefendFight;
			vector2 = this.posPetsBarWhenSelectorDefendFight;
			num = this.uisScaleDefendFight;
			goto IL_A2;
		}
		goto IL_47;
		IL_A2:
		this.PetsBar.GetComponent<RectTransform>().set_anchoredPosition(vector2);
		this.FormationSelector.GetComponent<RectTransform>().set_anchoredPosition(vector);
		this.UIs.set_localScale(new Vector3(num, num, 1f));
	}

	private void ResetUICoordinates(PetFormationType.FORMATION_TYPE type, int subType = 0)
	{
		if (type == PetFormationType.FORMATION_TYPE.Arena)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(-202f, -297f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Normal)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(350f, -125f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Elite)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(340f, -100f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Gang)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(-195f, -270f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.PVE)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(340f, -165f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Survival)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(466f, -90f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.ElementCopy)
		{
			if (subType == 1)
			{
				this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(110f, -7f, 0f));
			}
			else
			{
				this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(110f, -78f, 0f));
			}
		}
		else if (type == PetFormationType.FORMATION_TYPE.PVP)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(-226f, -285f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Defend)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(368f, -147f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.Bounty)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(-293f, -300f, 0f));
		}
		else if (type == PetFormationType.FORMATION_TYPE.MultiPve)
		{
			this.UIs.GetComponent<RectTransform>().set_localPosition(new Vector3(-196f, -181f, 0f));
		}
	}
}
