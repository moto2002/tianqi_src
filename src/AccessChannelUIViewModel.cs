using Foundation.Core;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AccessChannelUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemIconBg = "ItemIconBg";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_AccessChannelUIItems = "AccessChannelUIItems";

		public const string Event_OnCloseBtnUp = "OnCloseBtnUp";
	}

	private static AccessChannelUIViewModel m_instance;

	private SpriteRenderer _ItemIcon;

	private SpriteRenderer _ItemIconBg;

	private string _ItemName;

	public ObservableCollection<OOAccessChannelUIItem> AccessChannelUIItems = new ObservableCollection<OOAccessChannelUIItem>();

	private Action _ActionClose;

	public static AccessChannelUIViewModel Instance
	{
		get
		{
			return AccessChannelUIViewModel.m_instance;
		}
	}

	public SpriteRenderer ItemIcon
	{
		get
		{
			return this._ItemIcon;
		}
		set
		{
			this._ItemIcon = value;
			base.NotifyProperty("ItemIcon", value);
		}
	}

	public SpriteRenderer ItemIconBg
	{
		get
		{
			return this._ItemIconBg;
		}
		set
		{
			this._ItemIconBg = value;
			base.NotifyProperty("ItemIconBg", value);
		}
	}

	public string ItemName
	{
		get
		{
			return this._ItemName;
		}
		set
		{
			this._ItemName = value;
			base.NotifyProperty("ItemName", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		AccessChannelUIViewModel.m_instance = this;
	}

	public void OnCloseBtnUp()
	{
		Debuger.Error("OnCloseBtnUp", new object[0]);
		AccessChannelUIView component = base.GetComponent<AccessChannelUIView>();
		component.Show(false);
		if (this._ActionClose != null)
		{
			this._ActionClose.Invoke();
		}
	}

	public void ShowAccessChannels(int itemId, Action closeCallback = null)
	{
		this._ActionClose = closeCallback;
		this.AccessChannelUIItems.Clear();
		Items items = DataReader<Items>.Get(itemId);
		if (items == null)
		{
			return;
		}
		this.ItemIcon = GameDataUtils.GetIcon(items.icon);
		this.ItemIconBg = GameDataUtils.GetItemFrame(items.id);
		this.ItemName = GameDataUtils.GetItemName(items, true);
		List<int> getType = items.getType;
		for (int i = 0; i < getType.get_Count(); i++)
		{
			ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(getType.get_Item(i));
			if (zhuXianPeiZhi != null)
			{
				OOAccessChannelUIItem o = new OOAccessChannelUIItem
				{
					InstanceId = getType.get_Item(i),
					Icon = ResourceManager.GetIconSprite("i32300_s"),
					Title = GameDataUtils.GetChineseContent(zhuXianPeiZhi.name, false),
					TitleDesc = TextColorMgr.GetColor(GameDataUtils.GetChineseContent(DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi.chapterId).chapterName, false), "fefedc", string.Empty)
				};
				this.AccessChannelUIItems.Add(o);
			}
		}
	}
}
