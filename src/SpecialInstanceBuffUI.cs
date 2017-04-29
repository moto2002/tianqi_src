using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceBuffUI : UIBase
{
	private GridLayoutGroup m_buffList;

	protected SpecialFightMode currentMode;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_buffList = base.FindTransform("ListBuff").GetComponent<GridLayoutGroup>();
		base.FindTransform("TextTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502324, false));
		base.FindTransform("TextTips").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502325, false));
		this.InitBuffList();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
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
		EventDispatcher.AddListener(EventNames.UpdateSpecialInstanceDetailUI, new Callback(this.OnBuyBuffCallBack));
		EventDispatcher.AddListener(EventNames.SpecialInstanceBuffFail, new Callback(this.OnBuyBuffFailCallBack));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.UpdateSpecialInstanceDetailUI, new Callback(this.OnBuyBuffCallBack));
		EventDispatcher.RemoveListener(EventNames.SpecialInstanceBuffFail, new Callback(this.OnBuyBuffFailCallBack));
		base.RemoveListeners();
	}

	private void InitBuffList()
	{
		this.ClearScroll();
		List<FZengYibuffPeiZhi> dataList = DataReader<FZengYibuffPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			this.AddScrollCell(dataList.get_Item(i));
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_buffList.get_transform().get_childCount(); i++)
		{
			Object.Destroy(this.m_buffList.get_transform().GetChild(i).get_gameObject());
		}
	}

	private void AddScrollCell(FZengYibuffPeiZhi config)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("SpecialInstanceBuffItem");
		instantiate2Prefab.get_transform().SetParent(this.m_buffList.get_transform(), false);
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.GetComponent<SpecialInstanceBuffItem>().UpdateItem(config);
		instantiate2Prefab.set_name("BuffItem" + config.id);
	}

	public void SelectBuff(List<int> buffIds)
	{
		for (int i = 0; i < this.m_buffList.get_transform().get_childCount(); i++)
		{
			SpecialInstanceBuffItem component = this.m_buffList.get_transform().GetChild(i).GetComponent<SpecialInstanceBuffItem>();
			bool select = buffIds.Contains(component.buffId);
			component.setSelect(select);
		}
	}

	public void ClearBuffs()
	{
		for (int i = 0; i < this.m_buffList.get_transform().get_childCount(); i++)
		{
			this.m_buffList.get_transform().GetChild(i).GetComponent<SpecialInstanceBuffItem>().setSelect(false);
		}
	}

	public void SetModeInit(SpecialFightMode mode)
	{
		this.currentMode = mode;
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(mode) as SpecialFightInfo;
		if (specialFightInfo != null && specialFightInfo.m_BuffIds.get_Count() > 0)
		{
			this.SelectBuff(specialFightInfo.m_BuffIds);
		}
		else
		{
			this.ClearBuffs();
		}
	}

	private void OnBuyBuffCallBack()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(this.currentMode) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return;
		}
		if (specialFightInfo.m_BuffIds.get_Count() > 0)
		{
			this.SelectBuff(specialFightInfo.m_BuffIds);
		}
	}

	private void OnBuyBuffFailCallBack()
	{
		this.Show(false);
	}
}
