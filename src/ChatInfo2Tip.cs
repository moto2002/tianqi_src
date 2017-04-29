using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatInfo2Tip : ChatInfoBase
{
	private Image m_spChannelIcon;

	private static string _ChannelBlank;

	private static readonly Vector2 face_offset = new Vector2(15f, 0f);

	public static string ChannelBlank
	{
		get
		{
			if (ChatInfo2Tip._ChannelBlank == null)
			{
				ChatInfo2Tip._ChannelBlank = TextColorMgr.GetColorByID("123456", 301, 0f);
			}
			return ChatInfo2Tip._ChannelBlank;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FONT_SIZE = 18;
		this.LINE_WIDTH = 420f;
		this.m_spChannelIcon = base.FindTransform("ChannelIcon").GetComponent<Image>();
	}

	protected override void SetPreferredHeight()
	{
		this.m_height = this.m_lblContent.get_preferredHeight();
	}

	protected override void SetContentSize(string datetime)
	{
		RectTransform rectTransform = this.m_lblContent.get_transform() as RectTransform;
		rectTransform.set_sizeDelta(new Vector2(this.LINE_WIDTH, ChatInfoBase.GetContentHeight(this.m_lblContent)));
	}

	protected override bool IsLimitOneLine()
	{
		return false;
	}

	protected override string DealPlaceholder(string previous, string content, List<DetailInfo> items)
	{
		return base.Content2FilterPlaceholder(previous, content, items);
	}

	protected override void SetContent(string text_channel, string text_senderName, string text_chatContent, string datetime)
	{
		if (ChatManager.IsVoice(this.m_chatInfo))
		{
			text_chatContent = "[语音]";
		}
		ResourceManager.SetIconSprite(this.m_spChannelIcon, ChannelBit.GetChannelIcon(this.m_chatInfo.src_channel));
		base.SetContent(ChatInfo2Tip.ChannelBlank + text_senderName + text_chatContent, datetime);
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
		return ChatInfoBase.GetStringColorByChannel(this.m_chatInfo.src_channel, content);
	}

	protected override Vector2 GetFaceOffset()
	{
		return ChatInfo2Tip.face_offset;
	}
}
