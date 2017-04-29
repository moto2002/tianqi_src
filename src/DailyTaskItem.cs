using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyTaskItem : MonoBehaviour
{
	private Image mIcon;

	private Text mTxName;

	private Text mTxDesc;

	private Text mTxButton;

	private GameObject mGoGoto;

	private ButtonCustom mBtnGoto;

	private Text mTxLvTips;

	private Text mTxLiveness;

	private GameObject mGoFinished;

	private Text mTxPrice;

	private Image mImgPrice;

	private GameObject mRewards;

	private GameObject mRewardPrefab;

	private GameObject mGoFinded;

	private List<GameObject> mRewardItem;

	private readonly int[] ICON_ID = new int[]
	{
		36200,
		36203
	};

	private Action<DailyTaskItem> mEventHandler;

	private MeiRiMuBiao mDailyData;

	private MZhaoHui mFindData;

	private List<DiaoLuo> mFindReward;

	private DailyTask mTask;

	private DailyTaskType mType;

	private int mFxId;

	private Transform mSpine;

	private int mCurRewardCount;

	public MeiRiMuBiao DailyData
	{
		get
		{
			return this.mDailyData;
		}
	}

	public MZhaoHui FindData
	{
		get
		{
			return this.mFindData;
		}
	}

	public List<DiaoLuo> FindReward
	{
		get
		{
			return this.mFindReward;
		}
	}

	public DailyTask Task
	{
		get
		{
			return this.mTask;
		}
	}

	public DailyTaskType Type
	{
		get
		{
			return this.mType;
		}
	}

	public int CurrentLimit
	{
		get;
		private set;
	}

	public bool GuildWarOpen
	{
		get;
		private set;
	}

	public bool CanShowBuy
	{
		get;
		private set;
	}

	public int GoldPrice
	{
		get
		{
			return this.mFindData.goldUnivalent;
		}
	}

	public int DiamondPrice
	{
		get
		{
			return this.mFindData.diamondUnivalent;
		}
	}

	public string TaskName
	{
		get
		{
			return this.mTxName.get_text();
		}
	}

	private void Awake()
	{
		this.mIcon = UIHelper.GetImage(base.get_transform(), "Icon/icon");
		this.mTxName = UIHelper.GetText(base.get_transform(), "txName");
		this.mTxDesc = UIHelper.GetText(base.get_transform(), "txDesc");
		this.mGoGoto = UIHelper.GetObject(base.get_transform(), "Goto");
		this.mBtnGoto = UIHelper.GetCustomButton(this.mGoGoto, "BtnGoto");
		this.mTxButton = UIHelper.GetText(this.mBtnGoto.get_transform(), "Text");
		this.mTxLvTips = UIHelper.GetText(base.get_transform(), "UnOpen/Text");
		this.mTxLiveness = UIHelper.GetText(base.get_transform(), "txReward");
		this.mGoFinished = UIHelper.GetObject(base.get_transform(), "Finish");
		this.mSpine = UIHelper.GetObject(base.get_transform(), "Spine").get_transform();
		this.mGoFinded = UIHelper.GetObject(base.get_transform(), "Finded");
		this.mTxPrice = UIHelper.GetText(this.mGoGoto, "Price/Text");
		this.mImgPrice = UIHelper.GetImage(this.mGoGoto, "Price/Text/Image");
		this.mRewards = UIHelper.GetObject(base.get_transform(), "Rewards");
		this.mRewardPrefab = UIHelper.GetObject(this.mRewards, "Item");
		this.mRewardItem = new List<GameObject>();
	}

	private void Start()
	{
		this.mBtnGoto.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetReward);
		UIHelper.GetButton(base.get_transform(), "background").get_onClick().AddListener(new UnityAction(this.OnClickIcon));
	}

	private void OnClickGetReward(GameObject go)
	{
		if (this.mEventHandler != null)
		{
			this.mEventHandler.Invoke(this);
		}
	}

	private void OnClickIcon()
	{
		TaskTipsUI taskTipsUI = UIManagerControl.Instance.OpenUI("TaskTipsUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TaskTipsUI;
		taskTipsUI.OnOpen(this.mDailyData);
	}

	private void SetDailyData(MeiRiMuBiao dailyData)
	{
		this.SetTypeVisable(true, this.mTask.count >= dailyData.completeTime);
		this.mTxDesc.set_text(string.Format(GameDataUtils.GetChineseContent(dailyData.introduction2, false), dailyData.completeTime, this.mTask.count, dailyData.completeTime));
		this.mTxLiveness.set_text(string.Format(GameDataUtils.GetChineseContent(301048, false), Utils.SwitchChineseNumber((long)dailyData.vitality, 0)));
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(dailyData.sysId);
		if (EntityWorld.Instance.EntSelf.Lv < systemOpen.level)
		{
			this.mGoGoto.get_gameObject().SetActive(false);
			this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(true);
			this.mTxLvTips.set_text(string.Format(GameDataUtils.GetChineseContent(301034, false), systemOpen.level));
		}
		else if (systemOpen.taskId > 0 && !MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId))
		{
			string text = GameDataUtils.GetChineseContent(301035, false);
			RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(systemOpen.taskId);
			if (renWuPeiZhi != null)
			{
				text = string.Format(GameDataUtils.GetChineseContent(301036, false), GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false));
			}
			this.mGoGoto.get_gameObject().SetActive(false);
			this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(true);
			this.mTxLvTips.set_text(text);
		}
		else if (dailyData.id == 12040 && !GuildManager.Instance.IsJoinInGuild())
		{
			this.mGoGoto.get_gameObject().SetActive(false);
			this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(true);
			this.mTxLvTips.set_text(GameDataUtils.GetChineseContent(301049, false));
		}
	}

	private void SetFindData(MZhaoHui findData, bool isGoldBuy)
	{
		if (findData == null)
		{
			return;
		}
		this.SetTypeVisable(false, this.mTask.canFindTimes <= 0);
		this.ClearRewardItem();
		List<int> list;
		if (isGoldBuy)
		{
			list = findData.goldReward;
			this.mTxPrice.set_text(Utils.SwitchChineseNumber((long)this.GoldPrice, 0));
			ResourceManager.SetSprite(this.mImgPrice, ResourceManager.GetIconSprite("nav_icon_gold"));
		}
		else
		{
			list = findData.diamondReward;
			this.mTxPrice.set_text(Utils.SwitchChineseNumber((long)this.DiamondPrice, 0));
			ResourceManager.SetSprite(this.mImgPrice, ResourceManager.GetIconSprite("nav_icon_diamond"));
		}
		List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
		int lv = EntityWorld.Instance.EntSelf.Lv;
		if (this.mFindReward == null)
		{
			this.mFindReward = new List<DiaoLuo>();
		}
		else
		{
			this.mFindReward.Clear();
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			int num = list.get_Item(i);
			for (int j = 0; j < dataList.get_Count(); j++)
			{
				DiaoLuo diaoLuo = dataList.get_Item(j);
				if (diaoLuo.ruleId == num)
				{
					if (diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0)
					{
						this.mFindReward.Add(diaoLuo);
					}
					else if (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv)
					{
						this.mFindReward.Add(diaoLuo);
					}
					else if (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)
					{
						this.mFindReward.Add(diaoLuo);
					}
				}
			}
		}
		for (int k = 0; k < this.mFindReward.get_Count(); k++)
		{
			if (this.mFindReward.get_Count() < 3 || this.mFindReward.get_Item(k).goodsId != 1)
			{
				this.CreateReward(this.mFindReward.get_Item(k));
			}
		}
		this.mTxDesc.set_text(string.Format(GameDataUtils.GetChineseContent(301037, false), this.mTask.canFindTimes));
		this.mGoGoto.SetActive(this.mTask.canFindTimes > 0);
	}

	private void SetTimeData(MeiRiMuBiao dailyData)
	{
		this.SetDailyData(dailyData);
		this.mTxLiveness.get_gameObject().SetActive(false);
		this.mGoGoto.SetActive(false);
		this.GuildWarOpen = false;
		int id = dailyData.id;
		if (id != 12030)
		{
			if (id != 12050)
			{
				this.CurrentLimit = this.GetActiveIdByDailyId(dailyData.id);
				HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(this.CurrentLimit);
				if (huoDongZhongXin != null)
				{
					this.mTxDesc.set_text(GameDataUtils.GetChineseContent(513518, false) + " " + ActivityCenterManager.Instance.GetFormatOpenTime(huoDongZhongXin, true, true, "\n"));
				}
				if (!this.mTxLvTips.get_transform().get_parent().get_gameObject().get_activeSelf())
				{
					ActiveCenterInfo activeCenterInfo = null;
					if (this.CurrentLimit > 0 && ActivityCenterManager.infoDict.TryGetValue(this.CurrentLimit, ref activeCenterInfo) && activeCenterInfo.status == ActiveCenterInfo.ActiveStatus.AS.Start)
					{
						this.ShowGotoButton();
					}
				}
			}
			else if (!GuildManager.Instance.IsJoinInGuild())
			{
				this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(true);
				this.mTxLvTips.set_text(GameDataUtils.GetChineseContent(301049, false));
			}
			else if (!this.mTxLvTips.get_transform().get_parent().get_gameObject().get_activeSelf())
			{
				string[] guildWarOpenTime;
				if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH2_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_END)
				{
					guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(4);
				}
				else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH1_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.HALF_MATCH2_END)
				{
					guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(3);
				}
				else
				{
					guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(2);
				}
				this.mTxDesc.set_text(string.Concat(new string[]
				{
					GameDataUtils.GetChineseContent(513518, false),
					" ",
					GameDataUtils.GetChineseContent(513518 + int.Parse(guildWarOpenTime[0]), false),
					"\n",
					guildWarOpenTime[1],
					"-",
					guildWarOpenTime[3]
				}));
				if (GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH2_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.FINAL_MATCH_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH1_BEG)
				{
					this.GuildWarOpen = true;
					this.ShowGotoButton();
				}
			}
		}
		else if (!GuildManager.Instance.IsJoinInGuild())
		{
			this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(true);
			this.mTxLvTips.set_text(GameDataUtils.GetChineseContent(301049, false));
		}
		else if (!this.mTxLvTips.get_transform().get_parent().get_gameObject().get_activeSelf())
		{
			string[] array = DataReader<GongHuiXinXi>.Get("ExpTime").value.Split(new char[]
			{
				','
			});
			if (array != null && array.Length == 2)
			{
				this.mTxDesc.set_text(string.Concat(new string[]
				{
					GameDataUtils.GetChineseContent(513518, false),
					" 每天\n",
					array[0],
					"-",
					array[1]
				}));
				if (GuildManager.Instance.IsGuildFieldOpen)
				{
					this.ShowGotoButton();
				}
			}
		}
	}

	private void ShowGotoButton()
	{
		this.mGoGoto.SetActive(true);
		this.mFxId = FXSpineManager.Instance.ReplaySpine(this.mFxId, 603, this.mSpine, "DailyTaskUI", 2001, null, "UI", 0f, 0f, 1.7f, 1.2f, true, FXMaskLayer.MaskState.None);
	}

	private void SetTypeVisable(bool isDaily, bool isComplete)
	{
		this.CanShowBuy = (isDaily && this.DailyData.buyTime == 1 && isComplete);
		bool flag = true;
		if (this.CanShowBuy)
		{
			this.CanShowBuy = DailyTaskManager.Instance.IsCanBuy(this.DailyData.system);
			flag = DailyTaskManager.Instance.IsFinish(this.DailyData.system);
		}
		if (isDaily)
		{
			this.mTxButton.set_text(GameDataUtils.GetChineseContent(301028, false));
			this.mBtnGoto.get_gameObject().SetActive(true);
			this.mGoFinished.SetActive(false);
			if (isComplete)
			{
				if (!flag && this.CanShowBuy)
				{
					this.mTxButton.set_text(GameDataUtils.GetChineseContent(301027, false));
					this.mBtnGoto.get_gameObject().SetActive(true);
					this.mGoFinished.SetActive(false);
				}
				else if ((flag && this.CanShowBuy) || this.DailyData.buyTime == 2)
				{
					this.mBtnGoto.get_gameObject().SetActive(false);
					this.mGoFinished.SetActive(true);
				}
			}
		}
		else
		{
			this.mTxButton.set_text(GameDataUtils.GetChineseContent(301029, false));
			this.mBtnGoto.get_gameObject().SetActive(!isComplete);
			this.mGoFinished.SetActive(false);
		}
		if (this.CanShowBuy)
		{
			ResourceManager.SetSprite(this.mBtnGoto.GetComponent<Image>(), ResourceManager.GetIconSprite("qw_button_1"));
		}
		else
		{
			ResourceManager.SetSprite(this.mBtnGoto.GetComponent<Image>(), ResourceManager.GetIconSprite("qw_button"));
		}
		this.mTxLiveness.get_gameObject().SetActive(isDaily);
		this.mTxPrice.get_transform().get_parent().get_gameObject().SetActive(!isDaily);
		this.mTxPrice.get_gameObject().SetActive(true);
		this.mRewards.SetActive(!isDaily);
		this.mGoFinded.SetActive(isComplete && !isDaily);
		this.mTxLvTips.get_transform().get_parent().get_gameObject().SetActive(false);
		this.mGoGoto.SetActive(true);
	}

	private int GetActiveIdByDailyId(int dailyId)
	{
		if (dailyId == 10800)
		{
			return 10002;
		}
		if (dailyId == 10910)
		{
			return 10001;
		}
		if (dailyId == 12010)
		{
			return 10003;
		}
		if (dailyId != 12060)
		{
			return 0;
		}
		return 10006;
	}

	private void CreateReward(DiaoLuo data)
	{
		this.mCurRewardCount++;
		if (this.mCurRewardCount > 2)
		{
			return;
		}
		Items item = BackpackManager.Instance.GetItem((data.goodsId != 3001) ? data.goodsId : 2010);
		if (item == null)
		{
			return;
		}
		GameObject gameObject = this.CreateRewardItem();
		gameObject.set_name(data.goodsId.ToString());
		Image image = UIHelper.GetImage(gameObject.get_transform(), "Image");
		if (this.HaveIconId(data.goodsId))
		{
			image.get_rectTransform().set_sizeDelta(new Vector2(50f, 50f));
		}
		else
		{
			image.get_rectTransform().set_sizeDelta(new Vector2(40f, 40f));
		}
		ResourceManager.SetSprite(image, GameDataUtils.GetIcon((item.id >= 5) ? item.icon : item.littleIcon));
		string text = Utils.SwitchChineseNumber((long)this.mTask.canFindTimes * data.minNum, 1);
		if (item.id == 1)
		{
			text += "+";
		}
		UIHelper.GetText(gameObject.get_transform(), "Text").set_text(text);
		gameObject.SetActive(true);
	}

	private GameObject CreateRewardItem()
	{
		GameObject gameObject = this.mRewardItem.Find((GameObject e) => e.get_gameObject().get_name() == "Unused");
		if (gameObject != null)
		{
			return gameObject;
		}
		gameObject = Object.Instantiate<GameObject>(this.mRewardPrefab);
		UGUITools.SetParent(this.mRewards, gameObject, false);
		this.mRewardItem.Add(gameObject);
		return gameObject;
	}

	private bool HaveIconId(int id)
	{
		for (int i = 0; i < this.ICON_ID.Length; i++)
		{
			if (this.ICON_ID[i] == id)
			{
				return true;
			}
		}
		return false;
	}

	private void ClearRewardItem()
	{
		for (int i = 0; i < this.mRewardItem.get_Count(); i++)
		{
			this.mRewardItem.get_Item(i).set_name("Unused");
			this.mRewardItem.get_Item(i).SetActive(false);
		}
		this.mCurRewardCount = 0;
	}

	public void SetData(DailyTask task, DailyTaskType type, bool isGoldBuy, Action<DailyTaskItem> clickCallBack = null)
	{
		this.mTask = task;
		this.mType = type;
		this.mEventHandler = clickCallBack;
		this.mDailyData = DataReader<MeiRiMuBiao>.Get(this.mTask.taskId);
		if (this.mDailyData != null)
		{
			if (this.mFxId > 0)
			{
				FXSpineManager.Instance.DeleteSpine(this.mFxId, true);
				this.mFxId = 0;
			}
			if (this.mDailyData.Retrieve == 1)
			{
				this.mFindData = DataReader<MZhaoHui>.Get(this.mTask.taskId);
			}
			ResourceManager.SetSprite(this.mIcon, GameDataUtils.GetIcon(this.mDailyData.iconId));
			this.mTxName.set_text(GameDataUtils.GetChineseContent(this.mDailyData.introduction1, false));
			switch (this.mType)
			{
			case DailyTaskType.DAILY:
				this.SetDailyData(this.mDailyData);
				break;
			case DailyTaskType.FIND:
				this.SetFindData(this.mFindData, isGoldBuy);
				break;
			case DailyTaskType.LIMIT:
				this.SetTimeData(this.mDailyData);
				break;
			}
		}
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
