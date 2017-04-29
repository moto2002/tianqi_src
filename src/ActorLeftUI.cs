using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ActorLeftUI : UIBase
{
	private GameObject btnHaveSelect;

	public Text TextLV;

	public Text TextName;

	public ButtonCustom BtnWing;

	public ButtonCustom BtnWaistPendant;

	public ButtonCustom BtnTrousers;

	public ButtonCustom BtnCloth;

	public ButtonCustom BtnWeapon;

	public ButtonCustom BtnNecklace;

	public ButtonCustom BtnPopular;

	public ButtonCustom BtnShoe;

	public Text TextPower;

	public ButtonCustom BtnBackpack;

	public ButtonCustom BtnOneKey;

	public RawImage ImageActor;

	public GameObject ImageTouchPlace;

	private void Start()
	{
		this.RefreshUI();
		this.BtnBackpack.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBackpack);
		this.BtnWing.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWing);
		this.BtnWaistPendant.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWaistPendant);
		this.BtnTrousers.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnTrousers);
		this.BtnCloth.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnCloth);
		this.BtnWeapon.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWeapon);
		this.BtnNecklace.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnNecklace);
		this.BtnPopular.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnPopular);
		this.BtnShoe.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnShoe);
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_F1 = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_F1.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_F1.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
	}

	protected override void OnEnable()
	{
		EntityWorld.Instance.ActSelf.IsDisplayingByLayer = true;
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.Player);
		RTManager.Instance.AimTarget = EntityWorld.Instance.ActSelf.get_transform();
		RTManager.Instance.SetRotate(true, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EntityWorld.Instance.ActSelf.IsDisplayingByLayer = false;
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.Player);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.AddListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.OnGetRoleAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
		EventDispatcher.RemoveListener(EventNames.OnGetRoleAttrChangedNty, new Callback(this.OnGetRoleAttrChangedNty));
	}

	private void OnClickBtnBackpack(GameObject sender)
	{
		InstanceManagerUI.OpenInstanceUI(10101, false, UIType.FullScreen);
	}

	private void OnClickBtnWing(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Wing);
	}

	private void OnClickBtnWaistPendant(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.WaistPendant);
	}

	private void OnClickBtnTrousers(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Trousers);
	}

	private void OnClickBtnCloth(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Cloth);
	}

	private void OnClickBtnWeapon(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Weapon);
	}

	private void OnClickBtnNecklace(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Necklace);
	}

	private void OnClickBtnPopular(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Popular);
	}

	private void OnClickBtnShoe(GameObject sender)
	{
		this.SetOnSelectUI(sender);
		this.OnClickEquipments(EquipPostion.Shoe);
	}

	private void OnClickEquipments(EquipPostion position)
	{
	}

	private void SetOnSelectUI(GameObject gameobj)
	{
		if (this.btnHaveSelect != null)
		{
			this.btnHaveSelect.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(false);
		}
		this.btnHaveSelect = gameobj;
		this.btnHaveSelect.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(true);
	}

	private void RefreshIcons()
	{
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Weapon))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Weapon));
		}
		else
		{
			this.CloseIcon(this.BtnWeapon.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Waist))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Waist));
		}
		else
		{
			this.CloseIcon(this.BtnWaistPendant.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Necklace))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Necklace));
		}
		else
		{
			this.CloseIcon(this.BtnNecklace.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Shirt))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Shirt));
		}
		else
		{
			this.CloseIcon(this.BtnCloth.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Pant))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Pant));
		}
		else
		{
			this.CloseIcon(this.BtnTrousers.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Shoe))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Shoe));
		}
		else
		{
			this.CloseIcon(this.BtnShoe.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Weapon))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Weapon));
		}
		else
		{
			this.CloseIcon(this.BtnPopular.get_gameObject());
		}
		if (EquipGlobal.IsWearing(EquipLibType.ELT.Weapon))
		{
			this.SetIcon(this.BtnWeapon.get_gameObject(), EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Weapon));
		}
		else
		{
			this.CloseIcon(this.BtnWing.get_gameObject());
		}
	}

	private void CloseIcon(GameObject gameObj)
	{
		Transform transform = gameObj.get_transform().FindChild("Content");
		transform.get_gameObject().SetActive(false);
	}

	private void SetIcon(GameObject gameObj, int itemId)
	{
		Transform transform = gameObj.get_transform().FindChild("Content");
		Transform transform2 = gameObj.get_transform().FindChild("ImageFrame");
		transform.get_gameObject().SetActive(true);
		Transform child = transform.GetChild(0);
		child.set_name("image_" + itemId);
		GameDataUtils.SetItem(itemId, transform2.GetComponent<Image>(), child.GetComponent<Image>(), null, true);
	}

	private void ResetData()
	{
		this.TextLV.set_text(EntityWorld.Instance.EntSelf.Lv.ToString());
	}

	public void RefreshUI()
	{
		this.ResetData();
		this.RefreshIcons();
	}

	private void EquipEquipmentSucess()
	{
		this.RefreshIcons();
	}

	private void EquipViewShouldClose()
	{
		this.Show(false);
	}

	private void OnGetRoleAttrChangedNty()
	{
		this.ResetData();
	}
}
