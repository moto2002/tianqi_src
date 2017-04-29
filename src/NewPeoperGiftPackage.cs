using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NewPeoperGiftPackage : UIBase
{
	public static NewPeoperGiftPackage Instance;

	private ListPool m_ItemPool;

	private int m_effectID = -1;

	private void Start()
	{
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		NewPeoperGiftPackage.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClosePanel);
		base.FindTransform("ButtonBuyPro").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBuyProductClicked);
		base.FindTransform("ButtonBuyPro").GetComponent<ButtonCustom>().set_enabled(true);
		this.m_ItemPool = base.FindTransform("Items").GetComponent<ListPool>();
		this.m_ItemPool.SetItem("ItemShow");
		this.initView();
	}

	public void initView()
	{
		NovicePacksPush novicePacksPush = NewPeoperGiftPackageManager.Instance.getNovicePacksPush();
		if (novicePacksPush == null)
		{
			Debug.LogWarning("服务器推送的NovicePacksPush数据null，检测客户端或者服务器推送的数据.");
			return;
		}
		base.FindTransform("ButtonBuyPro").FindChild("BtnConfirmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(301027, false));
		XinShouLiBao xinShouLiBao = DataReader<XinShouLiBao>.Get(novicePacksPush.pack.id);
		if (xinShouLiBao != null)
		{
			Icon icon = DataReader<Icon>.Get(xinShouLiBao.Chinese);
			if (icon != null)
			{
				ResourceManager.SetSprite(base.FindTransform("IconImage").GetComponent<Image>(), ResourceManager.GetIconSprite(icon.icon));
			}
		}
		base.FindTransform("Count").GetComponent<Text>().set_text("X" + novicePacksPush.pack.price);
		ListPool pool = this.m_ItemPool;
		pool.Release();
		pool.Create(novicePacksPush.pack.items.get_Count(), delegate(int index)
		{
			DropItem dropItem = novicePacksPush.pack.items.get_Item(index);
			int typeId = dropItem.typeId;
			long count = dropItem.count;
			ItemShow.SetItem(pool.Items.get_Item(index), typeId, count, false, UINodesManager.T2RootOfSpecial, 3000);
		});
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ActionClose()
	{
		base.ActionClose();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
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

	private void OnClosePanel(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnBuyProductClicked(GameObject go)
	{
		NovicePacksPush novicePacksPush = NewPeoperGiftPackageManager.Instance.getNovicePacksPush();
		if (novicePacksPush.pack.price > EntityWorld.Instance.EntSelf.Diamond)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(510106, false), GameDataUtils.GetChineseContent(510107, false), delegate
			{
			}, delegate
			{
				this.OnClickMaskAction();
				LinkNavigationManager.OpenVIPUI2Recharge();
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			base.FindTransform("ButtonBuyPro").GetComponent<ButtonCustom>().set_enabled(false);
			NewPeoperGiftPackageManager.Instance.sendBuyDiscountItemReq();
		}
	}

	public void updateTimeCoundDown(string formatTime)
	{
		base.FindTransform("ActivityTitleTime").GetComponent<Text>().set_text(formatTime);
	}

	public void OnCloseCurrentUI()
	{
		this.OnClickMaskAction();
	}

	public void showEffect(bool isNeedInit = false)
	{
		if (this.m_effectID != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_effectID, true);
		}
		this.m_effectID = FXSpineManager.Instance.PlaySpine(4505, base.FindTransform("Icon"), "ActivityTossDiscountUI", 3001, delegate
		{
			this.FindTransform("ButtonBuyPro").GetComponent<ButtonCustom>().set_enabled(true);
			if (isNeedInit)
			{
				this.initView();
			}
		}, "UI", -65f, 55f, 1.24f, 1.26f, false, FXMaskLayer.MaskState.None);
	}
}
