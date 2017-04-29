using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstanceSelectUI : UIBase
{
	private const float InstancesX = 0f;

	private const float InstancesY = -252f;

	private Image ImageBG_Normal_1;

	private Image ImageBG_Normal_2;

	private Image ImageBG_Elite_1;

	private Image ImageBG_Elite_2;

	private Image ImageBG_Multi_1;

	private Image ImageBG_Multi_2;

	private ButtonCustom BtnImageLocate;

	private ButtonCustom BtnNormal;

	private ButtonCustom BtnElite;

	private ButtonCustom BtnMulti;

	private Text TextChapterTitle;

	private Transform InstancesLayout;

	private ButtonCustom BtnLastChapter;

	private ButtonCustom BtnNextChapter;

	private ButtonCustom BtnBoxReward;

	private Text TextRewardNow;

	private Image TouchPlace;

	private Transform InstancesSelector;

	private Transform Reward;

	private Transform Islands;

	private static InstanceSelectUIState currentInstanceSelectUIState;

	private static DungeonType.ENUM currentDungeonType = DungeonType.ENUM.Normal;

	private static int currentShowChapter = DungeonManager.Instance.chapterStart;

	private List<ChapterInfo> listData;

	private ChapterAwardInfo currentChapterAwardInfo;

	private int fxBoxReward;

	private List<GameObject> listIsland = new List<GameObject>();

	private Dictionary<int, ChapterInfoCustom> dicChaptersNormal = new Dictionary<int, ChapterInfoCustom>();

	private Dictionary<int, ChapterInfoCustom> dicChaptersElite = new Dictionary<int, ChapterInfoCustom>();

	private int minChapterNormal = 1;

	private int maxChapterNormal = 1;

	private int minChapterElite = 1;

	private int maxChapterElite = 1;

	private float dragOffsetCache;

	private float dragOffsetMax = 100f;

	private CanvasGroup CanvasGroupInstances;

	private int currentRewardNeedStar;

	private bool lockBtnBoxReward;

	private int btnChapterNormalCache;

	private int btnChapterEliteCache;

	private int btnChapterMultiCache;

	private Transform FxMaskTrans;

	private int FxID;

	private List<InstancesLayoutItem> listInstanceItem = new List<InstancesLayoutItem>();

	private List<GameObject> listBoss = new List<GameObject>();

	private List<GameObject> listNormal = new List<GameObject>();

	private void Awake()
	{
		base.hideMainCamera = true;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.ImageBG_Normal_1 = base.FindTransform("ImageBG_Normal_1").GetComponent<Image>();
		this.ImageBG_Normal_2 = base.FindTransform("ImageBG_Normal_2").GetComponent<Image>();
		this.ImageBG_Elite_1 = base.FindTransform("ImageBG_Elite_1").GetComponent<Image>();
		this.ImageBG_Elite_2 = base.FindTransform("ImageBG_Elite_2").GetComponent<Image>();
		this.ImageBG_Multi_1 = base.FindTransform("ImageBG_Multi_1").GetComponent<Image>();
		this.ImageBG_Multi_2 = base.FindTransform("ImageBG_Multi_2").GetComponent<Image>();
		ResourceManager.SetSprite(this.ImageBG_Normal_1, ResourceManager.GetIconSprite("bg_fb_blue1"));
		ResourceManager.SetSprite(this.ImageBG_Normal_2, ResourceManager.GetIconSprite("bg_fb_blue2"));
		ResourceManager.SetSprite(this.ImageBG_Elite_1, ResourceManager.GetIconSprite("bg_fb_blue1"));
		ResourceManager.SetSprite(this.ImageBG_Elite_2, ResourceManager.GetIconSprite("bg_fb_blue2"));
		ResourceManager.SetSprite(this.ImageBG_Multi_1, ResourceManager.GetIconSprite("bg_fb_orange1"));
		ResourceManager.SetSprite(this.ImageBG_Multi_2, ResourceManager.GetIconSprite("bg_fb_orange2"));
		this.Islands = base.FindTransform("Islands");
		this.BtnImageLocate = base.FindTransform("BtnImageLocate").GetComponent<ButtonCustom>();
		this.BtnNormal = base.FindTransform("BtnNormal").GetComponent<ButtonCustom>();
		this.BtnElite = base.FindTransform("BtnElite").GetComponent<ButtonCustom>();
		this.BtnMulti = base.FindTransform("BtnMulti").GetComponent<ButtonCustom>();
		this.TextChapterTitle = base.FindTransform("TextChapterTitle").GetComponent<Text>();
		this.InstancesLayout = base.FindTransform("InstancesLayout");
		this.BtnLastChapter = base.FindTransform("BtnLastChapter").GetComponent<ButtonCustom>();
		this.BtnNextChapter = base.FindTransform("BtnNextChapter").GetComponent<ButtonCustom>();
		this.BtnBoxReward = base.FindTransform("BtnBoxReward").GetComponent<ButtonCustom>();
		this.TextRewardNow = base.FindTransform("TextRewardNow").GetComponent<Text>();
		this.TouchPlace = base.FindTransform("TouchPlace").GetComponent<Image>();
		this.InstancesSelector = base.FindTransform("InstancesSelector");
		this.Reward = base.FindTransform("Reward");
		this.CanvasGroupInstances = this.InstancesSelector.GetComponent<CanvasGroup>();
		this.BtnNormal.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnNormal);
		this.BtnElite.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnElite);
		this.BtnMulti.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnMulti);
		this.BtnLastChapter.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnLastChapter);
		this.BtnNextChapter.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnNextChapter);
		this.BtnBoxReward.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBoxReward);
		this.BtnImageLocate.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnImageLocate);
		EventTriggerListener expr_2EA = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_2EA.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_2EA.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragInstance));
		EventTriggerListener expr_31B = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_31B.onBeginDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_31B.onBeginDrag, new EventTriggerListener.VoidDelegateData(this.OnBeginDragInstance));
		EventTriggerListener expr_34C = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_34C.onEndDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_34C.onEndDrag, new EventTriggerListener.VoidDelegateData(this.OnEndDragInstance));
		this.FxMaskTrans = base.FindTransform("FxMask");
		this.InitChapterDatas();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110036), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected void Start()
	{
		this.RefreshUI();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetChapterAwardChangeNty, new Callback(this.OnGetChapterAwardChangeNty));
		EventDispatcher.AddListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetChapterAwardChangeNty, new Callback(this.OnGetChapterAwardChangeNty));
		EventDispatcher.RemoveListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
	}

	private void OnGetChapterAwardChangeNty()
	{
		this.ResetChapterData();
		this.RefreshBoxReward(InstanceSelectUI.currentShowChapter, InstanceSelectUI.currentDungeonType);
		this.ResetChapterRewardBadge(InstanceSelectUI.currentShowChapter, InstanceSelectUI.currentDungeonType);
	}

	private void InstanceDataHaveChange()
	{
		this.RefreshUI();
	}

	private void OnClickIslandItem(GameObject sender)
	{
		Debug.LogError("sender.name  " + sender.get_name());
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(int.Parse(sender.get_name()));
		this.RefreshUI(zhuXianZhangJiePeiZhi.chapterOrder, InstanceSelectUI.currentDungeonType);
	}

	private void OnClickBtnImageLocate(GameObject sender)
	{
	}

	private void OnClickBtnNormal(GameObject sender)
	{
		ChapterResume chapterResume = DungeonManager.Instance.listChapterResume.Find((ChapterResume a) => a.dungeonType == DungeonType.ENUM.Normal);
		if (DungeonManager.Instance.NormalData.get_Count() == 0)
		{
			DungeonManager.Instance.SendGetDungeonDataReq(chapterResume.chapterId, chapterResume.dungeonType, delegate
			{
				this.OnClickBtnNormal(null);
			});
			return;
		}
		if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Normal)
		{
			return;
		}
		this.CacheCurrentChapterByType();
		if (this.btnChapterNormalCache != 0)
		{
			this.RefreshUI(this.btnChapterNormalCache, DungeonType.ENUM.Normal);
		}
		else
		{
			ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(DungeonManager.Instance.GetTheLastInstaceID(InstanceType.DungeonNormal));
			this.RefreshUI(DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi.chapterId).chapterOrder, DungeonType.ENUM.Normal);
		}
		UIManagerControl.Instance.HideUI("EliteDungeonUI");
	}

	private void OnClickBtnElite(GameObject sender)
	{
		InstanceManagerUI.OpenEliteDungeonUI();
	}

	private void OnClickBtnMulti(GameObject sender)
	{
		ChapterResume chapterResume = DungeonManager.Instance.listChapterResume.Find((ChapterResume a) => a.dungeonType == DungeonType.ENUM.Team);
		if (DungeonManager.Instance.TeamData.get_Count() == 0)
		{
			DungeonManager.Instance.SendGetDungeonDataReq(chapterResume.chapterId, chapterResume.dungeonType, delegate
			{
				this.OnClickBtnMulti(null);
			});
			return;
		}
		if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Team)
		{
			return;
		}
		this.CacheCurrentChapterByType();
		if (this.btnChapterMultiCache != 0)
		{
			this.RefreshUI(this.btnChapterMultiCache, DungeonType.ENUM.Team);
		}
		else
		{
			ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(DungeonManager.Instance.GetTheLastInstaceID(InstanceType.DungeonMutiPeople));
			this.RefreshUI(DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi.chapterId).chapterOrder, DungeonType.ENUM.Team);
		}
	}

	private void OnClickBtnLastChapter(GameObject sender)
	{
		if (this.CheckCanLastChapter())
		{
			InstanceSelectUI.currentShowChapter--;
			if (InstanceSelectUI.currentShowChapter < DungeonManager.Instance.chapterStart)
			{
				InstanceSelectUI.currentShowChapter = DungeonManager.Instance.chapterStart;
			}
			this.RefreshUI(InstanceSelectUI.currentShowChapter, InstanceSelectUI.currentDungeonType);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("没有上一个章节了");
		}
	}

	private void OnClickBtnNextChapter(GameObject sender)
	{
		Debug.LogError("OnClickBtnNextChapter");
		if (this.CheckCanNextChapter())
		{
			int chapterCheck = InstanceSelectUI.currentShowChapter + 1;
			ZhuXianZhangJiePeiZhi datazj = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterType == (int)InstanceSelectUI.currentDungeonType && a.chapterOrder == chapterCheck);
			if (this.listData.Find((ChapterInfo a) => a.chapterId == datazj.id) == null)
			{
				DungeonManager.Instance.SendGetDungeonDataReq(datazj.id, InstanceSelectUI.currentDungeonType, delegate
				{
					this.OnClickBtnNextChapter(null);
				});
				return;
			}
			InstanceSelectUI.currentShowChapter++;
			this.RefreshUI(InstanceSelectUI.currentShowChapter, InstanceSelectUI.currentDungeonType);
		}
	}

	private void OnClickBtnBoxReward(GameObject sender)
	{
		if (this.lockBtnBoxReward)
		{
			return;
		}
		if (BackpackManager.Instance.ShowBackpackFull())
		{
			return;
		}
		bool flag = true;
		ChapterInfoCustom chapterInfoCustom = null;
		if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Normal)
		{
			chapterInfoCustom = this.dicChaptersNormal.get_Item(InstanceSelectUI.currentShowChapter);
		}
		else if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Elite)
		{
			chapterInfoCustom = this.dicChaptersElite.get_Item(InstanceSelectUI.currentShowChapter);
		}
		ChapterInfo chapterInfo = this.listData.get_Item(InstanceSelectUI.currentShowChapter - 1);
		if (!chapterInfoCustom.canGetReward && chapterInfo.totalStar == chapterInfoCustom.needStar.get_Item(chapterInfoCustom.needStar.get_Count() - 1))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505077, false));
			return;
		}
		if (!this.currentChapterAwardInfo.canReceive)
		{
			flag = false;
		}
		XDict<string, int> xDict = BoxRewardManager.Instance.ParseAwardId(this.currentChapterAwardInfo.chapterAwardId);
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(xDict["chapter"]);
		List<int> itemIDList = new List<int>();
		List<long> itemCountList = new List<long>();
		if (zhuXianZhangJiePeiZhi != null)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			for (int i = 0; i < zhuXianZhangJiePeiZhi.needStar.get_Count(); i++)
			{
				if (zhuXianZhangJiePeiZhi.needStar.get_Item(i) == xDict["needstar"])
				{
					text = zhuXianZhangJiePeiZhi.rewardItem.get_Item(i).value;
					text2 = zhuXianZhangJiePeiZhi.rewardNum.get_Item(i).value;
					break;
				}
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			string[] array2 = text2.Split(new char[]
			{
				','
			});
			for (int j = 0; j < array.Length; j++)
			{
				itemIDList.Add(int.Parse(array[j]));
				itemCountList.Add((long)int.Parse(array2[j]));
			}
		}
		if (flag)
		{
			this.lockBtnBoxReward = true;
			if (this.FxMaskTrans == null)
			{
				this.FxMaskTrans = UINodesManager.MiddleUIRoot;
			}
			else
			{
				this.FxMaskTrans.get_gameObject().SetActive(true);
			}
			RewardUI rewardui;
			this.FxID = FXSpineManager.Instance.ReplaySpine(this.FxID, 801, this.FxMaskTrans, string.Empty, 14000, delegate
			{
				FXSpineManager.Instance.PlaySpine(802, this.FxMaskTrans, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				rewardui = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
				rewardui.SetRewardItem("获得奖励", itemIDList, itemCountList, true, true, delegate
				{
					BoxRewardManager.Instance.SendReceiveAwardReq(this.currentChapterAwardInfo.chapterAwardId);
					FXSpineManager.Instance.DeleteSpine(this.FxID, true);
					if (this.FxMaskTrans != UINodesManager.MiddleUIRoot)
					{
						this.FxMaskTrans.get_gameObject().SetActive(false);
						for (int k = 0; k < this.FxMaskTrans.get_childCount(); k++)
						{
							Transform child = this.FxMaskTrans.GetChild(k);
							Object.Destroy(child.get_gameObject());
						}
					}
				}, null);
				this.lockBtnBoxReward = false;
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.PlaySpine(803, this.FxMaskTrans, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			RewardUI rewardui = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardui.SetRewardItem("奖励预览", itemIDList, itemCountList, false, false, delegate
			{
				string text3 = GameDataUtils.GetChineseContent(510111, false);
				text3 = text3.Replace("{s1}", this.currentRewardNeedStar.ToString());
				UIManagerControl.Instance.ShowToastText(text3);
			}, null);
		}
	}

	private void OnClcikInstancesLayoutItem(GameObject sender)
	{
		InstancesLayoutItem component = sender.GetComponent<InstancesLayoutItem>();
		Hashtable hashtable = DungeonManager.Instance.CheckLock(component.instanceID);
		bool flag = (bool)hashtable.get_Item("ISLock");
		string text = (string)hashtable.get_Item("LockReason");
		if (flag)
		{
			UIManagerControl.Instance.ShowToastText(text);
		}
		else
		{
			FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(component.instanceID);
			if (fuBenJiChuPeiZhi.type != 103)
			{
				UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("InstanceSelectUI");
				if (uIIfExist != null)
				{
					(uIIfExist as InstanceSelectUI).RefreshUIByInstanceID(component.instanceID);
				}
				InstanceDetailUI instanceDetailUI = UIManagerControl.Instance.OpenUI("InstanceDetailUI", null, false, UIType.Pop) as InstanceDetailUI;
				InstanceManagerUI.InstanceID = component.instanceID;
				instanceDetailUI.RefreshUI(component.instanceID);
			}
		}
	}

	public void RefreshUIByInstanceID(int instanceID)
	{
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		this.RefreshUI(DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi.chapterId).chapterOrder, (DungeonType.ENUM)DataReader<FuBenJiChuPeiZhi>.Get(instanceID).type);
	}

	private void RefreshUI()
	{
		this.RefreshUI(InstanceSelectUI.currentShowChapter, InstanceSelectUI.currentDungeonType);
	}

	private void RefreshUI(int chapter, DungeonType.ENUM dungeonType)
	{
		InstanceSelectUI.currentShowChapter = chapter;
		BoxRewardManager.Instance.GetChapterAward(InstanceSelectUI.currentShowChapter, (int)dungeonType, delegate
		{
			this.ResetChapterData();
			this.RefreshDungeonTypeData(dungeonType);
			this.RefreshStateBtnsAndSetCurrentDungeonType(dungeonType);
			this.RefreshInstanceItems(chapter, dungeonType);
			this.RefreshBoxReward(chapter, dungeonType);
			this.ResetChapterRewardBadge(chapter, dungeonType);
			this.RefreshLeftAndRightButton(chapter, dungeonType);
			this.ResetImageIslands(chapter, dungeonType);
		});
	}

	private void RefreshDungeonTypeData(DungeonType.ENUM type)
	{
		this.listData = DungeonManager.Instance.GetDataByInstanceType((int)type);
	}

	private void ResetChapterData()
	{
		using (Dictionary<int, ChapterInfoCustom>.Enumerator enumerator = this.dicChaptersNormal.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ChapterInfoCustom> current = enumerator.get_Current();
				ChapterInfoCustom value = current.get_Value();
				this.MakeCic(value);
			}
		}
		using (Dictionary<int, ChapterInfoCustom>.Enumerator enumerator2 = this.dicChaptersElite.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<int, ChapterInfoCustom> current2 = enumerator2.get_Current();
				ChapterInfoCustom value2 = current2.get_Value();
				this.MakeCic(value2);
			}
		}
	}

	private void MakeCic(ChapterInfoCustom cic)
	{
		for (int i = 0; i < cic.needStar.get_Count(); i++)
		{
			int key = BoxRewardManager.Instance.MakeBoxRewardID(cic.chapterID, cic.type, cic.needStar.get_Item(i));
			if (BoxRewardManager.Instance.m_mapChapterAwards.ContainsKey(key))
			{
				ChapterAwardInfo chapterAwardInfo = BoxRewardManager.Instance.m_mapChapterAwards[key];
				if (chapterAwardInfo.canReceive && !chapterAwardInfo.isReceived)
				{
					cic.canGetReward = true;
					break;
				}
				cic.canGetReward = false;
			}
		}
		for (int j = 0; j < cic.needStar.get_Count(); j++)
		{
			int key2 = BoxRewardManager.Instance.MakeBoxRewardID(cic.chapterID, cic.type, cic.needStar.get_Item(j));
			if (BoxRewardManager.Instance.m_mapChapterAwards.ContainsKey(key2))
			{
				ChapterAwardInfo chapterAwardInfo2 = BoxRewardManager.Instance.m_mapChapterAwards[key2];
				if (chapterAwardInfo2.canReceive && !chapterAwardInfo2.isReceived)
				{
					cic.starCondition = cic.needStar.get_Item(j);
					break;
				}
				if (!chapterAwardInfo2.canReceive)
				{
					cic.starCondition = cic.needStar.get_Item(j);
					break;
				}
				if (chapterAwardInfo2.canReceive && chapterAwardInfo2.isReceived)
				{
					if (j == cic.needStar.get_Count() - 1)
					{
						cic.starCondition = cic.needStar.get_Item(j);
					}
				}
			}
		}
	}

	private void InitChapterDatas()
	{
		for (int i = 0; i < DataReader<ZhuXianZhangJiePeiZhi>.DataList.get_Count(); i++)
		{
			ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.DataList.get_Item(i);
			ChapterInfoCustom chapterInfoCustom = new ChapterInfoCustom();
			chapterInfoCustom.chapter = zhuXianZhangJiePeiZhi.chapterOrder;
			chapterInfoCustom.type = zhuXianZhangJiePeiZhi.chapterType;
			chapterInfoCustom.needStar.AddRange(zhuXianZhangJiePeiZhi.needStar);
			chapterInfoCustom.chapterID = zhuXianZhangJiePeiZhi.id;
			if (zhuXianZhangJiePeiZhi.chapterType == 101)
			{
				this.dicChaptersNormal.Add(zhuXianZhangJiePeiZhi.chapterOrder, chapterInfoCustom);
				if (zhuXianZhangJiePeiZhi.chapterOrder < this.minChapterNormal)
				{
					this.minChapterNormal = zhuXianZhangJiePeiZhi.chapterOrder;
				}
				else if (zhuXianZhangJiePeiZhi.chapterOrder > this.maxChapterNormal)
				{
					this.maxChapterNormal = zhuXianZhangJiePeiZhi.chapterOrder;
				}
			}
			else if (zhuXianZhangJiePeiZhi.chapterType == 102)
			{
				this.dicChaptersElite.Add(zhuXianZhangJiePeiZhi.chapterOrder, chapterInfoCustom);
				if (zhuXianZhangJiePeiZhi.chapterOrder < this.minChapterElite)
				{
					this.minChapterElite = zhuXianZhangJiePeiZhi.chapterOrder;
				}
				else if (zhuXianZhangJiePeiZhi.chapterOrder > this.maxChapterElite)
				{
					this.maxChapterElite = zhuXianZhangJiePeiZhi.chapterOrder;
				}
			}
		}
	}

	private void ResetImageIslands(int chapter, DungeonType.ENUM dungeonType)
	{
		int num = 0;
		bool flag = false;
		for (int i = 0; i < this.listData.get_Count(); i++)
		{
			ChapterInfo chapterInfo = this.listData.get_Item(i);
			for (int j = 0; j < chapterInfo.dungeons.get_Count(); j++)
			{
				DungeonInfo dungeonInfo = chapterInfo.dungeons.get_Item(j);
				if (!dungeonInfo.clearance)
				{
					num = i;
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
		}
		for (int k = 0; k < this.listIsland.get_Count(); k++)
		{
			Object.Destroy(this.listIsland.get_Item(k));
		}
		this.listIsland.Clear();
		for (int l = 0; l <= num; l++)
		{
			ChapterInfo chapterInfo2 = this.listData.get_Item(l);
			ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(chapterInfo2.chapterId);
			if (zhuXianZhangJiePeiZhi.mainSenceIcon != 0)
			{
				GameObject gameObject = new GameObject(chapterInfo2.chapterId.ToString());
				gameObject.get_transform().set_parent(this.Islands);
				this.listIsland.Add(gameObject);
				Image image = gameObject.AddComponent<Image>();
				ResourceManager.SetSprite(image, ResourceManager.GetIconSprite(DataReader<Icon>.Get(zhuXianZhangJiePeiZhi.mainSenceIcon).icon));
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.set_localPosition(new Vector2((float)zhuXianZhangJiePeiZhi.mainSenceIconPoint.get_Item(0), (float)zhuXianZhangJiePeiZhi.mainSenceIconPoint.get_Item(1)));
				component.set_localScale(Vector3.get_one());
				image.SetNativeSize();
				ButtonCustom buttonCustom = gameObject.AddComponent<ButtonCustom>();
				buttonCustom.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickIslandItem);
			}
		}
	}

	private void ResetChapterRewardBadge(int chapter, DungeonType.ENUM dungeonType)
	{
		ChapterInfoCustom chapterInfoCustom = this.dicChaptersNormal.get_Item(chapter);
		if (chapterInfoCustom.canGetReward)
		{
			this.BtnNormal.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnNormal.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
		chapterInfoCustom = this.dicChaptersElite.get_Item(chapter);
		if (chapterInfoCustom.canGetReward)
		{
			this.BtnElite.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnElite.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
		this.BtnNormal.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BoxRewardManager.Instance.CheckNormalDungeonBadge());
		this.BtnElite.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(BoxRewardManager.Instance.CheckEliteDungeonBadge());
		this.BtnLastChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		this.BtnNextChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		if (dungeonType == DungeonType.ENUM.Normal)
		{
			for (int i = chapter - 1; i >= this.minChapterNormal; i--)
			{
				chapterInfoCustom = this.dicChaptersNormal.get_Item(i);
				if (chapterInfoCustom.canGetReward)
				{
					this.BtnLastChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
					break;
				}
			}
			for (int j = chapter + 1; j <= this.maxChapterNormal; j++)
			{
				chapterInfoCustom = this.dicChaptersNormal.get_Item(j);
				if (chapterInfoCustom.canGetReward)
				{
					this.BtnNextChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
					break;
				}
			}
		}
		else if (dungeonType == DungeonType.ENUM.Elite)
		{
			for (int k = chapter - 1; k >= this.minChapterNormal; k--)
			{
				chapterInfoCustom = this.dicChaptersElite.get_Item(k);
				if (chapterInfoCustom.canGetReward)
				{
					this.BtnLastChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
					break;
				}
			}
			for (int l = chapter + 1; l <= this.maxChapterNormal; l++)
			{
				chapterInfoCustom = this.dicChaptersElite.get_Item(l);
				if (chapterInfoCustom.canGetReward)
				{
					this.BtnNextChapter.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
					break;
				}
			}
		}
	}

	private void RefreshBoxReward(int chapter, DungeonType.ENUM dungeonType)
	{
		if (dungeonType == DungeonType.ENUM.Normal || dungeonType == DungeonType.ENUM.Elite)
		{
			this.Reward.get_gameObject().SetActive(true);
			ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterOrder == chapter && a.chapterType == (int)dungeonType);
			if (zhuXianZhangJiePeiZhi != null)
			{
				for (int i = 0; i < zhuXianZhangJiePeiZhi.needStar.get_Count(); i++)
				{
					int needstar = zhuXianZhangJiePeiZhi.needStar.get_Item(i);
					int key = BoxRewardManager.Instance.MakeBoxRewardID(zhuXianZhangJiePeiZhi.id, (int)dungeonType, needstar);
					if (BoxRewardManager.Instance.m_mapChapterAwards.ContainsKey(key))
					{
						ChapterAwardInfo chapterAwardInfo = BoxRewardManager.Instance.m_mapChapterAwards[key];
						if (chapterAwardInfo.canReceive && !chapterAwardInfo.isReceived)
						{
							this.currentChapterAwardInfo = chapterAwardInfo;
							break;
						}
						if (!chapterAwardInfo.canReceive)
						{
							this.currentChapterAwardInfo = chapterAwardInfo;
							break;
						}
					}
					else
					{
						Debug.LogError("cai == null");
					}
				}
				ChapterInfoCustom chapterInfoCustom = null;
				if (dungeonType == DungeonType.ENUM.Normal)
				{
					chapterInfoCustom = this.dicChaptersNormal.get_Item(chapter);
				}
				else if (dungeonType == DungeonType.ENUM.Elite)
				{
					chapterInfoCustom = this.dicChaptersElite.get_Item(chapter);
				}
				ChapterInfo chapterInfo = this.listData.get_Item(chapter - 1);
				if (chapterInfoCustom.canGetReward)
				{
					this.BtnBoxReward.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
					this.BtnBoxReward.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
					if (this.fxBoxReward != 0)
					{
						FXSpineManager.Instance.DeleteSpine(this.fxBoxReward, true);
					}
					this.fxBoxReward = FXSpineManager.Instance.PlaySpine(1601, this.BtnBoxReward.get_transform().FindChild("FX"), "InstanceSelectUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}
				else if (!chapterInfoCustom.canGetReward && chapterInfo.totalStar == chapterInfoCustom.needStar.get_Item(chapterInfoCustom.needStar.get_Count() - 1))
				{
					this.BtnBoxReward.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
					this.BtnBoxReward.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
					if (this.fxBoxReward != 0)
					{
						FXSpineManager.Instance.DeleteSpine(this.fxBoxReward, true);
					}
				}
				else
				{
					this.BtnBoxReward.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
					this.BtnBoxReward.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
					if (this.fxBoxReward != 0)
					{
						FXSpineManager.Instance.DeleteSpine(this.fxBoxReward, true);
					}
				}
				this.currentRewardNeedStar = chapterInfoCustom.starCondition;
				this.TextRewardNow.set_text(string.Concat(new object[]
				{
					string.Empty,
					chapterInfo.totalStar,
					"/",
					chapterInfoCustom.starCondition
				}));
			}
		}
		else
		{
			this.Reward.get_gameObject().SetActive(false);
		}
	}

	private void ResetItems()
	{
		for (int i = 0; i < this.listBoss.get_Count(); i++)
		{
			this.listBoss.get_Item(i).SetActive(false);
		}
		for (int j = 0; j < this.listNormal.get_Count(); j++)
		{
			this.listNormal.get_Item(j).SetActive(false);
		}
		this.listInstanceItem.Clear();
	}

	private GameObject GetBoss()
	{
		for (int i = 0; i < this.listBoss.get_Count(); i++)
		{
			if (!this.listBoss.get_Item(i).get_activeSelf())
			{
				this.listBoss.get_Item(i).SetActive(true);
				this.listBoss.get_Item(i).GetComponent<RectTransform>().SetAsLastSibling();
				return this.listBoss.get_Item(i);
			}
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstancesLayoutBossItem");
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.GetComponent<RectTransform>().SetAsLastSibling();
		this.listBoss.Add(instantiate2Prefab);
		return instantiate2Prefab;
	}

	private GameObject GetNormal()
	{
		for (int i = 0; i < this.listNormal.get_Count(); i++)
		{
			if (!this.listNormal.get_Item(i).get_activeSelf())
			{
				this.listNormal.get_Item(i).SetActive(true);
				this.listNormal.get_Item(i).GetComponent<RectTransform>().SetAsLastSibling();
				return this.listNormal.get_Item(i);
			}
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstancesLayoutItem");
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.GetComponent<RectTransform>().SetAsLastSibling();
		this.listNormal.Add(instantiate2Prefab);
		return instantiate2Prefab;
	}

	private void RefreshInstanceItems(int chapter, DungeonType.ENUM dungeonType)
	{
		this.ResetItems();
		ChapterInfo chapterInfo = this.listData.get_Item(chapter - 1);
		List<DungeonInfo> list = new List<DungeonInfo>();
		list.AddRange(chapterInfo.dungeons);
		for (int i = 0; i < list.get_Count(); i++)
		{
			int num = i;
			for (int j = i + 1; j < list.get_Count(); j++)
			{
				DungeonInfo dungeonInfo = list.get_Item(num);
				ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(dungeonInfo.dungeonId);
				if (zhuXianPeiZhi == null)
				{
					Debug.LogError("GameData.InstanceConfigure no key = " + dungeonInfo.dungeonId);
				}
				else
				{
					DungeonInfo dungeonInfo2 = list.get_Item(j);
					ZhuXianPeiZhi zhuXianPeiZhi2 = DataReader<ZhuXianPeiZhi>.Get(dungeonInfo2.dungeonId);
					if (zhuXianPeiZhi2 == null)
					{
						Debug.LogError("GameData.InstanceConfigure no key = " + dungeonInfo2.dungeonId);
					}
					else if (zhuXianPeiZhi.instance > zhuXianPeiZhi2.instance)
					{
						num = j;
					}
				}
			}
			if (num != i)
			{
				XUtility.ListExchange<DungeonInfo>(list, i, num);
			}
		}
		for (int k = 0; k < list.get_Count(); k++)
		{
			DungeonInfo di = list.get_Item(k);
			if (k == 0)
			{
				FuBenJiChuPeiZhi icTmp = DataReader<FuBenJiChuPeiZhi>.Get(di.dungeonId);
				ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterType == icTmp.type && a.chapterOrder == DataReader<ZhuXianZhangJiePeiZhi>.Get(DataReader<ZhuXianPeiZhi>.Get(di.dungeonId).chapterId).chapterOrder);
				string chineseContent = GameDataUtils.GetChineseContent(zhuXianZhangJiePeiZhi.chapterName, false);
				string[] array = chineseContent.Split(new char[]
				{
					' '
				});
				this.TextChapterTitle.set_text(array[1] + "<size=25>(" + array[0] + ")</size>");
			}
			ZhuXianPeiZhi zhuXianPeiZhi3 = DataReader<ZhuXianPeiZhi>.Get(di.dungeonId);
			InstancesLayoutItem component;
			if (zhuXianPeiZhi3.bossInstanceBoss == 1)
			{
				GameObject boss = this.GetBoss();
				component = boss.GetComponent<InstancesLayoutItem>();
				boss.get_transform().SetParent(this.InstancesLayout);
				boss.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				boss.set_name(di.dungeonId.ToString());
			}
			else
			{
				GameObject normal = this.GetNormal();
				component = normal.GetComponent<InstancesLayoutItem>();
				normal.get_transform().SetParent(this.InstancesLayout);
				normal.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				normal.set_name(di.dungeonId.ToString());
			}
			component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClcikInstancesLayoutItem);
			this.listInstanceItem.Add(component);
			bool isLock = (bool)DungeonManager.Instance.CheckLock(di.dungeonId).get_Item("ISLock");
			component.RefreshUI(DataReader<FuBenJiChuPeiZhi>.Get(di.dungeonId), isLock, k, di.star);
		}
	}

	private void RefreshStateBtnsAndSetCurrentDungeonType(DungeonType.ENUM dungeonType)
	{
		switch (InstanceSelectUI.currentDungeonType)
		{
		case DungeonType.ENUM.Normal:
			this.BtnNormal.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnNormal.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.BtnNormal.get_transform().FindChild("Text1").get_gameObject().SetActive(true);
			this.BtnNormal.get_transform().FindChild("Text2").get_gameObject().SetActive(false);
			this.ImageBG_Normal_1.get_gameObject().SetActive(false);
			this.ImageBG_Normal_2.get_gameObject().SetActive(false);
			break;
		case DungeonType.ENUM.Elite:
			this.BtnElite.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnElite.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.BtnElite.get_transform().FindChild("Text1").get_gameObject().SetActive(true);
			this.BtnElite.get_transform().FindChild("Text2").get_gameObject().SetActive(false);
			this.ImageBG_Elite_1.get_gameObject().SetActive(false);
			this.ImageBG_Elite_2.get_gameObject().SetActive(false);
			break;
		case DungeonType.ENUM.Team:
			this.BtnMulti.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnMulti.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.BtnMulti.get_transform().FindChild("Text1").get_gameObject().SetActive(true);
			this.BtnMulti.get_transform().FindChild("Text2").get_gameObject().SetActive(false);
			this.ImageBG_Multi_1.get_gameObject().SetActive(false);
			this.ImageBG_Multi_2.get_gameObject().SetActive(false);
			break;
		}
		InstanceSelectUI.currentDungeonType = dungeonType;
		switch (InstanceSelectUI.currentDungeonType)
		{
		case DungeonType.ENUM.Normal:
			this.BtnNormal.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnNormal.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.BtnNormal.get_transform().FindChild("Text1").get_gameObject().SetActive(false);
			this.BtnNormal.get_transform().FindChild("Text2").get_gameObject().SetActive(true);
			this.ImageBG_Normal_1.get_gameObject().SetActive(true);
			this.ImageBG_Normal_2.get_gameObject().SetActive(true);
			break;
		case DungeonType.ENUM.Elite:
			this.BtnElite.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnElite.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.BtnElite.get_transform().FindChild("Text1").get_gameObject().SetActive(false);
			this.BtnElite.get_transform().FindChild("Text2").get_gameObject().SetActive(true);
			this.ImageBG_Elite_1.get_gameObject().SetActive(true);
			this.ImageBG_Elite_2.get_gameObject().SetActive(true);
			break;
		case DungeonType.ENUM.Team:
			this.BtnMulti.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnMulti.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.BtnMulti.get_transform().FindChild("Text1").get_gameObject().SetActive(false);
			this.BtnMulti.get_transform().FindChild("Text2").get_gameObject().SetActive(true);
			this.ImageBG_Multi_1.get_gameObject().SetActive(true);
			this.ImageBG_Multi_2.get_gameObject().SetActive(true);
			break;
		}
	}

	private void RefreshLeftAndRightButton(int chapter, DungeonType.ENUM dungeontype)
	{
		int num = (int)float.Parse(DataReader<GlobalParams>.Get("chapter_show").value);
		int dungeonType = (int)dungeontype;
		if ((DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi data) => data.chapterOrder == chapter + 1 && data.chapterType == dungeonType) == null || chapter >= num) && chapter != DungeonManager.Instance.chapterStart)
		{
			this.BtnNextChapter.set_enabled(false);
			this.BtnLastChapter.set_enabled(true);
			ImageColorMgr.SetImageColor(this.BtnNextChapter.get_transform().FindChild("Image").GetComponent<Image>(), true);
			ImageColorMgr.SetImageColor(this.BtnLastChapter.get_transform().FindChild("Image").GetComponent<Image>(), false);
		}
		else if (chapter == DungeonManager.Instance.chapterStart && DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi data) => data.chapterOrder == chapter + 1 && data.chapterType == dungeonType) != null)
		{
			this.BtnNextChapter.set_enabled(true);
			this.BtnLastChapter.set_enabled(false);
			ImageColorMgr.SetImageColor(this.BtnNextChapter.get_transform().FindChild("Image").GetComponent<Image>(), false);
			ImageColorMgr.SetImageColor(this.BtnLastChapter.get_transform().FindChild("Image").GetComponent<Image>(), true);
		}
		else if (chapter == DungeonManager.Instance.chapterStart && DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi data) => data.chapterOrder == chapter + 1 && data.chapterType == dungeonType) == null)
		{
			this.BtnNextChapter.set_enabled(false);
			this.BtnLastChapter.set_enabled(false);
			ImageColorMgr.SetImageColor(this.BtnNextChapter.get_transform().FindChild("Image").GetComponent<Image>(), true);
			ImageColorMgr.SetImageColor(this.BtnLastChapter.get_transform().FindChild("Image").GetComponent<Image>(), true);
		}
		else
		{
			this.BtnNextChapter.set_enabled(true);
			this.BtnLastChapter.set_enabled(true);
			ImageColorMgr.SetImageColor(this.BtnNextChapter.get_transform().FindChild("Image").GetComponent<Image>(), false);
			ImageColorMgr.SetImageColor(this.BtnLastChapter.get_transform().FindChild("Image").GetComponent<Image>(), false);
		}
	}

	private bool CheckCanNextChapter()
	{
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterOrder == InstanceSelectUI.currentShowChapter && a.chapterType == (int)InstanceSelectUI.currentDungeonType);
		using (List<ChapterInfo>.Enumerator enumerator = this.listData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ChapterInfo current = enumerator.get_Current();
				if (zhuXianZhangJiePeiZhi.id == current.chapterId)
				{
					using (List<DungeonInfo>.Enumerator enumerator2 = current.dungeons.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							DungeonInfo current2 = enumerator2.get_Current();
							if (!current2.clearance)
							{
								UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505098, false));
								bool result = false;
								return result;
							}
						}
					}
					break;
				}
			}
		}
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi2 = DataReader<ZhuXianZhangJiePeiZhi>.DataList.Find((ZhuXianZhangJiePeiZhi a) => a.chapterOrder == InstanceSelectUI.currentShowChapter + 1 && a.chapterType == (int)InstanceSelectUI.currentDungeonType);
		if (zhuXianZhangJiePeiZhi2 != null && EntityWorld.Instance.EntSelf.Lv < zhuXianZhangJiePeiZhi2.chapterLv)
		{
			string text = GameDataUtils.GetChineseContent(505086, false);
			text = text.Replace("{s1}", zhuXianZhangJiePeiZhi2.chapterLv.ToString());
			UIManagerControl.Instance.ShowToastText(text);
			return false;
		}
		return true;
	}

	private bool CheckCanLastChapter()
	{
		return InstanceSelectUI.currentShowChapter - 1 >= DungeonManager.Instance.chapterStart;
	}

	private void OnEndDragInstance(PointerEventData eventData)
	{
		this.CanvasGroupInstances.GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(this.CanvasGroupInstances.get_alpha(), 1f, 0f, 0.2f);
		this.CanvasGroupInstances.GetComponent<BaseTweenPostion>().MoveTo(new Vector3(0f, -252f, 0f), 0.2f);
		if (this.dragOffsetCache > this.dragOffsetMax * 0.7f)
		{
			if (this.CheckCanLastChapter())
			{
				this.OnClickBtnLastChapter(null);
			}
		}
		else if (this.dragOffsetCache < 0f - this.dragOffsetMax * 0.7f && this.CheckCanNextChapter())
		{
			this.OnClickBtnNextChapter(null);
		}
	}

	private void OnBeginDragInstance(PointerEventData eventData)
	{
		this.dragOffsetCache = 0f;
	}

	private void OnDragInstance(PointerEventData eventData)
	{
		this.dragOffsetCache += eventData.get_delta().x;
		float num = this.dragOffsetMax - Mathf.Abs(this.dragOffsetCache);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = num / this.dragOffsetMax;
		if (num2 < 0.3f)
		{
			num2 = 0.3f;
		}
		this.CanvasGroupInstances.set_alpha(num2);
	}

	private void CacheCurrentChapterByType()
	{
		if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Normal)
		{
			this.btnChapterNormalCache = InstanceSelectUI.currentShowChapter;
		}
		else if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Elite)
		{
			this.btnChapterEliteCache = InstanceSelectUI.currentShowChapter;
		}
		else if (InstanceSelectUI.currentDungeonType == DungeonType.ENUM.Team)
		{
			this.btnChapterMultiCache = InstanceSelectUI.currentShowChapter;
		}
	}
}
