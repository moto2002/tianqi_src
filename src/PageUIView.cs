using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageUIView : UIBase
{
	public static PageUIView Instance;

	private ToggleGroup m_ToggleGroup;

	private ListHelper m_ButtonsListHelper;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		PageUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_ToggleGroup = base.FindTransform("DOTList").GetComponent<ToggleGroup>();
		this.m_ButtonsListHelper = base.FindTransform("DOTList").get_gameObject().AddComponent<ListHelper>();
		this.m_ButtonsListHelper.ParentNode = base.FindTransform("DOTList");
		this.m_ButtonsListHelper.PrefabName = "PageDot";
	}

	public void SetPage(int num, int pageIndex)
	{
		List<Component> list = this.m_ButtonsListHelper.Show<Toggle>(num);
		int num2 = 0;
		while (num2 < list.get_Count() && num2 < num)
		{
			Toggle toggle = list.get_Item(num2) as Toggle;
			toggle.set_group(this.m_ToggleGroup);
			num2++;
		}
		pageIndex = Mathf.Max(0, pageIndex);
		int num3 = 0;
		while (num3 < list.get_Count() && num3 < num)
		{
			if (num3 == pageIndex)
			{
				Toggle toggle2 = list.get_Item(num3) as Toggle;
				toggle2.set_isOn(true);
				toggle2.get_transform().FindChild("BG").get_gameObject().SetActive(false);
			}
			else
			{
				Toggle toggle3 = list.get_Item(num3) as Toggle;
				toggle3.get_transform().FindChild("BG").get_gameObject().SetActive(true);
			}
			num3++;
		}
	}
}
