using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodSoldierUI : UIBase
{
	private GameObject mItemGrid;

	private GameObject mToggleGrid;

	private GameObject mAttrPanel;

	private Text mTxExp;

	private Slider mExpSlider;

	private ButtonCustom mBtnUpGrade;

	private Text mTxUpGrade;

	private Text mTxAttrTitle;

	private GameObject mGoMaxLevel;

	private GameObject mGoAttrPrefab;

	private Text mTxOpenTips;

	private Text mTxPower;

	private int mCurGodType;

	private List<GodSoldierItem> mItemList;

	private List<GodSoldierToggle> mToggleList;

	private List<GameObject> mAttrList;

	private GodSoldierItem mLastSelectItem;

	private GodSoldierToggle mLastSelectToggle;

	private ButtonCustom mEffectPanel;

	private Image mBody;

	private Image mLock;

	private int mFxBottom1Id;

	private int mFxBottom2Id;

	private int mFxBottom3Id;

	private int mFxOpenGod1Id;

	private int mLastGodPower1;

	private int mLastGodPower2;

	private int mLastGodPower3;

	private SShenBingPeiZhi mSelectData;

	public int LastPower
	{
		get
		{
			switch (this.mCurGodType)
			{
			case 1:
				return this.mLastGodPower1;
			case 2:
				return this.mLastGodPower2;
			case 3:
				return this.mLastGodPower3;
			default:
				return 0;
			}
		}
		set
		{
			switch (this.mCurGodType)
			{
			case 1:
				this.mLastGodPower1 = value;
				break;
			case 2:
				this.mLastGodPower2 = value;
				break;
			case 3:
				this.mLastGodPower3 = value;
				break;
			}
		}
	}

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110043), string.Empty, delegate
		{
			this.Show(false);
			SoundManager.SetBGMFade(true);
		}, false);
		this.SwitchToggle(1);
		WaitUI.CloseUI(0u);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mItemList = new List<GodSoldierItem>();
		this.mToggleList = new List<GodSoldierToggle>();
		this.mAttrList = new List<GameObject>();
		this.mItemGrid = base.FindTransform("Grid").get_gameObject();
		this.mToggleGrid = base.FindTransform("Toggles").get_gameObject();
		this.mAttrPanel = base.FindTransform("AttrPanel").get_gameObject();
		List<GodWeaponInfo> godList = GodSoldierManager.Instance.GodList;
		if (godList != null)
		{
			for (int i = 0; i < godList.get_Count(); i++)
			{
				this.CreateToggle(godList.get_Item(i));
			}
		}
		this.mEffectPanel = base.FindTransform("EffectPanel").GetComponent<ButtonCustom>();
		this.mBody = base.FindTransform("Body").GetComponent<Image>();
		this.mLock = base.FindTransform("Lock").GetComponent<Image>();
		this.mTxExp = base.FindTransform("txExp").GetComponent<Text>();
		this.mExpSlider = base.FindTransform("ExpSlider").GetComponent<Slider>();
		this.mBtnUpGrade = base.FindTransform("BtnUpGrade").GetComponent<ButtonCustom>();
		this.mTxUpGrade = base.FindTransform("txUpGrade").GetComponent<Text>();
		this.mTxAttrTitle = base.FindTransform("txAttrTitle").GetComponent<Text>();
		this.mGoMaxLevel = base.FindTransform("MaxLevel").get_gameObject();
		this.mGoAttrPrefab = base.FindTransform("AttrItem").get_gameObject();
		this.mTxOpenTips = base.FindTransform("txOpenTips").GetComponent<Text>();
		this.mTxPower = base.FindTransform("txPower").GetComponent<Text>();
		this.mBtnUpGrade.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUpGrade);
		this.mEffectPanel.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEffect);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener<int>(EventNames.GodSoldierListNty, new Callback<int>(this.OnGodListPush));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener<int>(EventNames.GodSoldierListNty, new Callback<int>(this.OnGodListPush));
	}

	private void OnClickMenu(GodSoldierToggle toggle)
	{
		this.SwitchToggle(toggle.Data.Type);
	}

	private void OnClickUpGrade(GameObject go)
	{
		if (GodSoldierManager.Instance.WaitUpGradeRes)
		{
			return;
		}
		if (this.mLastSelectItem != null)
		{
			int itemId2 = this.mLastSelectItem.ItemId;
			if (BackpackManager.Instance.OnGetGoodCount(itemId2) > 0L)
			{
				GodSoldierManager.Instance.SendUpGradeGodSoldier(this.mCurGodType, itemId2);
			}
			else
			{
				if (this.mSelectData != null)
				{
					int itemId;
					for (int i = 0; i < this.mSelectData.material.get_Count(); i++)
					{
						itemId = this.mSelectData.material.get_Item(i);
						if (BackpackManager.Instance.OnGetGoodCount(itemId) > 0L)
						{
							this.OnClickItem(this.mItemList.Find((GodSoldierItem e) => e.ItemId == itemId));
							GodSoldierManager.Instance.SendUpGradeGodSoldier(this.mCurGodType, itemId);
							return;
						}
					}
				}
				LinkNavigationManager.ItemNotEnoughToLink(itemId2, true, null, true);
			}
		}
	}

	private void OnClickItem(GodSoldierItem item)
	{
		if (this.mLastSelectItem != null)
		{
			this.mLastSelectItem.IsSelect = false;
		}
		this.mLastSelectItem = item;
		if (this.mLastSelectItem != null)
		{
			this.mLastSelectItem.IsSelect = true;
		}
	}

	private void OnGodListPush(int type)
	{
		GodSoldierToggle godSoldierToggle = this.mToggleList.Find((GodSoldierToggle e) => e.Data.Type == type);
		if (godSoldierToggle != null)
		{
			GodWeaponInfo godWeaponInfo = GodSoldierManager.Instance.GodList.Find((GodWeaponInfo e) => e.Type == type);
			if (godWeaponInfo != null)
			{
				this.RefreshLeft(godWeaponInfo);
				this.RefreshRight(godWeaponInfo, false);
				godSoldierToggle.SetData(godWeaponInfo, string.Empty);
			}
		}
	}

	private void RefreshPowerNumber(int type, List<int> attrIds, List<int> attrValues, bool isSwitch)
	{
		int num = (int)EquipGlobal.CalculateFightingByIDAndValue(attrIds, attrValues);
		ChangeNumAnim changeNumAnim = this.mTxPower.GetComponent<ChangeNumAnim>();
		if (isSwitch)
		{
			if (changeNumAnim != null)
			{
				changeNumAnim.set_enabled(false);
			}
			this.mTxPower.set_text(string.Format("战力：<color=#FFEB4B>{0}</color>", num));
		}
		else
		{
			if (changeNumAnim == null)
			{
				changeNumAnim = this.mTxPower.get_gameObject().AddComponent<ChangeNumAnim>();
			}
			else
			{
				changeNumAnim.set_enabled(true);
			}
			changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.mTxPower, (long)this.LastPower, (long)num, null, delegate(string resultStr)
			{
				this.mTxPower.set_text(string.Format("战力：<color=#FFEB4B>{0}</color>", resultStr));
			}, null);
		}
		this.LastPower = num;
	}

	private void CreateToggle(GodWeaponInfo data)
	{
		SShenBingPeiZhi sShenBingPeiZhi = DataReader<SShenBingPeiZhi>.Get(data.Type);
		if (sShenBingPeiZhi != null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GodSoldierToggle");
			UGUITools.SetParent(this.mToggleGrid, instantiate2Prefab, false);
			instantiate2Prefab.set_name(sShenBingPeiZhi.id.ToString());
			instantiate2Prefab.SetActive(true);
			GodSoldierToggle component = instantiate2Prefab.GetComponent<GodSoldierToggle>();
			component.SetData(data, GameDataUtils.GetChineseContent(sShenBingPeiZhi.name, false));
			component.EventHandler = new Action<GodSoldierToggle>(this.OnClickMenu);
			this.mToggleList.Add(component);
		}
	}

	private void SwitchToggle(int type)
	{
		if (this.mCurGodType == type)
		{
			return;
		}
		this.mCurGodType = type;
		if (this.mLastSelectToggle != null)
		{
			this.mLastSelectToggle.IsSelect = false;
		}
		for (int i = 0; i < this.mToggleList.get_Count(); i++)
		{
			if (type == this.mToggleList.get_Item(i).Data.Type)
			{
				this.mLastSelectToggle = this.mToggleList.get_Item(i);
				this.mLastSelectToggle.IsSelect = true;
				this.RefreshLeft(this.mLastSelectToggle.Data);
				this.RefreshRight(this.mLastSelectToggle.Data, true);
			}
		}
	}

	private void RefreshLeft(GodWeaponInfo data)
	{
		if (data != null)
		{
			if (this.mLastSelectToggle != null && !this.mLastSelectToggle.Data.isOpen && data.isOpen)
			{
				this.UnLockingEffect();
				TimerHeap.AddTimer(500u, 0, delegate
				{
					this.UnLockedEffect(data.Type);
					this.BottomEffect(data.isOpen);
				});
			}
			else if (data.isOpen)
			{
				this.UnLockedEffect(data.Type);
				this.BottomEffect(data.isOpen);
			}
			else
			{
				this.LockedEffect(data.Type);
				this.BottomEffect(data.isOpen);
			}
		}
	}

	private void RefreshRight(GodWeaponInfo info, bool isSwitch = false)
	{
		if (info != null)
		{
			this.RefreshMaterial(info, isSwitch);
			this.RefreshDesc(info, isSwitch);
		}
	}

	private void RefreshMaterial(GodWeaponInfo info, bool isSwitch)
	{
		this.mSelectData = DataReader<SShenBingPeiZhi>.Get(this.mCurGodType);
		if (this.mSelectData != null)
		{
			if (isSwitch)
			{
				this.OnClickItem(null);
				this.ClearItem();
				int itemId;
				for (int i = 0; i < this.mSelectData.material.get_Count(); i++)
				{
					itemId = this.mSelectData.material.get_Item(i);
					long num = BackpackManager.Instance.OnGetGoodCount(itemId);
					if (this.mLastSelectItem == null && num > 0L)
					{
						this.OnClickItem(this.CreateItem(itemId, num));
					}
					else
					{
						this.CreateItem(itemId, num);
					}
				}
				if (this.mLastSelectItem == null)
				{
					this.OnClickItem(this.mItemList.get_Item(0));
				}
			}
			else if (this.mLastSelectItem != null)
			{
				long num = BackpackManager.Instance.OnGetGoodCount(this.mLastSelectItem.ItemId);
				this.mLastSelectItem.Number = num.ToString();
				if (num <= 0L)
				{
					int itemId;
					for (int j = 0; j < this.mSelectData.material.get_Count(); j++)
					{
						itemId = this.mSelectData.material.get_Item(j);
						if (BackpackManager.Instance.OnGetGoodCount(itemId) > 0L)
						{
							this.OnClickItem(this.mItemList.Find((GodSoldierItem e) => e.ItemId == itemId));
							break;
						}
					}
				}
			}
		}
	}

	private GodSoldierItem CreateItem(int itemId, long itemNum = 0L)
	{
		GodSoldierItem godSoldierItem = this.mItemList.Find((GodSoldierItem e) => e.get_name() == "Unused");
		if (godSoldierItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GodSoldierItem");
			UGUITools.SetParent(this.mItemGrid, instantiate2Prefab, false);
			godSoldierItem = instantiate2Prefab.GetComponent<GodSoldierItem>();
			this.mItemList.Add(godSoldierItem);
		}
		godSoldierItem.get_gameObject().set_name(itemId.ToString());
		godSoldierItem.get_gameObject().SetActive(true);
		godSoldierItem.SetData(itemId, itemNum);
		godSoldierItem.EventHandler = new Action<GodSoldierItem>(this.OnClickItem);
		return godSoldierItem;
	}

	private void ClearItem()
	{
		for (int i = 0; i < this.mItemList.get_Count(); i++)
		{
			this.mItemList.get_Item(i).SetUnused();
		}
	}

	private void CreateAttr(int key, int value, bool isShowNext = false, int nextValue = 0)
	{
		GameObject gameObject = this.mAttrList.Find((GameObject e) => e.get_name() == "Unused");
		if (gameObject == null)
		{
			gameObject = Object.Instantiate<GameObject>(this.mGoAttrPrefab);
			UGUITools.SetParent(this.mAttrPanel, gameObject, false);
			this.mAttrList.Add(gameObject);
		}
		gameObject.get_gameObject().set_name(key.ToString());
		gameObject.get_gameObject().SetActive(true);
		gameObject.get_transform().FindChild("Attr").GetComponentInChildren<Text>().set_text(AttrUtility.GetStandardDesc(key, value, "ff7d4b"));
		GameObject gameObject2 = gameObject.get_transform().FindChild("Next").get_gameObject();
		if (isShowNext)
		{
			gameObject2.SetActive(true);
			gameObject2.GetComponentInChildren<Text>().set_text(AttrUtility.GetAttrValueDisplay(key, nextValue));
		}
		else
		{
			gameObject2.SetActive(false);
		}
	}

	private void ClearAttr()
	{
		for (int i = 0; i < this.mAttrList.get_Count(); i++)
		{
			this.mAttrList.get_Item(i).set_name("Unused");
			this.mAttrList.get_Item(i).SetActive(false);
		}
	}

	private void RefreshDesc(GodWeaponInfo data, bool isSwitch)
	{
		if (GodSoldierManager.Instance.DengjiDict.ContainsKey(data.Type))
		{
			Dictionary<int, SShenBingDengJi> dictionary = GodSoldierManager.Instance.DengjiDict.get_Item(data.Type);
			SShenBingDengJi sShenBingDengJi = null;
			SShenBingDengJi sShenBingDengJi2 = null;
			if (data.isOpen)
			{
				sShenBingDengJi = dictionary.get_Item(data.gLevel);
			}
			if (data.gLevel < dictionary.get_Count())
			{
				sShenBingDengJi2 = dictionary.get_Item(data.gLevel + 1);
			}
			if (sShenBingDengJi == null)
			{
				this.mTxExp.set_text(data.gExp + "/" + sShenBingDengJi2.EXP);
				this.mExpSlider.set_value((float)data.gExp / (float)sShenBingDengJi2.EXP);
				this.mBtnUpGrade.get_gameObject().SetActive(true);
				this.mGoMaxLevel.SetActive(false);
				this.mTxOpenTips.get_gameObject().SetActive(true);
				this.mTxOpenTips.set_text(this.mLastSelectToggle.Title + "解除封印后获得");
				this.mTxAttrTitle.set_text("解除封印");
				this.mTxUpGrade.set_text("解除封印");
			}
			else if (sShenBingDengJi2 == null)
			{
				this.mTxExp.set_text("MAX");
				this.mExpSlider.set_value(1f);
				this.mBtnUpGrade.get_gameObject().SetActive(false);
				this.mGoMaxLevel.SetActive(true);
				this.mTxOpenTips.get_gameObject().SetActive(true);
				this.mTxOpenTips.set_text("神兵已最大等级");
				this.mTxAttrTitle.set_text("最大等级");
			}
			else if (sShenBingDengJi != null && sShenBingDengJi2 != null)
			{
				this.mTxExp.set_text(data.gExp + "/" + sShenBingDengJi2.EXP);
				this.mExpSlider.set_value((float)data.gExp / (float)sShenBingDengJi2.EXP);
				this.mBtnUpGrade.get_gameObject().SetActive(true);
				this.mGoMaxLevel.SetActive(false);
				this.mTxOpenTips.get_gameObject().SetActive(false);
				this.mTxAttrTitle.set_text("升级属性");
				this.mTxUpGrade.set_text("神兵升级");
				this.UpGradeEffect(data.gLevel);
			}
			this.ClearAttr();
			if (sShenBingDengJi != null && sShenBingDengJi2 != null)
			{
				Attrs attrs = DataReader<Attrs>.Get(sShenBingDengJi.attrID);
				Attrs attrs2 = DataReader<Attrs>.Get(sShenBingDengJi2.attrID);
				if (attrs != null && attrs2 != null)
				{
					for (int i = 0; i < attrs.attrs.get_Count(); i++)
					{
						this.CreateAttr(attrs.attrs.get_Item(i), attrs.values.get_Item(i), true, attrs2.values.get_Item(i));
					}
					this.RefreshPowerNumber(data.Type, attrs.attrs, attrs.values, isSwitch);
				}
			}
			else
			{
				Attrs attrs3 = null;
				if (sShenBingDengJi != null)
				{
					attrs3 = DataReader<Attrs>.Get(sShenBingDengJi.attrID);
				}
				else if (sShenBingDengJi2 != null)
				{
					attrs3 = DataReader<Attrs>.Get(sShenBingDengJi2.attrID);
				}
				if (attrs3 != null)
				{
					for (int j = 0; j < attrs3.attrs.get_Count(); j++)
					{
						this.CreateAttr(attrs3.attrs.get_Item(j), attrs3.values.get_Item(j), false, 0);
					}
					this.RefreshPowerNumber(data.Type, attrs3.attrs, attrs3.values, isSwitch);
				}
			}
		}
	}

	private void UnLockingEffect()
	{
		FXSpineManager.Instance.PlaySpine(3710, this.mEffectPanel.get_transform(), "GodSoldierUI", 2004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void UnLockedEffect(int type)
	{
		ResourceManager.SetSprite(this.mBody, ResourceManager.GetIconSprite("shenbing_" + type));
		this.mBody.set_color(Color.get_white());
		this.mBody.SetNativeSize();
		this.mBody.GetComponent<Animator>().Play("GodSoldierAnim");
		int templateId = 0;
		int num = 0;
		int num2 = 0;
		this.GetGodFxId(type, ref templateId, ref num, ref num2);
		if (this.mBody.get_transform().FindChild(templateId.ToString()) == null)
		{
			FXSpineManager.Instance.DeleteSpine(this.mFxOpenGod1Id, true);
			this.mFxOpenGod1Id = FXSpineManager.Instance.PlaySpine(templateId, this.mBody.get_transform(), "GodSoldierUI", 2002, null, "UI", (float)num, (float)num2, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		this.mLock.get_gameObject().SetActive(false);
	}

	private void GetGodFxId(int type, ref int id, ref int offsetX, ref int offsetY)
	{
		switch (type)
		{
		case 1:
			id = 3703;
			offsetX = 5;
			offsetY = 19;
			break;
		case 2:
			id = 3707;
			offsetX = 2;
			offsetY = 36;
			break;
		case 3:
			id = 3705;
			offsetX = 29;
			offsetY = 20;
			break;
		}
	}

	private void LockedEffect(int type)
	{
		ResourceManager.SetSprite(this.mBody, ResourceManager.GetIconSprite("shenbing_" + type));
		this.mBody.SetNativeSize();
		this.mBody.set_color(new Color(0.4f, 0.4f, 0.4f));
		this.mBody.GetComponent<Animator>().Play("static");
		this.mLock.get_gameObject().SetActive(true);
		FXSpineManager.Instance.DeleteSpine(this.mFxOpenGod1Id, true);
		this.mFxOpenGod1Id = 0;
	}

	private void BottomEffect(bool isOpen)
	{
		Transform transform = this.mEffectPanel.get_transform().FindChild("3701");
		if (transform == null && isOpen)
		{
			this.mFxBottom1Id = FXSpineManager.Instance.PlaySpine(3701, this.mEffectPanel.get_transform(), "GodSoldierUI", 2000, null, "UI", 0f, -205f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mFxBottom2Id = FXSpineManager.Instance.PlaySpine(3711, this.mEffectPanel.get_transform(), "GodSoldierUI", 2000, null, "UI", 0f, -210f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mFxBottom3Id = FXSpineManager.Instance.PlaySpine(3712, this.mEffectPanel.get_transform(), "GodSoldierUI", 2003, null, "UI", 0f, -260f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (transform != null && !isOpen)
		{
			FXSpineManager.Instance.DeleteSpine(this.mFxBottom1Id, true);
			FXSpineManager.Instance.DeleteSpine(this.mFxBottom2Id, true);
			FXSpineManager.Instance.DeleteSpine(this.mFxBottom3Id, true);
		}
	}

	private void OnClickEffect(GameObject go)
	{
		UIStateSystem.LockOfClickInterval(200u);
		Vector2 vector;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(base.get_transform().get_parent().get_transform() as RectTransform, Input.get_mousePosition(), base.get_transform().get_parent().GetComponent<Canvas>().get_worldCamera(), ref vector))
		{
			FXSpineManager.Instance.PlaySpine(3702, base.FindTransform("LeftPanel"), "GodSoldierUI", 2004, null, "UI", vector.x + 300f, vector.y, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void UpGradeEffect(int level)
	{
		if (this.mLastSelectToggle != null && this.mLastSelectToggle.Data.gLevel < level)
		{
			FXSpineManager.Instance.PlaySpine(2206, this.mExpSlider.get_transform(), "GodSoldierUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}
}
