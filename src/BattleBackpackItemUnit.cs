using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleBackpackItemUnit : BaseUIBehaviour
{
	protected GameObject BattleBackpackNullDetail;

	protected Text BattleBackpackNullDetailText;

	protected GameObject BattleBackpackItem;

	protected Image BattleBackpackItemFrame;

	protected Image BattleBackpackItemIcon;

	protected Text BattleBackpackItemNum;

	protected GameObject BattleBackpackItemStep;

	protected Text BattleBackpackItemStepNum;

	protected bool unitIsShowItem;

	protected int unitItemID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BattleBackpackNullDetail = base.FindTransform("BattleBackpackNullDetail").get_gameObject();
		this.BattleBackpackNullDetailText = base.FindTransform("BattleBackpackNullDetailText").GetComponent<Text>();
		this.BattleBackpackItem = base.FindTransform("BattleBackpackItem").get_gameObject();
		this.BattleBackpackItemFrame = base.FindTransform("BattleBackpackItemFrame").GetComponent<Image>();
		this.BattleBackpackItemIcon = base.FindTransform("BattleBackpackItemIcon").GetComponent<Image>();
		this.BattleBackpackItemNum = base.FindTransform("BattleBackpackItemNum").GetComponent<Text>();
		this.BattleBackpackItemStep = base.FindTransform("BattleBackpackItemStep").get_gameObject();
		this.BattleBackpackItemStepNum = base.FindTransform("BattleBackpackItemStepNum").GetComponent<Text>();
		this.BattleBackpackItem.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickItem);
	}

	public void SetData(int index, bool isShowItem, int itemID, long num, string text)
	{
		this.unitIsShowItem = isShowItem;
		this.unitItemID = itemID;
		this.ShowItem(isShowItem);
		this.SetItem(itemID, num);
		this.SetNullDetailText(text);
	}

	public void ShowItem(bool isShow)
	{
		this.BattleBackpackNullDetail.SetActive(!isShow);
		this.BattleBackpackItem.SetActive(isShow);
	}

	public void SetItem(int itemID, long num)
	{
		if (DataReader<Items>.Contains(itemID))
		{
			Items items = DataReader<Items>.Get(itemID);
			ResourceManager.SetSprite(this.BattleBackpackItemFrame, GameDataUtils.GetItemFrame(items));
			ResourceManager.SetSprite(this.BattleBackpackItemIcon, GameDataUtils.GetIcon(items.icon));
			this.BattleBackpackItemNum.set_text((num != 0L) ? num.ToString() : string.Empty);
			if (items.step > 0)
			{
				this.BattleBackpackItemStep.SetActive(true);
				this.BattleBackpackItemStepNum.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
			}
			else
			{
				this.BattleBackpackItemStep.SetActive(false);
			}
		}
		else
		{
			ResourceManager.SetCodeSprite(this.BattleBackpackItemFrame, "frame_icon_white");
			ResourceManager.SetCodeSprite(this.BattleBackpackItemIcon, string.Empty);
			this.BattleBackpackItemNum.set_text(string.Empty);
			this.BattleBackpackItemStep.SetActive(false);
		}
	}

	public void SetNullDetailText(string text)
	{
		this.BattleBackpackNullDetailText.set_text(text);
	}

	protected void OnClickItem(GameObject go)
	{
		if (this.unitItemID == 0)
		{
			return;
		}
		if (!DataReader<Items>.Contains(this.unitItemID))
		{
			return;
		}
		ItemTipUIViewModel.ShowItem(this.unitItemID, null);
	}
}
