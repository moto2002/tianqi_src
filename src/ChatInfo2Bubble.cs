using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatInfo2Bubble : ChatInfoBase
{
	private const float HEIGHT_MAX = 200f;

	private const float MIN_Y = 50f;

	private const float MINX = 130f;

	private const float BackgroundMoreX = 35f;

	private const float BackgroundMoreY = 30f;

	private RectTransform mBackground;

	private float BackgroundSizeY;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.LINE_WIDTH = 180f;
		this.FONT_SIZE = 18;
		this.mBackground = (base.FindTransform("Background") as RectTransform);
	}

	protected override void SetPreferredHeight()
	{
		this.m_height = -Mathf.Min(200f, this.m_lblContent.get_preferredHeight());
	}

	protected override void SetContentSize(string datetime)
	{
		RectTransform rectTransform = this.m_lblContent.get_transform() as RectTransform;
		rectTransform.set_sizeDelta(new Vector2(this.LINE_WIDTH, Mathf.Max(200f, ChatInfoBase.GetContentHeight(this.m_lblContent))));
		this.SetBackgroundSize(ChatManager.IsVoice(this.m_chatInfo));
	}

	protected override bool IsLimitOneLine()
	{
		return false;
	}

	protected override string DealPlaceholder(string previous, string content, List<DetailInfo> items)
	{
		return base.Content2FilterPlaceholder(string.Empty, content, items);
	}

	protected override void SetContent(string text_channel, string text_senderName, string text_chatContent, string datetime)
	{
		if (ChatManager.IsVoice(this.m_chatInfo))
		{
			text_chatContent = "[语音]";
		}
		base.SetContent(text_chatContent, datetime);
	}

	protected override string GetSenderName(int channel, string senderName)
	{
		if (string.IsNullOrEmpty(senderName))
		{
			return string.Empty;
		}
		return TextColorMgr.GetColorByID(senderName + ":", 209);
	}

	protected override string GetContentTextInColor(string content)
	{
		return content;
	}

	protected override void SettingSuccess()
	{
		this.SetBubbleOffsetY(this.BackgroundSizeY - 50f);
	}

	private void SetBackgroundSize(bool isVoice)
	{
		if (isVoice)
		{
			this.BackgroundSizeY = 50f;
			this.mBackground.set_sizeDelta(new Vector2(130f, this.BackgroundSizeY));
		}
		else
		{
			this.BackgroundSizeY = Mathf.Max(50f, this.m_lblContent.get_preferredHeight() + 30f);
			this.mBackground.set_sizeDelta(new Vector2(Mathf.Max(this.GetWidthOfContent() + 35f, 130f), this.BackgroundSizeY));
		}
	}

	private float GetWidthOfContent()
	{
		return Mathf.Min(this.LINE_WIDTH, this.m_lblContent.get_preferredWidth());
	}

	private void SetBubbleOffsetY(float offsetY)
	{
	}
}
