using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemUI : UIBase
{
	private const int kEquipSlot = 4;

	public static GemUI instance;

	private Transform[] btnEquipSlots = new Transform[4];

	public EquipLibType.ELT equipCurr = EquipLibType.ELT.Weapon;

	public int slotCurr;

	public int typeIdCurr;

	public int typeIdNext;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		GemUI.instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		EquipDetailedUI equipDetailedUI = UIManagerControl.Instance.GetUIIfExist("EquipDetailedUI") as EquipDetailedUI;
		if (equipDetailedUI != null)
		{
			this.equipCurr = equipDetailedUI.currentSelectPos;
		}
		this.Init();
		this.CheckBadge();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			GemUI.instance = null;
			base.ReleaseSelf(true);
		}
	}

	public void CheckBadge()
	{
		for (int i = 0; i < 4; i++)
		{
			Transform transform = this.btnEquipSlots[i].get_transform();
			GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[this.equipCurr - EquipLibType.ELT.Weapon, i];
			Image component = transform.FindChild("notice").GetComponent<Image>();
			component.get_gameObject().SetActive(false);
			if (gemEmbedInfo != null)
			{
				bool flag = GemGlobal.IsCanWearGem((int)this.equipCurr, i, gemEmbedInfo.typeId);
				if (flag)
				{
					component.get_gameObject().SetActive(true);
				}
			}
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

	public void Refresh(EquipLibType.ELT equip)
	{
		this.equipCurr = equip;
		this.Init();
		this.CheckBadge();
	}

	private void SetButtonEvent()
	{
		for (int i = 0; i < 4; i++)
		{
			this.btnEquipSlots[i] = base.FindTransform("slot" + (i + 1));
			this.btnEquipSlots[i].GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnSlot);
		}
	}

	private void SetEquipSlot()
	{
		for (int i = 0; i < 4; i++)
		{
			GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[this.equipCurr - EquipLibType.ELT.Weapon, i];
			if (gemEmbedInfo != null)
			{
				this.SetOneEquipSlot(i, gemEmbedInfo.typeId);
			}
			else
			{
				this.SetOneEquipSlot(i, -1);
			}
			this.PlaySpineUnlockFx(i);
		}
	}

	public void Init()
	{
		this.SetButtonEvent();
		this.SetEquipSlot();
	}

	private void PlaySpineUnlockFx(int i)
	{
		if (GemManager.Instance.newOpeningSlots[this.equipCurr - EquipLibType.ELT.Weapon, i])
		{
			GemManager.Instance.newOpeningSlots[this.equipCurr - EquipLibType.ELT.Weapon, i] = false;
			Transform redPoint = this.btnEquipSlots[i].FindChild("notice");
			bool isShowRedPoint = redPoint.get_gameObject().get_activeSelf();
			redPoint.get_gameObject().SetActive(false);
			FXSpineManager.Instance.PlaySpine(1141, this.btnEquipSlots[i], "GemUI", 2001, delegate
			{
				redPoint.get_gameObject().SetActive(isShowRedPoint);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public void RefreshEquipSlot(int slotIndex, int typeId)
	{
		FXSpineManager.Instance.PlaySpine(1142, this.btnEquipSlots[slotIndex], "GemUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.SetOneEquipSlot(slotIndex, typeId);
		this.CheckBadge();
	}

	private void SetOneEquipSlot(int slotIndex, int typeId)
	{
		Transform transform = this.btnEquipSlots[slotIndex];
		Image component = transform.FindChild("ItemBg").GetComponent<Image>();
		Image component2 = transform.FindChild("item").GetComponent<Image>();
		Transform transform2 = transform.FindChild("name");
		Text component3 = transform2.FindChild("texLv").GetComponent<Text>();
		Image component4 = transform.FindChild("notice").GetComponent<Image>();
		transform2.get_gameObject().SetActive(false);
		component4.get_gameObject().SetActive(false);
		ResourceManager.SetSprite(component, GemGlobal.GetGemItemFrameSprite(this.equipCurr, slotIndex + 1));
		if (typeId == -1)
		{
			ResourceManager.SetSprite(component2, ResourceManager.GetIconSprite("jewel_box_suo"));
			component2.SetNativeSize();
			transform2.get_gameObject().SetActive(true);
			int slotUnlockRequireBatchLv = GemGlobal.GetSlotUnlockRequireBatchLv(this.equipCurr, slotIndex + 1);
			component3.set_text(string.Format(GameDataUtils.GetChineseContent(621001, false), slotUnlockRequireBatchLv));
		}
		else if (typeId == 0)
		{
			ResourceManager.SetSprite(component2, ResourceManagerBase.GetNullSprite());
		}
		else
		{
			ResourceManager.SetSprite(component2, GemGlobal.GetIconSprite(typeId));
			component2.SetNativeSize();
			transform2.get_gameObject().SetActive(true);
			component3.set_text(GemGlobal.GetName(typeId));
			if (!GemGlobal.IsGemMaxLv(typeId))
			{
				if (GemGlobal.IsCanGemLvUp(slotIndex + 1, typeId))
				{
					component4.get_gameObject().SetActive(true);
				}
			}
		}
	}

	private List<MaterialGem> GetLowerGems(int currSlot, int typeId)
	{
		List<MaterialGem> list = new List<MaterialGem>();
		for (int needId = GemGlobal.GetNeedId(typeId); needId != 0; needId = GemGlobal.GetNeedId(needId))
		{
			List<Goods> list2 = BackpackManager.Instance.OnGetGood(needId);
			using (List<Goods>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Goods current = enumerator.get_Current();
					list.Add(new MaterialGem
					{
						typeId = needId,
						gemId = current.GetLongId(),
						count = current.GetCount()
					});
				}
			}
		}
		list.Reverse();
		GemEmbedInfo equipSlot = this.GetEquipSlot(currSlot);
		if (equipSlot.typeId != 0)
		{
			list.Add(new MaterialGem
			{
				typeId = equipSlot.typeId,
				gemId = equipSlot.id,
				count = 1
			});
		}
		return list;
	}

	private List<CostGem> GetComposeMaterialGems(int currSlot, int typeId)
	{
		List<MaterialGem> lowerGems = this.GetLowerGems(currSlot, typeId);
		if (lowerGems.get_Count() == 0)
		{
			return null;
		}
		int value = GemGlobal.GetValue(typeId);
		int[] array = new int[lowerGems.get_Count()];
		bool flag = false;
		while (array[lowerGems.get_Count() - 1] <= lowerGems.get_Item(lowerGems.get_Count() - 1).count)
		{
			for (int i = 0; i < lowerGems.get_Count() - 1; i++)
			{
				if (array[i] > lowerGems.get_Item(i).count)
				{
					array[i] = 0;
					array[i + 1]++;
				}
			}
			int num = 0;
			for (int j = 0; j < lowerGems.get_Count(); j++)
			{
				num += array[j] * GemGlobal.GetValue(lowerGems.get_Item(j).typeId);
			}
			if (num == value)
			{
				flag = true;
				break;
			}
			array[0]++;
		}
		if (!flag)
		{
			return null;
		}
		List<CostGem> list = new List<CostGem>();
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k] > 0)
			{
				list.Add(new CostGem
				{
					gemId = lowerGems.get_Item(k).gemId,
					gemNum = (uint)array[k]
				});
			}
		}
		return list;
	}

	public GemEmbedInfo GetEquipSlot(int slotNum)
	{
		return GemGlobal.GetGemInfo(this.equipCurr, slotNum);
	}

	public GemEmbedInfo GetCurrSlot()
	{
		return this.GetEquipSlot(this.slotCurr);
	}

	private bool IsSlotUnlocked(int slotNum)
	{
		return this.GetEquipSlot(slotNum) != null;
	}

	private int GetCurrTypeId(int slotNum)
	{
		GemEmbedInfo equipSlot = this.GetEquipSlot(slotNum);
		return (equipSlot == null) ? -1 : equipSlot.typeId;
	}

	private void SlotLock()
	{
		string text = string.Format(GameDataUtils.GetChineseContent(621001, false), GemGlobal.GetSlotUnlockRequireBatchLv(this.equipCurr, this.slotCurr));
		UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
	}

	private void OnClickBtnSlot(GameObject sender)
	{
		string text = sender.get_name().Substring(4);
		this.slotCurr = int.Parse(text);
		if (!this.IsSlotUnlocked(this.slotCurr))
		{
			this.SlotLock();
			return;
		}
		this.typeIdCurr = this.GetCurrTypeId(this.slotCurr);
		this.typeIdNext = GemGlobal.GetNextGemItemId(this.typeIdCurr);
		if (this.typeIdCurr == 0)
		{
			GemSelectUI gemSelectUI = UIManagerControl.Instance.OpenUI("GemSelectUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GemSelectUI;
			gemSelectUI.Init((int)this.equipCurr, this.slotCurr);
		}
		else
		{
			GemSingleUI gemSingleUI = UIManagerControl.Instance.OpenUI("GemSingleUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GemSingleUI;
		}
	}
}
