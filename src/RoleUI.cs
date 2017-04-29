using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RoleUI : UIBase
{
	private ButtonCustom ItemRoleButton;

	private ButtonCustom ItemEquipButton;

	private ButtonCustom ItemPetButton;

	private ButtonCustom ItemSkillButton;

	private ButtonCustom ItemBackPackButton;

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.ItemRoleButton = base.FindTransform("ItemRoleButton").GetComponent<ButtonCustom>();
		this.ItemEquipButton = base.FindTransform("ItemRefiningButton").GetComponent<ButtonCustom>();
		this.ItemPetButton = base.FindTransform("ItemPetButton").GetComponent<ButtonCustom>();
		this.ItemSkillButton = base.FindTransform("ItemSkillButton").GetComponent<ButtonCustom>();
		this.ItemBackPackButton = base.FindTransform("ItemBackPackButton").GetComponent<ButtonCustom>();
		this.ItemRoleButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRole);
		this.ItemEquipButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEquip);
		this.ItemPetButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPet);
		this.ItemBackPackButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBackPack);
		this.ItemSkillButton.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkill);
	}

	protected override void OnEnable()
	{
		this.SetRoleButtonImage();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110027), string.Empty, delegate
		{
			this.Show(false);
			SoundManager.SetBGMFade(true);
		}, false);
		TimerHeap.AddTimer(300u, 0, delegate
		{
			this.CheckBadge();
		});
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	public void CheckBadge()
	{
		BadgeManager.Instance.ResetAllBadgeData();
		this.ItemRoleButton.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Actor));
		this.ItemBackPackButton.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Backpack));
		this.ItemSkillButton.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Skill));
		this.ItemPetButton.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Pet));
		this.ItemEquipButton.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BadgeManager.Instance.GetBadgeTypeIsShow(BadgeType.Equip));
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickRole(GameObject sender)
	{
		LinkNavigationManager.OpenActorUI(null);
	}

	private void OnClickEquip(GameObject sender)
	{
		LinkNavigationManager.OpenEquipStrengthenUI(EquipLibType.ELT.Weapon, null);
	}

	private void OnClickPet(GameObject sender)
	{
		LinkNavigationManager.OpenPetUI(null);
	}

	private void OnClickElement(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemClickOpen(2, 0, true))
		{
			return;
		}
		ElementUI elementUI = UIManagerControl.Instance.OpenUI("ElementUI", null, true, UIType.FullScreen) as ElementUI;
		elementUI.ResetUI();
	}

	private void OnClickRefining(GameObject sender)
	{
	}

	private void OnClickBackPack(GameObject sender)
	{
		LinkNavigationManager.OpenBackpackUI(null);
	}

	private void OnClickSkill(GameObject sender)
	{
		LinkNavigationManager.OpenSkillUI(null);
	}

	public void SetRoleButtonImage()
	{
		ResourceManager.SetSprite(this.ItemRoleButton.get_transform().FindChild("ImgeIcon").GetComponent<Image>(), UIUtils.GetRoleSelfMenuIcon());
	}
}
