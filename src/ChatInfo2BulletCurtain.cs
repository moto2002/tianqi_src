using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatInfo2BulletCurtain : ChatInfoBase
{
	private BaseTweenPostion m_BaseTweenPostion;

	public int m_srcChannel;

	private float Distance2Edge = 40f;

	private int RandomFontSize = 24;

	private float RandomDuration;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.IsHorizonalOverflow = true;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_BaseTweenPostion = base.get_gameObject().AddComponent<BaseTweenPostion>();
	}

	public void SetChat(ChatManager.ChatInfo chatInfo, Action callback)
	{
		this.FONT_SIZE = this.GetRandomFontSize();
		this.m_srcChannel = chatInfo.src_channel;
		this.ShowInfo(chatInfo);
		this.ResetAll();
		this.m_BaseTweenPostion.MoveTo(new Vector3(this.GetEndPosX(), base.get_transform().get_localPosition().y, 0f), this.GetRandomSpeed(), callback);
	}

	private void ResetAll()
	{
		this.SetRandomPosY();
		this.SetRandomFontSize();
		this.SetColor();
	}

	private void SetRandomPosY()
	{
		float num = Random.Range(-UIConst.UI_SIZE_HEIGHT * 0.5f + this.Distance2Edge, UIConst.UI_SIZE_HEIGHT * 0.5f - this.Distance2Edge);
		base.get_transform().set_localPosition(new Vector3(this.GetBeginPosX(), num, 0f));
	}

	private int GetRandomFontSize()
	{
		if (BulletCurtainManager.Instance.IsRandomSize)
		{
			this.RandomFontSize = Random.Range(24, 48);
		}
		return this.RandomFontSize;
	}

	private void SetRandomFontSize()
	{
		this.m_lblContent.set_fontSize(this.RandomFontSize);
		(this.m_lblContent.get_transform() as RectTransform).set_sizeDelta(new Vector2(this.m_lblContent.get_preferredWidth(), (float)(this.RandomFontSize + 15)));
	}

	private void SetColor()
	{
		if (BulletCurtainManager.Instance.IsRandomColor)
		{
			string text = this.m_lblContent.get_text();
			text = TextColorMgr.FilterColor(text);
			int quality = Random.Range(1, 7);
			text = TextColorMgr.GetColorByQuality(text, quality, BulletCurtainManager.Instance.Alpha);
			this.m_lblContent.set_text(text);
		}
	}

	private float GetRandomSpeed()
	{
		if (BulletCurtainManager.Instance.IsRandomSpeed)
		{
			this.RandomDuration = (float)Random.Range(5, 20);
		}
		else
		{
			this.RandomDuration = 15f;
		}
		return this.RandomDuration;
	}

	private float GetBeginPosX()
	{
		return (float)(Screen.get_width() / 2 + 200);
	}

	private float GetEndPosX()
	{
		return (float)(-(float)(Screen.get_width() / 2 + 200)) - this.m_lblContent.get_preferredWidth();
	}

	protected override void SetPreferredHeight()
	{
		this.m_height = (float)this.FONT_SIZE + 6f;
	}

	protected override void SetContentSize(string datetime)
	{
		RectTransform rectTransform = this.m_lblContent.get_transform() as RectTransform;
		rectTransform.set_sizeDelta(new Vector2(this.LINE_WIDTH, this.m_height));
	}

	protected override bool IsLimitOneLine()
	{
		return true;
	}

	protected override string DealPlaceholder(string previous, string content, List<DetailInfo> items)
	{
		return base.Content2FilterPlaceholder(previous, content, items);
	}

	protected override void SetContent(string text_channel, string text_senderName, string text_chatContent, string datetime)
	{
		base.SetContent(text_channel + text_senderName + text_chatContent, datetime);
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
		return new Vector2(0f, (float)this.FONT_SIZE * 0.5f + 4f);
	}
}
