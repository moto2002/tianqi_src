using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBuffUI : BaseUIBehaviour
{
	private Image mSpecialBuffIcon;

	private Text mTxSpecialBuffName;

	private Text mTxSpecialBuffCount;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void OnEnable()
	{
		this.RefreshUI(SpecialFightManager.Instance.CurBuffCount);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mSpecialBuffIcon = base.FindTransform("SpecialBuffIcon").GetComponent<Image>();
		this.mTxSpecialBuffName = base.FindTransform("SpecialBuffName").GetComponent<Text>();
		this.mTxSpecialBuffCount = base.FindTransform("SpecialBuffCount").GetComponent<Text>();
		base.FindTransform("SpecialBuff").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuff);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.BuySpecialBuffSuccess, new Callback(this.OnBuySpecialBuffSuccess));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.BuySpecialBuffSuccess, new Callback(this.OnBuySpecialBuffSuccess));
	}

	private void RefreshUI(int buffCount)
	{
		this.mTxSpecialBuffName.set_text(string.Empty);
		int key = Mathf.Max(1, buffCount);
		FZengYibuffPeiZhi fZengYibuffPeiZhi = DataReader<FZengYibuffPeiZhi>.Get(key);
		if (fZengYibuffPeiZhi != null)
		{
			ResourceManager.SetSprite(this.mSpecialBuffIcon, GameDataUtils.GetIcon(fZengYibuffPeiZhi.icon));
			this.mTxSpecialBuffName.set_text(GameDataUtils.GetChineseContent(fZengYibuffPeiZhi.buffName, false));
		}
		this.mTxSpecialBuffCount.set_text(buffCount.ToString());
	}

	private void OnBuySpecialBuffSuccess()
	{
		this.RefreshUI(SpecialFightManager.Instance.CurBuffCount);
	}

	private void OnClickBuff(GameObject go)
	{
		if (SpecialFightManager.Instance.CurBuffCount < 10)
		{
			UIManagerControl.Instance.OpenUI("BattleBuyBuffUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502323, false));
		}
	}
}
