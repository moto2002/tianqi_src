using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCleanFinishUI : UIBase
{
	private ButtonCustom BtnFinish;

	private ScrollRect Scroll;

	private Transform Content;

	public List<GameObject> listItem = new List<GameObject>();

	private bool animating;

	private float verticalScroll;

	private float stopPercent;

	private float stopScrollPosition;

	private uint timer;

	private bool shouldShowConfirmBtn;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = false;
		this.alpha = 0.75f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BtnFinish = base.FindTransform("BtnFinish").GetComponent<ButtonCustom>();
		this.Scroll = base.FindTransform("Scroll").GetComponent<ScrollRect>();
		this.Content = base.FindTransform("Content");
		this.BtnFinish.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnFinish);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
	}

	private void Update()
	{
		if (this.animating)
		{
			ScrollRect expr_11 = this.Scroll;
			expr_11.set_verticalNormalizedPosition(expr_11.get_verticalNormalizedPosition() - this.verticalScroll * Time.get_deltaTime());
			if (this.Scroll.get_verticalNormalizedPosition() <= this.stopPercent)
			{
				this.Scroll.set_verticalNormalizedPosition(this.stopPercent);
				this.animating = false;
				if (this.shouldShowConfirmBtn)
				{
					this.shouldShowConfirmBtn = false;
					this.BtnFinish.get_gameObject().SetActive(true);
				}
			}
		}
	}

	private void OnClickBtnFinish(GameObject sender)
	{
		TimerHeap.DelTimer(this.timer);
		this.Clean();
		this.Show(false);
	}

	private void Clean()
	{
		this.animating = false;
		for (int i = 0; i < this.listItem.get_Count(); i++)
		{
			Object.Destroy(this.listItem.get_Item(i));
		}
		this.listItem.Clear();
	}

	public void AddInstanceCleanFinishItems(List<List<DropItem>> listDrops)
	{
		Debuger.Error("listDrops.Count  " + listDrops.get_Count(), new object[0]);
		this.Clean();
		if (listDrops.get_Count() > 1)
		{
			this.BtnFinish.get_gameObject().SetActive(false);
		}
		int times = listDrops.get_Count();
		int interval = 1000;
		float scrollSpeed = 15f;
		uint stopTime = 500u;
		this.timer = TimerHeap.AddTimer(0u, interval, delegate
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstanceCleanFinishItem");
			instantiate2Prefab.get_transform().SetParent(this.Content);
			instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, (float)(this.Content.get_childCount() - 1) * instantiate2Prefab.GetComponent<RectTransform>().get_sizeDelta().y));
			InstanceCleanFinishItem component = instantiate2Prefab.GetComponent<InstanceCleanFinishItem>();
			component.SetRewardItems(listDrops.get_Item(times - 1), listDrops.get_Count() - times + 1);
			this.listItem.Add(instantiate2Prefab);
			if (this.Content.get_childCount() > 1)
			{
				float y = this.Scroll.get_content().get_sizeDelta().y;
				this.stopPercent = this.stopScrollPosition / y;
				this.verticalScroll = (1f - this.stopPercent) / (float)this.Content.get_childCount() * scrollSpeed;
				TimerHeap.AddTimer(stopTime, 0, delegate
				{
					this.animating = true;
				});
			}
			else
			{
				this.Scroll.set_verticalNormalizedPosition(1f);
			}
			times--;
			if (times == 0)
			{
				this.shouldShowConfirmBtn = true;
				TimerHeap.DelTimer(this.timer);
			}
		});
	}
}
