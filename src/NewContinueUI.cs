using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NewContinueUI : UIBase
{
	private enum OpenModel
	{
		Normal,
		Confilrm,
		OnlyPrompt
	}

	private ButtonCustom btnOKMid;

	private ButtonCustom btnOK;

	private ButtonCustom btnCancel;

	private Action okAction;

	private Action cancelAction;

	private Text okName;

	private Text cancelName;

	private Text title;

	private Text content;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.btnOKMid = base.FindTransform("BtnOKMid").GetComponent<ButtonCustom>();
		this.btnOK = base.FindTransform("BtnOK").GetComponent<ButtonCustom>();
		this.btnCancel = base.FindTransform("BtnCancel").GetComponent<ButtonCustom>();
		this.title = base.FindTransform("Title").GetComponent<Text>();
		this.content = base.FindTransform("Content").GetComponent<Text>();
		this.okName = base.FindTransform("OK").GetComponent<Text>();
		this.cancelName = base.FindTransform("Cancel").GetComponent<Text>();
		this.btnOKMid.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOK);
		this.btnOK.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOK);
		this.btnCancel.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCancel);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
	}

	private void OnClickOK(GameObject go)
	{
		this.Show(false);
		if (this.okAction != null)
		{
			this.okAction.Invoke();
		}
	}

	private void OnClickCancel(GameObject go)
	{
		this.Show(false);
		if (this.cancelAction != null)
		{
			this.cancelAction.Invoke();
		}
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public static void OpenAsNormal(string title, string content, Action ok_action, Action cancel_action, string okName = "确 定", string cancelName = "取 消")
	{
		NewContinueUI newContinueUI = UIManagerControl.Instance.OpenUI("NewContinueUI", UINodesManager.T4RootOfSpecial, false, UIType.NonPush) as NewContinueUI;
		newContinueUI.NormalSet(title, content, ok_action, cancel_action, okName, cancelName);
	}

	public void NormalSet(string _title, string _content, Action ok_action, Action cancel_action, string ok_Name, string cancel_Name)
	{
		this.btnOKMid.get_gameObject().SetActive(false);
		this.btnOK.get_gameObject().SetActive(true);
		this.btnCancel.get_gameObject().SetActive(true);
		this.okAction = ok_action;
		this.cancelAction = cancel_action;
		this.title.set_text(_title);
		this.content.set_text(_content);
		this.okName.set_text(ok_Name);
		this.cancelName.set_text(cancel_Name);
	}

	public static void OpenAsOnlyConfilrm(string title, string content, Action ok_action, string okName = "确 定")
	{
		NewContinueUI newContinueUI = UIManagerControl.Instance.OpenUI("NewContinueUI", UINodesManager.T4RootOfSpecial, false, UIType.NonPush) as NewContinueUI;
		newContinueUI.OnlyConfilrmSet(title, content, ok_action, okName);
	}

	public void OnlyConfilrmSet(string _title, string _content, Action ok_action, string ok_Name)
	{
		this.btnOKMid.get_gameObject().SetActive(true);
		this.btnOK.get_gameObject().SetActive(false);
		this.btnCancel.get_gameObject().SetActive(false);
		this.okAction = ok_action;
		this.title.set_text(_title);
		this.content.set_text(_content);
		this.okName.set_text(ok_Name);
	}

	public static void OpenAsOnlyPrompt(string title, string content)
	{
		NewContinueUI newContinueUI = UIManagerControl.Instance.OpenUI("NewContinueUI", UINodesManager.T4RootOfSpecial, false, UIType.NonPush) as NewContinueUI;
		newContinueUI.OnlyPromptSet(title, content);
	}

	public void OnlyPromptSet(string _title, string _content)
	{
		this.btnOKMid.get_gameObject().SetActive(false);
		this.btnOK.get_gameObject().SetActive(false);
		this.btnCancel.get_gameObject().SetActive(false);
		this.title.set_text(_title);
		this.content.set_text(_content);
	}
}
