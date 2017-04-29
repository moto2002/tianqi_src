using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GangFightMatchingFinishUI : UIBase
{
	public GameObject[] MyPets;

	public GameObject[] OpponentPets;

	private Image ImageLeftActor;

	private Text TextFightLeft;

	private Text TextWinsNumLeft;

	private Image ImageBloodActorLeft;

	private Text TextHPActorLeft;

	private Text TextNameLeft;

	private Image ImageRightActor;

	private Text TextFightRight;

	private Text TextWinsNumRight;

	private Text TextNameRight;

	private Image ImageBloodActorRight;

	private Text TextHPActorRight;

	private Image SelfHeadLeft;

	private Image SelfHeadRight;

	private Text SelfLvRight;

	private Text SelfLvLeft;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = false;
		this.alpha = 0.75f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageLeftActor = base.FindTransform("ImageLeftActor").GetComponent<Image>();
		this.ImageBloodActorLeft = base.FindTransform("ImageBloodActorLeft").GetComponent<Image>();
		this.TextHPActorLeft = base.FindTransform("TextHPActorLeft").GetComponent<Text>();
		this.ImageRightActor = base.FindTransform("ImageRightActor").GetComponent<Image>();
		this.ImageBloodActorRight = base.FindTransform("ImageBloodActorRight").GetComponent<Image>();
		this.TextHPActorRight = base.FindTransform("TextHPActorRight").GetComponent<Text>();
		this.TextNameLeft = base.FindTransform("TextNameLeft").GetComponent<Text>();
		this.TextNameRight = base.FindTransform("TextNameRight").GetComponent<Text>();
		this.TextFightLeft = base.FindTransform("TextFightLeft").GetComponent<Text>();
		this.TextFightRight = base.FindTransform("TextFightRight").GetComponent<Text>();
		this.TextWinsNumLeft = base.FindTransform("TextWinsNumLeft").GetComponent<Text>();
		this.TextWinsNumRight = base.FindTransform("TextWinsNumRight").GetComponent<Text>();
		this.SelfHeadLeft = base.FindTransform("SelfHeadLeft").GetComponent<Image>();
		this.SelfHeadRight = base.FindTransform("SelfHeadRight").GetComponent<Image>();
		this.SelfLvRight = base.FindTransform("SelfLvRight").GetComponent<Text>();
		this.SelfLvLeft = base.FindTransform("SelfLvLeft").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
		if (GangFightManager.Instance.gangFightBattleResult != null && GangFightManager.Instance.gangFightBattleResult.winnerId == EntityWorld.Instance.EntSelf.ID)
		{
			this.CloseAfterSecs(4);
		}
	}

	private void ResetLeft()
	{
		this.ImageBloodActorLeft.set_fillAmount((float)GangFightManager.Instance.gangFightMatchRoleSummary.selfCurrHp / (float)GangFightManager.Instance.gangFightMatchRoleSummary.selfMaxHp);
		this.TextHPActorLeft.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.selfCurrHp + "/" + GangFightManager.Instance.gangFightMatchRoleSummary.selfMaxHp);
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(EntityWorld.Instance.EntSelf.FixModelID);
		ResourceManager.SetSprite(this.ImageLeftActor, GameDataUtils.GetIcon(avatarModel.pic));
		this.TextNameLeft.set_text(EntityWorld.Instance.EntSelf.Name);
		this.TextFightLeft.set_text(EntityWorld.Instance.EntSelf.Fighting.ToString());
		this.TextWinsNumLeft.set_text(GangFightManager.Instance.combatWin + GameDataUtils.GetChineseContent(510108, false));
		ResourceManager.SetSprite(this.SelfHeadLeft, GameDataUtils.GetIcon(avatarModel.icon));
		this.SelfLvLeft.set_text(EntityWorld.Instance.EntSelf.Lv.ToString());
	}

	private void ResetRight()
	{
		this.TextHPActorRight.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.currHp + "/" + GangFightManager.Instance.gangFightMatchRoleSummary.maxHp);
		this.ImageBloodActorRight.set_fillAmount((float)GangFightManager.Instance.gangFightMatchRoleSummary.currHp / (float)GangFightManager.Instance.gangFightMatchRoleSummary.maxHp);
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(GangFightManager.Instance.gangFightMatchRoleSummary.modelId);
		ResourceManager.SetSprite(this.ImageRightActor, GameDataUtils.GetIcon(avatarModel.pic));
		this.TextNameRight.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.name);
		this.TextFightRight.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.fighting.ToString());
		this.TextWinsNumRight.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.combatWin + GameDataUtils.GetChineseContent(510108, false));
		ResourceManager.SetSprite(this.SelfHeadRight, GameDataUtils.GetIcon(avatarModel.icon));
		this.SelfLvRight.set_text(GangFightManager.Instance.gangFightMatchRoleSummary.lv.ToString());
	}

	public void RefreshUI()
	{
		this.ResetWinTimesNum(GangFightManager.Instance.combatWin, GangFightManager.Instance.gangFightMatchRoleSummary.combatWin);
		this.ResetLeft();
		this.ResetRight();
	}

	public void CloseAfterSecs(int secs)
	{
	}

	private void ResetWinTimesNum(int leftNum, int rightNum)
	{
	}
}
