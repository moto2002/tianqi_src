using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemCollabRewardUIView : UIBase
{
	public const uint DELAY_01 = 400u;

	public const uint DELAY_02 = 900u;

	public const uint DELAY_03 = 1200u;

	public static MemCollabRewardUIView Instance;

	private ListPool mPool;

	private GridLayoutGroup mGridLayoutGroup;

	private ListPool mPoolRewardsToScore;

	private Text m_lblGameTime;

	private Image m_spGameScore;

	private Transform mTitle;

	private Transform mGameScore;

	private int mSecond;

	private bool m_islock;

	private int fx_title_id;

	private int fx_score_id;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		MemCollabRewardUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mPool = base.FindTransform("Rewards").GetComponent<ListPool>();
		this.mPool.SetItem("RewardItem");
		this.mPool.LoadNumberFrame = 1;
		this.mPool.LoadInterval = 0.1f;
		this.mGridLayoutGroup = base.FindTransform("Rewards").GetComponent<GridLayoutGroup>();
		this.mPoolRewardsToScore = base.FindTransform("RewardsToScore").GetComponent<ListPool>();
		this.mPoolRewardsToScore.SetItem("RewardItem");
		this.mPoolRewardsToScore.LoadNumberFrame = 1;
		this.mPoolRewardsToScore.LoadInterval = 0.1f;
		this.m_lblGameTime = base.FindTransform("GameTime").GetComponent<Text>();
		this.m_spGameScore = base.FindTransform("GameScore").GetComponent<Image>();
		this.mTitle = base.FindTransform("Title");
		this.mGameScore = base.FindTransform("GameScore");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		FXSpineManager.Instance.DeleteSpine(this.fx_title_id, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_score_id, true);
	}

	protected override void OnClickMaskAction()
	{
		if (!this.m_islock)
		{
			base.OnClickMaskAction();
		}
	}

	protected override void ReleaseSelf(bool callDestroy)
	{
		MemCollabRewardUIView.Instance = null;
		base.ReleaseSelf(true);
	}

	public void SetRewards(List<ItemBriefInfo> rewards)
	{
		TimerHeap.AddTimer(1200u, 0, delegate
		{
			this.JustSetRewards(rewards);
		});
	}

	public void SetRewardsToScore(List<ItemBriefInfo> rewards)
	{
		if (rewards == null)
		{
			return;
		}
		this.mPoolRewardsToScore.Create(rewards.get_Count(), delegate(int index)
		{
			if (index < this.mPoolRewardsToScore.Items.get_Count() && index < rewards.get_Count())
			{
				RewardItem component = this.mPoolRewardsToScore.Items.get_Item(index).GetComponent<RewardItem>();
				component.SetRewardItem(rewards.get_Item(index).cfgId, rewards.get_Item(index).count, 0L);
			}
		});
	}

	public void SetTime(int second)
	{
		if (second <= 300)
		{
			this.m_lblGameTime.set_text(string.Format("时间：{0}秒", second));
		}
		else
		{
			this.m_lblGameTime.set_text(string.Format("时间: {0}分钟+", 5));
		}
	}

	public void StartSpine(int second)
	{
		this.m_islock = true;
		this.mSecond = second;
		this.fx_title();
		TimerHeap.AddTimer(400u, 0, delegate
		{
			this.fx_score();
		});
	}

	private void JustSetRewards(List<ItemBriefInfo> rewards)
	{
		int num = Mathf.CeilToInt((float)rewards.get_Count() * 0.5f);
		this.mGridLayoutGroup.set_constraintCount(num);
		RectTransform rectTransform = this.mGridLayoutGroup.get_transform() as RectTransform;
		rectTransform.set_anchoredPosition(new Vector2((float)(-(float)num) * 0.5f * this.mGridLayoutGroup.get_cellSize().x, rectTransform.get_anchoredPosition().y));
		this.mPool.Create(rewards.get_Count(), delegate(int index)
		{
			if (index < this.mPool.Items.get_Count() && index < rewards.get_Count())
			{
				RewardItem component = this.mPool.Items.get_Item(index).GetComponent<RewardItem>();
				component.SetRewardItem(rewards.get_Item(index).cfgId, rewards.get_Item(index).count, 0L);
				component.StartSpine("MemCollabRewardUI", 3001);
				if (index == rewards.get_Count() - 1)
				{
					this.m_islock = false;
				}
			}
		});
	}

	private void fx_title()
	{
		this.fx_title_id = FXSpineManager.Instance.PlaySpine(3406, this.mTitle, "MemCollabRewardUI", 3001, delegate
		{
			this.fx_title_id = FXSpineManager.Instance.PlaySpine(3401, this.mTitle, "MemCollabRewardUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void fx_score()
	{
		if (this.mSecond <= MemCollabManager.Instance.LEVEL_S)
		{
			this.fx_score(3407, 3403);
		}
		else if (this.mSecond <= MemCollabManager.Instance.LEVEL_A)
		{
			this.fx_score(3408, 3404);
		}
		else
		{
			this.fx_score(3409, 3405);
		}
	}

	private void fx_score(int id1, int id2)
	{
		this.fx_score_id = FXSpineManager.Instance.PlaySpine(id1, this.mGameScore, "MemCollabRewardUI", 3001, delegate
		{
			this.fx_score_id = FXSpineManager.Instance.PlaySpine(id2, this.mGameScore, "MemCollabRewardUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
