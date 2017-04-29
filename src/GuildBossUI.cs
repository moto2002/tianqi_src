using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class GuildBossUI : UIBase
{
	private ListPool killBossRewardListPool;

	private List<Transform> guildBossHurtRankingList;

	private ButtonCustom joinBtn;

	private Slider currentBloodSlider;

	private RawImage bossModelRawImg;

	private Text guildBossNameText;

	private Text remainTimeText;

	private Text coolDownText;

	private Text guildFundText;

	private Text joinBtnText;

	private Text currentBloodText;

	private Text tipDownText;

	private Text callBossTipText;

	private Image bossStepImgNum;

	private Transform guildCallBossTimeFullImgTrans;

	private Transform haveGuildBossTrans;

	private Transform guildBossUnknowImgTrans;

	private bool canKillBoss;

	private int modelUid;

	private TimeCountDown killBossCDTimeCD;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.joinBtn = base.FindTransform("JoinBtn").GetComponent<ButtonCustom>();
		this.joinBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickJoinBtn);
		this.bossModelRawImg = base.FindTransform("RawImageModel").GetComponent<RawImage>();
		this.remainTimeText = base.FindTransform("RemainTimeText").GetComponent<Text>();
		this.currentBloodSlider = base.FindTransform("CurrentBloodSlider").GetComponent<Slider>();
		this.guildBossNameText = base.FindTransform("BossNameText").GetComponent<Text>();
		this.guildFundText = base.FindTransform("CurrentCoinNum").GetComponent<Text>();
		this.coolDownText = base.FindTransform("CoolDownText").GetComponent<Text>();
		this.joinBtnText = base.FindTransform("JoinBtn").FindChild("Text").GetComponent<Text>();
		this.currentBloodText = base.FindTransform("CurrentBloodText").GetComponent<Text>();
		this.tipDownText = base.FindTransform("TipText").GetComponent<Text>();
		this.callBossTipText = base.FindTransform("CallBossTipText").GetComponent<Text>();
		this.bossStepImgNum = base.FindTransform("BossStepImgNum").GetComponent<Image>();
		this.haveGuildBossTrans = base.FindTransform("HaveBossRoot");
		this.guildCallBossTimeFullImgTrans = base.FindTransform("GuildCallBossTimeFullImg");
		this.guildBossUnknowImgTrans = base.FindTransform("BossUnknowImg");
		base.FindTransform("IntroductionBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickIntroductionBtn);
		this.killBossRewardListPool = base.FindTransform("KillBossRewardListPool").GetComponent<ListPool>();
		this.guildBossHurtRankingList = new List<Transform>();
		for (int i = 0; i < 4; i++)
		{
			Transform transform = base.FindTransform("Ranking" + i);
			this.guildBossHurtRankingList.Add(transform);
		}
		this.SetGuildBossReward();
		RTManager.Instance.SetModelRawImage1(this.bossModelRawImg, false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetGuildBossInfo, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.OnCallGuildBossRes, new Callback(this.OnCallGuildBossRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetGuildBossInfo, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.OnCallGuildBossRes, new Callback(this.OnCallGuildBossRes));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.killBossRewardListPool.Clear();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		this.OnSendGuildBossInfoReq();
		this.RefreshUI();
		this.RefreshGuildMoney();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		this.ClearKillBossCDTimeCD();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.DeleteBossModel();
			base.ReleaseSelf(true);
		}
	}

	private void OnClickIntroductionBtn(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 515062, 515061);
	}

	private void OnClickJoinBtn(GameObject go)
	{
		if (GuildBossManager.Instance.GuildBossActivityInfo != null && GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging && GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD <= 0)
		{
			GuildBossManager.Instance.SendChallengeGuildBossReq();
			return;
		}
		if (GuildBossManager.Instance.GuildBossActivityInfo != null && GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging && GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD > 0)
		{
			int cleanGuildBossCDCost = GuildBossManager.Instance.GetCleanGuildBossCDCost();
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), "清除CD需要消耗" + cleanGuildBossCDCost + "钻石", null, delegate
			{
				GuildBossManager.Instance.SendCleanGuildBossCDReq();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		if (GuildBossManager.Instance.GuildBossActivityInfo != null && !GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging && GuildBossManager.Instance.GuildBossActivityInfo.RemainCallBossTimes > 0)
		{
			UIManagerControl.Instance.OpenUI("GuildBossCallUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			return;
		}
	}

	private void OnShowRewards(GameObject go)
	{
		if (GuildBossManager.Instance.GuildBossActivityInfo == null || !GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging || GuildBossManager.Instance.GuildBossActivityInfo.GuildBossID == 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515085, false));
			return;
		}
		int guildBossID = GuildBossManager.Instance.GuildBossActivityInfo.GuildBossID;
		if (DataReader<JunTuanBOSSMoXing>.Get(guildBossID) == null)
		{
			return;
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		int num = this.guildBossHurtRankingList.FindIndex((Transform a) => a == go.get_transform().get_parent());
		if (num >= 0 && num <= 3)
		{
			List<KeyValuePair<int, int>> rewardListByRank = GuildBossManager.Instance.GetRewardListByRank(num);
			if (rewardListByRank != null && rewardListByRank.get_Count() > 0)
			{
				for (int i = 0; i < rewardListByRank.get_Count(); i++)
				{
					KeyValuePair<int, int> keyValuePair = rewardListByRank.get_Item(i);
					list.Add(keyValuePair.get_Key());
					list2.Add((long)keyValuePair.get_Value());
				}
				if (list.get_Count() > 0)
				{
					RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
					rewardUI.get_transform().SetAsLastSibling();
					rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513163, false), list, list2, true, true, null, null);
				}
			}
		}
	}

	private void SetGuildBossReward()
	{
		this.tipDownText.set_text(GameDataUtils.GetChineseContent(515083, false));
		List<int> list = new List<int>();
		string value = DataReader<GongHuiXinXi>.Get("rewardIcon").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				','
			});
			int num = (int)float.Parse(array2[1]);
			list.Add(num);
		}
		for (int j = 0; j < this.guildBossHurtRankingList.get_Count(); j++)
		{
			Transform transform = this.guildBossHurtRankingList.get_Item(j);
			Image component = transform.FindChild("RankingRewardItemImg").GetComponent<Image>();
			if (j >= list.get_Count())
			{
				break;
			}
			int key = list.get_Item(j);
			Icon icon = DataReader<Icon>.Get(key);
			if (icon != null)
			{
				ResourceManager.SetIconSprite(component, icon.icon);
			}
			component.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnShowRewards);
		}
	}

	private void OnSendGuildBossInfoReq()
	{
		GuildBossManager.Instance.SendGuildBossInfoReq();
	}

	private void RefreshUI()
	{
		if (GuildBossManager.Instance.GuildBossActivityInfo == null)
		{
			return;
		}
		this.RefreshHurtRanking();
		this.ClearKillBossCDTimeCD();
		this.remainTimeText.set_text(string.Concat(new object[]
		{
			"每周一5:00，BOSS将会离开\n还可召唤: <color=#50ff14>",
			GuildBossManager.Instance.GuildBossActivityInfo.RemainCallBossTimes,
			"/",
			GuildBossManager.Instance.GuildCallBossTotalTimesPerWeek,
			"</color>"
		}));
		this.callBossTipText.set_text(string.Empty);
		if (GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging)
		{
			if (this.guildBossUnknowImgTrans.get_gameObject().get_activeSelf())
			{
				this.guildBossUnknowImgTrans.get_gameObject().SetActive(false);
			}
			if (!this.haveGuildBossTrans.get_gameObject().get_activeSelf())
			{
				this.haveGuildBossTrans.get_gameObject().SetActive(true);
			}
			if (this.guildCallBossTimeFullImgTrans.get_gameObject().get_activeSelf())
			{
				this.guildCallBossTimeFullImgTrans.get_gameObject().SetActive(false);
			}
			base.FindTransform("BossModelPanel").get_gameObject().SetActive(true);
			this.SetGuildBossInfo();
			if (!GuildBossManager.Instance.CheckCanCleanCDTime() && GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD > 0)
			{
				if (this.joinBtn.get_gameObject().get_activeSelf())
				{
					this.joinBtn.get_gameObject().SetActive(false);
				}
			}
			else if (!this.joinBtn.get_gameObject().get_activeSelf())
			{
				this.joinBtn.get_gameObject().SetActive(true);
			}
			this.joinBtnText.set_text((GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD <= 0) ? "前往参加" : "钻石加速");
			this.SetKillBossCDTime(GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD);
		}
		else
		{
			this.DeleteBossModel();
			base.FindTransform("BossModelPanel").get_gameObject().SetActive(false);
			if (!this.guildBossUnknowImgTrans.get_gameObject().get_activeSelf())
			{
				this.guildBossUnknowImgTrans.get_gameObject().SetActive(true);
			}
			if (this.haveGuildBossTrans.get_gameObject().get_activeSelf())
			{
				this.haveGuildBossTrans.get_gameObject().SetActive(false);
			}
			if (GuildBossManager.Instance.GuildBossActivityInfo.RemainCallBossTimes <= 0)
			{
				if (!this.guildCallBossTimeFullImgTrans.get_gameObject().get_activeSelf())
				{
					this.guildCallBossTimeFullImgTrans.get_gameObject().SetActive(true);
				}
				if (this.joinBtn.get_gameObject().get_activeSelf())
				{
					this.joinBtn.get_gameObject().SetActive(false);
				}
			}
			else
			{
				if (!this.joinBtn.get_gameObject().get_activeSelf())
				{
					this.joinBtn.get_gameObject().SetActive(true);
				}
				this.joinBtnText.set_text("召唤Boss");
				this.callBossTipText.set_text(GameDataUtils.GetChineseContent(515087, false));
			}
		}
		this.ShowBossTest();
	}

	private void SetGuildBossInfo()
	{
		if (GuildBossManager.Instance.GuildBossActivityInfo == null)
		{
			return;
		}
		float num = (float)GuildBossManager.Instance.GuildBossActivityInfo.GuildBossCurrentBlood / ((float)GuildBossManager.Instance.GuildBossActivityInfo.GuildBossTotalBlood * 1f);
		num = ((num < 1f) ? num : 1f);
		this.currentBloodSlider.set_value(num);
		this.currentBloodText.set_text(GuildBossManager.Instance.GuildBossActivityInfo.GuildBossCurrentBlood + "/" + GuildBossManager.Instance.GuildBossActivityInfo.GuildBossTotalBlood);
		Monster monster = DataReader<Monster>.Get(GuildBossManager.Instance.GuildBossActivityInfo.GuildBossID);
		if (monster != null)
		{
			this.RefreshBossModel(monster.model, (int)monster.id);
			this.guildBossNameText.set_text(GameDataUtils.GetChineseContent(monster.name, false));
		}
		JunTuanBOSSMoXing junTuanBOSSMoXing = DataReader<JunTuanBOSSMoXing>.Get(GuildBossManager.Instance.GuildBossActivityInfo.GuildBossID);
		if (junTuanBOSSMoXing != null)
		{
			ResourceManager.SetIconSprite(this.bossStepImgNum, "shuzi_jie_" + junTuanBOSSMoXing.rank);
			this.SetGuildBossReward(junTuanBOSSMoXing.reward);
		}
	}

	private void SetGuildBossReward(List<int> itemList)
	{
		this.killBossRewardListPool.Clear();
		if (itemList != null)
		{
			this.killBossRewardListPool.Create(itemList.get_Count(), delegate(int index)
			{
				if (index < itemList.get_Count() && index < this.killBossRewardListPool.Items.get_Count())
				{
					RewardItem component = this.killBossRewardListPool.Items.get_Item(index).GetComponent<RewardItem>();
					if (component != null)
					{
						component.SetRewardItem(itemList.get_Item(index), -1L, 0L);
					}
				}
			});
		}
	}

	private void RefreshGuildMoney()
	{
		this.guildFundText.set_text(MoneyType.GetNum(6).ToString());
	}

	private void RefreshHurtRanking()
	{
		for (int i = 0; i < this.guildBossHurtRankingList.get_Count(); i++)
		{
			Transform transform = this.guildBossHurtRankingList.get_Item(i);
			Image component = transform.FindChild("ImageFrame").FindChild("ImageIcon").GetComponent<Image>();
			if (i == 0)
			{
				if (GuildBossManager.Instance.GuildBossActivityInfo.FinalHurtInfo != null)
				{
					transform.FindChild("ImageFrame").get_gameObject().SetActive(true);
					ResourceManager.SetSprite(component, UIUtils.GetRoleSmallIcon(GuildBossManager.Instance.GuildBossActivityInfo.FinalHurtInfo.RoleCarrer));
					transform.FindChild("Name").GetComponent<Text>().set_text(GuildBossManager.Instance.GuildBossActivityInfo.FinalHurtInfo.RoleName);
				}
				else
				{
					transform.FindChild("ImageFrame").get_gameObject().SetActive(false);
					transform.FindChild("Name").GetComponent<Text>().set_text("暂无");
				}
			}
			else if (GuildBossManager.Instance.GuildBossActivityInfo.HurtRankingList != null && GuildBossManager.Instance.GuildBossActivityInfo.HurtRankingList.get_Count() > i - 1)
			{
				int num = i - 1;
				ResourceManager.SetSprite(component, UIUtils.GetRoleSmallIcon(GuildBossManager.Instance.GuildBossActivityInfo.HurtRankingList.get_Item(num).RoleCarrer));
				transform.FindChild("ImageFrame").get_gameObject().SetActive(true);
				transform.FindChild("Name").GetComponent<Text>().set_text(GuildBossManager.Instance.GuildBossActivityInfo.HurtRankingList.get_Item(num).RoleName);
				transform.FindChild("DamageFighting").GetComponent<Text>().set_text(GuildBossManager.Instance.GuildBossActivityInfo.HurtRankingList.get_Item(num).HurtValue + string.Empty);
			}
			else
			{
				transform.FindChild("ImageFrame").get_gameObject().SetActive(false);
				transform.FindChild("Name").GetComponent<Text>().set_text("暂无");
				transform.FindChild("DamageFighting").GetComponent<Text>().set_text(string.Empty + 0);
			}
		}
	}

	private void OnCallGuildBossRes()
	{
		this.RefreshUI();
		this.RefreshGuildMoney();
	}

	private void ShowBossTest()
	{
		if (GuildBossManager.Instance.ShowGuildBossTest && GuildBossManager.Instance.GuildBossID > 0)
		{
			base.FindTransform("BossModelPanel").get_gameObject().SetActive(true);
			Monster monster = DataReader<Monster>.Get(GuildBossManager.Instance.GuildBossID);
			if (monster != null)
			{
				this.RefreshBossModel(monster.model, GuildBossManager.Instance.GuildBossID);
				this.guildBossNameText.set_text(GameDataUtils.GetChineseContent(monster.name, false));
				if (this.guildBossUnknowImgTrans.get_gameObject().get_activeSelf())
				{
					this.guildBossUnknowImgTrans.get_gameObject().SetActive(false);
				}
			}
		}
		else if (GuildBossManager.Instance.GuildBossActivityInfo != null && !GuildBossManager.Instance.GuildBossActivityInfo.IsChallenging)
		{
			base.FindTransform("BossModelPanel").get_gameObject().SetActive(false);
			this.DeleteBossModel();
			if (!this.guildBossUnknowImgTrans.get_gameObject().get_activeSelf())
			{
				this.guildBossUnknowImgTrans.get_gameObject().SetActive(true);
			}
		}
	}

	private void RefreshBossModel(int modelID, int guildboosID)
	{
		ModelDisplayManager.Instance.ShowModel(modelID, true, ModelDisplayManager.OFFSET_TO_BOSSUI, delegate(int uid)
		{
			this.modelUid = uid;
			ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (uIModel != null)
			{
				Vector3 zero = Vector3.get_zero();
				Vector3 localScale = Vector3.get_one();
				Vector3 zero2 = Vector3.get_zero();
				JunTuanBOSSMoXing junTuanBOSSMoXing = DataReader<JunTuanBOSSMoXing>.Get(guildboosID);
				if (junTuanBOSSMoXing != null)
				{
					for (int i = 0; i < junTuanBOSSMoXing.modelOffset.get_Count(); i++)
					{
						if (i == 0)
						{
							zero.x = junTuanBOSSMoXing.modelOffset.get_Item(0);
						}
						if (i == 1)
						{
							zero.y = junTuanBOSSMoXing.modelOffset.get_Item(1);
						}
						if (i == 2)
						{
							zero.z = junTuanBOSSMoXing.modelOffset.get_Item(2);
						}
					}
					localScale = Vector3.get_one() * junTuanBOSSMoXing.modelZoom;
					zero2.y = junTuanBOSSMoXing.modelAngle;
				}
				uIModel.get_transform().set_localPosition(zero);
				uIModel.get_transform().set_localScale(localScale);
				uIModel.get_transform().set_localEulerAngles(zero2);
				LayerSystem.SetGameObjectLayer(uIModel.get_gameObject(), "CameraRange", 2);
			}
		});
	}

	private void DeleteBossModel()
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

	private void SetKillBossCDTime(int cdTime)
	{
		this.ClearKillBossCDTimeCD();
		if (cdTime <= 0)
		{
			return;
		}
		this.killBossCDTimeCD = new TimeCountDown(cdTime, TimeFormat.HHMMSS, delegate
		{
			if (this.killBossCDTimeCD != null)
			{
				if (GuildBossManager.Instance.GuildBossActivityInfo != null)
				{
					GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD = this.killBossCDTimeCD.GetSeconds();
				}
				this.coolDownText.set_text("距离下次击杀BOSS还剩：" + this.killBossCDTimeCD.GetTime());
			}
		}, delegate
		{
			if (GuildBossManager.Instance.GuildBossActivityInfo != null)
			{
				GuildBossManager.Instance.GuildBossActivityInfo.CanKillBossCD = 0;
			}
			this.coolDownText.set_text(string.Empty);
			this.RefreshUI();
			this.ClearKillBossCDTimeCD();
		}, true);
	}

	private void ClearKillBossCDTimeCD()
	{
		this.coolDownText.set_text(string.Empty);
		if (this.killBossCDTimeCD != null)
		{
			this.killBossCDTimeCD.Dispose();
			this.killBossCDTimeCD = null;
		}
	}
}
