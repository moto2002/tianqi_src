using System;
using UnityEngine;

public class BaseTweenChangeSize : MonoBehaviour
{
	private bool isAnimating;

	private Vector2 m_dstSize;

	private float durationTime;

	private float timePass;

	private Vector2 velocityVector = default(Vector2);

	private Action finishCallBack;

	private RectTransform rectTransform;

	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (this.isAnimating)
		{
			this.timePass += Time.get_deltaTime();
			if (this.timePass <= this.durationTime)
			{
				Vector2 sizeDelta = this.rectTransform.get_sizeDelta();
				sizeDelta.x += Time.get_deltaTime() * this.velocityVector.x;
				sizeDelta.y += Time.get_deltaTime() * this.velocityVector.y;
				this.rectTransform.set_sizeDelta(sizeDelta);
			}
			else
			{
				this.isAnimating = false;
				this.rectTransform.set_sizeDelta(this.m_dstSize);
				if (this.finishCallBack != null)
				{
					this.finishCallBack.Invoke();
					this.finishCallBack = null;
				}
			}
		}
	}

	public void ChangeTo(Vector2 dstSize, float duration)
	{
		this.ResetAll();
		this.m_dstSize = dstSize;
		this.durationTime = duration;
		this.velocityVector.x = (dstSize.x - this.rectTransform.get_sizeDelta().x) / duration;
		this.velocityVector.y = (dstSize.y - this.rectTransform.get_sizeDelta().y) / duration;
	}

	public void ChangeTo(Vector3 dstPosition, float duration, Action callback)
	{
		this.finishCallBack = callback;
		this.ChangeTo(dstPosition, duration);
	}

	private void ResetAll()
	{
		this.timePass = 0f;
		this.isAnimating = true;
	}
}
