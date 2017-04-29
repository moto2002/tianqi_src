using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StrongerUiItem : BaseUIBehaviour
{
	private Image iconImg;

	private Text nameText;

	private Text nameDescText;

	private Text progressNumText;

	private Text progressDescText;

	private Image progressImage;

	private ButtonCustom gotoBtn;

	private int systemID;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.iconImg = base.FindTransform("Icon").GetComponent<Image>();
		this.nameText = base.FindTransform("Name").GetComponent<Text>();
		this.nameDescText = base.FindTransform("NameDesc").GetComponent<Text>();
		this.progressNumText = base.FindTransform("TextNum").GetComponent<Text>();
		this.progressDescText = base.FindTransform("TextDesc").GetComponent<Text>();
		this.progressImage = base.FindTransform("ImageProgress").GetComponent<Image>();
		this.gotoBtn = base.FindTransform("Button").GetComponent<ButtonCustom>();
		this.gotoBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGoTo);
		this.nameDescText.set_text(string.Empty);
		this.isInit = true;
	}

	private void OnClickGoTo(GameObject go)
	{
		LinkNavigationManager.SystemLink(this.systemID, true, null);
		UIManagerControl.Instance.HideUI("StrongerUI");
	}

	public void UpdateItem(StrongerInfoData info)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.systemID = info.SystemID;
		BianQiangJieMianPeiZhi bianQiangJieMianPeiZhi = DataReader<BianQiangJieMianPeiZhi>.Get((int)info.Type);
		DangQianDengJiLiLunZhanLi dangQianDengJiLiLunZhanLi = DataReader<DangQianDengJiLiLunZhanLi>.Get(EntityWorld.Instance.EntSelf.Lv);
		if (bianQiangJieMianPeiZhi == null)
		{
			return;
		}
		if (dangQianDengJiLiLunZhanLi == null)
		{
			return;
		}
		if (bianQiangJieMianPeiZhi.icon > 0)
		{
			ResourceManager.SetSprite(this.iconImg, GameDataUtils.GetIcon(bianQiangJieMianPeiZhi.icon));
		}
		this.nameText.set_text(GameDataUtils.GetChineseContent(bianQiangJieMianPeiZhi.name2, false));
		this.nameDescText.set_text(GameDataUtils.GetChineseContent(bianQiangJieMianPeiZhi.name, false) + "：" + StrongerManager.Instance.GetStandardFightingByStrongerType(info.Type));
		float num = StrongerManager.Instance.GetProgressByStrongerType(info.Type);
		if (num >= 1f)
		{
			num = 1f;
		}
		this.progressNumText.set_text(StrongerManager.Instance.GetFightingByStrongerType(info.Type).ToString());
		if ((double)num < 0.31)
		{
			ResourceManager.SetSprite(this.progressImage, ResourceManager.GetIconSprite("x_schedule02"));
		}
		else if ((double)num < 0.71)
		{
			ResourceManager.SetSprite(this.progressImage, ResourceManager.GetIconSprite("x_schedule01"));
		}
		else
		{
			ResourceManager.SetSprite(this.progressImage, ResourceManager.GetIconSprite("x_schedule03"));
		}
		this.progressImage.set_fillAmount(num);
		this.progressDescText.set_text(StrongerManager.Instance.GetProgressDescByStrongerType(info.Type));
	}
}
