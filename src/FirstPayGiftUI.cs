using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class FirstPayGiftUI : UIBase
{
	public static FirstPayGiftUI Instance;

	public GameObject ButtonGet1;

	public GameObject ButtonPay1;

	public GameObject ButtonGet2;

	public GameObject ButtonPay2;

	public GameObject ButtonGet3;

	public GameObject ButtonPay3;

	public ButtonCustom CloseButton;

	public Transform Items;

	public Transform Button;

	private Transform Pet;

	private ListPool m_ItemPool;

	private ListPool m_ItemPool2;

	private ListPool m_ItemPool3;

	private List<int> m_spineIds = new List<int>();

	private RawImage BackGround;

	private Text TextTitle2;

	private Text TextTitle3;

	private ActorModel roleModel;

	private ActorModel petModel;

	private GameObject ImageTouchPlace;

	private RawImage ImageActor;

	private void Awake()
	{
		FirstPayGiftUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ButtonPay1 = base.FindTransform("ButtonPay1").get_gameObject();
		this.ButtonPay2 = base.FindTransform("ButtonPay2").get_gameObject();
		this.ButtonPay3 = base.FindTransform("ButtonPay3").get_gameObject();
		this.ButtonGet1 = base.FindTransform("ButtonGet1").get_gameObject();
		this.ButtonGet2 = base.FindTransform("ButtonGet2").get_gameObject();
		this.ButtonGet3 = base.FindTransform("ButtonGet3").get_gameObject();
		this.TextTitle2 = base.FindTransform("TextTitle2").GetComponent<Text>();
		this.TextTitle3 = base.FindTransform("TextTitle3").GetComponent<Text>();
		this.m_ItemPool = base.FindTransform("Items").GetComponent<ListPool>();
		this.m_ItemPool.SetItem("ItemShow");
		this.m_ItemPool2 = base.FindTransform("Items2").GetComponent<ListPool>();
		this.m_ItemPool2.SetItem("ItemShow");
		this.m_ItemPool3 = base.FindTransform("Items3").GetComponent<ListPool>();
		this.m_ItemPool3.SetItem("ItemShow");
		this.Pet = base.FindTransform("Pet");
		this.BackGround = base.FindTransform("Background").GetComponent<RawImage>();
		string chineseContent = GameDataUtils.GetChineseContent(651071, false);
		base.FindTransform("TextInfoBottom").GetComponent<Text>().set_text(chineseContent);
		this.ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		this.ImageActor = base.FindTransform("RawImageActor").GetComponent<RawImage>();
	}

	private void Start()
	{
		this.ButtonPay1.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPay);
		this.ButtonPay2.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPay);
		this.ButtonPay3.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPay);
		this.ButtonGet1.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet1);
		this.ButtonGet2.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet2);
		this.ButtonGet3.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet3);
		this.CloseButton.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClose);
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_DB = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_DB.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_DB.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.ImageActor.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
	}

	protected override void OnEnable()
	{
		base.get_transform().SetAsLastSibling();
		FirstPayManager.Instance.IsFirstPayUIHasOpened = true;
		this.SetItems();
		this.UpdateBtnsState();
		this.UpdateRewardDiamond();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		this.ShowModel();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		for (int i = 0; i < this.m_spineIds.get_Count(); i++)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_spineIds.get_Item(i), true);
		}
		this.m_spineIds.Clear();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.ResetRoleModel();
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickGet(GameObject go)
	{
		if (!BackpackManager.Instance.ShowBackpackFull())
		{
			this.ButtonGetEnabledClick(false);
			this.OnClose(null);
		}
	}

	private void OnClickPay(GameObject go)
	{
		this.OnClickMaskAction();
		LinkNavigationManager.OpenVIPUI2Recharge();
	}

	private void OnClose(GameObject go)
	{
		this.OnClickMaskAction();
	}

	private void ButtonGetEnabledClick(bool canClick)
	{
		if (canClick)
		{
		}
	}

	private void OnClickGet1(GameObject go)
	{
		this.SendData(0);
	}

	private void OnClickGet2(GameObject go)
	{
		this.SendData(1);
	}

	private void OnClickGet3(GameObject go)
	{
		this.SendData(2);
		this.OnClose(null);
	}

	private void SendData(int index)
	{
		List<ShouChong> dataList = DataReader<ShouChong>.DataList;
		int id = dataList.get_Item(index).id;
		FirstPayManager.Instance.SendGetFirstPrize(id);
	}

	private void SetItems()
	{
		List<ShouChong> list = DataReader<ShouChong>.DataList;
		int loop = 1;
		int i;
		for (i = 0; i < list.get_Count(); i++)
		{
			int id = list.get_Item(i).Reward.get_Item(0);
			XDict<int, long> itemDic = FirstPayManager.Instance.GetRewardItems(id);
			ListPool pool = this.m_ItemPool;
			if (i == 1)
			{
				pool = this.m_ItemPool2;
			}
			if (i == 2)
			{
				pool = this.m_ItemPool3;
			}
			pool.Create(itemDic.Count, delegate(int index)
			{
				if (index < itemDic.Count && index < pool.Items.get_Count())
				{
					ItemShow.SetItem(pool.Items.get_Item(index), itemDic.ElementKeyAt(index), itemDic.ElementValueAt(index), false, UINodesManager.T2RootOfSpecial, 3000);
					Vector3 localScale = new Vector3(0.7f, 0.7f, 0.7f);
					if (index == 0)
					{
						localScale = new Vector3(0.85f, 0.85f, 0.85f);
					}
					pool.Items.get_Item(index).GetComponent<RectTransform>().set_localScale(localScale);
				}
				if (i == list.get_Count() && itemDic.Count == pool.Items.get_Count())
				{
					if (loop == 3)
					{
						this.PlaySpineOfItems();
					}
					loop++;
				}
			});
		}
	}

	private void PlaySpineOfItems()
	{
		int templateId = 157;
		for (int i = 0; i < 3; i++)
		{
			ListPool listPool = this.m_ItemPool;
			if (i == 1)
			{
				listPool = this.m_ItemPool2;
			}
			if (i == 2)
			{
				listPool = this.m_ItemPool3;
			}
			for (int j = 0; j < listPool.Items.get_Count(); j++)
			{
				if (j == 0)
				{
					int num = FXSpineManager.Instance.PlaySpine(templateId, listPool.Items.get_Item(j).get_transform(), "FirstPayGiftUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					this.m_spineIds.Add(num);
				}
			}
		}
	}

	public void UpdateBtnsState()
	{
		if (FirstPayManager.Instance.FirstPayRechargeInfo == null)
		{
			return;
		}
		for (int i = 0; i < FirstPayManager.Instance.FirstPayRechargeInfo.get_Count(); i++)
		{
			int id = FirstPayManager.Instance.FirstPayRechargeInfo.get_Item(i).id;
			int status = FirstPayManager.Instance.FirstPayRechargeInfo.get_Item(i).status;
			ShouChong shouChong = DataReader<ShouChong>.Get(id);
			if (shouChong != null)
			{
				this.UpdateOneButtonStatus(i, status);
			}
		}
	}

	private void UpdateOneButtonStatus(int index, int status)
	{
		switch (status)
		{
		case 0:
			base.FindTransform("ButtonPay" + (index + 1)).get_gameObject().SetActive(true);
			base.FindTransform("ButtonGet" + (index + 1)).get_gameObject().SetActive(false);
			break;
		case 1:
			base.FindTransform("ButtonPay" + (index + 1)).get_gameObject().SetActive(false);
			base.FindTransform("ButtonGet" + (index + 1)).get_gameObject().SetActive(true);
			base.FindTransform("ButtonGet" + (index + 1)).FindChild("Text").GetComponent<Text>().set_text("领 取");
			ImageColorMgr.SetImageColor(base.FindTransform("ButtonGet" + (index + 1)).GetComponent<Image>(), false);
			break;
		case 2:
			base.FindTransform("ButtonPay" + (index + 1)).get_gameObject().SetActive(false);
			base.FindTransform("ButtonGet" + (index + 1)).get_gameObject().SetActive(true);
			base.FindTransform("ButtonGet" + (index + 1)).FindChild("Text").GetComponent<Text>().set_text("已领取");
			ImageColorMgr.SetImageColor(base.FindTransform("ButtonGet" + (index + 1)).GetComponent<Image>(), true);
			break;
		}
	}

	public void UpdateRewardDiamond()
	{
		int allRechargeDiamond = FirstPayManager.Instance.AllRechargeDiamond;
		List<ShouChong> dataList = DataReader<ShouChong>.DataList;
		int diamond = dataList.get_Item(1).Diamond;
		int diamond2 = dataList.get_Item(2).Diamond;
		string text = string.Format("({0}/{1})", allRechargeDiamond, diamond);
		string text2 = string.Format("({0}/{1})", allRechargeDiamond, diamond2);
		this.TextTitle2.set_text(text);
		this.TextTitle3.set_text(text2);
		if (allRechargeDiamond < diamond)
		{
			this.TextTitle2.set_color(new Color(0.8901961f, 0.3019608f, 0.3019608f));
		}
		else
		{
			this.TextTitle2.set_color(new Color(0.3137255f, 0.882352948f, 0.0784313753f));
		}
		if (allRechargeDiamond < diamond2)
		{
			this.TextTitle3.set_color(new Color(0.8901961f, 0.3019608f, 0.3019608f));
		}
		else
		{
			this.TextTitle3.set_color(new Color(0.3137255f, 0.882352948f, 0.0784313753f));
		}
	}

	private void PlaySpineOfUI()
	{
		int num = FXSpineManager.Instance.PlaySpine(606, this.Button, "FirstPayGiftUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.m_spineIds.Add(num);
		num = FXSpineManager.Instance.PlaySpine(607, this.Pet, "FirstPayGiftUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.m_spineIds.Add(num);
		num = FXSpineManager.Instance.PlaySpine(609, this.Pet, "FirstPayGiftUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.m_spineIds.Add(num);
	}

	private void ChangeBackGround()
	{
		string name = "sc_bg001";
		switch (EntityWorld.Instance.EntSelf.TypeID)
		{
		case 4:
			name = "sc_bg001";
			break;
		case 7:
			name = "sc_bg002";
			break;
		case 8:
			name = "sc_bg003";
			break;
		}
		ResourceManager.SetTexture(this.BackGround, name);
	}

	protected void ShowModel()
	{
		List<ShouChong> dataList = DataReader<ShouChong>.DataList;
		if (dataList.get_Item(0) == null)
		{
			return;
		}
		int modelId = 0;
		int wingId = 0;
		int modelId2 = 0;
		int equipId = 0;
		string[] array = dataList.get_Item(0).model.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			int num = int.Parse(array2[0]);
			string[] array3 = array2[1].Split(new char[]
			{
				','
			});
			if (num == EntityWorld.Instance.EntSelf.TypeID)
			{
				modelId = int.Parse(array3[0]);
				wingId = int.Parse(array3[1]);
				modelId2 = int.Parse(array3[2]);
				equipId = int.Parse(array3[3]);
				break;
			}
		}
		ModelDisplayManager.Instance.ShowPetModel(modelId2, true, ModelDisplayManager.OFFSET_TO_PETUI, delegate(int uid)
		{
			this.petModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (this.petModel != null)
			{
				this.petModel.get_transform().set_localEulerAngles(Vector3.get_zero());
				this.petModel.get_gameObject().get_transform().set_localPosition(new Vector3(0.6f, -0.4f, 0f));
				this.petModel.get_gameObject().get_transform().set_localScale(new Vector3(0.8f, 0.8f, 0.8f));
			}
			ModelDisplayManager.Instance.ShowModel(modelId, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uids)
			{
				this.roleModel = ModelDisplayManager.Instance.GetUIModel(uids);
				if (this.roleModel != null)
				{
					this.roleModel.get_transform().set_localEulerAngles(Vector3.get_zero());
					this.roleModel.EquipWingOn(wingId);
					this.roleModel.EquipOn(equipId, 3);
				}
			});
		});
	}

	public void ResetRoleModel()
	{
		if (this.roleModel != null && this.roleModel.get_gameObject() != null)
		{
			Object.Destroy(this.roleModel.get_gameObject());
		}
		if (this.petModel != null && this.petModel.get_gameObject() != null)
		{
			Object.Destroy(this.petModel.get_gameObject());
		}
	}
}
