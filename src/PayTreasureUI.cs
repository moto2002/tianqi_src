using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayTreasureUI : UIBase
{
	public static PayTreasureUI Instance;

	public Text m_TitleName;

	public Text m_BoxInfo;

	public Text m_OriginalNum;

	public Text m_DiscountedNum;

	public Image m_OriginalIcon;

	public Image m_DiscountedIcon;

	public Transform m_ScrollviewList;

	public GameObject m_OriginalPrice;

	public GameObject m_DiscountedPrice;

	public Action CallBack;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		PayTreasureUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PayTreasureUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_TitleName = base.FindTransform("TitleName").GetComponent<Text>();
		this.m_BoxInfo = base.FindTransform("BoxInfo").GetComponent<Text>();
		this.m_ScrollviewList = base.FindTransform("ScrollviewList");
		this.m_OriginalNum = base.FindTransform("OriginalNum").GetComponent<Text>();
		this.m_DiscountedNum = base.FindTransform("DiscountedNum").GetComponent<Text>();
		this.m_OriginalIcon = base.FindTransform("OriginalIcon").GetComponent<Image>();
		this.m_DiscountedIcon = base.FindTransform("DiscountedIcon").GetComponent<Image>();
		this.m_OriginalPrice = base.FindTransform("OriginalPrice").get_gameObject();
		this.m_DiscountedPrice = base.FindTransform("DiscountedPrice").get_gameObject();
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnCloseClick);
		base.FindTransform("BtnOK").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnOKClick);
	}

	public void SetShowItem(int itemId, Action callback = null)
	{
		this.CallBack = callback;
		Items config = DataReader<Items>.Get(itemId);
		if (config == null)
		{
			PayTreasureUI.Instance.Show(false);
			return;
		}
		this.m_TitleName.set_text(GameDataUtils.GetChineseContent(config.name, false));
		for (int i = 0; i < this.m_ScrollviewList.get_childCount(); i++)
		{
			Object.Destroy(this.m_ScrollviewList.GetChild(i).get_gameObject());
		}
		if (config.effectId > 0)
		{
			List<DiaoLuo> list = DataReader<DiaoLuo>.DataList.FindAll((DiaoLuo a) => a.ruleId == config.effectId);
			if (list != null && list.get_Count() > 0)
			{
				for (int j = 0; j < list.get_Count(); j++)
				{
					DiaoLuo diaoLuo = list.get_Item(j);
					if (diaoLuo.dropType == 1)
					{
						ItemShow.ShowItem(this.m_ScrollviewList, diaoLuo.goodsId, diaoLuo.minNum, false, UINodesManager.T2RootOfSpecial, 2001);
					}
				}
			}
		}
		string text = string.Empty;
		if (config.originalPrice.get_Count() > 0)
		{
			this.m_OriginalPrice.SetActive(true);
			Items.OriginalpricePair originalpricePair = config.originalPrice.get_Item(0);
			int key = originalpricePair.key;
			ResourceManager.SetSprite(this.m_OriginalIcon, GameDataUtils.GetItemLitterIcon(key));
			this.m_OriginalNum.set_text(originalpricePair.value);
			text = originalpricePair.value;
		}
		else
		{
			this.m_OriginalPrice.SetActive(false);
		}
		if (config.discountedPrice.get_Count() > 0)
		{
			this.m_DiscountedPrice.SetActive(true);
			Items.DiscountedpricePair discountedpricePair = config.discountedPrice.get_Item(0);
			int key2 = discountedpricePair.key;
			ResourceManager.SetSprite(this.m_DiscountedIcon, GameDataUtils.GetItemLitterIcon(key2));
			this.m_DiscountedNum.set_text(discountedpricePair.value);
			text = discountedpricePair.value;
		}
		else
		{
			this.m_DiscountedPrice.SetActive(false);
		}
		this.m_BoxInfo.set_text(string.Format(GameDataUtils.GetChineseContent(517003, false), text));
	}

	public void OnBtnCloseClick(GameObject go)
	{
		PayTreasureUI.Instance.Show(false);
	}

	public void OnBtnOKClick(GameObject go)
	{
		PayTreasureUI.Instance.Show(false);
		if (this.CallBack != null)
		{
			this.CallBack.Invoke();
		}
	}
}
