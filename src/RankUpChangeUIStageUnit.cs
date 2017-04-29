using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankUpChangeUIStageUnit : BaseUIBehaviour
{
	protected Text RankUpChangeUIStageUnitTitleText;

	protected GameObject RankUpChangeUIStageUnitTask0;

	protected GameObject RankUpChangeUIStageUnitTask0Sign;

	protected Text RankUpChangeUIStageUnitTask0Text;

	protected GameObject RankUpChangeUIStageUnitTask0Tick;

	protected GameObject RankUpChangeUIStageUnitTask1;

	protected GameObject RankUpChangeUIStageUnitTask1Sign;

	protected Text RankUpChangeUIStageUnitTask1Text;

	protected GameObject RankUpChangeUIStageUnitTask1Tick;

	protected GameObject RankUpChangeUIStageUnitTask2;

	protected GameObject RankUpChangeUIStageUnitTask2Sign;

	protected Text RankUpChangeUIStageUnitTask2Text;

	protected GameObject RankUpChangeUIStageUnitTask2Tick;

	protected Vector3 RankUpChangeUIStageUnitTaskSlot0;

	protected Vector3 RankUpChangeUIStageUnitTaskSlot1;

	protected Vector3 RankUpChangeUIStageUnitTaskSlot2;

	protected Vector3 RankUpChangeUIStageUnitTaskSlot3;

	protected Vector3 RankUpChangeUIStageUnitTaskSlot4;

	protected Image RankUpChangeUIStageUnitTaskSign;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.RankUpChangeUIStageUnitTitleText = base.FindTransform("RankUpChangeUIStageUnitTitleText").GetComponent<Text>();
		this.RankUpChangeUIStageUnitTask0 = base.FindTransform("RankUpChangeUIStageUnitTask0").get_gameObject();
		this.RankUpChangeUIStageUnitTask0Sign = base.FindTransform("RankUpChangeUIStageUnitTask0Sign").get_gameObject();
		this.RankUpChangeUIStageUnitTask0Text = base.FindTransform("RankUpChangeUIStageUnitTask0Text").GetComponent<Text>();
		this.RankUpChangeUIStageUnitTask0Tick = base.FindTransform("RankUpChangeUIStageUnitTask0Tick").get_gameObject();
		this.RankUpChangeUIStageUnitTask1 = base.FindTransform("RankUpChangeUIStageUnitTask1").get_gameObject();
		this.RankUpChangeUIStageUnitTask1Sign = base.FindTransform("RankUpChangeUIStageUnitTask1Sign").get_gameObject();
		this.RankUpChangeUIStageUnitTask1Text = base.FindTransform("RankUpChangeUIStageUnitTask1Text").GetComponent<Text>();
		this.RankUpChangeUIStageUnitTask1Tick = base.FindTransform("RankUpChangeUIStageUnitTask1Tick").get_gameObject();
		this.RankUpChangeUIStageUnitTask2 = base.FindTransform("RankUpChangeUIStageUnitTask2").get_gameObject();
		this.RankUpChangeUIStageUnitTask2Sign = base.FindTransform("RankUpChangeUIStageUnitTask2Sign").get_gameObject();
		this.RankUpChangeUIStageUnitTask2Text = base.FindTransform("RankUpChangeUIStageUnitTask2Text").GetComponent<Text>();
		this.RankUpChangeUIStageUnitTask2Tick = base.FindTransform("RankUpChangeUIStageUnitTask2Tick").get_gameObject();
		this.RankUpChangeUIStageUnitTaskSlot0 = base.FindTransform("RankUpChangeUIStageUnitTaskSlot0").get_localPosition();
		this.RankUpChangeUIStageUnitTaskSlot1 = base.FindTransform("RankUpChangeUIStageUnitTaskSlot1").get_localPosition();
		this.RankUpChangeUIStageUnitTaskSlot2 = base.FindTransform("RankUpChangeUIStageUnitTaskSlot2").get_localPosition();
		this.RankUpChangeUIStageUnitTaskSlot3 = base.FindTransform("RankUpChangeUIStageUnitTaskSlot3").get_localPosition();
		this.RankUpChangeUIStageUnitTaskSlot4 = base.FindTransform("RankUpChangeUIStageUnitTaskSlot4").get_localPosition();
		this.RankUpChangeUIStageUnitTaskSign = base.FindTransform("RankUpChangeUIStageUnitTaskSign").GetComponent<Image>();
	}

	public void SetData(int title, XDict<int, bool> conditionInfo, RankUpChangeStageState state)
	{
		this.SetTitle(title);
		this.SetCondition(conditionInfo);
		this.SetState(state);
	}

	protected void SetTitle(int title)
	{
		this.RankUpChangeUIStageUnitTitleText.set_text(GameDataUtils.GetChineseContent(title, false));
	}

	protected void SetCondition(XDict<int, bool> conditionInfo)
	{
		if (conditionInfo == null || conditionInfo.Count == 0)
		{
			if (this.RankUpChangeUIStageUnitTask0.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask1.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask0Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask1Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask0Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0Tick.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask1Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1Tick.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Tick.SetActive(false);
			}
		}
		else if (conditionInfo.Count >= 3)
		{
			if (!this.RankUpChangeUIStageUnitTask0.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0.SetActive(true);
			}
			if (!this.RankUpChangeUIStageUnitTask1.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1.SetActive(true);
			}
			if (!this.RankUpChangeUIStageUnitTask2.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2.SetActive(true);
			}
			this.RankUpChangeUIStageUnitTask0.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot0);
			this.RankUpChangeUIStageUnitTask1.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot1);
			this.RankUpChangeUIStageUnitTask2.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot2);
			this.RankUpChangeUIStageUnitTask0Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(0), false));
			this.RankUpChangeUIStageUnitTask1Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(1), false));
			this.RankUpChangeUIStageUnitTask2Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(2), false));
			if (this.RankUpChangeUIStageUnitTask0Sign.get_activeSelf() == conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Sign.SetActive(!conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Sign.get_activeSelf() == conditionInfo.ElementValueAt(1))
			{
				this.RankUpChangeUIStageUnitTask1Sign.SetActive(!conditionInfo.ElementValueAt(1));
			}
			if (this.RankUpChangeUIStageUnitTask2Sign.get_activeSelf() == conditionInfo.ElementValueAt(2))
			{
				this.RankUpChangeUIStageUnitTask2Sign.SetActive(!conditionInfo.ElementValueAt(2));
			}
			if (this.RankUpChangeUIStageUnitTask0Tick.get_activeSelf() != conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Tick.SetActive(conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Tick.get_activeSelf() != conditionInfo.ElementValueAt(1))
			{
				this.RankUpChangeUIStageUnitTask1Tick.SetActive(conditionInfo.ElementValueAt(1));
			}
			if (this.RankUpChangeUIStageUnitTask2Tick.get_activeSelf() != conditionInfo.ElementValueAt(2))
			{
				this.RankUpChangeUIStageUnitTask2Tick.SetActive(conditionInfo.ElementValueAt(2));
			}
		}
		else if (conditionInfo.Count == 2)
		{
			if (!this.RankUpChangeUIStageUnitTask0.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0.SetActive(true);
			}
			if (!this.RankUpChangeUIStageUnitTask1.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1.SetActive(true);
			}
			if (this.RankUpChangeUIStageUnitTask2.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2.SetActive(false);
			}
			this.RankUpChangeUIStageUnitTask0.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot3);
			this.RankUpChangeUIStageUnitTask1.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot4);
			this.RankUpChangeUIStageUnitTask0Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(0), false));
			this.RankUpChangeUIStageUnitTask1Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(1), false));
			if (this.RankUpChangeUIStageUnitTask0Sign.get_activeSelf() == conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Sign.SetActive(!conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Sign.get_activeSelf() == conditionInfo.ElementValueAt(1))
			{
				this.RankUpChangeUIStageUnitTask1Sign.SetActive(!conditionInfo.ElementValueAt(1));
			}
			if (this.RankUpChangeUIStageUnitTask2Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask0Tick.get_activeSelf() != conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Tick.SetActive(conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Tick.get_activeSelf() != conditionInfo.ElementValueAt(1))
			{
				this.RankUpChangeUIStageUnitTask1Tick.SetActive(conditionInfo.ElementValueAt(1));
			}
			if (this.RankUpChangeUIStageUnitTask2Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Tick.SetActive(false);
			}
		}
		else if (conditionInfo.Count == 1)
		{
			if (!this.RankUpChangeUIStageUnitTask0.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask0.SetActive(true);
			}
			if (this.RankUpChangeUIStageUnitTask1.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2.SetActive(false);
			}
			this.RankUpChangeUIStageUnitTask0.get_transform().set_localPosition(this.RankUpChangeUIStageUnitTaskSlot1);
			this.RankUpChangeUIStageUnitTask0Text.set_text(GameDataUtils.GetChineseContent(conditionInfo.ElementKeyAt(0), false));
			if (this.RankUpChangeUIStageUnitTask0Sign.get_activeSelf() == conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Sign.SetActive(!conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2Sign.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Sign.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask0Tick.get_activeSelf() != conditionInfo.ElementValueAt(0))
			{
				this.RankUpChangeUIStageUnitTask0Tick.SetActive(conditionInfo.ElementValueAt(0));
			}
			if (this.RankUpChangeUIStageUnitTask1Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask1Tick.SetActive(false);
			}
			if (this.RankUpChangeUIStageUnitTask2Tick.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTask2Tick.SetActive(false);
			}
		}
	}

	protected void SetState(RankUpChangeStageState state)
	{
		switch (state)
		{
		case RankUpChangeStageState.None:
			if (this.RankUpChangeUIStageUnitTaskSign.get_gameObject().get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTaskSign.get_gameObject().SetActive(false);
			}
			break;
		case RankUpChangeStageState.Done:
			if (!this.RankUpChangeUIStageUnitTaskSign.get_gameObject().get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTaskSign.get_gameObject().SetActive(true);
			}
			ResourceManager.SetSprite(this.RankUpChangeUIStageUnitTaskSign, ResourceManager.GetCodeSprite("zz_biaoqian_02"));
			break;
		case RankUpChangeStageState.Doing:
			if (!this.RankUpChangeUIStageUnitTaskSign.get_gameObject().get_activeSelf())
			{
				this.RankUpChangeUIStageUnitTaskSign.get_gameObject().SetActive(true);
			}
			ResourceManager.SetSprite(this.RankUpChangeUIStageUnitTaskSign, ResourceManager.GetCodeSprite("zz_biaoqian_01"));
			break;
		}
	}
}
