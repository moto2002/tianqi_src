using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class MailManager : BaseSubSystemManager
{
	public class MailEvents
	{
		public const string IsMailReadTipOn = "MailEvents.IsMailReadTipOn";
	}

	public class MailStatus
	{
		public const int NoOpen = 0;

		public const int HasOpen = 1;

		public const int HasDrawMark = 0;

		public const string Sp_NoOpen = "youjian_1";

		public const string Sp_HasOpen = "youjian_2";
	}

	private List<MailInformation> _MailInformations = new List<MailInformation>();

	public static bool MailSendOn;

	private bool _IsMailsReadTipOn;

	private static MailManager instance;

	public List<MailInformation> MailInformations
	{
		get
		{
			return this._MailInformations;
		}
		set
		{
			this._MailInformations = value;
		}
	}

	public bool IsMailReadTipOn
	{
		get
		{
			return this._IsMailsReadTipOn;
		}
		set
		{
			this._IsMailsReadTipOn = value;
			EventDispatcher.Broadcast<bool>("MailEvents.IsMailReadTipOn", value);
		}
	}

	public static MailManager Instance
	{
		get
		{
			if (MailManager.instance == null)
			{
				MailManager.instance = new MailManager();
			}
			return MailManager.instance;
		}
	}

	private MailManager()
	{
	}

	private void CheckMailReadTip()
	{
		for (int i = 0; i < this.MailInformations.get_Count(); i++)
		{
			if (this.MailInformations.get_Item(i).mailInfo.status == 0)
			{
				this.IsMailReadTipOn = true;
				return;
			}
			if (this.MailInformations.get_Item(i).mailInfo.content.items != null && this.MailInformations.get_Item(i).mailInfo.content.items.get_Count() > 0 && this.MailInformations.get_Item(i).mailInfo.drawMark != 0)
			{
				this.IsMailReadTipOn = true;
				return;
			}
		}
		this.IsMailReadTipOn = false;
	}

	public bool CheckMailBtnActive()
	{
		bool result = false;
		if (this.IsMailReadTipOn)
		{
			result = true;
		}
		else
		{
			for (int i = 0; i < this.MailInformations.get_Count(); i++)
			{
				if (this.MailInformations.get_Item(i).mailInfo.content.items != null && this.MailInformations.get_Item(i).mailInfo.content.items.get_Count() > 0 && this.MailInformations.get_Item(i).mailInfo.drawMark != 0)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public override void Init()
	{
		base.Init();
		this.CheckMailReadTip();
	}

	public override void Release()
	{
		this.MailInformations.Clear();
		this._IsMailsReadTipOn = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<MailNotify>(new NetCallBackMethod<MailNotify>(this.OnMailNotifyRes));
		NetworkManager.AddListenEvent<SendMailRes>(new NetCallBackMethod<SendMailRes>(this.OnSendMailRes));
		NetworkManager.AddListenEvent<DrawMailAttachRes>(new NetCallBackMethod<DrawMailAttachRes>(this.OnDrawMailAttachRes));
		NetworkManager.AddListenEvent<DelMailRes>(new NetCallBackMethod<DelMailRes>(this.OnDelMailRes));
		NetworkManager.AddListenEvent<CheckMailRes>(new NetCallBackMethod<CheckMailRes>(this.OnCheckMailRes));
		NetworkManager.AddListenEvent<DrawAllMailAttachRes>(new NetCallBackMethod<DrawAllMailAttachRes>(this.OnDrawAllMailAttachRes));
	}

	public void SendMail(long receiverUID, string content)
	{
		if (MailManager.MailSendOn)
		{
			MailInfo mailInfo = new MailInfo();
			DetailInfo detailInfo = new DetailInfo();
			detailInfo.id = receiverUID;
			mailInfo.receivers.Add(detailInfo);
			mailInfo.content = new ArticleContent
			{
				text = content
			};
			NetworkManager.Send(new SendMailReq
			{
				msg = mailInfo
			}, ServerType.Data);
		}
	}

	public void SendDrawMailAttach(long mailUid)
	{
		NetworkManager.Send(new DrawMailAttachReq
		{
			id = mailUid,
			pos = 0
		}, ServerType.Data);
	}

	public void SendDelMail(long mailUid)
	{
		NetworkManager.Send(new DelMailReq
		{
			id = mailUid
		}, ServerType.Data);
	}

	public void SendCheckMail(long mailUid)
	{
		MailUIViewModel.Instance.MailDetailUID = mailUid;
		NetworkManager.Send(new CheckMailReq
		{
			id = mailUid
		}, ServerType.Data);
	}

	public void SendDrawAllMailAttach()
	{
		NetworkManager.Send(typeof(DrawAllMailAttachReq), null, ServerType.Data);
	}

	private void OnMailNotifyRes(short state, MailNotify down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.msgs.get_Count(); i++)
		{
			this.AddOne2Mails(down.msgs.get_Item(i));
		}
		if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_gameObject().get_activeSelf())
		{
			MailUIViewModel.Instance.UpdateUI(true);
		}
	}

	private void OnSendMailRes(short state, SendMailRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnDrawMailAttachRes(short state, DrawMailAttachRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText("邮件物品提取成功");
		this.UpdateMailDrawMark2HasDraw(down.id);
		this.UpdateMailStatus2HasOpen(down.id);
		this.DelOfAnim(down.id);
	}

	private void OnDelMailRes(short state, DelMailRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.DelOfAnim(down.id);
	}

	private void OnCheckMailRes(short state, CheckMailRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.UpdateMailStatus2HasOpen(down.id);
		if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_enabled())
		{
			MailUIViewModel.Instance.UpdateUI(false);
			MailUIViewModel.Instance.Update4MailDetailUI(down.id, down.info);
		}
	}

	private void OnDrawAllMailAttachRes(short state, DrawAllMailAttachRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.itemIds.get_Count() > 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502086, false));
		}
		for (int i = 0; i < down.mailIds.get_Count(); i++)
		{
			this.RemoveOne2Mails(down.mailIds.get_Item(i));
		}
		if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_enabled())
		{
			MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.list, true);
		}
	}

	private void DelOfAnim(long mailuid)
	{
		this.RemoveOne2Mails(mailuid);
		if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_enabled())
		{
			long nextUid = MailUIViewModel.Instance.GetNextMail(mailuid);
			if (MailUIViewModel.Instance.DelAnim(mailuid))
			{
				TimerHeap.AddTimer(350u, 0, delegate
				{
					if (MailUIViewModel.Instance != null && MailUIViewModel.Instance.get_enabled())
					{
						if (mailuid == MailUIViewModel.Instance.MailDetailUID && nextUid > 0L)
						{
							this.SendCheckMail(nextUid);
						}
						else
						{
							MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.list, false);
						}
					}
				});
			}
			else if (mailuid == MailUIViewModel.Instance.MailDetailUID && nextUid > 0L)
			{
				this.SendCheckMail(nextUid);
			}
			else
			{
				MailUIViewModel.Instance.ShowAs(MailUIViewModel.MailUIStatus.list, false);
			}
		}
	}

	private void AddOne2Mails(MailInfo mailInfo)
	{
		this.RemoveOne2Mails(mailInfo.id);
		this.UpdateOrAddMail(mailInfo);
		this.CheckMailReadTip();
	}

	private void RemoveOne2Mails(MailInfo mailInfo)
	{
		this.RemoveOne2Mails(mailInfo.id);
	}

	private void RemoveOne2Mails(long uid)
	{
		for (int i = 0; i < this.MailInformations.get_Count(); i++)
		{
			if (this.MailInformations.get_Item(i).mailInfo.id == uid)
			{
				this.MailInformations.RemoveAt(i);
				break;
			}
		}
		this.CheckMailReadTip();
	}

	public MailInfo FindMailInfoByUID(long uid)
	{
		for (int i = 0; i < this.MailInformations.get_Count(); i++)
		{
			if (this.MailInformations.get_Item(i).mailInfo.id == uid)
			{
				return this.MailInformations.get_Item(i).mailInfo;
			}
		}
		return null;
	}

	private void UpdateMailDrawMark2HasDraw(long uid)
	{
		MailInfo mailInfo = this.FindMailInfoByUID(uid);
		if (mailInfo != null)
		{
			mailInfo.drawMark = 0;
		}
	}

	private void UpdateMailStatus2HasOpen(long uid)
	{
		MailInfo mailInfo = this.FindMailInfoByUID(uid);
		if (mailInfo != null)
		{
			mailInfo.status = 1;
		}
		this.CheckMailReadTip();
	}

	private void UpdateOrAddMail(MailInfo mailInfo)
	{
		bool flag = false;
		for (int i = 0; i < this.MailInformations.get_Count(); i++)
		{
			if (this.MailInformations.get_Item(i).mailInfo.id == mailInfo.id)
			{
				flag = true;
				this.MailInformations.get_Item(i).mailInfo = mailInfo;
				this.MailInformations.get_Item(i).ResetTimeCountDown(mailInfo.timeoutSec);
				break;
			}
		}
		if (!flag)
		{
			MailInformation mailInformation = new MailInformation();
			mailInformation.mailInfo = mailInfo;
			mailInformation.ResetTimeCountDown(mailInfo.timeoutSec);
			this.MailInformations.Add(mailInformation);
		}
	}
}
