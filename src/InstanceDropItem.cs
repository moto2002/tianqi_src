using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine.UI;

public class InstanceDropItem : BaseUIBehaviour
{
	private Image ImageFrame;

	private Image ImageIcon;

	private Text ItemStepText;

	public int equipID;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.ImageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.ImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.ItemStepText = base.FindTransform("ItemStepText").GetComponent<Text>();
		this.isInit = true;
	}

	public void RefreshUI(int equipid)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		Items items = DataReader<Items>.Get(equipid);
		if (items == null)
		{
			return;
		}
		this.equipID = equipid;
		ResourceManager.SetSprite(this.ImageFrame, GameDataUtils.GetItemFrame(this.equipID));
		ResourceManager.SetSprite(this.ImageIcon, GameDataUtils.GetItemIcon(this.equipID));
		if (items.step <= 0)
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(true);
			if (this.ItemStepText != null)
			{
				this.ItemStepText.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
			}
		}
	}
}
