using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemComposeUI : UIBase
{
	private ButtonCustom[] btnGrids = new ButtonCustom[2];

	private int rootTypeId;

	private int branchTypeId;

	private GridLayoutGroup gridLayoutGroup;

	private ButtonCustom btnCompose;

	private ButtonCustom btnOneKeyCompose;

	private List<CostGem> branchGems;

	protected override void Preprocessing()
	{
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
		this.Init();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void OnClickMaskAction()
	{
	}

	private void OnClickBtnCompose(GameObject go)
	{
		if (!GemGlobal.IsGemEnoughLv(this.rootTypeId))
		{
			string text = string.Format(GameDataUtils.GetChineseContent(621005, false), new object[0]);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		if (!this.IsEnoughGemToCompose(this.rootTypeId))
		{
			int gemLv = GemGlobal.GetGemLv(this.branchTypeId);
			string gemColor = GemGlobal.GetGemColor(this.branchTypeId);
			string text2 = string.Format(GameDataUtils.GetChineseContent(621008, false), gemLv, gemColor);
			UIManagerControl.Instance.ShowToastText(text2, 2f, 2f);
			return;
		}
		int arg_B7_0 = (!this.IsComposeGemtoSlot(this.rootTypeId)) ? 0 : GemUI.instance.slotCurr;
		GemManager.Instance.SendGemSysCompositeReq(this.rootTypeId, 1);
		if (this.IsComposeGemtoSlot(this.rootTypeId))
		{
			GemSingleUI gemSingleUI = UIManagerControl.Instance.GetUIIfExist("GemSingleUI") as GemSingleUI;
			if (gemSingleUI != null)
			{
				gemSingleUI.Show(false);
			}
			this.Show(false);
		}
	}

	private void OnClickBtnGrid(GameObject go)
	{
		string text = go.get_name().Substring(7);
		int afterId;
		if (int.Parse(text) == 0)
		{
			if (GemUI.instance.typeIdNext == this.rootTypeId)
			{
				return;
			}
			afterId = GemGlobal.GetAfterId(this.rootTypeId);
		}
		else
		{
			afterId = this.branchTypeId;
			if (GemGlobal.GetNeedId(afterId) == 0)
			{
				UIManagerControl.Instance.OpenSourceReferenceUI(afterId, new Action(this.CloseUI));
				return;
			}
		}
		this.SetItemIcons(afterId);
	}

	private void OnClickBtnOneKeyCompose(GameObject go)
	{
		if (!GemGlobal.IsGemEnoughLv(this.rootTypeId))
		{
			string text = string.Format(GameDataUtils.GetChineseContent(621005, false), new object[0]);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		List<MaterialGem> oneKeyComposeGems = GemGlobal.GetOneKeyComposeGems(GemUI.instance.equipCurr, GemUI.instance.slotCurr, this.rootTypeId);
		if (oneKeyComposeGems == null)
		{
			string text2 = string.Format(GameDataUtils.GetChineseContent(621010, false), new object[0]);
			UIManagerControl.Instance.ShowToastText(text2, 2f, 2f);
			return;
		}
		List<CostGem> list = new List<CostGem>();
		using (List<MaterialGem>.Enumerator enumerator = oneKeyComposeGems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MaterialGem current = enumerator.get_Current();
				list.Add(new CostGem
				{
					gemId = current.gemId,
					gemNum = (uint)current.count
				});
			}
		}
		GemManager.Instance.SendGemSysCompositeReq(this.rootTypeId, 2);
		if (this.IsComposeGemtoSlot(this.rootTypeId))
		{
			this.CloseUI();
		}
	}

	public void OnClickTopGridItem(GameObject go)
	{
		for (int i = 0; i < this.gridLayoutGroup.get_transform().get_childCount(); i++)
		{
			Transform child = this.gridLayoutGroup.get_transform().GetChild(i);
			child.FindChild("ImageSelectBG").get_gameObject().SetActive(false);
		}
		go.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(true);
		this.SetItemIcons(int.Parse(go.get_name()));
	}

	private void SetScroll(int rootTypeId, int branchTypeId)
	{
		for (int i = 0; i < this.gridLayoutGroup.get_transform().get_childCount(); i++)
		{
			Transform child = this.gridLayoutGroup.get_transform().GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
		List<int> genealogy = this.GetGenealogy(rootTypeId, branchTypeId);
		using (List<int>.Enumerator enumerator = genealogy.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GemNavCell");
				instantiate2Prefab.get_transform().SetParent(this.gridLayoutGroup.get_transform(), false);
				instantiate2Prefab.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(false);
				instantiate2Prefab.set_name(current.ToString());
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTopGridItem);
				Image component = instantiate2Prefab.get_transform().FindChild("ImageFrame").GetComponent<Image>();
				Image component2 = instantiate2Prefab.get_transform().FindChild("ImageIcon").GetComponent<Image>();
				ResourceManager.SetSprite(component, GameDataUtils.GetItemFrame(current));
				ResourceManager.SetSprite(component2, GemGlobal.GetIconSprite(current));
				if (current == genealogy.get_Item(genealogy.get_Count() - 1))
				{
					instantiate2Prefab.get_transform().FindChild("ImageArrow").get_gameObject().SetActive(false);
				}
				if (current == branchTypeId)
				{
					instantiate2Prefab.get_transform().FindChild("ImageSelectBG").get_gameObject().SetActive(true);
				}
			}
		}
	}

	private void SetCostActive(string buttonName, bool isActive, int cost)
	{
		Transform transform = base.FindTransform(buttonName);
		Transform transform2 = transform.FindChild("texCostVal");
		Transform transform3 = transform.FindChild("imgCost");
		bool active = isActive && cost != 0;
		transform2.get_gameObject().SetActive(active);
		transform3.get_gameObject().SetActive(active);
	}

	private void InitButtons()
	{
		this.btnCompose = base.FindTransform("btnCompose").GetComponent<ButtonCustom>();
		this.btnOneKeyCompose = base.FindTransform("btnOneKeyCompose").GetComponent<ButtonCustom>();
		this.btnCompose.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnCompose);
		for (int i = 0; i < this.btnGrids.Length; i++)
		{
			this.btnGrids[i] = base.FindTransform("imgGrid" + i).GetComponent<ButtonCustom>();
			this.btnGrids[i].onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGrid);
		}
	}

	private void Init()
	{
		this.gridLayoutGroup = base.FindTransform("gridLayoutGroup").GetComponent<GridLayoutGroup>();
		this.InitButtons();
		this.SetItemIcons(GemUI.instance.typeIdNext);
	}

	private void InitTexAttrs(int buttonIndex, int typeId)
	{
		List<string> strAttrs = GemGlobal.GetStrAttrs(typeId);
		for (int i = 0; i < 2; i++)
		{
			Text component = this.btnGrids[buttonIndex].get_transform().FindChild("texAttr" + i).GetComponent<Text>();
			if (i < strAttrs.get_Count())
			{
				component.set_text(strAttrs.get_Item(i));
			}
			else
			{
				component.set_text(string.Empty);
			}
		}
	}

	private void SetOneItemIcon(int buttonIndex, int itemId)
	{
		Image component = this.btnGrids[buttonIndex].get_transform().FindChild("imgFrame").GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemFrame(itemId));
		Image component2 = this.btnGrids[buttonIndex].get_transform().FindChild("imgFrame").FindChild("imgItem").GetComponent<Image>();
		ResourceManager.SetSprite(component2, GemGlobal.GetIconSprite(itemId));
		Text component3 = this.btnGrids[buttonIndex].get_transform().FindChild("texName").GetComponent<Text>();
		component3.set_text(GemGlobal.GetName(itemId));
		Text component4 = this.btnGrids[buttonIndex].get_transform().FindChild("texLv").GetComponent<Text>();
		if (GemGlobal.IsGemEnoughLv(itemId))
		{
			component4.set_text(string.Empty);
		}
		else
		{
			component4.set_text("等级需求: " + GemGlobal.GetRoleLvRequire(itemId));
		}
		this.InitTexAttrs(buttonIndex, itemId);
	}

	private void SetItemIcons(int itemId)
	{
		this.rootTypeId = itemId;
		this.branchTypeId = GemGlobal.GetNeedId(itemId);
		bool enabled = GemUI.instance.typeIdNext != this.rootTypeId;
		this.btnGrids[0].get_transform().GetComponent<Animator>().set_enabled(enabled);
		this.btnGrids[0].get_transform().set_localScale(Vector3.get_one());
		GemSingleUI gemSingleUI = UIManagerControl.Instance.GetUIIfExist("GemSingleUI") as GemSingleUI;
		if (gemSingleUI != null)
		{
			gemSingleUI.SetOneGem(itemId);
		}
		if (GemGlobal.IsActiveOneKeyCompose(GemUI.instance.equipCurr, GemUI.instance.slotCurr, this.rootTypeId))
		{
			this.btnCompose.get_transform().set_localPosition(this.btnCompose.get_transform().get_localPosition().AssignX(-92.2f));
			this.btnOneKeyCompose.set_enabled(true);
			this.btnOneKeyCompose.get_gameObject().SetActive(true);
			this.btnOneKeyCompose.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnOneKeyCompose);
			List<MaterialGem> oneKeyComposeGems = GemGlobal.GetOneKeyComposeGems(GemUI.instance.equipCurr, GemUI.instance.slotCurr, this.rootTypeId);
			if (oneKeyComposeGems != null)
			{
				Text component = base.FindTransform("btnOneKeyCompose").FindChild("texCostVal").GetComponent<Text>();
				int oneKeyComposeCost = GemGlobal.GetOneKeyComposeCost(oneKeyComposeGems, this.rootTypeId);
				component.set_text("x" + oneKeyComposeCost);
				this.SetCostActive("btnOneKeyCompose", true, oneKeyComposeCost);
			}
			else
			{
				this.SetCostActive("btnOneKeyCompose", false, 0);
			}
		}
		else
		{
			this.btnCompose.get_transform().set_localPosition(this.btnCompose.get_transform().get_localPosition().AssignXZero());
			this.btnOneKeyCompose.set_enabled(false);
			this.btnOneKeyCompose.get_gameObject().SetActive(false);
		}
		this.SetScroll(GemUI.instance.typeIdNext, itemId);
		this.SetOneItemIcon(0, this.rootTypeId);
		this.SetOneItemIcon(1, this.branchTypeId);
		Text component2 = base.FindTransform("btnCompose").FindChild("texCostVal").GetComponent<Text>();
		int amount = GemGlobal.GetAmount(itemId);
		component2.set_text("x" + amount);
		this.SetCostActive("btnCompose", true, amount);
		int num = 0;
		List<long> materailGemIds = this.GetMaterailGemIds(itemId, ref num);
		Transform transform = this.btnGrids[1].get_transform().Find("texPercent");
		int composeAmount = GemGlobal.GetComposeAmount(itemId);
		string text = (materailGemIds.get_Count() < composeAmount) ? "<color=#ff0000>" : "<color=#00ff00>";
		transform.GetComponent<Text>().set_text(string.Concat(new object[]
		{
			text,
			num,
			"</color>/",
			composeAmount
		}));
	}

	private bool IsComposeGemtoSlot(int itemId)
	{
		return itemId == GemUI.instance.typeIdNext;
	}

	private bool IsEnoughGemToCompose(int itemId)
	{
		int num = 0;
		List<long> materailGemIds = this.GetMaterailGemIds(itemId, ref num);
		return materailGemIds.get_Count() >= GemGlobal.GetComposeAmount(itemId);
	}

	private void SetBranchActive(int i, bool value)
	{
		Image component = base.FindTransform("imgGrid" + (1 + i)).GetComponent<Image>();
		Image component2 = base.FindTransform("imgLine1" + (1 + i)).GetComponent<Image>();
		Image component3 = base.FindTransform("imgLine2" + (1 + i)).GetComponent<Image>();
		Image component4 = base.FindTransform("imgLine3" + (1 + i)).GetComponent<Image>();
		component.get_gameObject().SetActive(value);
		component2.get_gameObject().SetActive(value);
		component3.get_gameObject().SetActive(value);
		component4.get_gameObject().SetActive(value);
	}

	private void CloseUI()
	{
		GemSingleUI gemSingleUI = UIManagerControl.Instance.GetUIIfExist("GemSingleUI") as GemSingleUI;
		if (gemSingleUI != null)
		{
			gemSingleUI.Show(false);
		}
		this.Show(false);
	}

	private List<long> GetMaterailGemIds(int itemId, ref int ownGemAmount)
	{
		List<long> list = new List<long>();
		this.branchGems = new List<CostGem>();
		int composeAmount = GemGlobal.GetComposeAmount(itemId);
		if (composeAmount <= 0)
		{
			return list;
		}
		if (itemId == GemUI.instance.typeIdNext)
		{
			long id = GemUI.instance.GetCurrSlot().id;
			list.Add(id);
			this.branchGems.Add(new CostGem
			{
				gemId = id,
				gemNum = 1u
			});
			ownGemAmount++;
		}
		int needId = GemGlobal.GetNeedId(itemId);
		List<Goods> list2 = BackpackManager.Instance.OnGetGood(needId);
		int num = 0;
		using (List<Goods>.Enumerator enumerator = list2.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Goods current = enumerator.get_Current();
				num += current.GetCount();
			}
		}
		ownGemAmount += num;
		int num2 = Mathf.Min(num, composeAmount - list.get_Count());
		if (num2 == 0)
		{
			return list;
		}
		using (List<Goods>.Enumerator enumerator2 = list2.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				Goods current2 = enumerator2.get_Current();
				if (num2 == 0)
				{
					break;
				}
				long longId = current2.GetLongId();
				int count = current2.GetCount();
				int num3 = Mathf.Min(num2, count);
				num2 -= num3;
				for (int i = 0; i < num3; i++)
				{
					list.Add(longId);
				}
				this.branchGems.Add(new CostGem
				{
					gemId = longId,
					gemNum = (uint)num3
				});
			}
		}
		return list;
	}

	private List<int> GetGenealogy(int rootTypeId, int branchTypeId)
	{
		List<int> list = new List<int>();
		list.Add(rootTypeId);
		int needId = GemGlobal.GetNeedId(rootTypeId);
		while (needId != 0 && needId >= branchTypeId)
		{
			list.Add(needId);
			needId = GemGlobal.GetNeedId(needId);
		}
		return list;
	}

	private void Refresh(int slotIndex, int typeId)
	{
		int afterId = GemGlobal.GetAfterId(typeId);
		if (this.IsEnoughGemToCompose(afterId))
		{
			this.SetItemIcons(afterId);
		}
		else
		{
			this.SetItemIcons(typeId);
		}
	}
}
