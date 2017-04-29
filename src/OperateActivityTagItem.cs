using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OperateActivityTagItem : BaseUIBehaviour
{
	private GameObject m_goSelect;

	private Image m_spSelectIcon;

	private Text m_lblSelectText;

	private GameObject m_goUnselect;

	private Image m_spUnselectIcon;

	private Text m_lblUnselectText;

	private GameObject m_goTips;

	private GameObject m_goNew;

	private ButtonCustom Btn;

	public void AwakeSelf()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_goSelect = base.FindTransform("Select").get_gameObject();
		this.m_spSelectIcon = base.FindTransform("SelectIcon").GetComponent<Image>();
		this.m_lblSelectText = base.FindTransform("SelectText").GetComponent<Text>();
		this.m_goUnselect = base.FindTransform("Unselect").get_gameObject();
		this.m_spUnselectIcon = base.FindTransform("UnselectIcon").GetComponent<Image>();
		this.m_lblUnselectText = base.FindTransform("UnselectText").GetComponent<Text>();
		this.m_goTips = base.FindTransform("Tips").get_gameObject();
		this.m_goNew = base.FindTransform("New").get_gameObject();
		ButtonCustom expr_BC = base.GetComponent<ButtonCustom>();
		expr_BC.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_BC.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClick));
	}

	private void OnClick(GameObject go)
	{
		OperateActivityUI operateActivityUI = UIManagerControl.Instance.GetUIIfExist("OperateActivityUI") as OperateActivityUI;
		operateActivityUI.OpenActivityUI(base.get_gameObject());
		this.m_goNew.get_gameObject().SetActive(false);
	}

	public void UpdateItem(ActivityInfo info)
	{
		base.get_gameObject().set_name(info.typeId.ToString());
		ResourceManager.SetSprite(this.m_spSelectIcon, GameDataUtils.GetIcon(DataReader<HuoDongMuLu>.Get(info.typeId).icon));
		this.m_lblSelectText.set_text(GameDataUtils.GetChineseContent(DataReader<HuoDongMuLu>.Get(info.typeId).nameId, false));
		ResourceManager.SetSprite(this.m_spUnselectIcon.GetComponent<Image>(), ResourceManager.GetIconSprite(DataReader<HuoDongMuLu>.Get(info.typeId).icon + "2"));
		this.m_lblUnselectText.set_text(GameDataUtils.GetChineseContent(DataReader<HuoDongMuLu>.Get(info.typeId).nameId, false));
		this.m_goNew.get_gameObject().SetActive(!info.firstOpen);
		this.UpdateTips(info.typeId);
	}

	public void SetSelected(bool isSelected)
	{
		this.m_goSelect.SetActive(isSelected);
		this.m_goUnselect.SetActive(!isSelected);
	}

	public void UpdateTips(int typeId)
	{
		this.m_goTips.get_gameObject().SetActive(OperateActivityManager.Instance.GetActivityRedTips(typeId));
	}
}
