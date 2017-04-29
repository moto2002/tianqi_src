using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PopButtonTabsView : UIBase
{
	private static PopButtonTabsView m_Instance;

	private Transform m_tranButtonTabs;

	private RectTransform m_rtranButtonTabsBackground;

	private List<PopButtonTab> m_listPopButtonTab = new List<PopButtonTab>();

	public static float Offset;

	public static PopButtonTabsView Instance
	{
		get
		{
			if (PopButtonTabsView.m_Instance == null)
			{
				PopButtonTabsView.m_Instance = (UIManagerControl.Instance.OpenUI("PopButtonTabs", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PopButtonTabsView);
			}
			return PopButtonTabsView.m_Instance;
		}
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.m_tranButtonTabs = base.FindTransform("ButtonTabs");
		this.m_rtranButtonTabsBackground = (base.FindTransform("ButtonTabsBackground") as RectTransform);
	}

	public void ShowPopButtonTabs(List<ButtonInfoData> buttons)
	{
		this.HideButtons();
		if (buttons != null)
		{
			for (int i = 0; i < buttons.get_Count(); i++)
			{
				PopButtonTab popButtonTab;
				if (i < this.m_listPopButtonTab.get_Count())
				{
					popButtonTab = this.m_listPopButtonTab.get_Item(i);
				}
				else
				{
					GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("PopButtonTab");
					UGUITools.SetParent(this.m_tranButtonTabs.get_gameObject(), instantiate2Prefab, false);
					popButtonTab = instantiate2Prefab.AddComponent<PopButtonTab>();
					this.m_listPopButtonTab.Add(popButtonTab);
				}
				popButtonTab.set_name("PopButtonTab" + i);
				popButtonTab.get_gameObject().SetActive(true);
				popButtonTab.SetIndex(i);
				popButtonTab.SetName(buttons.get_Item(i).buttonName);
				Action callback = buttons.get_Item(i).onCall;
				popButtonTab.SetCallBack(delegate
				{
					if (callback != null)
					{
						callback.Invoke();
					}
					if (PopButtonTabsView.Instance != null)
					{
						PopButtonTabsView.Instance.Show(false);
					}
				});
			}
			this.SetPositions(buttons.get_Count());
		}
	}

	private void SetPositions(int num)
	{
		for (int i = 0; i < num; i++)
		{
			if (i < this.m_listPopButtonTab.get_Count())
			{
				float num2 = this.CalLengthsZero2DstIndex(i - 1) + PopButtonTabsView.Offset;
				this.m_listPopButtonTab.get_Item(i).get_transform().set_localPosition(new Vector3(num2, this.m_listPopButtonTab.get_Item(i).get_transform().get_localPosition().y, 0f));
			}
		}
		this.m_rtranButtonTabsBackground.set_sizeDelta(new Vector2(this.CalLengthsZero2DstIndex(num) + 30f, 60f));
	}

	private float CalLengthsZero2DstIndex(int dstIndex)
	{
		float num = 0f;
		int num2 = 0;
		while (num2 <= dstIndex && num2 < this.m_listPopButtonTab.get_Count())
		{
			num += this.m_listPopButtonTab.get_Item(num2).WidthLength;
			num2++;
		}
		return num;
	}

	private void HideButtons()
	{
		for (int i = 0; i < this.m_listPopButtonTab.get_Count(); i++)
		{
			this.m_listPopButtonTab.get_Item(i).get_gameObject().SetActive(false);
		}
	}
}
