using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstancePassUI : UIBase
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		base.FindTransform("BtnExit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExit);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast("GuideManager.InstanceWin");
		Utils.WinSetting(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Utils.WinSetting(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
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

	private void OnClickExit(GameObject go)
	{
		UIManagerControl.Instance.UnLoadUIPrefab("SpecialInstancePassUI");
		DefendFightManager.Instance.ExitDefendFightReq(false);
	}

	public void Init(DefendFightBtlResultNty result)
	{
		if (result != null)
		{
			List<ItemBriefInfo> normalDrops = new List<ItemBriefInfo>();
			base.FindTransform("Gold").get_gameObject().SetActive(false);
			base.FindTransform("Exp").get_gameObject().SetActive(false);
			for (int i = 0; i < result.normalDropItems.get_Count(); i++)
			{
				int cfgId = result.normalDropItems.get_Item(i).cfgId;
				long count = result.normalDropItems.get_Item(i).count;
				Items item = BackpackManager.Instance.GetItem(cfgId);
				if (item != null)
				{
					if (item.secondType == 15)
					{
						base.FindTransform("Gold").get_gameObject().SetActive(true);
						base.FindTransform("GoldNum").GetComponent<Text>().set_text(count.ToString());
					}
					else if (item.secondType == 16)
					{
						base.FindTransform("Exp").get_gameObject().SetActive(true);
						base.FindTransform("ExpNum").GetComponent<Text>().set_text(count.ToString());
					}
					else
					{
						normalDrops.Add(result.normalDropItems.get_Item(i));
					}
				}
			}
			ListPool pool1 = base.FindTransform("Items").GetComponent<ListPool>();
			pool1.Create(normalDrops.get_Count(), delegate(int index)
			{
				if (index < normalDrops.get_Count() && index < normalDrops.get_Count())
				{
					int cfgId2 = normalDrops.get_Item(index).cfgId;
					long count2 = normalDrops.get_Item(index).count;
					Debug.LogError(string.Concat(new object[]
					{
						"普通掉落:",
						cfgId2,
						"   ",
						count2
					}));
					pool1.Items.get_Item(index).GetComponent<SpecialInstancePassItem>().SetData(cfgId2, count2);
				}
			});
			ListPool pool2 = base.FindTransform("Item2s").GetComponent<ListPool>();
			base.FindTransform("extral").get_gameObject().SetActive(result.extendDropItems.get_Count() > 0);
			pool2.Create(result.extendDropItems.get_Count(), delegate(int index)
			{
				if (index < result.extendDropItems.get_Count() && index < pool2.Items.get_Count())
				{
					int cfgId2 = result.extendDropItems.get_Item(index).cfgId;
					long count2 = result.extendDropItems.get_Item(index).count;
					Debug.LogError(string.Concat(new object[]
					{
						"额外掉落:",
						cfgId2,
						"   ",
						count2
					}));
					pool2.Items.get_Item(index).GetComponent<SpecialInstancePassItem>().SetData(cfgId2, count2);
				}
			});
		}
	}
}
