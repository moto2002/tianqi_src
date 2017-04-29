using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyItem : MonoBehaviour
{
	public void updateItem(int stage)
	{
		base.set_name((SurvivalManager.Instance.StageMax - stage).ToString());
		base.get_transform().Find("Text").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(512030, false), stage));
		string spriteName = string.Empty;
		if (stage == SurvivalManager.Instance.StageCurr && !SurvivalManager.Instance.IsPassAll)
		{
			spriteName = "tzfb_mark2";
		}
		else if (stage > SurvivalManager.Instance.StageCurr)
		{
			spriteName = "tzfb_mark3";
		}
		else
		{
			spriteName = "tzfb_mark1";
		}
		ResourceManager.SetSprite(base.get_transform().Find("Flag").GetComponent<Image>(), ResourceManager.GetIconSprite(spriteName));
	}
}
