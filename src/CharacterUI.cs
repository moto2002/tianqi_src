using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : UIBase
{
	public ButtonCustom Close;

	private List<ButtonCustom> listBtn = new List<ButtonCustom>();

	private RiseUI riseUI;

	private ElementUI elementUI;

	private CharactorUIBtnType current = CharactorUIBtnType.None;

	private string btn1 = "fenleianniu_3";

	private string btn2 = "fenleianniu_2";

	private void Awake()
	{
		ButtonCustom expr_06 = this.Close;
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickClose));
	}

	private void Start()
	{
		Transform transform = base.get_transform().FindChild("Buttons");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			ButtonCustom component = transform.GetChild(i).GetComponent<ButtonCustom>();
			ButtonCustom expr_26 = component;
			expr_26.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_26.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickBtn));
			this.listBtn.Add(component);
		}
		this.OpenContent(CharactorUIBtnType.Rise);
	}

	private void OnClickClose(GameObject go)
	{
		DailyTaskManager.Instance.CloseEveryDayUI();
	}

	private void OnClickBtn(GameObject go)
	{
		int type = this.listBtn.FindIndex((ButtonCustom e) => e.get_gameObject() == go);
		this.OpenContent((CharactorUIBtnType)type);
	}

	public void OpenContent(CharactorUIBtnType type)
	{
		if (type == this.current)
		{
			return;
		}
		switch (type)
		{
		case CharactorUIBtnType.Element:
			if (this.elementUI == null)
			{
				this.elementUI = (UIManagerControl.Instance.OpenUI("ElementUI", base.get_transform(), false, UIType.FullScreen) as ElementUI);
				this.elementUI.ResetUI();
			}
			else
			{
				this.elementUI.Show(true);
			}
			break;
		case CharactorUIBtnType.Rise:
			if (this.riseUI == null)
			{
				this.riseUI = (UIManagerControl.Instance.OpenUI("RiseUI", base.get_transform(), false, UIType.FullScreen) as RiseUI);
			}
			else
			{
				this.riseUI.Show(true);
			}
			break;
		}
		this.HideContent(type);
	}

	public void HideContent(CharactorUIBtnType type)
	{
		ResourceManager.SetSprite(this.listBtn.get_Item((int)type).GetComponent<Image>(), ResourceManager.GetIconSprite(this.btn1));
		if (this.current != CharactorUIBtnType.None)
		{
			ResourceManager.SetSprite(this.listBtn.get_Item((int)this.current).GetComponent<Image>(), ResourceManager.GetIconSprite(this.btn2));
			switch (this.current)
			{
			case CharactorUIBtnType.Element:
				if (this.elementUI != null)
				{
					this.elementUI.Show(false);
				}
				break;
			case CharactorUIBtnType.Rise:
				if (this.riseUI != null)
				{
					this.riseUI.Show(false);
				}
				break;
			}
		}
		this.current = type;
	}
}
