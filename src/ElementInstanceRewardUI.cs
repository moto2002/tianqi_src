using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceRewardUI : UIBase
{
	private Transform ContentProperty;

	private Text TextContent;

	private Transform ContentItems;

	private GridLayoutGroup GridItems;

	private Text TextTitle;

	private List<GameObject> listRewards = new List<GameObject>();

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ContentProperty = base.FindTransform("ContentProperty");
		this.TextContent = base.FindTransform("TextContent").GetComponent<Text>();
		this.TextTitle = base.FindTransform("TextTitle").GetComponent<Text>();
		this.ContentItems = base.FindTransform("ContentItems");
		this.GridItems = base.FindTransform("GridItems").GetComponent<GridLayoutGroup>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	public void RefreshUI(string blockID)
	{
		base.GetComponent<BaseTweenAlphaBaseTime>().TweenAlpha(0f, 1f, 0f, 0.5f);
		BlockInfo blockInfo = ElementInstanceManager.Instance.GetBlockInfo(blockID);
		switch (blockInfo.incidentType)
		{
		case RandomIncidentType.IncidentType.TOOL:
		{
			this.ContentProperty.get_gameObject().SetActive(false);
			this.ContentItems.get_gameObject().SetActive(true);
			YDaoJuKu yDaoJuKu = DataReader<YDaoJuKu>.Get(blockInfo.incidentTypeId);
			string text = GameDataUtils.GetChineseContent(502320, false);
			text = text.Replace("{s1}", yDaoJuKu.holdName);
			this.TextTitle.set_text(text);
			for (int i = 0; i < this.listRewards.get_Count(); i++)
			{
				Object.Destroy(this.listRewards.get_Item(i));
			}
			this.listRewards.Clear();
			for (int j = 0; j < GlobalManager.Instance.DropGoods.get_Count(); j++)
			{
				ItemBriefInfo itemBriefInfo = GlobalManager.Instance.DropGoods.get_Item(j);
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstanceRewardItem");
				instantiate2Prefab.get_transform().set_parent(this.GridItems.get_transform());
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				Vector3 localPosition = instantiate2Prefab.GetComponent<RectTransform>().get_localPosition();
				localPosition.z = 0f;
				instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(localPosition);
				instantiate2Prefab.AddComponent<ElementInstanceSettleItem>();
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSettleItem);
				instantiate2Prefab.GetComponent<ElementInstanceSettleItem>().itemID = itemBriefInfo.cfgId;
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetItemIcon(itemBriefInfo.cfgId));
				ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageFrame").GetComponent<Image>(), GameDataUtils.GetItemFrame(itemBriefInfo.cfgId));
				instantiate2Prefab.get_transform().FindChild("Text").GetComponent<Text>().set_text(itemBriefInfo.count.ToString());
				this.listRewards.Add(instantiate2Prefab);
			}
			break;
		}
		case RandomIncidentType.IncidentType.PETPROPERTY:
		{
			this.ContentProperty.get_gameObject().SetActive(true);
			this.ContentItems.get_gameObject().SetActive(false);
			YChongWuJiaChengKu yChongWuJiaChengKu = DataReader<YChongWuJiaChengKu>.Get(blockInfo.incidentTypeId);
			string text2 = yChongWuJiaChengKu.depict;
			text2 = text2.Replace("{s1}", yChongWuJiaChengKu.addNum.ToString());
			this.TextContent.set_text(text2);
			text2 = GameDataUtils.GetChineseContent(502320, false);
			text2 = text2.Replace("{s1}", yChongWuJiaChengKu.eventName);
			this.TextTitle.set_text(text2);
			break;
		}
		case RandomIncidentType.IncidentType.PLAYERPROPERTY:
		{
			this.ContentProperty.get_gameObject().SetActive(true);
			this.ContentItems.get_gameObject().SetActive(false);
			YJiaoSeJiaChengKu yJiaoSeJiaChengKu = DataReader<YJiaoSeJiaChengKu>.Get(blockInfo.incidentTypeId);
			string text3 = yJiaoSeJiaChengKu.depict;
			text3 = text3.Replace("{s1}", yJiaoSeJiaChengKu.addNum.ToString());
			this.TextContent.set_text(text3);
			text3 = GameDataUtils.GetChineseContent(502320, false);
			text3 = text3.Replace("{s1}", yJiaoSeJiaChengKu.eventName);
			this.TextTitle.set_text(text3);
			break;
		}
		case RandomIncidentType.IncidentType.RECOVRYENERGY:
		{
			this.ContentProperty.get_gameObject().SetActive(true);
			this.ContentItems.get_gameObject().SetActive(false);
			YNengLiangHuiFu yNengLiangHuiFu = DataReader<YNengLiangHuiFu>.Get(blockInfo.incidentTypeId);
			string text4 = yNengLiangHuiFu.powerName;
			text4 = text4.Replace("{s1}", yNengLiangHuiFu.powerPoint.ToString());
			this.TextContent.set_text(text4);
			text4 = GameDataUtils.GetChineseContent(502320, false);
			text4 = text4.Replace("{s1}", yNengLiangHuiFu.eventName);
			this.TextTitle.set_text(text4);
			break;
		}
		}
	}

	private void OnClickSettleItem(GameObject sender)
	{
		ItemTipUIViewModel.ShowItem(sender.GetComponent<ElementInstanceSettleItem>().itemID, null);
	}
}
