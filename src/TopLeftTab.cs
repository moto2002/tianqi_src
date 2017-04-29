using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TopLeftTab : BaseUIBehaviour
{
	protected Text TopLeftTabsImageName;

	protected GameObject TopLeftTabsImageFG;

	protected int i = -1;

	protected Action<int> clickCallback;

	protected Action<bool> actionCallback;

	protected bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TryInit();
	}

	protected void TryInit()
	{
		if (this.isInit)
		{
			return;
		}
		this.isInit = true;
		this.TopLeftTabsImageName = base.FindTransform("TopLeftTabImageName").GetComponent<Text>();
		this.TopLeftTabsImageFG = base.FindTransform("TopLeftTabImageFG").get_gameObject();
		base.FindTransform("TopLeftTabImage").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtn);
	}

	public void SetData(int index, string name, Action<int> theClickCallback, Action<bool> theActionCallback)
	{
		this.TryInit();
		this.i = index;
		this.TopLeftTabsImageName.set_text(name);
		this.clickCallback = theClickCallback;
		this.actionCallback = theActionCallback;
	}

	public void SetClickState(bool isClick)
	{
		this.TryInit();
		if (this.TopLeftTabsImageFG.get_activeSelf() != isClick)
		{
			this.TopLeftTabsImageFG.SetActive(isClick);
		}
		if (this.actionCallback != null)
		{
			this.actionCallback.Invoke(isClick);
		}
	}

	protected void OnClickBtn(GameObject go)
	{
		if (this.clickCallback != null)
		{
			this.clickCallback.Invoke(this.i);
		}
	}
}
