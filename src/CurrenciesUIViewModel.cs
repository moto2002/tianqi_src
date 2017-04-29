using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class CurrenciesUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Diamond = "Diamond";

		public const string Attr_Gold = "Gold";

		public const string Attr_Strength = "Strength";

		public const string Attr_ShowSubUI = "ShowSubUI";

		public const string Attr_ShowCurrenciesClass = "ShowCurrenciesClass";

		public const string Attr_BtnBackName = "BtnBackName";

		public const string Attr_SubUIName = "SubUIName";

		public const string Attr_ShowGoldAdd = "ShowGoldAdd";

		public const string Attr_ShowDiamondAdd = "ShowDiamondAdd";

		public const string Event_OnClickDiamond = "OnClickDiamond";

		public const string Event_OnClickGold = "OnClickGold";

		public const string Event_OnClickStrength = "OnClickStrength";

		public const string Event_OnClickBack = "OnClickBack";
	}

	private const int DEFAULT_NUM = -1;

	private static CurrenciesUIViewModel m_instance;

	private long m_oldGold = -1L;

	private int m_oldDiamond = -1;

	private string _Diamond;

	private string _Gold;

	private string _Strength;

	private bool _ShowSubUI;

	private bool _ShowCurrenciesClass;

	private bool _ShowGoldAdd;

	private bool _ShowDiamondAdd;

	private string _BtnBackName;

	private SpriteRenderer _SubUIName;

	private Action callback;

	public static CurrenciesUIViewModel Instance
	{
		get
		{
			if (CurrenciesUIViewModel.m_instance == null)
			{
				UIManagerControl.Instance.OpenUI("CurrenciesUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
			}
			return CurrenciesUIViewModel.m_instance;
		}
		set
		{
			CurrenciesUIViewModel.m_instance = null;
		}
	}

	public bool IsCurrenciesBtnClickOff
	{
		get;
		set;
	}

	public string Diamond
	{
		get
		{
			return this._Diamond;
		}
		set
		{
			this._Diamond = value;
			base.NotifyProperty("Diamond", value);
		}
	}

	public string Gold
	{
		get
		{
			return this._Gold;
		}
		set
		{
			this._Gold = value;
			base.NotifyProperty("Gold", value);
		}
	}

	public string Strength
	{
		get
		{
			return this._Strength;
		}
		set
		{
			this._Strength = value;
			base.NotifyProperty("Strength", value);
		}
	}

	public bool ShowSubUI
	{
		get
		{
			return this._ShowSubUI;
		}
		set
		{
			this._ShowSubUI = value;
			base.NotifyProperty("ShowSubUI", value);
		}
	}

	public bool ShowCurrenciesClass
	{
		get
		{
			return this._ShowCurrenciesClass;
		}
		set
		{
			this._ShowCurrenciesClass = value;
			base.NotifyProperty("ShowCurrenciesClass", value);
		}
	}

	public bool ShowGoldAdd
	{
		get
		{
			return this._ShowGoldAdd;
		}
		set
		{
			this._ShowGoldAdd = value;
			base.NotifyProperty("ShowGoldAdd", value);
		}
	}

	public bool ShowDiamondAdd
	{
		get
		{
			return this._ShowDiamondAdd;
		}
		set
		{
			this._ShowDiamondAdd = value;
			base.NotifyProperty("ShowDiamondAdd", value);
		}
	}

	public string BtnBackName
	{
		get
		{
			return this._BtnBackName;
		}
		set
		{
			this._BtnBackName = value;
			base.NotifyProperty("BtnBackName", value);
		}
	}

	public SpriteRenderer SubUIName
	{
		get
		{
			return this._SubUIName;
		}
		set
		{
			this._SubUIName = value;
			base.NotifyProperty("SubUIName", value);
		}
	}

	private void SetGold(long gold)
	{
		if (this.m_oldGold != -1L && gold > this.m_oldGold)
		{
			SoundManager.PlayUI(10010, false);
		}
		this.m_oldGold = gold;
	}

	private void SetDiamond(int diamond)
	{
		if (this.m_oldDiamond != -1 && diamond > this.m_oldDiamond)
		{
			SoundManager.PlayUI(10011, false);
		}
		this.m_oldDiamond = diamond;
	}

	private void SetStrength(int strength)
	{
	}

	protected override void Awake()
	{
		base.Awake();
		CurrenciesUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
		this.RefreshUI();
	}

	private void OnDisable()
	{
		this._ShowCurrenciesClass = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		this.callback = null;
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.EnergyChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnGetRoleAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.EnergyChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnGetRoleAttrChangedNty));
	}

	private void OnGetRoleAttrChangedNty()
	{
		this.RefreshUI();
	}

	public void OnClickDiamond()
	{
		if (this.IsCurrenciesBtnClickOff)
		{
			return;
		}
		if (UIManagerControl.Instance.IsOpen("PrivilegeUI"))
		{
			return;
		}
		LinkNavigationManager.OpenVIPUI2Recharge();
	}

	public void OnClickGold()
	{
		if (this.IsCurrenciesBtnClickOff)
		{
			return;
		}
		if (!SystemOpenManager.IsSystemClickOpen(29, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("GoldBuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public void OnClickStrength()
	{
		if (this.IsCurrenciesBtnClickOff)
		{
			return;
		}
		if (!SystemOpenManager.IsSystemClickOpen(31, 0, true))
		{
			return;
		}
		EnergyManager.Instance.BuyEnergy(null);
	}

	public void OnClickBack()
	{
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
		else
		{
			UIStackManager.Instance.PopUIPrevious(UIType.FullScreen);
		}
	}

	public static void Show(bool isShow)
	{
		if (isShow)
		{
			if (CurrenciesUIView.Instance != null)
			{
				CurrenciesUIView.Instance.Show(true);
			}
			else
			{
				UIManagerControl.Instance.OpenUI("CurrenciesUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
			}
			if (CurrenciesUIViewModel.Instance != null)
			{
				CurrenciesUIViewModel.Instance.ShowAsTopCenter(isShow);
			}
		}
		else if (CurrenciesUIView.Instance != null)
		{
			CurrenciesUIView.Instance.Show(false);
		}
	}

	public void SetSubUI(bool isShow)
	{
		this.SetSubUI(isShow, null, string.Empty, null, false);
	}

	public void SetSubUI(bool isShow, SpriteRenderer uiName, string btnName, Action action, bool isClickOff = false)
	{
		this.callback = action;
		this.ShowSubUI = isShow;
		if (isShow)
		{
			this.SetName(uiName);
			this.SetCurrenciesBtnClickOff(isClickOff);
			this.BtnBackName = btnName;
		}
		this.ShowGoldAdd = SystemConfig.IsOpenPay;
		this.ShowDiamondAdd = SystemConfig.IsOpenPay;
	}

	public void SetName(SpriteRenderer uiName)
	{
		this.SubUIName = uiName;
	}

	public void SetCurrenciesBtnClickOff(bool isClickOff)
	{
		this.IsCurrenciesBtnClickOff = isClickOff;
	}

	private void RefreshUI()
	{
		if (EntityWorld.Instance.EntSelf != null)
		{
			this.SetGold(EntityWorld.Instance.EntSelf.Gold);
			this.SetDiamond(EntityWorld.Instance.EntSelf.Diamond);
			this.SetStrength(EntityWorld.Instance.EntSelf.Energy);
			this.Gold = AttrUtility.GetAttrValueDisplay(AttrType.Gold, EntityWorld.Instance.EntSelf.Gold);
			this.Diamond = EntityWorld.Instance.EntSelf.Diamond.ToString();
			this.Strength = string.Format("{0}/{1}", EntityWorld.Instance.EntSelf.Energy, EntityWorld.Instance.EntSelf.EnergyLmt);
		}
	}

	private void ShowAsTopCenter(bool isShow)
	{
		if (isShow)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			component.set_anchorMin(new Vector2(0f, 1f));
			component.set_anchorMax(new Vector2(1f, 1f));
			component.set_pivot(new Vector2(0.5f, 0f));
			component.set_anchoredPosition(new Vector3(0f, 0f));
			component.set_localScale(Vector3.get_one());
			base.get_transform().SetAsLastSibling();
		}
	}
}
