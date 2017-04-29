using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PetEvo : UIBase
{
	public static PetEvo instance;

	private List<int> cellIds;

	protected float OffsetY;

	protected float Spacing;

	private int clickedCellIndex = -1;

	private PetEvoTip petEvoTip;

	private Dictionary<int, string> qualityColorMap;

	public int ClickedCellIndex
	{
		get
		{
			return this.clickedCellIndex;
		}
		set
		{
			this.clickedCellIndex = value;
		}
	}

	protected PetEvo()
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		dictionary.Add(1, "绿色");
		dictionary.Add(2, "蓝色");
		dictionary.Add(3, "蓝色+1");
		dictionary.Add(4, "紫色");
		dictionary.Add(5, "紫色+1");
		dictionary.Add(6, "紫色+2");
		dictionary.Add(7, "橙色");
		this.qualityColorMap = dictionary;
		base..ctor();
	}

	protected abstract List<int> GetCellIds();

	protected abstract void OnPlaySKill(GameObject go);

	protected abstract void SetOneSkill(int cellIndex, int talentId, int talentLv, int nextTalentLv);

	protected abstract void InitCell(int cellIndex);

	public void AwakeSelf()
	{
		PetEvo.instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.None, false);
	}

	protected override void OnEnable()
	{
		this.Initialise();
	}

	public void Initialise()
	{
		this.cellIds = this.GetCellIds();
		this.InitList();
		this.SetList();
	}

	private void InitList()
	{
		this.DestroyCells();
		this.InitCells();
	}

	private void DestroyCells()
	{
		for (int i = 0; i < base.get_transform().get_childCount(); i++)
		{
			Transform child = base.get_transform().GetChild(i);
			child.set_name("unused");
			child.get_gameObject().SetActive(false);
		}
	}

	private void InitCells()
	{
		for (int i = 0; i < this.cellIds.get_Count(); i++)
		{
			this.InitCell(i);
		}
	}

	private void SetList()
	{
		for (int i = 0; i < this.cellIds.get_Count(); i++)
		{
			this.SetCell(i);
		}
	}

	private void OnBtnTip(GameObject go)
	{
		Debug.LogError("OnBtnDetail=" + base.get_name());
	}

	private void OnBtnLvUp(GameObject go)
	{
		Transform parent = go.get_transform().get_parent();
		int num = int.Parse(parent.get_name());
		int talentId = this.cellIds.get_Item(num);
		this.ClickedCellIndex = num;
		int skillLv = PetEvoGlobal.GetSkillLv(PetBasicUIViewModel.PetID, talentId);
		int num2 = skillLv + 1;
		if (num2 > PetManager.Instance.GetPetLevel(PetBasicUIViewModel.PetID))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(400005, false));
			return;
		}
		if (PetManager.Instance.GetSkillPoint() <= 0)
		{
			PetBasicUIViewModel.Instance.OnSPBtnBuyUp();
			return;
		}
		if (!PetEvoGlobal.IsEnoughMaterail(talentId, num2))
		{
			int materialId = PetEvoGlobal.GetMaterialId(talentId, num2);
			LinkNavigationManager.ItemNotEnoughToLink(materialId, true, null, true);
			return;
		}
		PetManager.Instance.SendPetTalentTrainReq(PetBasicUIViewModel.PetUID, talentId);
	}

	protected virtual void OnDownTip(GameObject go)
	{
		string name = go.get_transform().get_parent().get_name();
		int talentIndexByName = this.GetTalentIndexByName(name);
		int talentIdByName = this.GetTalentIdByName(name);
		this.petEvoTip = (UIManagerControl.Instance.OpenUI("PetEvoTip", null, false, UIType.NonPush) as PetEvoTip);
		this.petEvoTip.Init(talentIdByName);
		int num = 47 - 120 * talentIndexByName;
		num = Mathf.Max(num, -193);
		this.petEvoTip.get_transform().set_localPosition(new Vector3(-130f, (float)num));
	}

	protected virtual void OnUpTip(GameObject go)
	{
		if (this.petEvoTip)
		{
			this.petEvoTip.Show(false);
		}
	}

	protected int GetTalentIdByName(string name)
	{
		int talentIndexByName = this.GetTalentIndexByName(name);
		return this.cellIds.get_Item(talentIndexByName);
	}

	private int GetTalentIndexByName(string name)
	{
		Debug.LogError("GetTalentIndexByName index=" + name);
		return int.Parse(name);
	}

	private void SetCell(int cellIndex)
	{
		Transform child = base.get_transform().GetChild(cellIndex);
		int talentId = this.cellIds.get_Item(cellIndex);
		int skillLv = PetEvoGlobal.GetSkillLv(PetBasicUIViewModel.PetID, talentId);
		int nextTalentLv = skillLv + 1;
		this.SetImgIcon(child, talentId);
		this.SetButtons(child);
		this.SetOneSkill(cellIndex, talentId, skillLv, nextTalentLv);
	}

	private void SetImgIcon(Transform cell, int talentId)
	{
		Image component = cell.FindChild("imgNatural").GetComponent<Image>();
		ResourceManager.SetSprite(component, PetEvoGlobal.GetSprite(talentId));
		component.SetNativeSize();
	}

	private void SetButtons(Transform cell)
	{
		ButtonCustom component = cell.FindChild("btnLvUp").GetComponent<ButtonCustom>();
		PetInfo onePet = PetEvoGlobal.GetOnePet(PetBasicUIViewModel.PetID);
		if (onePet != null)
		{
			if (component.onClickCustom == null)
			{
				component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnLvUp);
			}
		}
		else
		{
			component.set_interactable(false);
			ImageColorMgr.SetImageColor(component.GetComponent<Image>(), true);
		}
	}

	protected void CanLvUp(Transform cell, int talentId, int nextTalentLv)
	{
		Image component = cell.FindChild("imgMaterial").GetComponent<Image>();
		Text component2 = cell.FindChild("texMaterial").GetComponent<Text>();
		ResourceManager.SetSprite(component, PetEvoGlobal.GetMaterialSprite(talentId, nextTalentLv));
		component2.set_text("x" + PetEvoGlobal.GetMaterialNum(talentId, nextTalentLv).ToString());
	}

	protected void CanNotLvUp(Transform cell, int talentId, int nextTalentLv)
	{
		Image component = cell.FindChild("imgMaterial").GetComponent<Image>();
		Text component2 = cell.FindChild("texMaterial").GetComponent<Text>();
		ResourceManager.SetSprite(component, PetEvoGlobal.GetMaterialSprite(talentId, nextTalentLv));
		component2.set_text("x" + PetEvoGlobal.GetMaterialNum(talentId, nextTalentLv).ToString());
	}

	protected void Locked(Transform cell, int talentId)
	{
		Image component = cell.FindChild("imgMaterial").GetComponent<Image>();
		Text component2 = cell.FindChild("texMaterial").GetComponent<Text>();
		ResourceManager.SetSprite(component, PetEvoGlobal.GetMaterialSprite(talentId, 1));
		component2.set_text("x" + PetEvoGlobal.GetMaterialNum(talentId, 1).ToString());
	}

	private string GetQualityColor(int quality)
	{
		return this.qualityColorMap.get_Item(quality);
	}

	protected void MaxLv(Transform cell)
	{
		ButtonCustom component = cell.FindChild("btnLvUp").GetComponent<ButtonCustom>();
		Image component2 = cell.FindChild("imgMaterial").GetComponent<Image>();
		Text component3 = cell.FindChild("texMaterial").GetComponent<Text>();
		component.get_gameObject().SetActive(false);
		component2.get_gameObject().SetActive(false);
		component3.get_gameObject().SetActive(false);
	}

	public void PlaySkillUpgrade(Transform transform)
	{
		FXSpineManager.Instance.PlaySpine(302, transform, "PetEvoSkillUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
