using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PopButtonsAdjustUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ButtonInfos = "ButtonInfos";
	}

	private static PopButtonsAdjustUIViewModel m_instance;

	public ObservableCollection<OOButtonInfo2MVVM> ButtonInfos = new ObservableCollection<OOButtonInfo2MVVM>();

	public static PopButtonsAdjustUIViewModel Instance
	{
		get
		{
			return PopButtonsAdjustUIViewModel.m_instance;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PopButtonsAdjustUIViewModel.m_instance = this;
	}

	public void SetButtonInfos(List<ButtonInfoData> datas)
	{
		float num = (float)datas.get_Count() * 65f + 30f;
		PopButtonsAdjustUIView.Instance.SetBackgroundHeight(num);
		this.CheckBound(260, (int)num);
		this.ButtonInfos.Clear();
		if (datas == null)
		{
			return;
		}
		for (int i = 0; i < datas.get_Count(); i++)
		{
			ButtonInfoData data = datas.get_Item(i);
			OOButtonInfo2MVVM oOButtonInfo2MVVM = new OOButtonInfo2MVVM();
			oOButtonInfo2MVVM.ButtonName = data.buttonName;
			oOButtonInfo2MVVM.ButtonBg = ButtonColorMgr.GetButton(data.color);
			oOButtonInfo2MVVM.IsShowRedPoint = data.isShowRedPoint;
			oOButtonInfo2MVVM.OnCallback = delegate
			{
				PopButtonsAdjustUIView.Instance.Show(false);
				if (data.onCall != null)
				{
					data.onCall.Invoke();
				}
			};
			this.ButtonInfos.Add(oOButtonInfo2MVVM);
		}
	}

	private void CheckBound(int width, int height)
	{
		float x = (base.get_transform() as RectTransform).get_anchoredPosition().x;
		float y = (base.get_transform() as RectTransform).get_anchoredPosition().y;
		UIUtils.GetBound(ref x, ref y, (float)width, (float)height, new Vector2(0f, 1f));
		(base.get_transform() as RectTransform).set_anchoredPosition(new Vector2(x, y));
	}

	public static void Open(Transform parent)
	{
		UIManagerControl.Instance.OpenUI("PopButtonsAdjustUI", parent, false, UIType.NonPush);
	}

	public void Close()
	{
		PopButtonsAdjustUIView.Instance.Show(false);
	}
}
