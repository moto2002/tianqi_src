using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RiseUI : UIBase
{
	public Vector3 targetPos;

	private ListPool pool1;

	private ScrollRectCustom scroll;

	private Image slider;

	private Text sliderShow;

	private Text attrUI;

	private int stage = 1;

	private bool isDisableBut;

	private bool isOut;

	private bool isMovePath;

	private Vector3 endPos = Vector3.get_zero();

	private RenWuLianTiXiTong data;

	private bool isFinal;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.hideMainCamera = true;
	}

	protected override void InitUI()
	{
		this.endPos = this.targetPos;
		this.isOut = true;
		base.FindTransform("Desc").GetComponent<Text>().set_text(string.Format("<color=#e4ffff>{0}</color>", GameDataUtils.GetChineseContent(70999, false)));
		this.pool1 = base.FindTransform("Grid").GetComponent<ListPool>();
		this.scroll = base.FindTransform("Content").GetComponent<ScrollRectCustom>();
		this.scroll.Arrow2First = base.FindTransform("left");
		this.scroll.Arrow2Last = base.FindTransform("right");
		this.slider = base.FindTransform("Fill").GetComponent<Image>();
		this.sliderShow = base.FindTransform("sum").GetComponent<Text>();
		this.attrUI = base.FindTransform("Des").GetComponent<Text>();
		ButtonCustom expr_E7 = base.FindTransform("Right").GetComponent<ButtonCustom>();
		expr_E7.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_E7.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickDes));
		ButtonCustom expr_118 = base.FindTransform("addButton").GetComponent<ButtonCustom>();
		expr_118.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_118.onClickCustom, new ButtonCustom.VoidDelegateObj(this.AddBtn));
		ButtonCustom expr_149 = base.FindTransform("left").GetComponent<ButtonCustom>();
		expr_149.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_149.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickLeft));
		ButtonCustom expr_17A = base.FindTransform("right").GetComponent<ButtonCustom>();
		expr_17A.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_17A.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickRight));
		this.scroll.OnMoveStopped = new Action<int>(this.SwitchTips);
	}

	private void OnClickRight(GameObject go)
	{
		this.scroll.Move2Next();
	}

	private void OnClickLeft(GameObject go)
	{
		this.scroll.Move2Previous();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110018), string.Empty, new Action(this.OnClickExit), false);
		CharacterManager.Instance.AllLightPoint.Clear();
		CharacterManager.Instance.NewBrightPoint = 0;
		this.UpdateData1(0);
		this.scroll.Move2Last(true);
		this.scroll.OnHasBuilt = delegate
		{
			this.scroll.Move2Last(true);
			this.scroll.OnHasBuilt = null;
		};
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.UpdateRiseUIPoint, new Callback<int>(this.OnUpdateRiseUIPoint));
		EventDispatcher.AddListener(EventNames.UpdateRiseUI, new Callback(this.OnGetRiseUpdate));
		EventDispatcher.AddListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnUpdateRiseItem));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.UpdateRiseUIPoint, new Callback<int>(this.OnUpdateRiseUIPoint));
		EventDispatcher.RemoveListener(EventNames.UpdateRiseUI, new Callback(this.OnGetRiseUpdate));
		EventDispatcher.AddListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnUpdateRiseItem));
	}

	private void OnClickDes(GameObject go)
	{
		if (this.isMovePath)
		{
			return;
		}
		base.StartCoroutine(this.MoveOnPath());
		this.UpdateAtrr(this.scroll.CurrentPageIndex + 1);
	}

	private void OnClickExit()
	{
		if (this.isDisableBut)
		{
			return;
		}
		UIManagerControl.Instance.UnLoadUIPrefab("RiseUI");
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnUpdateRiseUIPoint(int id)
	{
		this.SliderUpdate(CharacterManager.Instance.CurExp);
		this.pool1.Items.get_Item(this.stage - 1).GetComponent<RiseItem>().UpdateLine(id);
		if (!this.isFinal)
		{
			this.isDisableBut = false;
		}
	}

	private void OnGetRiseUpdate()
	{
		this.UpdateData1(this.stage);
		if (!CharacterManager.Instance.isMaxStep)
		{
			base.FindTransform("guide").get_gameObject().SetActive(true);
		}
		CharacterManager.Instance.AllLightPoint.Clear();
		this.isDisableBut = false;
	}

	private void OnUpdateRiseItem(int arg1, bool arg2)
	{
		this.isFinal = arg2;
		this.isDisableBut = true;
	}

	[DebuggerHidden]
	private IEnumerator MoveOnPath()
	{
		RiseUI.<MoveOnPath>c__Iterator41 <MoveOnPath>c__Iterator = new RiseUI.<MoveOnPath>c__Iterator41();
		<MoveOnPath>c__Iterator.<>f__this = this;
		return <MoveOnPath>c__Iterator;
	}

	public void UpdateData1(int stage = 0)
	{
		this.data = CharacterManager.Instance.GetRiseDataTpl();
		if (this.data != null)
		{
			this.SliderUpdate(CharacterManager.Instance.CurExp);
			this.SwitchTips((stage != 0) ? (stage - 1) : (CharacterManager.Instance.Stage - 1));
			this.pool1.Create(CharacterManager.Instance.Stage, delegate(int index)
			{
				if (index < CharacterManager.Instance.Stage && index < this.pool1.Items.get_Count())
				{
					bool isAct = CharacterManager.Instance.isMaxStep || index != CharacterManager.Instance.Stage - 1;
					this.pool1.Items.get_Item(index).GetComponent<RiseItem>().UpdateDataActive(index + 1, isAct);
				}
			});
		}
	}

	private void SwitchTips(int num)
	{
		this.stage = num + 1;
		string text = string.Empty;
		bool flag;
		if (this.stage < CharacterManager.Instance.Stage || CharacterManager.Instance.isMaxStep)
		{
			flag = false;
			text = "当阶已完成升阶";
		}
		else
		{
			int minLv = DataReader<RenWuLianTiXiTong>.Get(this.stage).minLv;
			if (minLv <= EntityWorld.Instance.EntSelf.Lv)
			{
				flag = true;
			}
			else
			{
				flag = false;
				text = string.Format(GameDataUtils.GetChineseContent(70998, false), minLv);
			}
		}
		base.FindTransform("tips").GetComponent<Text>().set_text(text);
		base.FindTransform("Down").get_gameObject().SetActive(flag);
		base.FindTransform("tips").get_gameObject().SetActive(!flag);
		base.FindTransform("guide").get_gameObject().SetActive(false);
		base.FindTransform("ItemTitle").GetComponent<Text>().set_text(string.Format("<color=#b1ffee>{0}</color>", GameDataUtils.GetChineseContent(CharacterManager.Instance.GetRiseDataTpl(this.stage).desc, false)));
	}

	private void UpdateAtrr(int stage)
	{
		this.attrUI.set_text(CharacterManager.Instance.GetAttrText());
	}

	private void SliderUpdate(int addNum)
	{
		int experience = this.data.experience;
		this.sliderShow.set_text(string.Format("{0}/{1}", addNum, experience));
		if (addNum < 0)
		{
			addNum = 0;
			Debuger.Error("num is overflow", new object[0]);
		}
		this.slider.set_fillAmount((float)addNum / (float)experience);
	}

	[DebuggerHidden]
	private IEnumerator SliderAnimation(float sNum)
	{
		RiseUI.<SliderAnimation>c__Iterator42 <SliderAnimation>c__Iterator = new RiseUI.<SliderAnimation>c__Iterator42();
		<SliderAnimation>c__Iterator.sNum = sNum;
		<SliderAnimation>c__Iterator.<$>sNum = sNum;
		<SliderAnimation>c__Iterator.<>f__this = this;
		return <SliderAnimation>c__Iterator;
	}

	private void AddBtn(GameObject go)
	{
		if (this.isDisableBut)
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("RiseGoodsUI", null, false, UIType.NonPush);
	}
}
