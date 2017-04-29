using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public static class ItemShow
{
	public static GameObject ShowItem(Transform parent, int itemId, long count = -1L, bool showName = false, Transform ItemTipRoot = null, int depthValue = 2001)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ItemShow");
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.get_transform().SetParent(parent);
		instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
		instantiate2Prefab.get_transform().set_localPosition(Vector3.get_zero());
		instantiate2Prefab.get_transform().set_localEulerAngles(Vector3.get_zero());
		ItemShow.SetItem(instantiate2Prefab, itemId, count, showName, ItemTipRoot, 2001);
		return instantiate2Prefab;
	}

	public static void SetItem(GameObject goItem, int itemId, long count = -1L, bool showName = false, Transform ItemTipRoot = null, int depthValue = 2001)
	{
		ButtonCustom expr_1E = goItem.GetComponent<ButtonCustom>();
		expr_1E.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_1E.onClickCustom, delegate(GameObject v)
		{
			ItemTipUIViewModel.ShowItem(itemId, ItemTipRoot);
		});
		Items items = DataReader<Items>.Get(itemId);
		int quality = 1;
		if (items != null)
		{
			quality = items.color;
		}
		ResourceManager.SetSprite(goItem.get_transform().FindChild("ImageBackground").GetComponent<Image>(), ResourceManager.GetCodeSprite(GameDataUtils.GetItemFrameName(quality)));
		if (items != null)
		{
			ResourceManager.SetSprite(goItem.get_transform().FindChild("Icon").GetComponent<Image>(), GameDataUtils.GetIcon(items.icon));
		}
		if (count == -1L)
		{
			goItem.get_transform().FindChild("Num").get_gameObject().SetActive(false);
		}
		else
		{
			goItem.get_transform().FindChild("Num").GetComponent<Text>().set_text(Utils.GetItemNum(itemId, count).ToString());
		}
		if (showName && items != null)
		{
			goItem.get_transform().FindChild("Name").GetComponent<Text>().set_text(GameDataUtils.GetItemName(itemId, false, 0L));
		}
		else
		{
			goItem.get_transform().FindChild("Name").get_gameObject().SetActive(false);
		}
		if (items != null && items.step > 0)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(505023, false), items.step);
			goItem.get_transform().FindChild("EquipStep").get_gameObject().SetActive(true);
			goItem.get_transform().FindChild("EquipStep").FindChild("EquipStepText").GetComponent<Text>().set_text(text);
		}
		else
		{
			goItem.get_transform().FindChild("EquipStep").get_gameObject().SetActive(false);
		}
		if (items == null || items.gogok <= 0)
		{
			goItem.get_transform().FindChild("ItemExcellentAttrIconList").get_gameObject().SetActive(false);
		}
		else
		{
			goItem.get_transform().FindChild("ItemExcellentAttrIconList").get_gameObject().SetActive(true);
			goItem.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(items.gogok >= 1);
			goItem.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(items.gogok >= 2);
			goItem.get_transform().FindChild("ItemExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(items.gogok >= 3);
		}
		if (items != null && items.tab == 2 && items.color >= 4)
		{
			int gogokNum = 0;
			Transform fxParentTrans = goItem.get_transform().FindChild("Icon");
			EquipGlobal.GetEquipIconFX(items.id, gogokNum, fxParentTrans, "ItemShow", depthValue, false);
			DepthOfUI depthOfUI = goItem.get_transform().FindChild("EquipStep").GetComponent<DepthOfUI>();
			if (depthOfUI == null)
			{
				depthOfUI = goItem.get_transform().FindChild("EquipStep").get_gameObject().AddComponent<DepthOfUI>();
			}
			depthOfUI.SortingOrder = depthValue + 1;
		}
	}

	public static GameObject ShowItemSmall(Transform parent, int itemId, long count = -1L)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ItemShowSmall");
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.get_transform().SetParent(parent);
		instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
		instantiate2Prefab.get_transform().set_localPosition(Vector3.get_zero());
		instantiate2Prefab.get_transform().set_localEulerAngles(Vector3.get_zero());
		ButtonCustom expr_61 = instantiate2Prefab.GetComponent<ButtonCustom>();
		expr_61.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_61.onClickCustom, delegate(GameObject v)
		{
			ItemTipUIViewModel.ShowItem(itemId, null);
		});
		ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("Icon").GetComponent<Image>(), GameDataUtils.GetIcon(DataReader<Items>.Get(itemId).icon));
		if (count == -1L)
		{
			instantiate2Prefab.get_transform().FindChild("Num").get_gameObject().SetActive(false);
		}
		else
		{
			instantiate2Prefab.get_transform().FindChild("Num").GetComponent<Text>().set_text(count.ToString());
		}
		return instantiate2Prefab;
	}
}
