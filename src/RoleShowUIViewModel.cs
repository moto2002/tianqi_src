using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class RoleShowUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_SubEquipment = "SubEquipment";

		public const string Attr_SubProperty = "SubProperty";

		public const string Attr_SubFormation = "SubFormation";

		public const string Attr_TextPower = "TextPower";

		public const string Attr_TextLv = "TextLv";

		public const string Attr_TextName = "TextName";
	}

	private static RoleShowUIViewModel m_instance;

	private Dictionary<EquipLibType.ELT, Transform> equipPartDic;

	private bool _SubEquipment;

	private bool _SubProperty;

	private bool _SubFormation;

	private string _TextPower;

	private string _TextLv;

	private string _TextName;

	public int level;

	public List<BuddyPetFormation> BuddyPetFormations;

	protected ExteriorArithmeticUnit exteriorUnit;

	private ActorModel roleModel;

	private int m_profession;

	private List<WearEquipInfo> m_equipInfos;

	private List<int> equipIDs = new List<int>();

	private List<string> m_fashionInfos;

	private WearWingInfo m_wingInfo;

	public static RoleShowUIViewModel Instance
	{
		get
		{
			return RoleShowUIViewModel.m_instance;
		}
	}

	public bool SubEquipment
	{
		get
		{
			return this._SubEquipment;
		}
		set
		{
			this._SubEquipment = value;
			base.NotifyProperty("SubEquipment", value);
		}
	}

	public bool SubProperty
	{
		get
		{
			return this._SubProperty;
		}
		set
		{
			this._SubProperty = value;
			base.NotifyProperty("SubProperty", value);
		}
	}

	public bool SubFormation
	{
		get
		{
			return this._SubFormation;
		}
		set
		{
			this._SubFormation = value;
			base.NotifyProperty("SubFormation", value);
			if (value)
			{
				RoleShowPetFormationUI roleShowPetFormationUI = UIManagerControl.Instance.OpenUI("RoleShowPetFormationUI", RoleShowUIView.Instance.PanelFormation, false, UIType.NonPush) as RoleShowPetFormationUI;
				roleShowPetFormationUI.RefreshUI(0);
			}
		}
	}

	public string TextPower
	{
		get
		{
			return this._TextPower;
		}
		set
		{
			this._TextPower = value;
			base.NotifyProperty("TextPower", this._TextPower);
		}
	}

	public string TextLv
	{
		get
		{
			return this._TextLv;
		}
		set
		{
			this._TextLv = "Lv." + value;
			base.NotifyProperty("TextLv", this._TextLv);
		}
	}

	public string TextName
	{
		get
		{
			return this._TextName;
		}
		set
		{
			this._TextName = value;
			base.NotifyProperty("TextName", this._TextName);
		}
	}

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

	private int GogokNum
	{
		get
		{
			int num = 0;
			if (this.m_equipInfos != null)
			{
				int num2 = this.m_equipInfos.FindIndex((WearEquipInfo a) => a.type == 1);
				if (num2 >= 0)
				{
					for (int i = 0; i < this.m_equipInfos.get_Item(num2).excellentAttrs.get_Count(); i++)
					{
						int attrId = this.m_equipInfos.get_Item(num2).excellentAttrs.get_Item(i).attrId;
						float color = this.m_equipInfos.get_Item(num2).excellentAttrs.get_Item(i).color;
						if (attrId > 0 && color >= 1f)
						{
							num++;
						}
					}
				}
			}
			return num;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		RoleShowUIViewModel.m_instance = this;
		this.equipPartDic = new Dictionary<EquipLibType.ELT, Transform>();
		for (int i = 1; i <= 10; i++)
		{
			Transform transform = base.get_transform().FindChild("PanelEquipment").FindChild("EquipItems").FindChild("BtnPart" + i);
			if (transform != null && !this.equipPartDic.ContainsKey((EquipLibType.ELT)i))
			{
				this.equipPartDic.Add((EquipLibType.ELT)i, transform);
			}
		}
	}

	private void OnEnable()
	{
		this.ResetRoleModel();
		this.SubProperty = true;
		this.SubFormation = false;
	}

	public List<PetInfo> GetPets(int index)
	{
		if (index < this.BuddyPetFormations.get_Count())
		{
			return this.BuddyPetFormations.get_Item(index).petInfo;
		}
		return null;
	}

	public void SetEquipItems(List<WearEquipInfo> equipInfos)
	{
		if (equipInfos == null)
		{
			return;
		}
		for (int i = 0; i < equipInfos.get_Count(); i++)
		{
			this.SetEquipItem(equipInfos.get_Item(i));
		}
	}

	public void ShowRoleModel(int profession, List<WearEquipInfo> equipInfos, List<string> fashionInfos, WearWingInfo wearWingInfo)
	{
		this.m_profession = profession;
		this.m_equipInfos = equipInfos;
		this.m_fashionInfos = fashionInfos;
		this.m_wingInfo = wearWingInfo;
		int wingID = (this.m_wingInfo == null) ? 0 : this.m_wingInfo.wingId;
		int wingLv = (this.m_wingInfo == null) ? 0 : this.m_wingInfo.lv;
		bool wingHide = this.m_wingInfo != null && this.m_wingInfo.wingHidden;
		if (DataReader<RoleCreate>.Get(profession) == null)
		{
			return;
		}
		if (this.m_equipInfos == null)
		{
			return;
		}
		this.equipIDs.Clear();
		for (int i = 0; i < equipInfos.get_Count(); i++)
		{
			this.equipIDs.Add(equipInfos.get_Item(i).id);
		}
		this.ExteriorUnit.WrapSetData(delegate
		{
			this.ExteriorUnit.SetType(this.m_profession);
			this.ExteriorUnit.EquipIDs = this.equipIDs;
			this.ExteriorUnit.FashionIDs = fashionInfos;
			this.ExteriorUnit.WingID = WingManager.GetWingModel(wingID, wingLv);
			this.ExteriorUnit.IsHideWing = wingHide;
		});
		ModelDisplayManager.Instance.ShowModel(this.ExteriorUnit.FinalModelID, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uid)
		{
			this.roleModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (this.roleModel != null)
			{
				this.roleModel.get_transform().set_localEulerAngles(Vector3.get_zero());
				this.roleModel.EquipOn(this.ExteriorUnit.FinalWeaponID, this.ExteriorUnit.FinalWeaponGogok);
				this.roleModel.EquipOn(this.ExteriorUnit.FinalClothesID, 0);
				this.roleModel.EquipWingOn(this.ExteriorUnit.FinalWingID);
				LayerSystem.SetGameObjectLayer(this.roleModel.get_gameObject(), "CameraRange", 2);
				this.roleModel.PreciseSetAction("idle_city");
			}
		});
	}

	public void ResetRoleModel()
	{
		this.ShowRoleModel(this.m_profession, this.m_equipInfos, this.m_fashionInfos, this.m_wingInfo);
	}

	private void SetEquipItem(WearEquipInfo wearEquipInfo)
	{
		if (wearEquipInfo == null)
		{
			return;
		}
		if (this.equipPartDic.ContainsKey((EquipLibType.ELT)wearEquipInfo.type))
		{
			Transform transform = this.equipPartDic.get_Item((EquipLibType.ELT)wearEquipInfo.type);
			if (transform.get_childCount() > 0)
			{
				Item2RoleShow component = transform.GetChild(0).GetComponent<Item2RoleShow>();
				if (component == null)
				{
					return;
				}
				component.ShowEquipItem(wearEquipInfo);
			}
		}
	}
}
