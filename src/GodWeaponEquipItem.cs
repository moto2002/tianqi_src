using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GodWeaponEquipItem : MonoBehaviour
{
	protected int mItemId;

	protected void Start()
	{
		ButtonCustom expr_06 = base.GetComponent<ButtonCustom>();
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickRewardItem));
	}

	protected void OnClickRewardItem(GameObject go)
	{
		ItemTipUIViewModel.ShowItem(this.mItemId, null);
	}

	public void SetItem(int itemId, bool isHave)
	{
		this.mItemId = itemId;
		Image component = base.get_transform().FindChild("RewardItemIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemIcon(this.mItemId));
		component.SetNativeSize();
		ResourceManager.SetSprite(base.get_transform().FindChild("RewardItemFrame").GetComponent<Image>(), GameDataUtils.GetItemFrame(this.mItemId));
		base.get_transform().FindChild("Select").get_gameObject().SetActive(isHave);
		Items items = DataReader<Items>.Get(itemId);
		if (items == null || items.step <= 0)
		{
			base.get_transform().FindChild("ItemStep").get_gameObject().SetActive(false);
		}
		else
		{
			base.get_transform().FindChild("ItemStep").get_gameObject().SetActive(true);
			base.get_transform().FindChild("ItemStep").FindChild("ItemStepText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
	}
}
