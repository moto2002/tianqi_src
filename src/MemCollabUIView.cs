using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MemCollabUIView : UIBase
{
	private const int NUMBER_BACK = 2;

	public static MemCollabUIView Instance;

	private ListPool mCollabPool;

	private ListPool mRankPool;

	private GameObject m_goRegionNoBegin;

	private GameObject m_goRegionBegin;

	private Text m_lblGameTime;

	private Image m_spGameScoreImage;

	private Text m_lblRewardTimes;

	private GameObject m_goRewardTimesAdd;

	private List<int> m_openIndexs = new List<int>();

	private uint TimerID;

	private bool IsMemcollabClickOn = true;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		MemCollabUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mCollabPool = base.FindTransform("CollabItems").GetComponent<ListPool>();
		this.mCollabPool.SetItem("MemCollabItem");
		this.mCollabPool.LoadNumberFrame = 6;
		this.mRankPool = base.FindTransform("RankItems").GetComponent<ListPool>();
		this.mRankPool.SetItem("MemCollabRankItem");
		this.m_goRegionNoBegin = base.FindTransform("RegionNoBegin").get_gameObject();
		this.m_goRegionBegin = base.FindTransform("RegionBegin").get_gameObject();
		this.m_lblGameTime = base.FindTransform("GameTime").GetComponent<Text>();
		this.m_spGameScoreImage = base.FindTransform("GameScoreImage").GetComponent<Image>();
		this.m_lblRewardTimes = base.FindTransform("RewardTimes").GetComponent<Text>();
		this.m_goRewardTimesAdd = base.FindTransform("RewardTimesAdd").get_gameObject();
		base.FindTransform("ButtonBegin").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonBeginClick));
		base.FindTransform("RewardTimesAdd").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonAddClick));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110041), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
		this.ShowRewardTimesAdd(MemCollabManager.Instance.IsTodayRestBuyTimesOn());
		SoundManager.Instance.PlayBGMByID(124);
		this.mRankPool.Clear();
		MemCollabManager.Instance.SendMemoryFlopOpenUI();
		this.IsMemcollabClickOn = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		TimerHeap.DelTimer(this.TimerID);
		MySceneManager.Instance.PlayBGM();
	}

	protected override void ReleaseSelf(bool callDestroy)
	{
		MemCollabUIView.Instance = null;
		base.ReleaseSelf(true);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void OnButtonBeginClick()
	{
		MemCollabManager.Instance.BeginMemoryFlop();
	}

	private void OnButtonAddClick()
	{
		if (!MemCollabManager.Instance.IsTodayRestBuyTimesOn())
		{
			return;
		}
		MemCollabManager.Instance.BuyExtentTimes();
	}

	public void OnMemCollabClick(int index)
	{
		if (!this.IsMemcollabClickOn)
		{
			return;
		}
		if (this.m_openIndexs.Contains(index))
		{
			return;
		}
		if (this.m_openIndexs.get_Count() < 2)
		{
			if (index < this.mCollabPool.Items.get_Count())
			{
				this.mCollabPool.Items.get_Item(index).GetComponent<MemCollabItem>().SetAnimation(MemCollabItem.PLAY_ANIM.Back_To_Front);
			}
			this.m_openIndexs.Add(index);
			MemCollabManager.Instance.FlopTimes++;
		}
		if (this.m_openIndexs.get_Count() == 2)
		{
			this.IsMemcollabClickOn = false;
			if (MemCollabManager.Instance.IsSame(this.m_openIndexs.get_Item(0), this.m_openIndexs.get_Item(1)))
			{
				this.TimerID = TimerHeap.AddTimer(500u, 0, delegate
				{
					if (this.m_openIndexs.get_Count() == 2 && MemCollabManager.Instance.IsSame(this.m_openIndexs.get_Item(0), this.m_openIndexs.get_Item(1)))
					{
						this.SetBackOrHide(false);
					}
				});
			}
			else
			{
				this.TimerID = TimerHeap.AddTimer(500u, 0, delegate
				{
					if (this.m_openIndexs.get_Count() == 2 && !MemCollabManager.Instance.IsSame(this.m_openIndexs.get_Item(0), this.m_openIndexs.get_Item(1)))
					{
						this.SetBackOrHide(true);
					}
				});
			}
		}
	}

	public void RefreshUI()
	{
		this.SetRewardTimes(MemCollabManager.Instance.TodayRestTimes);
		if (MemCollabManager.Instance.IsMemCollabGoing)
		{
			this.ShowIsBegin(true);
			MemCollabManager.Instance.SetTime();
			this.SetItemsOfCollab();
		}
		else
		{
			this.ShowIsBegin(false);
			this.SetTimeNoBegin();
		}
	}

	public void MemCollabBegin()
	{
		this.m_openIndexs.Clear();
		this.ShowIsBegin(true);
		this.SetItemsOfCollab();
	}

	public void SetTime(int seconds)
	{
		this.m_lblGameTime.set_text(string.Format("时间: {0}秒", seconds));
	}

	public void SetTimeUseUp(int minute)
	{
		this.m_lblGameTime.set_text(string.Format("时间: {0}分钟+", minute));
	}

	public void SetTimeNoBegin()
	{
		this.m_lblGameTime.set_text("时间: 未开始");
		this.m_spGameScoreImage.set_enabled(false);
	}

	public void SetScore(int seconds)
	{
		this.m_spGameScoreImage.set_enabled(true);
		ResourceManager.SetIconSprite(this.m_spGameScoreImage, MemCollabManager.Instance.GetScoreSprite(seconds));
		this.m_spGameScoreImage.SetNativeSize();
	}

	public void ShowIsBegin(bool isShow)
	{
		this.m_goRegionNoBegin.SetActive(!isShow);
		this.m_goRegionBegin.SetActive(isShow);
	}

	public void SetRewardTimes(int times)
	{
		this.m_lblRewardTimes.set_text(GameDataUtils.GetChineseContent(500120, false) + times);
	}

	public void ShowRewardTimesAdd(bool isShow)
	{
		this.m_goRewardTimesAdd.SetActive(isShow);
	}

	private void SetItemsOfCollab()
	{
		this.mCollabPool.Create(MemCollabManager.Instance.CardIndexs.get_Count(), delegate(int index)
		{
			if (index < MemCollabManager.Instance.CardIndexs.get_Count() && index < this.mCollabPool.Items.get_Count())
			{
				MemCollabItem component = this.mCollabPool.Items.get_Item(index).GetComponent<MemCollabItem>();
				component.index = index;
				if (MemCollabManager.Instance.SuccessIndexs.Contains(index))
				{
					component.SetHide();
				}
				else
				{
					component.SetBack();
				}
				int key = MemCollabManager.Instance.CardIndexs.get_Item(index);
				ChongWuLianLianKan chongWuLianLianKan = DataReader<ChongWuLianLianKan>.Get(key);
				if (chongWuLianLianKan != null)
				{
					component.SetIcon(GameDataUtils.GetIcon(chongWuLianLianKan.picture));
				}
			}
		});
	}

	public void SetItemsOfRank()
	{
		this.mRankPool.Create(MemCollabManager.Instance.RankInfo.get_Count(), delegate(int index)
		{
			if (index < MemCollabManager.Instance.RankInfo.get_Count() && index < this.mRankPool.Items.get_Count())
			{
				GameObject gameObject = this.mRankPool.Items.get_Item(index);
				MemoryFlopRankInfo memoryFlopRankInfo = MemCollabManager.Instance.RankInfo.get_Item(index);
				gameObject.get_transform().FindChild("Rank").GetComponent<Text>().set_text((index + 1).ToString());
				gameObject.get_transform().FindChild("Name").GetComponent<Text>().set_text("." + memoryFlopRankInfo.name);
				gameObject.get_transform().FindChild("SuccessTime").GetComponent<Text>().set_text(TimeConverter.GetTime(memoryFlopRankInfo.passTime, TimeFormat.MMSS_Chinese));
			}
		});
	}

	private void SetBackOrHide(bool isBack)
	{
		this.IsMemcollabClickOn = true;
		if (this.m_openIndexs.get_Count() == 2)
		{
			for (int i = 0; i < this.m_openIndexs.get_Count(); i++)
			{
				int num = this.m_openIndexs.get_Item(i);
				if (num < this.mCollabPool.Items.get_Count())
				{
					if (isBack)
					{
						this.mCollabPool.Items.get_Item(num).GetComponent<MemCollabItem>().SetAnimation(MemCollabItem.PLAY_ANIM.Front_To_Back);
					}
					else
					{
						this.mCollabPool.Items.get_Item(num).GetComponent<MemCollabItem>().SetAnimation(MemCollabItem.PLAY_ANIM.Front_To_Hide);
					}
				}
			}
			if (!isBack)
			{
				MemCollabManager.Instance.SuccessIndexs.AddRange(this.m_openIndexs);
				if (MemCollabManager.Instance.SuccessIndexs.get_Count() == MemCollabManager.Instance.CardIndexs.get_Count())
				{
					this.GameSuccess();
				}
			}
			this.m_openIndexs.Clear();
		}
	}

	private void GameSuccess()
	{
		MemCollabManager.Instance.SendEndMemoryFlop();
		this.mCollabPool.Create(0, null);
		this.ShowIsBegin(false);
	}
}
