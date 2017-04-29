using GameData;
using Package;
using System;

public class BackpackTools
{
	public static BackpackObservableItem GetBackpackObservableItem(Goods good, Action<BackpackObservableItem> action2ClickItem, int mode = 1)
	{
		BackpackObservableItem backpackObservableItem = new BackpackObservableItem();
		if (good != null)
		{
			backpackObservableItem.ItemRootNullOn = false;
			backpackObservableItem.ItemRootOn = true;
			backpackObservableItem.SetSelectedMode(mode);
			backpackObservableItem.OnClickItemAction = action2ClickItem;
			backpackObservableItem.ItemFlag = false;
			backpackObservableItem.SetIsSelected(false);
			backpackObservableItem.id = good.GetLongId();
			backpackObservableItem.ItemId = good.LocalItem.id;
			backpackObservableItem.ItemIcon = GameDataUtils.GetIcon(good.LocalItem.icon);
			backpackObservableItem.ItemNum = BackpackManager.Instance.OnGetGoodCount(good.GetLongId()).ToString();
			backpackObservableItem.ItemStepOn = (good.GetItem().step > 0);
			backpackObservableItem.ItemStepNum = string.Format(GameDataUtils.GetChineseContent(505023, false), good.GetItem().step);
			bool redPointOn = false;
			if ((good.LocalItem.function == 1 || good.LocalItem.function == 2 || good.LocalItem.function == 3) && good.LocalItem.secondType == 11 && EntityWorld.Instance.EntSelf != null && good.LocalItem.minLv <= EntityWorld.Instance.EntSelf.Lv)
			{
				redPointOn = true;
			}
			backpackObservableItem.RedPointOn = redPointOn;
			backpackObservableItem.EquipIsBinding = false;
			EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(good.GetLongId());
			if (equipSimpleInfoByEquipID != null && equipSimpleInfoByEquipID.suitId > 0)
			{
				TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipSimpleInfoByEquipID.equipId);
				if (equipForgeCfgData != null)
				{
					backpackObservableItem.ItemFrame = ResourceManager.GetIconSprite(equipForgeCfgData.frame);
				}
			}
			if (equipSimpleInfoByEquipID != null)
			{
				backpackObservableItem.EquipIsBinding = equipSimpleInfoByEquipID.binding;
			}
			int excellentCount;
			if (good.GetItem().tab == 2)
			{
				excellentCount = EquipGlobal.GetExcellentAttrsCountByColor(good.GetLongId(), 1f);
			}
			else
			{
				excellentCount = good.GetItem().gogok;
			}
			backpackObservableItem.ExcellentCount = excellentCount;
		}
		else
		{
			backpackObservableItem.ItemRootNullOn = true;
			backpackObservableItem.ItemRootOn = false;
			backpackObservableItem.SetSelectedMode(mode);
			backpackObservableItem.OnClickItemAction = action2ClickItem;
		}
		return backpackObservableItem;
	}
}
