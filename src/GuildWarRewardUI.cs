using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class GuildWarRewardUI : UIBase
{
	private Text lastWinnerGuildName;

	private Text titleAttrName;

	private Text titleRemainTime;

	private Text suitNameText;

	private Text wingNameText;

	private Text winnerNameText;

	private Text winnerSteakTimesText;

	private Text headTitleText;

	private Transform rewardItemsListTrans;

	private Image titleImg;

	private RawImage ImageActor;

	private GameObject ImageTouchPlace;

	private ButtonCustom btnQualification;

	private int fashionItemID;

	private int wingItemID;

	private int equipItemID;

	protected ExteriorArithmeticUnit exteriorUnit;

	private ActorModel roleModel;

	private TimeCountDown titleRemianTimeCd;

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(null, null, null, null);
			}
			return this.exteriorUnit;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.lastWinnerGuildName = base.FindTransform("GuildNameText").GetComponent<Text>();
		this.titleAttrName = base.FindTransform("RewardTitleAttrText").GetComponent<Text>();
		this.titleRemainTime = base.FindTransform("RewardTitleRemainTimeText").GetComponent<Text>();
		this.suitNameText = base.FindTransform("WinnerSuitNameText").GetComponent<Text>();
		this.winnerNameText = base.FindTransform("WinnerNameText").GetComponent<Text>();
		this.winnerSteakTimesText = base.FindTransform("WinnerSteakText").GetComponent<Text>();
		this.headTitleText = base.FindTransform("Header").GetComponent<Text>();
		this.wingNameText = base.FindTransform("WinnerWingNameText").GetComponent<Text>();
		this.titleImg = base.FindTransform("RewardTitleImg").GetComponent<Image>();
		this.ImageActor = base.FindTransform("RawImageActor").GetComponent<RawImage>();
		this.ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		this.rewardItemsListTrans = base.FindTransform("RewardItemsList");
		this.btnQualification = base.FindTransform("BtnQualification").GetComponent<ButtonCustom>();
		this.btnQualification.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnQualification);
		base.FindTransform("BtnGetReward").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGetReward);
		base.FindTransform("WinningStreakBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWinningStreak);
		base.FindTransform("GuildNameBgText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515098, false));
		base.FindTransform("GuildRewardTimeBgText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515099, false));
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_1D4 = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_1D4.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_1D4.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.ImageActor.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
		this.ResetTextUI();
		this.RefreshUIByCfg();
		this.headTitleText.set_text("王者军团");
		this.wingNameText.set_text(string.Empty);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.UpdateBtnState();
		this.OnGetGuildWarChampionReq();
		this.RefreshUI();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		this.RemoveTitleRemainTimeCD();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetGuildWarChampionRes, new Callback(this.OnGetGuildWarChampionRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetGuildWarChampionRes, new Callback(this.OnGetGuildWarChampionRes));
	}

	private void OnClickBtnQualification(GameObject go)
	{
		GuildWarVSInfoUI guildWarVSInfoUI = UIManagerControl.Instance.OpenUI("GuildWarVSInfoUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarVSInfoUI;
		guildWarVSInfoUI.get_transform().SetAsLastSibling();
		this.Show(false);
	}

	private void OnClickBtnGetReward(GameObject go)
	{
		if (!GuildManager.Instance.IsJoinInGuild())
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(34157, false));
			return;
		}
		if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.ELIGIBILITY && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_END)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515100, false));
			return;
		}
		GuildWarManager.Instance.SendGetGuildWarChampionDailyPrizeReq();
	}

	private void OnClickWinningStreak(GameObject go)
	{
		GuildWinningSteakUI guildWinningSteakUI = UIManagerControl.Instance.OpenUI("GuildWinningSteakUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWinningSteakUI;
		guildWinningSteakUI.get_transform().SetAsLastSibling();
	}

	private void ResetTextUI()
	{
		this.titleAttrName.set_text(string.Empty);
		this.suitNameText.set_text(string.Empty);
		this.titleRemainTime.set_text(string.Empty);
		this.lastWinnerGuildName.set_text(string.Empty);
		this.winnerNameText.set_text(string.Empty);
		this.winnerSteakTimesText.set_text(string.Empty);
	}

	private void OnGetGuildWarChampionReq()
	{
		GuildWarManager.Instance.SendGetGuildWarChampionReq();
	}

	private void RefreshUIByCfg()
	{
		this.UpdateRewardItems();
		JunTuanZhanJiangLi junTuanZhanJiangLi = DataReader<JunTuanZhanJiangLi>.Get(1);
		if (junTuanZhanJiangLi != null)
		{
			int title = junTuanZhanJiangLi.Title;
			ChengHao chengHao = DataReader<ChengHao>.Get(title);
			if (chengHao != null && chengHao.displayWay == 2)
			{
				ResourceManager.SetSprite(this.titleImg, GameDataUtils.GetIcon(chengHao.icon));
				List<int> attrs = DataReader<Attrs>.Get(chengHao.gainProperty).attrs;
				List<int> values = DataReader<Attrs>.Get(chengHao.gainProperty).values;
				string text = string.Empty;
				int num = (attrs.get_Count() <= 2) ? attrs.get_Count() : 2;
				for (int i = 0; i < num; i++)
				{
					if (i >= values.get_Count())
					{
						break;
					}
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						AttrUtility.GetAttrName((GameData.AttrType)attrs.get_Item(i)),
						": <color=#47eb1b>+",
						AttrUtility.GetAttrValueDisplay(attrs.get_Item(i), values.get_Item(i)),
						"</color>"
					});
					if (i < num - 1)
					{
						text += "\n";
					}
				}
				this.titleAttrName.set_text(text);
			}
		}
	}

	private void RefreshUI()
	{
		int num = EntityWorld.Instance.EntSelf.TypeID;
		string text = string.Empty;
		string text2 = "虚位以待";
		int num2 = 0;
		int remainTimeUTC = 0;
		if (GuildWarManager.Instance.GuildWarChampionInfo != null)
		{
			num = GuildWarManager.Instance.GuildWarChampionInfo.career;
			text2 = GuildWarManager.Instance.GuildWarChampionInfo.captainName;
			text = GuildWarManager.Instance.GuildWarChampionInfo.guildName;
			num2 = GuildWarManager.Instance.GuildWarChampionInfo.winnerTimes;
			remainTimeUTC = GuildWarManager.Instance.GuildWarChampionInfo.titleCD;
		}
		num = ((num != 0) ? num : EntityWorld.Instance.EntSelf.TypeID);
		this.UpdateRewardWingText(num);
		this.UpdateRewardSuit(num);
		if (GuildWarManager.Instance.GuildWarChampionInfo == null || text == string.Empty || text2 == string.Empty)
		{
			this.lastWinnerGuildName.GetComponent<Gradient>().topColor = new Color32(238, 238, 238, 255);
			this.lastWinnerGuildName.GetComponent<Gradient>().bottomColor = new Color32(172, 172, 172, 255);
			this.winnerNameText.GetComponent<Gradient>().topColor = new Color32(238, 238, 238, 255);
			this.winnerNameText.GetComponent<Gradient>().bottomColor = new Color32(172, 172, 172, 255);
		}
		else
		{
			this.lastWinnerGuildName.GetComponent<Gradient>().topColor = new Color32(255, 173, 254, 255);
			this.lastWinnerGuildName.GetComponent<Gradient>().bottomColor = new Color32(196, 36, 255, 255);
			this.winnerNameText.GetComponent<Gradient>().topColor = new Color32(255, 236, 160, 255);
			this.winnerNameText.GetComponent<Gradient>().bottomColor = new Color32(255, 164, 31, 255);
		}
		this.lastWinnerGuildName.set_text((!(text != string.Empty)) ? "虚位以待" : text);
		this.winnerNameText.set_text((!(text2 != string.Empty)) ? (GameDataUtils.GetChineseContent(506022, false) + ": 虚位以待") : (GameDataUtils.GetChineseContent(506022, false) + ": " + text2));
		num2 = ((num2 < 2) ? 0 : num2);
		this.winnerSteakTimesText.set_text("连胜记录：" + num2 + "次");
		this.AddTitleRemainTimeCD(remainTimeUTC);
		this.RefreshRoleModel(num);
	}

	private void RefreshRoleModel(int type)
	{
		this.ExteriorUnit.WrapSetData(delegate
		{
			this.ExteriorUnit.FashionIDs = this.GetFashionList();
			this.ExteriorUnit.ServerModelID = ((type != 0) ? 0 : EntityWorld.Instance.EntSelf.ModelID);
			this.ExteriorUnit.SetType((type != 0) ? type : EntityWorld.Instance.EntSelf.TypeID);
			this.ExteriorUnit.Gogok = 3;
			this.ExteriorUnit.IsHideWing = false;
		});
		ModelDisplayManager.Instance.ShowModel(this.ExteriorUnit.FinalModelID, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uid)
		{
			this.roleModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (this.roleModel != null)
			{
				this.roleModel.EquipOn(this.ExteriorUnit.FinalWeaponID, this.ExteriorUnit.FinalWeaponGogok);
				this.roleModel.EquipOn(this.ExteriorUnit.FinalClothesID, 0);
				this.roleModel.EquipWingOn(this.ExteriorUnit.FinalWingID);
				this.roleModel.PreciseSetAction("idle_city");
				LayerSystem.SetGameObjectLayer(this.roleModel.get_gameObject(), "CameraRange", 2);
				this.roleModel.get_transform().set_localPosition(new Vector3(0f, 0.1f, -0.3f));
				this.roleModel.get_transform().set_localScale(0.95f * Vector3.get_one());
				this.roleModel.get_transform().set_localEulerAngles(Vector3.get_zero());
			}
		});
	}

	private List<string> GetFashionList()
	{
		List<string> list = new List<string>();
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.DataList.Find((ShiZhuangXiTong a) => a.itemsID == this.fashionItemID);
		if (shiZhuangXiTong != null)
		{
			list.Add(shiZhuangXiTong.ID);
		}
		ShiZhuangXiTong shiZhuangXiTong2 = DataReader<ShiZhuangXiTong>.DataList.Find((ShiZhuangXiTong a) => a.itemsID == this.wingItemID);
		if (shiZhuangXiTong2 != null)
		{
			list.Add(shiZhuangXiTong2.ID);
		}
		ShiZhuangXiTong shiZhuangXiTong3 = DataReader<ShiZhuangXiTong>.DataList.Find((ShiZhuangXiTong a) => a.itemsID == this.equipItemID);
		if (shiZhuangXiTong3 != null)
		{
			list.Add(shiZhuangXiTong3.ID);
		}
		return list;
	}

	private void UpdateRewardWingText(int type)
	{
		int rewardWingID = GuildWarManager.Instance.GetRewardWingID(type);
		Items items = DataReader<Items>.Get(rewardWingID);
		if (items != null)
		{
			this.wingNameText.set_text("团长幻翼：" + GameDataUtils.GetChineseContent(items.name, false));
		}
	}

	private void UpdateRewardSuit(int type)
	{
		JunTuanZhanJiangLi junTuanZhanJiangLi = DataReader<JunTuanZhanJiangLi>.Get(1);
		if (junTuanZhanJiangLi != null)
		{
			string clothes = junTuanZhanJiangLi.Clothes;
			string[] array = clothes.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				int num = int.Parse(array2[0]);
				if (num == type)
				{
					string text = array2[1];
					string[] array3 = text.Split(new char[]
					{
						','
					});
					this.fashionItemID = int.Parse(array3[0]);
					this.equipItemID = int.Parse(array3[2]);
					this.wingItemID = GuildWarManager.Instance.GetRewardWingID(type);
					Items items = DataReader<Items>.Get(this.fashionItemID);
					if (items != null)
					{
						this.suitNameText.set_text("军团时装：" + GameDataUtils.GetChineseContent(items.name, false));
					}
				}
			}
		}
	}

	private void UpdateRewardItems()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		JunTuanZhanJiangLi junTuanZhanJiangLi = DataReader<JunTuanZhanJiangLi>.Get(1);
		if (junTuanZhanJiangLi != null)
		{
			for (int i = 0; i < junTuanZhanJiangLi.reward.get_Count(); i++)
			{
				JunTuanZhanJiangLi.RewardPair rewardPair = junTuanZhanJiangLi.reward.get_Item(i);
				list.Add(rewardPair.key);
				list2.Add(rewardPair.value);
			}
			for (int j = 0; j < list.get_Count(); j++)
			{
				ItemShow.ShowItem(this.rewardItemsListTrans, list.get_Item(j), -1L, false, null, 2001);
			}
		}
	}

	private void UpdateBtnState()
	{
		if (GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.ELIGIBILITY)
		{
			this.btnQualification.get_transform().FindChild("Text").GetComponent<Text>().set_text("本周入围军团");
		}
		else
		{
			this.btnQualification.get_transform().FindChild("Text").GetComponent<Text>().set_text("参加军团战");
		}
	}

	private void AddTitleRemainTimeCD(int remainTimeUTC)
	{
		this.RemoveTitleRemainTimeCD();
		int num = 0;
		if (remainTimeUTC > 0)
		{
			num = remainTimeUTC - TimeManager.Instance.PreciseServerSecond;
		}
		this.titleRemainTime.set_text(string.Empty);
		if (num <= 0)
		{
			this.titleRemainTime.set_text(GameDataUtils.GetChineseContent(1005016, false));
		}
		if (num > 0)
		{
			this.titleRemianTimeCd = new TimeCountDown(num, TimeFormat.DHHMM_Chinese, delegate
			{
				if (this.titleRemianTimeCd != null && this.titleRemainTime != null)
				{
					this.titleRemainTime.set_text(this.titleRemianTimeCd.GetTime());
				}
			}, delegate
			{
				this.RemoveTitleRemainTimeCD();
				this.titleRemainTime.set_text(TimeConverter.GetTime(0, TimeFormat.DHHMM_Chinese));
			}, true);
		}
	}

	private void RemoveTitleRemainTimeCD()
	{
		if (this.titleRemianTimeCd != null)
		{
			this.titleRemianTimeCd.Dispose();
			this.titleRemianTimeCd = null;
		}
	}

	private void OnGetGuildWarChampionRes()
	{
		this.RefreshUI();
	}
}
