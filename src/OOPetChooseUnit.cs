using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class OOPetChooseUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_PetId = "PetId";

		public const string Attr_PetIcon = "PetIcon";

		public const string Attr_PetIconHSV = "PetIconHSV";

		public const string Attr_PetIconBg = "PetIconBg";

		public const string Attr_PetName = "PetName";

		public const string Attr_Level = "Level";

		public const string Attr_MatVisibility = "MatVisibility";

		public const string Attr_MatNum = "MatNum";

		public const string Attr_BadgeTip = "BadgeTip";

		public const string Attr_InFormation = "InFormation";

		public const string Event_OnBtnChoosed = "OnBtnChoosed";
	}

	public enum Status
	{
		HaveActivation_StarEnough,
		HaveActivation_StarNoEnough,
		HaveActivation_StarTop,
		NoActivation
	}

	private OOPetChooseUnit.Status _PetStatus = OOPetChooseUnit.Status.NoActivation;

	public long PetUID;

	public long BattleFighting;

	private int _PetUpgradeLevel;

	private int _PetId;

	private SpriteRenderer _PetIcon;

	private int _PetIconHSV;

	private SpriteRenderer _PetIconBg;

	private string _PetName;

	private string _Level;

	private bool _MatVisibility;

	private string _MatNum;

	private bool _BadgeTip;

	private bool _InFormation;

	public OOPetChooseUnit.Status PetStatus
	{
		get
		{
			return this._PetStatus;
		}
		set
		{
			this._PetStatus = value;
			switch (this._PetStatus)
			{
			case OOPetChooseUnit.Status.HaveActivation_StarEnough:
				this.MatVisibility = false;
				break;
			case OOPetChooseUnit.Status.HaveActivation_StarNoEnough:
				this.MatVisibility = false;
				break;
			case OOPetChooseUnit.Status.HaveActivation_StarTop:
				this.MatVisibility = false;
				break;
			case OOPetChooseUnit.Status.NoActivation:
				this.MatVisibility = true;
				break;
			}
		}
	}

	public int PetUpgradeLevel
	{
		get
		{
			return this._PetUpgradeLevel;
		}
		set
		{
			this._PetUpgradeLevel = value;
			this.PetIconBg = PetManager.GetPetFrameSquare(value);
		}
	}

	public int PetId
	{
		get
		{
			return this._PetId;
		}
		set
		{
			this._PetId = value;
			base.NotifyProperty("PetId", value);
			Pet pet = DataReader<Pet>.Get(this._PetId);
			if (pet != null)
			{
				this.PetIcon = PetManager.Instance.GetSelfPic(pet);
			}
		}
	}

	public SpriteRenderer PetIcon
	{
		get
		{
			return this._PetIcon;
		}
		set
		{
			this._PetIcon = value;
			base.NotifyProperty("PetIcon", value);
		}
	}

	public int PetIconHSV
	{
		get
		{
			return this._PetIconHSV;
		}
		set
		{
			this._PetIconHSV = value;
			base.NotifyProperty("PetIconHSV", value);
		}
	}

	public SpriteRenderer PetIconBg
	{
		get
		{
			return this._PetIconBg;
		}
		set
		{
			this._PetIconBg = value;
			base.NotifyProperty("PetIconBg", value);
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

	public string Level
	{
		get
		{
			return this._Level;
		}
		set
		{
			this._Level = value;
			base.NotifyProperty("Level", value);
		}
	}

	public bool MatVisibility
	{
		get
		{
			return this._MatVisibility;
		}
		set
		{
			this._MatVisibility = value;
			base.NotifyProperty("MatVisibility", value);
		}
	}

	public string MatNum
	{
		get
		{
			return this._MatNum;
		}
		set
		{
			this._MatNum = value;
			base.NotifyProperty("MatNum", value);
		}
	}

	public bool BadgeTip
	{
		get
		{
			return this._BadgeTip;
		}
		set
		{
			this._BadgeTip = value;
			base.NotifyProperty("BadgeTip", value);
		}
	}

	public bool InFormation
	{
		get
		{
			return this._InFormation;
		}
		set
		{
			this._InFormation = value;
			base.NotifyProperty("InFormation", value);
		}
	}

	private bool IsActivation()
	{
		return this.PetStatus == OOPetChooseUnit.Status.HaveActivation_StarEnough || this.PetStatus == OOPetChooseUnit.Status.HaveActivation_StarNoEnough || this.PetStatus == OOPetChooseUnit.Status.HaveActivation_StarTop;
	}

	public void OnBtnChoosed()
	{
		if (this.IsActivation())
		{
			PetChooseUIViewModel.Instance.SetSelectedByID(this.PetId, true);
			PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
		}
		else
		{
			PetChooseUIViewModel.Instance.SetSelectedByID(this.PetId, true);
			PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
		}
	}
}
