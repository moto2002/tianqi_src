using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildAppointmentTipUI : UIBase
{
	private MemberInfo memberInfo;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("GuildChairman").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildChairman);
		base.FindTransform("GuildViceChairman").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildViceChairman);
		base.FindTransform("GuildManager").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildManager);
		base.FindTransform("GuildNormal").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildNormal);
		base.FindTransform("GuildChairman").FindChild("PositionText").GetComponent<Text>().set_text(GuildManager.Instance.GetTitleName(MemberTitleType.MTT.Chairman));
		base.FindTransform("GuildViceChairman").FindChild("PositionText").GetComponent<Text>().set_text(GuildManager.Instance.GetTitleName(MemberTitleType.MTT.ViceChairman));
		base.FindTransform("GuildManager").FindChild("PositionText").GetComponent<Text>().set_text(GuildManager.Instance.GetTitleName(MemberTitleType.MTT.Manager));
		base.FindTransform("GuildNormal").FindChild("PositionText").GetComponent<Text>().set_text(GuildManager.Instance.GetTitleName(MemberTitleType.MTT.Normal));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	private void OnClickGuildChairman(GameObject go)
	{
		if (!GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfChairman))
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("authority");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		if (this.memberInfo != null)
		{
			string content = "您确定将会长一职转让给" + this.memberInfo.name + "吗？";
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("提 示", content, null, delegate
			{
				GuildManager.Instance.SendAppointMemberReq(this.memberInfo.roleId, MemberTitleType.MTT.Chairman);
				this.Show(false);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	private void OnClickGuildViceChairman(GameObject go)
	{
		if (!GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfViceChairman))
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("authority");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		if (this.memberInfo != null)
		{
			GuildManager.Instance.SendAppointMemberReq(this.memberInfo.roleId, MemberTitleType.MTT.ViceChairman);
			this.Show(false);
		}
	}

	private void OnClickGuildNormal(GameObject go)
	{
		if (this.memberInfo != null)
		{
			GuildManager.Instance.SendAppointMemberReq(this.memberInfo.roleId, MemberTitleType.MTT.Normal);
			this.Show(false);
		}
	}

	private void OnClickGuildManager(GameObject go)
	{
		if (!GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfManager))
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("authority");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		if (this.memberInfo != null)
		{
			GuildManager.Instance.SendAppointMemberReq(this.memberInfo.roleId, MemberTitleType.MTT.Manager);
			this.Show(false);
		}
	}

	public void Refresh(long roleID)
	{
		this.memberInfo = GuildManager.Instance.GetMember(roleID);
		if (this.memberInfo != null)
		{
			base.FindTransform("name").GetComponent<Text>().set_text("[" + this.memberInfo.name + "]当前职位为" + GuildManager.Instance.GetTitleName(this.memberInfo.title.get_Item(0)));
		}
	}
}
