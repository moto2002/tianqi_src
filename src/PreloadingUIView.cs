using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreloadingUIView : UIBase
{
	private static PreloadingUIView m_instance;

	private Transform Point1;

	private Slider m_spProgressBg;

	private Text m_lblProgressText;

	private Text m_speed;

	private Text m_downTime;

	private string Tips = string.Empty;

	private bool IsInSmooth;

	private float SmoothPercent;

	public static PreloadingUIView Instance
	{
		get
		{
			return PreloadingUIView.m_instance;
		}
	}

	private void Awake()
	{
		PreloadingUIView.m_instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.Point1 = base.FindTransform("point1");
		this.m_spProgressBg = base.FindTransform("Slider").GetComponent<Slider>();
		this.m_lblProgressText = base.FindTransform("ProgressText").GetComponent<Text>();
		this.m_speed = base.FindTransform("speed").GetComponent<Text>();
		this.m_downTime = base.FindTransform("downTime").GetComponent<Text>();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		this.DeleteSpine();
		PreloadingUIView.m_instance = null;
		base.ReleaseSelf(true);
	}

	public static void Close()
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.Show(false);
		}
	}

	public static void SetSpeed(string sp)
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoSetSpeed(sp);
		}
	}

	private void DoSetSpeed(string sp)
	{
		if (this.m_speed != null)
		{
			this.m_speed.set_text(sp);
		}
	}

	public static void SetDownTime(string time)
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoSetDownTime(time);
		}
	}

	private void DoSetDownTime(string time)
	{
		if (this.m_downTime != null)
		{
			this.m_downTime.set_text(time);
		}
	}

	public static void SetProgressName(string name)
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoSetProgressName(name);
		}
	}

	private void DoSetProgressName(string name)
	{
		if (this.m_lblProgressText != null)
		{
			this.Tips = name;
			this.m_lblProgressText.set_text(this.Tips);
		}
	}

	public static void ResetProgress()
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoResetProgress();
		}
	}

	private void DoResetProgress()
	{
		this.SmoothPercent = 0f;
		this.m_spProgressBg.set_value(0f);
	}

	public static void SetProgress(float percent)
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoSetProgress(percent);
		}
	}

	private void DoSetProgress(float percent)
	{
		this.IsInSmooth = false;
		if (this.m_spProgressBg != null)
		{
			if (percent > this.m_spProgressBg.get_value())
			{
				this.m_spProgressBg.set_value(percent);
			}
			else if (percent < this.m_spProgressBg.get_value())
			{
				Debug.LogError("DoSetProgress, percent illegal");
			}
		}
	}

	public static void SetProgressInSmooth(float percent)
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoSetProgressInSmooth(percent);
		}
	}

	private void DoSetProgressInSmooth(float percent)
	{
		if (percent < 1f)
		{
			this.IsInSmooth = true;
			if (percent > this.SmoothPercent)
			{
				this.SmoothPercent = percent;
			}
			else if (percent < this.SmoothPercent)
			{
				Debug.LogError("DoSetProgressInSmooth, percent illegal");
			}
		}
		else
		{
			this.DoSetProgress(percent);
		}
	}

	private void Update()
	{
		if (this.IsInSmooth && this.m_spProgressBg != null)
		{
			Slider expr_22 = this.m_spProgressBg;
			expr_22.set_value(expr_22.get_value() + (this.SmoothPercent - this.m_spProgressBg.get_value()) * 0.1f);
		}
	}

	public static void PlayProgressFX()
	{
		if (PreloadingUIView.Instance != null)
		{
			PreloadingUIView.Instance.DoPlayProgressFX();
		}
	}

	private void DoPlayProgressFX()
	{
		ImageSequenceFrames component = base.FindTransform("LoadingFX").GetComponent<ImageSequenceFrames>();
		component.IsAnimating = false;
		List<string> list = new List<string>();
		for (int i = 0; i <= 24; i++)
		{
			list.Add("cat000" + i.ToString("D2"));
		}
		component.Scale = 1f;
		component.PlayAnimation2Loop(list, 0.0001f);
	}

	private void DeleteSpine()
	{
	}
}
