using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SystemOpenDescUIView : UIBase
{
	public static SystemOpenDescUIView Instance;

	private Image m_spDescIcon;

	private Text m_lblDescTip;

	private Text m_lblLockTip1;

	private Text m_lblLockTip2;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		SystemOpenDescUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spDescIcon = base.FindTransform("DescIcon").GetComponent<Image>();
		this.m_lblDescTip = base.FindTransform("DescTip").GetComponent<Text>();
		this.m_lblLockTip1 = base.FindTransform("LockTip1").GetComponent<Text>();
		this.m_lblLockTip2 = base.FindTransform("LockTip2").GetComponent<Text>();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			SystemOpenDescUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	public void RefreshUI(SystemOpen dataSO)
	{
		ResourceManager.SetSprite(this.m_spDescIcon, GameDataUtils.GetIcon(dataSO.icon));
		this.m_spDescIcon.SetNativeSize();
		this.m_lblDescTip.set_text(GameDataUtils.GetChineseContent(dataSO.description2, false));
		this.m_lblLockTip1.set_text(string.Empty);
		this.m_lblLockTip2.set_text(string.Empty);
		if (dataSO.level > EntityWorld.Instance.EntSelf.Lv)
		{
			this.m_lblLockTip1.set_text(string.Format("系统开启等级{0}", dataSO.level));
			this.m_lblLockTip2.set_text(string.Format("还差{0}级", Mathf.Max(0, dataSO.level - EntityWorld.Instance.EntSelf.Lv)));
		}
		else if (dataSO.taskId > 0)
		{
			this.m_lblLockTip1.set_text(string.Format(GameDataUtils.GetChineseContent(360047, false), MainTaskItem.GetTaskNameById(dataSO.taskId)));
			this.m_lblLockTip2.set_text(string.Empty);
		}
	}
}
