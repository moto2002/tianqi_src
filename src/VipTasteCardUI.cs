using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VipTasteCardUI : UIBase
{
	public static VipTasteCardUI Instance;

	private GameObject TasteRegion;

	private GameObject ExpireRegion;

	private GridLayoutGroup m_cardlist;

	private GridLayoutGroup m_expirecardlist;

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
		VipTasteCardUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("CloseBtn").get_gameObject().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClose);
		base.FindTransform("GoBtn").get_gameObject().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnGo);
		base.FindTransform("ExpireGoBtn").get_gameObject().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnExpireGo);
		this.TasteRegion = base.FindTransform("VipTasteRegion").get_gameObject();
		this.ExpireRegion = base.FindTransform("VipExpireRegion").get_gameObject();
		this.m_cardlist = base.FindTransform("CardEffectList").GetComponent<GridLayoutGroup>();
		this.m_expirecardlist = base.FindTransform("ExpireCardEffectList").GetComponent<GridLayoutGroup>();
	}

	private void Start()
	{
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			VipTasteCardUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnGo(GameObject go)
	{
		VipTasteCardManager.Instance.SendUseCard(VipTasteCardManager.Instance.CheckId);
		this.OnClickMaskAction();
	}

	private void OnExpireGo(GameObject go)
	{
		this.OnClickMaskAction();
		LinkNavigationManager.OpenVIPUI2VipLimit();
	}

	private void OnClose(GameObject go)
	{
		this.OnClickMaskAction();
	}

	public void SwitchMode(int mode)
	{
		switch (mode)
		{
		case 1:
			this.TasteRegion.SetActive(true);
			this.ExpireRegion.SetActive(false);
			this.InitTastePanel();
			break;
		case 2:
			this.TasteRegion.SetActive(false);
			this.ExpireRegion.SetActive(true);
			this.InitExpirePanel(false);
			break;
		case 3:
			this.TasteRegion.SetActive(false);
			this.ExpireRegion.SetActive(true);
			this.InitExpirePanel(true);
			break;
		}
	}

	private void InitTastePanel()
	{
		List<vipTiYanQia> dataList = DataReader<vipTiYanQia>.DataList;
		vipTiYanQia vipTiYanQia = dataList.get_Item(0);
		if (dataList == null)
		{
			return;
		}
		List<int> effect = vipTiYanQia.effect;
		string empty = string.Empty;
		int num = -1;
		this.ClearScroll();
		for (int i = 0; i < effect.get_Count(); i++)
		{
			int key = effect.get_Item(i);
			VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(key);
			if (vipXiaoGuo != null)
			{
				string chineseContent = GameDataUtils.GetChineseContent(vipXiaoGuo.name, true);
				if (!string.IsNullOrEmpty(chineseContent))
				{
					num++;
					this.UpdateCardItemInfo(num, chineseContent);
				}
			}
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_cardlist.get_transform().get_childCount(); i++)
		{
			this.m_cardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void UpdateCardItemInfo(int index, string str)
	{
		Transform transform = this.m_cardlist.get_transform().FindChild("Item2SpecialItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<Item2SpecialItem>().m_num.set_text(str);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Item2SpecialItem");
			instantiate2Prefab.get_transform().SetParent(this.m_cardlist.get_transform(), false);
			instantiate2Prefab.set_name("Item2SpecialItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<Item2SpecialItem>().m_num.set_text(str);
		}
	}

	private void InitExpirePanel(bool isMyOwnVip = false)
	{
		List<int> list = null;
		if (isMyOwnVip)
		{
			VipDengJi vipDengJi = DataReader<VipDengJi>.Get(EntityWorld.Instance.EntSelf.VipLv);
			if (vipDengJi != null)
			{
				list = vipDengJi.effect;
			}
		}
		else
		{
			List<vipTiYanQia> dataList = DataReader<vipTiYanQia>.DataList;
			vipTiYanQia vipTiYanQia = dataList.get_Item(0);
			if (dataList != null)
			{
				list = vipTiYanQia.effect;
			}
		}
		string empty = string.Empty;
		int num = -1;
		this.ClearExpireScroll();
		for (int i = 0; i < list.get_Count(); i++)
		{
			int key = list.get_Item(i);
			VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(key);
			if (vipXiaoGuo != null)
			{
				string chineseContent = GameDataUtils.GetChineseContent(vipXiaoGuo.name, true);
				if (!string.IsNullOrEmpty(chineseContent))
				{
					num++;
					this.UpdateExpireCardItemInfo(num, chineseContent);
				}
			}
		}
	}

	private void ClearExpireScroll()
	{
		for (int i = 0; i < this.m_expirecardlist.get_transform().get_childCount(); i++)
		{
			this.m_expirecardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void UpdateExpireCardItemInfo(int index, string str)
	{
		Transform transform = this.m_expirecardlist.get_transform().FindChild("ExpireItem2SpecialItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<Item2SpecialItem>().m_num.set_text(str);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Item2SpecialItem");
			instantiate2Prefab.get_transform().SetParent(this.m_expirecardlist.get_transform(), false);
			instantiate2Prefab.set_name("ExpireItem2SpecialItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<Item2SpecialItem>().m_num.set_text(str);
		}
	}
}
