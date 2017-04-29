using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;

public class SCItemPrizeUI : UIBase
{
	public Transform Items;

	public ButtonCustom ButtonOK;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.ButtonOK.onClickCustom = delegate(GameObject go)
		{
			this.Show(false);
		};
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.SCItemPrice();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.SCItemPrice();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SCItemPrice, new Callback(this.SCItemPrice));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SCItemPrice, new Callback(this.SCItemPrice));
	}

	private void SCItemPrice()
	{
		for (int i = 0; i < this.Items.get_childCount(); i++)
		{
			Object.Destroy(this.Items.GetChild(i).get_gameObject());
		}
		TiaoZhanBoCi currentInfo = SurvivalManager.Instance.GetCurrentInfo();
		for (int j = 0; j < currentInfo.currencyType.get_Count(); j++)
		{
			ItemShow.ShowItem(this.Items, currentInfo.currencyType.get_Item(j), (long)currentInfo.currencyNum.get_Item(j), false, null, 2001);
		}
	}
}
