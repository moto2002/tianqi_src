using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldBuyUI : UIBase
{
	private ButtonCustom mBtnBuy;

	private Text mTxBuy;

	private Text mTxFreeTimeNum;

	private Text mTxBuyTimeNum;

	private Text mTxGold;

	private Text mTxDiamond;

	private int mGold;

	private List<GetGoldPopup> mList;

	private Transform mPopupPool;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mBtnBuy = UIHelper.GetCustomButton(base.get_transform(), "BtnBuy");
		this.mTxBuy = UIHelper.GetText(this.mBtnBuy, "Text");
		this.mTxFreeTimeNum = UIHelper.GetText(base.get_transform(), "Panel/txFreeTimeNum");
		this.mTxBuyTimeNum = UIHelper.GetText(base.get_transform(), "Panel/txBuyTimeNum");
		this.mTxGold = UIHelper.GetText(base.get_transform(), "Panel/txGold");
		this.mTxDiamond = UIHelper.GetText(base.get_transform(), "Panel/txDiamond");
		UIHelper.GetText(base.get_transform(), "Panel/txFreeTimes").set_text(GameDataUtils.GetChineseContent(505152, false));
		UIHelper.GetText(base.get_transform(), "Panel/txBuyTimes").set_text(GameDataUtils.GetChineseContent(505153, false));
		UIHelper.GetText(base.get_transform(), "BtnCancel/Text").set_text(GameDataUtils.GetChineseContent(500012, false));
		UIHelper.GetText(base.get_transform(), "staticPanel/Header").set_text(GameDataUtils.GetChineseContent(500029, false));
		UIHelper.GetText(base.get_transform(), "Panel/txTop").set_text(GameDataUtils.GetChineseContent(505154, false));
		this.mBtnBuy.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuyGold);
		UIHelper.GetCustomButton(base.get_transform(), "BtnCancel").get_onClick().AddListener(delegate
		{
			UIManagerControl.Instance.HideUI("GoldBuyUI");
		});
		this.mList = new List<GetGoldPopup>();
		this.mPopupPool = UIHelper.GetObject(base.get_transform(), "TipsPool").get_transform();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshContent();
		base.SetAsLastSibling();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetBuyGoldRes, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.GoldBuyChangedNty, new Callback(this.RefreshContent));
		EventDispatcher.AddListener(EventNames.GoldBuyFail, new Callback(this.OnGoldBuyFail));
		EventDispatcher.AddListener(EventNames.VipTimeLimitNty, new Callback(this.RefreshContent));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetBuyGoldRes, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.GoldBuyChangedNty, new Callback(this.RefreshContent));
		EventDispatcher.RemoveListener(EventNames.GoldBuyFail, new Callback(this.OnGoldBuyFail));
		EventDispatcher.RemoveListener(EventNames.VipTimeLimitNty, new Callback(this.RefreshContent));
	}

	private void OnClickBuyGold(GameObject go)
	{
		if (GoldBuyManager.Instance.remainingBuyTimes > 0 || GoldBuyManager.Instance.remainingFreeTimes > 0)
		{
			GoldBuyManager.Instance.SendBuyGoldReq();
		}
		else
		{
			if (SystemConfig.IsOpenPay)
			{
				string chineseContent = GameDataUtils.GetChineseContent(505105, false);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), chineseContent, null, delegate
				{
					LinkNavigationManager.OpenVIPUI2Privilege();
					this.Show(false);
				}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
				return;
			}
			UIManagerControl.Instance.ShowToastText("今日购买次数已用完!!!");
		}
	}

	protected void RefreshContent()
	{
		int vipTimesByType = VIPPrivilegeManager.Instance.GetVipTimesByType(11);
		int remainingBuyTimes = GoldBuyManager.Instance.remainingBuyTimes;
		int vipTimesByType2 = VIPPrivilegeManager.Instance.GetVipTimesByType(12);
		int remainingFreeTimes = GoldBuyManager.Instance.remainingFreeTimes;
		this.mGold = DataReader<Golds>.Get(EntityWorld.Instance.EntSelf.Lv).gainGold;
		int num = 0;
		if (remainingFreeTimes <= 0)
		{
			int num2 = vipTimesByType + vipTimesByType2 - (remainingBuyTimes + remainingFreeTimes);
			if (num2 <= 0)
			{
				num2 = 1;
			}
			GouMaiCiShu gouMaiCiShu = DataReader<GouMaiCiShu>.Get(num2);
			if (gouMaiCiShu != null)
			{
				num = gouMaiCiShu.cost;
			}
		}
		this.mTxFreeTimeNum.set_text((remainingFreeTimes > 0) ? remainingFreeTimes.ToString() : "<color=#ff0000>0</color>");
		this.mTxBuyTimeNum.set_text(((remainingBuyTimes > 0) ? remainingBuyTimes.ToString() : "<color=#ff0000>0</color>") + "/" + vipTimesByType);
		this.mTxGold.set_text(this.mGold.ToString());
		this.mTxDiamond.set_text(num.ToString());
		this.mTxBuy.set_text((remainingFreeTimes <= 0) ? GameDataUtils.GetChineseContent(505126, false) : GameDataUtils.GetChineseContent(621061, false));
	}

	private void RefreshUI()
	{
		this.ShowPopup(this.mGold, GoldBuyManager.Instance.extPrize);
		this.RefreshContent();
	}

	private void ShowPopup(int gold, int ext = 1)
	{
		GetGoldPopup item = this.GetUnusedPopup();
		if (item == null)
		{
			item = ResourceManager.GetInstantiate2Prefab("GetGoldPopup").GetComponent<GetGoldPopup>();
			item.get_transform().SetParent(this.mPopupPool);
			this.mList.Add(item);
		}
		item.get_transform().set_localScale(new Vector3(0.1f, 0.1f, 0.1f));
		item.get_transform().set_localPosition(Vector3.get_zero());
		if (ext > 1)
		{
			item.topTips.set_text(string.Format("女神降下了奇迹，获得<color=#F76300FF>{0}倍</color>金币!", ext));
			item.topTips.get_transform().get_parent().get_gameObject().SetActive(true);
		}
		else
		{
			item.topTips.get_transform().get_parent().get_gameObject().SetActive(false);
		}
		item.botTips.set_text("获得:      +" + gold * ext);
		item.Unused = false;
		item.get_transform().SetAsLastSibling();
		item.get_gameObject().SetActive(true);
		BaseTweenScale bts = item.GetComponent<BaseTweenScale>();
		bts.ChangeScaleTo(new Vector3(1.2f, 1.2f, 1.2f), 0.2f, delegate
		{
			bts.ChangeScaleTo(new Vector3(1f, 1f, 1f), 0.2f, delegate
			{
				item.GetComponent<BaseTweenPostion>().MoveTo(new Vector3(0f, 220f, 0f), 1f);
				item.GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(1f, 0f, 1.5f, 1f, delegate
				{
					item.get_gameObject().SetActive(false);
					item.Unused = true;
				});
			});
		});
	}

	private GetGoldPopup GetUnusedPopup()
	{
		for (int i = 0; i < this.mList.get_Count(); i++)
		{
			if (this.mList.get_Item(i).Unused)
			{
				return this.mList.get_Item(i);
			}
		}
		return null;
	}

	private void OnGoldBuyFail()
	{
		UIManagerControl.Instance.HideUI("GoldBuyUI");
	}

	private int GetGoldTime(bool isFree = false)
	{
		int result = 0;
		if (VIPManager.Instance.LimitCardData.Times > TimeManager.Instance.PreciseServerSecond && EntityWorld.Instance.EntSelf != null)
		{
			VipDengJi vipDengJi = DataReader<VipDengJi>.Get(EntityWorld.Instance.EntSelf.VipLv);
			if (vipDengJi != null && vipDengJi.effect.get_Count() > 1)
			{
				VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(vipDengJi.effect.get_Item((!isFree) ? 4 : 5));
				if (vipXiaoGuo != null)
				{
					result = vipXiaoGuo.value1;
				}
			}
		}
		return result;
	}
}
