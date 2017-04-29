using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBloodUnit : MonoBehaviour
{
	public float time;

	public float stay;

	public float offset_x;

	public float offset_y;

	public float horizontalOffset;

	public float verticalOffset;

	public Vector2 screenPos;

	public float val;

	public WaveBloodPerformance wbp;

	public RectTransform thisTransform;

	public GridLayoutGroup m_lgTexts;

	public List<int> cellSize;

	public List<Image> listImageText = new List<Image>();

	public Image m_spImage;

	public AnimationCurve offsetYCurve;

	public AnimationCurve offsetXCurve;

	public AnimationCurve alphaCurve;

	public AnimationCurve scaleCurve;

	private bool IsInit;

	public float movementStart
	{
		get
		{
			return this.time + this.stay;
		}
	}

	public void AwakeSelf()
	{
		if (!this.IsInit)
		{
			this.IsInit = true;
			this.thisTransform = (base.get_transform() as RectTransform);
			this.m_lgTexts = this.thisTransform.FindChild("Texts").GetComponent<GridLayoutGroup>();
			this.m_spImage = this.thisTransform.FindChild("Image").GetComponent<Image>();
		}
	}

	public void SetImage(bool isShow, WaveBloodPerformance wbp)
	{
		Utils.SetTransformZOn(this.m_lgTexts.get_transform(), false);
		if (isShow)
		{
			this.m_spImage.set_enabled(true);
			if (wbp == WaveBloodPerformance.MissToSelf)
			{
				ResourceManager.SetIconSprite(this.m_spImage, "miss_ziji");
			}
			else if (wbp == WaveBloodPerformance.MissToOther)
			{
				ResourceManager.SetIconSprite(this.m_spImage, "miss_difang");
			}
			else if (wbp == WaveBloodPerformance.Break)
			{
				ResourceManager.SetIconSprite(this.m_spImage, "daduanchenggong");
			}
			this.m_spImage.SetNativeSize();
		}
		else
		{
			this.m_spImage.set_enabled(false);
		}
	}

	public void SetNum(int num, int resource)
	{
		this.SetImage(false, this.wbp);
		Vector2 vector = this.m_lgTexts.get_cellSize();
		vector.x = (float)this.cellSize.get_Item(0);
		vector.y = (float)this.cellSize.get_Item(1);
		this.m_lgTexts.set_cellSize(vector);
		Utils.SetTransformZOn(this.m_lgTexts.get_transform(), true);
		char[] array = num.ToString().ToCharArray();
		if (array.Length < this.listImageText.get_Count())
		{
			while (array.Length < this.listImageText.get_Count())
			{
				int num2 = this.listImageText.get_Count() - 1;
				WaveBloodManager.Instance.ReuseNum(this.listImageText.get_Item(num2).get_gameObject());
				this.listImageText.RemoveAt(num2);
			}
		}
		else if (array.Length > this.listImageText.get_Count())
		{
			for (int i = this.listImageText.get_Count(); i < array.Length; i++)
			{
				GameObject num3 = WaveBloodManager.Instance.GetNum();
				Image component = num3.GetComponent<Image>();
				component.get_rectTransform().SetParent(this.m_lgTexts.get_transform());
				this.listImageText.Add(component);
			}
		}
		int num4 = 0;
		while (num4 < this.listImageText.get_Count() && num4 < array.Length)
		{
			Image image = this.listImageText.get_Item(num4);
			image.get_rectTransform().set_localScale(Vector3.get_one());
			Vector2 sizeDelta = image.get_rectTransform().get_sizeDelta();
			sizeDelta.x = (float)this.cellSize.get_Item(0);
			sizeDelta.y = (float)this.cellSize.get_Item(1);
			image.get_rectTransform().set_sizeDelta(sizeDelta);
			ResourceManager.SetSprite(this.listImageText.get_Item(num4), ResourceManager.GetIconSprite(WaveBloodManager.GetNumPrefix(resource) + array[num4]));
			num4++;
		}
		this.offset_x = 0f;
		this.offset_y = 0f;
		this.thisTransform.get_gameObject().SetActive(true);
	}
}
