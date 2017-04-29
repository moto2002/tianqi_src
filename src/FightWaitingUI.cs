using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FightWaitingUI : UIBase
{
	private Text TextWaiting;

	private string text = string.Empty;

	private int dotNum;

	private float timeCal;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextWaiting = base.FindTransform("TextWaiting").GetComponent<Text>();
		this.text = GameDataUtils.GetChineseContent(504999, false);
	}

	private void Update()
	{
		this.timeCal += Time.get_deltaTime();
		if (this.timeCal > 1f)
		{
			this.timeCal = 0f;
			if (this.dotNum == 0)
			{
				this.TextWaiting.set_text(this.text + ".");
			}
			else if (this.dotNum == 1)
			{
				this.TextWaiting.set_text(this.text + "..");
			}
			else if (this.dotNum == 2)
			{
				this.TextWaiting.set_text(this.text + "...");
			}
			else if (this.dotNum == 3)
			{
				this.TextWaiting.set_text(this.text + "....");
			}
			this.dotNum++;
			if (this.dotNum == 4)
			{
				this.dotNum = 0;
			}
		}
	}
}
