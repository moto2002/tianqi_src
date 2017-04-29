using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FigureTime : UIBase
{
	private int time = 30;

	private bool orderAdd = true;

	private Image num1;

	private Image num2;

	private Action<bool> Over;

	private static FigureTime instance;

	private void Awake()
	{
		this.num1 = base.get_transform().FindChild("number1").GetComponent<Image>();
		this.num2 = base.get_transform().FindChild("number2").GetComponent<Image>();
	}

	[DebuggerHidden]
	private IEnumerator StartNum()
	{
		FigureTime.<StartNum>c__Iterator53 <StartNum>c__Iterator = new FigureTime.<StartNum>c__Iterator53();
		<StartNum>c__Iterator.<>f__this = this;
		return <StartNum>c__Iterator;
	}

	private void Number(int num)
	{
		if (num < 10)
		{
			if (this.num2.get_gameObject().get_activeSelf())
			{
				this.num2.get_gameObject().SetActive(false);
			}
			ResourceManager.SetSprite(this.num1, ResourceManager.GetIconSprite("peipeishuzi_" + num));
		}
		else if (num < 100)
		{
			if (!this.num2.get_gameObject().get_activeSelf())
			{
				this.num2.get_gameObject().SetActive(true);
			}
			Vector2 vector = new Vector2(this.num1.get_rectTransform().get_rect().get_width() * 0.5f + 5f, 0f);
			this.num1.get_rectTransform().set_anchoredPosition(Vector2.get_zero() - vector);
			this.num2.get_rectTransform().set_anchoredPosition(Vector2.get_zero() + vector);
			ResourceManager.SetSprite(this.num1, ResourceManager.GetIconSprite("peipeishuzi_" + num / 10));
			ResourceManager.SetSprite(this.num2, ResourceManager.GetIconSprite("peipeishuzi_" + num % 10));
		}
	}

	public void InitTime(int max, Action<bool> callBack, float alph, bool mask, bool order = true)
	{
		this.time = max;
		this.orderAdd = order;
		this.Over = callBack;
		base.SetMask(alph, mask, false);
		base.StartCoroutine(this.StartNum());
	}

	public static void CanleTimer(bool isEnd = false)
	{
		if (FigureTime.instance == null)
		{
			return;
		}
		if (FigureTime.instance.Over != null)
		{
			FigureTime.instance.Over.Invoke(isEnd);
		}
		FigureTime.CanleUI();
	}

	public static void CanleUI()
	{
		FigureTime.instance = null;
		UIManagerControl.Instance.UnLoadUIPrefab("Figure");
	}

	public static void Timer(int max, Action<bool> callBack, Transform parent, float a, bool mask, bool order = true)
	{
		if (FigureTime.instance == null)
		{
			FigureTime.instance = (UIManagerControl.Instance.OpenUI("Figure", parent, false, UIType.NonPush) as FigureTime);
		}
		FigureTime.instance.InitTime(max, callBack, a, mask, order);
	}
}
