using Foundation.Core;
using GameData;
using System;
using System.Collections.Generic;

public class RevealPackUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Item2Checks = "Item2Checks";
	}

	private static RevealPackUIViewModel m_instance;

	public ObservableCollection<OOItem2Check> Item2Checks = new ObservableCollection<OOItem2Check>();

	public static RevealPackUIViewModel Instance
	{
		get
		{
			return RevealPackUIViewModel.m_instance;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		RevealPackUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
		this.UpdateItem2Checks();
	}

	public void UpdateItem2Checks()
	{
		this.Item2Checks.Clear();
		List<Goods> bag = BackpackManager.Instance.Bag;
		for (int i = 0; i < bag.get_Count(); i++)
		{
			int itemId = bag.get_Item(i).GetItemId();
			if (this.IsItemCanShow(itemId))
			{
				Items items = DataReader<Items>.Get(itemId);
				if (items != null)
				{
					OOItem2Check oOItem2Check = new OOItem2Check();
					oOItem2Check.id = items.id;
					oOItem2Check.Frame = GameDataUtils.GetItemFrame(items.id);
					oOItem2Check.Icon = GameDataUtils.GetIcon(items.icon);
					oOItem2Check.CheckVisibility = ChatUIViewModel.Instance.IsItemInShow(items.id);
					this.Item2Checks.Add(oOItem2Check);
				}
			}
		}
	}

	public bool IsShowItemHasFull()
	{
		int num = 0;
		for (int i = 0; i < this.Item2Checks.Count; i++)
		{
			if (this.Item2Checks[i].CheckVisibility)
			{
				num++;
			}
		}
		return num >= ChatManager.MAX_NUM_2_ITEM;
	}

	private bool IsItemCanShow(int itemId)
	{
		Items items = DataReader<Items>.Get(itemId);
		return items != null && items.show == 1;
	}
}
