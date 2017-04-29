using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageAlpha : MonoBehaviour
{
	public float alpha = 1f;

	private Image m_image;

	private void Awake()
	{
		this.m_image = base.GetComponent<Image>();
	}

	private void Update()
	{
		if (this.m_image != null)
		{
			this.m_image.set_color(new Color(this.m_image.get_color().r, this.m_image.get_color().g, this.m_image.get_color().b, this.alpha));
		}
	}
}
