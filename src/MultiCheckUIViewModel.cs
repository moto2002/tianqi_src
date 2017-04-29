using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiCheckUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Title = "Title";

		public const string Attr_BtnLeftVisibility = "BtnLeftVisibility";

		public const string Attr_BtnRightVisibility = "BtnRightVisibility";

		public const string Attr_BtnConfirmVisibility = "BtnConfirmVisibility";

		public const string Attr_BtnLeftText = "BtnLeftText";

		public const string Attr_BtnRightText = "BtnRightText";

		public const string Attr_BtnConfirmText = "BtnConfirmText";

		public const string Attr_ContentTipText = "ContentTipText";

		public const string Attr_ImageBtnL = "ImageBtnL";

		public const string Attr_ImageBtnR = "ImageBtnR";

		public const string Attr_ImageBtnC = "ImageBtnC";

		public const string Attr_Items = "Items";

		public const string Event_OnBtnLeftUp = "OnBtnLeftUp";

		public const string Event_OnBtnRightUp = "OnBtnRightUp";

		public const string Event_OnBtnConfirmUp = "OnBtnConfirmUp";

		public const string Event_OnBtnCloseUp = "OnBtnCloseUp";
	}

	public struct ItemData
	{
		public int id;

		public string name;

		public bool isOn;
	}

	private static MultiCheckUIViewModel m_instance;

	private Action m_actionL;

	private Action m_actionR;

	private Action m_actionConfirm;

	private Action m_actionClose;

	private bool _BtnLeftVisibility;

	private bool _BtnRightVisibility;

	private bool _BtnConfirmVisibility;

	private string _BtnLeftText;

	private string _BtnRightText;

	private string _BtnConfirmText;

	private string _ContentTipText;

	private SpriteRenderer _ImageBtnL;

	private SpriteRenderer _ImageBtnR;

	private SpriteRenderer _ImageBtnC;

	public ObservableCollection<OOMultiCheckItem> Items = new ObservableCollection<OOMultiCheckItem>();

	private string _Title;

	public static MultiCheckUIViewModel Instance
	{
		get
		{
			if (MultiCheckUIViewModel.m_instance == null)
			{
				MultiCheckUIViewModel.Open(null);
			}
			return MultiCheckUIViewModel.m_instance;
		}
	}

	public bool BtnLeftVisibility
	{
		get
		{
			return this._BtnLeftVisibility;
		}
		set
		{
			this._BtnLeftVisibility = value;
			base.NotifyProperty("BtnLeftVisibility", value);
		}
	}

	public bool BtnRightVisibility
	{
		get
		{
			return this._BtnRightVisibility;
		}
		set
		{
			this._BtnRightVisibility = value;
			base.NotifyProperty("BtnRightVisibility", value);
		}
	}

	public bool BtnConfirmVisibility
	{
		get
		{
			return this._BtnConfirmVisibility;
		}
		set
		{
			this._BtnConfirmVisibility = value;
			base.NotifyProperty("BtnConfirmVisibility", value);
		}
	}

	public string BtnLeftText
	{
		get
		{
			return this._BtnLeftText;
		}
		set
		{
			this._BtnLeftText = value;
			base.NotifyProperty("BtnLeftText", value);
		}
	}

	public string BtnRightText
	{
		get
		{
			return this._BtnRightText;
		}
		set
		{
			this._BtnRightText = value;
			base.NotifyProperty("BtnRightText", value);
		}
	}

	public string BtnConfirmText
	{
		get
		{
			return this._BtnConfirmText;
		}
		set
		{
			this._BtnConfirmText = value;
			base.NotifyProperty("BtnConfirmText", value);
		}
	}

	public string ContentTipText
	{
		get
		{
			return this._ContentTipText;
		}
		set
		{
			this._ContentTipText = value;
			base.NotifyProperty("ContentTipText", value);
		}
	}

	public SpriteRenderer ImageBtnL
	{
		get
		{
			return this._ImageBtnL;
		}
		set
		{
			this._ImageBtnL = value;
			base.NotifyProperty("ImageBtnL", value);
		}
	}

	public SpriteRenderer ImageBtnR
	{
		get
		{
			return this._ImageBtnR;
		}
		set
		{
			this._ImageBtnR = value;
			base.NotifyProperty("ImageBtnR", value);
		}
	}

	public SpriteRenderer ImageBtnC
	{
		get
		{
			return this._ImageBtnC;
		}
		set
		{
			this._ImageBtnC = value;
			base.NotifyProperty("ImageBtnC", value);
		}
	}

	public string Title
	{
		get
		{
			return this._Title;
		}
		set
		{
			this._Title = ((!string.IsNullOrEmpty(value)) ? value : "提示");
			base.NotifyProperty("Title", this._Title);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		MultiCheckUIViewModel.m_instance = this;
	}

	public void OnBtnLeftUp()
	{
		this.Close();
		if (this.m_actionL != null)
		{
			this.m_actionL.Invoke();
		}
	}

	public void OnBtnRightUp()
	{
		this.Close();
		if (this.m_actionR != null)
		{
			this.m_actionR.Invoke();
		}
	}

	public void OnBtnConfirmUp()
	{
		if (this.m_actionConfirm != null)
		{
			this.m_actionConfirm.Invoke();
		}
		this.Close();
	}

	public void OnBtnCloseUp()
	{
		this.Close();
		if (this.m_actionClose != null)
		{
			this.m_actionClose.Invoke();
		}
	}

	public void ShowAsConfirm(string title, List<MultiCheckUIViewModel.ItemData> list, Action actionC, string nameC = "", string btnC = "button_orange_1", Transform root = null, string contentTip = "")
	{
		MultiCheckUIView.Instance.SetMask(0.7f, true, true);
		MultiCheckUIViewModel.Open(root);
		this.Title = title;
		this.m_actionConfirm = actionC;
		this.ImageBtnC = ButtonColorMgr.GetButton(btnC);
		this.BtnConfirmText = ((!string.IsNullOrEmpty(nameC)) ? nameC : this.BtnConfirmText);
		this.ContentTipText = contentTip;
		this.BtnLeftVisibility = false;
		this.BtnRightVisibility = false;
		this.BtnConfirmVisibility = true;
		this.SetItems(list);
	}

	public bool IsOn(int id)
	{
		for (int i = 0; i < this.Items.Count; i++)
		{
			if (this.Items[i].id == id)
			{
				return this.Items[i].IsOn;
			}
		}
		return false;
	}

	private static void Open(Transform root = null)
	{
		if (root == null)
		{
			root = UINodesManager.TopUIRoot;
		}
		UIManagerControl.Instance.OpenUI("MultiCheckUI", root, false, UIType.NonPush);
		if (MultiCheckUIView.Instance != null)
		{
			MultiCheckUIView.Instance.SetSibling();
		}
	}

	public void Close()
	{
		MultiCheckUIView.Instance.Show(false);
	}

	private void SetItems(List<MultiCheckUIViewModel.ItemData> list)
	{
		this.Items.Clear();
		for (int i = 0; i < list.get_Count(); i++)
		{
			OOMultiCheckItem oOMultiCheckItem = new OOMultiCheckItem();
			oOMultiCheckItem.id = list.get_Item(i).id;
			oOMultiCheckItem.Name = list.get_Item(i).name;
			oOMultiCheckItem.IsOn = list.get_Item(i).isOn;
			this.Items.Add(oOMultiCheckItem);
		}
	}
}
