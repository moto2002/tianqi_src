using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceReferenceUI : UIBase
{
	private const int LEVEL_TIP_ID = 502255;

	private Text TextTitle;

	private Transform Grid;

	private Text TextLastNum;

	private ScrollRect Scroll;

	private Transform Icon;

	private List<GameObject> ListFromItem = new List<GameObject>();

	private static Action m_actionClickGoToItem;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextTitle = base.FindTransform("TextTitle").GetComponent<Text>();
		this.Grid = base.FindTransform("Grid");
		this.TextLastNum = base.FindTransform("TextLastNum").GetComponent<Text>();
		this.Scroll = base.FindTransform("Scroll").GetComponent<ScrollRect>();
		this.Icon = base.FindTransform("Icon");
	}

	private Hashtable CheckIsOpen(int openID)
	{
		Debug.LogError("openID  " + openID);
		bool flag = false;
		Hashtable hashtable = new Hashtable();
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(openID);
		int level = systemOpen.level;
		if (EntityWorld.Instance.EntSelf.Lv >= level)
		{
			flag = true;
		}
		hashtable.Add("isOpen", flag);
		hashtable.Add("openLevel", level);
		return hashtable;
	}

	public void ShowSourceReference(int itemId, Action actionClickGoToItem = null)
	{
		SourceReferenceUI.m_actionClickGoToItem = actionClickGoToItem;
		this.Scroll.set_verticalNormalizedPosition(1f);
		for (int i = 0; i < this.ListFromItem.get_Count(); i++)
		{
			Object.Destroy(this.ListFromItem.get_Item(i));
		}
		this.ListFromItem.Clear();
		GridLayoutGroup component = this.Grid.GetComponent<GridLayoutGroup>();
		Items items = DataReader<Items>.Get(itemId);
		if (items == null)
		{
			Debug.LogError("GameData.Items no exist, id = " + itemId);
			return;
		}
		ResourceManager.SetSprite(this.Icon.FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetItemIcon(itemId));
		ResourceManager.SetSprite(this.Icon.FindChild("ImageFrame").GetComponent<Image>(), GameDataUtils.GetItemFrame(itemId));
		this.TextTitle.set_text(GameDataUtils.GetChineseContent(items.name, false));
		if (itemId == 5)
		{
			this.TextLastNum.set_text(EntityWorld.Instance.EntSelf.CompetitiveCurrency.ToString());
		}
		else
		{
			this.TextLastNum.set_text(BackpackManager.Instance.OnGetGoodCount(itemId).ToString());
		}
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int j = 0; j < items.getWay.get_Count(); j++)
		{
			int key = items.getWay.get_Item(j);
			DLuJingShuXing dLuJingShuXing = DataReader<DLuJingShuXing>.Get(key);
			bool flag = true;
			if (dLuJingShuXing.systemOpenID != 0)
			{
				Hashtable hashtable = this.CheckIsOpen(dLuJingShuXing.systemOpenID);
				flag = (bool)hashtable.get_Item("isOpen");
			}
			if (flag)
			{
				list2.Add(items.getWay.get_Item(j));
			}
			else
			{
				list3.Add(items.getWay.get_Item(j));
			}
		}
		list.AddRange(list2);
		list.AddRange(list3);
		int k = 0;
		while (k < list.get_Count())
		{
			int key2 = list.get_Item(k);
			DLuJingShuXing dLuJingShuXing2 = DataReader<DLuJingShuXing>.Get(key2);
			bool flag2 = true;
			int imageNum = 0;
			if (dLuJingShuXing2.systemOpenID != 0)
			{
				Hashtable hashtable2 = this.CheckIsOpen(dLuJingShuXing2.systemOpenID);
				flag2 = (bool)hashtable2.get_Item("isOpen");
				imageNum = (int)hashtable2.get_Item("openLevel");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			text = dLuJingShuXing2.name;
			text2 = dLuJingShuXing2.desc;
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipFromItem");
			GameObject gameObject = instantiate2Prefab.get_transform().FindChild("ImageGo").get_gameObject();
			GameObject gameObject2 = instantiate2Prefab.get_transform().FindChild("ImageNotOpen").get_gameObject();
			GameObject gameObject3 = instantiate2Prefab.get_transform().FindChild("LevelOpen").get_gameObject();
			GameObject gameObject4 = instantiate2Prefab.get_transform().FindChild("ImageBuy").get_gameObject();
			ImageNums component2 = instantiate2Prefab.get_transform().FindChild("LevelOpen").FindChild("ImageNum").GetComponent<ImageNums>();
			component2.Init(new Vector2(26f, 33f), "font_vip_");
			if (dLuJingShuXing2.type == 0)
			{
				int num = (int)float.Parse(dLuJingShuXing2.invokeParam);
				ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(num);
				if (zhuXianPeiZhi != null)
				{
					Hashtable hashtable3 = DungeonManager.Instance.CheckLock(num);
					bool flag3 = (bool)hashtable3.get_Item("ISLock");
					if (zhuXianPeiZhi.minLv > EntityWorld.Instance.EntSelf.Lv)
					{
						instantiate2Prefab.get_transform().FindChild("LevelOpen").FindChild("ImageNum").GetComponent<ImageNums>().SetImageNum(zhuXianPeiZhi.minLv);
						gameObject3.SetActive(true);
						gameObject.SetActive(false);
						gameObject2.SetActive(false);
						gameObject4.get_gameObject().SetActive(false);
					}
					else if (flag3)
					{
						gameObject3.SetActive(false);
						gameObject.SetActive(false);
						gameObject2.SetActive(true);
						gameObject4.get_gameObject().SetActive(false);
					}
					else
					{
						gameObject3.SetActive(false);
						gameObject.SetActive(true);
						gameObject2.SetActive(false);
						gameObject4.get_gameObject().SetActive(false);
					}
					goto IL_55C;
				}
				Debug.LogError(string.Concat(new string[]
				{
					"获取途径副本失败：Id =" + num
				}));
			}
			else
			{
				if (dLuJingShuXing2.type == 15)
				{
					if (flag2)
					{
						gameObject3.SetActive(false);
						gameObject.SetActive(false);
						gameObject2.SetActive(false);
						gameObject4.get_gameObject().SetActive(true);
					}
					else
					{
						instantiate2Prefab.get_transform().FindChild("LevelOpen").FindChild("ImageNum").GetComponent<ImageNums>().SetImageNum(imageNum);
						gameObject3.SetActive(true);
						gameObject.SetActive(false);
						gameObject2.SetActive(false);
						gameObject4.get_gameObject().SetActive(false);
					}
					goto IL_55C;
				}
				if (flag2)
				{
					gameObject3.SetActive(false);
					gameObject.SetActive(true);
					gameObject2.SetActive(false);
					gameObject4.get_gameObject().SetActive(false);
					goto IL_55C;
				}
				instantiate2Prefab.get_transform().FindChild("LevelOpen").FindChild("ImageNum").GetComponent<ImageNums>().SetImageNum(imageNum);
				gameObject3.SetActive(true);
				gameObject.SetActive(false);
				gameObject2.SetActive(false);
				gameObject4.get_gameObject().SetActive(false);
				goto IL_55C;
			}
			IL_60D:
			k++;
			continue;
			IL_55C:
			instantiate2Prefab.get_transform().SetParent(component.get_transform(), false);
			instantiate2Prefab.get_transform().FindChild("TextTitle").GetComponent<Text>().set_text(text);
			ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("Icon").FindChild("ImageIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(dLuJingShuXing2.icon));
			instantiate2Prefab.get_transform().FindChild("TextContent").GetComponent<Text>().set_text(text2);
			instantiate2Prefab.set_name(key2.ToString());
			instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFromItem);
			this.ListFromItem.Add(instantiate2Prefab);
			goto IL_60D;
		}
	}

	private void OnClickFromItem(GameObject sender)
	{
		SourceReferenceUI.GoTo(int.Parse(sender.get_name()));
		this.Show(false);
	}

	public static void GoTo(int pathId)
	{
		DLuJingShuXing dLuJingShuXing = DataReader<DLuJingShuXing>.Get(pathId);
		if (dLuJingShuXing.type == 0 && !UIManagerControl.Instance.IsOpen("InstanceDetailUI"))
		{
			int instanceID = (int)float.Parse(dLuJingShuXing.invokeParam);
			Hashtable hashtable = DungeonManager.Instance.CheckLock(instanceID);
			bool flag = (bool)hashtable.get_Item("ISLock");
			string text = (string)hashtable.get_Item("LockReason");
			if (flag)
			{
				UIManagerControl.Instance.ShowToastText(text);
				return;
			}
			InstanceManagerUI.OpenInstanceUI(instanceID, false, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 1 && !UIManagerControl.Instance.IsOpen("SurvivalChallengeUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenSurvivalChallengeUI();
		}
		else if (dLuJingShuXing.type == 2 && !UIManagerControl.Instance.IsOpen("PVPUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenPVPUI();
		}
		else if (dLuJingShuXing.type == 3 && !UIManagerControl.Instance.IsOpen("GangFightUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			UIManagerControl.Instance.OpenUI("GangFightUI", null, false, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 4 && !UIManagerControl.Instance.IsOpen("ElementInstanceUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			UIManagerControl.Instance.OpenUI("ElementInstanceUI", null, false, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 5 && !UIManagerControl.Instance.IsOpen("SpecialInstanceUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			InstanceManagerUI.OpenSpecialInstanceUI();
		}
		else if (dLuJingShuXing.type == 6 && !UIManagerControl.Instance.IsOpen("ShoppingUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			MarketManager.Instance.OpenShop(3);
		}
		else if (dLuJingShuXing.type == 7 && !UIManagerControl.Instance.IsOpen("LuckDrawUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			UIManagerControl.Instance.OpenUI("LuckDrawUI", null, true, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 8 && !UIManagerControl.Instance.IsOpen("EveryDayUI"))
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			UIManagerControl.Instance.OpenUI("DailyTaskUI", null, true, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 9)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenPetUI(null);
		}
		else if (dLuJingShuXing.type == 10)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenEquipGemUI(EquipLibType.ELT.Weapon, null);
		}
		else if (dLuJingShuXing.type == 11)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenActorUI(null);
		}
		else if (dLuJingShuXing.type == 12)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
		}
		else if (dLuJingShuXing.type == 13)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			UIManagerControl.Instance.OpenUI("RiseUI", null, true, UIType.FullScreen);
		}
		else if (dLuJingShuXing.type == 14)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenVIPUI2Recharge();
		}
		else if (dLuJingShuXing.type == 15)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			CurrenciesUIViewModel.Instance.OnClickGold();
		}
		else if (dLuJingShuXing.type == 16)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
		}
		else if (dLuJingShuXing.type == 17)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			InstanceManagerUI.OpenEliteDungeonUI();
		}
		else if (dLuJingShuXing.type == 18)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
		}
		else if (dLuJingShuXing.type == 19)
		{
			if (!SystemOpenManager.IsSystemClickOpen(37, 0, true))
			{
				return;
			}
			LinkNavigationManager.OpenEquipStarUpUI(EquipLibType.ELT.Weapon, null);
		}
		else if (dLuJingShuXing.type == 20)
		{
			LinkNavigationManager.OpenEquipStrengthenUI(EquipLibType.ELT.Weapon, null);
		}
		else if (dLuJingShuXing.type == 21)
		{
			if (!SystemOpenManager.IsSystemClickOpen(dLuJingShuXing.systemOpenID, 502255, true))
			{
				return;
			}
			LinkNavigationManager.OpenEquipGemUI(EquipLibType.ELT.Weapon, null);
		}
		else if (dLuJingShuXing.type == 22)
		{
			LinkNavigationManager.OpenSkillUI(null);
		}
		else if (dLuJingShuXing.type == 23)
		{
			LinkNavigationManager.OpenPetLevelUI();
		}
		else if (dLuJingShuXing.type == 24)
		{
			LinkNavigationManager.OpenPetStarUI();
		}
		else if (dLuJingShuXing.type == 25)
		{
			LinkNavigationManager.OpenPetSkillUI();
		}
		else if (dLuJingShuXing.type == 28)
		{
			if (!SystemOpenManager.IsSystemClickOpen(27, 0, true))
			{
				return;
			}
			InstanceManagerUI.OpenBountyUI();
		}
		else if (dLuJingShuXing.type == 29)
		{
			if (!SystemOpenManager.IsSystemClickOpen(41, 0, true))
			{
				return;
			}
		}
		else
		{
			Debug.LogError("错误的索引ID  " + pathId);
		}
		if (SourceReferenceUI.m_actionClickGoToItem != null)
		{
			SourceReferenceUI.m_actionClickGoToItem.Invoke();
			SourceReferenceUI.m_actionClickGoToItem = null;
		}
	}
}
