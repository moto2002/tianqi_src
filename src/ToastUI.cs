using System;
using System.Collections.Generic;
using UnityEngine;

public class ToastUI : UIBase
{
	public struct ToastData
	{
		public string text;

		public Color textColor;

		public float duration;

		public float delay;
	}

	private const float PER_ONE_DURATION = 0.3f;

	public Transform Pool;

	private List<ToastUIItem> listPool = new List<ToastUIItem>();

	public List<ToastUI.ToastData> listQueue = new List<ToastUI.ToastData>();

	private float timeCal;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isInterruptStick = false;
	}

	private void Awake()
	{
		this.Pool = base.FindTransform("Pool");
	}

	protected override void OnEnable()
	{
		this.listQueue.Clear();
		for (int i = 0; i < this.listPool.get_Count(); i++)
		{
			ToastUIItem toastUIItem = this.listPool.get_Item(i);
			if (!toastUIItem.Unused)
			{
				toastUIItem.Unused = true;
				toastUIItem.GetComponent<CanvasGroup>().set_alpha(0f);
				toastUIItem.get_gameObject().SetActive(false);
			}
		}
	}

	public void ShowText(string text, Color textColor, float duration, float delay)
	{
		ToastUI.ToastData toastData = default(ToastUI.ToastData);
		toastData.text = text;
		toastData.textColor = textColor;
		toastData.duration = duration;
		toastData.delay = delay;
		this.listQueue.Add(toastData);
	}

	public void ShowText(string text, float duration, float delay)
	{
		this.ShowText(text, Color.get_yellow(), duration, delay);
	}

	private void Update()
	{
		if (this.listQueue.get_Count() > 0)
		{
			this.timeCal += Time.get_deltaTime();
			if (this.timeCal >= 0.3f)
			{
				this.timeCal = 0f;
				this.PopOne();
			}
		}
	}

	private void PopOne()
	{
		ToastUI.ToastData toastData = this.listQueue.get_Item(0);
		this.DoToastOne(toastData.text, toastData.textColor, toastData.duration, toastData.delay);
		this.listQueue.RemoveAt(0);
	}

	private ToastUIItem FindIdle()
	{
		for (int i = 0; i < this.listPool.get_Count(); i++)
		{
			if (this.listPool.get_Item(i).Unused)
			{
				return this.listPool.get_Item(i);
			}
		}
		return null;
	}

	public void DoToastOne(string text, Color textColor, float duration, float delay)
	{
		ToastUIItem item = this.FindIdle();
		if (item == null)
		{
			item = ResourceManager.GetInstantiate2Prefab("ToastUIItem").GetComponent<ToastUIItem>();
			item.get_transform().SetParent(this.Pool);
			item.get_transform().set_localScale(Vector3.get_one());
			item.get_transform().set_localPosition(Vector3.get_zero());
			this.listPool.Add(item);
		}
		item.get_transform().set_localPosition(Vector3.get_zero());
		item.Text = text;
		item.Unused = false;
		item.get_gameObject().SetActive(true);
		BaseTweenAlphaBaseTime component = item.GetComponent<BaseTweenAlphaBaseTime>();
		component.TweenAlpha(1f, 0f, delay, duration, delegate
		{
			item.get_gameObject().SetActive(false);
			item.Unused = true;
		});
		BaseTweenPostion component2 = item.GetComponent<BaseTweenPostion>();
		component2.MoveTo(new Vector3(0f, 180f, 0f), 1f);
	}
}
