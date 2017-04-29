using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildLogItem : BaseUIBehaviour
{
	private Text itemNameText;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.itemNameText = base.FindTransform("ItemName").GetComponent<Text>();
		this.isInit = true;
	}

	public void RefreshUI(GuildLogTrace logTrace)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.itemNameText.set_text(string.Empty);
		DateTime dateTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(logTrace.logTimeUtc);
		int itemID = 0;
		if (logTrace.items.get_Count() > 0)
		{
			itemID = logTrace.items.get_Item(0).itemId;
		}
		if (logTrace.logType == GuildLogType.GDLT.GuildStorageEquipDonate)
		{
			base.FindTransform("LogTime").GetComponent<Text>().set_text(string.Concat(new object[]
			{
				dateTime.get_Month(),
				"-",
				dateTime.get_Day(),
				" "
			}));
		}
		else
		{
			base.FindTransform("LogTime").GetComponent<Text>().set_text("[" + dateTime.ToString() + "]");
		}
		base.FindTransform("Content").GetComponent<Text>().set_text(this.GetLogContent((int)logTrace.logType, logTrace.roleName, logTrace.value, itemID));
	}

	private string GetLogContent(int type, string roleName, int value, int itemID = 0)
	{
		GongHuiRiZhi gongHuiRiZhi = DataReader<GongHuiRiZhi>.Get(type);
		string result = string.Empty;
		if (gongHuiRiZhi == null)
		{
			return result;
		}
		switch (type)
		{
		case 1:
		case 2:
		case 3:
			result = string.Format(gongHuiRiZhi.log, roleName);
			break;
		case 4:
		{
			string titleName = GuildManager.Instance.GetTitleName((MemberTitleType.MTT)value);
			result = string.Format(gongHuiRiZhi.log, roleName, titleName);
			break;
		}
		case 5:
			result = gongHuiRiZhi.log;
			break;
		case 6:
		{
			string text = string.Empty;
			text = GameDataUtils.GetItemName(itemID, true, 0L);
			this.itemNameText.set_text(text);
			result = string.Format(gongHuiRiZhi.log, roleName, string.Empty);
			break;
		}
		case 7:
			result = string.Format(gongHuiRiZhi.log, value);
			break;
		case 8:
			result = string.Format(gongHuiRiZhi.log, roleName, value);
			break;
		case 11:
		case 12:
		{
			Items item = BackpackManager.Instance.GetItem(itemID);
			string content = string.Empty;
			if (item != null)
			{
				content = GameDataUtils.GetChineseContent(item.name, false);
				this.itemNameText.set_text(GameDataUtils.GetItemName(itemID, true, 0L));
			}
			string content2 = string.Format(gongHuiRiZhi.log, roleName, string.Empty);
			float helpTextPreferredWidth = this.GetHelpTextPreferredWidth(content2);
			float helpTextPreferredWidth2 = this.GetHelpTextPreferredWidth(content);
			DetailInfo detailInfo = new DetailInfo();
			detailInfo.cfgId = itemID;
			detailInfo.type = DetailType.DT.Equipment;
			Button2Touch button2Touch = base.get_transform().GetComponentInChildren<Button2Touch>();
			if (button2Touch == null)
			{
				button2Touch = this.GetButtonTouch(itemID);
				button2Touch.SetButton2Touch(null, base.get_transform(), detailInfo, null);
			}
			else
			{
				button2Touch.SetButton2Touch(null, base.get_transform(), detailInfo, null);
			}
			if (button2Touch != null)
			{
				float num = 338f + helpTextPreferredWidth;
				button2Touch.m_myRectTransform.set_anchoredPosition(new Vector2(num, 5f));
				button2Touch.Underline(helpTextPreferredWidth2 + 10f, 30, false);
			}
			result = string.Format(gongHuiRiZhi.log, roleName, string.Empty);
			break;
		}
		}
		return result;
	}

	private Button2Touch GetButtonTouch(int itemID)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Button2Touch");
		instantiate2Prefab.set_name("Item_" + itemID);
		return instantiate2Prefab.GetComponent<Button2Touch>();
	}

	private float GetHelpTextPreferredWidth(string content)
	{
		Text component = base.FindTransform("ContentHelp").GetComponent<Text>();
		component.set_text(TextColorMgr.FilterColor(content));
		return component.get_preferredWidth();
	}
}
