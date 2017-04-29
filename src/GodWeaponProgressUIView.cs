using Foundation.Core.Databinding;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class GodWeaponProgressUIView : BaseUIBehaviour
{
	public static GodWeaponProgressUIView Instance;

	private Image m_spSystemIcon;

	private Slider m_spProgressFg;

	private Text m_lblSystemTip;

	private Text mTxProgressValue;

	public void AwakeSelf()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		GodWeaponProgressUIView.Instance = this;
		this.m_spSystemIcon = base.FindTransform("SystemIcon").GetComponent<Image>();
		this.m_spProgressFg = base.FindTransform("ProgressBg").GetComponent<Slider>();
		this.m_lblSystemTip = base.FindTransform("SystemTip").GetComponent<Text>();
		this.mTxProgressValue = base.FindTransform("ProgressText").GetComponent<Text>();
		base.FindTransform("GodWeaponProgressUIBG").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	protected void OnEnable()
	{
		GodWeaponProgressManager.Instance.Refresh();
	}

	private void OnButtonClick()
	{
		GodWeaponProgressManager.Instance.OpenGodWeaponUI();
	}

	public void SetSystemIcon(int iconId)
	{
		ResourceManager.SetSprite(this.m_spSystemIcon, GameDataUtils.GetIcon(iconId));
		this.m_spSystemIcon.SetNativeSize();
	}

	public void SetProgress(float progress)
	{
		if (progress > 1f)
		{
			progress = 1f;
		}
		this.m_spProgressFg.set_value(progress);
		this.mTxProgressValue.set_text(progress.ToString("P0"));
	}

	public void SetProgress(string num)
	{
	}

	public void SetSystemTip(string tip)
	{
		this.m_lblSystemTip.set_text(tip);
	}
}
