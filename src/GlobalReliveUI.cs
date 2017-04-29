using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GlobalReliveUI : UIBase
{
	protected GameObject GlobalReliveUINum0;

	protected Image GlobalReliveUINum0Image;

	protected GameObject GlobalReliveUINum1;

	protected Image GlobalReliveUINum1Image;

	protected Vector3 GlobalReliveUINumSlot0;

	protected Vector3 GlobalReliveUINumSlot1;

	protected Vector3 GlobalReliveUINumSlot2;

	protected Text GlobalReliveUITip;

	protected GameObject GlobalReliveUIBuy;

	protected GameObject GlobalReliveUIBuyInfo;

	protected Text GlobalReliveUIBuyInfoText;

	protected Image GlobalReliveUIBuyInfoIcon;

	protected GameObject GlobalReliveUIExit;

	protected Vector3 GlobalReliveUIBtnSlot0;

	protected Vector3 GlobalReliveUIBtnSlot1;

	protected Vector3 GlobalReliveUIBtnSlot2;

	protected int restSecond;

	public Action countDownEndCallBack;

	public Action buyBtnCallBack;

	public Action exitBtnCallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.GlobalReliveUINum0 = base.FindTransform("GlobalReliveUINum0").get_gameObject();
		this.GlobalReliveUINum0Image = base.FindTransform("GlobalReliveUINum0Image").GetComponent<Image>();
		this.GlobalReliveUINum1 = base.FindTransform("GlobalReliveUINum1").get_gameObject();
		this.GlobalReliveUINum1Image = base.FindTransform("GlobalReliveUINum1Image").GetComponent<Image>();
		this.GlobalReliveUINumSlot0 = base.FindTransform("GlobalReliveUINumSlot0").get_localPosition();
		this.GlobalReliveUINumSlot1 = base.FindTransform("GlobalReliveUINumSlot1").get_localPosition();
		this.GlobalReliveUINumSlot2 = base.FindTransform("GlobalReliveUINumSlot2").get_localPosition();
		this.GlobalReliveUITip = base.FindTransform("GlobalReliveUITip").GetComponent<Text>();
		this.GlobalReliveUIBuy = base.FindTransform("GlobalReliveUIBuy").get_gameObject();
		this.GlobalReliveUIBuyInfo = base.FindTransform("GlobalReliveUIBuyInfo").get_gameObject();
		this.GlobalReliveUIBuyInfoText = base.FindTransform("GlobalReliveUIBuyInfoText").GetComponent<Text>();
		this.GlobalReliveUIBuyInfoIcon = base.FindTransform("GlobalReliveUIBuyInfoIcon").GetComponent<Image>();
		this.GlobalReliveUIExit = base.FindTransform("GlobalReliveUIExit").get_gameObject();
		this.GlobalReliveUIBtnSlot0 = base.FindTransform("GlobalReliveUIBtnSlot0").get_localPosition();
		this.GlobalReliveUIBtnSlot1 = base.FindTransform("GlobalReliveUIBtnSlot1").get_localPosition();
		this.GlobalReliveUIBtnSlot2 = base.FindTransform("GlobalReliveUIBtnSlot2").get_localPosition();
		base.FindTransform("GlobalReliveUIBuyBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuyBtn);
		base.FindTransform("GlobalReliveUIExitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExitBtn);
		base.FindTransform("GlobalReliveUITitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505188, false));
		base.FindTransform("GlobalReliveUIBuyBtnImageName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505186, false));
		base.FindTransform("GlobalReliveUIExitBtnImageName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505138, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OneSecondPast));
	}

	protected override void OnDisable()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OneSecondPast));
		this.countDownEndCallBack = null;
		this.buyBtnCallBack = null;
		this.exitBtnCallBack = null;
		base.OnDisable();
	}

	public void SetCountDown(int second, Action theCountdownEndCallBack)
	{
		this.restSecond = second;
		this.countDownEndCallBack = theCountdownEndCallBack;
		this.SetUISecond(this.restSecond);
		if (this.restSecond <= 0)
		{
			this.OnCountdownEnd();
		}
	}

	protected void OneSecondPast()
	{
		int num = this.restSecond;
		this.restSecond--;
		this.SetUISecond(this.restSecond);
		if (num > 0 && this.restSecond <= 0)
		{
			this.OnCountdownEnd();
		}
	}

	protected void SetUISecond(int second)
	{
		if (second > 99)
		{
			second = 99;
		}
		if (second < 0)
		{
			second = 0;
		}
		if (second < 10)
		{
			this.GlobalReliveUINum0.SetActive(false);
			this.GlobalReliveUINum1.get_transform().set_localPosition(this.GlobalReliveUINumSlot0);
		}
		else
		{
			this.GlobalReliveUINum0.SetActive(true);
			this.GlobalReliveUINum0.get_transform().set_localPosition(this.GlobalReliveUINumSlot1);
			this.GlobalReliveUINum1.get_transform().set_localPosition(this.GlobalReliveUINumSlot2);
		}
		ResourceManager.SetSprite(this.GlobalReliveUINum0Image, ResourceManager.GetIconSprite("fight_combofont_" + second / 10));
		ResourceManager.SetSprite(this.GlobalReliveUINum1Image, ResourceManager.GetIconSprite("fight_combofont_" + second % 10));
	}

	public void ShowBuyBtn(bool isShow, Action theBuyBtnCallBack = null, int itemID = 0, long count = 0L)
	{
		this.buyBtnCallBack = theBuyBtnCallBack;
		if (this.GlobalReliveUIBuy.get_activeSelf() != isShow)
		{
			this.GlobalReliveUIBuy.SetActive(isShow);
		}
		if (this.GlobalReliveUIBuy.get_activeSelf())
		{
			if (itemID > 0)
			{
				if (!this.GlobalReliveUIBuyInfo.get_activeSelf())
				{
					this.GlobalReliveUIBuyInfo.SetActive(true);
				}
				Items items = DataReader<Items>.Get(itemID);
				this.GlobalReliveUIBuyInfoText.set_text(string.Format(GameDataUtils.GetChineseContent(505187, false), count));
				ResourceManager.SetSprite(this.GlobalReliveUIBuyInfoIcon, GameDataUtils.GetIcon(items.icon));
			}
			else if (this.GlobalReliveUIBuyInfo.get_activeSelf())
			{
				this.GlobalReliveUIBuyInfo.SetActive(false);
			}
		}
		this.UpdateBtnPosition();
	}

	public void ShowExitBtn(bool isShow, Action theExitBtnCallBack = null)
	{
		this.exitBtnCallBack = theExitBtnCallBack;
		if (this.GlobalReliveUIExit.get_activeSelf() != isShow)
		{
			this.GlobalReliveUIExit.SetActive(isShow);
		}
		this.UpdateBtnPosition();
	}

	protected void UpdateBtnPosition()
	{
		if (this.GlobalReliveUIExit.get_activeSelf() && this.GlobalReliveUIBuy.get_activeSelf())
		{
			this.GlobalReliveUIExit.get_transform().set_localPosition(this.GlobalReliveUIBtnSlot1);
			this.GlobalReliveUIBuy.get_transform().set_localPosition(this.GlobalReliveUIBtnSlot2);
		}
		else if (this.GlobalReliveUIExit.get_activeSelf())
		{
			this.GlobalReliveUIExit.get_transform().set_localPosition(this.GlobalReliveUIBtnSlot0);
		}
		else if (this.GlobalReliveUIBuy.get_activeSelf())
		{
			this.GlobalReliveUIBuy.get_transform().set_localPosition(this.GlobalReliveUIBtnSlot0);
		}
	}

	public void ShowTip(bool isShow, string text = "")
	{
		if (this.GlobalReliveUITip.get_gameObject().get_activeSelf() != isShow)
		{
			this.GlobalReliveUITip.get_gameObject().SetActive(isShow);
		}
		this.GlobalReliveUITip.set_text(text);
	}

	protected void OnCountdownEnd()
	{
		Debug.LogError("OnCountdownEnd");
		if (this.countDownEndCallBack != null)
		{
			this.countDownEndCallBack.Invoke();
		}
	}

	protected void OnClickBuyBtn(GameObject go)
	{
		Debug.LogError("OnClickBuyBtn");
		if (this.buyBtnCallBack != null)
		{
			this.buyBtnCallBack.Invoke();
		}
	}

	protected void OnClickExitBtn(GameObject go)
	{
		Debug.LogError("OnClickExitBtn");
		if (this.exitBtnCallBack != null)
		{
			this.exitBtnCallBack.Invoke();
		}
	}
}
