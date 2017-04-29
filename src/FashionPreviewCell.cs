using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FashionPreviewCell : BaseUIBehaviour
{
	public enum FashionPreviewCellType
	{
		None,
		Dress,
		Undress,
		Renewal,
		Buy,
		Wardrobe
	}

	protected Image FashionPreviewCellBG;

	protected RawImage FashionPreviewCellModelProjection;

	protected GameObject FashionPreviewCellTitle;

	protected Text FashionPreviewCellTitleName;

	protected GameObject FashionPreviewCellTitleLimitMark;

	protected Transform FashionPreviewCellCondition;

	protected Text FashionPreviewCellConditionText;

	protected Transform FashionPreviewCellConditionSlot0;

	protected Transform FashionPreviewCellConditionSlot1;

	protected Transform FashionPreviewCellAttr;

	protected Transform FashionPreviewCellAttrSlot0;

	protected Transform FashionPreviewCellAttrSlot1;

	protected Text FashionPreviewCellAttrText0;

	protected Text FashionPreviewCellAttrText1;

	protected GameObject FashionPreviewCellDressBtn;

	protected GameObject FashionPreviewCellUndressBtn;

	protected GameObject FashionPreviewCellRenewalBtn;

	protected GameObject FashionPreviewCellBuyBtn;

	protected Text FashionPreviewCellDressBtnText;

	protected Text FashionPreviewCellUndressBtnText;

	protected Text FashionPreviewCellRenewalBtnText;

	protected Text FashionPreviewCellBuyBtnText;

	protected UIBase bindUI;

	protected string fashionDataID;

	protected List<string> allFashionDataID;

	protected ExteriorArithmeticUnit exteriorUnit;

	protected int modelIndex = -1;

	protected GameObject previewCamera;

	protected GameObject previewModel;

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(null, null, null, null);
			}
			return this.exteriorUnit;
		}
	}

	public int ModelIndex
	{
		get
		{
			return this.modelIndex;
		}
		set
		{
			this.modelIndex = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FashionPreviewCellBG = base.GetComponent<Image>();
		this.FashionPreviewCellModelProjection = base.FindTransform("FashionPreviewCellModelProjection").GetComponent<RawImage>();
		this.FashionPreviewCellTitle = base.FindTransform("FashionPreviewCellTitle").get_gameObject();
		this.FashionPreviewCellTitleName = base.FindTransform("FashionPreviewCellTitleName").GetComponent<Text>();
		this.FashionPreviewCellTitleLimitMark = base.FindTransform("FashionPreviewCellTitleLimitMark").get_gameObject();
		this.FashionPreviewCellCondition = base.FindTransform("FashionPreviewCellCondition");
		this.FashionPreviewCellConditionText = base.FindTransform("FashionPreviewCellConditionText").GetComponent<Text>();
		this.FashionPreviewCellConditionSlot0 = base.FindTransform("FashionPreviewCellConditionSlot0");
		this.FashionPreviewCellConditionSlot1 = base.FindTransform("FashionPreviewCellConditionSlot1");
		this.FashionPreviewCellAttr = base.FindTransform("FashionPreviewCellAttr");
		this.FashionPreviewCellAttrSlot0 = base.FindTransform("FashionPreviewCellAttrSlot0");
		this.FashionPreviewCellAttrSlot1 = base.FindTransform("FashionPreviewCellAttrSlot1");
		this.FashionPreviewCellAttrText0 = base.FindTransform("FashionPreviewCellAttrText0").GetComponent<Text>();
		this.FashionPreviewCellAttrText1 = base.FindTransform("FashionPreviewCellAttrText1").GetComponent<Text>();
		this.FashionPreviewCellDressBtn = base.FindTransform("FashionPreviewCellDressBtn").get_gameObject();
		this.FashionPreviewCellUndressBtn = base.FindTransform("FashionPreviewCellUndressBtn").get_gameObject();
		this.FashionPreviewCellRenewalBtn = base.FindTransform("FashionPreviewCellRenewalBtn").get_gameObject();
		this.FashionPreviewCellBuyBtn = base.FindTransform("FashionPreviewCellBuyBtn").get_gameObject();
		this.FashionPreviewCellDressBtnText = base.FindTransform("FashionPreviewCellDressBtnText").GetComponent<Text>();
		this.FashionPreviewCellUndressBtnText = base.FindTransform("FashionPreviewCellUndressBtnText").GetComponent<Text>();
		this.FashionPreviewCellRenewalBtnText = base.FindTransform("FashionPreviewCellRenewalBtnText").GetComponent<Text>();
		this.FashionPreviewCellBuyBtnText = base.FindTransform("FashionPreviewCellBuyBtnText").GetComponent<Text>();
		EventTriggerListener expr_1CF = EventTriggerListener.Get(base.get_gameObject());
		expr_1CF.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_1CF.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragModel));
		this.FashionPreviewCellDressBtn.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickDressBtn));
		this.FashionPreviewCellUndressBtn.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickUndressBtn));
		this.FashionPreviewCellRenewalBtn.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickRenewalBtn));
		this.FashionPreviewCellBuyBtn.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBuyBtn));
		this.FashionPreviewCellDressBtnText.set_text(GameDataUtils.GetChineseContent(1005007, false));
		this.FashionPreviewCellUndressBtnText.set_text(GameDataUtils.GetChineseContent(1005008, false));
		this.FashionPreviewCellRenewalBtnText.set_text(GameDataUtils.GetChineseContent(1005009, false));
		this.FashionPreviewCellBuyBtnText.set_text(GameDataUtils.GetChineseContent(1005015, false));
	}

	private void OnDestroy()
	{
		FashionPreviewManager.Instance.ReleasePreview(this);
	}

	public void Bind(UIBase theBindUI)
	{
		this.bindUI = theBindUI;
	}

	public void SetData(List<string> theAllFashionDataID, string theFashionDataID, FashionPreviewCell.FashionPreviewCellType type, bool isShowBG = true)
	{
		this.fashionDataID = theFashionDataID;
		this.allFashionDataID = theAllFashionDataID;
		this.ShowBG(isShowBG);
		this.SetDetailData(theFashionDataID, type);
		this.SetModelData(theAllFashionDataID);
		this.SetType(type);
	}

	protected void ShowBG(bool isShowBG)
	{
		this.FashionPreviewCellBG.set_enabled(isShowBG);
	}

	public void SetDetailData(string theFashionDataID, FashionPreviewCell.FashionPreviewCellType type)
	{
		if (!string.IsNullOrEmpty(theFashionDataID) && DataReader<ShiZhuangXiTong>.Contains(theFashionDataID))
		{
			ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(theFashionDataID);
			if (DataReader<Items>.Contains(shiZhuangXiTong.itemsID))
			{
				Items items = DataReader<Items>.Get(shiZhuangXiTong.itemsID);
				this.ShowTitle(true);
				this.ShowTimeLimitMark(shiZhuangXiTong.quality == 1);
				this.SetAttrText(this.IsTypeShowAttr(type), (shiZhuangXiTong.gainProperty != 0) ? AttrUtility.GetAllStandardAddDesc(shiZhuangXiTong.gainProperty) : null);
				this.SetTitleName((shiZhuangXiTong.title != 0) ? (GameDataUtils.GetChineseContent(shiZhuangXiTong.title, false) + "\n" + GameDataUtils.GetChineseContent(items.name, false)) : GameDataUtils.GetChineseContent(items.name, false));
				this.SetConditionText(true, GameDataUtils.GetChineseContent(items.describeId2, false));
			}
			else
			{
				this.ShowTitle(false);
				this.SetConditionText(false, string.Empty);
				this.SetAttrText(false, null);
			}
		}
		else
		{
			this.ShowTitle(false);
			this.SetConditionText(false, string.Empty);
			this.SetAttrText(false, null);
		}
	}

	protected bool IsTypeShowAttr(FashionPreviewCell.FashionPreviewCellType type)
	{
		return type != FashionPreviewCell.FashionPreviewCellType.Wardrobe;
	}

	protected void SetModelData(List<string> allFashionDataID)
	{
		this.ExteriorUnit.WrapSetData(delegate
		{
			this.ExteriorUnit.Clone(EntityWorld.Instance.EntSelf.ExteriorUnit, false);
			this.ExteriorUnit.ClientModelID = 0;
			this.ExteriorUnit.FashionIDs = allFashionDataID;
		});
		List<GameObject> list = FashionPreviewManager.Instance.SetModelData(this.FashionPreviewCellModelProjection, this.ExteriorUnit, out this.modelIndex);
		if (list.get_Count() > 0)
		{
			this.previewCamera = list.get_Item(0);
		}
		if (list.get_Count() > 1)
		{
			this.previewModel = list.get_Item(1);
		}
	}

	public void ResetModelData()
	{
		if (this.previewCamera)
		{
			Object.Destroy(this.previewCamera);
		}
		if (this.previewModel)
		{
			Object.Destroy(this.previewModel);
		}
	}

	protected void SetType(FashionPreviewCell.FashionPreviewCellType type)
	{
		switch (type)
		{
		case FashionPreviewCell.FashionPreviewCellType.None:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot1.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot1.get_localPosition());
			this.ShowDressBtn(false);
			this.ShowUndressBtn(false);
			this.ShowRenewalBtn(false);
			this.ShowBuyBtn(false);
			break;
		case FashionPreviewCell.FashionPreviewCellType.Dress:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot0.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot0.get_localPosition());
			this.ShowDressBtn(true);
			this.ShowUndressBtn(false);
			this.ShowRenewalBtn(false);
			this.ShowBuyBtn(false);
			break;
		case FashionPreviewCell.FashionPreviewCellType.Undress:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot0.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot0.get_localPosition());
			this.ShowDressBtn(false);
			this.ShowUndressBtn(true);
			this.ShowRenewalBtn(false);
			this.ShowBuyBtn(false);
			break;
		case FashionPreviewCell.FashionPreviewCellType.Renewal:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot0.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot0.get_localPosition());
			this.ShowDressBtn(false);
			this.ShowUndressBtn(false);
			this.ShowRenewalBtn(true);
			this.ShowBuyBtn(false);
			break;
		case FashionPreviewCell.FashionPreviewCellType.Buy:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot0.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot0.get_localPosition());
			this.ShowDressBtn(false);
			this.ShowUndressBtn(false);
			this.ShowRenewalBtn(false);
			this.ShowBuyBtn(true);
			break;
		case FashionPreviewCell.FashionPreviewCellType.Wardrobe:
			this.FashionPreviewCellCondition.set_localPosition(this.FashionPreviewCellConditionSlot1.get_localPosition());
			this.FashionPreviewCellAttr.set_localPosition(this.FashionPreviewCellAttrSlot1.get_localPosition());
			this.ShowDressBtn(false);
			this.ShowUndressBtn(false);
			this.ShowRenewalBtn(false);
			this.ShowBuyBtn(false);
			break;
		}
	}

	protected void ShowTitle(bool isShow)
	{
		this.FashionPreviewCellTitle.SetActive(isShow);
	}

	protected void SetTitleName(string name)
	{
		this.FashionPreviewCellTitleName.set_text(name);
	}

	protected void ShowTimeLimitMark(bool isShow)
	{
		this.FashionPreviewCellTitleLimitMark.SetActive(isShow);
	}

	protected void SetConditionText(bool isShow, string name = "")
	{
		this.FashionPreviewCellCondition.get_gameObject().SetActive(isShow && !string.IsNullOrEmpty(name));
		this.FashionPreviewCellConditionText.set_text(string.Format(GameDataUtils.GetChineseContent(1005004, false), name));
	}

	protected void SetAttrText(bool isShow, List<string> attrText = null)
	{
		this.FashionPreviewCellAttr.get_gameObject().SetActive(isShow && attrText != null && attrText.get_Count() != 0);
		this.SetAttrText0(false, string.Empty);
		this.SetAttrText1(false, string.Empty);
		if (!isShow)
		{
			return;
		}
		if (attrText == null || attrText.get_Count() == 0)
		{
			return;
		}
		if (attrText.get_Count() == 1)
		{
			this.SetAttrText0(true, attrText.get_Item(0));
		}
		else if (attrText.get_Count() == 2)
		{
			this.SetAttrText0(true, attrText.get_Item(0));
			this.SetAttrText1(true, attrText.get_Item(1));
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(attrText.get_Item(0));
			for (int i = 2; i < attrText.get_Count(); i += 2)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(attrText.get_Item(i));
			}
			this.SetAttrText0(true, stringBuilder.ToString());
			stringBuilder.Clear();
			stringBuilder.Append(attrText.get_Item(1));
			for (int j = 3; j < attrText.get_Count(); j += 2)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(attrText.get_Item(j));
			}
			this.SetAttrText1(true, stringBuilder.ToString());
		}
	}

	protected void SetAttrText0(bool isShow, string name = "")
	{
		this.FashionPreviewCellAttrText0.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.FashionPreviewCellAttrText0.set_text(name);
		}
	}

	protected void SetAttrText1(bool isShow, string name = "")
	{
		this.FashionPreviewCellAttrText1.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.FashionPreviewCellAttrText1.set_text(name);
		}
	}

	protected void ShowDressBtn(bool isShow)
	{
		this.FashionPreviewCellDressBtn.SetActive(isShow);
	}

	protected void ShowUndressBtn(bool isShow)
	{
		this.FashionPreviewCellUndressBtn.SetActive(isShow);
	}

	protected void ShowRenewalBtn(bool isShow)
	{
		this.FashionPreviewCellRenewalBtn.SetActive(isShow);
	}

	protected void ShowBuyBtn(bool isShow)
	{
		this.FashionPreviewCellBuyBtn.SetActive(isShow);
	}

	protected void SetDressBtnText(string name)
	{
		this.FashionPreviewCellDressBtnText.set_text(name);
	}

	protected void SetUndressBtnText(string name)
	{
		this.FashionPreviewCellUndressBtnText.set_text(name);
	}

	protected void SetRenewalBtnText(string name)
	{
		this.FashionPreviewCellRenewalBtnText.set_text(name);
	}

	protected void SetBuyBtnText(string name)
	{
		this.FashionPreviewCellBuyBtnText.set_text(name);
	}

	protected void OnDragModel(PointerEventData eventData)
	{
		if (!this.previewModel)
		{
			return;
		}
		Transform expr_1C = this.previewModel.get_transform();
		expr_1C.set_rotation(expr_1C.get_rotation() * Quaternion.AngleAxis(-eventData.get_delta().x, Vector3.get_up()));
	}

	protected void OnClickDressBtn()
	{
		FashionManager.Instance.Dress(this.fashionDataID);
		if (this.bindUI)
		{
			this.bindUI.Show(false);
		}
	}

	protected void OnClickUndressBtn()
	{
		FashionManager.Instance.Undress(this.fashionDataID);
		if (this.bindUI)
		{
			this.bindUI.Show(false);
		}
	}

	protected void OnClickRenewalBtn()
	{
		FashionManager.Instance.OpenBuyFashionUI(this.fashionDataID);
		if (this.bindUI)
		{
			this.bindUI.Show(false);
		}
	}

	protected void OnClickBuyBtn()
	{
		FashionManager.Instance.OpenBuyFashionUI(this.fashionDataID);
		if (this.bindUI)
		{
			this.bindUI.Show(false);
		}
	}

	public void DoOnApplicationPause()
	{
		this.SetModelData(this.allFashionDataID);
	}
}
