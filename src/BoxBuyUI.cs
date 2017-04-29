using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxBuyUI : UIBase
{
	public static BoxBuyUI Instance;

	public Text m_TitleName;

	public Text m_BoxInfo;

	public Transform m_ScrollviewList;

	public Action CallBack;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		BoxBuyUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			BoxBuyUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_TitleName = base.FindTransform("titleName").GetComponent<Text>();
		this.m_BoxInfo = base.FindTransform("BoxInfo").GetComponent<Text>();
		this.m_ScrollviewList = base.FindTransform("ScrollviewList");
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnCloseClick);
		base.FindTransform("BtnOK").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnOKClick);
	}

	public void SetShowItem(int itemId, Action callback)
	{
		Items config = DataReader<Items>.Get(itemId);
		if (config == null)
		{
			BoxBuyUI.Instance.Show(false);
			return;
		}
		this.m_TitleName.set_text(string.Format(GameDataUtils.GetChineseContent(508035, false), GameDataUtils.GetChineseContent(config.name, false)));
		this.m_BoxInfo.set_text(GameDataUtils.GetChineseContent(config.describeId1, false));
		for (int i = 0; i < this.m_ScrollviewList.get_childCount(); i++)
		{
			Object.Destroy(this.m_ScrollviewList.GetChild(i).get_gameObject());
		}
		if (config.effectId > 0)
		{
			List<DiaoLuo> list = DataReader<DiaoLuo>.DataList.FindAll((DiaoLuo a) => a.ruleId == config.effectId);
			if (list != null && list.get_Count() > 0)
			{
				for (int j = 0; j < list.get_Count(); j++)
				{
					DiaoLuo diaoLuo = list.get_Item(j);
					if (diaoLuo.dropType == 1)
					{
						ItemShow.ShowItem(this.m_ScrollviewList, diaoLuo.goodsId, diaoLuo.minNum, false, UINodesManager.T2RootOfSpecial, 2001);
					}
				}
			}
		}
		this.CallBack = callback;
	}

	public void OnBtnCloseClick(GameObject go)
	{
		BoxBuyUI.Instance.Show(false);
	}

	public void OnBtnOKClick(GameObject go)
	{
		BoxBuyUI.Instance.Show(false);
		if (this.CallBack != null)
		{
			this.CallBack.Invoke();
		}
	}
}
