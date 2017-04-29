using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GodSoldierToggle : MonoBehaviour
{
	public Action<GodSoldierToggle> EventHandler;

	private GameObject mGoSelect;

	private GameObject mGoLock;

	private Text mTxLevel;

	private Text mTxTitle;

	private Text mTxSelectTitle;

	private GameObject mGoRedPoint;

	private GodWeaponInfo mData;

	private bool mIsSelect;

	private bool mHasPoint;

	public GodWeaponInfo Data
	{
		get
		{
			return this.mData;
		}
	}

	public bool IsSelect
	{
		get
		{
			return this.mIsSelect;
		}
		set
		{
			if (this.mIsSelect != value)
			{
				this.mIsSelect = value;
				this.mGoSelect.SetActive(this.mIsSelect);
			}
		}
	}

	public bool HasPoint
	{
		get
		{
			return this.mHasPoint;
		}
		set
		{
			if (this.mHasPoint != value)
			{
				this.mHasPoint = value;
				this.mGoRedPoint.SetActive(this.mHasPoint);
			}
		}
	}

	public string Title
	{
		get
		{
			if (this.mTxTitle != null)
			{
				return this.mTxTitle.get_text();
			}
			return string.Empty;
		}
	}

	private void Awake()
	{
		this.mGoSelect = base.get_transform().FindChild("Select").get_gameObject();
		this.mGoLock = base.get_transform().FindChild("txLock").get_gameObject();
		this.mGoRedPoint = base.get_transform().FindChild("RedPoint").get_gameObject();
		this.mTxLevel = base.get_transform().FindChild("txLevel").GetComponent<Text>();
		this.mTxTitle = base.get_transform().FindChild("Text").GetComponent<Text>();
		this.mTxSelectTitle = this.mGoSelect.get_transform().FindChild("SelectText").GetComponent<Text>();
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickToggle);
	}

	private void OnClickToggle(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	public void SetData(GodWeaponInfo data, string title = "")
	{
		if (data != null)
		{
			this.mData = data;
			if (!string.IsNullOrEmpty(title))
			{
				this.mTxTitle.set_text(title);
			}
			this.mTxSelectTitle.set_text(this.mTxTitle.get_text());
			this.mGoLock.SetActive(!this.mData.isOpen);
			this.mTxLevel.get_gameObject().SetActive(this.mData.isOpen);
			this.mTxLevel.set_text("Lv" + this.mData.gLevel);
			this.HasPoint = GodSoldierManager.Instance.CheckMaterialById(this.mData.Type);
		}
	}
}
