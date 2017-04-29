using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WingCell : BaseUIBehaviour
{
	public int wingId;

	private Text m_lblWingName;

	private Image m_spWingIcon;

	private Image m_spBackground;

	private Transform m_imgHighlight;

	private Transform m_activeRequire;

	private Text m_lblProgress;

	private RectTransform m_imgProgress;

	private Image m_spimgIcon;

	private Transform m_imgTimeLimit;

	private Text m_txtTimeLimit;

	private int m_fxId;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_imgHighlight = base.FindTransform("imgHighlight");
		this.m_activeRequire = base.FindTransform("activeRequire");
		this.m_lblProgress = base.FindTransform("txtProgress").GetComponent<Text>();
		this.m_imgProgress = (base.FindTransform("imgProgress") as RectTransform);
		this.m_spimgIcon = base.FindTransform("imgIcon").GetComponent<Image>();
		this.m_imgTimeLimit = base.FindTransform("imgTimeLimit");
		this.m_txtTimeLimit = base.FindTransform("txtTimeLimit").GetComponent<Text>();
		this.m_lblWingName = base.FindTransform("WingName").GetComponent<Text>();
		this.m_spWingIcon = base.FindTransform("WingIcon").GetComponent<Image>();
		this.m_spBackground = base.FindTransform("Background").GetComponent<Image>();
		base.get_gameObject().GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickWing));
	}

	private void OnClickWing()
	{
		WingSelectUI.Instance.OnClickOneWing(this.wingId);
	}

	public void RefreshWing(wings dataWings)
	{
		this.wingId = dataWings.id;
		base.get_transform().set_name(dataWings.id.ToString());
		this.ResetAll();
		this.PlayCanActiveSpine();
		wings wingInfo = WingManager.GetWingInfo(this.wingId);
		this.m_lblWingName.set_text(TextColorMgr.GetColorByQuality(wingInfo.name, wingInfo.color));
		ResourceManager.SetSprite(this.m_spWingIcon, GameDataUtils.GetIcon(wingInfo.icon));
		if (WingManager.GetWingLv(this.wingId) == 0)
		{
			ImageColorMgr.SetImageColor(this.m_spWingIcon, true);
			int key = wingInfo.activation.get_Item(0).key;
			int value = wingInfo.activation.get_Item(0).value;
			long num = BackpackManager.Instance.OnGetGoodCount(key);
			int icon = DataReader<Items>.Get(key).icon;
			ResourceManager.SetSprite(this.m_spimgIcon, GameDataUtils.GetItemIcon(key));
			this.m_spimgIcon.SetNativeSize();
			this.m_activeRequire.get_gameObject().SetActive(true);
			this.m_lblProgress.set_text(num + "/" + value);
			float num2 = Mathf.Clamp01((float)num / (float)value);
			this.m_imgProgress.set_sizeDelta(new Vector2(180f * num2, 18.9f));
			if (WingSelectUI.IsTimeLimitWing(this.wingId))
			{
				this.m_imgTimeLimit.get_gameObject().SetActive(true);
			}
		}
		else
		{
			ImageColorMgr.SetImageColor(this.m_spBackground, false);
			ImageColorMgr.SetImageColor(this.m_spWingIcon, false);
			if (this.wingId == EntityWorld.Instance.EntSelf.Decorations.wingId)
			{
				this.m_imgHighlight.get_gameObject().SetActive(true);
			}
			if (WingSelectUI.IsTimeLimitWing(this.wingId))
			{
				this.m_imgTimeLimit.get_gameObject().SetActive(true);
				this.m_txtTimeLimit.get_gameObject().SetActive(true);
				this.m_txtTimeLimit.set_text(WingSelectUI.GetWingRemainTime(this.wingId));
			}
		}
	}

	private void ResetAll()
	{
		this.m_imgHighlight.get_gameObject().SetActive(false);
		this.m_activeRequire.get_gameObject().SetActive(false);
		this.m_imgTimeLimit.get_gameObject().SetActive(false);
		this.m_txtTimeLimit.get_gameObject().SetActive(false);
	}

	private void PlayCanActiveSpine()
	{
		if (this.IsPlayCanActiveSpine())
		{
			this.m_fxId = FXSpineManager.Instance.ReplaySpine(this.m_fxId, 2202, base.get_transform(), "WingSelectUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.m_fxId, true);
		}
	}

	private bool IsPlayCanActiveSpine()
	{
		return WingManager.GetWingLv(this.wingId) == 0 && WingManager.IsCanActiveWing(this.wingId);
	}

	public void PlayActiveSuccess()
	{
		FXSpineManager.Instance.PlaySpine(2205, base.get_transform(), string.Empty, 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
