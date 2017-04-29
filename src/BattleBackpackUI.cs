using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBackpackUI : UIBase
{
	protected Text BattleBackpackUITitleName;

	protected ButtonCustom BattleBackpackUICloseBtn;

	protected ListPool listPool;

	protected Transform BattleBackpackUIBGOffset;

	protected Vector3 BattleBackpackUIBGSlot0;

	protected Vector3 BattleBackpackUIBGSlot1;

	protected Transform BattleBackpackUISROffset;

	protected Vector3 BattleBackpackUISRSlot0;

	protected Vector3 BattleBackpackUISRSlot1;

	protected Text BattleBackpackUISRTip;

	protected Text BattleBackpackUITip;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BattleBackpackUITitleName = base.FindTransform("BattleBackpackUITitleName").GetComponent<Text>();
		this.BattleBackpackUICloseBtn = base.FindTransform("BattleBackpackUICloseBtn").GetComponent<ButtonCustom>();
		this.listPool = base.FindTransform("BattleBackpackUIPool").GetComponent<ListPool>();
		this.listPool.SetItem("BattleBackpackItemUnit");
		this.BattleBackpackUIBGOffset = base.FindTransform("BattleBackpackUIBGOffset");
		this.BattleBackpackUIBGSlot0 = base.FindTransform("BattleBackpackUIBGSlot0").get_localPosition();
		this.BattleBackpackUIBGSlot1 = base.FindTransform("BattleBackpackUIBGSlot1").get_localPosition();
		this.BattleBackpackUISROffset = base.FindTransform("BattleBackpackUISROffset");
		this.BattleBackpackUISRSlot0 = base.FindTransform("BattleBackpackUISRSlot0").get_localPosition();
		this.BattleBackpackUISRSlot1 = base.FindTransform("BattleBackpackUISRSlot1").get_localPosition();
		this.BattleBackpackUISRTip = base.FindTransform("BattleBackpackUISRTip").GetComponent<Text>();
		this.BattleBackpackUITip = base.FindTransform("BattleBackpackUITip").GetComponent<Text>();
		ButtonCustom expr_106 = this.BattleBackpackUICloseBtn;
		expr_106.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_106.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnBtnCloseClick));
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetTitleName(string text)
	{
		this.BattleBackpackUITitleName.set_text(text);
	}

	public void SetItem(List<KeyValuePair<int, long>> allItems, XDict<int, int> lockInfo, string nullText = "")
	{
		int num = (EntityWorld.Instance.EntSelf != null) ? EntityWorld.Instance.EntSelf.VipLv : 2147483647;
		int unlockCount = 0;
		for (int i = 0; i < lockInfo.Count; i++)
		{
			if (num >= lockInfo.Keys.get_Item(i))
			{
				unlockCount = lockInfo.Values.get_Item(i);
			}
		}
		int allGridCount = lockInfo.Values.get_Item(lockInfo.Count - 1);
		if (allGridCount == 0)
		{
			if (!this.BattleBackpackUISRTip.get_gameObject().get_activeSelf())
			{
				this.BattleBackpackUISRTip.get_gameObject().SetActive(true);
			}
			this.BattleBackpackUISRTip.set_text(nullText);
		}
		else
		{
			if (this.BattleBackpackUISRTip.get_gameObject().get_activeSelf())
			{
				this.BattleBackpackUISRTip.get_gameObject().SetActive(false);
			}
			this.listPool.Create(allGridCount, delegate(int index)
			{
				if (index < this.listPool.Items.get_Count() && index < allGridCount)
				{
					BattleBackpackItemUnit component = this.listPool.Items.get_Item(index).GetComponent<BattleBackpackItemUnit>();
					if (index < unlockCount)
					{
						if (index < allItems.get_Count())
						{
							component.SetData(index, true, allItems.get_Item(index).get_Key(), allItems.get_Item(index).get_Value(), string.Empty);
						}
						else
						{
							component.SetData(index, true, 0, 0L, string.Empty);
						}
					}
					else
					{
						for (int j = lockInfo.Count - 1; j >= 0; j--)
						{
							if (index >= lockInfo.Values.get_Item(j))
							{
								if (j == lockInfo.Count - 1)
								{
									Debug.LogError("Logic Error!!!");
								}
								else
								{
									component.SetData(index, false, 0, 0L, string.Format(GameDataUtils.GetChineseContent(511613, false), lockInfo.Keys.get_Item(j + 1)));
								}
								break;
							}
						}
					}
				}
			});
		}
	}

	public void SetItem(List<KeyValuePair<int, long>> allItems, string nullText = "")
	{
		if (allItems == null || allItems.get_Count() == 0)
		{
			if (!this.BattleBackpackUISRTip.get_gameObject().get_activeSelf())
			{
				this.BattleBackpackUISRTip.get_gameObject().SetActive(true);
			}
			this.BattleBackpackUISRTip.set_text(nullText);
		}
		else
		{
			if (this.BattleBackpackUISRTip.get_gameObject().get_activeSelf())
			{
				this.BattleBackpackUISRTip.get_gameObject().SetActive(false);
			}
			int num = (allItems.get_Count() >= 20) ? ((allItems.get_Count() % 5 != 0) ? ((allItems.get_Count() / 5 + 1) * 5) : allItems.get_Count()) : 20;
			this.listPool.Create(num, delegate(int index)
			{
				if (index < this.listPool.Items.get_Count())
				{
					BattleBackpackItemUnit component = this.listPool.Items.get_Item(index).GetComponent<BattleBackpackItemUnit>();
					if (index < allItems.get_Count())
					{
						component.SetData(index, true, allItems.get_Item(index).get_Key(), allItems.get_Item(index).get_Value(), string.Empty);
					}
					else
					{
						component.SetData(index, true, 0, 0L, string.Empty);
					}
				}
			});
		}
	}

	public void ShowTips(bool isShow, string text = "")
	{
		if (this.BattleBackpackUITip.get_gameObject().get_activeSelf() != isShow)
		{
			this.BattleBackpackUITip.get_gameObject().SetActive(isShow);
		}
		if (isShow)
		{
			this.BattleBackpackUITip.set_text(text);
			this.BattleBackpackUIBGOffset.set_localPosition(this.BattleBackpackUIBGSlot1);
			this.BattleBackpackUISROffset.set_localPosition(this.BattleBackpackUISRSlot1);
		}
		else
		{
			this.BattleBackpackUIBGOffset.set_localPosition(this.BattleBackpackUIBGSlot0);
			this.BattleBackpackUISROffset.set_localPosition(this.BattleBackpackUISRSlot0);
		}
	}

	protected void OnBtnCloseClick(GameObject go)
	{
		this.Show(false);
	}
}
