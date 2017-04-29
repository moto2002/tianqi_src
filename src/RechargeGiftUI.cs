using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargeGiftUI : UIBase
{
	public static RechargeGiftUI Instance;

	private Text activityTime;

	private int activityBeginTime;

	private int activityEndTime;

	private ButtonCustom btnRecharge;

	private ButtonCustom btnGet;

	private Image btnGetImg;

	private Transform panelBtnGetSpine;

	private int BtnGetState;

	private int showingBoxValue;

	private Slider rechargedSlider;

	private GameObject rewardList;

	private ListPool baoBoxList;

	public List<GameObject> RewardItems = new List<GameObject>();

	private RectTransform boxList;

	private List<AcItemInfo> boxListInfo;

	public Text rechargedNum;

	public Text conditionText;

	private List<string> boxImgList;

	private List<string> openBoxImgList;

	private int m_GetSpineTag;

	public List<string> BoxImgList
	{
		get
		{
			if (this.boxImgList == null)
			{
				this.boxImgList = new List<string>();
				this.boxImgList.Add("dailytask_icon_bag1");
				this.boxImgList.Add("dailytask_icon_bag3");
				this.boxImgList.Add("dailytask_icon_bag5");
				this.boxImgList.Add("dailytask_icon_bag7");
				this.boxImgList.Add("dailytask_icon_bag9");
			}
			return this.boxImgList;
		}
	}

	public List<string> OpenBoxImgList
	{
		get
		{
			if (this.openBoxImgList == null)
			{
				this.openBoxImgList = new List<string>();
				this.openBoxImgList.Add("dailytask_icon_bag2");
				this.openBoxImgList.Add("dailytask_icon_bag4");
				this.openBoxImgList.Add("dailytask_icon_bag6");
				this.openBoxImgList.Add("dailytask_icon_bag8");
				this.openBoxImgList.Add("dailytask_icon_bag10");
			}
			return this.openBoxImgList;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
	}

	private void Awake()
	{
		RechargeGiftUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.activityTime = base.FindTransform("ActivityTime").GetComponent<Text>();
		this.btnRecharge = base.FindTransform("BtnRecharge").GetComponent<ButtonCustom>();
		this.btnGet = base.FindTransform("BtnGet").GetComponent<ButtonCustom>();
		this.btnGetImg = base.FindTransform("BtnGet").GetComponent<Image>();
		this.panelBtnGetSpine = base.FindTransform("PanelBtnGetSpine");
		this.panelBtnGetSpine.set_localScale(new Vector3(1.2f, 1.2f, 1f));
		this.rechargedNum = base.FindTransform("Recharged").FindChild("RechargedNum").GetComponent<Text>();
		this.rechargedSlider = base.FindTransform("Progress").FindChild("Slider").GetComponent<Slider>();
		this.baoBoxList = base.FindTransform("BaoBoxList").GetComponent<ListPool>();
		this.baoBoxList.SetItem("BaoBoxItem");
		this.baoBoxList.isAnimation = false;
		this.boxList = base.FindTransform("BaoBoxList").GetComponent<RectTransform>();
		this.conditionText = base.FindTransform("ConditionText").GetComponent<Text>();
		this.rewardList = base.FindTransform("RewardList").get_gameObject();
		this.btnRecharge.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnRecharge);
		this.btnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGet);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			RechargeGiftUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnRecharge(GameObject sender)
	{
		ConsumeRechargeManager.Instance.OpenVIPUIOfRecharge();
	}

	private void OnClickItem(int itemId)
	{
		ItemTipUIViewModel.ShowItem(itemId, null);
	}

	public void OnClickBtnGet(GameObject sender)
	{
		switch (this.BtnGetState)
		{
		case 0:
			Debug.LogError("奖励条件尚未达成，无法领取");
			break;
		case 1:
			if (!BackpackManager.Instance.ShowBackpackFull())
			{
				ConsumeRechargeManager.Instance.SendGetAcPrizeReq(1, this.showingBoxValue);
			}
			break;
		case 2:
			Debug.LogError("奖励已经领取过，无法重复领取");
			break;
		default:
			Debug.LogError("There is no exist GetState: " + this.BtnGetState);
			break;
		}
	}

	public void RefreshUI()
	{
		Ac aC = ConsumeRechargeManager.Instance.GetAC(Ac.AcType.Type.Recharge);
		if (aC == null)
		{
			return;
		}
		this.ReflashBoxList(aC);
	}

	private void ReflashBoxList(Ac down)
	{
		this.activityBeginTime = down.beginTime;
		this.activityEndTime = down.endTime;
		this.SetActivityTimeShow();
		int curData = down.curData;
		this.boxListInfo = down.acItemInfo;
		this.boxListInfo.Sort(new Comparison<AcItemInfo>(ConsumeRechargeManager.SortCompare));
		int count = this.boxListInfo.get_Count();
		this.rechargedNum.set_text("当前充值：" + curData.ToString());
		this.SetSliderBarValue(curData, this.boxListInfo);
		if (this.boxListInfo != null)
		{
			this.AddBoxItem(this.boxListInfo);
		}
		if (this.boxListInfo.get_Item(0).status == 2)
		{
			this.ShowRewardList(this.boxListInfo.get_Item(0).targetVal);
		}
		else
		{
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.boxListInfo.get_Item(i).status != 2)
				{
					this.ShowRewardList(this.boxListInfo.get_Item(i).targetVal);
					break;
				}
			}
		}
	}

	private void SetActivityTimeShow()
	{
		string time = TimeConverter.GetTime(TimeManager.Instance.CalculateLocalServerTimeBySecond(this.activityBeginTime), TimeFormat.MDHHMM);
		string time2 = TimeConverter.GetTime(TimeManager.Instance.CalculateLocalServerTimeBySecond(this.activityEndTime), TimeFormat.MDHHMM);
		this.activityTime.set_text("活动时间：" + time + "~" + time2);
	}

	private void SetSliderBarValue(int Recharged, List<AcItemInfo> BoxListInfo)
	{
		int count = BoxListInfo.get_Count();
		if (Recharged >= BoxListInfo.get_Item(0).targetVal)
		{
			this.rechargedSlider.set_value(1f);
			return;
		}
		for (int i = count - 1; i >= 0; i--)
		{
			if (Recharged <= BoxListInfo.get_Item(i).targetVal)
			{
				if (i < count - 1)
				{
					int targetVal = BoxListInfo.get_Item(i + 1).targetVal;
					this.rechargedSlider.set_value(((float)(count - 1 - i) + (float)(Recharged - targetVal) / (float)(BoxListInfo.get_Item(i).targetVal - targetVal)) / (float)count);
				}
				else
				{
					this.rechargedSlider.set_value((float)Recharged / (float)BoxListInfo.get_Item(i).targetVal / (float)count);
				}
				return;
			}
		}
	}

	private void AddBoxItem(List<AcItemInfo> boxListInfo)
	{
		int BoxNum = boxListInfo.get_Count();
		float x = this.boxList.get_sizeDelta().x;
		this.baoBoxList.spacing = x / (float)BoxNum;
		int baoBoxSpriteStrIndex = 4;
		this.baoBoxList.Create(BoxNum, delegate(int index)
		{
			if (index < BoxNum && index < this.baoBoxList.Items.get_Count())
			{
				BaoBoxItemUnit component = this.baoBoxList.Items.get_Item(index).GetComponent<BaoBoxItemUnit>();
				Image baoBoxImg = component.BaoBoxImg;
				switch (boxListInfo.get_Item(index).status)
				{
				case 0:
					ResourceManager.SetSprite(baoBoxImg, ResourceManager.GetIconSprite(this.BoxImgList.get_Item(baoBoxSpriteStrIndex--)));
					ImageColorMgr.SetImageColor(baoBoxImg, true);
					break;
				case 1:
					ResourceManager.SetSprite(baoBoxImg, ResourceManager.GetIconSprite(this.BoxImgList.get_Item(baoBoxSpriteStrIndex--)));
					break;
				case 2:
					ResourceManager.SetSprite(baoBoxImg, ResourceManager.GetIconSprite(this.OpenBoxImgList.get_Item(baoBoxSpriteStrIndex--)));
					ImageColorMgr.SetImageColor(baoBoxImg, true);
					break;
				default:
					Debug.LogError("There is no exist BoxInfoState: " + boxListInfo.get_Item(index).status);
					break;
				}
				component.SetValue(boxListInfo.get_Item(index).targetVal);
			}
		});
	}

	public void ShowRewardList(int targetValue)
	{
		AcItemInfo acItemInfo = null;
		for (int i = 0; i < this.boxListInfo.get_Count(); i++)
		{
			if (this.boxListInfo.get_Item(i).targetVal == targetValue)
			{
				acItemInfo = this.boxListInfo.get_Item(i);
				break;
			}
		}
		if (acItemInfo == null)
		{
			Debug.LogError("Thers is no exist targetValue:" + targetValue);
			return;
		}
		this.ClearRewardItems();
		this.SetBtnGet(acItemInfo.status, acItemInfo.targetVal);
		string text = acItemInfo.targetVal.ToString();
		this.conditionText.set_text("累计充值" + text + "钻石可获得奖励:");
		Vector2 sizeDelta = new Vector2((float)(105 * acItemInfo.items.get_Count()), 110f);
		this.rewardList.GetComponent<RectTransform>().set_sizeDelta(sizeDelta);
		for (int j = 0; j < acItemInfo.items.get_Count(); j++)
		{
			int itemId = acItemInfo.items.get_Item(j).itemCfgId;
			int itemNum = acItemInfo.items.get_Item(j).itemNum;
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GiftRewardItem");
			this.RewardItems.Add(instantiate2Prefab);
			Transform transform = instantiate2Prefab.get_transform();
			transform.SetParent(this.rewardList.get_transform(), false);
			instantiate2Prefab.SetActive(true);
			ResourceManager.SetSprite(transform.FindChild("Frame").GetComponent<Image>(), GameDataUtils.GetItemFrame(itemId));
			ResourceManager.SetSprite(transform.FindChild("Icon").GetComponent<Image>(), GameDataUtils.GetItemIcon(itemId));
			transform.FindChild("Name").GetComponent<Text>().set_text(GameDataUtils.GetItemName(itemId, true, 0L));
			transform.FindChild("Num").GetComponent<Text>().set_text(itemNum.ToString());
			transform.GetComponent<ButtonCustom>().get_onClick().AddListener(delegate
			{
				this.OnClickItem(itemId);
			});
		}
	}

	private void ClearRewardItems()
	{
		for (int i = 0; i < this.RewardItems.get_Count(); i++)
		{
			Object.DestroyImmediate(this.RewardItems.get_Item(i));
		}
		this.RewardItems.Clear();
	}

	private void SetBtnGet(int state, int targetVal)
	{
		this.BtnGetState = state;
		this.showingBoxValue = targetVal;
		switch (state)
		{
		case 0:
			ImageColorMgr.SetImageColor(this.btnGetImg, true);
			this.PlayCanGetSpine(false);
			break;
		case 1:
			ImageColorMgr.SetImageColor(this.btnGetImg, false);
			this.PlayCanGetSpine(true);
			break;
		case 2:
			ImageColorMgr.SetImageColor(this.btnGetImg, true);
			this.PlayCanGetSpine(false);
			break;
		default:
			Debug.LogError("There is no exist GetState: " + state);
			break;
		}
	}

	private void PlayCanGetSpine(bool isShow)
	{
		if (isShow && this.m_GetSpineTag == 0)
		{
			this.m_GetSpineTag = FXSpineManager.Instance.PlaySpine(2201, this.panelBtnGetSpine, "RechargeGiftUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (!isShow && this.m_GetSpineTag != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_GetSpineTag, true);
			this.m_GetSpineTag = 0;
		}
	}
}
