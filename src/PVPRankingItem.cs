using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PVPRankingItem : MonoBehaviour
{
	public Image Topicon;

	public Image Headicon;

	public Image Integralicon;

	public Text ranking;

	public Text playerName;

	public Text integral;

	public Text levelText;

	private void Awake()
	{
	}

	private string GetRank(int p)
	{
		switch (p)
		{
		case 1:
			return "icon_paiming_1";
		case 2:
			return "icon_paiming_2";
		case 3:
			return "icon_paiming_3";
		default:
			return p.ToString();
		}
	}

	public void UpdateItem(RankingsItem item)
	{
		if (item != null)
		{
			string rank = this.GetRank(item.rank);
			if (item.rank > 3)
			{
				this.switchBut(true);
				this.ranking.set_text(rank);
			}
			else
			{
				this.switchBut(false);
				ResourceManager.SetSprite(this.Topicon, ResourceManager.GetCodeSprite(rank));
			}
			this.playerName.set_text(item.name);
			this.integral.set_text(string.Format("{0}分", item.score));
			this.levelText.set_text(string.Format("Lv{0}", item.lv));
			ResourceManager.SetSprite(this.Integralicon, ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(item.score, false)));
			ResourceManager.SetSprite(this.Headicon, UIUtils.GetRoleSmallIcon(item.modelId));
		}
	}

	private void switchBut(bool isRank)
	{
		this.ranking.get_gameObject().SetActive(isRank);
		this.Topicon.get_gameObject().SetActive(!isRank);
	}
}
