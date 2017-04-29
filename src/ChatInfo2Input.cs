using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatInfo2Input : ChatInfoBase
{
	private const float FACE_LENGTH = 28f;

	private RectTransform RNode2Content;

	protected List<Item2Face> OldFaces = new List<Item2Face>();

	private static readonly Vector2 FACE_OFFSET = new Vector2(0f, 0f);

	public int FaceNum;

	private void Awake()
	{
		this.IsHorizonalOverflow = true;
		this.LINE_WIDTH = 460f;
		this.FONT_SIZE = 30;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.RNode2Content = (base.FindTransform("Node2Content") as RectTransform);
		base.FaceParent = this.RNode2Content;
		this.m_lblContent = base.FindTransform("Content").GetComponent<Text>();
		this.m_lblContent.set_fontSize(this.FONT_SIZE);
		this.m_lblContent.set_text(string.Empty);
	}

	private void ReuseOldFaces()
	{
		for (int i = 0; i < this.OldFaces.get_Count(); i++)
		{
			ChatManager.Reuse2FacePool(this.OldFaces.get_Item(i));
		}
		this.OldFaces.Clear();
	}

	protected string Content2FilterPlaceholder(string content, bool faceShow)
	{
		this.FaceNum = 0;
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		string blank = ChatManager.GetBlank(28f, this.FONT_SIZE);
		int num = 0;
		while (num + ChatManager.FacePlaceholder2TotalLength <= content.get_Length())
		{
			string text = content.Substring(num, ChatManager.FacePlaceholder2TotalLength);
			int num2 = ChatManager.Instance.FindPlaceholder2FaceNum(text);
			if (num2 > 0)
			{
				this.FaceNum++;
				if (faceShow)
				{
					this.CreateFace(content, num, num2);
				}
				content = content.ReplaceFirst(text, blank, 0);
				if (!string.IsNullOrEmpty(blank))
				{
					int num3 = text.get_Length() - blank.get_Length();
					num -= num3;
				}
				num += ChatManager.FacePlaceholder2TotalLength;
			}
			else
			{
				num++;
			}
		}
		return content;
	}

	private void CreateFace(string content, int index, int num)
	{
		float preferredWidth = ChatManager.Instance.GetPreferredWidth(content.Substring(0, index), this.FONT_SIZE, true);
		Item2Face item2Face = ChatManager.CreateFace(num, base.FaceParent);
		item2Face.SetFaceSize(this.FONT_SIZE);
		this.Faces.Add(item2Face);
		RectTransform rectTransform = item2Face.get_transform() as RectTransform;
		rectTransform.set_anchoredPosition(new Vector2(preferredWidth, 0f));
		RectTransform expr_5D = rectTransform;
		expr_5D.set_anchoredPosition(expr_5D.get_anchoredPosition() + ChatInfo2Input.FACE_OFFSET);
	}

	public override void ShowInfo(string content)
	{
		this.Clear();
		this.Content2FilterPlaceholder(content, false);
		this.m_lblContent.set_text(content);
		float preferredWidth = this.m_lblContent.get_preferredWidth();
		if (preferredWidth > this.LINE_WIDTH)
		{
			this.RNode2Content.set_anchoredPosition(new Vector2(-(preferredWidth - this.LINE_WIDTH), this.RNode2Content.get_anchoredPosition().y));
		}
		else
		{
			this.RNode2Content.set_anchoredPosition(new Vector2(0f, this.RNode2Content.get_anchoredPosition().y));
		}
		TimerHeap.AddTimer(100u, 0, delegate
		{
			this.ReuseOldFaces();
		});
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
}
