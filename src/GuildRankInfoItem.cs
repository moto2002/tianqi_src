using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine.UI;

public class GuildRankInfoItem : BaseUIBehaviour
{
	private Text textRank;

	private Text textGuildName;

	private Text textGuildLV;

	private Text textPower;

	private Text textGuildLeaderName;

	private Text textGuildMemberSize;

	private Text textGuildWarRank;

	private Image rankImg;

	private bool isInit;

	private GuildBriefInfo m_guildBriefInfo;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.textRank = base.FindTransform("TextRank").GetComponent<Text>();
		this.textGuildName = base.FindTransform("TextGuildName").GetComponent<Text>();
		this.textGuildLV = base.FindTransform("TextLV").GetComponent<Text>();
		this.textPower = base.FindTransform("TextPower").GetComponent<Text>();
		this.textGuildLeaderName = base.FindTransform("TextGuildLeaderName").GetComponent<Text>();
		this.textGuildMemberSize = base.FindTransform("TextGuildMemberSize").GetComponent<Text>();
		this.textGuildWarRank = base.FindTransform("TextGuildWarRank").GetComponent<Text>();
		this.rankImg = base.FindTransform("Top").GetComponent<Image>();
		this.isInit = false;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateItem(GuildBriefInfo guildInfo)
	{
		if (guildInfo == null)
		{
			return;
		}
		this.m_guildBriefInfo = guildInfo;
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.textGuildLV.set_text("Lv" + guildInfo.lv + string.Empty);
		this.textGuildName.set_text(guildInfo.name);
		this.textPower.set_text(guildInfo.fighting + string.Empty);
		this.textGuildLeaderName.set_text(guildInfo.chairman.name);
		this.textGuildMemberSize.set_text(guildInfo.size + string.Empty);
		this.textGuildWarRank.set_text((guildInfo.lastWarRank > 0) ? (guildInfo.lastWarRank + string.Empty) : "无");
		this.textRank.set_text(string.Empty);
		int rank = guildInfo.rank;
		if (rank <= 3 && rank > 0)
		{
			this.rankImg.get_gameObject().SetActive(true);
			string rankIconName = this.GetRankIconName(rank);
			ResourceManager.SetCodeSprite(this.rankImg, rankIconName);
		}
		else
		{
			this.rankImg.get_gameObject().SetActive(false);
			this.textRank.set_text(rank + string.Empty);
		}
	}

	private string GetRankIconName(int rank)
	{
		switch (rank)
		{
		case 1:
			return "icon_paiming_1";
		case 2:
			return "icon_paiming_2";
		case 3:
			return "icon_paiming_3";
		default:
			return rank.ToString();
		}
	}
}
