using System;
using UnityEngine;
using UnityEngine.UI;

public class FloatTipUnit : MonoBehaviour
{
	private Transform m_floatNode;

	private BaseTweenAlphaBaseTime m_BaseTweenAlphaBaseTime;

	private BaseTweenPostion m_BaseTweenPostion;

	private RectTransform mBackground;

	private Text m_lblText;

	private Transform m_actorRoot;

	private float m_height;

	private void Awake()
	{
		this.m_floatNode = base.get_transform().FindChild("FloatNode");
		this.m_BaseTweenAlphaBaseTime = this.m_floatNode.GetComponent<BaseTweenAlphaBaseTime>();
		this.m_BaseTweenPostion = this.m_floatNode.GetComponent<BaseTweenPostion>();
		this.mBackground = (this.m_floatNode.FindChild("Background") as RectTransform);
		this.m_lblText = this.m_floatNode.FindChild("Text").GetComponent<Text>();
	}

	private void OnDisable()
	{
		this.m_floatNode.set_localPosition(Vector3.get_zero());
	}

	private void Update()
	{
		this.UpdatePos();
	}

	public void ShowAsFloatTip(Transform actorRoot, string text, string color, bool isShowBg, float duration, float duration_alpha, float height, float floatHeight)
	{
		base.get_transform().set_name("AsFloatTip");
		this.m_actorRoot = actorRoot;
		this.m_height = height;
		this.UpdatePos();
		this.m_lblText.set_text(TextColorMgr.GetColor(text, color, string.Empty));
		float num = Mathf.Max(300f, this.m_lblText.get_preferredWidth() + 100f);
		this.mBackground.set_sizeDelta(new Vector2(num, this.mBackground.get_sizeDelta().y));
		this.mBackground.get_gameObject().SetActive(isShowBg);
		this.m_BaseTweenAlphaBaseTime.TweenAlpha(1f, 0f, duration - duration_alpha, duration_alpha, delegate
		{
			FloatTipManager.FloatTipPool.ReUse(base.get_gameObject());
		});
		Vector3 dstPosition = new Vector3(0f, floatHeight, 0f);
		this.m_BaseTweenPostion.MoveTo(dstPosition, duration);
	}

	private void UpdatePos()
	{
		if (this.m_actorRoot == null)
		{
			return;
		}
		Vector3 vector = CamerasMgr.CameraMain.WorldToScreenPoint(this.m_actorRoot.get_position() + new Vector3(0f, this.m_height, 0f));
		Vector3 position = CamerasMgr.CameraUI.ScreenToWorldPoint(vector);
		base.get_transform().set_position(position);
		base.get_transform().set_localPosition(new Vector3(base.get_transform().get_localPosition().x, base.get_transform().get_localPosition().y, 0f));
	}
}
