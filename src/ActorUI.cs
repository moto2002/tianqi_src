using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorUI : UIBase
{
	public enum RightState
	{
		None,
		Property,
		Title,
		Wing
	}

	public static ActorUI Instance;

	private Transform Content;

	private Transform RightTransRoot;

	private Transform BtnChangeCareer;

	private ActorUI.RightState m_RightState;

	private ActorUI.RightState m_LastState;

	private GameObject TitleObject;

	private Dictionary<ActorUI.RightState, ButtonCustom> TabBtnDic;

	private Dictionary<EquipLibType.ELT, Transform> equipPartTransDic;

	private ButtonCustom WingBtn;

	private Dictionary<EquipLibType.ELT, int> equipPartFxDic;

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		ActorUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.Content = base.FindTransform("Content");
		this.TitleObject = base.FindTransform("Title").get_gameObject();
		this.RightTransRoot = base.FindTransform("RightTransRoot");
		this.BtnChangeCareer = base.FindTransform("BtnChangeCareer");
		this.BtnChangeCareer.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnChangeCareer);
		this.TabBtnDic = new Dictionary<ActorUI.RightState, ButtonCustom>();
		ButtonCustom component = base.FindTransform("InfoBtn").GetComponent<ButtonCustom>();
		component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		ButtonCustom component2 = base.FindTransform("TitleBtn").GetComponent<ButtonCustom>();
		component2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.WingBtn = base.FindTransform("WingBtn").GetComponent<ButtonCustom>();
		this.WingBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.TabBtnDic.Add(ActorUI.RightState.Property, component);
		this.TabBtnDic.Add(ActorUI.RightState.Title, component2);
		this.TabBtnDic.Add(ActorUI.RightState.Wing, this.WingBtn);
		this.equipPartTransDic = new Dictionary<EquipLibType.ELT, Transform>();
		this.equipPartFxDic = new Dictionary<EquipLibType.ELT, int>();
		for (int i = 1; i <= 10; i++)
		{
			Transform transform = base.FindTransform("Btn" + i);
			this.equipPartTransDic.Add((EquipLibType.ELT)i, transform);
			this.equipPartFxDic.Add((EquipLibType.ELT)i, 0);
			transform.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEquipPartBtn);
		}
		ButtonCustom component3 = base.FindTransform("WardrobeBtn").GetComponent<ButtonCustom>();
		component3.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWardrobeBtn);
		this.SetTitle(0L, 0);
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110015), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			ActorUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	public void CheckBadge()
	{
		for (int i = 1; i < 11; i++)
		{
			if (this.equipPartTransDic != null && this.equipPartTransDic.ContainsKey((EquipLibType.ELT)i))
			{
				this.equipPartTransDic.get_Item((EquipLibType.ELT)i).FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip((EquipLibType.ELT)i));
			}
		}
		using (Dictionary<ActorUI.RightState, ButtonCustom>.Enumerator enumerator = this.TabBtnDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<ActorUI.RightState, ButtonCustom> current = enumerator.get_Current();
				bool active = false;
				switch (current.get_Key())
				{
				case ActorUI.RightState.Property:
					active = EquipmentManager.Instance.CheckCanChangeEquipAllPos();
					break;
				case ActorUI.RightState.Title:
					active = TitleManager.Instance.HasNewTitle();
					break;
				case ActorUI.RightState.Wing:
					active = WingManager.CheckAllBadge();
					break;
				}
				current.get_Value().get_transform().FindChild("btnImg").get_gameObject().SetActive(active);
			}
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<long, int>("BillboardManager.Title", new Callback<long, int>(this.SetTitle));
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipEquipmentSucess));
		EventDispatcher.AddListener<string, bool>(EventNames.OnTipsStateChange, new Callback<string, bool>(this.OnRefreshTitle));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<long, int>("BillboardManager.Title", new Callback<long, int>(this.SetTitle));
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipEquipmentSucess));
		EventDispatcher.RemoveListener<string, bool>(EventNames.OnTipsStateChange, new Callback<string, bool>(this.OnRefreshTitle));
	}

	private void OnRefreshTitle(string name, bool isShow)
	{
		this.CheckBadge();
	}

	private void OnEquipEquipmentSucess()
	{
		this.CheckBadge();
		this.RefreshEquipPart();
	}

	private void SetTitle(long playerId, int titleId)
	{
		if (TitleManager.Instance.OwnCurrId == 0)
		{
			this.TitleObject.get_transform().Find("Image").get_gameObject().SetActive(false);
			this.TitleObject.get_transform().Find("Text").get_gameObject().SetActive(false);
		}
		else
		{
			ChengHao chengHao = DataReader<ChengHao>.Get(TitleManager.Instance.OwnCurrId);
			if (chengHao == null)
			{
				this.TitleObject.get_transform().Find("Image").get_gameObject().SetActive(false);
				this.TitleObject.get_transform().Find("Text").get_gameObject().SetActive(false);
			}
			else if (chengHao.displayWay == 1)
			{
				this.TitleObject.get_transform().Find("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(chengHao.icon, false));
				this.TitleObject.get_transform().Find("Image").get_gameObject().SetActive(false);
				this.TitleObject.get_transform().Find("Text").get_gameObject().SetActive(true);
			}
			else if (chengHao.displayWay == 2)
			{
				ResourceManager.SetSprite(this.TitleObject.get_transform().Find("Image").GetComponent<Image>(), GameDataUtils.GetIcon(chengHao.icon));
				this.TitleObject.get_transform().Find("Text").get_gameObject().SetActive(false);
				this.TitleObject.get_transform().Find("Image").get_gameObject().SetActive(true);
			}
		}
	}

	public void LogicClickTabToWing()
	{
		this.OnClickTab(this.WingBtn.get_gameObject());
	}

	private void OnClickTab(GameObject go)
	{
		string name = go.get_name();
		if (name != null)
		{
			if (ActorUI.<>f__switch$map15 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
				dictionary.Add("InfoBtn", 0);
				dictionary.Add("TitleBtn", 1);
				dictionary.Add("WingBtn", 2);
				ActorUI.<>f__switch$map15 = dictionary;
			}
			int num;
			if (ActorUI.<>f__switch$map15.TryGetValue(name, ref num))
			{
				switch (num)
				{
				case 0:
					this.m_RightState = ActorUI.RightState.Property;
					break;
				case 1:
					this.m_RightState = ActorUI.RightState.Title;
					break;
				case 2:
					if (!SystemOpenManager.IsSystemClickOpen(35, 0, true))
					{
						return;
					}
					this.m_RightState = ActorUI.RightState.Wing;
					break;
				}
			}
		}
		if (this.m_RightState == this.m_LastState)
		{
			return;
		}
		this.SetBtnLightAndDim(go, "fenleianniu_1", true);
		if (this.TabBtnDic.ContainsKey(this.m_LastState))
		{
			this.SetBtnLightAndDim(this.TabBtnDic.get_Item(this.m_LastState).get_gameObject(), "fenleianniu_2", false);
		}
		this.RefreshRightUIState(this.m_RightState);
		this.m_LastState = this.m_RightState;
	}

	private void OnClickBtnChangeCareer(GameObject go)
	{
		LinkNavigationManager.OpenChangeCareerUI();
	}

	private void OnClickEquipPartBtn(GameObject go)
	{
		EquipLibType.ELT pos = EquipLibType.ELT.Weapon;
		using (Dictionary<EquipLibType.ELT, Transform>.Enumerator enumerator = this.equipPartTransDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<EquipLibType.ELT, Transform> current = enumerator.get_Current();
				if (go == current.get_Value().get_gameObject())
				{
					pos = current.get_Key();
				}
			}
		}
		UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		EquipDetailedPopUI.Instance.SetSelectEquipTip(pos, true);
	}

	private void OnClickWardrobeBtn(GameObject go)
	{
		FashionManager.Instance.OpenFashionUI(FashionDataSelete.Clothes);
	}

	private void RefreshUI()
	{
		EventDispatcher.Broadcast<UIBase>(EventNames.RefreshTipsButtonStateInUIBase, this);
		this.SetChangeCareerLock();
		this.CheckBadge();
		UIManagerControl.Instance.OpenUI("EquipShowUI", this.Content, false, UIType.NonPush);
		this.m_RightState = ActorUI.RightState.Property;
		this.OnClickTab(base.FindTransform("InfoBtn").get_gameObject());
		this.RefreshEquipPart();
	}

	private void RefreshRightUIState(ActorUI.RightState state)
	{
		switch (this.m_LastState)
		{
		case ActorUI.RightState.Property:
			UIManagerControl.Instance.HideUI("ActorPropertyUI");
			break;
		case ActorUI.RightState.Title:
			UIManagerControl.Instance.HideUI("TitleUI");
			break;
		case ActorUI.RightState.Wing:
			UIManagerControl.Instance.HideUI("WingUI");
			break;
		}
		switch (state)
		{
		case ActorUI.RightState.Property:
		{
			ActorPropertyUI actorPropertyUI = UIManagerControl.Instance.OpenUI("ActorPropertyUI", this.RightTransRoot, false, UIType.NonPush) as ActorPropertyUI;
			actorPropertyUI.RefreshUI(EntityWorld.Instance.EntSelf);
			break;
		}
		case ActorUI.RightState.Title:
			UIManagerControl.Instance.OpenUI("TitleUI", this.RightTransRoot, false, UIType.NonPush);
			break;
		case ActorUI.RightState.Wing:
		{
			UIBase uIBase = UIManagerControl.Instance.OpenUI("WingUI", this.RightTransRoot, false, UIType.NonPush);
			uIBase.get_transform().set_localPosition(new Vector3(344f, -73f));
			break;
		}
		}
	}

	private void SetChangeCareerLock()
	{
		this.BtnChangeCareer.FindChild("Lock").get_gameObject().SetActive(SystemOpenManager.IsSystemHideEntrance(34));
	}

	private void RefreshEquipPart()
	{
		if (this.equipPartTransDic == null)
		{
			return;
		}
		using (Dictionary<EquipLibType.ELT, Transform>.Enumerator enumerator = this.equipPartTransDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<EquipLibType.ELT, Transform> current = enumerator.get_Current();
				this.SetEquipPartBtn(current.get_Key());
			}
		}
	}

	private void SetEquipPartBtn(EquipLibType.ELT type)
	{
		if (!this.equipPartTransDic.ContainsKey(type))
		{
			return;
		}
		if (EquipmentManager.Instance.equipmentData == null || EquipmentManager.Instance.equipmentData.equipLibs == null)
		{
			return;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == type);
		if (equipLib == null)
		{
			return;
		}
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(type);
		if (wearingEquipSimpleInfoByPos == null)
		{
			return;
		}
		Dictionary<string, string> iconNamesByEquipPos = EquipGlobal.GetIconNamesByEquipPos(type, true);
		if (iconNamesByEquipPos == null)
		{
			return;
		}
		int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(wearingEquipSimpleInfoByPos.equipId, 1f);
		Transform transform = this.equipPartTransDic.get_Item(type).FindChild("ImageIcon");
		this.equipPartTransDic.get_Item(type).FindChild("Num").GetComponent<Text>().set_text((equipLib.lv <= 0) ? string.Empty : ("+" + equipLib.lv));
		this.equipPartTransDic.get_Item(type).FindChild("EquipStepText").GetComponent<Text>().set_text(iconNamesByEquipPos.get_Item("EquipStep"));
		ResourceManager.SetSprite(this.equipPartTransDic.get_Item(type).FindChild("ImageIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconName")));
		if (this.equipPartFxDic.ContainsKey(type))
		{
			FXSpineManager.Instance.DeleteSpine(this.equipPartFxDic.get_Item(type), true);
		}
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(wearingEquipSimpleInfoByPos.equipId);
		int num;
		if (equipForgeCfgData != null && wearingEquipSimpleInfoByPos.suitId > 0)
		{
			ResourceManager.SetSprite(this.equipPartTransDic.get_Item(type).FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			num = FXSpineManager.Instance.PlaySpine(equipForgeCfgData.fxId, transform, "EquipDetailedUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			ResourceManager.SetSprite(this.equipPartTransDic.get_Item(type).FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconFrameName")));
			num = EquipGlobal.GetEquipIconFX(wearingEquipSimpleInfoByPos.cfgId, excellentAttrsCountByColor, transform, "EquipPartBtns", 2000, false);
		}
		if (this.equipPartFxDic.ContainsKey(type))
		{
			this.equipPartFxDic.set_Item(type, num);
		}
		this.equipPartTransDic.get_Item(type).FindChild("ExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 1);
		this.equipPartTransDic.get_Item(type).FindChild("ExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 2);
		this.equipPartTransDic.get_Item(type).FindChild("ExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 3);
		this.equipPartTransDic.get_Item(type).FindChild("ImageBinding").get_gameObject().SetActive(false);
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}
}
