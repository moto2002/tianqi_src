using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BountyRankItem : MonoBehaviour
{
	public Text ranking;

	public Image RankIcon;

	public Image RankIconBackground;

	public Image playerIcon;

	public Text playerName;

	public Text playerLevel;

	public Text mark;

	public GameObject plates;

	public void UpdateItem(BountyRankListInfo item)
	{
		if (item != null)
		{
			this.ranking.get_gameObject().SetActive(false);
			this.RankIcon.get_gameObject().SetActive(true);
			this.plates.SetActive(true);
			if (item.rank == 1)
			{
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("icon_paiming_1"));
				ResourceManager.SetSprite(this.RankIconBackground, ResourceManager.GetIconSprite("ranking_add_1"));
			}
			else if (item.rank == 2)
			{
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("icon_paiming_2"));
				ResourceManager.SetSprite(this.RankIconBackground, ResourceManager.GetIconSprite("ranking_add_2"));
			}
			else if (item.rank == 3)
			{
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("icon_paiming_3"));
				ResourceManager.SetSprite(this.RankIconBackground, ResourceManager.GetIconSprite("ranking_add_3"));
			}
			else
			{
				this.ranking.set_text(item.rank.ToString());
				this.ranking.get_gameObject().SetActive(true);
				this.RankIcon.get_gameObject().SetActive(false);
				this.plates.SetActive(false);
			}
			this.playerName.set_text(item.roleName);
			this.playerLevel.set_text("Lv" + item.roleLv.ToString());
			ResourceManager.SetSprite(this.playerIcon, UIUtils.GetRoleSmallIcon(item.career));
			this.mark.set_text(item.score.ToString());
		}
	}
}
