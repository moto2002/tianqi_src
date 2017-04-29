using System;
using UnityEngine;

public class BaseTweenAlphaBaseTime : MonoBehaviour
{
	private bool isAnimating;

	private float alphaDst;

	private float durationTime;

	private float delayTime;

	private float timePass;

	private float velocityAlpha;

	private CanvasGroup canvasGroup;

	private Action actionDone;

	private void Awake()
	{
		this.canvasGroup = base.get_gameObject().AddMissingComponent<CanvasGroup>();
	}

	private void Update()
	{
		if (this.isAnimating)
		{
			this.timePass += Time.get_deltaTime();
			if (this.timePass >= this.delayTime)
			{
				if (this.timePass - this.delayTime <= this.durationTime)
				{
					float num = this.canvasGroup.get_alpha() - this.velocityAlpha * Time.get_deltaTime();
					if (num < 0f)
					{
						num = 0f;
					}
					else if (num > 1f)
					{
						num = 1f;
					}
					this.canvasGroup.set_alpha(num);
				}
				else
				{
					this.isAnimating = false;
					this.canvasGroup.set_alpha(this.alphaDst);
					if (this.actionDone != null)
					{
						this.actionDone.Invoke();
					}
				}
			}
		}
	}

	public void SetAlpha(float alpha)
	{
		this.canvasGroup.set_alpha(alpha);
	}

	public void TweenAlpha(float srcAlhpa, float dstAlpha, float delay, float duration, Action actionFinish)
	{
		this.actionDone = actionFinish;
		this.TweenAlpha(srcAlhpa, dstAlpha, delay, duration);
	}

	public void TweenAlpha(float srcAlhpa, float dstAlpha, float delay, float duration)
	{
		this.canvasGroup.set_alpha(srcAlhpa);
		this.delayTime = delay;
		this.timePass = 0f;
		this.durationTime = duration;
		this.alphaDst = dstAlpha;
		this.isAnimating = true;
		this.velocityAlpha = (srcAlhpa - dstAlpha) / duration;
	}

	public void TweenAlphaToDst(float dstAlpha, float delay, float duration)
	{
		this.TweenAlpha(this.canvasGroup.get_alpha(), dstAlpha, delay, duration);
	}

	public void Reset(bool animOn = false, float alpha = 1f)
	{
		this.timePass = 0f;
		this.canvasGroup.set_alpha(alpha);
		this.isAnimating = animOn;
	}
}
