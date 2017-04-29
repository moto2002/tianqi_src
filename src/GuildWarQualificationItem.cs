using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine.UI;

public class GuildWarQualificationItem : BaseUIBehaviour
{
	private bool isInit;

	private Text guildRankingText;

	private Text guildNameText;

	private Text guildLvText;

	private Text guildFightingText;

	private Text guildActivePointText;

	private Image guildRankImg;

	private GuildParticipantInfo m_guildWarQualitificationRankData;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.guildRankingText = base.FindTransform("TextRank").GetComponent<Text>();
		this.guildNameText = base.FindTransform("TextName").GetComponent<Text>();
		this.guildLvText = base.FindTransform("TextLV").GetComponent<Text>();
		this.guildFightingText = base.FindTransform("TextPower").GetComponent<Text>();
		this.guildActivePointText = base.FindTransform("TextActivePoint").GetComponent<Text>();
		this.guildRankImg = base.FindTransform("Top").GetComponent<Image>();
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateItem(GuildParticipantInfo guildParticipantInfo)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (guildParticipantInfo == null)
		{
			return;
		}
		this.m_guildWarQualitificationRankData = guildParticipantInfo;
		this.guildRankingText.set_text(string.Empty);
		if (guildParticipantInfo.rank <= 3 && guildParticipantInfo.rank > 0)
		{
			this.guildRankImg.set_enabled(true);
			string rankIconName = this.GetRankIconName(guildParticipantInfo.rank);
			ResourceManager.SetCodeSprite(this.guildRankImg, rankIconName);
		}
		else
		{
			this.guildRankImg.set_enabled(false);
			this.guildRankingText.set_text(guildParticipantInfo.rank + string.Empty);
		}
		this.guildNameText.set_text(guildParticipantInfo.name);
		this.guildLvText.set_text("Lv" + guildParticipantInfo.lv);
		this.guildFightingText.set_text(guildParticipantInfo.fighting + string.Empty);
		this.guildActivePointText.set_text(guildParticipantInfo.activity + string.Empty);
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
