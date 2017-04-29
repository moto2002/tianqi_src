using System;
using UnityEngine;
using UnityEngine.UI;

public class ToastUIItem : MonoBehaviour
{
	public bool Unused = true;

	public Text TextShow;

	public Image mBackground;

	public string Text
	{
		set
		{
			if (this.TextShow != null && this.mBackground != null)
			{
				this.TextShow.set_text(value);
				this.mBackground.get_rectTransform().set_sizeDelta(new Vector2(Mathf.Min(this.TextShow.get_preferredWidth(), this.TextShow.get_rectTransform().get_sizeDelta().x) + 100f, this.TextShow.get_preferredHeight() + 15f));
			}
		}
	}

	private void Awake()
	{
		this.TextShow = base.get_transform().Find("TextCal/TextShow").GetComponent<Text>();
		this.mBackground = base.get_transform().Find("TextCal/Image").GetComponent<Image>();
	}
}
