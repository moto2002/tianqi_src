using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCleanFinishItem : BaseUIBehaviour
{
	private Text TextGoldNum;

	private Text TextExpNum;

	private GridLayoutGroup Grid;

	private Text TextTitle;

	private Image ImageGold;

	private Image ImageExp;

	private List<GameObject> listItem = new List<GameObject>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextGoldNum = base.FindTransform("TextGoldNum").GetComponent<Text>();
		this.TextExpNum = base.FindTransform("TextExpNum").GetComponent<Text>();
		this.Grid = base.FindTransform("Grid").GetComponent<GridLayoutGroup>();
		this.TextTitle = base.FindTransform("TextTitle").GetComponent<Text>();
		this.ImageGold = base.FindTransform("ImageGold").GetComponent<Image>();
		this.ImageExp = base.FindTransform("ImageExp").GetComponent<Image>();
	}

	private void OnClickInstanceRewardItem(GameObject obj)
	{
		ItemTipUIViewModel.ShowItem(int.Parse(obj.get_name()), null);
	}

	public void SetRewardItems(List<DropItem> listDrops, int time)
	{
		string text = GameDataUtils.GetChineseContent(505124, false);
		text = text.Replace("xx", time.ToString());
		this.TextTitle.set_text(text);
		this.TextExpNum.set_text("0");
		this.TextGoldNum.set_text("0");
		for (int i = 0; i < listDrops.get_Count(); i++)
		{
			DropItem dropItem = listDrops.get_Item(i);
			if (dropItem.typeId == 1)
			{
				ResourceManager.SetSprite(this.ImageExp, GameDataUtils.GetIcon(DataReader<Items>.Get(dropItem.typeId).littleIcon));
				this.TextExpNum.set_text("+" + dropItem.count.ToString());
			}
			else if (dropItem.typeId == 2)
			{
				ResourceManager.SetSprite(this.ImageGold, GameDataUtils.GetIcon(DataReader<Items>.Get(dropItem.typeId).littleIcon));
				this.TextGoldNum.set_text("+" + dropItem.count.ToString());
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstanceRewardItem");
				instantiate2Prefab.get_transform().FindChild("Text").GetComponent<Text>().set_text(dropItem.count.ToString());
				Items items = DataReader<Items>.Get(dropItem.typeId);
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetItemIcon(items.id));
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageFrame").GetComponent<Image>(), GameDataUtils.GetItemFrame(items.id));
				instantiate2Prefab.get_transform().SetParent(this.Grid.get_transform());
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickInstanceRewardItem);
				instantiate2Prefab.set_name(dropItem.typeId.ToString());
				this.listItem.Add(instantiate2Prefab);
			}
		}
	}
}
