using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TeamApplicationItem : BaseUIBehaviour
{
	private Image m_headIcon;

	private MemberResume memberInfo;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnRefuse").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefuse);
		base.FindTransform("BtnAccept").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAccept);
		this.m_headIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
	}

	public void RefreshUI(MemberResume memberInfo)
	{
		this.memberInfo = memberInfo;
		if (memberInfo == null)
		{
			return;
		}
		int vipLv = memberInfo.vipLv;
		long fighting = memberInfo.fighting;
		int level = memberInfo.level;
		base.FindTransform("level").GetComponent<Text>().set_text(level.ToString());
		base.FindTransform("name").GetComponent<Text>().set_text(memberInfo.name.ToString());
		base.FindTransform("fighting").GetComponent<Text>().set_text(fighting.ToString());
		Image component = base.FindTransform("VIPLevel1").GetComponent<Image>();
		Image component2 = base.FindTransform("VIPLevel2").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetNumIcon10(memberInfo.vipLv, NumType.Yellow_light));
		ResourceManager.SetSprite(component2, GameDataUtils.GetNumIcon1(memberInfo.vipLv, NumType.Yellow_light));
		ResourceManager.SetSprite(this.m_headIcon, UIUtils.GetRoleSmallIcon((int)memberInfo.career));
	}

	private void OnClickRefuse(GameObject go)
	{
		if (this.memberInfo != null)
		{
			TeamBasicManager.Instance.SendLeaderProcessAppointReq(this.memberInfo.roleId, false);
		}
	}

	private void OnClickAccept(GameObject go)
	{
		if (this.memberInfo != null)
		{
			TeamBasicManager.Instance.SendLeaderProcessAppointReq(this.memberInfo.roleId, true);
		}
	}
}
