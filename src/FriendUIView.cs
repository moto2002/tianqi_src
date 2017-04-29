using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class FriendUIView : UIBase
{
	public static FriendUIView Instance;

	private ButtonCustom ExchangeBtn;

	private Image mRefreshButtonFg;

	private bool isWait;

	private uint timerID;

	private int ExchangeTime = 3;

	private float mDeltaTime;

	private bool mIsCD;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		FriendUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		FriendManager.Instance.SendRefreshBuddyInfo();
		FriendManager.Instance.SendRecommendsInfo(6);
	}

	private void Update()
	{
		this.RefreshButton();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast("UIManagerControl.HidePopButtonsAdjustUI");
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		this.isWait = false;
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			FriendUIView.Instance = null;
			FriendUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnClickCloseBtn(GameObject go)
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.InitInput();
		this.ExchangeBtn = base.FindTransform("BtnExchange").GetComponent<ButtonCustom>();
		this.ExchangeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExchange);
		this.mRefreshButtonFg = base.FindTransform("BtnExchangeBg").GetComponent<Image>();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("ButtonToggles").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.SourceBinding.MemberName = "SubChannels";
		listBinder.PrefabName = "ButtonToggle2SubUI";
		ListViewBinder listViewBinder = base.FindTransform("FriendListScroll").get_gameObject().AddComponent<ListViewBinder>();
		listViewBinder.BindingProxy = base.get_gameObject();
		listViewBinder.PrefabName = "FriendInfoUnit";
		listViewBinder.m_spacing = 135f;
		listViewBinder.m_scrollStype = ListView.ListViewScrollStyle.Up;
		listViewBinder.SourceBinding.MemberName = "FriendInfoUnits";
		ListViewBinder listViewBinder2 = base.FindTransform("FriendFindsScroll").get_gameObject().AddComponent<ListViewBinder>();
		listViewBinder2.BindingProxy = base.get_gameObject();
		listViewBinder2.PrefabName = "FriendInfoUnit";
		listViewBinder2.m_spacing = 135f;
		listViewBinder2.m_scrollStype = ListView.ListViewScrollStyle.Up;
		listViewBinder2.SourceBinding.MemberName = "FriendInfoFinds";
		listViewBinder2 = base.FindTransform("FriendRecommendsScroll").get_gameObject().AddComponent<ListViewBinder>();
		listViewBinder2.BindingProxy = base.get_gameObject();
		listViewBinder2.PrefabName = "FriendInfoUnit";
		listViewBinder2.m_spacing = 135f;
		listViewBinder2.m_scrollStype = ListView.ListViewScrollStyle.Up;
		listViewBinder2.SourceBinding.MemberName = "FriendRecommends";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FriendInfosUIVisibility";
		visibilityBinder.Target = base.FindTransform("FriendInfosUI").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FriendFindsUIVisibility";
		visibilityBinder.Target = base.FindTransform("FriendFindsUI").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowFriendTip";
		visibilityBinder.Target = base.FindTransform("FriendTip").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "FriendRecommendsUIVisibility";
		visibilityBinder.Target = base.FindTransform("FriendRecommendsUI").get_gameObject();
		TextBinder textBinder = base.FindTransform("FriendTip").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FriendTip";
		textBinder = base.FindTransform("FriendNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FriendNum";
		textBinder = base.FindTransform("BtnExchangeTimeText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FriendNum";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	private void InitInput()
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InputUnit");
		UGUITools.SetParent(base.FindTransform("Node2FindNameInput").get_gameObject(), instantiate2Prefab, false, "SearchFriend");
		InputUnitViewModel mInputUnit = instantiate2Prefab.GetComponent<InputUnitViewModel>();
		mInputUnit.Title = GameDataUtils.GetChineseContent(502048, false);
		mInputUnit.BtnText = GameDataUtils.GetChineseContent(502050, false);
		mInputUnit.Action2Callback = delegate
		{
			if (string.IsNullOrEmpty(mInputUnit.Input))
			{
				FloatTextAddManager.Instance.AddFloatText(GameDataUtils.GetChineseContent(502066, false), Color.get_white());
				return;
			}
			FriendManager.Instance.SendLookUpBuddyInfo(mInputUnit.Input);
		};
	}

	public void UpdateRecommendsPanelPosition(bool isDown)
	{
		if (isDown)
		{
			base.FindTransform("FriendRecommendsUI").get_transform().set_localPosition(new Vector3(0f, -140f, 0f));
			base.FindTransform("FriendRecommendsScroll").GetComponent<RectTransform>().set_sizeDelta(new Vector2(852f, 270f));
			if (base.FindTransform("FriendRecommendsScroll").FindChild("ContentView"))
			{
				base.FindTransform("FriendRecommendsScroll").FindChild("ContentView").get_transform().set_localPosition(Vector3.get_zero());
			}
		}
		else
		{
			base.FindTransform("FriendRecommendsUI").get_transform().set_localPosition(Vector3.get_zero());
			base.FindTransform("FriendRecommendsScroll").GetComponent<RectTransform>().set_sizeDelta(new Vector2(852f, 410f));
		}
	}

	public void OnClickExchange(GameObject go)
	{
		if (this.isWait)
		{
			return;
		}
		this.isWait = true;
		this.mDeltaTime = 0f;
		this.mIsCD = true;
		if (FriendUIViewModel.Instance)
		{
			FriendUIViewModel.Instance.RandomShowRecommendsFriends();
		}
	}

	private void RefreshButton()
	{
		if (this.mIsCD)
		{
			this.mDeltaTime += Time.get_deltaTime();
			this.mRefreshButtonFg.set_fillAmount(this.mDeltaTime / (float)this.ExchangeTime);
			if (this.mDeltaTime > (float)this.ExchangeTime)
			{
				this.mIsCD = false;
				this.mDeltaTime = 0f;
				this.isWait = false;
				this.mRefreshButtonFg.set_fillAmount(1f);
			}
		}
	}

	private void ShowExchangeTime()
	{
		if (this.ExchangeTime <= 0)
		{
			base.FindTransform("BtnExchangeText").get_gameObject().SetActive(true);
			base.FindTransform("BtnExchangeTimeText").get_gameObject().SetActive(false);
			ImageColorMgr.SetImageColor(base.FindTransform("BtnExchangeBg").GetComponent<Image>(), false);
			if (this.timerID != 0u)
			{
				TimerHeap.DelTimer(this.timerID);
			}
			this.ExchangeTime = 0;
			this.isWait = false;
		}
		else
		{
			base.FindTransform("BtnExchangeText").get_gameObject().SetActive(false);
			base.FindTransform("BtnExchangeTimeText").get_gameObject().SetActive(true);
			base.FindTransform("BtnExchangeTimeText").GetComponent<Text>().set_text(this.ExchangeTime.ToString());
			ImageColorMgr.SetImageColor(base.FindTransform("BtnExchangeBg").GetComponent<Image>(), true);
		}
		this.ExchangeTime--;
	}
}
