using Foundation.Core;
using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIBehaviour : BaseEventListener
{
	protected bool m_bNameUnique;

	protected bool m_bInited;

	protected Transform m_myTransform;

	private Dictionary<string, string> m_widgetToFullName;

	protected void AwakeBase(BindingContext.BindingContextMode bcm = BindingContext.BindingContextMode.MonoBinding, bool nameUnique = false)
	{
		if (!this.m_bInited)
		{
			this.m_bInited = true;
			this.m_bNameUnique = nameUnique;
			this.DoWidgetToFullName();
			this.InitUI();
			if (bcm != BindingContext.BindingContextMode.None)
			{
				this.DataBinding();
				this.EventsBinding();
				this.BindingContextBinding(bcm);
			}
			base.AddListenersWhenAwake();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	protected void ResetParent()
	{
		this.m_bInited = false;
	}

	public Transform FindTransform(string transformName)
	{
		if (this.m_widgetToFullName == null)
		{
			this.DoWidgetToFullName();
		}
		Transform transform = null;
		if (this.m_widgetToFullName.ContainsKey(transformName))
		{
			transform = this.m_myTransform.Find(this.m_widgetToFullName.get_Item(transformName));
		}
		if (transform == null && this.m_myTransform.get_name() == transformName)
		{
			transform = this.m_myTransform;
		}
		return transform;
	}

	public void FillTransform2Editor(Transform rootTransform)
	{
		this.m_myTransform = rootTransform;
		this.JustDoWidgetToFullName();
	}

	protected virtual void InitUI()
	{
	}

	protected virtual void DataBinding()
	{
	}

	protected virtual void EventsBinding()
	{
	}

	private void BindingContextBinding(BindingContext.BindingContextMode bcm)
	{
		if (bcm == BindingContext.BindingContextMode.None)
		{
			return;
		}
		BindingContext componentInParent = this.m_myTransform.GetComponentInParent<BindingContext>();
		if (componentInParent != null)
		{
			if (bcm == BindingContext.BindingContextMode.MonoBinding)
			{
				componentInParent.ViewModel = this.m_myTransform.GetComponentInParent<ViewModelBase>();
			}
			componentInParent.ContextMode = bcm;
			componentInParent.OnEnableSelf();
		}
	}

	private void DoWidgetToFullName()
	{
		if (this.m_myTransform == null)
		{
			this.m_myTransform = base.get_transform();
		}
		this.JustDoWidgetToFullName();
	}

	private void JustDoWidgetToFullName()
	{
		this.m_widgetToFullName = WidgetPathManager.FillFullNameData(base.GetType().get_Name(), this.m_myTransform, this.m_bNameUnique);
	}
}
