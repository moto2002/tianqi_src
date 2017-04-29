using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MushroomHitStartUI : UIBase
{
	private Text m_TextTimes;

	private int m_SpineId;

	private bool isEnterGame;

	private Text m_RankContent;

	private GridLayoutGroup m_ranklist;

	private GameObject m_imageNoOne;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_TextTimes = base.FindTransform("TextTimes").GetComponent<Text>();
		base.FindTransform("BtnTips").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnTips);
		base.FindTransform("BtnEnter").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEnter);
		base.FindTransform("BtnExit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnExit);
		this.m_RankContent = base.FindTransform("RankContent").GetComponent<Text>();
		base.FindTransform("RankPanel").GetComponent<DepthOfUINoCollider>().SortingOrder = 2002;
		this.m_ranklist = base.FindTransform("RankSR").FindChild("RankItems").GetComponent<GridLayoutGroup>();
		this.m_imageNoOne = base.FindTransform("ImageNoOne").get_gameObject();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(false);
		MushroomHitManager.Instance.SendGetHitMouseInfoReq();
		MushroomHitManager.Instance.SendGetHitMouseRankReq();
		this.SetTimesText();
		this.PlayMushroomSpine(true);
		this.isEnterGame = false;
		SoundManager.Instance.PlayBGMByID(124);
		this.m_RankContent.set_text("尚未有玩家上榜");
		this.m_imageNoOne.SetActive(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.PlayMushroomSpine(false);
		if (!this.isEnterGame)
		{
			MySceneManager.Instance.PlayBGM();
		}
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
		EventDispatcher.AddListener(EventNames.MushroomHitInfo, new Callback(this.OnUpdateInfo));
		EventDispatcher.AddListener(EventNames.MushroomHitStart, new Callback(this.OnMushroomHitStart));
		EventDispatcher.AddListener<GetHitMouseRankRes>(EventNames.MushroomHitRankInfo, new Callback<GetHitMouseRankRes>(this.OnMushroomRankInfo));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.MushroomHitInfo, new Callback(this.OnUpdateInfo));
		EventDispatcher.RemoveListener(EventNames.MushroomHitStart, new Callback(this.OnMushroomHitStart));
		EventDispatcher.RemoveListener<GetHitMouseRankRes>(EventNames.MushroomHitRankInfo, new Callback<GetHitMouseRankRes>(this.OnMushroomRankInfo));
	}

	protected void OnClickBtnTips(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 340025, 340024);
	}

	protected void OnClickBtnEnter(GameObject go)
	{
		if (MushroomHitManager.Instance.gameTimes <= 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(340023, false), 1f, 2f);
			return;
		}
		MushroomHitManager.Instance.SendHitMouseStartReq();
	}

	protected void OnClickBtnExit(GameObject go)
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected void OnMushroomHitStart()
	{
		this.isEnterGame = true;
		UIManagerControl.Instance.OpenUI("MushroomHitUI", null, false, UIType.FullScreen);
	}

	protected void OnUpdateInfo()
	{
		this.SetTimesText();
	}

	protected void SetTimesText()
	{
		this.m_TextTimes.set_text(GameDataUtils.GetChineseContent(340020, false) + MushroomHitManager.Instance.gameTimes);
	}

	private void PlayMushroomSpine(bool isShow)
	{
		if (isShow && this.m_SpineId == 0)
		{
			Transform host = base.FindTransform("PanelSpine");
			this.m_SpineId = FXSpineManager.Instance.PlaySpine(3625, host, "MushroomHitUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (!isShow && this.m_SpineId != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_SpineId, true);
		}
	}

	protected void OnMushroomRankInfo(GetHitMouseRankRes msg)
	{
		this.ClearScroll();
		for (int i = 0; i < msg.infos.get_Count(); i++)
		{
			this.AddScrollCell(i, msg.infos.get_Item(i));
		}
		if (msg.infos.get_Count() > 0)
		{
			this.m_RankContent.set_text(string.Empty);
			this.m_imageNoOne.SetActive(false);
		}
		else
		{
			this.m_RankContent.set_text("尚未有玩家上榜");
			this.m_imageNoOne.SetActive(true);
		}
	}

	private void AddScrollCell(int index, HitMouseRankInfo data)
	{
		Transform transform = this.m_ranklist.get_transform().FindChild("MushroomHitRankItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.get_transform().FindChild("Rank").GetComponent<Text>().set_text((index + 1).ToString());
			transform.get_transform().FindChild("Score").GetComponent<Text>().set_text(data.score.ToString() + "分");
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("MushroomHitRankItem");
			instantiate2Prefab.get_transform().SetParent(this.m_ranklist.get_transform(), false);
			instantiate2Prefab.set_name("MushroomHitRankItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.get_transform().FindChild("Rank").GetComponent<Text>().set_text((index + 1).ToString() + "." + data.name);
			instantiate2Prefab.get_transform().FindChild("Score").GetComponent<Text>().set_text(data.score.ToString() + "分");
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_ranklist.get_transform().get_childCount(); i++)
		{
			this.m_ranklist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}
}
