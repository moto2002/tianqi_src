using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildSettingUI : UIBase
{
	private InputField inputField;

	private bool isSelected;

	private int roleMinLV;

	private List<int> joinLvMinList;

	private bool IsClickArrow;

	private void Awake()
	{
		this.joinLvMinList = new List<int>();
		string value = DataReader<GongHuiXinXi>.Get("Level Limits").value;
		string[] array = value.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.joinLvMinList.Add(int.Parse(array[i]));
		}
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.inputField = base.FindTransform("InputField").GetComponent<InputField>();
		base.FindTransform("BtnSettingInvite").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSetting);
		base.FindTransform("quitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuit);
		base.FindTransform("sureBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSure);
		base.FindTransform("guildSelectLvBtnRegion").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUP);
		string value = DataReader<GongHuiXinXi>.Get("manifesto").value;
		int characterLimit = (int)float.Parse(value);
		this.inputField.set_characterLimit(characterLimit);
		this.SetTextName();
	}

	private void SetTextName()
	{
		base.FindTransform("guildRoleMinLV").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515038, false));
		base.FindTransform("guildNoticeSetting").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515037, false));
		base.FindTransform("InputField").GetComponent<InputField>().get_placeholder().GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515040, false));
		base.FindTransform("quitBtn").FindChild("GuildNoticeBtnTxt").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515046, false));
		base.FindTransform("bookmark").FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515047, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnClickMaskAction()
	{
		base.OnClickMaskAction();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void RefreshUI()
	{
		if (GuildManager.Instance.MyGuildInviteSetting != null)
		{
			this.isSelected = !GuildManager.Instance.MyGuildInviteSetting.verify;
			base.FindTransform("BtnSettingSelect").get_gameObject().SetActive(this.isSelected);
			this.roleMinLV = GuildManager.Instance.MyGuildInviteSetting.roleMinLv;
			base.FindTransform("guildRoleMinLVText").GetComponent<Text>().set_text(this.roleMinLV.ToString());
			this.inputField.set_text(GuildManager.Instance.MyGuildnfo.notice);
			this.IsClickArrow = false;
			base.FindTransform("selectList").get_gameObject().SetActive(this.IsClickArrow);
			Transform transform = base.FindTransform("selectList").FindChild("ListselectList").FindChild("Contair");
			if (transform.get_childCount() <= 0)
			{
				for (int i = 0; i < this.joinLvMinList.get_Count(); i++)
				{
					GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildButtonItem");
					instantiate2Prefab.set_name("guildItem_" + i);
					GuildButtonItem component = instantiate2Prefab.GetComponent<GuildButtonItem>();
					instantiate2Prefab.get_transform().SetParent(transform);
					instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
					instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectRoleMinLV);
					component.Refresh(this.joinLvMinList.get_Item(i));
				}
			}
			base.FindTransform("quitBtn").get_gameObject().SetActive(GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.DissolveGuild));
		}
	}

	private void OnClickSetting(GameObject go)
	{
		this.isSelected = !this.isSelected;
		base.FindTransform("BtnSettingSelect").get_gameObject().SetActive(this.isSelected);
	}

	private void OnClickSure(GameObject go)
	{
		if (!this.isSelected == GuildManager.Instance.MyGuildInviteSetting.verify && this.roleMinLV == GuildManager.Instance.MyGuildInviteSetting.roleMinLv && this.inputField.get_text() == GuildManager.Instance.MyGuildnfo.notice)
		{
			UIManagerControl.Instance.ShowToastText("没有任何改动");
			return;
		}
		string empty = string.Empty;
		if (WordFilter.filter(this.inputField.get_text(), out empty, 1, false, false, "*"))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515026, false));
			return;
		}
		GuildManager.Instance.SendInviteSettingReq(this.roleMinLV, !this.isSelected, this.inputField.get_text());
		this.Show(false);
	}

	private void OnClickQuit(GameObject go)
	{
		if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("disband");
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), tipContentByKey, null, delegate
			{
				GuildManager.Instance.SendDissolveGuildReq();
				this.Show(false);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	private void OnClickSelectRoleMinLV(GameObject go)
	{
		if (go.GetComponent<GuildButtonItem>() != null)
		{
			this.roleMinLV = go.GetComponent<GuildButtonItem>().LV;
		}
		base.FindTransform("guildRoleMinLVText").GetComponent<Text>().set_text(this.roleMinLV.ToString());
		this.IsClickArrow = false;
		base.FindTransform("selectList").get_gameObject().SetActive(this.IsClickArrow);
	}

	private void OnClickUP(GameObject go)
	{
		this.IsClickArrow = !this.IsClickArrow;
		base.FindTransform("selectList").get_gameObject().SetActive(this.IsClickArrow);
	}
}
