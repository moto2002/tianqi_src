using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
	public const string UNUSED = "Unused";

	protected int equip_fxID;

	protected int m_itemId;

	protected long m_uId;

	protected int fx_id;

	protected virtual void Start()
	{
		ButtonCustom expr_06 = base.GetComponent<ButtonCustom>();
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickRewardItem));
	}

	protected virtual void OnDisable()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_id, true);
		FXSpineManager.Instance.DeleteSpine(this.equip_fxID, true);
	}

	protected virtual void OnClickRewardItem(GameObject go)
	{
		if (DataReader<Items>.Contains(this.m_itemId))
		{
			Items items = DataReader<Items>.Get(this.m_itemId);
			if (items.tab == 2)
			{
				ItemTipUIViewModel.ShowEquipItem(this.m_itemId, this.m_uId, null);
			}
			else
			{
				ItemTipUIViewModel.ShowItem(this.m_itemId, null);
			}
		}
	}

	public void SetRewardItem(int itemId, long num = -1L, long uid = 0L)
	{
		this.m_itemId = itemId;
		this.m_uId = uid;
		Image component = base.get_transform().FindChild("RewardItemIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemIcon(this.m_itemId));
		ResourceManager.SetSprite(base.get_transform().FindChild("RewardItemFrame").GetComponent<Image>(), GameDataUtils.GetItemFrame(this.m_itemId));
		if (num <= 0L)
		{
			base.get_transform().FindChild("RewardItemText").GetComponent<Text>().set_text(string.Empty);
		}
		else
		{
			base.get_transform().FindChild("RewardItemText").GetComponent<Text>().set_text(Utils.GetItemNum(itemId, num));
		}
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
		if (items == null || items.gogok <= 0)
		{
			base.get_transform().FindChild("ItemExcellentAttrIconList").get_gameObject().SetActive(false);
		}
		else
		{
			base.get_transform().FindChild("ItemExcellentAttrIconList").get_gameObject().SetActive(true);
			base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(items.gogok >= 1);
			base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(items.gogok >= 2);
			base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(items.gogok >= 3);
		}
		if (items != null && items.tab == 2)
		{
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(uid, 1f);
			if (excellentAttrsCountByColor > 0)
			{
				base.get_transform().FindChild("ItemExcellentAttrIconList").get_gameObject().SetActive(true);
				base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 1);
				base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 2);
				base.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 3);
			}
		}
	}

	public void StartSpine(string baseui, int depthvalue)
	{
		FXSpineManager.Instance.ReplaySpine(this.fx_id, 1107, base.get_transform(), baseui, depthvalue, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
