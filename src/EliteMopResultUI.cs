using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteMopResultUI : UIBase
{
	private List<Transform> itemShows;

	private Transform TitleFx;

	private int FxID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.itemShows = new List<Transform>();
		for (int i = 0; i < 10; i++)
		{
			Transform transform = base.FindTransform("ItemShow" + (i + 1));
			if (transform != null)
			{
				this.itemShows.Add(transform);
			}
		}
		this.TitleFx = base.FindTransform(string.Empty);
		base.FindTransform("ButtonConfirm").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSure);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	private void OnClickSure(GameObject go)
	{
		this.Show(false);
	}

	public void Refresh(List<int> ItemIDs, List<int> ItemCounts)
	{
		if (this.FxID != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.FxID, true);
		}
		FXSpineManager.Instance.ReplaySpine(0, 1104, this.TitleFx, "EliteMopResultUI", 3001, delegate
		{
			this.FxID = FXSpineManager.Instance.ReplaySpine(0, 1105, this.TitleFx, "EliteMopResultUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			int count = ItemIDs.get_Count();
			if (this.itemShows.get_Count() >= count)
			{
				int i;
				for (i = 0; i < count; i++)
				{
					this.itemShows.get_Item(i).get_gameObject().SetActive(true);
				}
				while (i < this.itemShows.get_Count())
				{
					this.itemShows.get_Item(i).get_gameObject().SetActive(false);
					i++;
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (j >= this.itemShows.get_Count())
				{
					break;
				}
				this.SetItem(j, ItemIDs.get_Item(j), ItemCounts.get_Item(j));
			}
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SetItem(int index, int itemID, int Count)
	{
		Transform transform = base.FindTransform("ItemShow" + (index + 1));
		if (transform == null)
		{
			return;
		}
		if (DataReader<Items>.Get(itemID) == null)
		{
			return;
		}
		ResourceManager.SetSprite(transform.FindChild("ImageBackground").GetComponent<Image>(), GameDataUtils.GetItemFrame(itemID));
		ResourceManager.SetSprite(transform.FindChild("Icon").GetComponent<Image>(), GameDataUtils.GetItemIcon(itemID));
		transform.FindChild("Num").GetComponent<Text>().set_text(Count.ToString());
	}
}
