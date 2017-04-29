using Foundation.Core.Databinding;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class SystemOpenProgressUIView : BaseUIBehaviour
{
	public static SystemOpenProgressUIView Instance;

	private Image m_spSystemIcon;

	private Image m_spSystemIconName;

	private Text m_lblSystemDesc;

	private Text m_lblSystemLevel;

	public void AwakeSelf()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		SystemOpenProgressUIView.Instance = this;
		this.m_spSystemIcon = base.FindTransform("SystemIcon").GetComponent<Image>();
		this.m_spSystemIconName = base.FindTransform("SystemIconName").GetComponent<Image>();
		this.m_lblSystemDesc = base.FindTransform("SystemDesc").GetComponent<Text>();
		this.m_lblSystemLevel = base.FindTransform("SystemLevel").GetComponent<Text>();
		base.FindTransform("SystemOpenProgressUIBG").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonClick));
		SystemOpenProgressManager.Instance.Refresh();
	}

	protected void OnEnable()
	{
		SystemOpenProgressManager.Instance.Refresh();
	}

	private void OnButtonClick()
	{
		SystemOpenProgressManager.Instance.OpenSystemOpenDescUI();
	}

	public void SetSystemIcon(int iconId, int iconId2)
	{
		ResourceManager.SetSprite(this.m_spSystemIcon, GameDataUtils.GetIcon(iconId));
		this.m_spSystemIcon.SetNativeSize();
		if (iconId2 > 0)
		{
			base.FindTransform("SystemIconName").get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.m_spSystemIconName, GameDataUtils.GetIcon(iconId2));
			this.m_spSystemIconName.SetNativeSize();
		}
		else
		{
			base.FindTransform("SystemIconName").get_gameObject().SetActive(false);
		}
	}

	public void SetSystemDesc(string desc)
	{
		this.m_lblSystemDesc.set_text(desc);
	}

	public void SetSystemLevel(int level)
	{
		this.m_lblSystemLevel.set_text(string.Format("{0}级任务解锁", level));
	}
}
