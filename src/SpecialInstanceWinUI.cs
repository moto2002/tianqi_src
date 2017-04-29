using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceWinUI : UIBase
{
	public Text gold;

	public Text exp;

	public Text time;

	public ButtonCustom exitButton;

	public Transform rewardItems;

	public Image ResultImage;

	public Image ResultImageFx;

	public Text num;

	private void Start()
	{
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Utils.WinSetting(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Utils.WinSetting(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickDefendExit(GameObject go)
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		EventDispatcher.Broadcast("GuideManager.InstanceWin");
		DefendFightManager.Instance.ExitDefendFightReq(false);
	}

	private void OnClickExperienceExit(GameObject go)
	{
		SpecialFightManager.Instance.ExitExperienceReq(false);
	}

	public void UpdateData(DefendFightBtlResultNty result)
	{
		if ((DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Hold || DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Save || DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Protect) && result.result.winnerId != EntityWorld.Instance.EntSelf.ID)
		{
			ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("failure_bg_zi01"));
			ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_02"));
		}
		else
		{
			ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("win_01"));
			ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_01"));
		}
		Debug.LogError(result.normalDropItems.get_Count() + "===========" + result.extendDropItems.get_Count());
		for (int i = 0; i < this.rewardItems.get_childCount(); i++)
		{
			Object.Destroy(this.rewardItems.GetChild(i).get_gameObject());
		}
		using (List<ItemBriefInfo>.Enumerator enumerator = result.normalDropItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ItemBriefInfo current = enumerator.get_Current();
				if (current.cfgId == 1)
				{
					this.exp.set_text(current.count.ToString());
				}
				else if (current.cfgId == 2)
				{
					this.gold.set_text(current.count.ToString());
				}
				else
				{
					ItemShow.ShowItem(this.rewardItems, current.cfgId, current.count, false, null, 2001);
				}
			}
		}
		base.FindTransform("RewardBgsBoss").get_gameObject().SetActive(result.extendDropItems.get_Count() > 0);
		Transform transform = base.FindTransform("BossRewardItems");
		for (int j = 0; j < transform.get_childCount(); j++)
		{
			Object.Destroy(transform.GetChild(j).get_gameObject());
		}
		for (int k = 0; k < result.extendDropItems.get_Count(); k++)
		{
			ItemBriefInfo itemBriefInfo = result.extendDropItems.get_Item(k);
			ItemShow.ShowItem(transform, itemBriefInfo.cfgId, itemBriefInfo.count, false, null, 2001);
		}
		this.time.set_text(GameDataUtils.GetChineseContent(501004, false) + " " + TimeConverter.GetTime(result.result.killTargetUsedTime, TimeFormat.HHMMSS));
		int id = (DefendFightManager.Instance.SelectDetailMode != DefendFightMode.DFMD.Protect) ? 513536 : 513537;
		this.num.set_text(string.Format(GameDataUtils.GetChineseContent(id, false), result.maxWave.ToString()));
		this.exitButton.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDefendExit);
	}

	public void UpdateData(ResultExperienceCopyNty result, int batch = -1)
	{
		ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("win_01"));
		ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_01"));
		for (int i = 0; i < this.rewardItems.get_childCount(); i++)
		{
			Object.Destroy(this.rewardItems.GetChild(i).get_gameObject());
		}
		using (List<DropItem>.Enumerator enumerator = result.item.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DropItem current = enumerator.get_Current();
				if (current.typeId == 1)
				{
					this.exp.set_text(current.count.ToString());
				}
				else if (current.typeId == 2)
				{
					this.gold.set_text(current.count.ToString());
				}
				else
				{
					ItemShow.ShowItem(this.rewardItems, current.typeId, current.count, false, null, 2001);
				}
			}
		}
		base.FindTransform("RewardBgsBoss").get_gameObject().SetActive(false);
		Transform transform = base.FindTransform("BossRewardItems");
		for (int j = 0; j < transform.get_childCount(); j++)
		{
			Object.Destroy(transform.GetChild(j).get_gameObject());
		}
		this.time.set_text(string.Empty);
		this.num.set_text((batch < 0) ? string.Empty : string.Format(GameDataUtils.GetChineseContent(513536, false), batch.ToString()));
		this.exitButton.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExperienceExit);
	}

	public void anim_Items(AnimationEvent e)
	{
	}

	private void anim_RewardNums()
	{
	}

	public void anim_WinPop()
	{
	}

	public void anim_End(AnimationEvent e)
	{
	}
}
