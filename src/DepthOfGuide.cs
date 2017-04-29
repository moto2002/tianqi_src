using System;
using UnityEngine;
using UnityEngine.UI;

public class DepthOfGuide : MonoBehaviour
{
	private bool m_isGuideOn;

	private void OnEnable()
	{
		EventDispatcher.AddListener("GuideManager.StepBegin", new Callback(this.OnStepBegin));
		EventDispatcher.AddListener("GuideManager.StepSuccess", new Callback(this.OnStepSuccess));
		EventDispatcher.AddListener("GuideManager.BroadOfEndGuide", new Callback(this.StopGuide));
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveListener("GuideManager.StepBegin", new Callback(this.OnStepBegin));
		EventDispatcher.RemoveListener("GuideManager.StepSuccess", new Callback(this.OnStepSuccess));
		EventDispatcher.RemoveListener("GuideManager.BroadOfEndGuide", new Callback(this.StopGuide));
		this.DestroyGuide();
	}

	private void OnDestroy()
	{
		this.DestroyGuide();
	}

	private void OnStepBegin()
	{
		if (this != null)
		{
			GraphicRaycaster component = base.GetComponent<GraphicRaycaster>();
			if (component != null)
			{
				component.set_enabled(true);
			}
		}
	}

	private void OnStepSuccess()
	{
		if (!this.m_isGuideOn && this != null)
		{
			GraphicRaycaster component = base.GetComponent<GraphicRaycaster>();
			if (component != null)
			{
				component.set_enabled(false);
			}
		}
	}

	private void StopGuide()
	{
		if (this != null)
		{
			GraphicRaycaster component = base.GetComponent<GraphicRaycaster>();
			if (component != null)
			{
				component.set_enabled(true);
			}
		}
	}

	public void GuideOn()
	{
		this.m_isGuideOn = true;
		LayerSystem.SetGameObjectLayer(base.get_gameObject(), "UI", 1);
		DepthManager.SetGraphicRaycaster(base.get_gameObject());
		DepthManager.SetDepth(base.get_gameObject(), 10002);
		Canvas component = base.GetComponent<Canvas>();
		if (component != null)
		{
			component.set_overrideSorting(true);
			component.set_enabled(true);
		}
	}

	public void GuideOff()
	{
		this.m_isGuideOn = false;
		if (this != null && base.get_gameObject() != null)
		{
			this.JustDestroyGuide();
		}
	}

	public void DestroyGuide()
	{
		if (this == null || base.get_gameObject() == null)
		{
			return;
		}
		if (this.m_isGuideOn)
		{
			return;
		}
		this.GuideOff();
		this.JustDestroyGuide();
	}

	public void JustDestroyGuide()
	{
		if (this == null || base.get_gameObject() == null)
		{
			return;
		}
		Object.DestroyImmediate(base.get_gameObject().GetComponent<GraphicRaycaster>());
		Object.DestroyImmediate(base.get_gameObject().GetComponent<Canvas>());
		base.set_enabled(false);
	}

	public void PauseGuideSystem(bool isPause)
	{
		if (isPause)
		{
			DepthManager.SetDepth(base.get_gameObject(), 0);
		}
		else
		{
			DepthManager.SetDepth(base.get_gameObject(), 10002);
		}
	}
}
