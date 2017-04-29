using System;
using UnityEngine;
using UnityEngine.UI;

public class FloatTextUnit : MonoBehaviour
{
	private Transform m_floatNode;

	private BaseTweenAlphaBaseTime m_BaseTweenAlphaBaseTime;

	private BaseTweenPostion m_BaseTweenPostion;

	private Text m_lblText;

	private void Awake()
	{
		this.m_floatNode = base.get_transform().FindChild("FloatNode");
		this.m_BaseTweenAlphaBaseTime = this.m_floatNode.GetComponent<BaseTweenAlphaBaseTime>();
		this.m_BaseTweenPostion = this.m_floatNode.GetComponent<BaseTweenPostion>();
		this.m_lblText = this.m_floatNode.GetChild(0).GetComponent<Text>();
	}

	private void OnDisable()
	{
		this.m_floatNode.set_localPosition(Vector3.get_zero());
	}

	public void ShowAsFloatText(string text, Color col)
	{
		base.get_transform().set_name("AsFloatText");
		this.m_lblText.set_text(text);
		this.m_lblText.set_color(col);
		this.m_BaseTweenAlphaBaseTime.TweenAlpha(1f, 0f, 1f, 0.5f, delegate
		{
			FloatTextAddManager.FloatTextPool.ReUse(base.get_gameObject());
		});
		Vector3 dstPosition = new Vector3(0f, 150f, 0f);
		this.m_BaseTweenPostion.MoveTo(dstPosition, 1.5f);
	}
}
