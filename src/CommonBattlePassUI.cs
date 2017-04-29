using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonBattlePassUI : UIBase
{
	public Action ExitCallback;

	public Action AgainCallback;

	public Action MultipleCallback;

	public Action HookRewardCallback;

	private Text norewardText;

	private Text passTimeText;

	private Text btnTipText;

	private Text btnExitText;

	private Text btnStatictisText;

	private Text btnAgainText;

	private Text btnMultipleText;

	private Text btnMultCoinText;

	private Text hookRewardsWaveText;

	private ButtonCustom btnExit;

	private ButtonCustom btnStatictis;

	private ButtonCustom btnAgain;

	private ButtonCustom btnMultiple;

	private ButtonCustom btnHookRewards;

	private List<Transform> expAndGoldList;

	private Transform passTimeTrans;

	private Transform currentStageTrans;

	private Transform fxTrans;

	private Transform pvpRewardsTrans;

	private Transform rewardBgsTrans;

	private Transform itemsRegionTrans;

	private Transform weaponRewardsTrans;

	private GameObject hookRewardsGo;

	private Image mGodWeaponIcon;

	private GameObject mMultExpPanel;

	private GameObject mGoMultExp;

	private Text mTxOldLevel;

	private Text mTxNewLevel;

	private long mGetExp;

	private ListPool rewardItemsUpListPool;

	private ListPool rewardItemsDownListPool;

	private bool _BtnTipTextVisibility;

	private bool _BtnExitVisibility;

	private bool _BtnAgainVisibility;

	private bool _BtnStatictisVisibity;

	private bool _BtnMultipleVisibility;

	private string _BtnExitContent;

	private string _BtnAgainContent;

	private string _BtnStatictisContent;

	private string _BtnTipTextContent;

	private string _BtnMultipleTextContent;

	private string _HookRewardsWaveTextContent;

	private bool _PassTimeVisibility;

	private string _PassTimeTextContent;

	private bool _CurrentStageVisibility;

	private int mFxGodWeaponId;

	private TimeCountDown timeCountDown;

	private int fx_WinExplode;

	private int fx_WinStart;

	private int fx_WinIdle;

	private int fx_ResultExplode;

	private int fx_ResultStart;

	private int fx_ResultIdle;

	private int fx_LoseExplode;

	private int fx_LoseStart;

	private int fx_LoseIdle;

	private int fx_TimesUpExplode;

	private int fx_TimesUpStart;

	private int fx_TimesUpIdle;

	public bool BtnTipTextVisibility
	{
		get
		{
			return this._BtnTipTextVisibility;
		}
		set
		{
			this._BtnTipTextVisibility = value;
			this.btnTipText.get_gameObject().SetActive(this._BtnTipTextVisibility);
		}
	}

	public bool BtnExitVisibility
	{
		get
		{
			return this._BtnExitVisibility;
		}
		set
		{
			this._BtnExitVisibility = value;
			this.btnExit.get_gameObject().SetActive(this._BtnExitVisibility);
		}
	}

	public bool BtnAgainVisibility
	{
		get
		{
			return this._BtnAgainVisibility;
		}
		set
		{
			this._BtnAgainVisibility = value;
			this.btnAgain.get_gameObject().SetActive(this._BtnAgainVisibility);
		}
	}

	public bool BtnStatictisVisibity
	{
		get
		{
			return this._BtnStatictisVisibity;
		}
		set
		{
			this._BtnStatictisVisibity = value;
			this.btnStatictis.get_gameObject().SetActive(this._BtnStatictisVisibity);
		}
	}

	public bool BtnMultipleVisibility
	{
		get
		{
			return this._BtnMultipleVisibility;
		}
		set
		{
			this._BtnMultipleVisibility = value;
			this.btnMultiple.get_gameObject().SetActive(this._BtnMultipleVisibility);
		}
	}

	public string BtnExitContent
	{
		get
		{
			return this._BtnExitContent;
		}
		set
		{
			this._BtnExitContent = value;
		}
	}

	public string BtnAgainContent
	{
		get
		{
			return this._BtnAgainContent;
		}
		set
		{
			this._BtnAgainContent = value;
		}
	}

	public string BtnStatictisContent
	{
		get
		{
			return this._BtnStatictisContent;
		}
		set
		{
			this._BtnStatictisContent = value;
		}
	}

	public string BtnTipTextContent
	{
		get
		{
			return this._BtnTipTextContent;
		}
		set
		{
			this._BtnTipTextContent = value;
			this.btnTipText.set_text(this._BtnTipTextContent);
		}
	}

	public string BtnMultipleTextContent
	{
		get
		{
			return this._BtnMultipleTextContent;
		}
		set
		{
			this._BtnMultipleTextContent = value;
			this.btnMultipleText.set_text(this._BtnMultipleTextContent);
		}
	}

	public string HookRewardsWaveTextContent
	{
		get
		{
			return this._HookRewardsWaveTextContent;
		}
		set
		{
			this._HookRewardsWaveTextContent = value;
			this.hookRewardsWaveText.set_text(this._HookRewardsWaveTextContent);
		}
	}

	public bool PassTimeVisibility
	{
		get
		{
			return this._PassTimeVisibility;
		}
		set
		{
			this._PassTimeVisibility = value;
			this.passTimeTrans.get_gameObject().SetActive(this._PassTimeVisibility);
		}
	}

	public string PassTimeTextContent
	{
		get
		{
			return this._PassTimeTextContent;
		}
		set
		{
			this._PassTimeTextContent = value;
			this.passTimeText.set_text(this._PassTimeTextContent);
		}
	}

	public bool CurrentStageVisibility
	{
		get
		{
			return this._CurrentStageVisibility;
		}
		set
		{
			this._CurrentStageVisibility = value;
			this.currentStageTrans.get_gameObject().SetActive(this._CurrentStageVisibility);
		}
	}

	public bool NormalRewardVisibility
	{
		set
		{
			this.itemsRegionTrans.get_gameObject().SetActive(value);
		}
	}

	public bool DropRewardVisibility
	{
		set
		{
			this.rewardItemsUpListPool.get_gameObject().SetActive(value);
			this.rewardItemsDownListPool.get_gameObject().SetActive(value);
		}
	}

	public int WeaponRewardId
	{
		set
		{
			this.weaponRewardsTrans.get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.mGodWeaponIcon, GameDataUtils.GetIcon(value));
			this.mFxGodWeaponId = FXSpineManager.Instance.ReplaySpine(this.mFxGodWeaponId, 3907, this.mGodWeaponIcon.get_transform(), "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public string NoRewardTipText
	{
		get
		{
			if (this.norewardText != null)
			{
				return this.norewardText.get_text();
			}
			return string.Empty;
		}
		set
		{
			if (this.norewardText != null)
			{
				this.norewardText.set_text(value);
			}
		}
	}

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
		this.norewardText = base.FindTransform("NoRewardText").GetComponent<Text>();
		this.passTimeText = base.FindTransform("PassTimeNum").GetComponent<Text>();
		this.btnTipText = base.FindTransform("BtnTipText").GetComponent<Text>();
		this.btnExitText = base.FindTransform("BtnExitName").GetComponent<Text>();
		this.btnAgainText = base.FindTransform("BtnAgainName").GetComponent<Text>();
		this.btnStatictisText = base.FindTransform("BtnStatisticsName").GetComponent<Text>();
		this.btnMultipleText = base.FindTransform("BtnMultipleName").GetComponent<Text>();
		this.btnMultCoinText = base.FindTransform("txPrice").GetComponent<Text>();
		this.hookRewardsWaveText = base.FindTransform("HookRewardsWave").GetComponent<Text>();
		this.btnExit = base.FindTransform("BtnExit").GetComponent<ButtonCustom>();
		this.btnExit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExitBtn);
		this.btnAgain = base.FindTransform("BtnAgain").GetComponent<ButtonCustom>();
		this.btnAgain.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAgainBtn);
		this.btnStatictis = base.FindTransform("BtnStatistics").GetComponent<ButtonCustom>();
		this.btnStatictis.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStatisticUp);
		this.btnMultiple = base.FindTransform("BtnMultiple").GetComponent<ButtonCustom>();
		this.btnMultiple.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMultiple);
		this.btnHookRewards = base.FindTransform("HookRewardsBtn").GetComponent<ButtonCustom>();
		this.btnHookRewards.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHookReward);
		this.fxTrans = base.FindTransform("FXRoot");
		this.currentStageTrans = base.FindTransform("CurrentStage");
		this.passTimeTrans = base.FindTransform("PassTime");
		this.pvpRewardsTrans = base.FindTransform("PvpRewards");
		this.rewardBgsTrans = base.FindTransform("RewardBgs");
		this.itemsRegionTrans = base.FindTransform("ItemsRegion");
		this.weaponRewardsTrans = base.FindTransform("WeaponRewards");
		this.hookRewardsGo = base.FindTransform("HookRewards").get_gameObject();
		this.mGodWeaponIcon = base.FindTransform("WeaponIcon").GetComponent<Image>();
		this.mMultExpPanel = base.FindTransform("MultExpRewards").get_gameObject();
		this.mGoMultExp = base.FindTransform("MultExp").get_gameObject();
		this.mTxOldLevel = base.FindTransform("txOldLevel").GetComponent<Text>();
		this.mTxNewLevel = base.FindTransform("txNewLevel").GetComponent<Text>();
		this.expAndGoldList = new List<Transform>();
		this.expAndGoldList.Add(base.FindTransform("Exp"));
		this.expAndGoldList.Add(base.FindTransform("Gold"));
		this.rewardItemsUpListPool = base.FindTransform("RewardItemsUp").GetComponent<ListPool>();
		this.rewardItemsDownListPool = base.FindTransform("RewardItemsDown").GetComponent<ListPool>();
		this.rewardItemsUpListPool.SetItem("RewardItem");
		this.rewardItemsDownListPool.SetItem("RewardItem");
		base.FindTransform("HookRewardsTip").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(511614, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.NoRewardTipText = string.Empty;
		this.UpdateRewardData();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.StopCountDown();
		this.ClearRewards();
		this.ClearGodWeapon();
		this.ClearMultExp();
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
		EventDispatcher.AddListener(DungeonManagerEvent.OnGetDropItems, new Callback(this.UpdateRewardData));
		EventDispatcher.AddListener(EventNames.ExitExperienceSuccess, new Callback(this.OnExitExperienceSuccess));
		EventDispatcher.AddListener(EventNames.GetExperienceCopyRewardRes, new Callback(this.RefreshMultReward));
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.RefreshMultReward));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(DungeonManagerEvent.OnGetDropItems, new Callback(this.UpdateRewardData));
		EventDispatcher.RemoveListener(EventNames.ExitExperienceSuccess, new Callback(this.OnExitExperienceSuccess));
		EventDispatcher.RemoveListener(EventNames.GetExperienceCopyRewardRes, new Callback(this.RefreshMultReward));
		EventDispatcher.RemoveListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.RefreshMultReward));
	}

	private void OnClickStatisticUp(GameObject go)
	{
		InstanceDamageCal instanceDamageCal = UIManagerControl.Instance.OpenUI("InstanceDamageCal", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as InstanceDamageCal;
		instanceDamageCal.ResetUI();
	}

	private void OnClickExitBtn(GameObject go)
	{
		LoadingUIView.Open(false);
		this.Show(false);
		if (this.ExitCallback != null)
		{
			this.ExitCallback.Invoke();
		}
	}

	private void OnClickAgainBtn(GameObject go)
	{
		if (this.AgainCallback != null)
		{
			this.AgainCallback.Invoke();
		}
	}

	private void OnClickMultiple(GameObject go)
	{
		if (this.MultipleCallback != null)
		{
			this.MultipleCallback.Invoke();
		}
	}

	private void OnClickHookReward(GameObject go)
	{
		if (this.HookRewardCallback != null)
		{
			this.HookRewardCallback.Invoke();
		}
	}

	private void UpdateRewardData()
	{
		this.SetPassTime();
	}

	private void SetAllReward(List<DropItem> list)
	{
		if (list == null)
		{
			return;
		}
		List<KeyValuePair<int, long>> list2 = new List<KeyValuePair<int, long>>();
		List<KeyValuePair<int, long>> list3 = new List<KeyValuePair<int, long>>();
		for (int i = 0; i < list.get_Count(); i++)
		{
			int typeId = list.get_Item(i).typeId;
			long count = list.get_Item(i).count;
			Items item = BackpackManager.Instance.GetItem(typeId);
			if (item != null)
			{
				if (typeId < 5)
				{
					list3.Add(new KeyValuePair<int, long>(typeId, count));
				}
				else if (item.tab == 2)
				{
					int num = 0;
					while ((long)num < count)
					{
						list2.Add(new KeyValuePair<int, long>(typeId, 1L));
						num++;
					}
				}
				else
				{
					list2.Add(new KeyValuePair<int, long>(typeId, count));
				}
			}
		}
		this.SetExpAndGold(list3);
		this.SetDropItem(list2);
	}

	private void SetExpAndGold(List<KeyValuePair<int, long>> specialItems = null)
	{
		int num = 0;
		if (specialItems != null)
		{
			for (int i = 0; i < specialItems.get_Count(); i++)
			{
				if (num < 2)
				{
					int key = specialItems.get_Item(i).get_Key();
					long value = specialItems.get_Item(i).get_Value();
					if (key <= 5 && value > 0L)
					{
						Items item = BackpackManager.Instance.GetItem(key);
						if (item != null)
						{
							this.expAndGoldList.get_Item(num).get_gameObject().SetActive(true);
							Image component = this.expAndGoldList.get_Item(num).FindChild("Icon").GetComponent<Image>();
							ResourceManager.SetSprite(component, GameDataUtils.GetIcon(item.littleIcon));
							this.expAndGoldList.get_Item(num).FindChild("Num").GetComponent<Text>().set_text(Utils.SwitchChineseNumber(value, 0));
							num++;
						}
					}
				}
			}
		}
		for (int j = num; j < 2; j++)
		{
			this.expAndGoldList.get_Item(j).get_gameObject().SetActive(false);
		}
	}

	private void SetDropItem(List<KeyValuePair<int, long>> item)
	{
		this.ClearRewards();
		if (item == null || item.get_Count() <= 0)
		{
			return;
		}
		int halfNum = item.get_Count() / 2;
		int num = 0;
		int num2;
		if (item.get_Count() <= 4)
		{
			num2 = item.get_Count();
		}
		else
		{
			num2 = halfNum;
			num = item.get_Count() - halfNum;
		}
		this.rewardItemsUpListPool.Create(num2, delegate(int index)
		{
			if (index < item.get_Count() && index < this.rewardItemsUpListPool.Items.get_Count())
			{
				int key = item.get_Item(index).get_Key();
				long value = item.get_Item(index).get_Value();
				this.rewardItemsUpListPool.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(key, value, 0L);
			}
		});
		if (num > 0)
		{
			this.rewardItemsDownListPool.Create(num, delegate(int index)
			{
				int num3 = index + halfNum;
				if (num3 < item.get_Count() && index < this.rewardItemsDownListPool.Items.get_Count())
				{
					int key = item.get_Item(num3).get_Key();
					long value = item.get_Item(num3).get_Value();
					this.rewardItemsDownListPool.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(key, value, 0L);
				}
			});
		}
	}

	private void SetItemBriefInfos(List<ItemBriefInfo> items)
	{
		this.ClearRewards();
		if (items == null || items.get_Count() <= 0)
		{
			return;
		}
		int halfNum = items.get_Count() / 2;
		int num = 0;
		int num2;
		if (items.get_Count() <= 4)
		{
			num2 = items.get_Count();
		}
		else
		{
			num2 = halfNum;
			num = items.get_Count() - halfNum;
		}
		this.rewardItemsUpListPool.Create(num2, delegate(int index)
		{
			if (index < items.get_Count() && index < this.rewardItemsUpListPool.Items.get_Count())
			{
				int cfgId = items.get_Item(index).cfgId;
				long count = items.get_Item(index).count;
				long uId = items.get_Item(index).uId;
				this.rewardItemsUpListPool.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(cfgId, count, uId);
			}
		});
		if (num > 0)
		{
			this.rewardItemsDownListPool.Create(num, delegate(int index)
			{
				int num3 = index + halfNum;
				if (num3 < items.get_Count() && index < this.rewardItemsDownListPool.Items.get_Count())
				{
					int cfgId = items.get_Item(num3).cfgId;
					long count = items.get_Item(num3).count;
					long uId = items.get_Item(num3).uId;
					this.rewardItemsDownListPool.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(cfgId, count, uId);
				}
			});
		}
	}

	private void SetPassTime()
	{
		int curUsedTime = InstanceManager.CurUsedTime;
		this.passTimeText.set_text(GameDataUtils.GetChineseContent(501004, false) + " " + TimeConverter.GetTime(curUsedTime, TimeFormat.HHMMSS));
	}

	private void OnExitExperienceSuccess()
	{
		this.Show(false);
	}

	private void RefreshMultReward()
	{
		this.BtnMultipleVisibility = false;
		this.mGoMultExp.SetActive(true);
		this.mTxOldLevel.set_text("Lv." + ExperienceCopyInstance.Instance.BeforeLevel);
		this.mTxNewLevel.set_text("Lv." + EntityWorld.Instance.EntSelf.Lv);
		this.expAndGoldList.get_Item(0).FindChild("Num").GetComponent<Text>().set_text(AttrUtility.GetExpValueStr(this.mGetExp * 2L));
	}

	private void ClearGodWeapon()
	{
		this.weaponRewardsTrans.get_gameObject().SetActive(false);
		FXSpineManager.Instance.DeleteSpine(this.mFxGodWeaponId, true);
	}

	public void UpdateScResultUI(SecretAreaChallengeResultNty result)
	{
		if (result == null)
		{
			return;
		}
		this._PassTimeTextContent = string.Empty;
		this.CurrentStageVisibility = true;
		int num = (result.startStage - 1 <= 0) ? 1 : (result.startStage - 1);
		this.currentStageTrans.FindChild("StartStageText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(512053, false), num));
		this.currentStageTrans.FindChild("EndStageText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(512054, false), result.endStage));
		this.UpdateDungeonRewards(result.copyRewards);
	}

	public void UpdateEliteUI(List<DropItem> list)
	{
		if (list == null)
		{
			return;
		}
		this.SetAllReward(list);
	}

	public void OnCountDownToExit(int countTime, Action countDownEndAction)
	{
		this.StopCountDown();
		this.timeCountDown = new TimeCountDown(countTime, TimeFormat.SECOND, delegate
		{
			if (this.btnTipText != null)
			{
				this.btnTipText.set_text(string.Format("<color=green>" + this.timeCountDown.GetSeconds() + "秒</color>", new object[0]) + "后自动离开");
			}
		}, delegate
		{
			this.StopCountDown();
			countDownEndAction.Invoke();
		}, true);
	}

	private void StopCountDown()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}

	public void UpdateExperienceCopyUI(ResultExperienceCopyNty result, int batch = -1)
	{
		string[] array = DataReader<FJingYanFuBenPeiZhi>.Get("ExpCost").value.Split(new char[]
		{
			';'
		});
		if (array.Length == 2)
		{
			ResourceManager.SetSprite(base.FindTransform("CoinIcon").GetComponent<Image>(), GameDataUtils.GetIcon(DataReader<Items>.Get(int.Parse(array[0])).littleIcon));
			this.btnMultCoinText.set_text("x" + array[1]);
			this.BtnMultipleTextContent = DataReader<FJingYanFuBenPeiZhi>.Get("Exp").num + "倍经验";
		}
		this.BtnTipTextContent = ((batch < 0) ? string.Empty : string.Format(GameDataUtils.GetChineseContent(513536, false), batch.ToString()));
		this.mGetExp = SpecialFightManager.Instance.GetBatchExp(batch) * (long)batch;
		this.expAndGoldList.get_Item(0).FindChild("Num").GetComponent<Text>().set_text(AttrUtility.GetExpValueStr(this.mGetExp));
		this.expAndGoldList.get_Item(0).get_gameObject().SetActive(true);
		this.expAndGoldList.get_Item(1).get_gameObject().SetActive(false);
		this.mTxOldLevel.set_text("Lv." + ExperienceCopyInstance.Instance.BeforeLevel);
		this.mTxNewLevel.set_text("Lv." + this.GetNewLevel(ExperienceCopyInstance.Instance.BeforeExp + this.mGetExp, ExperienceCopyInstance.Instance.BeforeLevel));
		this.mMultExpPanel.SetActive(true);
		Debug.Log(string.Concat(new object[]
		{
			"当前Exp:",
			ExperienceCopyInstance.Instance.BeforeExp,
			", 获得Exp:",
			this.mGetExp,
			", 当前等级: Lv.",
			ExperienceCopyInstance.Instance.BeforeLevel
		}));
	}

	private int GetNewLevel(long getExp, int curLevel)
	{
		GenericAttribute genericAttribute = DataReader<GenericAttribute>.Get(curLevel);
		if (genericAttribute == null)
		{
			return curLevel - 1;
		}
		if (getExp > genericAttribute.personExp)
		{
			return this.GetNewLevel(getExp - genericAttribute.personExp, curLevel + 1);
		}
		return curLevel;
	}

	public void UpdateTowerCopyUI(DefendFightBtlResultNty result)
	{
		this.PassTimeTextContent = GameDataUtils.GetChineseContent(501004, false) + " " + TimeConverter.GetTime(result.result.killTargetUsedTime, TimeFormat.HHMMSS);
		int id = (DefendFightManager.Instance.SelectDetailMode != DefendFightMode.DFMD.Protect) ? 513536 : 513537;
		this.BtnTipTextContent = string.Format(GameDataUtils.GetChineseContent(id, false), result.maxWave.ToString());
		List<ItemBriefInfo> normalDropItems = result.normalDropItems;
		if (normalDropItems != null && result.extendDropItems != null && result.extendDropItems.get_Count() > 0)
		{
			normalDropItems.AddRange(result.extendDropItems);
		}
		this.UpdateDungeonRewards(normalDropItems);
	}

	public void UpdateGuildBossReward(ChallengeGuildBossNty result)
	{
		if (result == null)
		{
			return;
		}
		this.passTimeText.set_text(GameDataUtils.GetChineseContent(515060, false));
	}

	public void UpdateHookReward(long exp, long gold, int wave, Action hookRewardCallback, Action exitCallback)
	{
		List<KeyValuePair<int, long>> list = new List<KeyValuePair<int, long>>();
		list.Add(new KeyValuePair<int, long>(1, exp));
		list.Add(new KeyValuePair<int, long>(2, gold));
		this.SetExpAndGold(list);
		this.hookRewardsGo.SetActive(true);
		this.HookRewardsWaveTextContent = string.Format(GameDataUtils.GetChineseContent(511615, false), wave);
		this.HookRewardCallback = hookRewardCallback;
		this.ExitCallback = exitCallback;
	}

	public void UpdateDarkTrialReward(bool isWin, int time, int wave, long exp, long gold, List<KeyValuePair<int, long>> reward, Action exitCallback)
	{
		if (isWin)
		{
			this.PassTimeTextContent = string.Format(GameDataUtils.GetChineseContent(50710, false), TimeConverter.GetTime(time, TimeFormat.HHMMSS));
		}
		else
		{
			this.PassTimeTextContent = string.Format(GameDataUtils.GetChineseContent(50709, false), wave);
		}
		this.ExitCallback = exitCallback;
	}

	public void UpdatePvpInstanceUI(ArenaChallengeBattleResult result)
	{
		if (result == null)
		{
			return;
		}
		this.pvpRewardsTrans.get_gameObject().SetActive(true);
		this.CurrentStageVisibility = false;
		this.PassTimeVisibility = false;
		this.BtnTipTextContent = string.Empty;
		this.rewardBgsTrans.get_gameObject().SetActive(false);
		this.itemsRegionTrans.get_gameObject().SetActive(false);
		switch (result.status)
		{
		case ArenaChallengeBattleResult.BattleResult.Win:
			base.FindTransform("pvpCoinIntergral").get_gameObject().SetActive(result.competitiveCurrency > 0);
			if (result.competitiveCurrency > 0)
			{
				base.FindTransform("pvpCoinValue").GetComponent<Text>().set_text("+" + result.competitiveCurrency.ToString());
			}
			break;
		}
		ResourceManager.SetSprite(base.FindTransform("sign").GetComponent<Image>(), ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(result.oldScore + result.gainScore, true)));
		base.FindTransform("homeIntergralValue").GetComponent<Text>().set_text(result.gainScore.ToString());
		base.FindTransform("victoryTimeValue").GetComponent<Text>().set_text(result.combatWinCount.ToString());
	}

	public void UpdateDungeonRewards(List<ItemBriefInfo> rewardList)
	{
		if (rewardList == null)
		{
			return;
		}
		List<ItemBriefInfo> list = new List<ItemBriefInfo>();
		List<KeyValuePair<int, long>> list2 = new List<KeyValuePair<int, long>>();
		for (int i = 0; i < rewardList.get_Count(); i++)
		{
			int cfgId = rewardList.get_Item(i).cfgId;
			long count = rewardList.get_Item(i).count;
			if (BackpackManager.Instance.GetItem(cfgId) != null)
			{
				if (cfgId <= 5)
				{
					list2.Add(new KeyValuePair<int, long>(cfgId, count));
				}
				else
				{
					list.Add(rewardList.get_Item(i));
				}
			}
		}
		this.SetExpAndGold(list2);
		this.SetItemBriefInfos(list);
	}

	public void SetDropItems(PassUICommonDrop commonDrop)
	{
		List<KeyValuePair<int, long>> list = new List<KeyValuePair<int, long>>();
		List<KeyValuePair<int, long>> list2 = new List<KeyValuePair<int, long>>();
		if (commonDrop != null)
		{
			list2.Add(new KeyValuePair<int, long>(1, commonDrop.exp));
			list2.Add(new KeyValuePair<int, long>(2, commonDrop.gold));
			for (int i = 0; i < commonDrop.item.get_Count(); i++)
			{
				int key = commonDrop.item.get_Item(i).get_Key();
				long value = commonDrop.item.get_Item(i).get_Value();
				Items items = DataReader<Items>.Get(key);
				if (items != null && items.tab == 2)
				{
					int num = 0;
					while ((long)num < value)
					{
						list.Add(new KeyValuePair<int, long>(key, 1L));
						num++;
					}
				}
				else
				{
					list.Add(commonDrop.item.get_Item(i));
				}
			}
		}
		this.SetExpAndGold(list2);
		this.SetDropItem(list);
	}

	public void PlayAnimation(InstanceResultType type)
	{
		this.DeleteAllFX();
		TimerHeap.AddTimer(50u, 0, delegate
		{
			if (type == InstanceResultType.Win)
			{
				this.fx_WinStart = FXSpineManager.Instance.PlaySpine(425, this.fxTrans, "CommonBattlePassUI", 2002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
					this.fx_WinIdle = FXSpineManager.Instance.ReplaySpine(this.fx_WinIdle, 422, this.fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_WinExplode = FXSpineManager.Instance.PlaySpine(428, this.fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			else if (type == InstanceResultType.Result)
			{
				this.fx_ResultStart = FXSpineManager.Instance.PlaySpine(423, this.fxTrans, "CommonBattlePassUI", 2002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_ResultStart, true);
					this.fx_ResultIdle = FXSpineManager.Instance.ReplaySpine(this.fx_ResultIdle, 420, this.fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_ResultExplode = FXSpineManager.Instance.PlaySpine(426, this.fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			else if (type == InstanceResultType.Lose)
			{
				this.fx_LoseStart = FXSpineManager.Instance.PlaySpine(424, this.fxTrans, "CommonBattlePassUI", 2002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
					this.fx_LoseIdle = FXSpineManager.Instance.ReplaySpine(this.fx_LoseIdle, 421, this.fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_LoseExplode = FXSpineManager.Instance.PlaySpine(427, this.fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			else if (type == InstanceResultType.TimesUp)
			{
				this.fx_TimesUpStart = FXSpineManager.Instance.PlaySpine(430, this.fxTrans, "CommonBattlePassUI", 2002, delegate
				{
					FXSpineManager.Instance.DeleteSpine(this.fx_TimesUpStart, true);
					this.fx_TimesUpIdle = FXSpineManager.Instance.ReplaySpine(this.fx_LoseIdle, 429, this.fxTrans, "CommonBattlePassUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.fx_TimesUpExplode = FXSpineManager.Instance.PlaySpine(431, this.fxTrans, "CommonBattlePassUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		});
	}

	private void DeleteAllFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_WinExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinIdle, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_ResultIdle, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_LoseIdle, true);
	}

	public void ShowAnimation()
	{
		base.get_gameObject().GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(0f, 1f, 0f, 0.1f, delegate
		{
		});
	}

	private void ClearRewards()
	{
		this.rewardItemsUpListPool.Clear();
		this.rewardItemsDownListPool.Clear();
		this.NoRewardTipText = string.Empty;
		this.hookRewardsGo.SetActive(false);
	}

	private void ClearMultExp()
	{
		if (this.mGoMultExp != null)
		{
			this.mGoMultExp.SetActive(false);
		}
		if (this.mMultExpPanel != null)
		{
			this.mMultExpPanel.SetActive(false);
		}
		this.BtnMultipleVisibility = false;
	}
}
