using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseTweenAlpha : MonoBehaviour
{
	private bool IncludeChildren = true;

	public float m_fSrcA;

	public float m_fDstA;

	public float m_fAmount = 0.8f;

	private Image[] m_listImage;

	private bool m_bNeedTween;

	private bool m_bAdd = true;

	private Action m_actionTweenEnd;

	public void TweenAlpha(Transform tran, float srcA, float dstA, bool includeChildren = false, Action tweenEnd = null)
	{
		this.IncludeChildren = includeChildren;
		if (this.IncludeChildren)
		{
			this.m_listImage = tran.GetComponentsInChildren<Image>(true);
		}
		else
		{
			this.m_listImage = tran.GetComponents<Image>();
		}
		this.m_fSrcA = srcA;
		this.m_fDstA = dstA;
		this.m_actionTweenEnd = tweenEnd;
		if (dstA > srcA)
		{
			this.m_bAdd = true;
			this.m_bNeedTween = true;
		}
		else if (dstA < srcA)
		{
			this.m_bAdd = false;
			this.m_bNeedTween = true;
		}
		else
		{
			this.m_bNeedTween = false;
		}
	}

	private void Update()
	{
		if (this.m_bNeedTween)
		{
			if (this.m_bAdd)
			{
				this.m_fSrcA += Time.get_deltaTime() * this.m_fAmount;
				if (this.m_fSrcA >= this.m_fDstA)
				{
					this.m_fSrcA = this.m_fDstA;
					this.m_bNeedTween = false;
					this.DoAction();
				}
			}
			else
			{
				this.m_fSrcA -= Time.get_deltaTime() * this.m_fAmount;
				if (this.m_fSrcA <= this.m_fDstA)
				{
					this.m_fSrcA = this.m_fDstA;
					this.m_bNeedTween = false;
					this.DoAction();
				}
			}
			this.SetAlpha(this.m_fSrcA);
		}
	}

	private void DoAction()
	{
		if (this.m_actionTweenEnd != null)
		{
			this.m_actionTweenEnd.Invoke();
			this.m_actionTweenEnd = null;
		}
	}

	public void SetAlpha(Transform tran, float a, bool includeChildren = false)
	{
		this.IncludeChildren = includeChildren;
		if (this.IncludeChildren)
		{
			this.m_listImage = tran.GetComponentsInChildren<Image>(true);
		}
		else
		{
			this.m_listImage = tran.GetComponents<Image>();
		}
		this.SetAlpha(a);
	}

	private void SetAlpha(float a)
	{
		for (int i = 0; i < this.m_listImage.Length; i++)
		{
			if (this.m_listImage[i] != null)
			{
				Image image = this.m_listImage[i];
				image.set_color(new Color(image.get_color().r, image.get_color().g, image.get_color().b, a));
			}
		}
	}
}
