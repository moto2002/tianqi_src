using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChatInfoBase : BaseUIBehaviour
{
	private const float HEIGHT_MORE_SIZE = 10f;

	[HideInInspector]
	public bool IsHorizonalOverflow;

	[HideInInspector]
	public float LINE_WIDTH = 700f;

	[HideInInspector]
	public int FONT_SIZE = 24;

	[HideInInspector]
	public Text m_lblContent;

	[HideInInspector]
	private Transform _FaceParent;

	public float m_height;

	protected ChatManager.ChatInfo m_chatInfo;

	protected List<Item2Face> Faces = new List<Item2Face>();

	private int PlaceholderIndex;

	protected long SenderUID;

	protected string SenderName;

	protected Transform FaceParent
	{
		get
		{
			if (this._FaceParent == null)
			{
				this._FaceParent = base.get_transform();
			}
			return this._FaceParent;
		}
		set
		{
			this._FaceParent = value;
		}
	}

	protected long GuildId
	{
		get
		{
			return GuildManager.Instance.GetGuildId();
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_lblContent = base.FindTransform("Content").GetComponent<Text>();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public virtual void ShowInfo(string content)
	{
	}

	public virtual void ShowInfo(ChatManager.ChatInfo chatInfo)
	{
		this.JustShowInfo(chatInfo);
	}

	protected virtual void SetPreferredHeight()
	{
	}

	protected virtual void SetContentSize(string datetime)
	{
	}

	public virtual void Clear()
	{
		this.ReuseFaces();
		this.ClearColliders();
		this.m_lblContent.set_text(string.Empty);
	}

	protected virtual void ClearColliders()
	{
	}

	protected virtual void ButtonItem(string text_itemPrevious, DetailInfo detailInfo, string name)
	{
	}

	protected virtual void ButtonModule(string text_modulePreviour, string moduleHrefString, HrefModule moduleHref)
	{
	}

	protected virtual void ButtonSender(string text_channel, string text_senderName)
	{
	}

	protected virtual void SetSenderIcon(int channel, int occupation)
	{
	}

	protected virtual void SetSenderName(string name, string datetime, int viplevel)
	{
	}

	protected virtual void SetVIP(int level)
	{
	}

	protected virtual void SettingSuccess()
	{
	}

	private void JustShowInfo(ChatManager.ChatInfo chatInfo)
	{
		this.m_chatInfo = chatInfo;
		this.SenderUID = chatInfo.sender_uid;
		this.SenderName = chatInfo.sender_name;
		this.SetSenderIcon(chatInfo.src_channel, chatInfo.sender_occupation);
		this.SetVIP(chatInfo.viplevel);
		string channelNameWithColor = ChatManager.GetChannelNameWithColor(chatInfo.src_channel);
		string senderName = this.GetSenderName(chatInfo.src_channel, chatInfo.sender_name);
		this.SetSenderName(senderName, chatInfo.time, chatInfo.viplevel);
		string previous = channelNameWithColor + senderName;
		if (ChatManager.IsVoice(chatInfo))
		{
			this.SetContent(channelNameWithColor, senderName, chatInfo.chat_content, chatInfo.time);
			return;
		}
		string text = this.GetContent();
		text = this.DealPlaceholder(previous, text, chatInfo.items);
		for (int i = this.PlaceholderIndex; i < chatInfo.items.get_Count(); i++)
		{
			DetailInfo detailInfo = chatInfo.items.get_Item(i);
			if (detailInfo.type == DetailType.DT.Equipment)
			{
				Items items = DataReader<Items>.Get(detailInfo.cfgId);
				if (items != null)
				{
					string detailInfoName = ChatManager.GetDetailInfoName(detailInfo);
					this.ButtonItem(text, detailInfo, detailInfoName);
					text += detailInfoName;
				}
			}
		}
		text = this.GetContentTextInColor(text);
		this.SetContent(channelNameWithColor, senderName, text, chatInfo.time);
		this.ButtonSender(channelNameWithColor, senderName);
		this.SettingSuccess();
	}

	protected void ReuseFaces()
	{
		for (int i = 0; i < this.Faces.get_Count(); i++)
		{
			ChatManager.Reuse2FacePool(this.Faces.get_Item(i));
		}
		this.Faces.Clear();
	}

	protected abstract string DealPlaceholder(string previous, string content, List<DetailInfo> items);

	protected virtual Vector2 GetFaceOffset()
	{
		return Vector2.get_zero();
	}

	protected string Content2FilterPlaceholder(string previous, string content, List<DetailInfo> items)
	{
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		this.PlaceholderIndex = 0;
		int num = 0;
		while (num + ChatManager.ItemPlaceholder2Length <= content.get_Length())
		{
			string text = content.Substring(num, ChatManager.ItemPlaceholder2Length);
			if (text.Equals(ChatManager.ItemPlaceholder))
			{
				if (this.PlaceholderIndex < items.get_Count())
				{
					DetailInfo detailInfo = items.get_Item(this.PlaceholderIndex);
					float preferredWidth = ChatManager.Instance.GetPreferredWidth(previous + content.Substring(0, num), this.FONT_SIZE, true);
					if (!this.IsHorizonalOverflow)
					{
						if (!this.HorizonalOverflowIsFalse(preferredWidth, content.Substring(0, num), detailInfo))
						{
							return content;
						}
					}
					else
					{
						this.HorizonalOverflowIsTrue(preferredWidth, content.Substring(0, num), detailInfo);
					}
					if (detailInfo.type == DetailType.DT.Face)
					{
						string blank = ChatManager.GetBlank((float)this.FONT_SIZE * 1f, this.FONT_SIZE);
						content = content.ReplaceFirst(ChatManager.ItemPlaceholder, blank, 0);
						if (!string.IsNullOrEmpty(blank))
						{
							int num2 = ChatManager.ItemPlaceholder.get_Length() - blank.get_Length();
							num -= num2;
						}
					}
					else if (!string.IsNullOrEmpty(detailInfo.label))
					{
						string detailInfoName = ChatManager.GetDetailInfoName(detailInfo);
						content = content.ReplaceFirst(ChatManager.ItemPlaceholder, detailInfoName, 0);
						int num3 = ChatManager.ItemPlaceholder.get_Length() - detailInfoName.get_Length();
						num -= num3;
					}
				}
				num += ChatManager.ItemPlaceholder2Length;
				this.PlaceholderIndex++;
			}
			else
			{
				num++;
			}
		}
		return content;
	}

	private bool HorizonalOverflowIsFalse(float length, string previous_content, DetailInfo detailInfo)
	{
		int num = (int)(length / this.LINE_WIDTH);
		float num2 = length % this.LINE_WIDTH;
		if (this.IsLimitOneLine() && num > 0)
		{
			return false;
		}
		if (detailInfo.type == DetailType.DT.Face)
		{
			Item2Face item2Face = ChatManager.CreateFace(detailInfo.cfgId, this.FaceParent);
			item2Face.SetFaceSize(this.FONT_SIZE);
			this.Faces.Add(item2Face);
			RectTransform rectTransform = item2Face.get_transform() as RectTransform;
			rectTransform.set_anchoredPosition(new Vector2(num2, (float)(-(float)num * this.FONT_SIZE)));
			RectTransform expr_80 = rectTransform;
			expr_80.set_anchoredPosition(expr_80.get_anchoredPosition() + this.GetFaceOffset());
			return true;
		}
		string detailInfoName = ChatManager.GetDetailInfoName(detailInfo);
		this.ButtonItem(previous_content, detailInfo, detailInfoName);
		return true;
	}

	private bool HorizonalOverflowIsTrue(float length, string previous_content, DetailInfo detailInfo)
	{
		if (detailInfo.type == DetailType.DT.Face)
		{
			Item2Face item2Face = ChatManager.CreateFace(detailInfo.cfgId, this.FaceParent);
			item2Face.SetFaceSize(this.FONT_SIZE);
			this.Faces.Add(item2Face);
			RectTransform rectTransform = item2Face.get_transform() as RectTransform;
			rectTransform.set_anchoredPosition(new Vector2(length, 0f));
			RectTransform expr_54 = rectTransform;
			expr_54.set_anchoredPosition(expr_54.get_anchoredPosition() + this.GetFaceOffset());
			return true;
		}
		string detailInfoName = ChatManager.GetDetailInfoName(detailInfo);
		this.ButtonItem(previous_content, detailInfo, detailInfoName);
		return true;
	}

	protected abstract bool IsLimitOneLine();

	protected virtual string GetContent()
	{
		return this.m_chatInfo.chat_content;
	}

	protected abstract void SetContent(string text_channel, string text_senderName, string text_chatContent, string datetime);

	protected void SetContent(string content, string datetime)
	{
		this.m_lblContent.set_text(content);
		this.SetPreferredHeight();
		this.SetContentSize(datetime);
	}

	protected abstract string GetContentTextInColor(string content);

	protected static string GetStringColorByChannel(int channel, string content)
	{
		switch (channel)
		{
		case 1:
			content = TextColorMgr.GetColorByID(content, 205);
			return content;
		case 2:
			content = TextColorMgr.GetColorByID(content, 207);
			return content;
		case 3:
		case 5:
		case 6:
		case 7:
			IL_2A:
			if (channel == 32)
			{
				content = TextColorMgr.GetColorByID(content, 213);
				return content;
			}
			if (channel == 64)
			{
				content = TextColorMgr.GetColorByID(content, 208);
				return content;
			}
			if (channel != 128)
			{
				return content;
			}
			content = TextColorMgr.GetColorByID(content, 215);
			return content;
		case 4:
			content = TextColorMgr.GetColorByID(content, 206);
			return content;
		case 8:
			content = TextColorMgr.GetColorByID(content, 208);
			return content;
		}
		goto IL_2A;
	}

	protected abstract string GetSenderName(int channel, string senderName);

	public static float GetContentHeight(Text content)
	{
		return content.get_preferredHeight() + 10f;
	}
}
