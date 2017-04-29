using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenDayUI : UIBase
{
	public enum ItemServerSignInState
	{
		CanNotGetReward,
		CanGetReward,
		HaveGot
	}

	private int m_Day2ItemFxId;

	private int m_Day5ItemFxId;

	private List<int> FxUidList = new List<int>();

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		ButtonCustom expr_18 = base.FindTransform("BtnGet").GetComponent<ButtonCustom>();
		expr_18.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_18.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnBtnGet));
		ButtonCustom expr_49 = base.FindTransform("BtnClose").GetComponent<ButtonCustom>();
		expr_49.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_49.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnBtnClose));
		this.ClearFx();
		for (int i = 1; i <= 7; i++)
		{
			string transformName = "Day_" + i.ToString();
			if (base.FindTransform(transformName))
			{
				ButtonCustom expr_A6 = base.FindTransform(transformName).GetComponent<ButtonCustom>();
				expr_A6.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_A6.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnBtnItemClick));
			}
			if (i < 7)
			{
				this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1122, base.FindTransform(transformName).FindChild("Fx"), "SevenDayUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
			}
		}
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1123, base.FindTransform("Day_7").FindChild("Fx"), "SevenDayUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1124, base.FindTransform("Day_2").FindChild("Fx2"), "SevenDayUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1124, base.FindTransform("Day_5").FindChild("Fx2"), "SevenDayUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
	}

	private void ClearFx()
	{
		using (List<int>.Enumerator enumerator = this.FxUidList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				FXSpineManager.Instance.DeleteSpine(current, true);
			}
		}
		this.FxUidList.Clear();
	}

	private void Star()
	{
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		SignInManager.Instance.IsSevenDayUIOpened = true;
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ClearFx();
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
		EventDispatcher.AddListener(EventNames.OnLoginWelfareUpdate, new Callback(this.OnGetSignChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnLoginWelfareUpdate, new Callback(this.OnGetSignChangedNty));
	}

	private void OnGetSignChangedNty()
	{
		this.RefreshUI();
	}

	private void OnBtnClose(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnBtnGet(GameObject go)
	{
		List<EveryDayInfo> loginWelfareList = SignInManager.Instance.loginWelfareList;
		int num = 0;
		int num2 = 0;
		if (loginWelfareList != null && loginWelfareList.get_Count() > 0)
		{
			for (int i = 0; i < loginWelfareList.get_Count(); i++)
			{
				if (loginWelfareList.get_Item(i).status == 1)
				{
					num = loginWelfareList.get_Item(i).loginDays;
					break;
				}
				if (loginWelfareList.get_Item(i).status == 2)
				{
					num2 = loginWelfareList.get_Item(i).loginDays;
				}
			}
		}
		if (num > 0)
		{
			SignInManager.Instance.SendGetLoginWelfareReq(num);
		}
		else
		{
			string text = GameDataUtils.GetChineseContent(502215, false);
			num2++;
			if (num2 > 7)
			{
				return;
			}
			text = text.Replace("xx", num2.ToString());
			UIManagerControl.Instance.ShowToastText(text);
		}
	}

	private void OnBtnItemClick(GameObject sender)
	{
		string[] array = sender.get_name().Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[1]);
		List<EveryDayInfo> loginWelfareList = SignInManager.Instance.loginWelfareList;
		if (loginWelfareList != null && num <= loginWelfareList.get_Count())
		{
			for (int i = 0; i < loginWelfareList.get_Count(); i++)
			{
				if (loginWelfareList.get_Item(i).loginDays == num)
				{
					int itemId = loginWelfareList.get_Item(i).rewardItem.itemId;
					XDict<int, long> rewardItems = FirstPayManager.Instance.GetRewardItems(itemId);
					List<int> list = new List<int>();
					List<long> list2 = new List<long>();
					for (int j = 0; j < rewardItems.Count; j++)
					{
						list.Add(rewardItems.ElementKeyAt(j));
						list2.Add(rewardItems.ElementValueAt(j));
					}
					RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
					rewardUI.SetRewardItem("查看物品", list, list2, true, false, null, null);
					break;
				}
			}
		}
	}

	private void RefreshUI()
	{
		List<EveryDayInfo> loginWelfareList = SignInManager.Instance.loginWelfareList;
		if (loginWelfareList.get_Count() < 1)
		{
			return;
		}
		bool isWhiteBlack = true;
		for (int i = 1; i <= loginWelfareList.get_Count(); i++)
		{
			EveryDayInfo everyDayInfo = loginWelfareList.get_Item(i - 1);
			string transformName = "Day_" + everyDayInfo.loginDays.ToString();
			if (i == 7)
			{
				Image component = base.FindTransform(transformName).FindChild("ImageGet").GetComponent<Image>();
				Image component2 = base.FindTransform(transformName).FindChild("ImageBgDay").GetComponent<Image>();
				switch (everyDayInfo.status)
				{
				case 0:
					base.FindTransform(transformName).FindChild("Fx").get_gameObject().SetActive(false);
					component.get_gameObject().SetActive(false);
					component2.get_gameObject().SetActive(true);
					break;
				case 1:
					base.FindTransform(transformName).FindChild("Fx").get_gameObject().SetActive(true);
					isWhiteBlack = false;
					component.get_gameObject().SetActive(false);
					component2.get_gameObject().SetActive(true);
					break;
				case 2:
					base.FindTransform(transformName).FindChild("Fx").get_gameObject().SetActive(false);
					component.get_gameObject().SetActive(true);
					component2.get_gameObject().SetActive(false);
					break;
				}
			}
			else if (base.FindTransform(transformName))
			{
				string chineseContent = GameDataUtils.GetChineseContent(502208, false);
				if (i == 2 || i == 5)
				{
					base.FindTransform(transformName).FindChild("ImageIcon").GetComponent<DepthOfUINoCollider>().SortingOrder = 3002;
					base.FindTransform(transformName).FindChild("ImageGet").GetComponent<DepthOfUINoCollider>().SortingOrder = 3003;
					base.FindTransform(transformName).FindChild("ImageIcon").FindChild("TextDay").GetComponent<Text>().set_text(chineseContent.Replace("xx", everyDayInfo.loginDays.ToString()));
					base.FindTransform(transformName).FindChild("ImageIcon").FindChild("TextItemName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(everyDayInfo.chineseId, false));
				}
				else
				{
					base.FindTransform(transformName).FindChild("TextDay").GetComponent<Text>().set_text(chineseContent.Replace("xx", everyDayInfo.loginDays.ToString()));
					base.FindTransform(transformName).FindChild("TextItemName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(everyDayInfo.chineseId, false));
				}
				if (everyDayInfo.iconId > 0)
				{
					ResourceManager.SetSprite(base.FindTransform(transformName).FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetIcon(everyDayInfo.iconId));
				}
				Image component3 = base.FindTransform(transformName).FindChild("ImageBgDay").GetComponent<Image>();
				Image component4 = base.FindTransform(transformName).FindChild("ImageBgGeryDay").GetComponent<Image>();
				Image component5 = base.FindTransform(transformName).FindChild("ImageGet").GetComponent<Image>();
				GameObject gameObject = base.FindTransform(transformName).FindChild("Fx").get_gameObject();
				switch (everyDayInfo.status)
				{
				case 0:
					component3.get_gameObject().SetActive(true);
					component4.get_gameObject().SetActive(false);
					component5.get_gameObject().SetActive(false);
					gameObject.get_gameObject().SetActive(false);
					if (i == 2 || i == 5)
					{
						base.FindTransform(transformName).FindChild("Fx2").get_gameObject().SetActive(true);
					}
					break;
				case 1:
					component3.get_gameObject().SetActive(true);
					component4.get_gameObject().SetActive(false);
					component5.get_gameObject().SetActive(false);
					gameObject.get_gameObject().SetActive(true);
					isWhiteBlack = false;
					if (i == 2 || i == 5)
					{
						base.FindTransform(transformName).FindChild("Fx2").get_gameObject().SetActive(true);
					}
					break;
				case 2:
					component3.get_gameObject().SetActive(false);
					component4.get_gameObject().SetActive(true);
					component5.get_gameObject().SetActive(true);
					gameObject.get_gameObject().SetActive(false);
					if (i == 2 || i == 5)
					{
						base.FindTransform(transformName).FindChild("Fx2").get_gameObject().SetActive(false);
					}
					break;
				}
			}
		}
		ImageColorMgr.SetImageColor(base.FindTransform("ImageLight").GetComponent<Image>(), isWhiteBlack);
		ImageColorMgr.SetImageColor(base.FindTransform("ImageText").GetComponent<Image>(), isWhiteBlack);
	}
}
