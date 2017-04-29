using System;
using UnityEngine;

public class BaseTweenScale : MonoBehaviour
{
	private bool isAnimating;

	private Vector3 dstScale;

	private float durationTime;

	private float timePass;

	private Vector3 veloCityScaleChange = Vector3.get_zero();

	private Action finishCallBack;

	private Transform rectTransform;

	private void Awake()
	{
		this.rectTransform = base.GetComponent<Transform>();
	}

	private void Update()
	{
		if (this.isAnimating)
		{
			this.timePass += Time.get_deltaTime();
			if (this.timePass <= this.durationTime)
			{
				Vector3 localScale = this.rectTransform.get_localScale();
				localScale.x += Time.get_deltaTime() * this.veloCityScaleChange.x;
				localScale.y += Time.get_deltaTime() * this.veloCityScaleChange.y;
				localScale.z += Time.get_deltaTime() * this.veloCityScaleChange.z;
				this.rectTransform.set_localScale(localScale);
			}
			else
			{
				this.isAnimating = false;
				this.rectTransform.set_localScale(this.dstScale);
				if (this.finishCallBack != null)
				{
					this.finishCallBack.Invoke();
				}
			}
		}
	}

	public void ChangeScaleTo(Vector3 scale, float duration)
	{
		this.ResetAll();
		this.dstScale = scale;
		this.durationTime = duration;
		Vector3 localScale = this.rectTransform.get_localScale();
		this.veloCityScaleChange.x = (this.dstScale.x - localScale.x) / duration;
		this.veloCityScaleChange.y = (this.dstScale.y - localScale.y) / duration;
		this.veloCityScaleChange.z = (this.dstScale.z - localScale.z) / duration;
	}

	public void ChangeScaleTo(Vector3 dstPosition, float duration, Action callback)
	{
		this.finishCallBack = callback;
		this.ChangeScaleTo(dstPosition, duration);
	}

	private void ResetAll()
	{
		this.timePass = 0f;
		this.isAnimating = true;
	}
}
