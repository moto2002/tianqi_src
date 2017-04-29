using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetFormationUI : UIBase
{
	private List<PetFormationUnit> m_petFormation = new List<PetFormationUnit>();

	private Transform HaveLinkPet;

	private Transform NoLinkPet;

	private Transform Property1;

	private Transform Property2;

	private Transform Property3;

	private Transform linkedPet1;

	private Transform linkedPet2;

	private Transform linkedPet3;

	private int currentSelectFormationID = 1;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_petFormation.Clear();
		for (int i = 1; i <= 3; i++)
		{
			PetFormationUnit component = base.FindTransform("Formation" + i).GetComponent<PetFormationUnit>();
			component.SetAction(new Action<int>(this.OnClickFormation), new Action<int>(this.OnClickBtnChange), i - 1);
			this.m_petFormation.Add(component);
		}
		this.HaveLinkPet = base.FindTransform("HaveLinkPet");
		this.NoLinkPet = base.FindTransform("NoLinkPet");
		this.Property1 = base.FindTransform("Property1");
		this.Property2 = base.FindTransform("Property2");
		this.Property3 = base.FindTransform("Property3");
		this.linkedPet1 = base.FindTransform("linkedPet1");
		this.linkedPet2 = base.FindTransform("linkedPet2");
		this.linkedPet3 = base.FindTransform("linkedPet3");
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("PetManager.PetFormationHaveChange", new Callback(this.PetFormationHaveChange));
		EventDispatcher.AddListener(EventNames.OnGetSetCurFormationIdRes, new Callback(this.OnGetSetCurFormationIdRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("PetManager.PetFormationHaveChange", new Callback(this.PetFormationHaveChange));
		EventDispatcher.RemoveListener(EventNames.OnGetSetCurFormationIdRes, new Callback(this.OnGetSetCurFormationIdRes));
	}

	private void PetFormationHaveChange()
	{
		this.RefreshUI();
	}

	private void OnGetSetCurFormationIdRes()
	{
		this.RefreshCurFormation();
	}

	private void OnClickFormation(int index)
	{
		List<int> formationLvs = PetManager.Instance.GetFormationLvs();
		if (EntityWorld.Instance.EntSelf.Lv < formationLvs.get_Item(index))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510100, false));
			return;
		}
		this.RefreshFormationItemAndSetCurrentSelectFormationID(index + 1);
		this.RefreshRight(this.currentSelectFormationID);
		this.SendCurFormation(index + 1);
	}

	private void OnClickBtnChange(int index)
	{
		List<int> formationLvs = PetManager.Instance.GetFormationLvs();
		if (EntityWorld.Instance.EntSelf.Lv < formationLvs.get_Item(index))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510100, false));
			return;
		}
		PetSelectUI petSelectUI = UIManagerControl.Instance.OpenUI("PetSelectUI", null, false, UIType.NonPush) as PetSelectUI;
		petSelectUI.RefreshUIIsInstance(index + 1);
		this.RefreshRight(this.currentSelectFormationID);
	}

	public void RefreshUI(int currentSelectFormation)
	{
		this.currentSelectFormationID = currentSelectFormation;
		this.RefreshUI();
	}

	public void RefreshUI()
	{
		this.RefreshOpen();
		for (int i = 0; i < this.m_petFormation.get_Count(); i++)
		{
			this.SetPetItems(this.m_petFormation.get_Item(i), i + 1);
		}
		this.RefreshFormationItemAndSetCurrentSelectFormationID(this.currentSelectFormationID);
		this.RefreshRight(this.currentSelectFormationID);
		this.RefreshCurFormation();
	}

	private void RefreshCurFormation()
	{
		for (int i = 0; i < this.m_petFormation.get_Count(); i++)
		{
			this.m_petFormation.get_Item(i).ShowCurrentFormationFlag(false);
		}
		if (PetManager.Instance.CurrentFormationID > 0 && PetManager.Instance.CurrentFormationID <= this.m_petFormation.get_Count())
		{
			this.m_petFormation.get_Item(PetManager.Instance.CurrentFormationID - 1).ShowCurrentFormationFlag(true);
		}
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

	private void RefreshRight(int formationID)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == formationID);
		if (petFormation != null && petFormation.petFormationArr != null && petFormation.petFormationArr.Int64Array != null)
		{
			PetManager.Instance.GetFormationAddAttrValue(petFormation, out num, out num2, out num3);
			List<int> list = new List<int>();
			for (int i = 0; i < petFormation.petFormationArr.Int64Array.get_Count(); i++)
			{
				Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(i);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
				list.Add(petInfo.petId);
			}
			List<ChongWuJiBan> list2 = new List<ChongWuJiBan>();
			for (int j = 0; j < DataReader<ChongWuJiBan>.DataList.get_Count(); j++)
			{
				bool flag = true;
				ChongWuJiBan chongWuJiBan = DataReader<ChongWuJiBan>.DataList.get_Item(j);
				for (int k = 0; k < chongWuJiBan.linkedPetId.get_Count(); k++)
				{
					int num4 = chongWuJiBan.linkedPetId.get_Item(k);
					if (!list.Contains(num4))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list2.Add(chongWuJiBan);
				}
			}
			if (list2.get_Count() > 0)
			{
				for (int l = 0; l < list2.get_Count(); l++)
				{
					ChongWuJiBan chongWuJiBan2 = list2.get_Item(l);
					Transform transform = null;
					if (l == 0)
					{
						transform = this.linkedPet1;
					}
					if (l == 2)
					{
						transform = this.linkedPet2;
					}
					if (l == 3)
					{
						transform = this.linkedPet3;
					}
					transform.FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(chongWuJiBan2.name, false));
					string text = string.Empty;
					Attrs attrs = DataReader<Attrs>.Get(chongWuJiBan2.linkedAttrId);
					for (int m = 0; m < attrs.attrs.get_Count(); m++)
					{
						text += AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(m), attrs.values.get_Item(m));
					}
					transform.FindChild("Content").GetComponent<Text>().set_text(text);
				}
				if (list2.get_Count() == 1)
				{
					this.linkedPet1.get_gameObject().SetActive(true);
					this.linkedPet2.get_gameObject().SetActive(false);
					this.linkedPet3.get_gameObject().SetActive(false);
				}
				else if (list2.get_Count() == 2)
				{
					this.linkedPet1.get_gameObject().SetActive(true);
					this.linkedPet2.get_gameObject().SetActive(true);
					this.linkedPet3.get_gameObject().SetActive(false);
				}
				else if (list2.get_Count() == 3)
				{
					this.linkedPet1.get_gameObject().SetActive(true);
					this.linkedPet2.get_gameObject().SetActive(true);
					this.linkedPet3.get_gameObject().SetActive(true);
				}
			}
		}
		this.Property1.FindChild("Content").GetComponent<Text>().set_text(AttrUtility.GetAttrName(GameData.AttrType.Atk) + ":");
		this.Property2.FindChild("Content").GetComponent<Text>().set_text(AttrUtility.GetAttrName(GameData.AttrType.Defence) + ":");
		this.Property3.FindChild("Content").GetComponent<Text>().set_text(AttrUtility.GetAttrName(GameData.AttrType.HpLmt) + ":");
		this.Property1.FindChild("Value").GetComponent<Text>().set_text(num.ToString());
		this.Property2.FindChild("Value").GetComponent<Text>().set_text(num2.ToString());
		this.Property3.FindChild("Value").GetComponent<Text>().set_text(num3.ToString());
	}

	private void SetPetItems(PetFormationUnit pfu, int formationID)
	{
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == formationID);
		if (petFormation == null || petFormation.petFormationArr == null || petFormation.petFormationArr.Int64Array == null)
		{
			for (int i = 0; i < 3; i++)
			{
				pfu.pets.get_Item(i).SetItem(null, null, false);
			}
		}
		else
		{
			for (int j = 0; j < petFormation.petFormationArr.Int64Array.get_Count(); j++)
			{
				Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(j);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
				Pet dataPet = DataReader<Pet>.Get(petInfo.petId);
				pfu.pets.get_Item(int64IndexValue.index).SetItem(petInfo, dataPet, PetManager.Instance.IsFormationFromInstance && PetManagerBase.IsPetLimit(dataPet));
			}
			for (int k = 0; k < 3; k++)
			{
				if (k >= petFormation.petFormationArr.Int64Array.get_Count())
				{
					pfu.pets.get_Item(k).SetItem(null, null, false);
				}
			}
		}
	}

	private void RefreshFormationItemAndSetCurrentSelectFormationID(int formationid)
	{
		for (int i = 0; i < this.m_petFormation.get_Count(); i++)
		{
			if (this.currentSelectFormationID == i + 1)
			{
				this.m_petFormation.get_Item(i).SetSelect(false);
				this.m_petFormation.get_Item(i).ShowImageSelete(false);
			}
			else
			{
				this.m_petFormation.get_Item(i).SetSelect(true);
			}
		}
		this.currentSelectFormationID = formationid;
		for (int j = 0; j < this.m_petFormation.get_Count(); j++)
		{
			if (this.currentSelectFormationID == j + 1)
			{
				this.m_petFormation.get_Item(j).SetSelect(true);
				this.m_petFormation.get_Item(j).ShowImageSelete(true);
			}
			else
			{
				this.m_petFormation.get_Item(j).SetSelect(false);
			}
		}
	}

	private void SendCurFormation(int formationID)
	{
		List<int> formationLvs = PetManager.Instance.GetFormationLvs();
		if (PetManager.Instance.CurrentFormationID != formationID && EntityWorld.Instance.EntSelf.Lv >= formationLvs.get_Item(formationID - 1))
		{
			PetManager.Instance.SendSetCurFormationIdReq(formationID);
		}
	}
}
