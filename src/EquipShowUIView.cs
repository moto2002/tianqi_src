using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class EquipShowUIView : UIBase
{
	public static EquipShowUIView Instance;

	private GameObject btnHaveSelect;

	private GameObject ImageTouchPlace;

	private Text TextName;

	private Text TextLevel;

	private Text TextFightNum;

	private RawImage ImageActor;

	private ActorModel roleModel;

	protected ExteriorArithmeticUnit exteriorUnit;

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(new Action<Action>(this.UpdateActor), new Action(this.UpdateWeapon), new Action(this.UpdateClothes), new Action(this.UpdateWing));
			}
			return this.exteriorUnit;
		}
	}

	private void Awake()
	{
		EquipShowUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextName = base.FindTransform("TextName").GetComponent<Text>();
		this.ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		this.ImageActor = base.FindTransform("RawImageActor").GetComponent<RawImage>();
		this.TextFightNum = base.FindTransform("TextFightNum").GetComponent<Text>();
		this.TextLevel = base.FindTransform("TextLevel").GetComponent<Text>();
	}

	private void Start()
	{
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_1C = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_1C.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_1C.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.ImageActor.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
	}

	protected override void OnEnable()
	{
		base.SetAsFirstSibling();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		this.RefreshRoleModel();
		this.RefreshBtm();
	}

	protected override void OnDisable()
	{
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.ResetRoleModel();
			EquipShowUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.AddListener(EventNames.EquipViewShouldClose, new Callback(this.EquipViewShouldClose));
		EventDispatcher.AddListener<bool>(EventNames.ComposeEquipmentSucess, new Callback<bool>(this.ComposeEquipmentSucess));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.FightingChanged, new Callback(this.ChangeFighting));
		EventDispatcher.AddListener(ExteriorArithmeticUnitEvent.UnitChanged, new Callback(this.RefreshRoleModel));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.RemoveListener(EventNames.EquipViewShouldClose, new Callback(this.EquipViewShouldClose));
		EventDispatcher.RemoveListener<bool>(EventNames.ComposeEquipmentSucess, new Callback<bool>(this.ComposeEquipmentSucess));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.FightingChanged, new Callback(this.ChangeFighting));
		EventDispatcher.RemoveListener(ExteriorArithmeticUnitEvent.UnitChanged, new Callback(this.RefreshRoleModel));
	}

	private void OnEquipWeapon(int equipCfgID)
	{
		if (this.roleModel == null)
		{
			return;
		}
		int gogokNum = 0;
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(EquipLibType.ELT.Weapon);
		if (wearingEquipSimpleInfoByPos != null)
		{
			long equipId = wearingEquipSimpleInfoByPos.equipId;
			gogokNum = EquipGlobal.GetExcellentAttrsCountByColor(equipId, 1f);
		}
		this.roleModel.EquipOn(equipCfgID, gogokNum);
	}

	private void RefreshRoleModel()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		this.ExteriorUnit.WrapSetDataAndForceUpdate(delegate
		{
			this.ExteriorUnit.Clone(EntityWorld.Instance.EntSelf.ExteriorUnit, false);
			this.ExteriorUnit.ClientModelID = 0;
		});
	}

	private void UpdateActor(Action logicCallback)
	{
		ModelDisplayManager.Instance.ShowModel(this.ExteriorUnit.FinalModelID, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uid)
		{
			this.roleModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (this.roleModel != null)
			{
				this.roleModel.get_transform().set_localEulerAngles(Vector3.get_zero());
				if (logicCallback != null)
				{
					logicCallback.Invoke();
				}
				LayerSystem.SetGameObjectLayer(this.roleModel.get_gameObject(), "CameraRange", 2);
				this.roleModel.PreciseSetAction("idle_city");
			}
		});
	}

	private void UpdateWeapon()
	{
		this.roleModel.EquipOn(this.ExteriorUnit.FinalWeaponID, this.ExteriorUnit.FinalWeaponGogok);
	}

	private void UpdateClothes()
	{
		this.roleModel.EquipOn(this.ExteriorUnit.FinalClothesID, 0);
	}

	private void UpdateWing()
	{
		this.roleModel.EquipWingOn(this.ExteriorUnit.FinalWingID);
	}

	public void ResetRoleModel()
	{
		this.ExteriorUnit.Reset();
		if (this.roleModel != null && this.roleModel.get_gameObject() != null)
		{
			Object.Destroy(this.roleModel.get_gameObject());
		}
	}

	private void RefreshBtm()
	{
		this.TextName.set_text(EntityWorld.Instance.EntSelf.Name);
		this.TextLevel.set_text("Lv." + EntityWorld.Instance.EntSelf.Lv);
		this.TextFightNum.set_text(EntityWorld.Instance.EntSelf.Fighting.ToString());
	}

	public void SetOnSelectBtn(GameObject gameobj)
	{
		if (this.btnHaveSelect != null)
		{
			this.btnHaveSelect.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(false);
		}
		EventDispatcher.Broadcast(EventNames.ActorUIShouldShouldEquipment);
		this.btnHaveSelect = gameobj;
		this.btnHaveSelect.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(true);
	}

	private void RefreshIcons()
	{
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Weapon))
		{
			this.SetIcon("BtnWeapon", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.WeaponLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnWeapon");
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Waist))
		{
			this.SetIcon("BtnWaistPendant", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.WaistLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnWaistPendant");
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Necklace))
		{
			this.SetIcon("BtnNecklace", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.NecklaceLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnNecklace");
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Shirt))
		{
			this.SetIcon("BtnCloth", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.ClothesLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnCloth");
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Pant))
		{
			this.SetIcon("BtnTrousers", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.PantsLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnTrousers");
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Shoe))
		{
			this.SetIcon("BtnShoe", EquipmentManager.Instance.dicEquips.get_Item(EquipmentManager.Instance.ShoesLib.wearingId).cfgId);
		}
		else
		{
			this.CloseIcon("BtnShoe");
		}
	}

	private void CloseIcon(string iconName)
	{
		Transform transform = base.get_transform().FindChild(iconName);
		Transform transform2 = transform.FindChild("ImageNothing");
		Transform transform3 = transform.FindChild("ImageColor");
		Transform transform4 = transform.FindChild("ImageIcon");
		transform2.get_gameObject().SetActive(true);
		transform3.get_gameObject().SetActive(false);
		transform4.get_gameObject().SetActive(false);
	}

	private void SetIcon(string iconName, int iconID)
	{
		Transform transform = base.get_transform().FindChild(iconName);
		Transform transform2 = transform.FindChild("ImageIcon");
		Transform transform3 = transform.FindChild("ImageColor");
		Transform transform4 = transform.FindChild("ImageNothing");
		transform4.get_gameObject().SetActive(false);
		transform3.get_gameObject().SetActive(true);
		transform2.get_gameObject().SetActive(true);
		Items items = DataReader<Items>.Get(iconID);
		if (items != null)
		{
			ResourceManager.SetSprite(transform2.GetComponent<Image>(), GameDataUtils.GetIcon(items.icon));
			ResourceManager.SetSprite(transform3.GetComponent<Image>(), ResourceManager.GetIconSprite("Frame_" + items.color));
		}
	}

	private void EquipEquipmentSucess()
	{
	}

	private void EquipViewShouldClose()
	{
		this.Show(false);
	}

	private void ComposeEquipmentSucess(bool result)
	{
		if (!result)
		{
			return;
		}
	}

	private void ChangeFighting()
	{
		this.RefreshBtm();
	}
}
