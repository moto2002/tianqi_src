using System;
using UnityEngine;

public class BaseTweenPostion : MonoBehaviour
{
	private bool isAnimating;

	private Vector3 dstPos;

	private float durationTime;

	private float timePass;

	private Vector3 veloCityVector = default(Vector3);

	private Action finishCallBack;

	private RectTransform rectTransform;

	public bool IsAnimating
	{
		get
		{
			return this.isAnimating;
		}
		set
		{
			this.isAnimating = value;
		}
	}

	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (this.IsAnimating)
		{
			this.timePass += Time.get_deltaTime();
			if (this.timePass <= this.durationTime)
			{
				Vector3 localPosition = this.rectTransform.get_localPosition();
				localPosition.x += Time.get_deltaTime() * this.veloCityVector.x;
				localPosition.y += Time.get_deltaTime() * this.veloCityVector.y;
				localPosition.z += Time.get_deltaTime() * this.veloCityVector.z;
				this.rectTransform.set_localPosition(localPosition);
			}
			else
			{
				this.IsAnimating = false;
				this.rectTransform.set_localPosition(this.dstPos);
				if (this.finishCallBack != null)
				{
					this.finishCallBack.Invoke();
				}
			}
		}
	}

	public void MoveTo(Vector3 dstPosition, float duration)
	{
		this.Reset(true, false);
		this.dstPos = dstPosition;
		this.durationTime = duration;
		this.veloCityVector.x = (dstPosition.x - this.rectTransform.get_localPosition().x) / duration;
		this.veloCityVector.y = (dstPosition.y - this.rectTransform.get_localPosition().y) / duration;
		this.veloCityVector.z = (dstPosition.z - this.rectTransform.get_localPosition().z) / duration;
	}

	public void MoveTo(Vector3 dstPosition, float duration, Action callback)
	{
		this.finishCallBack = callback;
		this.MoveTo(dstPosition, duration);
	}

	public void Reset(bool animOn = false, bool isBackToLocalZero = false)
	{
		this.timePass = 0f;
		this.IsAnimating = animOn;
		if (isBackToLocalZero)
		{
			base.get_transform().set_localPosition(Vector3.get_zero());
		}
	}
}
