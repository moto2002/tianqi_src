using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiInviteItem : BaseUIBehaviour
{
	private Image m_spVIPLevel1;

	private Image m_spVIPLevel2;

	private InviteData role;

	private int second;

	private float espacTime;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_spVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<Image>();
		this.m_spVIPLevel2 = base.FindTransform("VIPLevel2").GetComponent<Image>();
	}

	private void Start()
	{
		base.FindTransform("InviteButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickInvite);
		EventDispatcher.AddListener<InviteRoleRes>(EventNames.MultiInviteItemUpdate, new Callback<InviteRoleRes>(this.OnUpdteItemInvited));
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		EventDispatcher.RemoveListener<InviteRoleRes>(EventNames.MultiInviteItemUpdate, new Callback<InviteRoleRes>(this.OnUpdteItemInvited));
	}

	private void OnUpdteItemInvited(InviteRoleRes Invite)
	{
		if (this.role.role.roleId == Invite.roleId && !this.role.isInvited)
		{
			this.role.isInvited = true;
			int num = Invite.cdTime * 1000;
			TeamBasicManager.Instance.AddTime(this.role.role.roleId, num);
			this.InitCdTime(num);
		}
	}

	private void OnClickInvite(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			TeamBasicManager.Instance.SendInvitePartnerReq(this.role.role.roleId);
		}
	}

	private void OnUpdteItem(bool invited)
	{
		if (invited)
		{
			base.FindTransform("InviteButton").GetComponent<ButtonCustom>().set_enabled(false);
			ImageColorMgr.SetImageColor(base.FindTransform("InviteButton").GetComponent<Image>(), true);
			ImageColorMgr.SetImageColor(base.FindTransform("TextImg").GetComponent<Image>(), true);
		}
		else
		{
			base.FindTransform("InviteButton").GetComponent<ButtonCustom>().set_enabled(true);
			ImageColorMgr.SetImageColor(base.FindTransform("InviteButton").GetComponent<Image>(), false);
			ImageColorMgr.SetImageColor(base.FindTransform("TextImg").GetComponent<Image>(), false);
		}
	}

	public void UpdateRow(InviteData row)
	{
		if (row.role != null)
		{
			this.role = row;
			Image component = base.FindTransform("headIcon").GetComponent<Image>();
			ResourceManager.SetSprite(component, UIUtils.GetRoleSmallIcon((int)row.role.career));
			base.FindTransform("roleName").GetComponent<Text>().set_text(this.role.role.name);
			base.FindTransform("fightValue").GetComponent<Text>().set_text(string.Empty + this.role.role.fighting.ToString());
			base.FindTransform("level").GetComponent<Text>().set_text("Lv" + this.role.role.level.ToString());
			ResourceManager.SetSprite(this.m_spVIPLevel1, GameDataUtils.GetNumIcon10(row.role.vipLv, NumType.Yellow));
			ResourceManager.SetSprite(this.m_spVIPLevel2, GameDataUtils.GetNumIcon1(row.role.vipLv, NumType.Yellow));
			this.InitCdTime(this.role.cdTime);
		}
	}

	private void InitCdTime(int remainingTime)
	{
		this.OnUpdteItem(this.role.isInvited);
		this.espacTime = (float)(remainingTime % 1000) * 0.001f;
		this.second = ((this.espacTime <= 0f) ? Mathf.FloorToInt((float)remainingTime * 0.001f) : Mathf.CeilToInt((float)remainingTime * 0.001f));
		base.FindTransform("InviteText").GetComponent<Text>().set_text((this.second <= 0) ? "邀 请" : this.second.ToString());
	}

	private void Update()
	{
		if (!this.role.isInvited)
		{
			return;
		}
		if (this.second >= 0)
		{
			this.espacTime += Time.get_deltaTime();
			if (this.espacTime > 1f)
			{
				this.espacTime -= (float)((int)this.espacTime);
				this.second--;
				base.FindTransform("InviteText").GetComponent<Text>().set_text(this.second.ToString());
			}
		}
		else
		{
			this.StopTimer();
		}
	}

	private void StopTimer()
	{
		this.role.isInvited = false;
		TeamBasicManager.Instance.RemoveRoleCd(this.role.role.roleId);
		this.OnUpdteItem(this.role.isInvited);
		base.FindTransform("InviteText").GetComponent<Text>().set_text("邀 请");
	}
}
