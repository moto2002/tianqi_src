using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UIBase
{
	public const float zi_height = 220f;

	private RectTransform m_Background0;

	private RectTransform mBottomBg;

	private Text m_lblTitleName;

	private Image m_spBtnGetBg;

	private Text m_lblTextTips;

	private Text mTxGet;

	private Text mTxGetDouble;

	private Text mTxPrice;

	private ButtonCustom mBtnGetDouble;

	private ListPool m_pool1;

	private ListPool m_pool2;

	private ListPool m_poolList;

	private Action actionGet;

	private Action<bool> doubleGet;

	private bool isHide;

	private bool isClickCallBack;

	private bool isDouble;

	public static int fx_zi_1;

	public static int fx_zi_2;

	public static bool action_taskopen;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_pool1 = base.FindTransform("Grid1").GetComponent<ListPool>();
		this.m_pool1.SetItem("RewardItem");
		this.m_pool2 = base.FindTransform("Grid2").GetComponent<ListPool>();
		this.m_pool2.SetItem("RewardItem");
		this.m_poolList = base.FindTransform("GridList").GetComponent<ListPool>();
		this.m_poolList.lineContainNum = 4;
		this.m_poolList.SetItem("RewardItem");
		this.m_lblTitleName = base.FindTransform("TitleName").GetComponent<Text>();
		this.m_spBtnGetBg = base.FindTransform("BtnGetBg").GetComponent<Image>();
		this.m_lblTextTips = base.FindTransform("TextTips").GetComponent<Text>();
		this.m_Background0 = (base.FindTransform("Background0") as RectTransform);
		this.mBottomBg = (base.FindTransform("BottomBg") as RectTransform);
		this.mTxGet = base.FindTransform("BtnGetName").GetComponent<Text>();
		this.mTxGetDouble = base.FindTransform("BtnGetDoubleName").GetComponent<Text>();
		this.mTxPrice = base.FindTransform("txPrice").GetComponent<Text>();
		this.mBtnGetDouble = base.FindTransform("BtnGetDouble").GetComponent<ButtonCustom>();
		base.FindTransform("BtnGet").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetReward);
		this.mBtnGetDouble.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetDoubleReward);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.FindTransform("TextTips").get_gameObject().SetActive(false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.Clear();
		this.CloseIfTask();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		base.OnClickMaskAction();
		if (this.isClickCallBack && this.actionGet != null)
		{
			this.actionGet.Invoke();
		}
	}

	private void OnClickGetReward(GameObject go)
	{
		if (!this.isDouble && this.actionGet != null)
		{
			this.actionGet.Invoke();
		}
		if (this.isDouble && this.doubleGet != null)
		{
			this.doubleGet.Invoke(false);
		}
		if (this.isHide)
		{
			this.Show(false);
		}
	}

	private void OnClickGetDoubleReward(GameObject go)
	{
		if (this.doubleGet != null)
		{
			this.doubleGet.Invoke(true);
		}
		if (this.isHide)
		{
			this.Show(false);
		}
	}

	public void SetRewardItem(string title, List<int> goods, List<long> goodNums, bool isClickBtnOn, bool isClickMaskCallBack, Action actionGet, List<long> goodsUids = null)
	{
		base.SetMask(0.8f, true, true);
		this.actionGet = actionGet;
		this.isClickCallBack = isClickMaskCallBack;
		this.isHide = isClickBtnOn;
		this.m_lblTitleName.set_text(title);
		ImageColorMgr.SetImageColor(this.m_spBtnGetBg, !isClickBtnOn);
		this.SetItems(goods, goodNums, goodsUids);
		this.isDouble = false;
		this.mTxGet.set_text("确 定");
		this.mBtnGetDouble.get_gameObject().SetActive(this.isDouble);
	}

	public void SetTipsText(string text)
	{
		this.m_lblTextTips.get_gameObject().SetActive(true);
		this.m_lblTextTips.set_text(text);
	}

	public void SetDoubleReward(string title, int price, List<int> goods, List<long> goodNums, Action<bool> doubleGet, string leftText, string rightText)
	{
		base.SetMask(0.8f, true, true);
		this.doubleGet = doubleGet;
		this.isClickCallBack = false;
		this.isHide = true;
		this.m_lblTitleName.set_text(title);
		ImageColorMgr.SetImageColor(this.m_spBtnGetBg, false);
		this.SetItems(goods, goodNums, null);
		this.m_Background0.set_sizeDelta(new Vector2(this.m_Background0.get_sizeDelta().x, this.m_Background0.get_sizeDelta().y + 20f));
		this.mBottomBg.set_offsetMax(new Vector2(242f, -82.3f));
		this.mBottomBg.set_offsetMin(new Vector2(-242f, 127.7f));
		this.isDouble = true;
		this.mTxGet.set_text(rightText);
		this.mTxPrice.set_text("x" + price);
		this.mTxGetDouble.set_text(leftText);
		this.mBtnGetDouble.get_gameObject().SetActive(this.isDouble);
	}

	private void Clear()
	{
		this.m_pool1.Clear();
		this.m_pool2.Clear();
		this.m_poolList.Clear();
	}

	private void SetItems(List<int> goods, List<long> goodNums, List<long> goodUids = null)
	{
		this.Clear();
		int count = goods.get_Count();
		if (count <= 8)
		{
			this.m_poolList.get_transform().get_parent().get_gameObject().SetActive(false);
			if (count <= 4)
			{
				this.m_Background0.set_sizeDelta(new Vector2(520f, 350f));
				this.m_pool1.Create(count, delegate(int index)
				{
					if (index < goods.get_Count() && index < goodNums.get_Count() && index < this.m_pool1.Items.get_Count())
					{
						long uid = 0L;
						if (goodUids != null && index < goodUids.get_Count())
						{
							uid = goodUids.get_Item(index);
						}
						this.m_pool1.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(goods.get_Item(index), goodNums.get_Item(index), uid);
					}
				});
			}
			else
			{
				this.m_Background0.set_sizeDelta(new Vector2(520f, 458f));
				int pool1num = count / 2;
				this.m_pool1.Create(pool1num, delegate(int index)
				{
					if (index < goods.get_Count() && index < goodNums.get_Count() && index < this.m_pool1.Items.get_Count())
					{
						long uid = 0L;
						if (goodUids != null && index < goodUids.get_Count())
						{
							uid = goodUids.get_Item(index);
						}
						this.m_pool1.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(goods.get_Item(index), goodNums.get_Item(index), uid);
					}
				});
				this.m_pool2.Create(count - pool1num, delegate(int index)
				{
					int num = index + pool1num;
					if (num < goods.get_Count() && num < goodNums.get_Count() && index < this.m_pool2.Items.get_Count())
					{
						long uid = 0L;
						if (goodUids != null && num < goodUids.get_Count())
						{
							uid = goodUids.get_Item(num);
						}
						this.m_pool2.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(goods.get_Item(num), goodNums.get_Item(num), uid);
					}
				});
			}
		}
		else
		{
			this.m_Background0.set_sizeDelta(new Vector2(520f, 458f));
			this.m_poolList.get_transform().get_parent().get_gameObject().SetActive(true);
			this.m_poolList.Create(count, delegate(int index)
			{
				if (index < goods.get_Count() && index < goodNums.get_Count() && index < this.m_poolList.Items.get_Count())
				{
					long uid = 0L;
					if (goodUids != null && index < goodUids.get_Count())
					{
						uid = goodUids.get_Item(index);
					}
					this.m_poolList.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(goods.get_Item(index), goodNums.get_Item(index), uid);
				}
			});
		}
		this.mBottomBg.set_offsetMax(new Vector2(242f, -82.3f));
		this.mBottomBg.set_offsetMin(new Vector2(-242f, 107.7f));
	}

	public void ShowTaskFinishedFX()
	{
		FXSpineManager.Instance.PlaySpine(804, base.FindTransform("fx_bg"), "RewardUI", 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void CloseIfTask()
	{
		if (RewardUI.action_taskopen)
		{
			RewardUI.action_taskopen = false;
			FXSpineManager.Instance.DeleteSpine(RewardUI.fx_zi_1, true);
			FXSpineManager.Instance.DeleteSpine(RewardUI.fx_zi_2, true);
			FXSpineManager.Instance.PlaySpine(1803, UINodesManager.TopUIRoot, "RewardUI", 14000, null, "UI", 0f, 220f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}
}
