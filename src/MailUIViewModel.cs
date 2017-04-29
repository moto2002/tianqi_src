using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;

public class MailUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_MailListUIVisibility = "MailListUIVisibility";

		public const string Attr_MailDetailUIVisibility = "MailDetailUIVisibility";

		public const string Attr_MailSendUIVisibility = "MailSendUIVisibility";

		public const string Attr_MailNoTipUIVisibility = "MailNoTipUIVisibility";

		public const string Attr_MailInfoUnits = "MailInfoUnits";

		public const string Attr_NoMailTip = "NoMailTip";

		public const string Attr_MailItems = "MailItems";

		public const string Attr_MailSender = "MailSender";

		public const string Attr_MailDate = "MailDate";

		public const string Attr_MailContent = "MailContent";

		public const string Attr_MailBtn1Visibility = "MailBtn1Visibility";

		public const string Attr_MailBtn1Name = "MailBtn1Name";

		public const string Attr_DownTime = "DownTime";

		public const string Attr_MailSendContent = "MailSendContent";

		public const string Event_OnMailBtn1Up = "OnMailBtn1Up";

		public const string Event_OnMailBtnOneKeyUp = "OnMailBtnOneKeyUp";

		public const string Event_OnBtnSendUp = "OnBtnSendUp";

		public const string Event_OnBtnBackUp = "OnBtnBackUp";
	}

	public enum MailUIStatus
	{
		list,
		list_detial,
		send,
		no_tip
	}

	public enum MailDetailBtnStatus
	{
		Draw,
		Delete
	}

	public static MailUIViewModel Instance;

	private MailUIViewModel.MailUIStatus _UIStatus;

	private MailUIViewModel.MailDetailBtnStatus _thisMailDetailBtnStatus = MailUIViewModel.MailDetailBtnStatus.Delete;

	private bool _MailListUIVisibility = true;

	private bool _MailDetailUIVisibility;

	private bool _MailSendUIVisibility;

	private bool _MailNoTipUIVisibility;

	private string _MailSender;

	private string _MailDate;

	private string _MailContent;

	private bool _MailBtn1Visibility;

	private string _MailBtn1Name;

	private string _DownTime;

	private string _MailSendContent;

	private bool _NoMailTip;

	public ObservableCollection<OOMailInfoUnit> MailInfoUnits = new ObservableCollection<OOMailInfoUnit>();

	public ObservableCollection<OOItem2Draw> MailItems = new ObservableCollection<OOItem2Draw>();

	public long ReceiverUID;

	private long _MailDetailUID;

	private TimeCountDown m_TimeCountDown;

	private MailUIViewModel.MailUIStatus UIStatus
	{
		get
		{
			return this._UIStatus;
		}
		set
		{
			this._UIStatus = value;
			this.MailListUIVisibility = false;
			this.MailDetailUIVisibility = false;
			this.MailSendUIVisibility = false;
			this.MailNoTipUIVisibility = false;
			switch (value)
			{
			case MailUIViewModel.MailUIStatus.list:
				this.MailListUIVisibility = true;
				break;
			case MailUIViewModel.MailUIStatus.list_detial:
				this.MailListUIVisibility = true;
				this.MailDetailUIVisibility = true;
				break;
			case MailUIViewModel.MailUIStatus.send:
				this.MailSendUIVisibility = true;
				break;
			case MailUIViewModel.MailUIStatus.no_tip:
				this.MailNoTipUIVisibility = true;
				this.NoMailTip = true;
				break;
			}
		}
	}

	public MailUIViewModel.MailDetailBtnStatus thisMailDetailBtnStatus
	{
		get
		{
			return this._thisMailDetailBtnStatus;
		}
		set
		{
			this._thisMailDetailBtnStatus = value;
			if (this._thisMailDetailBtnStatus == MailUIViewModel.MailDetailBtnStatus.Draw)
			{
				this.MailBtn1Visibility = true;
				this.MailBtn1Name = GameDataUtils.GetChineseContent(502030, false);
			}
			else if (this._thisMailDetailBtnStatus == MailUIViewModel.MailDetailBtnStatus.Delete)
			{
				this.MailBtn1Visibility = true;
				this.MailBtn1Name = GameDataUtils.GetChineseContent(502029, false);
			}
		}
	}

	public bool MailListUIVisibility
	{
		get
		{
			return this._MailListUIVisibility;
		}
		set
		{
			this._MailListUIVisibility = value;
			base.NotifyProperty("MailListUIVisibility", value);
		}
	}

	public bool MailDetailUIVisibility
	{
		get
		{
			return this._MailDetailUIVisibility;
		}
		set
		{
			this._MailDetailUIVisibility = value;
			base.NotifyProperty("MailDetailUIVisibility", value);
		}
	}

	public bool MailSendUIVisibility
	{
		get
		{
			return this._MailSendUIVisibility;
		}
		set
		{
			this._MailSendUIVisibility = value;
			base.NotifyProperty("MailSendUIVisibility", value);
		}
	}

	public bool MailNoTipUIVisibility
	{
		get
		{
			return this._MailNoTipUIVisibility;
		}
		set
		{
			this._MailNoTipUIVisibility = value;
			base.NotifyProperty("MailNoTipUIVisibility", value);
		}
	}

	public string MailSender
	{
		get
		{
			return this._MailSender;
		}
		set
		{
			this._MailSender = "邮件详情";
			base.NotifyProperty("MailSender", this._MailSender);
		}
	}

	public string MailDate
	{
		get
		{
			return this._MailDate;
		}
		set
		{
			this._MailDate = value;
			base.NotifyProperty("MailDate", value);
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

	public bool MailBtn1Visibility
	{
		get
		{
			return this._MailBtn1Visibility;
		}
		set
		{
			this._MailBtn1Visibility = value;
			base.NotifyProperty("MailBtn1Visibility", value);
		}
	}

	public string MailBtn1Name
	{
		get
		{
			return this._MailBtn1Name;
		}
		set
		{
			this._MailBtn1Name = value;
			base.NotifyProperty("MailBtn1Name", value);
		}
	}

	public string DownTime
	{
		get
		{
			return this._DownTime;
		}
		set
		{
			this._DownTime = value;
			base.NotifyProperty("DownTime", value);
		}
	}

	public string MailSendContent
	{
		get
		{
			return this._MailSendContent;
		}
		set
		{
			this._MailSendContent = value;
			base.NotifyProperty("MailSendContent", value);
		}
	}

	public bool NoMailTip
	{
		get
		{
			return this._NoMailTip;
		}
		set
		{
			this._NoMailTip = value;
			base.NotifyProperty("NoMailTip", value);
		}
	}

	public long MailDetailUID
	{
		get
		{
			return this._MailDetailUID;
		}
		set
		{
			this._MailDetailUID = value;
			for (int i = 0; i < this.MailInfoUnits.Count; i++)
			{
				this.MailInfoUnits[i].IsSelected = (this.MailInfoUnits[i].MailUID == this._MailDetailUID);
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		MailUIViewModel.Instance = this;
	}

	private void OnEnable()
	{
		this.ShowAs(MailUIViewModel.MailUIStatus.list, true);
	}

	private void OnDisable()
	{
		this.MailDetailUID = 0L;
	}

	public void OnMailBtn1Up()
	{
		if (this.thisMailDetailBtnStatus == MailUIViewModel.MailDetailBtnStatus.Draw)
		{
			MailManager.Instance.SendDrawMailAttach(this.MailDetailUID);
		}
		else if (this.thisMailDetailBtnStatus == MailUIViewModel.MailDetailBtnStatus.Delete)
		{
			MailManager.Instance.SendDelMail(this.MailDetailUID);
		}
	}

	public void OnMailBtnOneKeyUp()
	{
		MailManager.Instance.SendDrawAllMailAttach();
	}

	public void OnBtnSendUp()
	{
		MailManager.Instance.SendMail(this.ReceiverUID, this.MailSendContent);
		this.MailSendContent = string.Empty;
	}

	public void OnBtnBackUp()
	{
		this.ShowAs(MailUIViewModel.MailUIStatus.list, true);
	}

	public void ShowAs(MailUIViewModel.MailUIStatus status, bool resort)
	{
		this.UIStatus = status;
		this.UpdateUI(resort);
	}

	public void UpdateUI(bool resort)
	{
		this.Update4MailListUI(resort);
	}

	public void Update4MailDetailUI(long uid, MailInfo mailInfo)
	{
		if (this.MailDetailUID == uid)
		{
			this.Update4MailDetailUI(mailInfo);
		}
	}

	public bool DelAnim(long uid)
	{
		for (int i = 0; i < this.MailInfoUnits.Count; i++)
		{
			if (this.MailInfoUnits[i].MailUID == uid)
			{
				this.MailInfoUnits[i].CallAction = true;
				return true;
			}
		}
		return false;
	}

	public long GetNextMail(long uid)
	{
		long result = 0L;
		for (int i = 0; i < this.MailInfoUnits.Count; i++)
		{
			if (this.MailInfoUnits[i].MailUID == uid)
			{
				if (i < this.MailInfoUnits.Count - 1)
				{
					result = this.MailInfoUnits[i + 1].MailUID;
				}
				else if (i > 0)
				{
					result = this.MailInfoUnits[i - 1].MailUID;
				}
			}
		}
		return result;
	}

	private void Update4MailListUI(bool resort)
	{
		this.MailInfoUnits.Clear();
		this.NoMailTip = false;
		List<MailInformation> mailInformations = MailManager.Instance.MailInformations;
		if (mailInformations.get_Count() == 0)
		{
			this.UIStatus = MailUIViewModel.MailUIStatus.no_tip;
			return;
		}
		if (resort)
		{
			mailInformations.Sort(new Comparison<MailInformation>(MailUIViewModel.MailSortCompare));
		}
		for (int i = 0; i < mailInformations.get_Count(); i++)
		{
			MailInfo mailInfo = mailInformations.get_Item(i).mailInfo;
			OOMailInfoUnit oOMailInfoUnit = new OOMailInfoUnit();
			oOMailInfoUnit.MailUID = mailInfo.id;
			oOMailInfoUnit.SenderUID = mailInfo.sender.id;
			oOMailInfoUnit.thisMailStatus = mailInfo.status;
			oOMailInfoUnit.SendName = mailInfo.title;
			oOMailInfoUnit.SendDate = TimeManager.Instance.CalculateLocalServerTimeBySecond((int)mailInfo.buildDate).ToString("MM/dd");
			oOMailInfoUnit.MailContent = mailInfo.content.text;
			if (mailInfo.type == MailType.MT.Private && MailManager.MailSendOn)
			{
				oOMailInfoUnit.ButtonStatus = OOMailInfoUnit.MailButtonStatus.Reply;
			}
			else if (mailInfo.content.items != null && mailInfo.content.items.get_Count() > 0 && mailInfo.drawMark != 0)
			{
				oOMailInfoUnit.ButtonStatus = OOMailInfoUnit.MailButtonStatus.Draw;
			}
			else
			{
				oOMailInfoUnit.ButtonStatus = OOMailInfoUnit.MailButtonStatus.None;
			}
			this.MailInfoUnits.Add(oOMailInfoUnit);
		}
		if (MailUIViewModel.Instance.MailDetailUID <= 0L && this.MailInfoUnits.Count > 0 && this.MailInfoUnits[0].MailUID > 0L)
		{
			MailUIViewModel.Instance.MailDetailUID = this.MailInfoUnits[0].MailUID;
			MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.list_detial, false);
			MailManager.Instance.SendCheckMail(MailUIViewModel.Instance.MailDetailUID);
		}
		this.MailDetailUID = this.MailDetailUID;
	}

	private static int MailSortCompare(MailInformation AF1, MailInformation AF2)
	{
		int result = 0;
		if (AF1.mailInfo.status == 1 && AF2.mailInfo.status == 0)
		{
			result = 1;
		}
		else if (AF1.mailInfo.status == 0 && AF2.mailInfo.status == 1)
		{
			result = -1;
		}
		else if ((AF1.mailInfo.content.items.get_Count() == 0 || AF1.mailInfo.drawMark == 0) && AF2.mailInfo.content.items.get_Count() > 0 && AF2.mailInfo.drawMark != 0)
		{
			result = 1;
		}
		else if (AF1.mailInfo.content.items.get_Count() > 0 && AF1.mailInfo.drawMark != 0 && (AF2.mailInfo.content.items.get_Count() == 0 || AF2.mailInfo.drawMark == 0))
		{
			result = -1;
		}
		else if (AF1.mailInfo.buildDate > AF2.mailInfo.buildDate)
		{
			result = -1;
		}
		else if (AF1.mailInfo.buildDate < AF2.mailInfo.buildDate)
		{
			result = 1;
		}
		else if (AF1.mailInfo.id > AF2.mailInfo.id)
		{
			result = -1;
		}
		else if (AF1.mailInfo.id < AF2.mailInfo.id)
		{
			result = 1;
		}
		return result;
	}

	private void Update4MailDetailUI(MailInfo mailInfo)
	{
		if (mailInfo != null)
		{
			this.MailSender = mailInfo.title;
			this.MailDate = TimeManager.Instance.CalculateLocalServerTimeBySecond((int)mailInfo.buildDate).ToString("yyyy/MM/dd HH:mm");
			this.MailContent = mailInfo.content.text;
			this.SetMailItems(mailInfo.content.items, mailInfo.drawMark);
			if (mailInfo.content.items.get_Count() > 0 && mailInfo.drawMark != 0)
			{
				this.thisMailDetailBtnStatus = MailUIViewModel.MailDetailBtnStatus.Draw;
			}
			else
			{
				this.thisMailDetailBtnStatus = MailUIViewModel.MailDetailBtnStatus.Delete;
			}
			this.SetDownTime(mailInfo);
		}
	}

	private void SetDownTime(MailInfo mailInfo)
	{
		this.DownTime = string.Empty;
		if (this.m_TimeCountDown != null)
		{
			if (mailInfo.timeoutSec > 0)
			{
				this.m_TimeCountDown.ResetSeconds(mailInfo.timeoutSec);
			}
			else
			{
				this.m_TimeCountDown.Dispose();
				this.m_TimeCountDown = null;
			}
		}
		else if (mailInfo.timeoutSec > 0)
		{
			this.m_TimeCountDown = new TimeCountDown(mailInfo.timeoutSec, TimeFormat.DHHMM_Chinese, delegate
			{
				if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_gameObject().get_activeSelf())
				{
					string color = TextColorMgr.GetColor(this.m_TimeCountDown.GetTime(), "ff7d4b", string.Empty);
					this.DownTime = color + "后自动删除";
				}
			}, delegate
			{
				if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_gameObject().get_activeSelf())
				{
					this.DownTime = string.Empty;
				}
			}, true);
		}
	}

	private void SetMailItems(List<DetailInfo> items, int drawMark)
	{
		this.MailItems.Clear();
		for (int i = 0; i < items.get_Count(); i++)
		{
			DetailInfo detailInfo = items.get_Item(i);
			OOItem2Draw oOItem2Draw = new OOItem2Draw();
			Items items2 = DataReader<Items>.Get(detailInfo.cfgId);
			if (items2 != null)
			{
				oOItem2Draw.ID = items2.id;
				oOItem2Draw.FrameIcon = GameDataUtils.GetItemFrame(items2.id);
				oOItem2Draw.ItemIcon = GameDataUtils.GetIcon(items2.icon);
				oOItem2Draw.ItemName = Utils.GetItemNum(items2.id, detailInfo.num);
			}
			else
			{
				oOItem2Draw.ID = 0;
				oOItem2Draw.ItemIcon = ResourceManagerBase.GetNullSprite();
				oOItem2Draw.ItemName = string.Empty;
			}
			this.MailItems.Add(oOItem2Draw);
		}
	}

	private void Update4MailSendUI()
	{
		if (this.UIStatus != MailUIViewModel.MailUIStatus.send)
		{
			return;
		}
	}
}
