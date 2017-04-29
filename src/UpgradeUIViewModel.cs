using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_StarIcon = "StarIcon";

		public const string Attr_PetName = "PetName";

		public const string Attr_FightingNum1 = "FightingNum1";

		public const string Attr_FightingNum2 = "FightingNum2";

		public const string Attr_UpgradeUnits = "UpgradeUnits";

		public const string Attr_ShowNaturalRegion = "ShowNaturalRegion";

		public const string Attr_NaturalIcon = "NaturalIcon";

		public const string Attr_NaturalName = "NaturalName";

		public const string Attr_NaturalDesc = "NaturalDesc";

		public const string Event_OnBtnComfirmClick = "OnBtnComfirmClick";
	}

	private static UpgradeUIViewModel m_instance;

	private SpriteRenderer _StarIcon;

	private string _PetName;

	private string _FightingNum1;

	private string _FightingNum2;

	private bool _ShowNaturalRegion;

	private SpriteRenderer _NaturalIcon;

	private string _NaturalName;

	private string _NaturalDesc;

	public ObservableCollection<OOUpgradeUnit> UpgradeUnits = new ObservableCollection<OOUpgradeUnit>();

	public static UpgradeUIViewModel Instance
	{
		get
		{
			if (UpgradeUIViewModel.m_instance == null)
			{
				UpgradeUIViewModel.Open();
			}
			return UpgradeUIViewModel.m_instance;
		}
	}

	public SpriteRenderer StarIcon
	{
		get
		{
			return this._StarIcon;
		}
		set
		{
			this._StarIcon = value;
			base.NotifyProperty("StarIcon", value);
		}
	}

	public string PetName
	{
		get
		{
			return this._PetName;
		}
		set
		{
			this._PetName = value;
			base.NotifyProperty("PetName", value);
		}
	}

	public string FightingNum1
	{
		get
		{
			return this._FightingNum1;
		}
		set
		{
			this._FightingNum1 = value;
			base.NotifyProperty("FightingNum1", value);
		}
	}

	public string FightingNum2
	{
		get
		{
			return this._FightingNum2;
		}
		set
		{
			this._FightingNum2 = value;
			base.NotifyProperty("FightingNum2", value);
		}
	}

	public bool ShowNaturalRegion
	{
		get
		{
			return this._ShowNaturalRegion;
		}
		set
		{
			this._ShowNaturalRegion = value;
			base.NotifyProperty("ShowNaturalRegion", value);
		}
	}

	public SpriteRenderer NaturalIcon
	{
		get
		{
			return this._NaturalIcon;
		}
		set
		{
			this._NaturalIcon = value;
			base.NotifyProperty("NaturalIcon", value);
		}
	}

	public string NaturalName
	{
		get
		{
			return this._NaturalName;
		}
		set
		{
			this._NaturalName = value;
			base.NotifyProperty("NaturalName", value);
		}
	}

	public string NaturalDesc
	{
		get
		{
			return this._NaturalDesc;
		}
		set
		{
			this._NaturalDesc = value;
			base.NotifyProperty("NaturalDesc", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		UpgradeUIViewModel.m_instance = this;
	}

	public void OnBtnComfirmClick()
	{
		UpgradeUIView.Instance.Show(false);
	}

	public void SetUpgradeUnits(List<UpgradeUnitData> units)
	{
		this.UpgradeUnits.Clear();
		for (int i = 0; i < units.get_Count(); i++)
		{
			UpgradeUnitData upgradeUnitData = units.get_Item(i);
			OOUpgradeUnit oOUpgradeUnit = new OOUpgradeUnit();
			oOUpgradeUnit.AttBegin = upgradeUnitData.BeginStr + " :";
			oOUpgradeUnit.AttBegin1 = upgradeUnitData.BeginStr1;
			oOUpgradeUnit.AttEnd = upgradeUnitData.EndStr;
			this.UpgradeUnits.Add(oOUpgradeUnit);
		}
	}

	public static void Open()
	{
		UIManagerControl.Instance.OpenUI("UpgradeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}
}
