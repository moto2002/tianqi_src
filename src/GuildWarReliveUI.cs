using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildWarReliveUI : UIBase
{
	private ButtonCustom ReliveUIBuyBtn;

	protected GameObject GuildWarReliveUINum0;

	protected Image GuildWarReliveUINum0Image;

	protected GameObject GuildWarReliveUINum1;

	protected Image GuildWarReliveUINum1Image;

	protected Vector3 GuildWarReliveUINumSlot0;

	protected Vector3 GuildWarReliveUINumSlot1;

	protected Vector3 GuildWarReliveUINumSlot2;

	protected int restSecond;

	private Action countDownEndCallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.GuildWarReliveUINum0 = base.FindTransform("GuildWarReliveUINum0").get_gameObject();
		this.GuildWarReliveUINum0Image = base.FindTransform("GuildWarReliveUINum0Image").GetComponent<Image>();
		this.GuildWarReliveUINum1 = base.FindTransform("GuildWarReliveUINum1").get_gameObject();
		this.GuildWarReliveUINum1Image = base.FindTransform("GuildWarReliveUINum1Image").GetComponent<Image>();
		this.GuildWarReliveUINumSlot0 = base.FindTransform("GuildWarReliveUINumSlot0").get_localPosition();
		this.GuildWarReliveUINumSlot1 = base.FindTransform("GuildWarReliveUINumSlot1").get_localPosition();
		this.GuildWarReliveUINumSlot2 = base.FindTransform("GuildWarReliveUINumSlot2").get_localPosition();
		this.ReliveUIBuyBtn = base.FindTransform("GuildWarReliveUIBuyBtn").GetComponent<ButtonCustom>();
		this.ReliveUIBuyBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickReliveBtn);
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
		base.OnDisable();
	}

	public void RefreshUI(int second, Action cdEndCallBck, bool showBuyBtn = true)
	{
		this.SetCountDown(second);
		this.countDownEndCallBack = cdEndCallBck;
		if (this.ReliveUIBuyBtn != null && this.ReliveUIBuyBtn.get_gameObject().get_activeSelf() != showBuyBtn)
		{
			this.ReliveUIBuyBtn.get_gameObject().SetActive(showBuyBtn);
		}
	}

	public void SetCountDown(int second)
	{
		this.restSecond = second;
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
			this.GuildWarReliveUINum0.SetActive(false);
			this.GuildWarReliveUINum1.get_transform().set_localPosition(this.GuildWarReliveUINumSlot0);
		}
		else
		{
			this.GuildWarReliveUINum0.SetActive(true);
			this.GuildWarReliveUINum0.get_transform().set_localPosition(this.GuildWarReliveUINumSlot1);
			this.GuildWarReliveUINum1.get_transform().set_localPosition(this.GuildWarReliveUINumSlot2);
		}
		ResourceManager.SetSprite(this.GuildWarReliveUINum0Image, ResourceManager.GetIconSprite("fight_combofont_" + second / 10));
		ResourceManager.SetSprite(this.GuildWarReliveUINum1Image, ResourceManager.GetIconSprite("fight_combofont_" + second % 10));
	}

	protected void OnClickReliveBtn(GameObject go)
	{
		int num = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("ResurrectionDiamond").value);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(515070, false), string.Format(GameDataUtils.GetChineseContent(515071, false), num), delegate
		{
		}, delegate
		{
			GuildWarManager.Instance.SendReliveInGuildWarReq();
		}, GameDataUtils.GetChineseContent(621272, false), GameDataUtils.GetChineseContent(621271, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	protected void OnCountdownEnd()
	{
		Debug.LogError("OnCountdownEnd");
		if (this.countDownEndCallBack != null)
		{
			this.countDownEndCallBack.Invoke();
		}
		this.Show(false);
	}
}
