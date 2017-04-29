using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiseGoodsUI : UIBase
{
	private ListPool pool;

	private Image slider;

	private Text gold;

	private Text sliderShow;

	private float price;

	private int riseNum;

	private List<RefineBodyItemInfo> goods = new List<RefineBodyItemInfo>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.pool = base.FindTransform("Grid").GetComponent<ListPool>();
		this.gold = base.FindTransform("explain").GetComponent<Text>();
		this.slider = base.FindTransform("Fill").GetComponent<Image>();
		this.sliderShow = base.FindTransform("sum").GetComponent<Text>();
		ButtonCustom expr_68 = base.FindTransform("UpEnterButton").GetComponent<ButtonCustom>();
		expr_68.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_68.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickRiseBtn));
		ButtonCustom expr_99 = base.FindTransform("CloseButton").GetComponent<ButtonCustom>();
		expr_99.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_99.onClickCustom, new ButtonCustom.VoidDelegateObj(this.CloseBtn));
		this.price = float.Parse(DataReader<GlobalParams>.Get("refine_body_exp_to_gold_coin").value);
		this.UpdateData2();
	}

	public void UpdateData2()
	{
		List<Goods> rise = BackpackManager.Instance.RiseGoods;
		this.pool.Create(rise.get_Count(), delegate(int index)
		{
			if (index < rise.get_Count() && index < this.pool.Items.get_Count())
			{
				this.pool.Items.get_Item(index).GetComponent<RiseGood>().UpdateUI(rise.get_Item(index));
			}
		});
		this.SliderUpdate(CharacterManager.Instance.CurExp);
	}

	private void OnClickRiseBtn(GameObject go)
	{
		if (this.riseNum < CharacterManager.Instance.GetStageRiseValue())
		{
			UIManagerControl.Instance.ShowToastText("炼体值不足", 1f, 1f);
			return;
		}
		if ((float)EntityWorld.Instance.EntSelf.Gold < (float)this.riseNum * float.Parse(DataReader<GlobalParams>.Get("refine_body_exp_to_gold_coin").value))
		{
			UIManagerControl.Instance.ShowToastText("炼体金币不足", 1f, 1f);
			return;
		}
		if (this.goods.get_Count() > 0)
		{
			CharacterManager.Instance.SendPlanLightPointReq(this.goods);
			this.goods.Clear();
			this.CloseBtn(null);
		}
	}

	private void OnGetRiseResult(int id, bool isFinal)
	{
	}

	private void OnClickGood(long id, int num, bool isAdd)
	{
		int num2 = BackpackManager.Instance.OnGetGood(id).LocalItem.point;
		if (!isAdd)
		{
			num2 *= -1;
		}
		this.SliderUpdate(num2);
		RefineBodyItemInfo refineBodyItemInfo = this.goods.Find((RefineBodyItemInfo e) => e.id == id);
		if (refineBodyItemInfo != null)
		{
			refineBodyItemInfo.count = num;
		}
		else
		{
			refineBodyItemInfo = new RefineBodyItemInfo
			{
				id = id,
				count = num
			};
			this.goods.Add(refineBodyItemInfo);
		}
	}

	private void SliderUpdate(int addNum)
	{
		this.riseNum += addNum;
		Debuger.Error(string.Concat(new object[]
		{
			"riseNum:",
			this.riseNum,
			"addNum:",
			addNum
		}), new object[0]);
		int experience = CharacterManager.Instance.GetRiseDataTpl().experience;
		this.sliderShow.set_text(string.Format("{0}/{1}", this.riseNum, experience));
		if (this.riseNum < 0)
		{
			this.riseNum = 0;
			Debuger.Error("num is overflow", new object[0]);
		}
		this.slider.set_fillAmount((float)this.riseNum / (float)experience);
		if (this.slider.get_fillAmount() >= 1f)
		{
			EventDispatcher.Broadcast<bool>(EventNames.SliderOverflow, true);
		}
		else
		{
			EventDispatcher.Broadcast<bool>(EventNames.SliderOverflow, false);
		}
		this.gold.set_text(string.Format("金币消耗:{0}", this.price * (float)(this.riseNum - CharacterManager.Instance.CurExp)));
	}

	private void CloseBtn(GameObject go)
	{
		this.riseNum = 0;
		UIManagerControl.Instance.UnLoadUIPrefab("RiseGoodsUI");
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<long, int, bool>(EventNames.UpdateSlider, new Callback<long, int, bool>(this.OnClickGood));
		EventDispatcher.AddListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnGetRiseResult));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<long, int, bool>(EventNames.UpdateSlider, new Callback<long, int, bool>(this.OnClickGood));
		EventDispatcher.RemoveListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnGetRiseResult));
	}
}
