using System;
using UnityEngine;
using UnityEngine.UI;

public class GodSoldierItem : MonoBehaviour
{
	public Action<GodSoldierItem> EventHandler;

	private int mItemId;

	private Text mTxNumber;

	private Image mItemIcon;

	private Image mItemFrame;

	private bool mIsSelect;

	private GameObject mGoSelect;

	public int ItemId
	{
		get
		{
			return this.mItemId;
		}
	}

	public string Number
	{
		set
		{
			if (this.mTxNumber != null)
			{
				this.mTxNumber.set_text(value);
			}
		}
	}

	public bool IsSelect
	{
		get
		{
			return this.mGoSelect.get_activeSelf();
		}
		set
		{
			if (this.mIsSelect != value)
			{
				this.mIsSelect = value;
				this.mGoSelect.SetActive(value);
			}
		}
	}

	private void Awake()
	{
		this.mGoSelect = base.get_transform().FindChild("Select").get_gameObject();
		this.mGoSelect.SetActive(false);
		this.mIsSelect = false;
		this.mTxNumber = UIHelper.GetText(base.get_transform(), "RewardItemText");
		this.mItemIcon = UIHelper.GetImage(base.get_transform(), "RewardItemIcon");
		this.mItemFrame = UIHelper.GetImage(base.get_transform(), "RewardItemFrame");
	}

	private void Start()
	{
		base.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRewardItem);
	}

	private void OnClickRewardItem(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	public void SetData(int itemId, long num)
	{
		this.mItemId = itemId;
		ResourceManager.SetSprite(this.mItemFrame, GameDataUtils.GetItemFrame(this.mItemId));
		ResourceManager.SetSprite(this.mItemIcon, GameDataUtils.GetItemIcon(this.mItemId));
		this.mItemIcon.SetNativeSize();
		this.mTxNumber.set_text(num.ToString());
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
