using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GrowUpPlanUI : UIBase
{
	public static GrowUpPlanUI Instance;

	private ButtonCustom buyBtn;

	private Text textVipLevel;

	private Text textBuyDiamond;

	private int canVipLv;

	private GridLayoutGroup m_awardlist;

	private void Start()
	{
	}

	private void Awake()
	{
		GrowUpPlanUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.textVipLevel = base.FindTransform("ImageVip").get_transform().FindChild("TextVipLevel").GetComponent<Text>();
		this.textBuyDiamond = base.FindTransform("BtnBuy").get_transform().FindChild("TextDiamond").GetComponent<Text>();
		this.m_awardlist = base.FindTransform("Content").FindChild("Grid").GetComponent<GridLayoutGroup>();
		this.buyBtn = base.FindTransform("BtnBuy").GetComponent<ButtonCustom>();
		this.buyBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBuy);
		this.UpdateBuyBtn(true);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void ActionClose()
	{
		base.ActionClose();
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
		EventDispatcher.AddListener(EventNames.OnGetSignChangedNty, new Callback(this.OnGetSignChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetSignChangedNty, new Callback(this.OnGetSignChangedNty));
	}

	private void OnGetSignChangedNty()
	{
	}

	private void OnClickExit()
	{
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickBtnBuy(GameObject go)
	{
		if (EntityWorld.Instance.EntSelf.VipLv < this.canVipLv)
		{
			int needDiamondsToVIP = VIPManager.Instance.GetNeedDiamondsToVIP(this.canVipLv);
			string content = string.Format(GameDataUtils.GetChineseContent(513171, false), needDiamondsToVIP, this.canVipLv);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, delegate
			{
				LinkNavigationManager.OpenVIPUI2Privilege();
			}, delegate
			{
				DiamondBuyUIView.Instance.Show(false);
			}, GameDataUtils.GetChineseContent(513181, false), GameDataUtils.GetChineseContent(513182, false), "button_orange_1", "button_yellow_1", null, true, true);
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(513178, false), this.canVipLv));
			return;
		}
		string chineseContent = GameDataUtils.GetChineseContent(621264, false);
		string content2 = "是否购买成长计划？";
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, content2, delegate
		{
		}, delegate
		{
			GrowUpPlanManager.Instance.SendBuyPlanReq();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	private static int GetButtonState(bool hasGetPrize, bool canGetFlag)
	{
		int result = 1;
		if (hasGetPrize)
		{
			result = 3;
		}
		else if (canGetFlag)
		{
			result = 2;
		}
		return result;
	}

	private GrowthPlanListPush DataSort(GrowthPlanListPush down)
	{
		down.item.Sort(delegate(GrowthPlanListPush.Items a, GrowthPlanListPush.Items b)
		{
			int buttonState = GrowUpPlanUI.GetButtonState(a.hasGetPrize, a.canGetFlag);
			int buttonState2 = GrowUpPlanUI.GetButtonState(b.hasGetPrize, b.canGetFlag);
			int result = 0;
			if (buttonState != buttonState2)
			{
				if (buttonState == 2)
				{
					result = -1;
				}
				else if (buttonState == 1 && buttonState2 != 2)
				{
					result = -1;
				}
			}
			else
			{
				result = a.roleLv.CompareTo(b.roleLv);
			}
			return result;
		});
		return down;
	}

	private void UpdatePanel()
	{
		GrowthPlanListPush growthPlanListPush = GrowUpPlanManager.Instance.GetGrowUpPlanData();
		if (growthPlanListPush == null)
		{
			return;
		}
		if (growthPlanListPush.item == null)
		{
			return;
		}
		this.UpdateBuyBtn(!growthPlanListPush.hasBuy);
		this.canVipLv = growthPlanListPush.vipLv;
		string text = string.Format(GameDataUtils.GetChineseContent(513179, false), growthPlanListPush.vipLv);
		this.textVipLevel.set_text(text);
		this.textBuyDiamond.set_text("x" + growthPlanListPush.price);
		growthPlanListPush = this.DataSort(growthPlanListPush);
		this.ClearScroll();
		for (int i = 0; i < growthPlanListPush.item.get_Count(); i++)
		{
			this.AddScrollCell(i, growthPlanListPush.item.get_Item(i), growthPlanListPush.typeId);
		}
	}

	private void AddScrollCell(int index, GrowthPlanListPush.Items info, int typeId)
	{
		Transform transform = this.m_awardlist.get_transform().FindChild("GrowUpPlanItem" + index);
		int buttonState = GrowUpPlanUI.GetButtonState(info.hasGetPrize, info.canGetFlag);
		ChengChangJiHua chengChangJiHua = DataReader<ChengChangJiHua>.Get(info.roleLv);
		GrowUpPlanDataUnite itemData = new GrowUpPlanDataUnite
		{
			typeId = typeId,
			itemId = chengChangJiHua.ItemId,
			count = chengChangJiHua.ItemNum,
			condition = info.roleLv,
			state = buttonState
		};
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<GrowUpPlanItem>().UpdateItem(itemData);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GrowUpPlanItem");
			instantiate2Prefab.get_transform().SetParent(this.m_awardlist.get_transform(), false);
			instantiate2Prefab.set_name("GrowUpPlanItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<GrowUpPlanItem>().UpdateItem(itemData);
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_awardlist.get_transform().get_childCount(); i++)
		{
			this.m_awardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void ScrollToAvailableCell()
	{
		GrowthPlanListPush growUpPlanData = GrowUpPlanManager.Instance.GetGrowUpPlanData();
		RectTransform component = this.m_awardlist.GetComponent<RectTransform>();
		component.set_localPosition(new Vector3(0f, 0f, 0f)
		{
			y = (float)(-(float)growUpPlanData.item.get_Count()) * (this.m_awardlist.get_cellSize().y + this.m_awardlist.get_spacing().y)
		});
	}

	private void UpdateBuyBtn(bool isShow)
	{
		this.buyBtn.set_enabled(isShow);
		this.buyBtn.get_transform().FindChild("ImageLight").get_gameObject().SetActive(isShow);
		this.buyBtn.get_transform().FindChild("TextBuy").get_gameObject().SetActive(isShow);
		this.buyBtn.get_transform().FindChild("ImageDiamond").get_gameObject().SetActive(isShow);
		this.buyBtn.get_transform().FindChild("TextDiamond").get_gameObject().SetActive(isShow);
		this.buyBtn.get_transform().FindChild("ImageGrey").get_gameObject().SetActive(!isShow);
		this.buyBtn.get_transform().FindChild("TextHaveBuy").get_gameObject().SetActive(!isShow);
	}

	public void RefreshUI()
	{
		this.UpdatePanel();
	}
}
