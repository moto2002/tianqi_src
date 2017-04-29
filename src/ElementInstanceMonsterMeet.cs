using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceMonsterMeet : UIBase
{
	private ButtonCustom BtnFight;

	private Text TextMineName;

	private Text TextDes;

	public string blockID;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextDes = base.FindTransform("TextDes").GetComponent<Text>();
		this.BtnFight = base.FindTransform("BtnFight").GetComponent<ButtonCustom>();
		this.TextMineName = base.FindTransform("TextMineName").GetComponent<Text>();
		this.BtnFight.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnFight);
		this.TextMineName.set_text(GameDataUtils.GetChineseContent(502266, false));
	}

	private void Start()
	{
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.ElementCopy, base.get_transform(), 1);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.GetComponent<RectTransform>().SetAsLastSibling();
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.ElementCopy, base.get_transform(), 1);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
	}

	public void RefreshUI(int typeID)
	{
		string text = GameDataUtils.GetChineseContent(502315, false);
		text = text.Replace("{s1}", DataReader<YGuaiWuKu>.Get(typeID).Name);
		this.TextDes.set_text(text);
	}

	private void OnClickBtnOut(GameObject sender)
	{
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickBtnFight(GameObject sender)
	{
		if (ElementInstanceManager.Instance.m_elementCopyLoginPush.exploreEnergy <= 0)
		{
			ElementInstanceManager.Instance.BuyRecovery();
			return;
		}
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		ElementInstanceManager.Instance.SendStartToFightReq(this.blockID, delegate
		{
		});
	}
}
