using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpFloatTipsUI : UIBase
{
	protected Transform pool;

	protected List<ExpFloatTipsUIItem> itemCache = new List<ExpFloatTipsUIItem>();

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isInterruptStick = false;
	}

	private void Awake()
	{
		this.pool = base.FindTransform("Pool");
	}

	protected override void OnEnable()
	{
		for (int i = 0; i < this.itemCache.get_Count(); i++)
		{
			ExpFloatTipsUIItem expFloatTipsUIItem = this.itemCache.get_Item(i);
			if (!expFloatTipsUIItem.Unused)
			{
				expFloatTipsUIItem.Unused = true;
				expFloatTipsUIItem.GetComponent<CanvasGroup>().set_alpha(0f);
				expFloatTipsUIItem.get_gameObject().SetActive(false);
			}
		}
	}

	public void ShowText(string text, float duration, float delay)
	{
		ExpFloatTipsUIItem expFloatTipsUIItem = this.itemCache.Find((ExpFloatTipsUIItem a) => a.Unused == base.get_transform());
		if (expFloatTipsUIItem == null)
		{
			expFloatTipsUIItem = ResourceManager.GetInstantiate2Prefab("ExpFloatTipsUIItem").GetComponent<ExpFloatTipsUIItem>();
			expFloatTipsUIItem.get_transform().SetParent(this.pool);
			expFloatTipsUIItem.get_transform().set_localScale(Vector3.get_one());
			expFloatTipsUIItem.get_transform().set_localPosition(Vector3.get_zero());
			this.itemCache.Add(expFloatTipsUIItem);
		}
		expFloatTipsUIItem.ShowText(text, duration, delay);
	}
}
