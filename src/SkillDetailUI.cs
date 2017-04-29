using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine.UI;

public class SkillDetailUI : UIBase
{
	protected Text SkillDetailUITitleName;

	protected Image SkillDetailUIIconFG;

	protected Text SkillDetailUIInfo;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.SkillDetailUITitleName = base.FindTransform("SkillDetailUITitleName").GetComponent<Text>();
		this.SkillDetailUIIconFG = base.FindTransform("SkillDetailUIIconFG").GetComponent<Image>();
		this.SkillDetailUIInfo = base.FindTransform("SkillDetailUIInfo").GetComponent<Text>();
		base.FindTransform("SkillDetailUICloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetData(int skillID)
	{
		if (!DataReader<Skill>.Contains(skillID))
		{
			return;
		}
		Skill skill = DataReader<Skill>.Get(skillID);
		this.SkillDetailUITitleName.set_text(GameDataUtils.GetChineseContent(skill.name, false));
		ResourceManager.SetSprite(this.SkillDetailUIIconFG, GameDataUtils.GetIcon(skill.icon));
		this.SkillDetailUIInfo.set_text(GameDataUtils.GetChineseContent(skill.describeId, false));
	}
}
