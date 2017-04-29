using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipStoneItem : BaseUIBehaviour
{
	public int ItemID;

	private Image m_iconImg;

	private Text m_needCostNum;

	private GameObject m_selectedImg;

	protected bool selected;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.ShowSelect(value);
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_iconImg = base.FindTransform("IConImg").GetComponent<Image>();
		this.m_needCostNum = base.FindTransform("CostNum").GetComponent<Text>();
		this.m_selectedImg = base.FindTransform("SelectImg").get_gameObject();
	}

	private void ShowSelect(bool isShow)
	{
		this.selected = isShow;
		this.m_selectedImg.SetActive(isShow);
	}

	public void SetItemData(int itemID)
	{
		this.ItemID = itemID;
		Items items = DataReader<Items>.Get(itemID);
		if (items == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_iconImg, GameDataUtils.GetIcon(items.icon));
		long num = BackpackManager.Instance.OnGetGoodCount(itemID);
		if (num > 0L)
		{
			this.m_needCostNum.set_text(string.Format("<color=#ffeb4b>x{0}</color>", num));
		}
		else
		{
			this.m_needCostNum.set_text(string.Format("<color=#ff0000>x{0}</color>", num));
		}
	}

	public void UpdateNum()
	{
		long num = BackpackManager.Instance.OnGetGoodCount(this.ItemID);
		if (num > 0L)
		{
			this.m_needCostNum.set_text(string.Format("<color=#ffeb4b>x{0}</color>", num));
		}
		else
		{
			this.m_needCostNum.set_text(string.Format("<color=#ff0000>x{0}</color>", num));
		}
	}
}
