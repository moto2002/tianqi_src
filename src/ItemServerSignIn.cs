using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemServerSignIn : BaseUIBehaviour
{
	public enum ItemServerSignInState
	{
		CanNotGetReward,
		CanGetReward,
		HaveGot
	}

	private Text TextDay;

	public ButtonCustom BtnGet;

	private Transform Scroll;

	private Transform Content;

	private Image ImageBG1;

	private Image ImageBG2;

	private Text TextBtn;

	private Transform FlagTip;

	private List<GameObject> listChildItems = new List<GameObject>();

	private List<GameObject> unuseListChildItems = new List<GameObject>();

	public OpenServer openServerCache;

	public EveryDayInfo everydayinfoCache;

	public ItemServerSignIn.ItemServerSignInState itemServerSignInState;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BtnGet = base.FindTransform("BtnGet").GetComponent<ButtonCustom>();
		this.TextDay = base.FindTransform("TextDay").GetComponent<Text>();
		this.Scroll = base.FindTransform("Scroll");
		this.Content = base.FindTransform("Content");
		this.ImageBG1 = this.BtnGet.get_transform().FindChild("ImageBG1").GetComponent<Image>();
		this.ImageBG2 = this.BtnGet.get_transform().FindChild("ImageBG2").GetComponent<Image>();
		this.TextBtn = this.BtnGet.get_transform().FindChild("Text").GetComponent<Text>();
		this.FlagTip = base.FindTransform("FlagTip");
		this.TextBtn.set_text(GameDataUtils.GetChineseContent(502209, false));
	}

	private void OnClickSignInServerItem(GameObject sender)
	{
		ItemTipUIViewModel.ShowItem(sender.GetComponent<SignInServerItem>().itemIDCache, null);
	}

	public void SetUI(EveryDayInfo dayInfo)
	{
		this.everydayinfoCache = dayInfo;
		string chineseContent = GameDataUtils.GetChineseContent(502208, false);
		this.TextDay.set_text(chineseContent.Replace("xx", dayInfo.loginDays.ToString()));
		this.itemServerSignInState = (ItemServerSignIn.ItemServerSignInState)dayInfo.status;
		ItemServerSignIn.ItemServerSignInState itemServerSignInState = this.itemServerSignInState;
		if (itemServerSignInState != ItemServerSignIn.ItemServerSignInState.CanNotGetReward)
		{
			if (itemServerSignInState != ItemServerSignIn.ItemServerSignInState.CanGetReward)
			{
				this.FlagTip.get_gameObject().SetActive(true);
				this.BtnGet.get_gameObject().SetActive(false);
				this.BtnGet.set_enabled(false);
			}
			else
			{
				this.FlagTip.get_gameObject().SetActive(false);
				this.BtnGet.get_gameObject().SetActive(true);
				this.BtnGet.set_enabled(true);
				this.ImageBG1.get_gameObject().SetActive(true);
				this.ImageBG2.get_gameObject().SetActive(false);
			}
		}
		else
		{
			this.FlagTip.get_gameObject().SetActive(false);
			this.BtnGet.get_gameObject().SetActive(true);
			this.BtnGet.set_enabled(false);
			this.ImageBG1.get_gameObject().SetActive(false);
			this.ImageBG2.get_gameObject().SetActive(true);
		}
		this.unuseListChildItems.Clear();
		this.unuseListChildItems.AddRange(this.listChildItems);
		for (int i = 0; i < this.listChildItems.get_Count(); i++)
		{
			this.listChildItems.get_Item(i).get_gameObject().SetActive(false);
		}
		this.Scroll.GetComponent<ScrollRect>().set_enabled(false);
		if (dayInfo.rewardItem != null)
		{
			int itemId = dayInfo.rewardItem.itemId;
			int count = dayInfo.rewardItem.count;
			GameObject gameObject;
			if (this.unuseListChildItems.get_Count() > 0)
			{
				gameObject = this.unuseListChildItems.get_Item(0);
				this.unuseListChildItems.RemoveAt(0);
			}
			else
			{
				gameObject = ResourceManager.GetInstantiate2Prefab("SignInServerItem");
				gameObject.get_transform().SetParent(this.Content);
				gameObject.GetComponent<RectTransform>().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
				this.listChildItems.Add(gameObject);
			}
			gameObject.get_gameObject().SetActive(true);
			Items items = DataReader<Items>.Get(itemId);
			if (items != null)
			{
				SignInServerItem component = gameObject.GetComponent<SignInServerItem>();
				component.itemIDCache = itemId;
				ResourceManager.SetSprite(component.ImageFrame, GameDataUtils.GetItemFrame(items.id));
				ResourceManager.SetSprite(component.ImageIcon, GameDataUtils.GetIcon(items.icon));
				component.ImageIcon.SetNativeSize();
				component.Text.set_text(count.ToString());
				component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSignInServerItem);
			}
		}
	}

	public void SetUI(OpenServer openServer, ItemServerSignIn.ItemServerSignInState state)
	{
		this.itemServerSignInState = state;
		this.openServerCache = openServer;
		string text = GameDataUtils.GetChineseContent(502208, false);
		text = text.Replace("xx", openServer.time.ToString());
		this.TextDay.set_text(text);
		if (this.itemServerSignInState == ItemServerSignIn.ItemServerSignInState.CanGetReward)
		{
			this.FlagTip.get_gameObject().SetActive(false);
			this.BtnGet.get_gameObject().SetActive(true);
			this.BtnGet.set_enabled(true);
			this.TextBtn.set_text(GameDataUtils.GetChineseContent(502209, false));
			this.ImageBG1.get_gameObject().SetActive(true);
			this.ImageBG2.get_gameObject().SetActive(false);
		}
		else if (this.itemServerSignInState == ItemServerSignIn.ItemServerSignInState.CanNotGetReward)
		{
			this.FlagTip.get_gameObject().SetActive(false);
			this.BtnGet.get_gameObject().SetActive(true);
			this.BtnGet.set_enabled(false);
			this.TextBtn.set_text(GameDataUtils.GetChineseContent(502209, false));
			this.ImageBG1.get_gameObject().SetActive(false);
			this.ImageBG2.get_gameObject().SetActive(true);
		}
		else
		{
			this.FlagTip.get_gameObject().SetActive(true);
			this.BtnGet.get_gameObject().SetActive(false);
			this.BtnGet.set_enabled(false);
		}
		this.unuseListChildItems.Clear();
		this.unuseListChildItems.AddRange(this.listChildItems);
		for (int i = 0; i < this.listChildItems.get_Count(); i++)
		{
			this.listChildItems.get_Item(i).get_gameObject().SetActive(false);
		}
		if (openServer.itemId.get_Count() <= 3)
		{
			this.Scroll.GetComponent<ScrollRect>().set_enabled(false);
		}
		else
		{
			this.Scroll.GetComponent<ScrollRect>().set_enabled(true);
		}
		for (int j = 0; j < openServer.itemId.get_Count(); j++)
		{
			int num = openServer.itemId.get_Item(j);
			int num2 = openServer.num.get_Item(j);
			GameObject gameObject;
			if (this.unuseListChildItems.get_Count() > 0)
			{
				gameObject = this.unuseListChildItems.get_Item(0);
				this.unuseListChildItems.RemoveAt(0);
			}
			else
			{
				gameObject = ResourceManager.GetInstantiate2Prefab("SignInServerItem");
				gameObject.get_transform().SetParent(this.Content);
				gameObject.GetComponent<RectTransform>().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
				this.listChildItems.Add(gameObject);
			}
			gameObject.get_gameObject().SetActive(true);
			Items items = DataReader<Items>.Get(num);
			if (items != null)
			{
				SignInServerItem component = gameObject.GetComponent<SignInServerItem>();
				component.itemIDCache = num;
				ResourceManager.SetSprite(component.ImageFrame, GameDataUtils.GetItemFrame(items.id));
				ResourceManager.SetSprite(component.ImageIcon, GameDataUtils.GetIcon(items.icon));
				component.ImageIcon.SetNativeSize();
				component.Text.set_text(num2.ToString());
				component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSignInServerItem);
			}
		}
	}
}
