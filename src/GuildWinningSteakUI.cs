using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildWinningSteakUI : UIBase
{
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
		this.UpdateCfgData();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void UpdateCfgData()
	{
		Transform transform = base.FindTransform("GuildWarRewardItem1");
		if (transform != null)
		{
			transform.FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515072, false));
			transform.FindChild("Content").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515073, false));
			Transform parent = transform.FindChild("ItemList");
			int itemId = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("RankingReward").value);
			ItemShow.ShowItem(parent, itemId, -1L, false, null, 2001);
		}
		Transform transform2 = base.FindTransform("GuildWarRewardItem2");
		if (transform2 != null)
		{
			transform2.FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515074, false));
			transform2.FindChild("Content").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515075, false));
			Transform parent2 = transform2.FindChild("ItemList");
			int itemId2 = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("EndReward").value);
			ItemShow.ShowItem(parent2, itemId2, -1L, false, null, 2001);
		}
		Transform transform3 = base.FindTransform("GuildWarRewardItem3");
		if (transform3 != null)
		{
			transform3.FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515076, false));
			transform3.FindChild("Content").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515077, false));
			int id = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("BuffWord").value);
			transform3.FindChild("Content2").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(id, false));
		}
	}
}
