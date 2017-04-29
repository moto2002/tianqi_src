using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WingUI : UIBase
{
	private const int pageCount = 2;

	public static WingUI instance;

	public static int wingIdLast;

	private Transform m_btnPage_1Badge;

	private Transform m_btnPage_2Badge;

	private void Awake()
	{
		WingUI.instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_btnPage_1Badge = base.FindTransform("btnPage_1Badge");
		this.m_btnPage_2Badge = base.FindTransform("btnPage_2Badge");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		if (EntityWorld.Instance.EntSelf != null)
		{
			WingUI.wingIdLast = EntityWorld.Instance.EntSelf.Decorations.wingId;
		}
		this.InitButtonListener();
		this.SetBtnPageHightlight(1);
		this.CheckBadge();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			WingGlobal.ResetRawImage();
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnVisible(GameObject sender)
	{
		if (this.IsHaveWearWing())
		{
			WingUI.wingIdLast = EntityWorld.Instance.EntSelf.Decorations.wingId;
			WingManager.Instance.SendWingHiddenReq(true);
		}
		else
		{
			WingManager.Instance.SendWingHiddenReq(false);
		}
	}

	private void OnClickBtnPage(GameObject sender)
	{
		int num = int.Parse(sender.get_name().Split(new char[]
		{
			'_'
		})[1]);
		this.SetBtnPageHightlight(num);
		if (num == 1)
		{
			UIManagerControl.Instance.HideUI("WingSelectUI");
			Transform parent = base.FindTransform("RootWingUpgradeUI");
			WingUpgradeUI wingUpgradeUI = UIManagerControl.Instance.OpenUI("WingUpgradeUI", parent, false, UIType.NonPush) as WingUpgradeUI;
			wingUpgradeUI.Refresh();
		}
		else
		{
			UIManagerControl.Instance.HideUI("WingUpgradeUI");
			Transform parent2 = base.FindTransform("RootWingSelectUI");
			UIManagerControl.Instance.OpenUI("WingSelectUI", parent2, false, UIType.NonPush);
		}
	}

	public void Refresh()
	{
		string spriteName = (!this.IsHaveWearWing()) ? "c_winglook_on" : "c_winglook_off";
		ResourceManager.SetSprite(base.FindTransform("btnVisible").GetComponent<Image>(), ResourceManager.GetIconSprite(spriteName));
	}

	private void InitButtonListener()
	{
		for (int i = 0; i < 2; i++)
		{
			int num = i + 1;
			Button btnPage = base.FindTransform("btnPage_" + num).GetComponent<Button>();
			btnPage.get_onClick().RemoveAllListeners();
			btnPage.get_onClick().AddListener(delegate
			{
				this.OnClickBtnPage(btnPage.get_gameObject());
			});
			if (i == 0)
			{
				this.OnClickBtnPage(btnPage.get_gameObject());
			}
		}
		Transform transform = base.FindTransform("btnVisible");
		transform.get_gameObject().SetActive(false);
		Button btnVisible = transform.GetComponent<Button>();
		btnVisible.get_onClick().RemoveAllListeners();
		btnVisible.get_onClick().AddListener(delegate
		{
			this.OnClickBtnVisible(btnVisible.get_gameObject());
		});
		this.Refresh();
	}

	private bool IsHaveWearWing()
	{
		return EntityWorld.Instance.EntSelf.Decorations.wingId != 0;
	}

	private WingSeries.WS GetWingType(int pageNum)
	{
		if (pageNum == 1)
		{
			return WingSeries.WS.BIOLOGY;
		}
		if (pageNum == 2)
		{
			return WingSeries.WS.GHOST;
		}
		return WingSeries.WS.MACHINE;
	}

	private void SetBtnPageHightlight(int pageNum)
	{
		for (int i = 1; i <= 2; i++)
		{
			Button component = base.FindTransform("btnPage_" + i).GetComponent<Button>();
			string spriteName = (i != pageNum) ? "y_fenye2" : "y_fenye1";
			ResourceManager.SetSprite(component.GetComponent<Image>(), ResourceManager.GetIconSprite(spriteName));
		}
	}

	private int GetMaxWingLv(int id)
	{
		return WingManager.GetWingLvInfos(id).get_Count();
	}

	public void CheckBadge()
	{
		this.ShowPage1Badge(WingManager.CheckPage1Badge());
		this.ShowPage2Badge(WingManager.CheckPage2Badge());
	}

	private void ShowPage1Badge(bool isOn)
	{
		this.m_btnPage_1Badge.get_gameObject().SetActive(isOn);
	}

	private void ShowPage2Badge(bool isOn)
	{
		this.m_btnPage_2Badge.get_gameObject().SetActive(isOn);
	}
}
