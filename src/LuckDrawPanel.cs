using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LuckDrawPanel : UIBase
{
	private int DrawType = -1;

	public GameObject ButtonArea1;

	public GameObject ButtonArea10;

	public ButtonCustom ButtonDraw1;

	public ButtonCustom ButtonDraw10;

	public ButtonCustom ButtonClose;

	public Text Num1;

	public Text Num10;

	public Image ItemIcon1;

	public Image ItemIcon10;

	public Image Image1;

	public TextCustom Title;

	private int FxId;

	private string freeDraw;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.ButtonArea1 = base.FindTransform("ButtonArea1").get_gameObject();
		this.ButtonArea10 = base.FindTransform("ButtonArea10").get_gameObject();
	}

	private void Start()
	{
		this.ButtonDraw1.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDraw1);
		this.ButtonDraw10.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDraw10);
		this.ButtonClose.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickClose);
	}

	protected override void OnEnable()
	{
		this.ButtonDraw1.set_enabled(true);
		this.ButtonDraw10.set_enabled(true);
		PetManager.Instance.IsLuckDrawing = true;
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110024), "BACK", delegate
		{
			base.Show(false);
			UIManagerControl.Instance.UnLoadUIPrefab("LuckDrawPanel");
			this.ShowLuckDrawUI();
		}, false);
		this.UpdateUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		PetManager.Instance.IsLuckDrawing = false;
		CurrenciesUIViewModel.Show(false);
		FXSpineManager.Instance.DeleteSpine(this.FxId, true);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		base.RemoveListeners();
	}

	public void OnClickDraw1(GameObject go)
	{
		if (LuckDrawManager.Instance.CheckDrawType(this.DrawType))
		{
			this.ButtonDraw1.set_enabled(false);
			this.ButtonDraw10.set_enabled(false);
		}
		LuckDrawManager.Instance.LuckDrawReq(this, this.DrawType);
	}

	public void OnClickDraw10(GameObject go)
	{
		if (LuckDrawManager.Instance.CheckDrawType(this.DrawType + 1))
		{
			this.ButtonDraw1.set_enabled(false);
			this.ButtonDraw10.set_enabled(false);
		}
		LuckDrawManager.Instance.LuckDrawReq(this, this.DrawType + 1);
	}

	public void OnClickClose(GameObject go)
	{
		this.Show(false);
		UIManagerControl.Instance.UnLoadUIPrefab("LuckDrawPanel");
		this.ShowLuckDrawUI();
	}

	private void ShowLuckDrawUI()
	{
		UIManagerControl.Instance.OpenUI("LuckDrawUI", null, true, UIType.FullScreen);
	}

	private void OnSecondsPast()
	{
		if (this.DrawType == 1 || (LuckDrawManager.Instance.DrawIdDateTime.ContainsKey(this.DrawType) && LuckDrawManager.Instance.DrawIdDateTime.get_Item(this.DrawType) > TimeManager.Instance.PreciseServerTime))
		{
			this.Num1.get_gameObject().SetActive(true);
			this.ItemIcon1.set_enabled(true);
		}
		else
		{
			this.Num1.set_text(this.freeDraw);
			this.ItemIcon1.set_enabled(false);
		}
	}

	public void UpdateUI()
	{
		this.DrawType = LuckDrawManager.Instance.lastSelectMode;
		ChouJiangXiaoHao chouJiangXiaoHao = DataReader<ChouJiangXiaoHao>.Get(this.DrawType);
		ChouJiangXiaoHao chouJiangXiaoHao2 = DataReader<ChouJiangXiaoHao>.Get(this.DrawType + 1);
		this.Title.TextId = chouJiangXiaoHao.titleId;
		string text = string.Empty;
		if (this.DrawType == 1)
		{
			text = "1";
			this.ButtonArea1.SetActive(true);
			this.ButtonArea10.SetActive(false);
		}
		else if (this.DrawType == 3)
		{
			text = "2";
			this.ButtonArea1.SetActive(true);
			this.ButtonArea10.SetActive(true);
		}
		else if (this.DrawType == 5)
		{
			text = "3";
			this.ButtonArea1.SetActive(true);
			this.ButtonArea10.SetActive(true);
		}
		ResourceManager.SetSprite(this.ButtonDraw1.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite("cj_button_" + text));
		ResourceManager.SetSprite(this.ButtonDraw10.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite("cj_button_" + text));
		ResourceManager.SetSprite(this.Image1, ResourceManager.GetIconSprite("cj_icon_" + text));
		if (BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) >= (long)chouJiangXiaoHao.lotteryAmount || this.DrawType == 1)
		{
			this.Num1.set_text(BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) + "/" + chouJiangXiaoHao.lotteryAmount);
			ResourceManager.SetSprite(this.ItemIcon1, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.lotteryId).littleIcon));
		}
		else
		{
			this.Num1.set_text("x" + chouJiangXiaoHao.amount.ToString());
			ResourceManager.SetSprite(this.ItemIcon1, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.itemId).littleIcon));
		}
		if (this.DrawType == 1 || (chouJiangXiaoHao2.lotteryId > 0 && BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao2.lotteryId) >= (long)chouJiangXiaoHao2.lotteryAmount))
		{
			this.Num10.set_text(BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao2.lotteryId) + "/" + chouJiangXiaoHao2.lotteryAmount);
			ResourceManager.SetSprite(this.ItemIcon10, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao2.lotteryId).littleIcon));
		}
		else
		{
			this.Num10.set_text("x" + chouJiangXiaoHao2.amount.ToString());
			ResourceManager.SetSprite(this.ItemIcon10, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao2.itemId).littleIcon));
		}
		this.freeDraw = GameDataUtils.GetChineseContent(621061, false);
		this.OnSecondsPast();
	}
}
