using Foundation.Core;
using System;
using UnityEngine;

public class OOMailInfoUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_StatusIcon = "StatusIcon";

		public const string Attr_SendName = "SendName";

		public const string Attr_SendDate = "SendDate";

		public const string Attr_MailContent = "MailContent";

		public const string Attr_IsSelected = "IsSelected";

		public const string Attr_CallAction = "CallAction";

		public const string Event_OnBtnUp = "OnBtnUp";
	}

	public enum MailButtonStatus
	{
		None,
		Draw,
		Reply
	}

	public long MailUID;

	public long SenderUID;

	private int _thisMailStatus;

	private OOMailInfoUnit.MailButtonStatus _ButtonStatus;

	private SpriteRenderer _StatusIcon;

	private string _SendName;

	private string _SendDate;

	private string _MailContent;

	private bool _IsSelected;

	private bool _CallAction;

	public int thisMailStatus
	{
		get
		{
			return this._thisMailStatus;
		}
		set
		{
			this._thisMailStatus = value;
			if (value == 0)
			{
				this.StatusIcon = ResourceManager.GetIconSprite("youjian_1");
			}
			else if (value == 1)
			{
				this.StatusIcon = ResourceManager.GetIconSprite("youjian_2");
			}
		}
	}

	public OOMailInfoUnit.MailButtonStatus ButtonStatus
	{
		get
		{
			return this._ButtonStatus;
		}
		set
		{
			this._ButtonStatus = value;
			switch (value)
			{
			}
		}
	}

	public SpriteRenderer StatusIcon
	{
		get
		{
			return this._StatusIcon;
		}
		set
		{
			this._StatusIcon = value;
			base.NotifyProperty("StatusIcon", value);
		}
	}

	public string SendName
	{
		get
		{
			return this._SendName;
		}
		set
		{
			this._SendName = value;
			base.NotifyProperty("SendName", value);
		}
	}

	public string SendDate
	{
		get
		{
			return this._SendDate;
		}
		set
		{
			this._SendDate = value;
			base.NotifyProperty("SendDate", value);
		}
	}

	public string MailContent
	{
		get
		{
			return this._MailContent;
		}
		set
		{
			this._MailContent = value;
			base.NotifyProperty("MailContent", value);
		}
	}

	public bool IsSelected
	{
		get
		{
			return this._IsSelected;
		}
		set
		{
			this._IsSelected = value;
			base.NotifyProperty("IsSelected", value);
		}
	}

	public bool CallAction
	{
		get
		{
			return this._CallAction;
		}
		set
		{
			base.NotifyProperty("CallAction", value);
		}
	}

	public void OnBtnUp()
	{
		if (this.MailUID != MailUIViewModel.Instance.MailDetailUID)
		{
			MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.list_detial, false);
			MailManager.Instance.SendCheckMail(this.MailUID);
		}
	}

	public void OnBtn1Up()
	{
		switch (this.ButtonStatus)
		{
		case OOMailInfoUnit.MailButtonStatus.Draw:
			MailManager.Instance.SendDrawMailAttach(this.MailUID);
			break;
		case OOMailInfoUnit.MailButtonStatus.Reply:
			if (MailManager.MailSendOn)
			{
				MailUIViewModel.Instance.ReceiverUID = this.SenderUID;
				MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.send, false);
			}
			break;
		}
	}
}
