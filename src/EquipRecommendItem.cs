using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipRecommendItem : BaseUIBehaviour
{
	private Transform excellentTrans;

	private Text itemNameText;

	private Text fightingText;

	private Text titleText;

	private Text btnNameText;

	private Text itemStepText;

	private Image m_imageFrame;

	private Image m_imageIcon;

	private Action BtnCallBackAction;

	private Action BtnCloseCallBack;

	private int equipFxID;

	private int m_itemID;

	private int showUIDepth;

	public string ItemNameContent
	{
		get
		{
			return this.itemNameText.get_text();
		}
		set
		{
			if (this.itemNameText != null)
			{
				this.itemNameText.set_text(value);
			}
		}
	}

	public string FightingContent
	{
		get
		{
			return this.fightingText.get_text();
		}
		set
		{
			if (this.fightingText != null)
			{
				this.fightingText.set_text(value);
			}
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("BtnReplace").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickReplace);
		base.FindTransform("CloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnCloseBtn);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.itemNameText = base.FindTransform("ItemName").GetComponent<Text>();
		this.fightingText = base.FindTransform("Fighting").GetComponent<Text>();
		this.titleText = base.FindTransform("TitleText").GetComponent<Text>();
		this.btnNameText = base.FindTransform("BtnNameText").GetComponent<Text>();
		this.itemStepText = base.FindTransform("ItemStepText").GetComponent<Text>();
		this.m_imageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_imageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.excellentTrans = base.FindTransform("ExcellentAttrIconList");
		if (this.excellentTrans != null && this.excellentTrans.get_gameObject().get_activeSelf())
		{
			this.excellentTrans.get_gameObject().SetActive(false);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void OnClickClose(GameObject go)
	{
		this.OnDestroy();
		FXSpineManager.Instance.DeleteSpine(this.equipFxID, true);
		this.equipFxID = 0;
		if (base.get_gameObject() != null)
		{
			base.get_gameObject().SetActive(false);
		}
	}

	private void OnClickReplace(GameObject go)
	{
		this.OnClickClose(null);
		if (this.BtnCallBackAction != null)
		{
			this.BtnCallBackAction.Invoke();
		}
	}

	private void OnCloseBtn(GameObject go)
	{
		this.OnClickClose(null);
		if (this.BtnCloseCallBack != null)
		{
			this.BtnCloseCallBack.Invoke();
		}
	}

	public void UpdateUIData(int itemCfgID, string titleName = "", string btnName = "", Action BtnCallBack = null, bool showCloseBtn = false, Action BtnCloseCallBack = null, int depth = 2000)
	{
		this.InitUI();
		FXSpineManager.Instance.DeleteSpine(this.equipFxID, true);
		this.equipFxID = 0;
		Items items = DataReader<Items>.Get(itemCfgID);
		if (items == null)
		{
			Debug.Log("item == null  itemID = " + itemCfgID);
			return;
		}
		this.m_itemID = itemCfgID;
		this.showUIDepth = depth;
		ResourceManager.SetSprite(this.m_imageFrame, GameDataUtils.GetItemFrame(itemCfgID));
		ResourceManager.SetSprite(this.m_imageIcon, GameDataUtils.GetItemIcon(itemCfgID));
		if (items.step > 0)
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(true);
			this.itemStepText.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
		else
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(false);
		}
		if (items.tab == 2)
		{
			DepthOfUI depthOfUI = base.FindTransform("ItemStep").GetComponent<DepthOfUI>();
			if (depthOfUI == null)
			{
				depthOfUI = base.FindTransform("ItemStep").get_gameObject().AddComponent<DepthOfUI>();
			}
			depthOfUI.SortingOrder = depth + 1;
			DepthOfUI depthOfUI2 = base.FindTransform("ExcellentAttrIconList").GetComponent<DepthOfUI>();
			if (depthOfUI2 == null)
			{
				depthOfUI2 = base.FindTransform("ExcellentAttrIconList").get_gameObject().AddComponent<DepthOfUI>();
			}
			depthOfUI2.SortingOrder = depth + 1;
			DepthOfUI depthOfUI3 = base.FindTransform("ImageBinding").GetComponent<DepthOfUI>();
			if (depthOfUI3 == null)
			{
				depthOfUI3 = base.FindTransform("ImageBinding").get_gameObject().AddComponent<DepthOfUI>();
			}
			depthOfUI3.SortingOrder = depth + 1;
		}
		this.ItemNameContent = GameDataUtils.GetItemName(itemCfgID, true, 0L);
		this.titleText.set_text(titleName);
		this.btnNameText.set_text(btnName);
		if (items != null)
		{
			base.FindTransform("FightingRegion").get_gameObject().SetActive(items.tab == 2);
		}
		if (BtnCallBack != null)
		{
			this.BtnCallBackAction = BtnCallBack;
		}
		this.BtnCloseCallBack = BtnCloseCallBack;
		base.FindTransform("CloseBtn").get_gameObject().SetActive(showCloseBtn);
		GuideManager.Instance.CheckQueue(false, false);
	}

	public void UpdateUIData(int iconID, string titleName = "", string itemName = "", string btnName = "", Action BtnCallBack = null, Action BtnCloseCallBack = null, bool showCloseBtn = false, int depth = 2000)
	{
		this.InitUI();
		this.showUIDepth = depth;
		ResourceManager.SetIconSprite(this.m_imageFrame, "frame_icon_white");
		ResourceManager.SetSprite(this.m_imageIcon, GameDataUtils.GetIcon(iconID));
		base.FindTransform("ItemStep").get_gameObject().SetActive(false);
		base.FindTransform("FightingRegion").get_gameObject().SetActive(false);
		this.ItemNameContent = itemName;
		this.titleText.set_text(titleName);
		this.btnNameText.set_text(btnName);
		this.BtnCallBackAction = BtnCallBack;
		this.BtnCloseCallBack = BtnCloseCallBack;
		base.FindTransform("CloseBtn").get_gameObject().SetActive(showCloseBtn);
		GuideManager.Instance.CheckQueue(false, false);
	}

	public void SetExcellentCount(int count)
	{
		if (count > 0)
		{
			if (this.excellentTrans == null)
			{
				return;
			}
			if (!this.excellentTrans.get_gameObject().get_activeSelf())
			{
				this.excellentTrans.get_gameObject().SetActive(true);
			}
			int i;
			for (i = 0; i < count; i++)
			{
				if (i >= 3)
				{
					break;
				}
				this.excellentTrans.FindChild("Image" + (i + 1)).get_gameObject().SetActive(true);
			}
			for (int j = i; j < 3; j++)
			{
				this.excellentTrans.FindChild("Image" + (j + 1)).get_gameObject().SetActive(false);
			}
		}
		else if (this.excellentTrans != null && this.excellentTrans.get_gameObject().get_activeSelf())
		{
			this.excellentTrans.get_gameObject().SetActive(false);
		}
		this.equipFxID = EquipGlobal.GetEquipIconFX(this.m_itemID, count, this.m_imageIcon.get_transform(), "EquipRecommendItem", this.showUIDepth, false);
	}

	public void SetEquipBinding(bool isShow = false)
	{
		Transform transform = base.FindTransform("ImageBinding");
		if (transform != null && transform.get_gameObject() != null && transform.get_gameObject().get_activeSelf() != isShow)
		{
			transform.get_gameObject().SetActive(isShow);
		}
	}
}
