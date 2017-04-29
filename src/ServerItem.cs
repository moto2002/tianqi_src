using System;
using UnityEngine;
using UnityEngine.UI;

public class ServerItem : BaseUIBehaviour
{
	public Image bg;

	public Text content;

	private ButtonCustom btn;

	private int index;

	private bool isSelect;

	private bool isClick;

	private string btn1 = "fenleianniu_1";

	private string btn2 = "fenleianniu_2";

	private void Start()
	{
		this.btn = base.GetComponent<ButtonCustom>();
		ButtonCustom expr_12 = this.btn;
		expr_12.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_12.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickPart));
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
		if (this.isSelect && !this.isClick)
		{
			this.isSelect = false;
			ResourceManager.SetSprite(this.bg, ResourceManager.GetIconSprite(this.btn2));
			this.content.set_color(new Color(1f, 0.843137264f, 0.549019635f));
		}
		this.isClick = false;
	}

	private void OnClickPart(GameObject go)
	{
		ServerPanel serverPanel = UIManagerControl.Instance.GetUIIfExist("ServerUI") as ServerPanel;
		if (serverPanel != null)
		{
			serverPanel.UpdateServerList(ServerPanel.Instance.GetDataByPageIndex(this.index));
			this.isClick = true;
			this.isSelect = true;
			ResourceManager.SetSprite(this.bg, ResourceManager.GetIconSprite(this.btn1));
			this.content.set_color(new Color(1f, 0.980392158f, 0.9019608f));
			EventDispatcher.Broadcast(EventNames.SERVER_SELECT);
		}
	}

	public void UpdateText(int part)
	{
		this.index = part;
		this.content.set_text(ServerPanel.Instance.GerPageStringByIndex(this.index));
	}

	public void SetSelect()
	{
		this.isSelect = true;
		this.content.set_color(new Color(1f, 0.980392158f, 0.9019608f));
		ResourceManager.SetSprite(this.bg, ResourceManager.GetIconSprite(this.btn1));
	}
}
