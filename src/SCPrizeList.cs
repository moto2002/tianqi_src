using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SCPrizeList : BaseUIBehaviour
{
	private Items dataIt;

	public Image goldIcon;

	public Text goldNum;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void OnClickItem(GameObject go)
	{
		ItemTipUIViewModel.ShowItem(this.dataIt.id, null);
	}

	public void UpdateItem(int item, int num)
	{
		this.dataIt = DataReader<Items>.Get(item);
		ResourceManager.SetSprite(this.goldIcon, GameDataUtils.GetIcon(this.dataIt.icon));
		this.goldNum.set_text(num.ToString());
	}
}
