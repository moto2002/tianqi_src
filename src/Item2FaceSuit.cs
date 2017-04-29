using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item2FaceSuit : BaseUIBehaviour
{
	private ImageSequenceFrames m_spFaceIcon;

	private Action actionShow;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spFaceIcon = base.FindTransform("FaceIcon").GetComponent<ImageSequenceFrames>();
		this.ResetAll();
		base.get_gameObject().GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnClick));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public void SetFaces(List<string> listAnimate, Action actionClick)
	{
		this.actionShow = actionClick;
		this.m_spFaceIcon.IsAnimating = false;
		this.m_spFaceIcon.PlayAnimation2Loop(listAnimate, 0.166666672f);
	}

	public void SetFaceScale(float scale)
	{
		(this.m_spFaceIcon.get_transform() as RectTransform).set_localScale(new Vector3(scale, scale, 1f));
	}

	public void ResetAll()
	{
		ResourceManager.SetSprite(this.m_spFaceIcon, ResourceManagerBase.GetNullSprite());
	}

	private void OnBtnClick()
	{
		if (this.actionShow != null)
		{
			this.actionShow.Invoke();
		}
	}
}
