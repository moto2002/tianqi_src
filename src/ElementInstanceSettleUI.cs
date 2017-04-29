using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceSettleUI : UIBase
{
	private GridLayoutGroup GridItems;

	private ButtonCustom BtnQuit;

	private List<GameObject> listObjs = new List<GameObject>();

	private Text GoldNum;

	private Text ExpNum;

	private Transform Exp;

	private Transform Gold;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.3f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BtnQuit = base.FindTransform("BtnQuit").GetComponent<ButtonCustom>();
		this.GridItems = base.FindTransform("GridItems").GetComponent<GridLayoutGroup>();
		this.GoldNum = base.FindTransform("GoldNum").GetComponent<Text>();
		this.ExpNum = base.FindTransform("ExpNum").GetComponent<Text>();
		this.Gold = base.FindTransform("Gold");
		this.Exp = base.FindTransform("Exp");
		this.BtnQuit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnQuit);
	}

	public void RefreshUI(List<CopyReward> copyRewards)
	{
		base.GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(0f, 1f, 0f, 0.5f);
		for (int i = 0; i < this.listObjs.get_Count(); i++)
		{
			Object.Destroy(this.listObjs.get_Item(i));
		}
		this.listObjs.Clear();
		bool flag = false;
		bool flag2 = false;
		for (int j = 0; j < copyRewards.get_Count(); j++)
		{
			CopyReward copyReward = copyRewards.get_Item(j);
			if (DataReader<YWanFaSheZhi>.Get("goldID").num == copyReward.itemId)
			{
				flag = true;
				this.GoldNum.set_text(copyReward.itemNum.ToString());
				if (copyReward.itemNum == 0)
				{
					this.Gold.get_gameObject().SetActive(false);
				}
				else
				{
					this.Gold.get_gameObject().SetActive(true);
				}
			}
			else if (DataReader<YWanFaSheZhi>.Get("expID").num == copyReward.itemId)
			{
				flag2 = true;
				this.ExpNum.set_text(copyReward.itemNum.ToString());
				if (copyReward.itemNum == 0)
				{
					this.Exp.get_gameObject().SetActive(false);
				}
				else
				{
					this.Exp.get_gameObject().SetActive(true);
				}
			}
			else
			{
				if (!flag)
				{
					this.Gold.get_gameObject().SetActive(false);
				}
				if (!flag2)
				{
					this.Exp.get_gameObject().SetActive(false);
				}
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ElementInstanceSettleItem");
				instantiate2Prefab.get_transform().set_parent(this.GridItems.get_transform());
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				Vector3 localPosition = instantiate2Prefab.GetComponent<RectTransform>().get_localPosition();
				localPosition.z = 0f;
				instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(localPosition);
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSettleItem);
				instantiate2Prefab.GetComponent<ElementInstanceSettleItem>().itemID = copyReward.itemId;
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetIcon(DataReader<Items>.Get(copyReward.itemId).icon));
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite("Frame_" + DataReader<Items>.Get(copyReward.itemId).color));
				instantiate2Prefab.get_transform().FindChild("TextNum").GetComponent<Text>().set_text(copyReward.itemNum.ToString());
				this.listObjs.Add(instantiate2Prefab);
			}
		}
	}

	protected void OnClickBtnQuit(GameObject sender)
	{
		ElementInstanceManager.Instance.SendExitElementCopyReq(delegate
		{
			UIStackManager.Instance.PopUIPrevious(UIType.FullScreen);
		});
	}

	protected void OnClickSettleItem(GameObject sender)
	{
		ItemTipUIViewModel.ShowItem(sender.GetComponent<ElementInstanceSettleItem>().itemID, null);
	}
}
