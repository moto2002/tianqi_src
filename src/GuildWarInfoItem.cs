using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class GuildWarInfoItem : BaseUIBehaviour
{
	private Text textRank;

	private Text textName;

	private Text textLV;

	private Text textPower;

	private Text textGive;

	private Text textSceneStatus;

	private Image rankImg;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.textRank = base.FindTransform("TextRank").GetComponent<Text>();
		this.textName = base.FindTransform("TextName").GetComponent<Text>();
		this.textLV = base.FindTransform("TextLV").GetComponent<Text>();
		this.textPower = base.FindTransform("TextPower").GetComponent<Text>();
		this.textGive = base.FindTransform("TextGive").GetComponent<Text>();
		this.textSceneStatus = base.FindTransform("TextScene").GetComponent<Text>();
		this.rankImg = base.FindTransform("Top").GetComponent<Image>();
		this.rankImg.get_gameObject().SetActive(true);
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateItem(GuildMemberInfoInGuildWarScene memberInfo, int rank = 1)
	{
		if (memberInfo == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.textGive.set_text(memberInfo.ResourceNum + string.Empty);
		this.textLV.set_text("Lv" + memberInfo.RoleLv + string.Empty);
		this.textName.set_text(memberInfo.RoleName);
		this.textPower.set_text(memberInfo.RoleFighting + string.Empty);
		this.textSceneStatus.set_text(string.Empty);
		this.textRank.set_text(string.Empty);
		if (rank <= 3 && rank > 0)
		{
			this.rankImg.set_enabled(true);
			string rankIconName = this.GetRankIconName(rank);
			ResourceManager.SetCodeSprite(this.rankImg, rankIconName);
		}
		else
		{
			this.textRank.set_text(rank + string.Empty);
			this.rankImg.set_enabled(false);
		}
		this.textSceneStatus.set_text(GuildWarManager.Instance.GetMemberInSceneStatusDes(memberInfo.Status));
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
