using Foundation.Core;
using System;
using UnityEngine;

public class DialogExitUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_BtnLeftVisibility = "BtnLeftVisibility";

		public const string Attr_BtnRightVisibility = "BtnRightVisibility";

		public const string Attr_BtnConfirmVisibility = "BtnConfirmVisibility";

		public const string Attr_BtnLeftText = "BtnLeftText";

		public const string Attr_BtnRightText = "BtnRightText";

		public const string Attr_BtnConfirmText = "BtnConfirmText";

		public const string Attr_Title = "Title";

		public const string Attr_Content = "Content";

		public const string Attr_ImageBtnL = "ImageBtnL";

		public const string Attr_ImageBtnR = "ImageBtnR";

		public const string Attr_ImageBtnC = "ImageBtnC";

		public const string Event_OnBtnLeftUp = "OnBtnLeftUp";

		public const string Event_OnBtnRightUp = "OnBtnRightUp";

		public const string Event_OnBtnConfirmUp = "OnBtnConfirmUp";
	}

	private static DialogExitUIViewModel m_instance;

	private Action m_actionL;

	private Action m_actionR;

	private Action m_actionConfirm;

	private bool _BtnLeftVisibility;

	private bool _BtnRightVisibility;

	private bool _BtnConfirmVisibility;

	private string _BtnLeftText;

	private string _BtnRightText;

	private string _BtnConfirmText;

	private SpriteRenderer _ImageBtnL;

	private SpriteRenderer _ImageBtnR;

	private SpriteRenderer _ImageBtnC;

	private string _Title;

	private string _Content;

	public static DialogExitUIViewModel Instance
	{
		get
		{
			if (DialogExitUIViewModel.m_instance == null)
			{
				DialogExitUIViewModel.Open(null);
			}
			return DialogExitUIViewModel.m_instance;
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

	protected override void Awake()
	{
		base.Awake();
		DialogExitUIViewModel.m_instance = this;
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

	public void ShowAsOKCancel(string title, string content, Action actionL, Action actionR, string nameL = "取 消", string nameR = "确 定", string btnL = "button_orange_1", string btnR = "button_orange_1", Transform root = null)
	{
		DialogExitUIViewModel.Open(root);
		this.m_actionL = actionL;
		this.m_actionR = actionR;
		this.BtnRclose = true;
		this.BtnLeftText = ((!string.IsNullOrEmpty(nameL)) ? nameL : this.BtnLeftText);
		this.BtnRightText = ((!string.IsNullOrEmpty(nameR)) ? nameR : this.BtnRightText);
		this.Title = title;
		this.Content = content;
		this.ImageBtnL = ButtonColorMgr.GetButton(btnL);
		this.ImageBtnR = ButtonColorMgr.GetButton(btnR);
		this.BtnLeftVisibility = true;
		this.BtnRightVisibility = true;
		this.BtnConfirmVisibility = false;
	}

	public void ShowAsConfirm(string title, string content, Action actionC, string nameC = "", string btnC = "button_orange_1", Transform root = null)
	{
		DialogExitUIViewModel.Open(root);
		this.m_actionConfirm = actionC;
		this.BtnConfirmText = ((!string.IsNullOrEmpty(nameC)) ? nameC : this.BtnConfirmText);
		this.Title = title;
		this.Content = content;
		this.ImageBtnC = ButtonColorMgr.GetButton(btnC);
		this.BtnLeftVisibility = false;
		this.BtnRightVisibility = false;
		this.BtnConfirmVisibility = true;
	}

	private static void SetRoot(Transform root)
	{
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("DialogExitUI");
		if (uIIfExist != null)
		{
			uIIfExist.get_transform().SetParent(root);
		}
	}

	private static void Open(Transform root = null)
	{
		UIManagerControl.Instance.OpenUI("DialogExitUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
		if (root == null)
		{
			root = UINodesManager.T4RootOfSpecial;
		}
		DialogExitUIViewModel.SetRoot(root);
	}

	private void Close()
	{
		DialogExitUIView.Instance.Show(false);
	}
}
