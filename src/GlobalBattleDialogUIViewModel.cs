using Foundation.Core;
using System;
using UnityEngine;

public class GlobalBattleDialogUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_BtnLeftVisibility = "BtnLeftVisibility";

		public const string Attr_BtnRightVisibility = "BtnRightVisibility";

		public const string Attr_BtnConfirmVisibility = "BtnConfirmVisibility";

		public const string Attr_ContentVisibility = "ContentVisibility";

		public const string Attr_Content1Visibility = "Content1Visibility";

		public const string Attr_Content2Visibility = "Content2Visibility";

		public const string Attr_BtnCloseVisibility = "BtnCloseVisibility";

		public const string Attr_ToggleDontShowAgainVisibility = "ToggleDontShowAgainVisibility";

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

		public const string Attr_Content2 = "Content2";

		public const string Attr_TextToggle = "TextToggle";

		public const string Attr_ToggleDontShowAgainOn = "ToggleDontShowAgainOn";

		public const string Event_OnBtnLeftUp = "OnBtnLeftUp";

		public const string Event_OnBtnRightUp = "OnBtnRightUp";

		public const string Event_OnBtnConfirmUp = "OnBtnConfirmUp";

		public const string Event_OnBtnCloseUp = "OnBtnCloseUp";
	}

	private static GlobalBattleDialogUIViewModel m_instance;

	private Action m_actionL;

	private Action m_actionR;

	private Action m_actionConfirm;

	private Action m_actionClose;

	private Action<bool> m_changeToggle;

	private bool _BtnLeftVisibility;

	private bool _BtnRightVisibility;

	private bool _BtnConfirmVisibility;

	private string _BtnLeftText;

	private string _BtnRightText;

	private string _BtnConfirmText;

	private SpriteRenderer _ImageBtnL;

	private SpriteRenderer _ImageBtnR;

	private SpriteRenderer _ImageBtnC;

	private bool _ContentVisibility;

	private bool _Content1Visibility;

	private bool _Content2Visibility;

	private bool _ToggleDontShowAgainVisibility;

	private bool _BtnCloseVisibility;

	private SpriteRenderer _ImageGood;

	private string _Title;

	private string _Content;

	private string _GoodNum;

	private string _FinalContent;

	private string _DescContent;

	private string _Content2;

	private string _TextToggle;

	private bool _ToggleDontShowAgainOn;

	public static GlobalBattleDialogUIViewModel Instance
	{
		get
		{
			if (GlobalBattleDialogUIViewModel.m_instance == null)
			{
				GlobalBattleDialogUIViewModel.Open(null);
			}
			return GlobalBattleDialogUIViewModel.m_instance;
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

	public bool Content2Visibility
	{
		get
		{
			return this._Content2Visibility;
		}
		set
		{
			this._Content2Visibility = value;
			base.NotifyProperty("Content2Visibility", value);
		}
	}

	public bool ToggleDontShowAgainVisibility
	{
		get
		{
			return this._ToggleDontShowAgainVisibility;
		}
		set
		{
			this._ToggleDontShowAgainVisibility = value;
			base.NotifyProperty("ToggleDontShowAgainVisibility", value);
		}
	}

	public bool BtnCloseVisibility
	{
		get
		{
			return this._BtnCloseVisibility;
		}
		set
		{
			this._BtnCloseVisibility = value;
			base.NotifyProperty("BtnCloseVisibility", value);
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

	public string Content2
	{
		get
		{
			return this._Content2;
		}
		set
		{
			this._Content2 = value;
			base.NotifyProperty("Content2", this._Content2);
		}
	}

	public string TextToggle
	{
		get
		{
			return this._TextToggle;
		}
		set
		{
			this._TextToggle = value;
			base.NotifyProperty("TextToggle", this._TextToggle);
		}
	}

	public bool ToggleDontShowAgainOn
	{
		get
		{
			return this._ToggleDontShowAgainOn;
		}
		set
		{
			this._ToggleDontShowAgainOn = value;
			if (this.m_changeToggle != null)
			{
				this.m_changeToggle.Invoke(value);
			}
			base.NotifyProperty("ToggleDontShowAgainOn", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		GlobalBattleDialogUIViewModel.m_instance = this;
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
		if (this.BtnRclose)
		{
			this.Close();
		}
		if (this.m_actionR != null)
		{
			this.m_actionR.Invoke();
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

	public void OnBtnCloseUp()
	{
		this.Close();
		if (this.m_actionClose != null)
		{
			this.m_actionClose.Invoke();
			this.m_actionClose = null;
		}
		else if (this.m_actionL != null)
		{
			this.m_actionL.Invoke();
		}
	}

	public void SetActionClose(Action actionClose)
	{
		this.m_actionClose = actionClose;
	}

	public void ShowAsOKCancel_as(string title, string content, Action actionShow, Action actionL, Action actionR, string nameL = "", string nameR = "", string btnL = "button_orange_1", string btnR = "button_yellow_1", Transform root = null)
	{
		if (actionShow != null)
		{
			actionShow.Invoke();
		}
		this.ShowAsOKCancel(title, content, actionL, actionR, nameL, nameR, btnL, btnR, root, true);
	}

	public void ShowAsOKCancel(string title, string content, Action actionL, Action actionR, string nameL = "取 消", string nameR = "确 定", string btnL = "button_orange_1", string btnR = "button_yellow_1", Transform root = null, bool isShowCloseBtn = true)
	{
		GlobalBattleDialogUIViewModel.Open(root);
		this.m_actionL = actionL;
		this.m_actionR = actionR;
		this.BtnRclose = true;
		this.BtnLeftText = ((!string.IsNullOrEmpty(nameL)) ? nameL : this.BtnLeftText);
		this.BtnRightText = ((!string.IsNullOrEmpty(nameR)) ? nameR : this.BtnRightText);
		this.Title = title;
		this.ContentVisibility = true;
		this.Content1Visibility = false;
		this.Content2Visibility = false;
		this.Content = content;
		this.ImageBtnL = ButtonColorMgr.GetButton(btnL);
		this.ImageBtnR = ButtonColorMgr.GetButton(btnR);
		this.BtnLeftVisibility = true;
		this.BtnRightVisibility = true;
		this.BtnConfirmVisibility = false;
		this.BtnCloseVisibility = isShowCloseBtn;
		this.ToggleDontShowAgainVisibility = false;
	}

	public void ShowAsOKCancelContent(string title, int goodId, int num, string content, Action actionL, Action actionR, string decsContent = null, Transform root = null, bool isShowCloseBtn = true)
	{
		this.ShowAsOKCancel(title, string.Empty, actionL, actionR, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true);
		this.ContentVisibility = false;
		this.Content1Visibility = true;
		this.BtnCloseVisibility = isShowCloseBtn;
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

	public void ShowAsOKCancelDontShowAgain(string title, string content, string toggle, Action actionL, Action actionR, Action<bool> onChangeValue, Transform root = null)
	{
		this.ShowAsOKCancel(title, string.Empty, actionL, actionR, "取 消", "确 定", "button_orange_1", "button_yellow_1", root, true);
		this.TextToggle = toggle;
		this.m_changeToggle = onChangeValue;
		this.ContentVisibility = false;
		this.Content1Visibility = false;
		this.Content2Visibility = true;
		this.ToggleDontShowAgainVisibility = true;
		this.Content2 = content;
	}

	public void ShowAsConfirm(string title, string content, Action actionC, string nameC = "", string btnC = "button_orange_1", Transform root = null)
	{
		GlobalBattleDialogUIView.Instance.SetMask(0.7f, true, true);
		GlobalBattleDialogUIViewModel.Open(root);
		this.m_actionConfirm = actionC;
		this.ContentVisibility = true;
		this.Content1Visibility = false;
		this.Content2Visibility = false;
		this.BtnConfirmText = ((!string.IsNullOrEmpty(nameC)) ? nameC : this.BtnConfirmText);
		this.Title = title;
		this.Content = content;
		this.ImageBtnC = ButtonColorMgr.GetButton(btnC);
		this.BtnLeftVisibility = false;
		this.BtnRightVisibility = false;
		this.BtnConfirmVisibility = true;
		this.ToggleDontShowAgainVisibility = false;
	}

	public void ShowAsConfirm(string title, string content, Action actionClose, Action actionC, bool isClickMask = true, string nameC = "", string btnC = "button_orange_1", Transform root = null)
	{
		GlobalBattleDialogUIView.Instance.SetMask(0.7f, true, isClickMask);
		GlobalBattleDialogUIViewModel.Open(root);
		this.m_actionClose = actionClose;
		this.m_actionConfirm = actionC;
		this.ContentVisibility = true;
		this.Content1Visibility = false;
		this.Content2Visibility = false;
		this.BtnConfirmText = ((!string.IsNullOrEmpty(nameC)) ? nameC : this.BtnConfirmText);
		this.Title = title;
		this.Content = content;
		this.ImageBtnC = ButtonColorMgr.GetButton(btnC);
		this.BtnLeftVisibility = false;
		this.BtnRightVisibility = false;
		this.BtnConfirmVisibility = true;
		this.ToggleDontShowAgainVisibility = false;
	}

	private static void Open(Transform root = null)
	{
		if (root == null)
		{
			root = UINodesManager.TopUIRoot;
		}
		UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", root, false, UIType.NonPush);
		if (GlobalBattleDialogUIView.Instance != null)
		{
			GlobalBattleDialogUIView.Instance.SetSibling();
		}
	}

	public void Close()
	{
		if (GlobalBattleDialogUIView.Instance != null)
		{
			GlobalBattleDialogUIView.Instance.Show(false);
		}
	}
}
