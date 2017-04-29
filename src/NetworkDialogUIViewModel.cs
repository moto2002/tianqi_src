using Foundation.Core;
using System;
using UnityEngine;

public class NetworkDialogUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_BtnLeftVisibility = "BtnLeftVisibility";

		public const string Attr_BtnRightVisibility = "BtnRightVisibility";

		public const string Attr_BtnConfirmVisibility = "BtnConfirmVisibility";

		public const string Attr_ContentVisibility = "ContentVisibility";

		public const string Attr_Content1Visibility = "Content1Visibility";

		public const string Attr_BtnLeftText = "BtnLeftText";

		public const string Attr_BtnRightText = "BtnRightText";

		public const string Attr_BtnConfirmText = "BtnConfirmText";

		public const string Attr_Title = "Title";

		public const string Attr_Content = "Content";

		public const string Attr_FinalContent = "FinalContent";

		public const string Attr_GoodNum = "GoodNum";

		public const string Attr_DescContent = "DescContent";

		public const string Attr_ImageBtnL = "ImageBtnL";

		public const string Attr_ImageBtnR = "ImageBtnR";

		public const string Attr_ImageBtnC = "ImageBtnC";

		public const string Attr_ImageGood = "ImageGood";

		public const string Event_OnBtnLeftUp = "OnBtnLeftUp";

		public const string Event_OnBtnRightUp = "OnBtnRightUp";

		public const string Event_OnBtnConfirmUp = "OnBtnConfirmUp";
	}

	private static NetworkDialogUIViewModel instance;

	private Action m_actionL;

	private Action m_actionR;

	private Action m_actionConfirm;

	private bool _BtnLeftVisibility;

	private bool _BtnRightVisibility;

	private bool _BtnConfirmVisibility;

	private bool _ContentVisibility;

	private bool _Content1Visibility;

	private string _BtnLeftText;

	private string _BtnRightText;

	private string _BtnConfirmText;

	private SpriteRenderer _ImageBtnL;

	private SpriteRenderer _ImageBtnR;

	private SpriteRenderer _ImageGood;

	private SpriteRenderer _ImageBtnC;

	private string _Title;

	private string _Content;

	private string _GoodNum;

	private string _FinalContent;

	private string _DescContent;

	public static NetworkDialogUIViewModel Instance
	{
		get
		{
			if (NetworkDialogUIViewModel.instance == null)
			{
				NetworkDialogUIViewModel.Open();
			}
			return NetworkDialogUIViewModel.instance;
		}
	}

	public bool BtnRclose
	{
		get;
		set;
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

	public bool ContentVisibility
	{
		get
		{
			return this._ContentVisibility;
		}
		set
		{
			this._ContentVisibility = value;
			base.NotifyProperty("ContentVisibility", value);
		}
	}

	public bool Content1Visibility
	{
		get
		{
			return this._Content1Visibility;
		}
		set
		{
			this._Content1Visibility = value;
			base.NotifyProperty("Content1Visibility", value);
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

	public SpriteRenderer ImageGood
	{
		get
		{
			return this._ImageGood;
		}
		set
		{
			this._ImageGood = value;
			base.NotifyProperty("ImageGood", value);
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
			this._Title = ((!string.IsNullOrEmpty(value)) ? value : "弹出框");
			base.NotifyProperty("Title", this._Title);
		}
	}

	public string Content
	{
		get
		{
			return this._Content;
		}
		set
		{
			this._Content = value;
			base.NotifyProperty("Content", this._Content);
		}
	}

	public string GoodNum
	{
		get
		{
			return this._GoodNum;
		}
		set
		{
			this._GoodNum = value;
			base.NotifyProperty("GoodNum", this._GoodNum);
		}
	}

	public string FinalContent
	{
		get
		{
			return this._FinalContent;
		}
		set
		{
			this._FinalContent = value;
			base.NotifyProperty("FinalContent", this._FinalContent);
		}
	}

	public string DescContent
	{
		get
		{
			return this._DescContent;
		}
		set
		{
			this._DescContent = value;
			base.NotifyProperty("DescContent", this._DescContent);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		NetworkDialogUIViewModel.instance = this;
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
		if (this.m_actionR != null)
		{
			this.m_actionR.Invoke();
		}
		if (this.BtnRclose)
		{
			this.Close();
		}
	}

	public void OnBtnConfirmUp()
	{
		this.Close();
		if (this.m_actionConfirm != null)
		{
			this.m_actionConfirm.Invoke();
		}
	}

	public void ShowAsOKCancel(string title, string content, Action actionL, Action actionR, string nameL = "取 消", string nameR = "确 定", string btnL = "button_orange_1", string btnR = "button_yellow_1")
	{
		NetworkDialogUIViewModel.Open();
		this.m_actionL = actionL;
		this.m_actionR = actionR;
		this.BtnRclose = true;
		this.BtnLeftText = ((!string.IsNullOrEmpty(nameL)) ? nameL : this.BtnLeftText);
		this.BtnRightText = ((!string.IsNullOrEmpty(nameR)) ? nameR : this.BtnRightText);
		this.Title = title;
		this.ContentVisibility = true;
		this.Content1Visibility = false;
		this.Content = content;
		this.ImageBtnL = ButtonColorMgr.GetButton(btnL);
		this.ImageBtnR = ButtonColorMgr.GetButton(btnR);
		this.BtnLeftVisibility = true;
		this.BtnRightVisibility = true;
		this.BtnConfirmVisibility = false;
		NetworkDialogUIView.Instance.isClick = false;
	}

	public void ShowAsOKCancelContent(string title, int goodId, int num, string content, Action actionL, Action actionR, string decsContent = null, Transform root = null)
	{
		this.ShowAsOKCancel(title, string.Empty, actionL, actionR, "取 消", "确 定", "button_orange_1", "button_yellow_1");
		this.ContentVisibility = false;
		this.Content1Visibility = true;
		int num2;
		if (goodId == 2)
		{
			num2 = 100002;
		}
		else if (goodId == 3)
		{
			num2 = 100003;
		}
		else
		{
			num2 = goodId;
		}
		this.ImageGood = ResourceManager.GetIconSprite(num2.ToString());
		this.GoodNum = num.ToString();
		this.FinalContent = content;
		this.DescContent = decsContent;
	}

	public void ShowAsConfirm(string title, string content, Action actionC, string nameC = "", string btnC = "button_orange_1")
	{
		NetworkDialogUIViewModel.Open();
		NetworkDialogUIView.Instance.SetMask(0.7f, true, true);
		this.m_actionConfirm = actionC;
		this.ContentVisibility = true;
		this.Content1Visibility = false;
		this.BtnConfirmText = ((!string.IsNullOrEmpty(nameC)) ? nameC : this.BtnConfirmText);
		this.Title = title;
		this.Content = content;
		this.ImageBtnC = ButtonColorMgr.GetButton(btnC);
		this.BtnLeftVisibility = false;
		this.BtnRightVisibility = false;
		this.BtnConfirmVisibility = true;
		NetworkDialogUIView.Instance.isClick = false;
	}

	private static void Open()
	{
		UIManagerControl.Instance.OpenUI("NetworkDialogUI", UINodesManager.T4RootOfSpecial, false, UIType.NonPush);
		NetworkDialogUIView.Instance.SetSibling();
	}

	private void Close()
	{
		NetworkDialogUIView.Instance.Show(false);
	}
}
