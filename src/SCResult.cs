using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SCResult : UIBase
{
	private Text lastBatchText;

	private Text currBatchText;

	private Text expText;

	private Text goldText;

	private Transform ItemConten;

	private ButtonCustom BtnConfirm;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		InstanceManager.StopAllClientAI(true);
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.lastBatchText = base.FindTransform("Start").GetComponent<Text>();
		this.currBatchText = base.FindTransform("End").GetComponent<Text>();
		this.expText = base.FindTransform("ExpNum").GetComponent<Text>();
		this.goldText = base.FindTransform("GoldNum").GetComponent<Text>();
		this.ItemConten = base.FindTransform("ItemContent");
		this.BtnConfirm = base.FindTransform("BtnExit").GetComponent<ButtonCustom>();
		this.BtnConfirm.get_transform().FindChild("BtnExitName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505114, false));
		ButtonCustom expr_B5 = this.BtnConfirm;
		expr_B5.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_B5.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickConfirm));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		Utils.WinSetting(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Utils.WinSetting(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void UpdateSCWinUI(SecretAreaChallengeResultNty result)
	{
		base.FindTransform("BtnStatisticsNode").get_gameObject().SetActive(true);
		this.lastBatchText.set_text(string.Format(GameDataUtils.GetChineseContent(512053, false), result.startStage));
		this.currBatchText.set_text(string.Format(GameDataUtils.GetChineseContent(512054, false), result.endStage));
		for (int i = 0; i < this.ItemConten.get_childCount(); i++)
		{
			Object.Destroy(this.ItemConten.GetChild(i).get_gameObject());
		}
		for (int j = 0; j < result.copyRewards.get_Count(); j++)
		{
			ItemBriefInfo itemBriefInfo = result.copyRewards.get_Item(j);
			if (itemBriefInfo.cfgId == 1)
			{
				this.expText.set_text(itemBriefInfo.count.ToString());
			}
			else if (itemBriefInfo.cfgId == 2)
			{
				this.goldText.set_text(itemBriefInfo.count.ToString());
			}
			else
			{
				ItemShow.ShowItem(this.ItemConten, itemBriefInfo.cfgId, itemBriefInfo.count, false, null, 2001);
			}
		}
	}

	private void OnClickConfirm(GameObject go)
	{
		NetworkManager.Send(new ExitSecretAreaChallengeReq(), ServerType.Data);
	}
}
