using System;
using UnityEngine;
using UnityEngine.UI;

public class OwerItem : BaseUIBehaviour
{
	public Image bg;

	public Text content;

	private ButtonCustom btn;

	private bool isSelect;

	private bool isClick;

	private string btn1 = "fenleianniu_1";

	private string btn2 = "fenleianniu_2";

	private void Start()
	{
		this.btn = base.GetComponent<ButtonCustom>();
		ButtonCustom expr_12 = this.btn;
		expr_12.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_12.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickPartower));
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SERVER_SELECT, new Callback(this.OnSelectClick));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SERVER_SELECT, new Callback(this.OnSelectClick));
	}

	private void OnSelectClick()
	{
		Debuger.Info(string.Concat(new object[]
		{
			"isSelect",
			this.isSelect,
			" isClick",
			this.isClick
		}), new object[0]);
		if (this.isSelect && !this.isClick)
		{
			this.isSelect = false;
			ResourceManager.SetSprite(this.bg, ResourceManager.GetIconSprite(this.btn2));
			this.content.set_color(new Color(1f, 0.843137264f, 0.549019635f));
		}
		this.isClick = false;
	}

	private void OnClickPartower(GameObject go)
	{
		ServerPanel serverPanel = UIManagerControl.Instance.GetUIIfExist("ServerUI") as ServerPanel;
		if (serverPanel != null)
		{
			serverPanel.UpdateOwerList();
			ResourceManager.SetSprite(this.bg, ResourceManager.GetIconSprite(this.btn1));
			this.content.set_color(new Color(1f, 0.980392158f, 0.9019608f));
			this.isClick = true;
			this.isSelect = true;
			EventDispatcher.Broadcast(EventNames.SERVER_SELECT);
		}
	}
}
