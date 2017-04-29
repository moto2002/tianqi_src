using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PopButtonsUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ButtonInfos = "ButtonInfos";
	}

	private static PopButtonsUIViewModel m_instance;

	public ObservableCollection<OOButtonInfo2MVVM> ButtonInfos = new ObservableCollection<OOButtonInfo2MVVM>();

	public static PopButtonsUIViewModel Instance
	{
		get
		{
			return PopButtonsUIViewModel.m_instance;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PopButtonsUIViewModel.m_instance = this;
	}

	public void SetButtonInfos(List<ButtonInfoData> datas)
	{
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
			oOButtonInfo2MVVM.OnCallback = delegate
			{
				PopButtonsUIView.Instance.Show(false);
				if (data.onCall != null)
				{
					data.onCall.Invoke();
				}
			};
			this.ButtonInfos.Add(oOButtonInfo2MVVM);
		}
	}

	public static void Open(Transform parent)
	{
		UIManagerControl.Instance.OpenUI("PopPrvateButtonsUI", parent, false, UIType.NonPush);
	}

	public void Close()
	{
		PopButtonsUIView.Instance.Show(false);
	}
}
