using GameData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeCareerTask : MonoBehaviour
{
	private int jump;

	private void Awake()
	{
		base.get_transform().FindChild("ButtonLink").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButtonLink));
	}

	private void OnClickButtonLink()
	{
		SourceReferenceUI.GoTo(this.jump);
	}

	public void SetName(ZhuanZhiRenWu dataRW, int count, bool isfinish)
	{
		this.jump = dataRW.uiJump;
		string text = ChangeCareerManager.GetTaskName(dataRW);
		if (dataRW.missionType == 1)
		{
			if (dataRW.missionData.get_Count() >= 2)
			{
				text += string.Format(dataRW.message1, count, dataRW.missionData.get_Item(1));
			}
		}
		else if (dataRW.missionType == 2)
		{
			if (dataRW.missionData.get_Count() >= 2)
			{
				text += string.Format(dataRW.message1, count, dataRW.missionData.get_Item(1));
			}
		}
		else if (dataRW.missionType == 3)
		{
			if (dataRW.missionData.get_Count() >= 3)
			{
				text += string.Format(dataRW.message1, count, dataRW.missionData.get_Item(2));
			}
		}
		else if (dataRW.missionType == 4)
		{
			if (dataRW.missionData.get_Count() >= 2)
			{
				text += string.Format(dataRW.message1, count, dataRW.missionData.get_Item(1));
			}
		}
		else if (dataRW.missionType == 5)
		{
		}
		text = TextColorMgr.FilterColor(text);
		if (isfinish)
		{
			base.get_transform().FindChild("TaskName").GetComponent<Text>().set_text(TextColorMgr.GetColor(text, "FFC32D", string.Empty));
		}
		else
		{
			base.get_transform().FindChild("TaskName").GetComponent<Text>().set_text(TextColorMgr.GetColor(text, "75523E", string.Empty));
		}
	}

	public void SetFinish(bool isfinish)
	{
		this.SetStar(isfinish);
		this.ShowLink(!isfinish);
	}

	private void SetStar(bool active)
	{
		Image component = base.get_transform().FindChild("TaskStar").GetComponent<Image>();
		if (active)
		{
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("fb_star_1"));
		}
		else
		{
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("fb_star_2"));
		}
	}

	private void ShowLink(bool isShow)
	{
		base.get_transform().FindChild("ButtonLink").get_gameObject().SetActive(isShow);
	}
}
