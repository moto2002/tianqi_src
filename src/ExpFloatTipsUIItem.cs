using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpFloatTipsUIItem : BaseUIBehaviour
{
	public bool Unused = true;

	protected Text TextShow;

	protected BaseTweenPostion baseTweenPostion;

	protected BaseTweenAlphaBaseTime baseTweenAlphaBaseTime;

	private void Awake()
	{
		this.TextShow = base.get_transform().FindChild("TextShow").GetComponent<Text>();
		this.baseTweenPostion = base.GetComponent<BaseTweenPostion>();
		this.baseTweenAlphaBaseTime = base.GetComponent<BaseTweenAlphaBaseTime>();
	}

	public void ShowText(string text, float duration, float delay)
	{
		base.get_gameObject().SetActive(true);
		base.get_transform().set_localPosition(Vector3.get_zero());
		this.Unused = false;
		this.TextShow.set_text(text);
		this.baseTweenPostion.MoveTo(new Vector3(0f, 56f, 0f), delay);
		this.baseTweenAlphaBaseTime.TweenAlpha(1f, 0f, delay, duration, delegate
		{
			base.get_gameObject().SetActive(false);
			this.Unused = true;
		});
	}
}
