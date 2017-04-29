using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activity7Day : UIBase
{
	private const int pageMax = 4;

	private const int dayMax = 7;

	public static Activity7Day Instance;

	private List<Tab.TAB> tabList = new List<Tab.TAB>();

	private int pageTab;

	private int dayNum = 1;

	private Text txtTime;

	private Transform imgBox;

	private Transform scroll;

	private Button[] btnPages = new Button[4];

	private Button[] btnDays = new Button[7];

	private GameObject currBtnGet;

	private List<RawInfo> rawInfoList;

	private int imgBoxSpineTag;

	private int[] strGradeId = new int[]
	{
		0,
		513184,
		513185,
		513186,
		513187,
		513188
	};

	private Dictionary<Tab.TAB, int> strTypeName;

	private string[,] btnDaysPictures;

	public Activity7Day()
	{
		Dictionary<Tab.TAB, int> dictionary = new Dictionary<Tab.TAB, int>();
		dictionary.Add(Tab.TAB.RoleGrow, 513189);
		dictionary.Add(Tab.TAB.PlayPass, 513190);
		dictionary.Add(Tab.TAB.EquipForming, 513191);
		dictionary.Add(Tab.TAB.PetTrain, 513192);
		this.strTypeName = dictionary;
		string[,] expr_86 = new string[7, 2];
		expr_86[0, 0] = "j_bt_celebration001";
		expr_86[0, 1] = "j_bt_celebration002";
		expr_86[1, 0] = "j_bt_celebration003";
		expr_86[1, 1] = "j_bt_celebration004";
		expr_86[2, 0] = "j_bt_celebration003";
		expr_86[2, 1] = "j_bt_celebration004";
		expr_86[3, 0] = "j_bt_celebration003";
		expr_86[3, 1] = "j_bt_celebration004";
		expr_86[4, 0] = "j_bt_celebration003";
		expr_86[4, 1] = "j_bt_celebration004";
		expr_86[5, 0] = "j_bt_celebration003";
		expr_86[5, 1] = "j_bt_celebration004";
		expr_86[6, 0] = "j_bt_celebration005";
		expr_86[6, 1] = "j_bt_celebration006";
		this.btnDaysPictures = expr_86;
		base..ctor();
	}

	private void Awake()
	{
		Activity7Day.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.txtTime = base.FindTransform("txtTime").GetComponent<Text>();
		this.imgBox = base.FindTransform("imgBox");
		this.scroll = base.FindTransform("scrollLayout");
		this.imgBox.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickImgBox);
	}

	private void Update()
	{
		this.SetTxtTime();
	}

	protected override void OnEnable()
	{
		this.InitButtons();
		this.dayNum = Mathf.Min(7, Activity7DayManager.Instance.startDay);
		this.SetBtnDaysState(this.dayNum);
		this.RefreshBtnPages(this.dayNum);
		this.SetActivityScroll();
		this.RefreshRedPoint();
	}

	protected override void OnDisable()
	{
		this.PlayImgBoxSpine(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			Activity7Day.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OpenServerBoxUpdate, new Callback(this.BoxUpdateCallBack));
		EventDispatcher.AddListener<int>(EventNames.GetActivityItemPrize, new Callback<int>(this.GetActivityPrizeCallBack));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OpenServerBoxUpdate, new Callback(this.BoxUpdateCallBack));
		EventDispatcher.RemoveListener<int>(EventNames.GetActivityItemPrize, new Callback<int>(this.GetActivityPrizeCallBack));
		base.RemoveListeners();
	}

	private void OnClickBtnPage(GameObject gameObject)
	{
		string text = gameObject.get_name().Substring("btnPage".get_Length());
		int num = int.Parse(text) - 1;
		if (num == this.pageTab)
		{
			return;
		}
		this.SetBtnPagesState(num);
		this.pageTab = num;
		this.SetActivityScroll();
	}

	private void OnClickBtnDay(GameObject gameObject)
	{
		string text = gameObject.get_name().Substring("btnDay".get_Length());
		int num = int.Parse(text);
		if (num == this.dayNum)
		{
			return;
		}
		if (num - 1 >= Activity7DayManager.Instance.startDay)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513118, false), 1f, 1f);
			return;
		}
		this.SetBtnDaysState(num);
		this.RefreshBtnPages(num);
		this.dayNum = num;
		this.SetBtnPagesState(this.pageTab);
		this.SetActivityScroll();
		this.RefreshRedPoint();
	}

	private void OnClickBtnGet(GameObject btnGet)
	{
		this.currBtnGet = btnGet;
		int cellIndex = int.Parse(btnGet.get_transform().get_parent().get_name());
		this.ClickBtnGetToDo(btnGet.get_transform(), cellIndex);
	}

	private void OnClickImgBox(GameObject go)
	{
		int boxFlag = Activity7DayManager.Instance.boxFlag;
		if (boxFlag != 0)
		{
			if (boxFlag == 1)
			{
				Activity7DayManager.Instance.SendGetOpenServerBoxReq();
			}
		}
		else
		{
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			for (int i = 0; i < Activity7DayManager.Instance.boxItems.get_Count(); i++)
			{
				ItemInfo1 itemInfo = Activity7DayManager.Instance.boxItems.get_Item(i);
				list.Add(itemInfo.itemId);
				list2.Add((long)itemInfo.count);
			}
			RewardUI rewardUI = UIManagerControl.Instance.OpenUI("RewardUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as RewardUI;
			rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513163, false), list, list2, true, false, null, null);
			rewardUI.SetTipsText(GameDataUtils.GetChineseContent(513197, false));
		}
	}

	private string GetTime(int seconds)
	{
		int num = seconds / 86400;
		int num2 = (seconds - num * 86400) / 3600;
		return string.Format(GameDataUtils.GetChineseContent(513165, false), num, num2);
	}

	private void SetTxtTime()
	{
		if (Activity7DayManager.Instance.endTimeouts.get_Count() > 0)
		{
			this.txtTime.set_text(this.GetTime(Activity7DayManager.Instance.endTimeouts.get_Item(0)));
		}
	}

	private void InitButtons()
	{
		this.PlayImgBoxSpine(Activity7DayManager.Instance.boxFlag == 1);
		this.InitBtnPages();
		this.InitBtnDays();
		this.SetTxtTime();
	}

	private void InitBtnPages()
	{
		for (int i = 0; i < this.btnPages.Length; i++)
		{
			Transform transform = base.FindTransform("btnPage" + (i + 1));
			this.btnPages[i] = transform.GetComponent<Button>();
			GameObject gameObjectPage = this.btnPages[i].get_gameObject();
			this.btnPages[i].get_onClick().RemoveAllListeners();
			this.btnPages[i].get_onClick().AddListener(delegate
			{
				this.OnClickBtnPage(gameObjectPage);
			});
		}
		this.SetBtnPagesState(0);
	}

	private void InitBtnDays()
	{
		for (int i = 0; i < this.btnDays.Length; i++)
		{
			this.btnDays[i] = base.get_transform().FindChild("south").FindChild("btnDay" + (i + 1)).GetComponent<Button>();
			this.btnDays[i].get_onClick().RemoveAllListeners();
			GameObject go = this.btnDays[i].get_gameObject();
			this.btnDays[i].get_onClick().AddListener(delegate
			{
				this.OnClickBtnDay(go);
			});
		}
		this.SetBtnDaysState(this.dayNum - 1);
	}

	private void SetBtnPagesState(int selectIndex)
	{
		for (int i = 0; i < this.btnPages.Length; i++)
		{
			Image component = this.btnPages[i].get_gameObject().GetComponent<Image>();
			Text component2 = this.btnPages[i].get_transform().FindChild("Text").GetComponent<Text>();
			if (i != selectIndex)
			{
				ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("j_Paging_02"));
				component2.set_color(new Color32(205, 161, 92, 255));
			}
			else
			{
				ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("j_Paging_01"));
				component2.set_color(new Color32(89, 51, 20, 255));
			}
		}
	}

	private void SetBtnDaysState(int dayNum)
	{
		int num = dayNum - 1;
		for (int i = 0; i < this.btnDays.Length; i++)
		{
			Image component = this.btnDays[i].get_gameObject().GetComponent<Image>();
			if (i == num)
			{
				ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(this.btnDaysPictures[i, 1]));
			}
			else
			{
				ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(this.btnDaysPictures[i, 0]));
			}
		}
	}

	public void Refresh()
	{
		this.RefreshBtnGet();
		this.RefreshRedPoint();
	}

	public void RefreshBtnGet()
	{
		GameObject gameObject = this.currBtnGet;
		if (gameObject != null)
		{
			int cellIndex = int.Parse(gameObject.get_transform().get_parent().get_name());
			this.SetBtnGet(gameObject.get_transform(), cellIndex);
		}
	}

	public void RefreshRedPoint()
	{
		for (int i = 0; i < this.tabList.get_Count(); i++)
		{
			Tab.TAB type = this.tabList.get_Item(i);
			bool subPageRedPoint = Activity7DayManager.Instance.GetSubPageRedPoint((int)type, this.dayNum);
			Transform transform = this.btnPages[i].get_transform().FindChild("redPoint");
			transform.get_gameObject().SetActive(subPageRedPoint);
		}
		for (int j = 0; j < 7; j++)
		{
			bool dayRedPoint = Activity7DayManager.Instance.GetDayRedPoint(j + 1);
			Transform transform2 = this.btnDays[j].get_transform().FindChild("redPoint");
			transform2.get_gameObject().SetActive(dayRedPoint);
		}
	}

	public void RefreshBtnPages(int day)
	{
		this.tabList.Clear();
		this.pageTab = -1;
		List<RawInfo> list = Activity7DayManager.Instance.GetRawInfoList(day, 0, false);
		Dictionary<Tab.TAB, bool> dictionary = new Dictionary<Tab.TAB, bool>();
		for (int i = 0; i < list.get_Count(); i++)
		{
			RawInfo rawInfo = list.get_Item(i);
			dictionary.set_Item(rawInfo.tab, true);
		}
		using (Dictionary<Tab.TAB, bool>.Enumerator enumerator = dictionary.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Tab.TAB, bool> current = enumerator.get_Current();
				this.tabList.Add(current.get_Key());
			}
		}
		this.tabList.Sort((Tab.TAB a, Tab.TAB b) => a.CompareTo(b));
		for (int j = 0; j < this.btnPages.Length; j++)
		{
			GameObject gameObject = this.btnPages[j].get_gameObject();
			if (j < this.tabList.get_Count())
			{
				Tab.TAB tAB = this.tabList.get_Item(j);
				gameObject.SetActive(true);
				gameObject.get_transform().FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(this.strTypeName.get_Item(tAB), false));
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
		if (this.tabList.get_Count() > 0)
		{
			this.pageTab = 0;
		}
	}

	private void PlayImgBoxSpine(bool isShow)
	{
		if (isShow && this.imgBoxSpineTag == 0)
		{
			this.imgBoxSpineTag = FXSpineManager.Instance.PlaySpine(1121, this.imgBox, "Activity7Day", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (!isShow && this.imgBoxSpineTag != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.imgBoxSpineTag, true);
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.scroll.get_childCount(); i++)
		{
			Object.Destroy(this.scroll.GetChild(i).get_gameObject());
		}
	}

	private void ClickBtnGetToDo(Transform btnGet, int cellIndex)
	{
		RawInfo rawInfo = this.rawInfoList.get_Item(cellIndex);
		int acId = rawInfo.acId;
		if (!Activity7DayManager.Instance.activityInfos.ContainsKey(acId))
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(513193, false), GameDataUtils.GetChineseContent(513194, false), delegate
			{
				UIManagerControl.Instance.HideUI("OperateActivityUI");
				UIStackManager.Instance.PopUIPrevious(UIType.FullScreen);
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
			return;
		}
		ActivityItemInfo activityItemInfo = Activity7DayManager.Instance.activityInfos.get_Item(acId);
		bool canGetFlag = activityItemInfo.canGetFlag;
		if (!activityItemInfo.hasGetPrize)
		{
			if (canGetFlag)
			{
				if (BackpackManager.Instance.ShowBackpackFull())
				{
					return;
				}
				Activity7DayManager.Instance.SendGetActivityItemPrizeReq(2, acId);
			}
			else
			{
				SourceReferenceUI.GoTo((int)rawInfo.servletId);
			}
		}
	}

	private void SetBtnGet(Transform btnGet, int cellIndex)
	{
		RawInfo rawInfo = this.rawInfoList.get_Item(cellIndex);
		ActivityItemInfo activityItemInfo = Activity7DayManager.Instance.activityInfos.get_Item(rawInfo.acId);
		if (activityItemInfo == null)
		{
			return;
		}
		if (activityItemInfo.hasGetPrize)
		{
			btnGet.get_gameObject().SetActive(false);
			btnGet.GetComponent<Button>().set_enabled(false);
			btnGet.get_parent().FindChild("imgFinish").get_gameObject().SetActive(true);
		}
		else
		{
			btnGet.get_gameObject().SetActive(true);
			btnGet.GetComponent<Button>().set_enabled(true);
			if (activityItemInfo.canGetFlag)
			{
				ResourceManager.SetSprite(btnGet.GetComponent<Image>(), ResourceManager.GetIconSprite("button_yellow_1"));
				btnGet.FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513203, false));
			}
			else if (rawInfo.servletId == -1L)
			{
				btnGet.get_gameObject().SetActive(false);
			}
			else
			{
				ResourceManager.SetSprite(btnGet.GetComponent<Image>(), ResourceManager.GetIconSprite("button_orange_1"));
				btnGet.FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513202, false));
			}
		}
	}

	private void SetOneCell(int cellIndex, RawInfo rawInfo)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Activity7DayCell");
		instantiate2Prefab.get_transform().SetParent(this.scroll, false);
		instantiate2Prefab.get_gameObject().SetActive(true);
		instantiate2Prefab.set_name(cellIndex.ToString());
		Transform btnGet = instantiate2Prefab.get_transform().FindChild("btnGet");
		btnGet.GetComponent<Button>().get_onClick().RemoveAllListeners();
		btnGet.GetComponent<Button>().get_onClick().AddListener(delegate
		{
			this.OnClickBtnGet(btnGet.get_gameObject());
		});
		this.SetBtnGet(btnGet, cellIndex);
		Transform transform = instantiate2Prefab.get_transform().FindChild("north").FindChild("txtCondition");
		string text = GameDataUtils.GetChineseContent((int)rawInfo.chineseId, false);
		List<int> needParams = rawInfo.needParams;
		if (rawInfo.tab == Tab.TAB.PlayPass)
		{
			if (rawInfo.subTab == SubTab.ST.EliteDungeon)
			{
				JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(needParams.get_Item(0));
				if (jingYingFuBenPeiZhi != null)
				{
					JJingYingFuBenQuYu jJingYingFuBenQuYu = DataReader<JJingYingFuBenQuYu>.Get(jingYingFuBenPeiZhi.map);
					string chineseContent = GameDataUtils.GetChineseContent(jingYingFuBenPeiZhi.bossName, false);
					text = string.Format(text, chineseContent);
				}
			}
		}
		else
		{
			string[] array = new string[needParams.get_Count()];
			for (int i = 0; i < needParams.get_Count(); i++)
			{
				int grade = needParams.get_Item(i);
				if (i == 1 && rawInfo.subTab == SubTab.ST.PetUpStage)
				{
					array[i] = this.GetPetGrade(grade);
				}
				else
				{
					array[i] = grade.ToString();
				}
			}
			text = string.Format(text, array);
		}
		transform.GetComponent<Text>().set_text(text);
		ItemInfo1 rewardItem = rawInfo.rewardItem;
		Transform parent = instantiate2Prefab.get_transform().FindChild("imgGrid");
		if (rewardItem != null)
		{
			ItemShow.ShowItem(parent, rewardItem.itemId, (long)rewardItem.count, false, UINodesManager.T2RootOfSpecial, 2001);
		}
	}

	private void SetActivityScroll()
	{
		this.ClearScroll();
		if (this.pageTab >= 0 && this.tabList.get_Count() > 0)
		{
			this.rawInfoList = Activity7DayManager.Instance.GetRawInfoList(this.dayNum, (int)this.tabList.get_Item(this.pageTab), true);
			for (int i = 0; i < this.rawInfoList.get_Count(); i++)
			{
				this.SetOneCell(i, this.rawInfoList.get_Item(i));
			}
		}
	}

	private void BoxUpdateCallBack()
	{
		this.PlayImgBoxSpine(Activity7DayManager.Instance.boxFlag == 1);
	}

	private void GetActivityPrizeCallBack(int activityType)
	{
		if (activityType == 2)
		{
			this.Refresh();
		}
	}

	private string GetGrade(int grade)
	{
		if (grade < this.strGradeId.Length)
		{
			return GameDataUtils.GetChineseContent(this.strGradeId[grade], false);
		}
		return string.Empty;
	}

	private string GetPetGrade(int grade)
	{
		PetConversion petConversion = DataReader<PetConversion>.Get(grade);
		if (petConversion != null)
		{
			return petConversion.tianfuSuffix;
		}
		return string.Empty;
	}
}
