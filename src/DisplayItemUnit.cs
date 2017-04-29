using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemUnit : MonoBehaviour
{
	private Transform mDisplayItemUnitTween;

	private BaseTweenPostion mBaseTweenPostion;

	private BaseTweenAlphaBaseTime mBaseTweenAlphaBaseTime;

	private Vector3 mTweenPositionEnd = new Vector3(0f, 380f, 0f);

	private Vector3 mTweenPositionOffset = new Vector3(0f, 40f, 0f);

	private Text mDisplayItemUnitText;

	private Image mDisplayItemUnitIcon;

	private void Awake()
	{
		this.mDisplayItemUnitTween = base.get_transform().FindChild("DisplayItemUnitOffest/DisplayItemUnitTween");
		this.mBaseTweenAlphaBaseTime = this.mDisplayItemUnitTween.GetComponent<BaseTweenAlphaBaseTime>();
		this.mBaseTweenPostion = this.mDisplayItemUnitTween.GetComponent<BaseTweenPostion>();
		this.mDisplayItemUnitText = this.mDisplayItemUnitTween.GetChild(0).GetComponent<Text>();
		this.mDisplayItemUnitIcon = this.mDisplayItemUnitTween.GetChild(1).GetComponent<Image>();
	}

	private void OnDisable()
	{
		if (this.mDisplayItemUnitTween != null)
		{
			this.mDisplayItemUnitTween.set_localPosition(Vector3.get_zero());
		}
	}

	public void MoveTo(int offset, int icon, string text, bool isEnd)
	{
		ResourceManager.SetSprite(this.mDisplayItemUnitIcon, GameDataUtils.GetItemIcon(icon));
		this.mDisplayItemUnitText.set_text(text);
		float num = this.mTweenPositionEnd.y - this.mTweenPositionOffset.y * (float)offset;
		float duration = num / 500f;
		this.mBaseTweenPostion.MoveTo(new Vector3(this.mTweenPositionOffset.x, num, this.mTweenPositionOffset.z), duration, delegate
		{
			DisplayItemManager.Instance.MoveToEnd(this, isEnd);
		});
	}

	public void FadeOut()
	{
		this.mBaseTweenAlphaBaseTime.TweenAlpha(1f, 0f, 0f, 0.5f, delegate
		{
			DisplayItemManager.Instance.FadeOutEnd(this);
		});
	}

	public void Reset()
	{
		this.mBaseTweenPostion.Reset(false, true);
		this.mBaseTweenAlphaBaseTime.Reset(false, 1f);
		DisplayItemManager.Instance.DisplayEnd(base.get_gameObject());
	}
}
