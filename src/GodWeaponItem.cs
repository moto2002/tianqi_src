using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GodWeaponItem : MonoBehaviour
{
	public enum State
	{
		NOT_OPEN,
		WAITING,
		HAVE_GOT,
		READY
	}

	private Image mImgIcon;

	private Text mTxName;

	private Image mTips;

	private Image mBackground;

	public Action<int, HolyWeaponInfo> EventHandler;

	private HolyWeaponInfo mInfo;

	private Artifact mData;

	private int mIndex = -1;

	public HolyWeaponInfo Info
	{
		get
		{
			return this.mInfo;
		}
	}

	public int Index
	{
		get
		{
			return this.mIndex;
		}
	}

	private void Awake()
	{
		this.mBackground = base.GetComponent<Image>();
		this.mTxName = UIHelper.GetText(base.get_transform(), "Name");
		this.mImgIcon = UIHelper.GetImage(base.get_transform(), "Icon");
		this.mTips = UIHelper.GetImage(base.get_transform(), "Tips");
	}

	private void Start()
	{
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickIcon);
	}

	private void OnClickIcon(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this.mIndex, this.mInfo);
		}
	}

	private void RefreshState(GodWeaponItem.State state)
	{
		switch (state)
		{
		case GodWeaponItem.State.NOT_OPEN:
			ResourceManager.SetSprite(this.mBackground, ResourceManager.GetIconSprite("dikuang_2"));
			this.mTips.get_gameObject().SetActive(false);
			this.mImgIcon.set_color(Color.get_gray());
			this.mTxName.set_color(new Color(0.45f, 0.4f, 0.4f));
			break;
		case GodWeaponItem.State.WAITING:
		case GodWeaponItem.State.READY:
			ResourceManager.SetSprite(this.mBackground, ResourceManager.GetIconSprite("dikuang_2"));
			ResourceManager.SetSprite(this.mTips, ResourceManager.GetIconSprite("yinzhang_2"));
			this.mTips.get_gameObject().SetActive(true);
			this.mImgIcon.set_color(Color.get_gray());
			this.mTxName.set_color(new Color(0.45f, 0.4f, 0.4f));
			break;
		case GodWeaponItem.State.HAVE_GOT:
			ResourceManager.SetSprite(this.mBackground, ResourceManager.GetIconSprite("dikuang_1"));
			ResourceManager.SetSprite(this.mTips, ResourceManager.GetIconSprite("yinzhang_1"));
			this.mTips.get_gameObject().SetActive(true);
			this.mImgIcon.set_color(Color.get_white());
			this.mTxName.set_color(new Color(0.7f, 0.56f, 0.33f));
			break;
		}
	}

	public void SetData(int index, HolyWeaponInfo info)
	{
		this.mIndex = index;
		if (info != null)
		{
			this.mInfo = info;
			this.mData = DataReader<Artifact>.Get(this.mInfo.Id);
			if (this.mData != null)
			{
				this.mTxName.set_text(GameDataUtils.GetChineseContent(this.mData.name, false));
				ResourceManager.SetSprite(this.mImgIcon, GameDataUtils.GetIcon(this.mData.model));
				this.RefreshState((GodWeaponItem.State)this.mInfo.State);
			}
		}
	}

	public void Unused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
