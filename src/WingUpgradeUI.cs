using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WingUpgradeUI : UIBase
{
	public enum WingUpgradeState
	{
		NotActive,
		Upgradeable,
		Max
	}

	private const int MatIconId = 1801;

	public static WingUpgradeUI Instance;

	private Transform m_activeRequire;

	private Image progressIcon;

	private Transform m_upgradeRequire;

	private Transform m_btnActive;

	private Transform m_btnWear;

	private Transform m_btnUndress;

	private Transform m_btnUpgrade;

	private Transform m_attrCurr;

	private Transform m_attrNext;

	private Transform m_btnIcon;

	private Transform m_maxLevelTip;

	private int m_fxIconSpine;

	private int m_fxCanActiveSpine;

	private int m_fxUpgradeSpine;

	private int m_fxProgressSpine;

	private void Awake()
	{
		WingUpgradeUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.m_activeRequire = base.FindTransform("activeRequire");
		this.progressIcon = base.FindTransform("activeRequire").FindChild("imgIcon").GetComponent<Image>();
		ResourceManager.SetSprite(this.progressIcon, GameDataUtils.GetIcon(1801));
		this.progressIcon.SetNativeSize();
		this.m_upgradeRequire = base.FindTransform("upgradeRequire");
		Image component = this.m_upgradeRequire.FindChild("imgIcon").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetIcon(1801));
		component.SetNativeSize();
		this.m_btnActive = base.FindTransform("btnActive");
		this.m_btnWear = base.FindTransform("btnWear");
		this.m_btnUndress = base.FindTransform("btnUndress");
		this.m_btnUpgrade = base.FindTransform("btnUpgrade");
		this.m_attrCurr = base.FindTransform("attrCurr");
		this.m_attrNext = base.FindTransform("attrNext");
		this.m_btnIcon = base.FindTransform("btnIcon");
		this.m_maxLevelTip = base.FindTransform("maxLevelTip");
		this.InitButtonListener();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.PlayIconSpine();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		WingUpgradeUI.Instance = null;
		base.ReleaseSelf(true);
	}

	private void InitButtonListener()
	{
		Button btnWear = this.m_btnWear.GetComponent<Button>();
		btnWear.get_onClick().RemoveAllListeners();
		btnWear.get_onClick().AddListener(delegate
		{
			this.OnClickBtnWear(btnWear.get_gameObject());
		});
		Button component = this.m_btnUndress.GetComponent<Button>();
		component.get_onClick().RemoveAllListeners();
		component.get_onClick().AddListener(delegate
		{
			this.OnClickBtnUndress();
		});
		Button btnUpgrade = this.m_btnUpgrade.GetComponent<Button>();
		btnUpgrade.get_onClick().RemoveAllListeners();
		btnUpgrade.get_onClick().AddListener(delegate
		{
			this.OnClickBtnUpgrade(btnUpgrade.get_gameObject());
		});
		Button component2 = this.m_btnActive.GetComponent<Button>();
		component2.get_onClick().RemoveAllListeners();
		component2.get_onClick().AddListener(delegate
		{
			this.OnClickBtnActive(btnUpgrade.get_gameObject());
		});
		Button btnIcon = this.m_btnIcon.GetComponent<Button>();
		btnIcon.get_onClick().RemoveAllListeners();
		btnIcon.get_onClick().AddListener(delegate
		{
			this.OnClickBtnIcon(btnIcon.get_gameObject());
		});
	}

	private void OnClickBtnIcon(GameObject sender)
	{
		int num = 1;
		int wingLv = WingManager.GetWingLv(num);
		WingPreviewTwoUI wingPreviewTwoUI = UIManagerControl.Instance.OpenUI("WingPreviewTwoUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as WingPreviewTwoUI;
		wingPreviewTwoUI.Init(num);
		wingPreviewTwoUI.get_transform().SetAsLastSibling();
		if (WingManager.firstGetWingDetail && WingManager.GetWingLv(1) > 0)
		{
			WingManager.Instance.SendGetWingDetailReq();
		}
	}

	private void OnClickBtnWear(GameObject sender)
	{
		WingUI.wingIdLast = 1;
		WingManager.Instance.SendWingWearReq(1);
		WingManager.Instance.SendWingHiddenReq(false);
	}

	private void OnClickBtnUndress()
	{
		WingManager.Instance.SendWingHiddenReq(true);
	}

	private void OnClickBtnUpgrade(GameObject sender)
	{
		WingManager.Instance.SendWingUpLvReq(1);
	}

	private void OnClickBtnActive(GameObject sender)
	{
		WingManager.Instance.SendWingComposeReq(1);
	}

	private void OnClickUpgradeRequire(int itemId)
	{
		if (!WingManager.IsCanUpgradeWing(1))
		{
			LinkNavigationManager.ItemNotEnoughToLink(itemId, true, null, true);
		}
	}

	public void Refresh()
	{
		this.ResetAll();
		int wingLv = WingManager.GetWingLv(1);
		this.SetAttrCurr(1, wingLv > 0);
		if (wingLv == 0)
		{
			this.m_activeRequire.get_gameObject().SetActive(true);
			this.m_btnActive.get_gameObject().SetActive(true);
			this.SetActiveRequire(1);
			Image component = this.m_btnActive.GetComponent<Image>();
			if (WingManager.IsCanActiveWing(1))
			{
				this.m_btnActive.GetComponent<Button>().set_enabled(true);
				ImageColorMgr.SetImageColor(component, false);
			}
			else
			{
				this.m_btnActive.GetComponent<Button>().set_enabled(false);
				ImageColorMgr.SetImageColor(component, true);
			}
		}
		else if (WingManager.IsMaxWingLv(1, wingLv))
		{
			this.SetButtonWearAndButtonUndress();
			this.m_maxLevelTip.get_gameObject().SetActive(true);
		}
		else
		{
			this.m_upgradeRequire.get_gameObject().SetActive(true);
			this.SetButtonWearAndButtonUndress();
			this.m_btnUpgrade.get_gameObject().SetActive(true);
			this.m_attrNext.get_gameObject().SetActive(true);
			this.SetUpgradeRequire(1);
			this.SetAttrNext(1);
		}
	}

	private void SetButtonWearAndButtonUndress()
	{
		if (WingGlobal.IsWingWearAndNoHidden(1))
		{
			this.m_btnUndress.get_gameObject().SetActive(true);
		}
		else
		{
			this.m_btnWear.get_gameObject().SetActive(true);
		}
	}

	private void ResetAll()
	{
		this.m_activeRequire.get_gameObject().SetActive(false);
		this.m_upgradeRequire.get_gameObject().SetActive(false);
		this.m_maxLevelTip.get_gameObject().SetActive(false);
		this.m_btnActive.get_gameObject().SetActive(false);
		this.m_btnWear.get_gameObject().SetActive(false);
		this.m_btnUndress.get_gameObject().SetActive(false);
		this.m_btnUpgrade.get_gameObject().SetActive(false);
		this.m_attrNext.get_gameObject().SetActive(false);
		this.PlayCanActiveSpine();
	}

	private void SetAttrCurr(int wingId, bool isActivation)
	{
		int wingLv = WingManager.GetWingLv(wingId);
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv);
		Text component = base.get_transform().Find("txtName").GetComponent<Text>();
		component.set_text(TextColorMgr.GetColorByQuality(wingLvInfo.name, wingLvInfo.color));
		Image component2 = this.m_btnIcon.GetComponent<Image>();
		ResourceManager.SetSprite(component2, GameDataUtils.GetIcon(wingLvInfo.icon));
		ImageColorMgr.SetImageColor(component2, !isActivation);
		if (wingLv == 0)
		{
			this.m_attrCurr.Find("txtLv").GetComponent<Text>().set_text("激活后");
		}
		else
		{
			this.m_attrCurr.Find("txtLv").GetComponent<Text>().set_text("Lv." + wingLv);
		}
		Attrs attrs = DataReader<Attrs>.Get(wingLvInfo.templateId);
		for (int i = 0; i < 6; i++)
		{
			if (i < attrs.attrs.get_Count())
			{
				Debug.Log(attrs.attrs.get_Item(i) + "=" + (AttrType)attrs.attrs.get_Item(i));
				this.m_attrCurr.Find("txtAttrName" + (i + 1)).get_gameObject().SetActive(true);
				this.m_attrCurr.Find("txtAttrName" + (i + 1)).GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i), "ff7d4b"));
			}
			else
			{
				this.m_attrCurr.Find("txtAttrName" + (i + 1)).get_gameObject().SetActive(false);
			}
		}
	}

	private void SetAttrNext(int wingId)
	{
		wings wingInfo = WingManager.GetWingInfo(wingId);
		int num = WingManager.GetWingLv(wingId) + 1;
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, num);
		this.m_attrNext.Find("txtLv").GetComponent<Text>().set_text("Lv." + num);
		Attrs attrs = DataReader<Attrs>.Get(wingLvInfo.templateId);
		for (int i = 0; i < 6; i++)
		{
			if (i < attrs.attrs.get_Count())
			{
				this.m_attrNext.Find("txtAttrVal" + (i + 1)).get_gameObject().SetActive(true);
				this.m_attrNext.Find("txtAttrVal" + (i + 1)).GetComponent<Text>().set_text(this.GetFormatAttrValue(attrs.attrs.get_Item(i), (float)attrs.values.get_Item(i)));
			}
			else
			{
				this.m_attrNext.Find("txtAttrVal" + (i + 1)).get_gameObject().SetActive(false);
			}
		}
	}

	private string GetFormatAttrValue(int attrType, float attrValue)
	{
		if (this.IsFormatPercent(attrType))
		{
			return attrValue * 100f + "%";
		}
		return attrValue.ToString();
	}

	private bool IsFormatPercent(int attrType)
	{
		return AttrUtility.GetAttrValueDisplayMode(attrType) == 1;
	}

	private void SetActiveRequire(int wingId)
	{
		wings wingInfo = WingManager.GetWingInfo(wingId);
		int key = wingInfo.activation.get_Item(0).key;
		int value = wingInfo.activation.get_Item(0).value;
		long num = BackpackManager.Instance.OnGetGoodCount(key);
		int icon = DataReader<Items>.Get(key).icon;
		float num2 = Mathf.Min(1f, (float)num / (float)value);
		this.m_activeRequire.Find("imgProgress").GetComponent<RectTransform>().set_sizeDelta(new Vector2(205f * num2, 18.9f));
		string text = num + "/" + value;
		this.m_activeRequire.Find("txtProgress").GetComponent<Text>().set_text(text);
	}

	private void SetUpgradeRequire(int wingId)
	{
		int wingLv = WingManager.GetWingLv(wingId);
		wingLv wingLvInfo = WingManager.GetWingLvInfo(wingId, wingLv);
		int requireItemId = wingLvInfo.update.get_Item(0).key;
		int value = wingLvInfo.update.get_Item(0).value;
		long num = BackpackManager.Instance.OnGetGoodCount(requireItemId);
		int icon = DataReader<Items>.Get(requireItemId).icon;
		string text = num + "/" + value;
		this.m_upgradeRequire.Find("txtProgress").GetComponent<Text>().set_text(text);
		float num2 = Mathf.Min(1f, (float)num / (float)value);
		this.m_upgradeRequire.Find("imgProgress").GetComponent<RectTransform>().set_sizeDelta(new Vector2(205f * num2, 18.9f));
		Button component = this.m_upgradeRequire.GetComponent<Button>();
		component.get_onClick().RemoveAllListeners();
		component.get_onClick().AddListener(delegate
		{
			this.OnClickUpgradeRequire(requireItemId);
		});
	}

	public void PlayIconSpine()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fxIconSpine, true);
		if (WingManager.firstGetWingDetail && WingManager.GetWingLv(1) > 0)
		{
			this.m_fxIconSpine = FXSpineManager.Instance.PlaySpine(2203, this.m_btnIcon, "WingUpgradeUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public void PlayActiveSuccess()
	{
		FXSpineManager.Instance.PlaySpine(2205, this.m_btnIcon, "WingUpgradeUI", 2000, delegate
		{
			this.PlayIconSpine();
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void PlayCanActiveSpine()
	{
		if (this.IsPlayCanActiveSpine() && this.m_fxCanActiveSpine == 0)
		{
			this.m_fxCanActiveSpine = FXSpineManager.Instance.PlaySpine(2201, this.m_btnActive, "WingUpgradeUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (!this.IsPlayCanActiveSpine() && this.m_fxCanActiveSpine != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_fxCanActiveSpine, true);
		}
	}

	private bool IsPlayCanActiveSpine()
	{
		return WingManager.GetWingLv(1) == 0 && WingManager.IsCanActiveWing(1);
	}

	public void PlayUpgradeSpine()
	{
		this.m_fxUpgradeSpine = FXSpineManager.Instance.ReplaySpine(this.m_fxUpgradeSpine, 2208, this.m_btnIcon, "WingUpgradeUI", 2000, null, "UI", 2f, -18f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void PlayProgressSpine()
	{
		this.m_fxProgressSpine = FXSpineManager.Instance.ReplaySpine(this.m_fxProgressSpine, 2206, this.m_upgradeRequire, "WingUpgradeUI", 2000, null, "UI", 0f, 0f, 0.6f, 0.6f, false, FXMaskLayer.MaskState.None);
	}
}
