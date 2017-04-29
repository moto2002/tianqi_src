using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildInfoItem : BaseUIBehaviour
{
	private GuildBriefInfo guildBriefInfo;

	private Text guildName;

	private Text guildLv;

	private Text guildNotice;

	private Text guildNum;

	private Text guildRoleMinLv;

	private ButtonCustom btnJoin;

	private TimeCountDown timeCountDown;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.guildName = base.FindTransform("name").GetComponent<Text>();
		this.guildNotice = base.FindTransform("notice").GetComponent<Text>();
		this.guildNum = base.FindTransform("MemberNum").GetComponent<Text>();
		this.guildLv = base.FindTransform("Level").GetComponent<Text>();
		this.guildRoleMinLv = base.FindTransform("roleminLV").GetComponent<Text>();
		this.btnJoin = base.FindTransform("BtnJoin").GetComponent<ButtonCustom>();
		this.btnJoin.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnJoin);
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<long, int>(EventNames.UpdateGuildInfoCoolDown, new Callback<long, int>(this.RefreshCoolDown));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<long, int>(EventNames.UpdateGuildInfoCoolDown, new Callback<long, int>(this.RefreshCoolDown));
	}

	protected override void OnDestroy()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
		base.OnDestroy();
	}

	public void RefreshUI(GuildBriefInfo guildInfo, bool isCanShowJoin = true)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (guildInfo == null)
		{
			return;
		}
		this.guildBriefInfo = guildInfo;
		this.guildName.set_text(guildInfo.name);
		this.guildNotice.set_text("公告：" + guildInfo.notice);
		base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515032, false));
		GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(guildInfo.lv);
		if (gongHuiDengJi == null)
		{
			return;
		}
		this.guildNum.set_text(guildInfo.size + "/" + gongHuiDengJi.member);
		this.guildLv.set_text("Lv" + guildInfo.lv + string.Empty);
		Image component = base.FindTransform("ImageIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetIcon(gongHuiDengJi.icon));
		this.SetBtnJoinState(isCanShowJoin);
		this.guildRoleMinLv.set_text(string.Empty);
		if (isCanShowJoin)
		{
			this.guildRoleMinLv.set_text(guildInfo.roleMinLv.ToString() + "级可加入");
			this.RefreshCoolDown(this.guildBriefInfo.guildId, this.guildBriefInfo.applicantCd);
		}
	}

	private void SetBtnJoinState(bool isCanShowJoin = true)
	{
		if (this.btnJoin.get_gameObject().get_activeSelf() != isCanShowJoin)
		{
			this.btnJoin.get_gameObject().SetActive(isCanShowJoin);
		}
		if (isCanShowJoin)
		{
			GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(this.guildBriefInfo.lv);
			if (gongHuiDengJi != null)
			{
				if (this.guildBriefInfo.size >= gongHuiDengJi.member)
				{
					ImageColorMgr.SetImageColor(this.btnJoin.GetComponent<Image>(), true);
					this.btnJoin.GetComponent<ButtonCustom>().set_enabled(false);
				}
				else
				{
					ImageColorMgr.SetImageColor(this.btnJoin.GetComponent<Image>(), false);
					this.btnJoin.GetComponent<ButtonCustom>().set_enabled(true);
				}
			}
		}
	}

	private void OnClickBtnJoin(GameObject go)
	{
		if (this.guildBriefInfo != null)
		{
			if (this.guildBriefInfo.roleMinLv > EntityWorld.Instance.EntSelf.Lv)
			{
				string tipContentByKey = GuildManager.Instance.GetTipContentByKey("lvless");
				UIManagerControl.Instance.ShowToastText(tipContentByKey);
				return;
			}
			GuildManager.Instance.SendMakeAnApplicationForAGuild(this.guildBriefInfo.guildId);
		}
	}

	private void RefreshCoolDown(long guildID, int CoolDown)
	{
		if (guildID == this.guildBriefInfo.guildId && CoolDown > 0)
		{
			if (this.timeCountDown != null)
			{
				this.timeCountDown.Dispose();
				this.timeCountDown = null;
			}
			ImageColorMgr.SetImageColor(this.btnJoin.GetComponent<Image>(), true);
			this.btnJoin.GetComponent<ButtonCustom>().set_enabled(false);
			base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(CoolDown.ToString());
			this.timeCountDown = new TimeCountDown(CoolDown, TimeFormat.SECOND, delegate
			{
				if (this.timeCountDown != null && base.get_transform() != null)
				{
					base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(this.timeCountDown.GetSeconds().ToString());
				}
			}, delegate
			{
				if (this.timeCountDown != null && base.get_transform() != null)
				{
					ImageColorMgr.SetImageColor(this.btnJoin.GetComponent<Image>(), false);
					this.btnJoin.GetComponent<ButtonCustom>().set_enabled(true);
					base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515032, false));
				}
			}, true);
		}
	}
}
