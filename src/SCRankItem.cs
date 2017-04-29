using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SCRankItem : MonoBehaviour
{
	public Text ranking;

	public Image RankIcon;

	public Text playerName;

	public Text playerLevel;

	public Text fight;

	public Text hard;

	public Text passTime;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void UpdateItem(SecretAreaRankInfo item)
	{
		if (item != null)
		{
			if (item.rank == 1)
			{
				this.ranking.get_gameObject().SetActive(false);
				this.RankIcon.get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("fight_hurtfont_1"));
			}
			else if (item.rank == 2)
			{
				this.ranking.get_gameObject().SetActive(false);
				this.RankIcon.get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("fight_bloodfont_2"));
			}
			else if (item.rank == 3)
			{
				this.ranking.get_gameObject().SetActive(false);
				this.RankIcon.get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.RankIcon, ResourceManager.GetIconSprite("fight_normalfont_3"));
			}
			else
			{
				this.ranking.set_text(item.rank.ToString());
				this.ranking.get_gameObject().SetActive(true);
				this.RankIcon.get_gameObject().SetActive(false);
			}
			this.playerName.set_text(item.name);
			this.playerLevel.set_text("Lv" + item.lv.ToString());
			this.fight.set_text(item.fighting.ToString());
			this.hard.set_text(item.maxClearBatch.ToString());
			this.passTime.set_text(item.clearCostTime.ToString() + "s");
		}
	}
}
