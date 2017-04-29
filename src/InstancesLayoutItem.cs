using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InstancesLayoutItem : BaseUIBehaviour
{
	private Image Image1;

	private Image Image2;

	private Image ImageIcon1;

	private Image ImageIcon2;

	private Image ImageQue;

	private Text Text1;

	private Text Text2;

	private Transform Stars;

	private Image ImageStar1_2;

	private Image ImageStar2_2;

	private Image ImageStar3_2;

	public int instanceID;

	public bool isBoss;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.Image1 = base.FindTransform("Image1").GetComponent<Image>();
		this.Image2 = base.FindTransform("Image2").GetComponent<Image>();
		this.ImageIcon1 = base.FindTransform("ImageIcon1").GetComponent<Image>();
		this.ImageIcon2 = base.FindTransform("ImageIcon2").GetComponent<Image>();
		this.ImageQue = base.FindTransform("ImageQue").GetComponent<Image>();
		this.Text1 = base.FindTransform("Text1").GetComponent<Text>();
		this.Text2 = base.FindTransform("Text2").GetComponent<Text>();
		this.Stars = base.FindTransform("Stars");
		this.ImageStar1_2 = base.FindTransform("ImageStar1_2").GetComponent<Image>();
		this.ImageStar2_2 = base.FindTransform("ImageStar2_2").GetComponent<Image>();
		this.ImageStar3_2 = base.FindTransform("ImageStar3_2").GetComponent<Image>();
	}

	public void RefreshUI(FuBenJiChuPeiZhi ic, bool isLock, int instanceIndex, int starNum)
	{
		this.instanceID = ic.id;
		string iconName = GameDataUtils.GetIconName(DataReader<ZhuXianPeiZhi>.Get(this.instanceID).icon);
		ResourceManager.SetSprite(this.ImageIcon1, ResourceManager.GetIconSprite(iconName));
		ResourceManager.SetSprite(this.ImageIcon2, ResourceManager.GetIconSprite(iconName));
		this.Text1.set_text(GameDataUtils.GetChineseContent(505085, false) + (instanceIndex + 1).ToString());
		this.Text2.set_text(GameDataUtils.GetChineseContent(505085, false) + (instanceIndex + 1).ToString());
		if (isLock)
		{
			this.Image1.get_gameObject().SetActive(true);
			this.Image2.get_gameObject().SetActive(false);
			this.Text1.get_gameObject().SetActive(true);
			this.Text2.get_gameObject().SetActive(false);
			this.Stars.get_gameObject().SetActive(false);
			this.ImageQue.get_gameObject().SetActive(true);
			this.ImageIcon1.get_gameObject().SetActive(true);
			this.ImageIcon2.get_gameObject().SetActive(false);
		}
		else
		{
			this.Image1.get_gameObject().SetActive(false);
			this.Image2.get_gameObject().SetActive(true);
			this.Text1.get_gameObject().SetActive(false);
			this.Text2.get_gameObject().SetActive(true);
			if (ic.type == 103)
			{
				this.Stars.get_gameObject().SetActive(false);
			}
			else
			{
				this.Stars.get_gameObject().SetActive(true);
			}
			this.ImageQue.get_gameObject().SetActive(false);
			this.ImageIcon1.get_gameObject().SetActive(false);
			this.ImageIcon2.get_gameObject().SetActive(true);
			if (starNum == 0)
			{
				this.ImageStar1_2.get_gameObject().SetActive(false);
				this.ImageStar2_2.get_gameObject().SetActive(false);
				this.ImageStar3_2.get_gameObject().SetActive(false);
			}
			else if (starNum == 1)
			{
				this.ImageStar1_2.get_gameObject().SetActive(true);
				this.ImageStar2_2.get_gameObject().SetActive(false);
				this.ImageStar3_2.get_gameObject().SetActive(false);
			}
			else if (starNum == 2)
			{
				this.ImageStar1_2.get_gameObject().SetActive(true);
				this.ImageStar2_2.get_gameObject().SetActive(true);
				this.ImageStar3_2.get_gameObject().SetActive(false);
			}
			else if (starNum == 3)
			{
				this.ImageStar1_2.get_gameObject().SetActive(true);
				this.ImageStar2_2.get_gameObject().SetActive(true);
				this.ImageStar3_2.get_gameObject().SetActive(true);
			}
		}
	}
}
