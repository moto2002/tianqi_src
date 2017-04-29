using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : UIBase
{
	private ListPool rankListPool;

	private List<ButtonCustom> btns = new List<ButtonCustom>();

	public RankingType.ENUM _current;

	private RankingType.ENUM lastCurrent;

	private int timeDown = -1;

	private Color32 COLOR_LIGHT = new Color32(255, 250, 230, 255);

	private Color32 COLOR_LIGHT_NO = new Color32(255, 215, 140, 255);

	public RankingType.ENUM Current
	{
		get
		{
			return this._current;
		}
		set
		{
			this._current = value;
			RankingManager.Instance.current = this._current;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.InitData();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.rankListPool = base.FindTransform("Grid").GetComponent<ListPool>();
	}

	private void Start()
	{
		WaitUI.OpenUI(3000u);
		this.UpdateType();
		RankingManager.Instance.SendGetRankInfoReq();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110005), string.Empty, new Action(this.OnClickExit), false);
		base.StopCoroutine("TimeDown");
		base.StartCoroutine(this.TimeDown());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		RankingManager.Instance.overTime = DateTime.get_UtcNow().AddSeconds((double)this.timeDown);
		this.timeDown = -1;
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			this.Current = RankingType.ENUM.Lv;
			this.SetBtnCurrent(this.Current);
			this.lastCurrent = (RankingType.ENUM)0;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnRankingPush, new Callback(this.OnRankingPush));
		EventDispatcher.AddListener(EventNames.OnRankingPersonalPush, new Callback(this.OnRankingPersonalPush));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnRankingPush, new Callback(this.OnRankingPush));
		EventDispatcher.RemoveListener(EventNames.OnRankingPersonalPush, new Callback(this.OnRankingPersonalPush));
	}

	private void OnClickExit()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("RankingUI");
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnRankingPush()
	{
		this.UpdateContentList();
		this.UpdatePersonalData();
	}

	private void OnRankingPersonalPush()
	{
		this.UpdatePersonalData();
	}

	private void InitData()
	{
		for (int i = 0; i < base.FindTransform("Buttons").get_childCount(); i++)
		{
			ButtonCustom component = base.FindTransform("Buttons").GetChild(i).GetComponent<ButtonCustom>();
			component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRankingType);
			this.btns.Add(component);
		}
		this.Current = RankingType.ENUM.Lv;
		this.lastCurrent = (RankingType.ENUM)0;
	}

	private void UpdateType()
	{
		this.SetBtnCurrent(this.Current);
		RankingManager.Instance.SendRankInfoReq(this.Current);
		this.lastCurrent = this.Current;
	}

	private void SetBtnCurrent(RankingType.ENUM btnType)
	{
		this.SetBtnLightAndDim(this.lastCurrent, "fenleianniu_2", false);
		this.SetBtnLightAndDim(btnType, "fenleianniu_1", true);
	}

	private void SetBtnLightAndDim(RankingType.ENUM btnType, string btnIcon, bool isLight)
	{
		int num = btnType - RankingType.ENUM.Lv;
		if (num >= 0 && num < this.btns.get_Count())
		{
			ButtonCustom buttonCustom = this.btns.get_Item(num);
			if (buttonCustom != null)
			{
				ResourceManager.SetSprite(buttonCustom.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
				buttonCustom.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? this.COLOR_LIGHT_NO : this.COLOR_LIGHT);
			}
		}
	}

	private void OnClickRankingType(GameObject go)
	{
		int num = this.btns.FindIndex((ButtonCustom e) => e.get_gameObject() == go);
		if (num >= 0 && this.btns.get_Count() > num)
		{
			this.Current = num + RankingType.ENUM.Lv;
			if (this.lastCurrent == this.Current)
			{
				return;
			}
			this.UpdateType();
		}
	}

	private void UpdatePersonalData()
	{
		if (RankingManager.Instance.Personal == null)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		switch (this.Current)
		{
		case RankingType.ENUM.Lv:
			if (RankingManager.Instance.Personal.lvInfo.get_Count() > 0)
			{
				OneLvRankInfo oneLvRankInfo = RankingManager.Instance.Personal.lvInfo.get_Item(0);
				text2 = "主角等级:";
				text = oneLvRankInfo.rank.ToString();
				text3 = oneLvRankInfo.lv.ToString();
			}
			break;
		case RankingType.ENUM.Fighting:
			if (RankingManager.Instance.Personal.fightingInfo.get_Count() > 0)
			{
				OneFightingRankInfo oneFightingRankInfo = RankingManager.Instance.Personal.fightingInfo.get_Item(0);
				text2 = "综合战斗力:";
				text = oneFightingRankInfo.rank.ToString();
				text3 = oneFightingRankInfo.fighting.ToString();
			}
			break;
		case RankingType.ENUM.PetFighting:
			if (RankingManager.Instance.Personal.petFInfo.get_Count() > 0)
			{
				OnePetFRankInfo onePetFRankInfo = RankingManager.Instance.Personal.petFInfo.get_Item(0);
				text2 = "宠物战斗力:";
				text = onePetFRankInfo.rank.ToString();
				text3 = onePetFRankInfo.petF.ToString();
			}
			break;
		}
		if (string.IsNullOrEmpty(text))
		{
			this.SwicthTips(true);
			base.FindTransform("Descvalue").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(621101, false));
		}
		else
		{
			this.SwicthTips(false);
			base.FindTransform("Allname").GetComponent<Text>().set_text(text2);
			base.FindTransform("Rankvalue").GetComponent<Text>().set_text(text);
			base.FindTransform("Allvalue").GetComponent<Text>().set_text(text3);
		}
	}

	private void SwicthTips(bool isShowTips)
	{
		base.FindTransform("DownBG").get_gameObject().SetActive(!isShowTips);
		base.FindTransform("Descvalue").get_gameObject().SetActive(isShowTips);
	}

	[DebuggerHidden]
	private IEnumerator TimeDown()
	{
		RankingUI.<TimeDown>c__Iterator48 <TimeDown>c__Iterator = new RankingUI.<TimeDown>c__Iterator48();
		<TimeDown>c__Iterator.<>f__this = this;
		return <TimeDown>c__Iterator;
	}

	private void ShowTime()
	{
		if (RankingManager.Instance.RemainingTime != null)
		{
			base.FindTransform("time").GetComponent<Text>().set_text(string.Format("刷新时间: <color=#50ff14>{0}</color>", RankingManager.Instance.RemainingTime.GetTime()));
		}
	}

	private void UpdateContentList()
	{
		base.FindTransform("Content").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
		switch (this.Current)
		{
		case RankingType.ENUM.Lv:
			this.UpdateLvRank();
			break;
		case RankingType.ENUM.Fighting:
			this.UpdateFightingRank();
			break;
		case RankingType.ENUM.PetFighting:
			this.UpdatePetRank();
			break;
		}
	}

	private void UpdateLvRank()
	{
		if (this.rankListPool != null)
		{
			this.rankListPool.Clear();
		}
		if (RankingManager.Instance.LvRank != null && RankingManager.Instance.LvRank.items != null)
		{
			List<LvRankInfo> rankData = RankingManager.Instance.LvRank.items;
			if (rankData != null)
			{
				this.rankListPool.Create(rankData.get_Count(), delegate(int index)
				{
					if (index < rankData.get_Count() && index < this.rankListPool.Items.get_Count())
					{
						RankDataUnite data = new RankDataUnite
						{
							rankingType = RankingType.ENUM.Lv,
							roleId = rankData.get_Item(index).roleId,
							ranking = rankData.get_Item(index).rank,
							roleName = rankData.get_Item(index).name,
							num = (long)rankData.get_Item(index).lv,
							career = (PlayerCareerType)rankData.get_Item(index).career
						};
						this.rankListPool.Items.get_Item(index).GetComponent<RankingItem>().UpdateItem(data);
					}
				});
			}
		}
	}

	private void UpdateFightingRank()
	{
		if (this.rankListPool != null)
		{
			this.rankListPool.Clear();
		}
		if (RankingManager.Instance.FightingRank == null || RankingManager.Instance.FightingRank.items == null)
		{
			return;
		}
		List<FightingRankInfo> rankData = RankingManager.Instance.FightingRank.items;
		if (rankData != null)
		{
			this.rankListPool.Create(rankData.get_Count(), delegate(int index)
			{
				if (index < rankData.get_Count() && index < this.rankListPool.Items.get_Count())
				{
					RankDataUnite data = new RankDataUnite
					{
						rankingType = RankingType.ENUM.Fighting,
						roleId = rankData.get_Item(index).roleId,
						ranking = rankData.get_Item(index).rank,
						roleName = rankData.get_Item(index).name,
						num = rankData.get_Item(index).fighting,
						career = (PlayerCareerType)rankData.get_Item(index).career
					};
					this.rankListPool.Items.get_Item(index).GetComponent<RankingItem>().UpdateItem(data);
				}
			});
		}
	}

	private void UpdatePetRank()
	{
		if (this.rankListPool != null)
		{
			this.rankListPool.Clear();
		}
		if (RankingManager.Instance.PetFRank == null || RankingManager.Instance.PetFRank.items == null)
		{
			return;
		}
		List<PetFRankInfo> rankData = RankingManager.Instance.PetFRank.items;
		if (rankData != null)
		{
			this.rankListPool.Create(rankData.get_Count(), delegate(int index)
			{
				if (index < rankData.get_Count() && index < this.rankListPool.Items.get_Count())
				{
					RankDataUnite data = new RankDataUnite
					{
						rankingType = RankingType.ENUM.PetFighting,
						roleId = rankData.get_Item(index).roleId,
						ranking = rankData.get_Item(index).rank,
						roleName = rankData.get_Item(index).name,
						num = rankData.get_Item(index).fighting,
						petId = rankData.get_Item(index).petCfgId,
						petColor = rankData.get_Item(index).quality,
						petStar = rankData.get_Item(index).star
					};
					this.rankListPool.Items.get_Item(index).GetComponent<RankingItem>().UpdateItem(data);
				}
			});
		}
	}
}
