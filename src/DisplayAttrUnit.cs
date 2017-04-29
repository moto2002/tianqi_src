using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAttrUnit : MonoBehaviour
{
	protected Transform DisplayAttrUnitTween;

	protected BaseTweenPostion baseTweenPostion;

	protected BaseTweenAlphaBaseTime baseTweenAlphaBaseTime;

	protected Vector3 tweenPositionEnd = new Vector3(0f, 450f, 0f);

	protected Vector3 tweenPositionOffset = new Vector3(0f, 30f, 0f);

	protected Text DisplayAttrUnitText;

	private void Awake()
	{
		this.DisplayAttrUnitTween = base.get_transform().FindChild("DisplayAttrUnitOffest/DisplayAttrUnitTween");
		this.baseTweenAlphaBaseTime = this.DisplayAttrUnitTween.GetComponent<BaseTweenAlphaBaseTime>();
		this.baseTweenPostion = this.DisplayAttrUnitTween.GetComponent<BaseTweenPostion>();
		this.DisplayAttrUnitText = this.DisplayAttrUnitTween.GetChild(0).GetComponent<Text>();
	}

	private void OnDisable()
	{
		this.DisplayAttrUnitTween.set_localPosition(Vector3.get_zero());
	}

	public void MoveTo(int offset, string text, bool isEnd)
	{
		this.DisplayAttrUnitText.set_text(text);
		float num = this.tweenPositionEnd.y - this.tweenPositionOffset.y * (float)offset;
		float duration = num / 500f;
		this.baseTweenPostion.MoveTo(new Vector3(this.tweenPositionOffset.x, num, this.tweenPositionOffset.z), duration, delegate
		{
			DisplayAttrManager.Instance.MoveToEnd(this, isEnd);
		});
	}

	public void FadeOut()
	{
		this.baseTweenAlphaBaseTime.TweenAlpha(1f, 0f, 0f, 0.5f, delegate
		{
			DisplayAttrManager.Instance.FadeOutEnd(this);
		});
	}

	public void Reset()
	{
		this.baseTweenPostion.Reset(false, true);
		this.baseTweenAlphaBaseTime.Reset(false, 1f);
		DisplayAttrManager.Instance.DisplayEnd(base.get_gameObject());
	}
}
