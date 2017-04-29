using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_BtnOKText = "BtnOKText";

		public const string Attr_ConsumeIcon = "ConsumeIcon";

		public const string Attr_ConsumeNum = "ConsumeNum";

		public const string Attr_ObatinItems = "ObatinItems";

		public const string Attr_SpecialItemText = "SpecialItemText";

		public const string Attr_VIPLevel10 = "VIPLevel10";

		public const string Attr_VIPLevel1 = "VIPLevel1";

		public const string Attr_ConsumeOn = "ConsumeOn";

		public const string Event_OnClickOK = "OnClickOK";
	}

	public static TreasureUIViewModel Instance;

	private string _BtnOKText;

	private bool _ConsumeOn;

	private SpriteRenderer _ConsumeIcon;

	private string _ConsumeNum;

	private string _SpecialItemText;

	private SpriteRenderer _VIPLevel10;

	private SpriteRenderer _VIPLevel1;

	public ObservableCollection<OOItem2Draw> ObatinItems = new ObservableCollection<OOItem2Draw>();

	private VipXiaoGuo VIPEffect;

	public string BtnOKText
	{
		get
		{
			return this._BtnOKText;
		}
		set
		{
			this._BtnOKText = value;
			base.NotifyProperty("BtnOKText", value);
		}
	}

	public bool ConsumeOn
	{
		get
		{
			return this._ConsumeOn;
		}
		set
		{
			this._ConsumeOn = value;
			base.NotifyProperty("ConsumeOn", value);
		}
	}

	public SpriteRenderer ConsumeIcon
	{
		get
		{
			return this._ConsumeIcon;
		}
		set
		{
			this._ConsumeIcon = value;
			base.NotifyProperty("ConsumeIcon", value);
		}
	}

	public string ConsumeNum
	{
		get
		{
			return this._ConsumeNum;
		}
		set
		{
			this._ConsumeNum = value;
			base.NotifyProperty("ConsumeNum", value);
		}
	}

	public string SpecialItemText
	{
		get
		{
			return this._SpecialItemText;
		}
		set
		{
			this._SpecialItemText = value;
			base.NotifyProperty("SpecialItemText", value);
		}
	}

	public SpriteRenderer VIPLevel10
	{
		get
		{
			return this._VIPLevel10;
		}
		set
		{
			this._VIPLevel10 = value;
			base.NotifyProperty("VIPLevel10", value);
		}
	}

	public SpriteRenderer VIPLevel1
	{
		get
		{
			return this._VIPLevel1;
		}
		set
		{
			this._VIPLevel1 = value;
			base.NotifyProperty("VIPLevel1", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		TreasureUIViewModel.Instance = this;
	}

	public void OnClickOK()
	{
		TreasureUIView.Instance.Show(false);
		if (this.VIPEffect != null)
		{
			VIPManager.Instance.SendOpenVipBox(this.VIPEffect.effect);
		}
	}

	public static bool IsTreasureValid(int vipLevel, VipXiaoGuo dataVIPEffect)
	{
		VipEffectInfo vipEffectInfo = VIPManager.Instance.GetVipEffectInfo(vipLevel, dataVIPEffect.effect);
		if (vipEffectInfo != null && vipEffectInfo.opened && !vipEffectInfo.boxOpened)
		{
			return true;
		}
		if (vipEffectInfo != null && !vipEffectInfo.opened)
		{
			return false;
		}
		if (vipEffectInfo != null && vipEffectInfo.boxOpened)
		{
			UIManagerControl.Instance.ShowToastText("宝箱已开启");
		}
		else if (vipEffectInfo != null && !vipEffectInfo.opened)
		{
			UIManagerControl.Instance.ShowToastText("VIP等级不足");
		}
		return false;
	}

	public static bool IsTreasureObtain(int vipLevel, VipXiaoGuo dataVIPEffect)
	{
		VipEffectInfo vipEffectInfo = VIPManager.Instance.GetVipEffectInfo(vipLevel, dataVIPEffect.effect);
		return (vipEffectInfo == null || !vipEffectInfo.opened || vipEffectInfo.boxOpened) && vipEffectInfo != null && vipEffectInfo.boxOpened;
	}

	public void OpenTreasure(int vipLevel, VipXiaoGuo dataVIPEffect)
	{
		if (!TreasureUIViewModel.IsTreasureValid(vipLevel, dataVIPEffect))
		{
			TreasureUIView.Instance.Show(false);
			return;
		}
		UIManagerControl.Instance.OpenUI("TreasureUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
		this.ObatinItems.Clear();
		this.SpecialItemText = string.Empty;
		this.VIPEffect = dataVIPEffect;
		this.VIPLevel10 = GameDataUtils.GetNumIcon10(vipLevel, NumType.Yellow_light);
		this.VIPLevel1 = GameDataUtils.GetNumIcon1(vipLevel, NumType.Yellow_light);
		this.BtnOKText = GameDataUtils.GetChineseContent(508009, false);
		if (dataVIPEffect.value2 > 0 && dataVIPEffect.value3 > 0)
		{
			this.ConsumeOn = true;
			this.ConsumeIcon = GameDataUtils.GetItemIcon(dataVIPEffect.value2);
			this.ConsumeNum = dataVIPEffect.value3.ToString();
		}
		else
		{
			this.ConsumeOn = false;
		}
		string text = string.Empty;
		List<VipBoxItemInfo> vIPBox = VIPManager.Instance.GetVIPBox(dataVIPEffect.effect);
		if (vIPBox != null)
		{
			for (int i = 0; i < vIPBox.get_Count(); i++)
			{
				VipBoxItemInfo vipBoxItemInfo = vIPBox.get_Item(i);
				if (SpecialItem.IsSpecial(vipBoxItemInfo.itemId))
				{
					string colorByID = TextColorMgr.GetColorByID("x" + vipBoxItemInfo.itemCount, 405);
					if (string.IsNullOrEmpty(text))
					{
						text = GameDataUtils.GetItemName(vipBoxItemInfo.itemId, true, 0L) + colorByID;
					}
					else
					{
						text = text + ", " + GameDataUtils.GetItemName(vipBoxItemInfo.itemId, true, 0L) + colorByID;
					}
				}
				else
				{
					OOItem2Draw oOItem2Draw = new OOItem2Draw();
					oOItem2Draw.ID = vipBoxItemInfo.itemId;
					oOItem2Draw.ItemIcon = GameDataUtils.GetItemIcon(vipBoxItemInfo.itemId);
					oOItem2Draw.ItemName = Utils.GetItemNum(vipBoxItemInfo.itemId, (long)vipBoxItemInfo.itemCount);
					this.ObatinItems.Add(oOItem2Draw);
				}
			}
		}
		this.SpecialItemText = text;
	}
}
