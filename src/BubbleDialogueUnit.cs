using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BubbleDialogueUnit : BaseUIBehaviour
{
	private const float MAX_WIDTH = 300f;

	private const float BACKGROUND_HEIGHT_MORE = 65f;

	public Transform Node2TargetPosition;

	private Image m_spBackground;

	private Text m_lblTalkContent;

	private BaseTweenAlphaBaseTime m_BaseTweenAlphaBaseTime;

	public long uuid;

	private static readonly Vector2 CONTECT_OFFSET = new Vector2(-25f, 58f);

	public void AwakeSelf(Transform prefab)
	{
		this.m_myTransform = prefab;
		this.m_myTransform.get_gameObject().SetActive(base.get_gameObject().get_activeSelf());
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spBackground = base.FindTransform("Background").GetComponent<Image>();
		this.m_lblTalkContent = base.FindTransform("TalkContent").GetComponent<Text>();
		this.m_BaseTweenAlphaBaseTime = this.m_myTransform.GetComponent<BaseTweenAlphaBaseTime>();
	}

	private void LateUpdate()
	{
		if (this.Node2TargetPosition == null || this.m_myTransform == null)
		{
			return;
		}
		if (CamerasMgr.CameraMain != null && !CamerasMgr.CameraMain.get_enabled())
		{
			return;
		}
		this.Show(BubbleDialogueManager.Instance.IsAVCOn(this.uuid));
		Vector3 position = this.Node2TargetPosition.get_position();
		Vector3 vector = CamerasMgr.CameraMain.WorldToScreenPoint(position);
		Vector3 position2 = CamerasMgr.CameraUI.ScreenToWorldPoint(vector);
		this.m_myTransform.set_position(position2);
		if (EntityWorld.Instance.ActSelf == null)
		{
			return;
		}
		if (Time.get_frameCount() % 10 != 0)
		{
			return;
		}
		Vector3 vector2 = CamerasMgr.CameraMain.WorldToScreenPoint(base.get_transform().get_position());
		if (vector2.z <= 1.5f)
		{
			this.TweenAlpha(false);
		}
		else if (vector2.z > 6f)
		{
			this.TweenAlpha(true);
		}
	}

	private void OnDisable()
	{
		if (this.m_myTransform != null)
		{
			this.m_myTransform.get_gameObject().SetActive(false);
		}
	}

	private void OnEnable()
	{
		if (this.m_myTransform != null)
		{
			this.m_myTransform.get_gameObject().SetActive(true);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		this.Show(false);
		BubbleDialogueManager.Instance.RemoveBubbleDialogue(this.uuid, base.get_transform());
	}

	public Transform GetBillboardTransform()
	{
		return this.m_myTransform;
	}

	public void SetTargetPositionNode(Transform targetPosition)
	{
		this.Node2TargetPosition = targetPosition;
	}

	public void SetUUID(long _uuid)
	{
		this.uuid = _uuid;
	}

	public void SetContent(string content)
	{
		if (EntityWorld.Instance.EntSelf != null)
		{
			ResourceManager.SetSprite(this.m_spBackground, ResourceManager.GetIconSprite("jueseduihuakuang"));
		}
		if (this.m_lblTalkContent != null)
		{
			this.m_lblTalkContent.get_rectTransform().set_anchoredPosition(BubbleDialogueUnit.CONTECT_OFFSET);
			this.m_lblTalkContent.get_rectTransform().set_sizeDelta(new Vector2(300f, 200f));
			this.m_lblTalkContent.set_text(content);
			if (this.m_lblTalkContent.get_preferredWidth() < 300f)
			{
				this.m_spBackground.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblTalkContent.get_preferredWidth() + Mathf.Abs(BubbleDialogueUnit.CONTECT_OFFSET.x) * 2f, this.m_lblTalkContent.get_preferredHeight() + 65f));
				this.m_lblTalkContent.get_rectTransform().set_anchoredPosition(new Vector2(BubbleDialogueUnit.CONTECT_OFFSET.x - (300f - this.m_lblTalkContent.get_preferredWidth()) * 0.5f, BubbleDialogueUnit.CONTECT_OFFSET.y));
			}
			else
			{
				this.m_spBackground.get_rectTransform().set_sizeDelta(new Vector2(300f + Mathf.Abs(BubbleDialogueUnit.CONTECT_OFFSET.x) * 2f, this.m_lblTalkContent.get_preferredHeight() + 65f));
			}
		}
	}

	public void ResetAll()
	{
		base.ResetParent();
		this.uuid = 0L;
		this.m_myTransform = null;
	}

	private void TweenAlpha(bool isShow)
	{
		if (isShow)
		{
			this.m_BaseTweenAlphaBaseTime.TweenAlphaToDst(1f, 0f, 0.5f);
		}
		else
		{
			this.m_BaseTweenAlphaBaseTime.TweenAlphaToDst(0f, 0f, 0.5f);
		}
	}

	private void Show(bool isShow)
	{
		if (this.m_spBackground != null)
		{
			this.m_spBackground.set_enabled(isShow);
		}
		if (this.m_lblTalkContent != null)
		{
			this.m_lblTalkContent.set_enabled(isShow);
		}
	}
}
