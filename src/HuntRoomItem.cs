using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HuntRoomItem : MonoBehaviour
{
	public Action<HuntRoomItem> EventHandler;

	private GameObject mGoSelect;

	private GameObject mGoTeam;

	private Text mTxName;

	private Text mTxDesc;

	public RoomUiInfo Info
	{
		get;
		private set;
	}

	private void Awake()
	{
		this.mGoSelect = UIHelper.GetObject(base.get_transform(), "background/Select");
		this.mGoTeam = UIHelper.GetObject(base.get_transform(), "background/imgTeam");
		this.mTxName = UIHelper.GetText(base.get_transform(), "background/txName");
		this.mTxDesc = UIHelper.GetText(base.get_transform(), "background/txDesc");
		UIHelper.GetCustomButton(base.get_transform(), "background").onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRoom);
	}

	private void OnClickRoom(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	private void RefreshRoom(RoomUiInfo info)
	{
		if (info != null)
		{
			this.mTxName.set_text(info.roomId % HuntManager.Instance.AreaId + "号狩猎点");
			this.mTxDesc.set_text(string.Format("当前人数：{0}/{1}", info.playerNums, HuntManager.Instance.RoomMaxPlayer));
			this.mGoTeam.SetActive(info.teamFlag != 0);
		}
	}

	public void SetData(RoomUiInfo info)
	{
		this.Info = info;
		this.RefreshRoom(info);
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
