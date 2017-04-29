using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildApplicationItem : BaseUIBehaviour
{
	private ApplicantInfo applicationInfo;

	private Image m_headIcon;

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

	public void RefreshUI(ApplicantInfo applicationInfo)
	{
		this.applicationInfo = applicationInfo;
		base.FindTransform("level").GetComponent<Text>().set_text(applicationInfo.lv.ToString());
		base.FindTransform("name").GetComponent<Text>().set_text(applicationInfo.name.ToString());
		base.FindTransform("fighting").GetComponent<Text>().set_text(applicationInfo.fighting.ToString());
		base.FindTransform("vipLV").GetComponent<Text>().set_text(applicationInfo.vipLv.ToString());
		ResourceManager.SetSprite(this.m_headIcon, UIUtils.GetRoleSmallIcon(applicationInfo.career));
	}

	private void OnClickRefuse(GameObject go)
	{
		if (this.applicationInfo != null)
		{
			List<long> list = new List<long>();
			list.Add(this.applicationInfo.roleId);
			GuildManager.Instance.SendRefuseGuildApplicant(list);
			if (GuildManager.Instance.ApplicationPlayers != null)
			{
				int num = GuildManager.Instance.ApplicationPlayers.FindIndex((ApplicantInfo a) => a.roleId == this.applicationInfo.roleId);
				if (num >= 0)
				{
					GuildManager.Instance.ApplicationPlayers.RemoveAt(num);
				}
			}
		}
	}

	private void OnClickAccept(GameObject go)
	{
		if (this.applicationInfo != null)
		{
			List<long> list = new List<long>();
			list.Add(this.applicationInfo.roleId);
			GuildManager.Instance.SendAcceptGuildApplicant(list);
			if (GuildManager.Instance.ApplicationPlayers != null)
			{
				int num = GuildManager.Instance.ApplicationPlayers.FindIndex((ApplicantInfo a) => a.roleId == this.applicationInfo.roleId);
				if (num >= 0)
				{
					GuildManager.Instance.ApplicationPlayers.RemoveAt(num);
				}
			}
		}
	}
}
