using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangFightSettle : UIBase
{
	private Image ImageLose;

	private Image ImageWin;

	private Text TextNumRight;

	private Text TextNameRight;

	private Image ImageDefeatRight;

	private Image ImageHeadRight;

	private Text TextNumLeft;

	private Text TextNameLeft;

	private Image ImageDefeatLeft;

	private Image ImageHeadLeft;

	private Image ImageWinsDefeatRight;

	private Image ImageWinsDefeatLeft;

	private Transform WinDerc;

	private Transform LoseDerc;

	private Transform ImageBGWinLeft;

	private Transform ImageBGLoseLeft;

	private Transform ImageTitleBGWinLeft;

	private Transform ImageTitleBGLoseLeft;

	private Transform ImageWinBGWinLeft;

	private Transform ImageWinBGLoseLeft;

	private Transform ImageBGWinRight;

	private Transform ImageBGLoseRight;

	private Transform ImageTitleBGWinRight;

	private Transform ImageTitleBGLoseRight;

	private Transform ImageWinBGWinRight;

	private Transform ImageWinBGLoseRight;

	private Transform TextAllLose;

	private Transform TextSelfLose;

	private Transform Reward;

	private Transform Lose;

	private List<Transform> listWinShow;

	private List<Transform> listLoseShow;

	private List<Transform> rewardItemsList;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = false;
		this.alpha = 0.75f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageLose = base.FindTransform("ImageLose").GetComponent<Image>();
		this.ImageWin = base.FindTransform("ImageWin").GetComponent<Image>();
		this.TextNumRight = base.FindTransform("TextNumRight").GetComponent<Text>();
		this.TextNameRight = base.FindTransform("TextNameRight").GetComponent<Text>();
		this.ImageDefeatRight = base.FindTransform("ImageDefeatRight").GetComponent<Image>();
		this.ImageHeadRight = base.FindTransform("ImageHeadRight").GetComponent<Image>();
		this.TextNumLeft = base.FindTransform("TextNumLeft").GetComponent<Text>();
		this.TextNameLeft = base.FindTransform("TextNameLeft").GetComponent<Text>();
		this.ImageDefeatLeft = base.FindTransform("ImageDefeatLeft").GetComponent<Image>();
		this.ImageHeadLeft = base.FindTransform("ImageHeadLeft").GetComponent<Image>();
		this.Reward = base.FindTransform("Reward");
		this.Lose = base.FindTransform("Lose");
		this.ImageWinsDefeatRight = base.FindTransform("ImageWinsDefeatRight").GetComponent<Image>();
		this.ImageWinsDefeatLeft = base.FindTransform("ImageWinsDefeatLeft").GetComponent<Image>();
		this.WinDerc = base.FindTransform("WinDerc");
		this.LoseDerc = base.FindTransform("LoseDerc");
		this.ImageBGWinLeft = base.FindTransform("ImageBGWinLeft");
		this.ImageBGLoseLeft = base.FindTransform("ImageBGLoseLeft");
		this.ImageTitleBGWinLeft = base.FindTransform("ImageTitleBGWinLeft");
		this.ImageTitleBGLoseLeft = base.FindTransform("ImageTitleBGLoseLeft");
		this.ImageWinBGWinLeft = base.FindTransform("ImageWinBGWinLeft");
		this.ImageWinBGLoseLeft = base.FindTransform("ImageWinBGLoseLeft");
		this.ImageBGWinRight = base.FindTransform("ImageBGWinRight");
		this.ImageBGLoseRight = base.FindTransform("ImageBGLoseRight");
		this.ImageTitleBGWinRight = base.FindTransform("ImageTitleBGWinRight");
		this.ImageTitleBGLoseRight = base.FindTransform("ImageTitleBGLoseRight");
		this.ImageWinBGWinRight = base.FindTransform("ImageWinBGWinRight");
		this.ImageWinBGLoseRight = base.FindTransform("ImageWinBGLoseRight");
		this.TextAllLose = base.FindTransform("TextAllLose");
		this.TextSelfLose = base.FindTransform("TextSelfLose");
		this.rewardItemsList = new List<Transform>();
		for (int i = 1; i < 4; i++)
		{
			Transform transform = base.FindTransform("Reward" + i);
			if (transform != null)
			{
				this.rewardItemsList.Add(transform);
			}
		}
		this.listWinShow = new List<Transform>();
		this.listWinShow.Add(this.ImageBGWinLeft);
		this.listWinShow.Add(this.ImageTitleBGWinLeft);
		this.listWinShow.Add(this.ImageWinBGWinLeft);
		this.listWinShow.Add(this.ImageBGWinRight);
		this.listWinShow.Add(this.ImageTitleBGWinRight);
		this.listWinShow.Add(this.ImageWinBGWinRight);
		this.listLoseShow = new List<Transform>();
		this.listLoseShow.Add(this.ImageBGLoseLeft);
		this.listLoseShow.Add(this.ImageTitleBGLoseLeft);
		this.listLoseShow.Add(this.ImageWinBGLoseLeft);
		this.listLoseShow.Add(this.ImageBGLoseRight);
		this.listLoseShow.Add(this.ImageTitleBGLoseRight);
		this.listLoseShow.Add(this.ImageWinBGLoseRight);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	private void ShowLoseUIs()
	{
		this.WinDerc.get_gameObject().SetActive(false);
		this.LoseDerc.get_gameObject().SetActive(true);
		this.ImageWin.get_gameObject().SetActive(false);
		this.ImageLose.get_gameObject().SetActive(true);
		this.Lose.get_gameObject().SetActive(true);
		this.Reward.get_gameObject().SetActive(false);
		for (int i = 0; i < this.listWinShow.get_Count(); i++)
		{
			this.listWinShow.get_Item(i).get_gameObject().SetActive(false);
		}
		for (int j = 0; j < this.listLoseShow.get_Count(); j++)
		{
			this.listLoseShow.get_Item(j).get_gameObject().SetActive(true);
		}
	}

	private void ShowWinUIs()
	{
		this.WinDerc.get_gameObject().SetActive(true);
		this.LoseDerc.get_gameObject().SetActive(false);
		this.ImageWin.get_gameObject().SetActive(true);
		this.ImageLose.get_gameObject().SetActive(false);
		this.Reward.get_gameObject().SetActive(true);
		this.Lose.get_gameObject().SetActive(false);
		for (int i = 0; i < this.listWinShow.get_Count(); i++)
		{
			this.listWinShow.get_Item(i).get_gameObject().SetActive(true);
		}
		for (int j = 0; j < this.listLoseShow.get_Count(); j++)
		{
			this.listLoseShow.get_Item(j).get_gameObject().SetActive(false);
		}
	}

	private void SetRewardItems(List<ItemBriefInfo> rewardItems, List<ItemBriefInfo> rewardItemsExt)
	{
		int i = 0;
		if (rewardItems != null)
		{
			while (i < rewardItems.get_Count())
			{
				long num = rewardItems.get_Item(i).count;
				int cfgId = rewardItems.get_Item(i).cfgId;
				long uId = rewardItems.get_Item(i).uId;
				if (rewardItemsExt != null && i < rewardItemsExt.get_Count())
				{
					num += rewardItemsExt.get_Item(i).count;
				}
				if (i < this.rewardItemsList.get_Count())
				{
					GameObject gameObject = this.rewardItemsList.get_Item(i).get_gameObject();
					if (gameObject != null && !gameObject.get_activeSelf())
					{
						gameObject.SetActive(true);
					}
					Text component = this.rewardItemsList.get_Item(i).FindChild("TextReward").GetComponent<Text>();
					component.set_text(num + string.Empty);
					Image component2 = this.rewardItemsList.get_Item(i).FindChild("ImageReward").GetComponent<Image>();
					ResourceManager.SetSprite(component2, GameDataUtils.GetItemIcon(cfgId));
				}
				i++;
			}
		}
		for (int j = i; j < 3; j++)
		{
			GameObject gameObject2 = this.rewardItemsList.get_Item(j).get_gameObject();
			if (gameObject2 != null && gameObject2.get_activeSelf())
			{
				gameObject2.SetActive(false);
			}
		}
	}

	public void RefreshUI(GangFightBattleResult gfbr)
	{
		if (gfbr == null)
		{
			return;
		}
		this.ImageWinsDefeatRight.get_gameObject().SetActive(false);
		this.ImageWinsDefeatLeft.get_gameObject().SetActive(false);
		this.TextAllLose.get_gameObject().SetActive(false);
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(EntityWorld.Instance.EntSelf.FixModelID);
		if (avatarModel != null)
		{
			ResourceManager.SetSprite(this.ImageHeadLeft, GameDataUtils.GetIcon(avatarModel.pic));
		}
		AvatarModel avatarModel2 = DataReader<AvatarModel>.Get(GangFightManager.Instance.gangFightMatchRoleSummary.modelId);
		if (avatarModel2 != null)
		{
			ResourceManager.SetSprite(this.ImageHeadRight, GameDataUtils.GetIcon(avatarModel2.pic));
		}
		this.SetRewardItems(gfbr.reward, gfbr.rewardExt);
		if (gfbr.winnerId == 0L)
		{
			this.ShowLoseUIs();
			this.TextNameLeft.set_text(EntityWorld.Instance.EntSelf.Name);
			this.TextNumLeft.set_text(gfbr.fromCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
			this.TextNumRight.set_text(gfbr.toCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
			this.TextNameRight.set_text(gfbr.toName);
			if (gfbr.fromLastCombatWinCount > 1)
			{
				this.ImageWinsDefeatLeft.get_gameObject().SetActive(true);
			}
			if (gfbr.toLastCombatWinCount > 1)
			{
				this.ImageWinsDefeatRight.get_gameObject().SetActive(true);
			}
			this.ImageDefeatLeft.get_gameObject().SetActive(false);
			this.ImageDefeatRight.get_gameObject().SetActive(false);
			this.TextAllLose.get_gameObject().SetActive(true);
			this.TextSelfLose.get_gameObject().SetActive(false);
		}
		else
		{
			bool flag = false;
			if (gfbr.winnerId == EntityWorld.Instance.EntSelf.ID)
			{
				flag = true;
			}
			if (flag)
			{
				this.ShowWinUIs();
				this.ImageDefeatLeft.get_gameObject().SetActive(false);
				this.ImageDefeatRight.get_gameObject().SetActive(true);
				this.TextNameLeft.set_text(EntityWorld.Instance.EntSelf.Name);
				this.TextNumLeft.set_text(gfbr.fromCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
				this.TextNumRight.set_text(gfbr.toCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
				this.TextNameRight.set_text(gfbr.toName);
				if (gfbr.toLastCombatWinCount > 1)
				{
					this.ImageWinsDefeatRight.get_gameObject().SetActive(true);
				}
			}
			else
			{
				this.ShowLoseUIs();
				this.TextAllLose.get_gameObject().SetActive(false);
				this.TextSelfLose.get_gameObject().SetActive(true);
				this.ImageDefeatLeft.get_gameObject().SetActive(true);
				this.ImageDefeatRight.get_gameObject().SetActive(false);
				this.TextNameLeft.set_text(EntityWorld.Instance.EntSelf.Name);
				this.TextNumLeft.set_text(gfbr.fromCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
				this.TextNumRight.set_text(gfbr.toCurrCombatWinCount + GameDataUtils.GetChineseContent(510108, false));
				this.TextNameRight.set_text(gfbr.toName);
				if (gfbr.fromLastCombatWinCount > 1)
				{
					this.ImageWinsDefeatLeft.get_gameObject().SetActive(true);
				}
			}
		}
	}

	public void ShowAnimate(bool isWin)
	{
		base.get_gameObject().GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(0f, 1f, 0f, 0.5f, delegate
		{
		});
		TimerHeap.AddTimer(4000u, 0, delegate
		{
			this.get_gameObject().GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(1f, 0f, 0f, 0.5f, delegate
			{
				this.Show(false);
				if (isWin)
				{
					GangFightManager.Instance.SendExitFromGangFightFieldReq();
				}
			});
		});
	}
}
