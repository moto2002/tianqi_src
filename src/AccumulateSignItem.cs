using System;
using UnityEngine;
using UnityEngine.UI;

public class AccumulateSignItem : BaseBehaviour
{
	private Image imgIcon;

	private GameObject imgGet;

	private GameObject imgHaveGet;

	public Text textName;

	public Text textCount;

	public Text textDay;

	public int itemId;

	public Transform SItem;

	public int effectId;

	public int effectId2;

	public ButtonCustom IconButtonCustom;

	public int IconItemId;

	private void Awake()
	{
		this.SItem = base.get_transform().FindChild("Abg").FindChild("SItem");
		this.textDay = this.SItem.FindChild("DayText").GetComponent<Text>();
		this.textName = this.SItem.FindChild("SItemName").GetComponent<Text>();
		this.textCount = this.SItem.FindChild("SItemIcon").FindChild("STextNum").GetComponent<Text>();
		this.imgIcon = this.SItem.FindChild("SItemIcon").FindChild("SImageIcon").GetComponent<Image>();
		this.SItem.FindChild("SItemIcon").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDetal);
	}

	private void OnEnable()
	{
		this.effectId = FXSpineManager.Instance.PlaySpine(602, this.SItem.FindChild("SItemIcon").get_transform(), "SignInUI", 3001, null, "UI", 0f, 0f, 1.1f, 1.1f, false, FXMaskLayer.MaskState.None);
	}

	private void OnDisable()
	{
		FXSpineManager.Instance.DeleteSpine(this.effectId, true);
		FXSpineManager.Instance.DeleteSpine(this.effectId2, true);
	}

	private void OnClickBtnDetal(GameObject go)
	{
		ItemTipUIViewModel.ShowItem(this.IconItemId, null);
	}

	private void OnClickGet(GameObject go)
	{
		SignInManager.Instance.SendAcceptMonthTotalReq(this.itemId);
	}

	public void UpdateItem(int indexId, int dropId, int title, int flag)
	{
		XDict<int, long> rewardItems = FirstPayManager.Instance.GetRewardItems(dropId);
		int num;
		long num2;
		if (rewardItems != null && rewardItems.Count > 0)
		{
			num = rewardItems.ElementKeyAt(0);
			num2 = rewardItems.ElementValueAt(0);
		}
		else
		{
			num = 41000;
			num2 = 30L;
		}
		ResourceManager.SetSprite(this.imgIcon, GameDataUtils.GetItemIcon(num));
		this.imgIcon.SetNativeSize();
		this.textDay.set_text(GameDataUtils.GetChineseContent(title, false));
		this.textName.set_text(GameDataUtils.GetItemName(num, false, 0L));
		this.textCount.set_text(num2.ToString());
		this.itemId = indexId;
		this.IconItemId = num;
		this.UpdateItemState(flag);
		base.get_transform().FindChild("BtnRect").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
	}

	public void UpdateItemState(int flag)
	{
		switch (flag)
		{
		case 0:
			this.SItem.FindChild("SGetImage").get_gameObject().SetActive(false);
			this.SItem.FindChild("SHaveGetImage").get_gameObject().SetActive(false);
			break;
		case 1:
			this.SItem.FindChild("SGetImage").get_gameObject().SetActive(true);
			this.SItem.FindChild("SHaveGetImage").get_gameObject().SetActive(false);
			FXSpineManager.Instance.DeleteSpine(this.effectId2, true);
			this.effectId2 = FXSpineManager.Instance.PlaySpine(603, base.get_transform().FindChild("Abg").get_transform(), "SignInUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			break;
		case 2:
			this.SItem.FindChild("SGetImage").get_gameObject().SetActive(false);
			this.SItem.FindChild("SHaveGetImage").get_gameObject().SetActive(true);
			FXSpineManager.Instance.DeleteSpine(this.effectId2, true);
			break;
		}
	}
}
