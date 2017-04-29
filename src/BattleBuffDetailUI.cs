using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class BattleBuffDetailUI : UIBase
{
	protected Text BattleBuffDetailUITitleText;

	protected Image BattleBuffDetailUIBuffInfoIconFG;

	protected Text BattleBuffDetailUIBuffInfoName;

	protected Text BattleBuffDetailUIBuffInfoEffectText;

	protected Text BattleBuffDetailUIDescText;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BattleBuffDetailUITitleText = base.FindTransform("BattleBuffDetailUITitleText").GetComponent<Text>();
		this.BattleBuffDetailUIBuffInfoIconFG = base.FindTransform("BattleBuffDetailUIBuffInfoIconFG").GetComponent<Image>();
		this.BattleBuffDetailUIBuffInfoName = base.FindTransform("BattleBuffDetailUIBuffInfoName").GetComponent<Text>();
		this.BattleBuffDetailUIBuffInfoEffectText = base.FindTransform("BattleBuffDetailUIBuffInfoEffectText").GetComponent<Text>();
		this.BattleBuffDetailUIDescText = base.FindTransform("BattleBuffDetailUIDescText").GetComponent<Text>();
		base.FindTransform("BattleBuffDetailUICloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
	}

	public void SetData(int iconID, int titleID, int nameID, string effectText, int descID)
	{
		ResourceManager.SetSprite(this.BattleBuffDetailUIBuffInfoIconFG, GameDataUtils.GetIcon(iconID));
		this.BattleBuffDetailUITitleText.set_text(GameDataUtils.GetChineseContent(titleID, false));
		this.BattleBuffDetailUIBuffInfoName.set_text(GameDataUtils.GetChineseContent(titleID, false));
		this.BattleBuffDetailUIBuffInfoEffectText.set_text(effectText);
		this.BattleBuffDetailUIDescText.set_text(GameDataUtils.GetChineseContent(titleID, false));
	}
}
