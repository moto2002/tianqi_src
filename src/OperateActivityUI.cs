using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperateActivityUI : UIBase
{
	private ListView2 m_listTab;

	private Transform m_TransformContent;

	public Text m_lblTimeText;

	protected override void Preprocessing()
	{
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
		this.m_listTab = base.FindTransform("ListTabs").GetComponent<ListView2>();
		this.m_listTab.RowFrefabName = "OperateActivityTagItem";
		this.m_TransformContent = base.FindTransform("Content");
		this.m_lblTimeText = base.FindTransform("TimeText").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		DateTime preciseServerTime = TimeManager.Instance.PreciseServerTime;
		this.m_lblTimeText.set_text(string.Concat(new object[]
		{
			preciseServerTime.get_Year(),
			"年",
			preciseServerTime.get_Month(),
			"月",
			preciseServerTime.get_Day(),
			"日"
		}));
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110034), "BACK", delegate
		{
			base.Show(false);
		}, false);
		OperateActivityManager.Instance.SortedInfoList(true);
		this.OnRefreshActivityInfo();
		this.OpenActivityUI(null);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			OperateActivityManager.Instance.CurrentOpenTypeId = -1;
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
		EventDispatcher.AddListener(EventNames.RefreshActivityInfo, new Callback(this.OnRefreshActivityInfo));
		EventDispatcher.AddListener<int>(EventNames.OperateActivityTipsUpdate, new Callback<int>(this.UpdateTagItemTips));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.RefreshActivityInfo, new Callback(this.OnRefreshActivityInfo));
		EventDispatcher.RemoveListener<int>(EventNames.OperateActivityTipsUpdate, new Callback<int>(this.UpdateTagItemTips));
		base.RemoveListeners();
	}

	private void OnRefreshActivityInfo()
	{
		List<ActivityInfo> openActivitys = OperateActivityManager.Instance.GetOpenActivitys();
		int count = openActivitys.get_Count();
		this.m_listTab.CreateRow(count, 0);
		for (int i = 0; i < openActivitys.get_Count(); i++)
		{
			OperateActivityTagItem component = this.m_listTab.Items.get_Item(i).GetComponent<OperateActivityTagItem>();
			component.AwakeSelf();
			component.UpdateItem(openActivitys.get_Item(i));
		}
	}

	public void UpdateTagItemTips(int TypeId)
	{
		Transform transform = this.m_listTab.get_transform().FindChild(TypeId.ToString());
		if (transform != null)
		{
			OperateActivityTagItem component = transform.GetComponent<OperateActivityTagItem>();
			component.UpdateTips(TypeId);
		}
	}

	public void OpenActivityUI(GameObject go)
	{
		if (go == null && OperateActivityManager.Instance.CurrentOpenTypeId < 0)
		{
			OperateActivityManager.Instance.CurrentOpenTypeId = OperateActivityManager.Instance.GetDefaultOpenTypeID();
		}
		else if (go != null)
		{
			OperateActivityManager.Instance.CurrentOpenTypeId = int.Parse(go.get_name());
		}
		for (int i = 0; i < this.m_listTab.Items.get_Count(); i++)
		{
			GameObject gameObject = this.m_listTab.Items.get_Item(i);
			OperateActivityTagItem component = gameObject.GetComponent<OperateActivityTagItem>();
			component.SetSelected(OperateActivityManager.Instance.CurrentOpenTypeId == int.Parse(gameObject.get_name()));
		}
		bool flag = false;
		string uI = OperateActivityTypeID.GetUI(OperateActivityManager.Instance.CurrentOpenTypeId);
		for (int j = 0; j < this.m_TransformContent.get_childCount(); j++)
		{
			GameObject gameObject2 = this.m_TransformContent.GetChild(j).get_gameObject();
			if (gameObject2.get_name().Equals(uI))
			{
				gameObject2.SetActive(true);
				flag = true;
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
		if (!flag)
		{
			UIManagerControl.Instance.OpenUI(uI, this.m_TransformContent, false, UIType.NonPush);
		}
		ActivityInfo activityInfoCurrent = OperateActivityManager.Instance.GetActivityInfoCurrent();
		if (activityInfoCurrent != null && !activityInfoCurrent.firstOpen)
		{
			activityInfoCurrent.firstOpen = true;
			NetworkManager.Send(new FirstOpenReq
			{
				typeId = activityInfoCurrent.typeId
			}, ServerType.Data);
		}
	}
}
