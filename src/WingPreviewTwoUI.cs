using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WingPreviewTwoUI : UIBase
{
	public static WingPreviewTwoUI instance;

	private WingPreviewCell m_WingPreviewCellCurrent;

	private WingPreviewCell m_WingPreviewCellNext;

	private GameObject m_goBtnArrowL;

	private GameObject m_goBtnArrowR;

	private wingLv m_wingLvInfoL;

	private wingLv m_wingLvInfoR;

	private int m_current_wingId;

	private int m_current_winglevel;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		WingPreviewTwoUI.instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_goBtnArrowL = base.FindTransform("BtnArrowL").get_gameObject();
		this.m_goBtnArrowR = base.FindTransform("BtnArrowR").get_gameObject();
		base.FindTransform("BtnArrowL").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnArrowL));
		base.FindTransform("BtnArrowR").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnArrowR));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	private void OnClickBtnGet(int itemId)
	{
		LinkNavigationManager.ItemNotEnoughToLink(itemId, true, delegate
		{
			LinkNavigationManager.OpenXMarketUI2();
			this.Show(false);
		}, true);
	}

	private void OnClickBtnArrowL()
	{
		wingLv wingLvInfoPreDifferent = WingManager.GetWingLvInfoPreDifferent(this.m_current_wingId, this.m_wingLvInfoL.lv);
		if (wingLvInfoPreDifferent.model == this.m_wingLvInfoL.model)
		{
			return;
		}
		this.m_wingLvInfoR = this.m_wingLvInfoL;
		this.m_wingLvInfoL = wingLvInfoPreDifferent;
		WingGlobal.ResetRawImage();
		this.SetWingPreviewCellAll();
	}

	private void OnClickBtnArrowR()
	{
		this.SetWingPreviewCellAll();
		wingLv wingLvInfoNextDifferent = WingManager.GetWingLvInfoNextDifferent(this.m_current_wingId, this.m_wingLvInfoR.lv);
		if (wingLvInfoNextDifferent.model == this.m_wingLvInfoR.model)
		{
			return;
		}
		this.m_wingLvInfoL = this.m_wingLvInfoR;
		this.m_wingLvInfoR = wingLvInfoNextDifferent;
		WingGlobal.ResetRawImage();
		this.SetWingPreviewCellAll();
	}

	public void Init(int wingId)
	{
		WingGlobal.ResetRawImage();
		if (this.m_WingPreviewCellCurrent != null && this.m_WingPreviewCellCurrent.get_gameObject() != null)
		{
			Object.Destroy(this.m_WingPreviewCellCurrent.get_gameObject());
		}
		if (this.m_WingPreviewCellNext != null && this.m_WingPreviewCellNext.get_gameObject() != null)
		{
			Object.Destroy(this.m_WingPreviewCellNext.get_gameObject());
		}
		this.m_current_wingId = wingId;
		this.m_current_winglevel = WingManager.GetWingLv(wingId);
		this.m_wingLvInfoL = WingManager.GetWingLvInfo(this.m_current_wingId, this.m_current_winglevel);
		this.m_wingLvInfoR = WingManager.GetWingLvInfoNextDifferent(this.m_current_wingId, this.m_current_winglevel);
		if (this.m_wingLvInfoL.model == this.m_wingLvInfoR.model)
		{
			this.m_wingLvInfoL = WingManager.GetWingLvInfoPreDifferent(this.m_current_wingId, this.m_current_winglevel);
		}
		this.m_WingPreviewCellCurrent = WingGlobal.GetOneWingPreview(base.get_transform());
		this.m_WingPreviewCellCurrent.get_transform().set_localPosition(new Vector3(-245f, 0f));
		this.m_WingPreviewCellNext = WingGlobal.GetOneWingPreview(base.get_transform());
		this.m_WingPreviewCellNext.get_transform().set_localPosition(new Vector3(245f, 0f));
		base.get_transform().Find("imgArrow").SetAsLastSibling();
		this.SetWingPreviewCellAll();
	}

	private void SetWingPreviewCellAll()
	{
		this.SetWingPreviewCellL();
		this.SetWingPreviewCellR();
	}

	private void SetWingPreviewCellL()
	{
		wingLv wingLvInfoPreDifferent = WingManager.GetWingLvInfoPreDifferent(this.m_current_wingId, this.m_wingLvInfoL.lv);
		this.m_goBtnArrowL.SetActive(wingLvInfoPreDifferent.model != this.m_wingLvInfoL.model);
		this.m_WingPreviewCellCurrent.SetRawImage(this.m_wingLvInfoL.model);
		this.m_WingPreviewCellCurrent.SetName(TextColorMgr.GetColorByQuality(this.m_wingLvInfoL.name, this.m_wingLvInfoL.color));
		if (this.m_current_winglevel == 0 || this.m_wingLvInfoL.lv > this.m_current_winglevel)
		{
			this.m_WingPreviewCellCurrent.SetCondition(true, string.Format(GameDataUtils.GetChineseContent(16301, false), this.m_wingLvInfoL.lv));
		}
		else
		{
			this.m_WingPreviewCellCurrent.SetCondition(false, string.Empty);
		}
		if (this.m_current_winglevel == 0 && !WingManager.IsCanActiveWing(this.m_current_wingId))
		{
			wings wingInfo = WingManager.GetWingInfo(this.m_current_wingId);
			this.m_WingPreviewCellCurrent.ShowButtonGet(true);
			this.m_WingPreviewCellCurrent.actionButtonGet = delegate
			{
				this.OnClickBtnGet(wingInfo.activation.get_Item(0).key);
			};
		}
		else
		{
			this.m_WingPreviewCellCurrent.ShowButtonGet(false);
		}
	}

	private void SetWingPreviewCellR()
	{
		wingLv wingLvInfoNextDifferent = WingManager.GetWingLvInfoNextDifferent(this.m_current_wingId, this.m_wingLvInfoR.lv);
		this.m_goBtnArrowR.SetActive(wingLvInfoNextDifferent.model != this.m_wingLvInfoR.model);
		this.m_WingPreviewCellNext.SetRawImage(this.m_wingLvInfoR.model);
		this.m_WingPreviewCellNext.SetName(TextColorMgr.GetColorByQuality(this.m_wingLvInfoR.name, this.m_wingLvInfoR.color));
		if (wingLvInfoNextDifferent.model == this.m_wingLvInfoR.model && WingManager.IsMaxWingLv(this.m_current_wingId, this.m_current_winglevel))
		{
			this.m_WingPreviewCellNext.SetCondition(true, "已获得最高级");
		}
		else
		{
			this.m_WingPreviewCellNext.SetCondition(this.m_wingLvInfoR.lv > this.m_current_winglevel, string.Format(GameDataUtils.GetChineseContent(16301, false), this.m_wingLvInfoR.lv));
		}
		this.m_WingPreviewCellNext.ShowButtonGet(false);
	}

	private void OnApplicationFocus(bool focusStatus)
	{
		WingGlobal.ResetRawImage();
		this.m_WingPreviewCellCurrent.DoOnApplicationPause();
		this.m_WingPreviewCellNext.DoOnApplicationPause();
	}

	private void OnApplicationPause(bool bPause)
	{
		WingGlobal.ResetRawImage();
		this.m_WingPreviewCellCurrent.DoOnApplicationPause();
		this.m_WingPreviewCellNext.DoOnApplicationPause();
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy())
			{
				WingGlobal.ResetRawImage();
				this.m_WingPreviewCellCurrent.DoOnApplicationPause();
				this.m_WingPreviewCellNext.DoOnApplicationPause();
			}
		});
	}
}
