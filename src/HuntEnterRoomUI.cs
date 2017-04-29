using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HuntEnterRoomUI : UIBase
{
	private Text mTxAddress;

	private int mRoomId;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("txTop").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(511629, false));
		base.FindTransform("txBottom").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(511630, false));
		this.mTxAddress = base.FindTransform("txAddress").GetComponent<Text>();
		base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirm);
		base.FindTransform("BtnCancel").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
	}

	public void SetData(int roomId)
	{
		this.mRoomId = roomId;
		this.mTxAddress.set_text(HuntManager.Instance.GetRoomName(HuntManager.Instance.MapId, HuntManager.Instance.AreaId, this.mRoomId));
	}

	private void OnClickConfirm(GameObject go)
	{
		WaitUI.OpenUI((uint)(HuntManager.Instance.RefreshTime * 1000));
		HuntManager.Instance.SendEnterRoomReq(HuntManager.Instance.AreaId, this.mRoomId);
		this.Show(false);
	}
}
