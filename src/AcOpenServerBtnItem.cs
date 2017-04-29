using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AcOpenServerBtnItem : BaseUIBehaviour
{
	private KaiFuHuoDong activityTypeCfgData;

	private bool isInit;

	private Text typeNameText;

	private Image typeImg;

	private Image lockImg;

	private Image redPointImg;

	private bool selected;

	private bool locked;

	public int ActivityTypeID
	{
		get
		{
			if (this.activityTypeCfgData != null)
			{
				return this.activityTypeCfgData.Type;
			}
			return 1;
		}
	}

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			this.SetBtnLightAndDim(this.selected, this.Locked);
		}
	}

	public bool Locked
	{
		get
		{
			return this.locked;
		}
		set
		{
			this.locked = value;
			this.lockImg.set_enabled(value);
			this.SetBtnLightAndDim(this.Selected, this.locked);
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.typeNameText = base.FindTransform("TypeName").GetComponent<Text>();
		this.typeImg = base.FindTransform("TypeBg").GetComponent<Image>();
		this.lockImg = base.FindTransform("LockImg").GetComponent<Image>();
		this.redPointImg = base.FindTransform("RedPoint").GetComponent<Image>();
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateAcTypeCanGetRewardTip, new Callback(this.UpdateAcTypeCanGetRewardTip));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateAcTypeCanGetRewardTip, new Callback(this.UpdateAcTypeCanGetRewardTip));
	}

	public void UpdateUI(KaiFuHuoDong cfgData)
	{
		this.activityTypeCfgData = cfgData;
		if (this.activityTypeCfgData == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.Locked = !AcOpenServerManager.Instance.CheckActivityTypeUnLock(cfgData.Type);
		this.typeNameText.set_text(GameDataUtils.GetChineseContent(this.activityTypeCfgData.name, false));
		this.ShowRedPointTip();
	}

	public void ShowRedPointTip()
	{
		if (this.activityTypeCfgData != null && this.redPointImg != null)
		{
			this.redPointImg.set_enabled(AcOpenServerManager.Instance.CheckCanShowAcTypeRedPoint((OpenServerType.acType)this.activityTypeCfgData.Type));
		}
	}

	private void UpdateAcTypeCanGetRewardTip()
	{
		if (this.activityTypeCfgData != null)
		{
			this.ShowRedPointTip();
		}
	}

	private void SetBtnLightAndDim(bool selected, bool isLocked)
	{
		string spriteName = string.Empty;
		Color effectColor = new Color(0.360784322f, 0.239215687f, 0.1254902f);
		string text = string.Empty;
		if (isLocked)
		{
			spriteName = "bt_fenleianniu_4";
			effectColor = new Color(0.239215687f, 0.223529413f, 0.211764708f);
			text = "#a5a2a0";
		}
		else if (selected && !isLocked)
		{
			spriteName = "bt_fenleianniu_1";
			effectColor = new Color(0.5764706f, 0.3254902f, 0f);
			text = "#fffbcc";
		}
		else if (!selected && !isLocked)
		{
			spriteName = "bt_fenleianniu_3";
			effectColor = new Color(0.360784322f, 0.239215687f, 0.1254902f);
			text = "#e6bca3";
		}
		ResourceManager.SetSprite(this.typeImg, ResourceManager.GetIconSprite(spriteName));
		this.typeImg.SetNativeSize();
		this.typeNameText.GetComponent<Outline>().set_effectColor(effectColor);
		if (this.activityTypeCfgData != null)
		{
			this.typeNameText.set_text(string.Concat(new string[]
			{
				"<color=",
				text,
				">",
				GameDataUtils.GetChineseContent(this.activityTypeCfgData.name, false),
				"</color>"
			}));
		}
	}
}
