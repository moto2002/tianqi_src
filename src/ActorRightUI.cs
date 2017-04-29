using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ActorRightUI : UIBase
{
	private Text TextTitleBase;

	private Text TextPowerTitle;

	private Text TextTitleElement;

	private Transform ElementGround;

	private Transform ElementFire;

	private Transform ElementWater;

	private Transform ElementLighting;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextTitleBase = base.FindTransform("TextTitleBase").GetComponent<Text>();
		this.TextPowerTitle = base.FindTransform("TextPowerTitle").GetComponent<Text>();
		this.TextTitleElement = base.FindTransform("TextTitleElement").GetComponent<Text>();
		this.ElementGround = base.FindTransform("ElementGround");
		this.ElementFire = base.FindTransform("ElementFire");
		this.ElementWater = base.FindTransform("ElementWater");
		this.ElementLighting = base.FindTransform("ElementLighting");
		this.TextTitleBase.set_text(GameDataUtils.GetChineseContent(510013, false));
		this.TextTitleElement.set_text(GameDataUtils.GetChineseContent(510007, false));
		this.TextPowerTitle.set_text(GameDataUtils.GetChineseContent(510014, false));
		this.ElementGround.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElementGround);
		this.ElementFire.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElementFire);
		this.ElementWater.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElementWater);
		this.ElementLighting.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElementLighting);
	}

	protected override void OnEnable()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.OnEnable();
		this.ResetData();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.OnGetRoleAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.OnGetRoleAttrChangedNty));
	}

	private void OnClickElementGround(GameObject sender)
	{
		this.GoToElement(1);
	}

	private void OnClickElementFire(GameObject sender)
	{
		this.GoToElement(2);
	}

	private void OnClickElementWater(GameObject sender)
	{
		this.GoToElement(3);
	}

	private void OnClickElementLighting(GameObject sender)
	{
		this.GoToElement(4);
	}

	private void GoToElement(int element)
	{
		ElementUI elementUI = UIManagerControl.Instance.OpenUI("ElementUI", UINodesManager.NormalUIRoot, true, UIType.FullScreen) as ElementUI;
		elementUI.ResetUI();
		elementUI.ResetTypeUI(element, 1);
	}

	public void ResetData()
	{
	}

	private void OnGetRoleAttrChangedNty()
	{
		this.ResetData();
	}
}
