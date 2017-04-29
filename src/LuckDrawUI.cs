using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class LuckDrawUI : UIBase
{
	public static LuckDrawUI Instance;

	private GameObject Mode1Tips;

	private GameObject Mode2Tips;

	private GameObject Mode3Tips;

	private Text Mode2Time;

	private Text Mode3Time;

	public Image DrawDesc2;

	public Image DrawDesc3;

	private Text FreeDraw2;

	private Text FreeDraw3;

	private string freeDrawDesc;

	private List<int> FxIdList = new List<int>();

	private ButtonCustom LuckyButton1;

	private ButtonCustom LuckyButton10;

	public Text LuckyNum1;

	public Text LuckyNum10;

	public Image LuckyItemIcon1;

	public Image LuckyItemIcon10;

	private ActorModel roleModel;

	private GameObject ImageTouchPlace;

	private RawImage ImageActor;

	private Text petAttor;

	private GameObject ButtonArea1Consume;

	private TimeSpan ShowTime;

	private int[] FxIds = new int[3];

	private uint timerID;

	protected override void Preprocessing()
	{
		this.isMask = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		LuckDrawUI.Instance = this;
		this.ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		this.ImageActor = base.FindTransform("RawImageActor").GetComponent<RawImage>();
		this.petAttor = base.FindTransform("TextPetAttor").GetComponent<Text>();
		this.ButtonArea1Consume = base.FindTransform("ButtonArea1Consume").get_gameObject();
	}

	private void Start()
	{
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_1C = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_1C.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_1C.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.ImageActor.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.Mode1Tips = base.FindTransform("Tips1").get_gameObject();
		this.Mode2Tips = base.FindTransform("Tips2").get_gameObject();
		this.Mode3Tips = base.FindTransform("Tips3").get_gameObject();
		this.Mode2Time = base.FindTransform("Time2").GetComponent<Text>();
		this.Mode3Time = base.FindTransform("Time3").GetComponent<Text>();
		this.FreeDraw2 = base.FindTransform("FreeDrawText2").GetComponent<Text>();
		this.FreeDraw3 = base.FindTransform("FreeDrawText3").GetComponent<Text>();
		this.freeDrawDesc = GameDataUtils.GetChineseContent(621073, false);
		this.LuckyButton1 = base.FindTransform("LuckyButton1").GetComponent<ButtonCustom>();
		this.LuckyButton10 = base.FindTransform("LuckyButton10").GetComponent<ButtonCustom>();
		this.LuckyButton1.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOne);
		this.LuckyButton10.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTen);
		this.LuckyNum1 = base.FindTransform("ButtonArea1Num").GetComponent<Text>();
		this.LuckyNum10 = base.FindTransform("ButtonArea10Num").GetComponent<Text>();
		this.LuckyItemIcon1 = base.FindTransform("ButtonArea1Icon").GetComponent<Image>();
		this.LuckyItemIcon10 = base.FindTransform("ButtonArea10Icon").GetComponent<Image>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		PetManager.Instance.IsLuckDrawing = true;
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110024), "BACK", delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.Mode1Tips.SetActive(false);
		this.Mode2Tips.SetActive(false);
		this.Mode3Tips.SetActive(false);
		this.UpdateUI();
		this.UpdateBtnInfo();
		this.OnSecondsPast();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		base.SetAsFirstSibling();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		this.ShowRandomModel();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		PetManager.Instance.IsLuckDrawing = false;
		using (List<int>.Enumerator enumerator = this.FxIdList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				FXSpineManager.Instance.DeleteSpine(current, true);
			}
		}
		int[] fxIds = this.FxIds;
		for (int i = 0; i < fxIds.Length; i++)
		{
			int uid = fxIds[i];
			FXSpineManager.Instance.DeleteSpine(uid, true);
		}
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		LuckDrawManager.Instance.CheckTipsInTownUI();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			LuckDrawUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnLuckDrawInfoChangeNty, new Callback(this.UpdateUI));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OnLuckDrawInfoChangeNty, new Callback(this.UpdateUI));
		base.RemoveListeners();
	}

	private void OnSecondsPast()
	{
		this.Check1();
		this.Check2();
		this.Check3();
	}

	private void Check1()
	{
		bool activeSelf = this.Mode1Tips.get_activeSelf();
		if (LuckDrawManager.Instance.CheckDrawType(1))
		{
			this.Mode1Tips.SetActive(true);
		}
		else
		{
			this.Mode1Tips.SetActive(false);
		}
		if (activeSelf || !this.Mode1Tips.get_activeSelf())
		{
			if (activeSelf && !this.Mode1Tips.get_activeSelf())
			{
				FXSpineManager.Instance.DeleteSpine(this.FxIds[0], true);
			}
		}
	}

	private void Check2()
	{
		bool activeSelf = this.Mode2Tips.get_activeSelf();
		if (!LuckDrawManager.Instance.CheckTipOfEquip())
		{
			this.ShowTime = LuckDrawManager.Instance.DrawIdDateTime.get_Item(3) - TimeManager.Instance.PreciseServerTime;
			int num = this.ShowTime.get_Days() * 24 + this.ShowTime.get_Hours();
			string text = string.Concat(new string[]
			{
				(num.ToString().get_Length() >= 2) ? num.ToString() : ("0" + num),
				":",
				(this.ShowTime.get_Minutes().ToString().get_Length() >= 2) ? this.ShowTime.get_Minutes().ToString() : ("0" + this.ShowTime.get_Minutes()),
				":",
				(this.ShowTime.get_Seconds().ToString().get_Length() >= 2) ? this.ShowTime.get_Seconds().ToString() : ("0" + this.ShowTime.get_Seconds()),
				this.freeDrawDesc
			});
			this.Mode2Time.set_text(text);
			this.Mode2Tips.SetActive(false);
			this.Mode2Time.get_gameObject().SetActive(true);
			this.FreeDraw2.get_gameObject().SetActive(false);
		}
		else
		{
			this.Mode2Time.get_gameObject().SetActive(false);
			this.FreeDraw2.get_gameObject().SetActive(true);
			this.Mode2Tips.SetActive(true);
		}
		if (activeSelf || !this.Mode2Tips.get_activeSelf())
		{
			if (activeSelf && !this.Mode2Tips.get_activeSelf())
			{
				FXSpineManager.Instance.DeleteSpine(this.FxIds[1], true);
			}
		}
	}

	private void Check3()
	{
		bool activeSelf = this.Mode3Tips.get_activeSelf();
		if (!LuckDrawManager.Instance.CheckTipOfTimeOne())
		{
			this.ShowTime = LuckDrawManager.Instance.DrawIdDateTime.get_Item(5) - TimeManager.Instance.PreciseServerTime;
			int num = this.ShowTime.get_Days() * 24 + this.ShowTime.get_Hours();
			string text = string.Concat(new string[]
			{
				(num.ToString().get_Length() >= 2) ? num.ToString() : ("0" + num),
				":",
				(this.ShowTime.get_Minutes().ToString().get_Length() >= 2) ? this.ShowTime.get_Minutes().ToString() : ("0" + this.ShowTime.get_Minutes()),
				":",
				(this.ShowTime.get_Seconds().ToString().get_Length() >= 2) ? this.ShowTime.get_Seconds().ToString() : ("0" + this.ShowTime.get_Seconds()),
				this.freeDrawDesc
			});
			this.Mode3Time.set_text(text);
			this.Mode3Time.get_gameObject().SetActive(true);
			this.FreeDraw3.get_gameObject().SetActive(false);
			this.Mode3Tips.SetActive(false);
			this.ButtonArea1Consume.SetActive(true);
		}
		else
		{
			this.Mode3Time.get_gameObject().SetActive(false);
			this.FreeDraw3.get_gameObject().SetActive(true);
			this.Mode3Tips.SetActive(true);
			this.ButtonArea1Consume.SetActive(false);
		}
		if (activeSelf || !this.Mode3Tips.get_activeSelf())
		{
			if (activeSelf && !this.Mode3Tips.get_activeSelf())
			{
				FXSpineManager.Instance.DeleteSpine(this.FxIds[2], true);
			}
		}
	}

	private void UpdateUI()
	{
		if (3 < LuckDrawManager.Instance.LuckDrawInfoMap.get_Count() && LuckDrawManager.Instance.LuckDrawInfoMap.get_Item(3).diamondTimes > 0)
		{
			ResourceManager.SetSprite(this.DrawDesc2, ResourceManager.GetIconSprite("cj_font_3"));
		}
		else
		{
			ResourceManager.SetSprite(this.DrawDesc2, ResourceManager.GetIconSprite("cj_font_1"));
		}
		if (5 < LuckDrawManager.Instance.LuckDrawInfoMap.get_Count() && LuckDrawManager.Instance.LuckDrawInfoMap.get_Item(5).diamondTimes > 0)
		{
			ResourceManager.SetSprite(this.DrawDesc3, ResourceManager.GetIconSprite("cj_font_4"));
		}
		else
		{
			ResourceManager.SetSprite(this.DrawDesc3, ResourceManager.GetIconSprite("cj_font_2"));
		}
	}

	private void UpdateBtnInfo()
	{
		LuckDrawManager.Instance.lastSelectMode = 5;
		int lastSelectMode = LuckDrawManager.Instance.lastSelectMode;
		ChouJiangXiaoHao chouJiangXiaoHao = DataReader<ChouJiangXiaoHao>.Get(LuckDrawManager.Instance.lastSelectMode);
		ChouJiangXiaoHao chouJiangXiaoHao2 = DataReader<ChouJiangXiaoHao>.Get(LuckDrawManager.Instance.lastSelectMode + 1);
		if (BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) >= (long)chouJiangXiaoHao.lotteryAmount || lastSelectMode == 1)
		{
			this.LuckyNum1.set_text(BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) + "/" + chouJiangXiaoHao.lotteryAmount);
			ResourceManager.SetSprite(this.LuckyItemIcon1, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.lotteryId).littleIcon));
		}
		else
		{
			this.LuckyNum1.set_text("x" + chouJiangXiaoHao.amount.ToString());
			ResourceManager.SetSprite(this.LuckyItemIcon1, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.itemId).littleIcon));
		}
		if (lastSelectMode == 1 || (chouJiangXiaoHao2.lotteryId > 0 && BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao2.lotteryId) >= (long)chouJiangXiaoHao2.lotteryAmount))
		{
			this.LuckyNum10.set_text(BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao2.lotteryId) + "/" + chouJiangXiaoHao2.lotteryAmount);
			ResourceManager.SetSprite(this.LuckyItemIcon10, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao2.lotteryId).littleIcon));
		}
		else
		{
			this.LuckyNum10.set_text("x" + chouJiangXiaoHao2.amount.ToString());
			ResourceManager.SetSprite(this.LuckyItemIcon10, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao2.itemId).littleIcon));
		}
	}

	public void OnClickMode(GameObject go)
	{
		int lastSelectMode = -1;
		if (go.get_name().EndsWith("1"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(42, 0, true))
			{
				return;
			}
			lastSelectMode = 1;
		}
		else if (go.get_name().EndsWith("2"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(43, 0, true))
			{
				return;
			}
			lastSelectMode = 3;
		}
		else if (go.get_name().EndsWith("3"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(44, 0, true))
			{
				return;
			}
			lastSelectMode = 5;
		}
		LuckDrawManager.Instance.lastSelectMode = lastSelectMode;
		UIManagerControl.Instance.OpenUI("LuckDrawPanel", null, true, UIType.FullScreen);
	}

	public void OnClickOne(GameObject go)
	{
		LuckDrawManager.Instance.lastSelectMode = 5;
		if (LuckDrawManager.Instance.CheckDrawType(LuckDrawManager.Instance.lastSelectMode))
		{
			this.SetButtonLuckDrawLock(false);
		}
		LuckDrawManager.Instance.LuckDrawReq(this, LuckDrawManager.Instance.lastSelectMode);
	}

	public void OnClickTen(GameObject go)
	{
		LuckDrawManager.Instance.lastSelectMode = 5;
		if (LuckDrawManager.Instance.CheckDrawType(LuckDrawManager.Instance.lastSelectMode))
		{
			this.SetButtonLuckDrawLock(false);
		}
		LuckDrawManager.Instance.LuckDrawReq(this, LuckDrawManager.Instance.lastSelectMode + 1);
	}

	protected void ShowModel()
	{
		List<ChouJiangZhanShi> dataList = DataReader<ChouJiangZhanShi>.DataList;
		if (dataList == null)
		{
			return;
		}
		int num = Random.Range(0, dataList.get_Count());
		ChouJiangZhanShi chouJiangZhanShi = dataList.get_Item(num);
		int modelId = chouJiangZhanShi.modelId;
		int explain = chouJiangZhanShi.explain;
		this.petAttor.set_text(GameDataUtils.GetChineseContent(explain, false));
		ModelDisplayManager.Instance.ShowModel(modelId, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uid)
		{
			this.roleModel = ModelDisplayManager.Instance.GetUIModel(uid);
			if (this.roleModel != null)
			{
				LayerSystem.SetGameObjectLayer(this.roleModel.get_gameObject(), "CameraRange", 2);
			}
		});
	}

	private void ShowRandomModel()
	{
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		this.timerID = TimerHeap.AddTimer(0u, 10000, new Action(this.ShowModel));
	}

	public void ResetRoleModel()
	{
		if (this.roleModel != null && this.roleModel.get_gameObject() != null)
		{
			Object.Destroy(this.roleModel.get_gameObject());
		}
	}

	public void SetButtonLuckDrawLock(bool isOn)
	{
		this.LuckyButton1.set_enabled(isOn);
		this.LuckyButton10.set_enabled(isOn);
	}
}
