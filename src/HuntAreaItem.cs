using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HuntAreaItem : MonoBehaviour
{
	public enum AreaType
	{
		NONE,
		NORMAL,
		CHAOS,
		VIP
	}

	public Action<GuaJiQuYuPeiZhi> EventHandler;

	private GuaJiQuYuPeiZhi mData;

	private Image mBackground;

	private Text mTxTitle;

	private Text mTxTips;

	private readonly string[] BG_IMG = new string[]
	{
		string.Empty,
		"putongqu_icon",
		"huluanqu_icon",
		"vipqu_icon"
	};

	public GuaJiQuYuPeiZhi Data
	{
		get
		{
			return this.mData;
		}
	}

	private void Awake()
	{
		this.mBackground = UIHelper.GetImage(base.get_transform(), "Background");
		this.mTxTitle = UIHelper.GetText(base.get_transform(), "txTitle");
		this.mTxTips = UIHelper.GetText(base.get_transform(), "txTips");
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickArea);
	}

	private void OnClickArea(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this.mData);
		}
	}

	private void RefreshArea(GuaJiQuYuPeiZhi data)
	{
		if (data != null)
		{
			ResourceManager.SetSprite(this.mBackground, ResourceManager.GetIconSprite(this.BG_IMG[data.areaType]));
			this.mTxTitle.set_text(string.Format(GameDataUtils.GetChineseContent(511636, false), AttrUtility.GetExpValueStr(data.exp)));
			if (data.areaType == 3)
			{
				this.mTxTips.set_text(string.Format(GameDataUtils.GetChineseContent(HuntManager.Instance.GetIntOtherData("areaInfoTip" + data.areaType), false), data.condition));
			}
			else
			{
				this.mTxTips.set_text(GameDataUtils.GetChineseContent(HuntManager.Instance.GetIntOtherData("areaInfoTip" + data.areaType), false));
			}
		}
	}

	public void SetData(GuaJiQuYuPeiZhi data)
	{
		this.mData = data;
		this.RefreshArea(data);
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
