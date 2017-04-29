using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InstanceChapterItem : BaseUIBehaviour
{
	public Image ImageBG1;

	public Image ImageBG2;

	public Image ImageBossHead;

	public Text TextDes;

	public Transform Stars;

	public Image ImageStar1;

	public Image ImageStar2;

	public Image ImageStar3;

	public Image ImageLock1;

	public Image ImageLock2;

	public Transform ImageFx;

	public int instanceID;

	private void OnEnable()
	{
		this.CheckAndPlayFx();
	}

	public void CheckAndPlayFx()
	{
		if (MainTaskManager.Instance.IsGoingTask(this.instanceID))
		{
			this.ImageFx.get_gameObject().SetActive(true);
			this.ImageFx.GetComponent<Animator>().Play("InstanceWorldMapItemFX");
		}
		else
		{
			this.ImageFx.get_gameObject().SetActive(false);
			this.ImageFx.GetComponent<Animator>().Play("None");
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageBG1 = base.FindTransform("ImageBG1").GetComponent<Image>();
		this.ImageBG2 = base.FindTransform("ImageBG2").GetComponent<Image>();
		this.ImageBossHead = base.FindTransform("ImageBossHead").GetComponent<Image>();
		this.ImageStar1 = base.FindTransform("ImageStar1").GetComponent<Image>();
		this.ImageStar2 = base.FindTransform("ImageStar2").GetComponent<Image>();
		this.ImageStar3 = base.FindTransform("ImageStar3").GetComponent<Image>();
		this.TextDes = base.FindTransform("TextDes").GetComponent<Text>();
		this.Stars = base.FindTransform("Stars");
		this.ImageLock1 = base.FindTransform("ImageLock1").GetComponent<Image>();
		this.ImageLock2 = base.FindTransform("ImageLock2").GetComponent<Image>();
		this.ImageFx = base.FindTransform("ImageFx");
	}
}
