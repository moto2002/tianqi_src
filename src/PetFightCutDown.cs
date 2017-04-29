using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetFightCutDown : BaseUIBehaviour
{
	protected List<Image> TimeImages = new List<Image>();

	protected Image CDImage;

	protected int index;

	protected float time;

	protected bool isCoundowning;

	protected bool isEnable = true;

	protected bool isPetSkill = true;

	public void Init(int theIndex, Transform outerCDTransform)
	{
		this.TimeImages.Add(base.FindTransform("NumImage10").GetComponent<Image>());
		this.TimeImages.Add(base.FindTransform("NumImage").GetComponent<Image>());
		this.CDImage = outerCDTransform.GetComponent<Image>();
		this.index = theIndex;
	}

	public void SetCutDown(float t, bool isDo = true, bool ispetSkill = true)
	{
		if (t == 0f)
		{
			return;
		}
		this.time = t / 1000f;
		ImageColorMgr.SetImageColor(this.TimeImages.get_Item(0), false);
		ImageColorMgr.SetImageColor(this.TimeImages.get_Item(1), false);
		if (isDo)
		{
			this.CDImage.set_enabled(false);
		}
		this.isEnable = isDo;
		base.get_gameObject().SetActive(true);
		if (ispetSkill)
		{
			EventDispatcher.Broadcast<int>(EventNames.PetCountDownStart, this.index);
		}
		this.isPetSkill = ispetSkill;
	}

	public void Hidden()
	{
		this.InitUI();
		base.get_gameObject().SetActive(false);
		if (this.isEnable)
		{
			this.CDImage.set_enabled(true);
		}
	}

	private void Update()
	{
		this.time -= Time.get_deltaTime();
		if (this.time > 0f)
		{
			int num = (int)this.time % 60;
			if (num / 10 > 0)
			{
				this.TimeImages.get_Item(0).get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.TimeImages.get_Item(0), ResourceManager.GetIconSprite("new_cd_" + num / 10));
			}
			else
			{
				this.TimeImages.get_Item(0).get_gameObject().SetActive(false);
			}
			ResourceManager.SetSprite(this.TimeImages.get_Item(1), ResourceManager.GetIconSprite("new_cd_" + num % 10));
			this.isCoundowning = true;
		}
		else
		{
			this.Hidden();
			if (this.isCoundowning && this.isPetSkill)
			{
				EventDispatcher.Broadcast<int>(EventNames.PetCountDownEnd, this.index);
			}
			this.isCoundowning = false;
		}
	}
}
