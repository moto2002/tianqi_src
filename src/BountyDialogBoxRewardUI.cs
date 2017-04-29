using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyDialogBoxRewardUI : UIBase
{
	public Text Title;

	public Transform Items;

	public ButtonCustom Confirm;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.75f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.Confirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OncClickBtnGet);
	}

	protected override void OnEnable()
	{
		base.GetComponent<CanvasGroup>().set_alpha(0f);
		this.isClick = false;
		base.SetMask();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetReceiveAwardRes, new Callback(this.OnGetReceiveAwardRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetReceiveAwardRes, new Callback(this.OnGetReceiveAwardRes));
	}

	public void InitUI(int titleId, Dictionary<int, int> itemIdNum)
	{
		this.isClick = false;
		base.SetMask();
		for (int i = 0; i < this.Items.get_childCount(); i++)
		{
			Object.Destroy(this.Items.GetChild(i).get_gameObject());
		}
		using (Dictionary<int, int>.Enumerator enumerator = itemIdNum.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				ItemShow.ShowItem(this.Items, current.get_Key(), (long)current.get_Value(), false, null, 2001);
			}
		}
		FXSpineManager.Instance.PlaySpine(801, base.get_transform(), string.Empty, 3001, delegate
		{
			FXSpineManager.Instance.PlaySpine(802, UINodesManager.MiddleUIRoot, string.Empty, 3002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			TimerHeap.AddTimer(300u, 0, delegate
			{
				base.GetComponent<CanvasGroup>().set_alpha(1f);
				this.isClick = true;
				base.SetMask();
			});
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void OncClickBtnGet(GameObject sender)
	{
		this.Show(false);
	}

	private void OnClickInstanceRewardItem(GameObject sender)
	{
		ItemTipUIViewModel.ShowItem(int.Parse(sender.get_name()), null);
	}

	private void OnGetReceiveAwardRes()
	{
		this.Show(false);
	}
}
