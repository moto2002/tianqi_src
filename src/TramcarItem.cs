using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TramcarItem : MonoBehaviour
{
	private List<GameObject> mRewardList;

	private Text mTxTitle;

	private Image mImgTask;

	private Text mTxExp;

	private Text mTxGole;

	private ButtonCustom mBtnBast;

	private GameObject mRewardPanel;

	private GameObject mGoSelect;

	public KuangChePinZhi Data
	{
		get;
		private set;
	}

	public bool IsSelect
	{
		get
		{
			return this.mGoSelect.get_activeSelf();
		}
		set
		{
			this.mGoSelect.SetActive(value);
		}
	}

	private void Awake()
	{
		this.mRewardList = new List<GameObject>();
		this.mTxTitle = UIHelper.GetText(base.get_transform(), "View/txTitle");
		this.mImgTask = UIHelper.GetImage(base.get_transform(), "View/ImgCar");
		this.mTxExp = UIHelper.GetText(base.get_transform(), "View/txExp");
		this.mTxGole = UIHelper.GetText(base.get_transform(), "View/txGold");
		this.mRewardPanel = UIHelper.GetObject(base.get_transform(), "View/Rewards/Grid");
		this.mGoSelect = UIHelper.GetObject(base.get_transform(), "View/Select");
		this.mBtnBast = UIHelper.GetCustomButton(base.get_transform(), "View/BtnBast");
		this.mBtnBast.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBast);
	}

	private void OnClickBast(GameObject go)
	{
		if (TramcarManager.Instance.CurQuality >= TramcarUI.ITEM_COUNT)
		{
			UIManagerControl.Instance.ShowToastText("已到达最高品质！");
		}
		else if (VIPManager.Instance.IsVIPCardOn())
		{
			this.ShowBuyBastTramcar();
		}
		else if (EntityWorld.Instance.EntSelf.VipLv < TramcarManager.Instance.BastTramcarVip)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), string.Format(GameDataUtils.GetChineseContent(513178, false), TramcarManager.Instance.BastTramcarVip), null, delegate
			{
				LinkNavigationManager.OpenVIPUI2Recharge();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
		else if (!VIPManager.Instance.IsVIPPrivilegeOn())
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(508080, false), null, delegate
			{
				LinkNavigationManager.OpenVIPUI2VipLimit();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			this.ShowBuyBastTramcar();
		}
	}

	private void ShowBuyBastTramcar()
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), string.Format(GameDataUtils.GetChineseContent(513662, false), TramcarManager.Instance.UseDiamond), null, delegate
		{
			TramcarManager.Instance.SendRefreshTramcarReq(5, true);
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void ClearReward()
	{
		for (int i = 0; i < this.mRewardList.get_Count(); i++)
		{
			this.mRewardList.get_Item(i).set_name("Unused");
			this.mRewardList.get_Item(i).SetActive(false);
		}
	}

	private GameObject GetUnusedItem()
	{
		for (int i = 0; i < this.mRewardList.get_Count(); i++)
		{
			if (this.mRewardList.get_Item(i).get_name() == "Unused")
			{
				return this.mRewardList.get_Item(i);
			}
		}
		return null;
	}

	private GameObject CreateRewards(int id, long value)
	{
		GameObject go = this.GetUnusedItem();
		if (go == null)
		{
			go = ResourceManager.GetInstantiate2Prefab("TramcarRewardItem");
			go.GetComponent<Button>().get_onClick().AddListener(delegate
			{
				int num = int.Parse(go.get_name());
				if (num != 1)
				{
					ItemTipUIViewModel.ShowItem(num, null);
				}
			});
			UGUITools.SetParent(this.mRewardPanel, go, false);
			this.mRewardList.Add(go);
		}
		go.set_name(id.ToString());
		ResourceManager.SetSprite(go.GetComponent<Image>(), GameDataUtils.GetItemFrame(id));
		ResourceManager.SetSprite(go.get_transform().FindChild("Image").GetComponent<Image>(), GameDataUtils.GetItemIcon(id));
		go.get_transform().FindChild("Text").GetComponent<Text>().set_text(Utils.SwitchChineseNumber(value, 1));
		Items items = DataReader<Items>.Get(id);
		if (items == null || items.step <= 0)
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(false);
		}
		else
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(true);
			go.get_transform().FindChild("ItemStep").FindChild("ItemStepText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
		go.SetActive(true);
		return go;
	}

	public void SetData(KuangChePinZhi data)
	{
		if (data != null)
		{
			this.Data = data;
			this.Refresh();
		}
	}

	public void Refresh()
	{
		this.mTxTitle.set_text(TramcarManager.Instance.TRAMCAR_NAME[this.Data.quality]);
		ResourceManager.SetSprite(this.mImgTask, ResourceManager.GetIconSprite("kuangche_" + this.Data.quality));
		this.mBtnBast.get_gameObject().SetActive(this.Data.quality >= 5 && (VIPManager.Instance.IsVIPCardOn() || (EntityWorld.Instance.EntSelf.VipLv >= TramcarManager.Instance.BastTramcarVip && VIPManager.Instance.IsVIPPrivilegeOn())));
		this.ClearReward();
		List<DropItem> tramcarRewards = TramcarManager.Instance.TramcarRewards;
		if (tramcarRewards != null)
		{
			for (int i = 0; i < tramcarRewards.get_Count(); i++)
			{
				if (tramcarRewards.get_Item(i).typeId == 1)
				{
					this.mTxExp.set_text("经验：" + AttrUtility.GetExpValueStr(tramcarRewards.get_Item(i).count * (long)this.Data.parament / 100L));
				}
				else if (tramcarRewards.get_Item(i).typeId == 2)
				{
					this.mTxGole.set_text("金币：" + AttrUtility.GetGoldValueStr(tramcarRewards.get_Item(i).count * (long)this.Data.parament / 100L));
				}
				else
				{
					this.CreateRewards(tramcarRewards.get_Item(i).typeId, tramcarRewards.get_Item(i).count * (long)this.Data.parament / 100L);
				}
			}
		}
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
		this.IsSelect = false;
	}
}
