using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XEngineActor;

internal class BossBookUI : UIBase
{
	private int curBossId;

	private int curPage;

	private Dictionary<int, int> lastSelBoss = new Dictionary<int, int>();

	private Toggle m_TrackToggle;

	private GameObject m_BtnMonsterModel;

	private GameObject m_BtnDropPreview;

	private GameObject m_PanelModel;

	private GameObject m_PanelDropPreview;

	private Transform m_ScrollLayoutDrop;

	private Transform m_ScrollLayoutTop;

	private Transform m_ScrollLayoutLeft;

	private Text m_TextHabitat;

	private Text m_TextRefresh;

	private Text m_TextRefreshTime;

	private Text m_TextBtnGo;

	private Text m_TextBossName;

	private Text m_TextFightTimes1;

	private RawImage m_RawImageModel;

	private GameObject m_ImageTouchPlace;

	private int modelUid;

	private bool isClickToggle = true;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnExit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExit);
		base.FindTransform("BtnDropRecord").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDropRecord);
		base.FindTransform("BtnSlayRecord").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnSlayRecord);
		base.FindTransform("BtnGo").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGo);
		this.m_BtnMonsterModel = base.FindTransform("BtnMonsterModel").get_gameObject();
		this.m_BtnMonsterModel.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnMonsterModel);
		this.m_BtnDropPreview = base.FindTransform("BtnDropPreview").get_gameObject();
		this.m_BtnDropPreview.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDropPreview);
		this.m_TrackToggle = base.FindTransform("TrackOn").GetComponent<Toggle>();
		this.m_TrackToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnClickTrackToggle));
		this.m_PanelDropPreview = base.FindTransform("PanelDropPreview").get_gameObject();
		this.m_PanelModel = base.FindTransform("PanelModel").get_gameObject();
		this.m_ScrollLayoutDrop = base.FindTransform("ScrollLayoutDrop");
		this.m_ScrollLayoutTop = base.FindTransform("ScrollLayoutTop");
		this.m_ScrollLayoutLeft = base.FindTransform("ScrollLayoutLeft");
		this.m_TextHabitat = base.FindTransform("TextHabitat").GetComponent<Text>();
		this.m_TextRefresh = base.FindTransform("TextRefresh").GetComponent<Text>();
		this.m_TextRefreshTime = base.FindTransform("TextRefreshTime").GetComponent<Text>();
		this.m_TextBtnGo = base.FindTransform("TextBtnGo").GetComponent<Text>();
		this.m_TextBossName = base.FindTransform("TextBossName").GetComponent<Text>();
		this.m_TextFightTimes1 = base.FindTransform("TextFightTimes1").GetComponent<Text>();
		this.InitPageBtns();
		this.m_RawImageModel = base.FindTransform("RawImageModel").GetComponent<RawImage>();
		this.m_ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		RTManager.Instance.SetModelRawImage1(this.m_RawImageModel, false);
		this.m_RawImageModel.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.SwitchModelAndDrop(true);
		base.SetAsFirstSibling();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		if (this.m_ScrollLayoutTop.get_childCount() > 0)
		{
			int num = BossBookManager.Instance.CurrentUITabIndex;
			num = ((num >= 0 && num < this.m_ScrollLayoutTop.get_childCount()) ? num : 0);
			BossBookPageBtn component = this.m_ScrollLayoutTop.GetChild(num).GetComponent<BossBookPageBtn>();
			this.SelPage(component.pageId);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		BossBookManager.Instance.SavePushSettings();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.DeleteModel();
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<List<BossKilledLog>>(EventNames.BossSlayLogUpdate, new Callback<List<BossKilledLog>>(this.OpenSlayRecordUI));
		EventDispatcher.AddListener<List<BossDropLog>>(EventNames.BossDropLogUpdate, new Callback<List<BossDropLog>>(this.OpenDropRecordUI));
		EventDispatcher.AddListener<List<int>>(EventNames.BossBookPageUpdate, new Callback<List<int>>(this.OnPageUpdate));
		EventDispatcher.AddListener<int>(EventNames.BossBookItemUpdate, new Callback<int>(this.OnBossUpdate));
		EventDispatcher.AddListener<List<int>>(EventNames.BossBookBossDataUpdate, new Callback<List<int>>(this.OnBossDataUpdate));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<List<BossKilledLog>>(EventNames.BossSlayLogUpdate, new Callback<List<BossKilledLog>>(this.OpenSlayRecordUI));
		EventDispatcher.RemoveListener<List<BossDropLog>>(EventNames.BossDropLogUpdate, new Callback<List<BossDropLog>>(this.OpenDropRecordUI));
		EventDispatcher.RemoveListener<List<int>>(EventNames.BossBookPageUpdate, new Callback<List<int>>(this.OnPageUpdate));
		EventDispatcher.RemoveListener<int>(EventNames.BossBookItemUpdate, new Callback<int>(this.OnBossUpdate));
		EventDispatcher.RemoveListener<List<int>>(EventNames.BossBookBossDataUpdate, new Callback<List<int>>(this.OnBossDataUpdate));
	}

	protected void InitPageBtns()
	{
		Dictionary<int, List<int>>.KeyCollection keys = BossBookManager.Instance.PageDictionary.get_Keys();
		List<int> list = new List<int>();
		using (Dictionary<int, List<int>>.KeyCollection.Enumerator enumerator = keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				list.Add(current);
			}
		}
		list.Sort((int a, int b) => a.CompareTo(b));
		for (int i = 0; i < list.get_Count(); i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("BossBookPageBtn");
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.get_transform().SetParent(this.m_ScrollLayoutTop);
			instantiate2Prefab.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<BossBookPageBtn>().InitBtn(list.get_Item(i), new ButtonCustom.VoidDelegateObj(this.OnClickPageBtn));
			instantiate2Prefab.set_name("BossBookPageBtn" + (i + 1));
			this.lastSelBoss.Add(list.get_Item(i), 0);
		}
	}

	protected void UpdatePageBtns(int newSelPage)
	{
		for (int i = 0; i < this.m_ScrollLayoutTop.get_childCount(); i++)
		{
			Transform child = this.m_ScrollLayoutTop.GetChild(i);
			BossBookPageBtn component = child.GetComponent<BossBookPageBtn>();
			component.SetSelect(component.pageId == newSelPage);
		}
	}

	protected void UpdateBossBtns(int newSelBoss)
	{
		for (int i = 0; i < this.m_ScrollLayoutLeft.get_childCount(); i++)
		{
			Transform child = this.m_ScrollLayoutLeft.GetChild(i);
			BossBookBossBtn component = child.GetComponent<BossBookBossBtn>();
			component.SetSelect(component.bossId == newSelBoss);
		}
	}

	protected void ChangePage(int newSelPage)
	{
		List<int> pageDictionary = BossBookManager.Instance.GetPageDictionary(newSelPage);
		if (pageDictionary == null)
		{
			return;
		}
		this.curPage = newSelPage;
		BossBookManager.Instance.CurrentUITabIndex = this.curPage - 1;
		int i;
		for (i = 0; i < pageDictionary.get_Count(); i++)
		{
			int bossId = pageDictionary.get_Item(i);
			if (this.m_ScrollLayoutLeft.get_childCount() > i)
			{
				GameObject gameObject = this.m_ScrollLayoutLeft.GetChild(i).get_gameObject();
				gameObject.SetActive(true);
				BossBookBossBtn component = gameObject.GetComponent<BossBookBossBtn>();
				if (component != null)
				{
					component.UpdateBtn(bossId);
					component.SetSelect(false);
				}
			}
			else
			{
				Transform transform = ResourceManager.GetInstantiate2Prefab("BossBookBossBtn").get_transform();
				transform.SetParent(this.m_ScrollLayoutLeft);
				transform.set_localScale(new Vector3(1f, 1f, 1f));
				transform.get_gameObject().SetActive(true);
				transform.GetComponent<BossBookBossBtn>().InitBtn(new ButtonCustom.VoidDelegateObj(this.OnClickBossBtn));
				BossBookBossBtn component2 = transform.GetComponent<BossBookBossBtn>();
				component2.UpdateBtn(bossId);
				component2.SetSelect(false);
			}
		}
		for (int j = i; j < this.m_ScrollLayoutLeft.get_childCount(); j++)
		{
			GameObject gameObject2 = this.m_ScrollLayoutLeft.GetChild(j).get_gameObject();
			gameObject2.SetActive(false);
		}
		int bossId2;
		if (this.lastSelBoss.get_Item(this.curPage) != 0)
		{
			bossId2 = this.lastSelBoss.get_Item(this.curPage);
		}
		else
		{
			bossId2 = pageDictionary.get_Item(0);
		}
		this.SelBoss(bossId2);
		this.UpdateFightTimes(this.curPage > 2);
	}

	protected void ClearBossInfo()
	{
		this.m_TextHabitat.set_text(string.Empty);
		this.m_TextRefresh.set_text(string.Empty);
		this.m_TextRefreshTime.set_text(string.Empty);
		this.m_TextBossName.set_text(string.Empty);
		this.m_TextBtnGo.set_text(GameDataUtils.GetChineseContent(517518, false));
		this.SetToggle(false);
		for (int i = 0; i < this.m_ScrollLayoutDrop.get_childCount(); i++)
		{
			Object.Destroy(this.m_ScrollLayoutDrop.GetChild(i).get_gameObject());
		}
		this.m_ScrollLayoutDrop.GetComponent<RectTransform>().set_anchoredPosition(new Vector2(0f, 0f));
		this.curBossId = 0;
	}

	protected void UpdateBossShow(int BossId)
	{
		this.ClearBossInfo();
		if (BossId == 0)
		{
			return;
		}
		BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(BossId);
		if (bossBiaoQian == null)
		{
			return;
		}
		this.curBossId = BossId;
		this.m_TextBossName.set_text(GameDataUtils.GetChineseContent(bossBiaoQian.nameId, false));
		ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(bossBiaoQian.scene);
		this.m_TextHabitat.set_text(GameDataUtils.GetChineseContent(517519, false) + GameDataUtils.GetChineseContent(zhuChengPeiZhi.name, false));
		int vipLv = EntityWorld.Instance.EntSelf.VipLv;
		if (vipLv >= bossBiaoQian.vipLevel)
		{
			this.m_TextBtnGo.set_text(GameDataUtils.GetChineseContent(517518, false));
		}
		else
		{
			this.m_TextBtnGo.set_text(string.Format(GameDataUtils.GetChineseContent(517517, false), bossBiaoQian.vipLevel));
		}
		this.RefreshModel(bossBiaoQian);
		for (int i = 0; i < bossBiaoQian.dropItem.get_Count(); i++)
		{
			ItemShow.ShowItem(this.m_ScrollLayoutDrop, bossBiaoQian.dropItem.get_Item(i), -1L, false, UINodesManager.T2RootOfSpecial, 2001);
		}
	}

	protected void UpdateBossInfo()
	{
		this.EndCountDown();
		BossItemInfo bossItemInfo = BossBookManager.Instance.GetBossItemInfo(this.curBossId);
		if (bossItemInfo == null)
		{
			return;
		}
		this.SetToggle(bossItemInfo.trackFlag);
		if (bossItemInfo.survivalBossCount > 0)
		{
			this.m_TextRefresh.set_text(GameDataUtils.GetChineseContent(517520, false));
			this.m_TextRefreshTime.set_text(bossItemInfo.survivalBossCount.ToString());
		}
		else
		{
			this.m_TextRefresh.set_text(GameDataUtils.GetChineseContent(517521, false));
			int num = BossBookManager.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
			int nextRefreshSec = bossItemInfo.nextRefreshSec;
			if (nextRefreshSec > num)
			{
				this.m_TextRefreshTime.set_text(TimeConverter.GetTime(nextRefreshSec - num, TimeFormat.HHMMSS));
				this.StratCountDown();
			}
			else
			{
				this.m_TextRefresh.set_text(GameDataUtils.GetChineseContent(517520, false));
				this.m_TextRefreshTime.set_text(bossItemInfo.survivalBossCount.ToString());
			}
		}
	}

	protected void UpdateFightTimes(bool isVip)
	{
		if (isVip)
		{
			this.m_TextFightTimes1.set_text(GameDataUtils.GetChineseContent(517524, false));
		}
		else if (this.curPage == 1)
		{
			int num = int.Parse(GameDataUtils.SplitString4Dot0(DataReader<YeWaiBOSS>.Get("limitA").value));
			int id = 517522;
			this.m_TextFightTimes1.set_text(string.Format(GameDataUtils.GetChineseContent(id, false), WildBossManager.Instance.BossRemainRewardTimes, num));
		}
		else if (this.curPage == 2)
		{
			int num = int.Parse(GameDataUtils.SplitString4Dot0(DataReader<YeWaiBOSS>.Get("limitB").value));
			int id = 517523;
			this.m_TextFightTimes1.set_text(string.Format(GameDataUtils.GetChineseContent(id, false), WildBossManager.Instance.MultiBossRemainRewardTimes, num));
		}
	}

	protected void OnClickExit(GameObject go)
	{
		this.Show(false);
	}

	protected void OnClickBtnDropRecord(GameObject go)
	{
		BossBookManager.Instance.SendGetBossDropLogReq(this.curPage);
	}

	protected void OnClickBtnSlayRecord(GameObject go)
	{
		BossBookManager.Instance.SendGetBossKilledLogReq(this.curBossId);
	}

	protected void OnClickBtnMonsterModel(GameObject go)
	{
		this.SwitchModelAndDrop(true);
	}

	protected void OnClickBtnDropPreview(GameObject go)
	{
		this.SwitchModelAndDrop(false);
	}

	private void OnClickTrackToggle(bool check)
	{
		if (this.isClickToggle)
		{
			BossBookManager.Instance.SendTraceBossReq(this.curBossId, check);
		}
		this.isClickToggle = true;
	}

	private void OnClickPageBtn(GameObject go)
	{
		BossBookPageBtn component = go.get_transform().get_parent().GetComponent<BossBookPageBtn>();
		BMuLuFenYe bMuLuFenYe = DataReader<BMuLuFenYe>.Get(component.pageId);
		if (bMuLuFenYe != null && bMuLuFenYe.level > EntityWorld.Instance.EntSelf.Lv)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(606121, false), bMuLuFenYe.level));
			return;
		}
		this.SelPage(component.pageId);
	}

	private void OnClickBossBtn(GameObject go)
	{
		BossBookBossBtn component = go.get_transform().get_parent().GetComponent<BossBookBossBtn>();
		this.SelBoss(component.bossId);
		BossBookManager.Instance.SendGetBossLabelInfoReq(component.bossId);
	}

	private void OnClickBtnGo(GameObject go)
	{
		BossBiaoQian bossBiaoQian = DataReader<BossBiaoQian>.Get(this.curBossId);
		if (bossBiaoQian == null)
		{
			return;
		}
		int vipLv = EntityWorld.Instance.EntSelf.VipLv;
		if (vipLv < bossBiaoQian.vipLevel)
		{
			UIManagerControl.Instance.OpenUI("DialogBoxUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505106, false), null, null, delegate
			{
				this.Show(false);
				LinkNavigationManager.OpenVIPUI2VipLimit();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			return;
		}
		if (BossBookManager.Instance.TeleportAndNavToBoss(this.curBossId))
		{
			this.Show(false);
		}
	}

	protected void OpenSlayRecordUI(List<BossKilledLog> slayLog)
	{
		BossBookSlayRecordUI bossBookSlayRecordUI = UIManagerControl.Instance.OpenUI("BossBookSlayRecordUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BossBookSlayRecordUI;
		bossBookSlayRecordUI.SetSlayLog(this.curBossId, slayLog);
	}

	protected void OpenDropRecordUI(List<BossDropLog> dropLog)
	{
		BossBookDropRecordUI bossBookDropRecordUI = UIManagerControl.Instance.OpenUI("BossBookDropRecordUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BossBookDropRecordUI;
		bossBookDropRecordUI.SetDropLog(dropLog);
	}

	protected void OnPageUpdate(List<int> updataIds)
	{
		if (updataIds.Contains(this.curBossId))
		{
			this.UpdateBossInfo();
		}
	}

	protected void OnBossUpdate(int bossId)
	{
		if (bossId != this.curBossId)
		{
			return;
		}
		this.UpdateBossInfo();
	}

	protected void OnBossDataUpdate(List<int> bossIds)
	{
		for (int i = 0; i < bossIds.get_Count(); i++)
		{
			if (bossIds.get_Item(i) == this.curBossId)
			{
				BossBookManager.Instance.SendGetBossLabelInfoReq(this.curBossId);
				break;
			}
		}
	}

	protected void SelPage(int page)
	{
		this.UpdatePageBtns(page);
		this.ChangePage(page);
		BossBookManager.Instance.SendGetBossPageInfoReq(page);
	}

	protected void SelBoss(int bossId)
	{
		this.UpdateBossBtns(bossId);
		this.UpdateBossShow(bossId);
	}

	protected void SwitchModelAndDrop(bool isShowModel)
	{
		this.m_BtnMonsterModel.SetActive(!isShowModel);
		this.m_PanelModel.SetActive(isShowModel);
		this.m_BtnDropPreview.SetActive(isShowModel);
		this.m_PanelDropPreview.SetActive(!isShowModel);
	}

	private void RefreshModel(BossBiaoQian config)
	{
		List<float> offset = config.modelOffset;
		WaitUI.OpenUI(0u);
		ModelDisplayManager.Instance.ShowModel(config.modelId, true, new Vector2(offset.get_Item(0), offset.get_Item(1)), delegate(int uid)
		{
			this.modelUid = uid;
			ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (uIModel != null)
			{
				Vector3 localPosition = uIModel.get_transform().get_localPosition();
				uIModel.get_transform().set_localPosition(new Vector3(localPosition.x, localPosition.y, offset.get_Item(2)));
				uIModel.get_transform().set_localEulerAngles(new Vector3(0f, config.modelAngle, 0f));
				LayerSystem.SetGameObjectLayer(uIModel.get_gameObject(), "CameraRange", 2);
			}
			WaitUI.CloseUI(0u);
		});
	}

	public void DeleteModel()
	{
		if (this.modelUid > -1)
		{
			ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(this.modelUid);
			if (uIModel != null && uIModel.get_gameObject() != null)
			{
				Object.Destroy(uIModel.get_gameObject());
			}
		}
		this.modelUid = -1;
	}

	private void StratCountDown()
	{
		this.EndCountDown();
		if (this != null && base.get_gameObject().get_activeSelf())
		{
			base.StartCoroutine(this.CountDown());
		}
	}

	private void EndCountDown()
	{
		base.StopAllCoroutines();
	}

	[DebuggerHidden]
	private IEnumerator CountDown()
	{
		BossBookUI.<CountDown>c__Iterator3E <CountDown>c__Iterator3E = new BossBookUI.<CountDown>c__Iterator3E();
		<CountDown>c__Iterator3E.<>f__this = this;
		return <CountDown>c__Iterator3E;
	}

	private void ShowTime()
	{
		BossItemInfo bossItemInfo = BossBookManager.Instance.GetBossItemInfo(this.curBossId);
		if (bossItemInfo == null)
		{
			this.EndCountDown();
			return;
		}
		int num = BossBookManager.ToTimeStamp(TimeManager.Instance.PreciseServerTime);
		int nextRefreshSec = bossItemInfo.nextRefreshSec;
		if (nextRefreshSec > num)
		{
			this.m_TextRefreshTime.set_text(TimeConverter.GetTime(nextRefreshSec - num, TimeFormat.HHMMSS));
		}
		else
		{
			this.EndCountDown();
		}
	}

	private void SetToggle(bool isOn)
	{
		if (this.m_TrackToggle.get_isOn() != isOn)
		{
			this.isClickToggle = false;
			this.m_TrackToggle.set_isOn(isOn);
		}
	}
}
