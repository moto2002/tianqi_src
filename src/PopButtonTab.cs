using System;
using UnityEngine;
using UnityEngine.UI;

public class PopButtonTab : MonoBehaviour
{
	private RectTransform m_rtanButton;

	private Text m_lblButtonText;

	private RectTransform m_tranButtonVerticalLine;

	private Action m_callback;

	public float WidthLength = 100f;

	private void Awake()
	{
		this.m_rtanButton = (base.get_transform() as RectTransform);
		this.m_lblButtonText = base.get_transform().Find("ButtonText").GetComponent<Text>();
		this.m_tranButtonVerticalLine = (base.get_transform().Find("ButtonVerticalLine") as RectTransform);
		EventTriggerListener.Get(base.get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnButtonUp);
	}

	private void OnButtonUp(GameObject sender)
	{
		if (this.m_callback != null)
		{
			this.m_callback.Invoke();
		}
	}

	public void SetIndex(int index)
	{
		this.m_tranButtonVerticalLine.get_gameObject().SetActive(index != 0);
	}

	public void SetName(string name)
	{
		this.m_lblButtonText.set_text("    " + name + "    ");
		this.CalWidthLength();
	}

	public void SetCallBack(Action callback)
	{
		this.m_callback = callback;
	}

	private void CalWidthLength()
	{
		this.WidthLength = this.m_lblButtonText.get_preferredWidth();
		(this.m_lblButtonText.get_transform() as RectTransform).set_sizeDelta(new Vector3(this.WidthLength, 50f));
		this.m_rtanButton.set_sizeDelta(new Vector2(this.WidthLength, this.m_rtanButton.get_sizeDelta().y));
		this.m_tranButtonVerticalLine.set_anchoredPosition(new Vector3(this.m_tranButtonVerticalLine.get_sizeDelta().x / 2f, this.m_tranButtonVerticalLine.get_localPosition().y));
	}
}
