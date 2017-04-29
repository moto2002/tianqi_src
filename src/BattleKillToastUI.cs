using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class BattleKillToastUI : UIBase
{
	private Text TextContent;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isInterruptStick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextContent = base.get_transform().FindChild("TextContent").GetComponent<Text>();
	}

	public void ShowText(string text, float delay)
	{
		float duration = 1f;
		this.TextContent.set_text(text);
		BaseTweenAlphaBaseTime bta = base.get_transform().GetComponent<BaseTweenAlphaBaseTime>();
		bta.TweenAlpha(0f, 1f, 0f, 0.2f, delegate
		{
			bta.TweenAlpha(1f, 0f, delay, duration, delegate
			{
				this.Show(false);
			});
		});
	}
}
