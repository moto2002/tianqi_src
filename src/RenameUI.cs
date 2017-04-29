using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RenameUI : UIBase
{
	public static RenameUI Instance;

	public Text m_Content;

	public InputFieldCustom m_InputField;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		RenameUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			RenameUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_Content = base.FindTransform("Content").GetComponent<Text>();
		this.m_InputField = base.FindTransform("inputField").GetComponent<InputFieldCustom>();
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnCloseClick);
		base.FindTransform("BtnOK").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnOKClick);
		base.FindTransform("BtnCancel").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnCloseClick);
	}

	protected override void OnEnable()
	{
		this.m_InputField.set_text(string.Empty);
	}

	public void SetItem(int itemId)
	{
		this.m_Content.set_text(string.Format(GameDataUtils.GetChineseContent(519001, false), GameDataUtils.GetItemName(itemId, true, 0L)));
	}

	public void OnBtnCloseClick(GameObject go)
	{
		RenameUI.Instance.Show(false);
	}

	public void OnBtnOKClick(GameObject go)
	{
		if (this.checkName(this.m_InputField.get_text()))
		{
			RenameManager.Instance.SendRoleReNameReq(this.m_InputField.get_text());
			RenameUI.Instance.Show(false);
		}
	}

	public bool checkName(string roleName)
	{
		string empty = string.Empty;
		if (string.IsNullOrEmpty(roleName))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(519002, false), 2f, 2f);
			return false;
		}
		if (this.m_InputField.get_text().Equals(EntityWorld.Instance.EntSelf.Name))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(519003, false), 2f, 2f);
			return false;
		}
		if (roleName.IndexOf(" ") > -1)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(519004, false), 2f, 2f);
			return false;
		}
		if (WordFilter.filter(roleName, out empty, 3, true, true, "*"))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(519005, false), 2f, 2f);
			return false;
		}
		if (roleName.get_Length() > 6)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(519006, false), 2f, 2f);
			return false;
		}
		return true;
	}
}
