using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayGiftUI : UIBase
{
	private ListPool m_ItemPool;

	private List<int> m_spineIds = new List<int>();

	private Image btnImg1;

	private Image btnImg2;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_ItemPool = base.FindTransform("Items").GetComponent<ListPool>();
		this.m_ItemPool.SetItem("ItemShow");
		this.btnImg1 = base.FindTransform("Imagego1").GetComponent<Image>();
		this.btnImg2 = base.FindTransform("Imagego2").GetComponent<Image>();
	}

	private void Start()
	{
		ButtonCustom expr_10 = base.FindTransform("ButtonGet").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickGet));
		ButtonCustom expr_41 = base.FindTransform("ClosedButton").GetComponent<ButtonCustom>();
		expr_41.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_41.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClose));
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.SetItems();
		this.UpdateBtnState();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		for (int i = 0; i < this.m_spineIds.get_Count(); i++)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_spineIds.get_Item(i), true);
		}
		this.m_spineIds.Clear();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickGet(GameObject go)
	{
		this.OnClickMaskAction();
		EventDispatcher.Broadcast<int>(EventNames.OpenActivityUI, -1);
	}

	private void OnClose(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	public void UpdateBtnState()
	{
		bool active = false;
		bool active2 = false;
		switch (DayGiftManager.Instance.State)
		{
		case 0:
			active2 = true;
			break;
		case 1:
			active = true;
			break;
		case 2:
			active = true;
			break;
		}
		this.btnImg1.get_gameObject().SetActive(active);
		this.btnImg2.get_gameObject().SetActive(active2);
	}

	private void SetItems()
	{
		MinorLoginNty info = DayGiftManager.Instance.GetRewardItems();
		if (info == null || info.items == null)
		{
			return;
		}
		this.m_ItemPool.Create(info.items.get_Count(), delegate(int index)
		{
			if (index < info.items.get_Count() && index < this.m_ItemPool.Items.get_Count())
			{
				ItemShow.SetItem(this.m_ItemPool.Items.get_Item(index), info.items.get_Item(index).cfgId, info.items.get_Item(index).count, false, UINodesManager.T2RootOfSpecial, 14000);
			}
			if (index == this.m_ItemPool.Items.get_Count() - 1)
			{
				this.PlaySpineOfItems();
			}
		});
	}

	private void PlaySpineOfItems()
	{
		int num = FXSpineManager.Instance.PlaySpine(605, this.m_ItemPool.Items.get_Item(0).get_transform(), "DayGiftUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.m_spineIds.Add(num);
	}
}
