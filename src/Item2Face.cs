using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Item2Face : BaseUIBehaviour
{
	private ImageSequenceFrames m_spFaceIcon;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spFaceIcon = base.FindTransform("FaceIcon").GetComponent<ImageSequenceFrames>();
		this.ResetAll();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public void SetFaces(List<string> listAnimate)
	{
		this.m_spFaceIcon.IsAnimating = false;
		this.m_spFaceIcon.PlayAnimation2Loop(listAnimate, 0.166666672f);
	}

	public void SetFaceSize(int size)
	{
		(this.m_spFaceIcon.get_transform() as RectTransform).set_sizeDelta(new Vector2((float)size, (float)size));
	}

	public void ResetAll()
	{
		ResourceManager.SetSprite(this.m_spFaceIcon, ResourceManagerBase.GetNullSprite());
	}
}
