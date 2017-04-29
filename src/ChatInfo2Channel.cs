using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatInfo2Channel : ChatInfoBase
{
	private const int QUESTION_WIDTH = 365;

	private const int under_line_size_more = 4;

	private const int BG_SIZEMORE_X = 45;

	private const int BG_SIZEMORE_Y = 30;

	private const int QUESTION_DY = 60;

	private const int OFFSET_MAIN = 120;

	private const int OFFSET_BG_R = 130;

	private const int OFFSET_SENDER_R = 170;

	private const int OFFSET_SENDERNAEME_R = 125;

	private const int OFFSET_ROOTCONTENT_L = 105;

	private const int OFFSET_ROOTCONTENT_R = -190;

	private const int OFFSET_QUESTIONROOTCONTENT = 175;

	private const int OFFSET_VOICEICON_R = 95;

	private const int OFFSET_VOICEDOT_R = -146;

	private const int OFFSET_VOICETIME_R = 210;

	private const int OFFSET_VOICEDOT_L = -145;

	private const int OFFSET_VOICETIME_L = -125;

	private const int OFFSET_QUESTIONBG = 322;

	private const int MIN_TIME_LENGTH = 2;

	private static readonly Color Color_quInfoBg = new Color(255f, 255f, 255f, 165f);

	private static readonly Color Color_questionContent_GuildRightAnswer = new Color32(80, 255, 20, 255);

	private static readonly Color Color_questionContent_GuildQuestionNotice = new Color32(120, 80, 60, 255);

	private RectTransform m_sender;

	private Image m_spSenderBG;

	private Image m_spSenderIcon;

	private Text m_lblSenderName;

	private Text m_lblSenderTime;

	private Image m_spContentBg;

	private Button m_btnContentBg;

	private RectTransform m_rootContent;

	private RectTransform m_quContentBg;

	private RectTransform m_quContentBg2;

	private Image m_quInfoBg;

	private RectTransform m_rootVoice;

	private RectTransform m_rootVoiceIcon;

	private ImageSequenceFrames m_isfVoiceIcon;

	private RectTransform m_rootVoiceDOT;

	private Text m_lblRootVoiceTime;

	private RectTransform m_rectVIP;

	private LayoutElement m_leVIPName;

	private LayoutElement m_leVIPLevel10;

	private LayoutElement m_leVIPLevel1;

	private Image m_spVIPLevel10;

	private Image m_spVIPLevel1;

	private GameObject m_normalMsg;

	private GameObject m_questionMsg;

	private Text m_questionContent;

	private RectTransform m_questionRootContent;

	private Text m_questionTitle;

	private GameObject m_questionRootTitle;

	private List<GameObject> Colliders = new List<GameObject>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.LINE_WIDTH = 290f;
		this.m_sender = (base.FindTransform("Sender") as RectTransform);
		this.m_spSenderBG = base.FindTransform("Sender").GetComponent<Image>();
		this.m_spSenderIcon = base.FindTransform("SenderIcon").GetComponent<Image>();
		this.m_lblSenderName = base.FindTransform("SenderName").GetComponent<Text>();
		this.m_lblSenderTime = base.FindTransform("SenderTime").GetComponent<Text>();
		this.m_spContentBg = base.FindTransform("ContentBg").GetComponent<Image>();
		this.m_quContentBg = (base.FindTransform("QuestionContentBg") as RectTransform);
		this.m_quContentBg2 = (base.FindTransform("QuestionContentBg2") as RectTransform);
		this.m_quInfoBg = base.FindTransform("QuestionInfoBg").GetComponent<Image>();
		this.m_rectVIP = (base.FindTransform("VIP") as RectTransform);
		this.m_leVIPName = base.FindTransform("VIPName").GetComponent<LayoutElement>();
		this.m_leVIPLevel10 = base.FindTransform("VIPLevel10").GetComponent<LayoutElement>();
		this.m_leVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<LayoutElement>();
		this.m_spVIPLevel10 = base.FindTransform("VIPLevel10").GetComponent<Image>();
		this.m_spVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<Image>();
		this.m_rootContent = (base.FindTransform("RootContent") as RectTransform);
		base.FaceParent = this.m_rootContent;
		this.m_questionRootContent = (base.FindTransform("QuestionRootContent") as RectTransform);
		this.m_normalMsg = base.FindTransform("NormalMsg").get_gameObject();
		this.m_questionMsg = base.FindTransform("QuestionMsg").get_gameObject();
		this.m_questionContent = base.FindTransform("QuestionContent").GetComponent<Text>();
		this.m_questionRootTitle = base.FindTransform("QuestionRootTitle").get_gameObject();
		this.m_questionTitle = base.FindTransform("QuestionTitle").GetComponent<Text>();
		this.m_rootVoice = (base.FindTransform("RootVoice") as RectTransform);
		this.m_rootVoiceIcon = (base.FindTransform("RootVoiceIcon") as RectTransform);
		this.m_isfVoiceIcon = base.FindTransform("RootVoiceIcon").GetComponent<ImageSequenceFrames>();
		this.m_rootVoiceDOT = (base.FindTransform("RootVoiceDOT") as RectTransform);
		this.m_lblRootVoiceTime = base.FindTransform("RootVoiceTime").GetComponent<Text>();
		ButtonCustom expr_281 = base.FindTransform("SenderIcon").GetComponent<ButtonCustom>();
		expr_281.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_281.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickHeadIcon));
		this.m_btnContentBg = this.m_spContentBg.GetComponent<Button>();
		this.m_btnContentBg.get_onClick().AddListener(new UnityAction(this.OnClickBackground));
	}

	private void OnDisable()
	{
		this.SetVoiceIcon(false);
	}

	protected override void ButtonItem(string previous_content, DetailInfo detailInfo, string name)
	{
		if (detailInfo.type == DetailType.DT.Default)
		{
			return;
		}
		float num = 0f;
		float preferredWidthOfSpace = ChatManager.Instance.GetPreferredWidthOfSpace(previous_content, this.FONT_SIZE, this.LINE_WIDTH, ref num);
		int num2 = (int)(preferredWidthOfSpace / this.LINE_WIDTH);
		float num3 = preferredWidthOfSpace % this.LINE_WIDTH;
		float preferredWidthOfSpace2 = ChatManager.Instance.GetPreferredWidthOfSpace(previous_content + name, this.FONT_SIZE, this.LINE_WIDTH, ref num);
		float num4 = preferredWidthOfSpace2 - (float)(num2 + 1) * this.LINE_WIDTH;
		if (num4 < 0f)
		{
			num4 = 0f;
		}
		float num5 = preferredWidthOfSpace2 - preferredWidthOfSpace;
		if (num4 > 0f)
		{
			num5 = num5 - num4 - num;
		}
		Button2Touch buttonTouch = this.GetButtonTouch();
		buttonTouch.SetButton2Touch(this.m_chatInfo, this.m_rootContent, detailInfo, null);
		buttonTouch.m_myRectTransform.set_anchoredPosition(new Vector2(num3, (float)(-(float)this.GetButtonItemY(num2))));
		buttonTouch.Underline(num5, this.FONT_SIZE, true);
		int num6 = 0;
		while (num4 > 0f)
		{
			num6++;
			Button2Touch buttonTouch2 = this.GetButtonTouch();
			buttonTouch2.SetButton2Touch(this.m_chatInfo, this.m_rootContent, detailInfo, null);
			buttonTouch2.m_myRectTransform.set_anchoredPosition(new Vector2(0f, (float)(-(float)this.GetButtonItemY(num2 + num6))));
			if (num4 <= this.LINE_WIDTH)
			{
				if (num6 == 1)
				{
					buttonTouch2.Underline(num4, this.FONT_SIZE, true);
					num4 = 0f;
				}
				else
				{
					buttonTouch2.Underline(num4, this.FONT_SIZE, true);
					num4 = 0f;
				}
			}
			else
			{
				buttonTouch2.Underline(this.LINE_WIDTH, this.FONT_SIZE, true);
				num4 -= this.LINE_WIDTH;
			}
		}
	}

	protected override void ButtonModule(string text_modulePreviour, string moduleHrefString, HrefModule moduleHref)
	{
		string text = text_modulePreviour + moduleHrefString;
		float preferredWidth = ChatManager.Instance.GetPreferredWidth(text, this.FONT_SIZE, true);
		float preferredWidth2 = ChatManager.Instance.GetPreferredWidth(base.get_name(), this.FONT_SIZE, true);
		float num = preferredWidth - preferredWidth2;
		int row = (int)(num / this.LINE_WIDTH);
		Button2Touch buttonTouch = this.GetButtonTouch();
		buttonTouch.SetButton2Touch(this.m_chatInfo, this.m_rootContent, null, moduleHref.m_hrefModuleClick);
		buttonTouch.m_myRectTransform.set_anchoredPosition(new Vector2(num, (float)(-(float)this.GetButtonItemY(row))));
		buttonTouch.Underline(preferredWidth2, this.FONT_SIZE, true);
	}

	private int GetButtonItemY(int row)
	{
		float num = ((float)this.FONT_SIZE + 1.5f) * (float)row + 4f;
		return (int)num;
	}

	private Button2Touch GetButtonTouch()
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Button2Touch");
		this.Colliders.Add(instantiate2Prefab);
		return instantiate2Prefab.GetComponent<Button2Touch>();
	}

	protected override void ClearColliders()
	{
		for (int i = 0; i < this.Colliders.get_Count(); i++)
		{
			Object.Destroy(this.Colliders.get_Item(i));
		}
		this.Colliders.Clear();
	}

	protected override void SetSenderIcon(int channel, int occupation)
	{
		if (channel == 2 && occupation <= 0)
		{
			this.m_spSenderBG.set_enabled(true);
			ResourceManager.SetSprite(this.m_spSenderIcon, ResourceManager.GetCodeSprite("icon_laba"));
		}
		else if (channel == 8)
		{
			this.m_spSenderBG.set_enabled(true);
			ResourceManager.SetSprite(this.m_spSenderIcon, ResourceManager.GetCodeSprite("icon_laba"));
		}
		else if (channel == 64)
		{
			this.m_spSenderBG.set_enabled(true);
			ResourceManager.SetSprite(this.m_spSenderIcon, ResourceManager.GetIconSprite("icon_laba"));
		}
		else if (channel == 32 && occupation <= 0)
		{
			this.m_spSenderBG.set_enabled(true);
			ResourceManager.SetSprite(this.m_spSenderIcon, ResourceManager.GetCodeSprite("icon_laba"));
		}
		else
		{
			this.m_spSenderBG.set_enabled(true);
			ResourceManager.SetSprite(this.m_spSenderIcon, UIUtils.GetRoleSmallIcon(occupation));
		}
	}

	protected override void SetSenderName(string sender_name, string datetime, int viplevel)
	{
		if (this.IsOwn())
		{
			this.m_lblSenderName.set_text(sender_name + this.GetVIPBlank(viplevel));
		}
		else
		{
			this.m_lblSenderName.set_text(this.GetVIPBlank(viplevel) + sender_name);
		}
		this.m_lblSenderTime.set_text(datetime);
	}

	private string GetVIPBlank(int viplevel)
	{
		if (!this.ShowVIP(viplevel))
		{
			return "  ";
		}
		if (viplevel < 10)
		{
			return "            ";
		}
		return "              ";
	}

	protected override void SetVIP(int level)
	{
		if (this.ShowVIP(level))
		{
			base.FindTransform("VIP").get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.m_spVIPLevel10, GameDataUtils.GetNumIcon10(level, NumType.Yellow_light));
			ResourceManager.SetSprite(this.m_spVIPLevel1, GameDataUtils.GetNumIcon1(level, NumType.Yellow_light));
		}
		else
		{
			base.FindTransform("VIP").get_gameObject().SetActive(false);
		}
	}

	private bool ShowVIP(int level)
	{
		return level > 0;
	}

	private void SetBackgroundSize()
	{
		RectTransform rectTransform = this.m_spContentBg.get_transform() as RectTransform;
		if (this.CheckIsQuestionContent())
		{
			float num = (this.GetQuestionContentType() != DetailType.DT.GuildQuestion) ? 30f : 75f;
			this.m_quContentBg.set_sizeDelta(new Vector2(410f, this.m_questionContent.get_preferredHeight() + num));
			this.m_quContentBg2.set_sizeDelta(new Vector2(410f, this.m_questionContent.get_preferredHeight() + num));
			RectTransform rectTransform2 = this.m_quInfoBg.get_transform() as RectTransform;
			rectTransform2.set_sizeDelta(new Vector2(410f, this.m_questionContent.get_preferredHeight() + num));
		}
		else
		{
			rectTransform.set_sizeDelta(new Vector2(Mathf.Max((float)ChatManager.GetVoiceBackgroundSize(2), this.GetWidthOfContent() + 45f), this.m_lblContent.get_preferredHeight() + 30f));
		}
	}

	private float GetWidthOfContent()
	{
		return Mathf.Min(this.LINE_WIDTH, this.m_lblContent.get_preferredWidth());
	}

	private bool IsOwn()
	{
		return this.SenderUID == EntityWorld.Instance.EntSelf.ID;
	}

	private void SetVoiceIcon(bool is_anim)
	{
		if (is_anim)
		{
			this.m_isfVoiceIcon.PlayAnimation2Loop(ChatManager.Instance.VOICE_ANIMS, 0.35f);
		}
		else
		{
			this.m_isfVoiceIcon.Scale = 1f;
			this.m_isfVoiceIcon.IsAnimating = false;
			ResourceManager.SetSprite(this.m_isfVoiceIcon, ResourceManager.GetIconSprite("yuyin3"));
			this.m_isfVoiceIcon.SetNativeSize();
		}
	}

	private void SetSender(bool isOwn, bool isVoice, string sender_name, string datetime)
	{
		if (!isOwn)
		{
			bool flag = this.CheckIsQuestionContent();
			this.m_normalMsg.SetActive(!flag);
			if (this.m_questionMsg != null)
			{
				this.m_questionMsg.SetActive(flag);
			}
			if (flag)
			{
				DetailType.DT questionContentType = this.GetQuestionContentType();
				if (questionContentType == DetailType.DT.GuildQuestionNotice)
				{
					if (this.m_quContentBg != null)
					{
						this.m_quContentBg.get_gameObject().SetActive(false);
					}
					if (this.m_quContentBg2 != null)
					{
						this.m_quContentBg2.get_gameObject().SetActive(false);
					}
					if (this.m_quInfoBg != null)
					{
						this.m_quInfoBg.get_gameObject().SetActive(true);
					}
					if (this.m_questionRootTitle != null)
					{
						this.m_questionRootTitle.SetActive(false);
					}
					if (this.m_questionContent != null)
					{
						this.m_questionContent.set_color(ChatInfo2Channel.Color_questionContent_GuildQuestionNotice);
					}
					if (this.m_questionRootContent != null)
					{
						this.m_questionRootContent.set_anchoredPosition(new Vector2(-175f, this.m_questionRootContent.get_anchoredPosition().y));
					}
				}
				else if (questionContentType == DetailType.DT.GuildQuestion)
				{
					if (this.m_quContentBg != null)
					{
						this.m_quContentBg.get_gameObject().SetActive(true);
					}
					if (this.m_quContentBg2 != null)
					{
						this.m_quContentBg2.get_gameObject().SetActive(true);
					}
					if (this.m_quInfoBg != null)
					{
						this.m_quInfoBg.get_gameObject().SetActive(false);
					}
					if (this.m_questionRootTitle != null)
					{
						this.m_questionRootTitle.SetActive(true);
					}
					if (this.m_questionRootContent != null)
					{
						this.m_questionRootContent.set_anchoredPosition(new Vector2(-210f, -60f));
					}
				}
				else if (questionContentType == DetailType.DT.GuildRightAnswer)
				{
					if (this.m_quContentBg != null)
					{
						this.m_quContentBg.get_gameObject().SetActive(false);
					}
					if (this.m_quContentBg2 != null)
					{
						this.m_quContentBg2.get_gameObject().SetActive(false);
					}
					if (this.m_quInfoBg != null)
					{
						this.m_quInfoBg.get_gameObject().SetActive(true);
					}
					if (this.m_questionRootTitle != null)
					{
						this.m_questionRootTitle.SetActive(false);
					}
					if (this.m_questionRootContent != null)
					{
						this.m_questionRootContent.set_anchoredPosition(new Vector2(-175f, this.m_questionRootContent.get_anchoredPosition().y));
					}
					if (this.m_quInfoBg != null)
					{
						ResourceManager.SetSprite(this.m_quInfoBg, ResourceManager.GetCodeSprite("heidi_yuyin"));
						this.m_quInfoBg.set_color(ChatInfo2Channel.Color_quInfoBg);
					}
					if (this.m_questionContent != null)
					{
						this.m_questionContent.set_color(ChatInfo2Channel.Color_questionContent_GuildRightAnswer);
						this.m_questionContent.set_alignment(1);
					}
					if (this.m_questionContent != null)
					{
						this.m_questionContent.get_rectTransform().set_pivot(ConstVector2.MR);
						this.m_questionContent.get_rectTransform().set_anchoredPosition(new Vector2(175f, 0f));
					}
				}
			}
			else
			{
				ResourceManager.SetSprite(this.m_spContentBg, ResourceManager.GetCodeSprite("talkframe_1"));
				RectTransform rectTransform = this.m_spContentBg.get_transform() as RectTransform;
				rectTransform.set_pivot(new Vector2(0f, 1f));
				rectTransform.set_anchoredPosition(new Vector2(-130f, rectTransform.get_anchoredPosition().y));
				this.m_sender.set_anchoredPosition(new Vector2(-170f, this.m_sender.get_anchoredPosition().y));
				this.m_lblSenderName.set_alignment(3);
				RectTransform rectTransform2 = this.m_lblSenderName.get_transform() as RectTransform;
				rectTransform2.set_pivot(new Vector2(0f, 0.5f));
				rectTransform2.set_anchoredPosition(new Vector2(-125f, rectTransform2.get_anchoredPosition().y));
				this.m_rectVIP.set_pivot(new Vector2(0f, 0.5f));
				this.m_rectVIP.set_anchoredPosition(new Vector2(rectTransform2.get_anchoredPosition().x, this.m_rectVIP.get_anchoredPosition().y));
				this.m_rectVIP.GetComponent<HorizontalLayoutGroup>().set_padding(new RectOffset(0, 128, 0, 0));
				this.m_rectVIP.GetComponent<HorizontalLayoutGroup>().set_childAlignment(0);
				this.m_rootContent.set_anchoredPosition(new Vector2(-105f, this.m_rootContent.get_anchoredPosition().y));
				this.m_rootVoice.get_gameObject().SetActive(isVoice);
				if (isVoice)
				{
					this.m_rootVoice.set_anchoredPosition(new Vector2(0f, this.m_rootVoice.get_anchoredPosition().y));
					this.m_rootVoiceIcon.set_localEulerAngles(new Vector3(0f, 0f, 0f));
					this.m_rootVoiceIcon.set_anchoredPosition(new Vector2(-95f, this.m_rootVoiceIcon.get_anchoredPosition().y));
					this.m_rootVoiceDOT.set_pivot(new Vector2(0f, 0.5f));
					this.m_rootVoiceDOT.set_anchoredPosition(new Vector2(rectTransform.get_sizeDelta().x + -145f, this.m_rootVoiceDOT.get_anchoredPosition().y));
					this.m_lblRootVoiceTime.get_rectTransform().set_pivot(new Vector2(0f, 0.5f));
					this.m_lblRootVoiceTime.get_rectTransform().set_anchoredPosition(new Vector2(rectTransform.get_sizeDelta().x + -125f, this.m_lblRootVoiceTime.get_rectTransform().get_anchoredPosition().y));
				}
			}
		}
		else
		{
			this.m_normalMsg.SetActive(true);
			ResourceManager.SetSprite(this.m_spContentBg, ResourceManager.GetCodeSprite("talkframe_2"));
			RectTransform rectTransform3 = this.m_spContentBg.get_transform() as RectTransform;
			rectTransform3.set_pivot(new Vector2(1f, 1f));
			rectTransform3.set_anchoredPosition(new Vector2(130f, rectTransform3.get_anchoredPosition().y));
			this.m_sender.set_anchoredPosition(new Vector2(170f, this.m_sender.get_anchoredPosition().y));
			this.m_lblSenderName.set_alignment(5);
			RectTransform rectTransform4 = this.m_lblSenderName.get_transform() as RectTransform;
			rectTransform4.set_pivot(new Vector2(1f, 0.5f));
			rectTransform4.set_anchoredPosition(new Vector2(125f, rectTransform4.get_anchoredPosition().y));
			this.m_rectVIP.set_pivot(new Vector2(1f, 0.5f));
			this.m_rectVIP.set_anchoredPosition(new Vector2(rectTransform4.get_anchoredPosition().x - 4f, this.m_rectVIP.get_anchoredPosition().y));
			this.m_rectVIP.GetComponent<HorizontalLayoutGroup>().set_padding(new RectOffset(0, 0, 0, 0));
			this.m_rectVIP.GetComponent<HorizontalLayoutGroup>().set_childAlignment(2);
			this.m_rootContent.set_anchoredPosition(new Vector2(-190f + this.LINE_WIDTH - this.GetWidthOfContent(), this.m_rootContent.get_anchoredPosition().y));
			this.m_rootVoice.get_gameObject().SetActive(isVoice);
			if (isVoice)
			{
				this.m_rootVoice.set_anchoredPosition(new Vector2(0f, this.m_rootVoice.get_anchoredPosition().y));
				this.m_rootVoiceIcon.set_localEulerAngles(new Vector3(0f, 180f, 0f));
				this.m_rootVoiceIcon.set_anchoredPosition(new Vector2(95f, this.m_rootVoiceIcon.get_anchoredPosition().y));
				this.m_rootVoiceDOT.set_pivot(new Vector2(1f, 0.5f));
				this.m_rootVoiceDOT.set_anchoredPosition(new Vector2(-rectTransform3.get_sizeDelta().x - -146f, this.m_rootVoiceDOT.get_anchoredPosition().y));
				this.m_lblRootVoiceTime.get_rectTransform().set_pivot(new Vector2(1f, 0.5f));
				this.m_lblRootVoiceTime.get_rectTransform().set_anchoredPosition(new Vector2(-rectTransform3.get_sizeDelta().x + 210f, this.m_lblRootVoiceTime.get_rectTransform().get_anchoredPosition().y));
			}
		}
	}

	private bool CheckIsQuestionContent()
	{
		bool result = false;
		if (this.m_chatInfo.src_channel == 2 && this.m_chatInfo.items != null)
		{
			for (int i = 0; i < this.m_chatInfo.items.get_Count(); i++)
			{
				DetailInfo detailInfo = this.m_chatInfo.items.get_Item(i);
				if (detailInfo.type == DetailType.DT.GuildQuestion || detailInfo.type == DetailType.DT.GuildQuestionNotice || detailInfo.type == DetailType.DT.GuildRightAnswer)
				{
					result = true;
				}
			}
		}
		return result;
	}

	private DetailType.DT GetQuestionContentType()
	{
		DetailType.DT result = DetailType.DT.Equipment;
		if (this.m_chatInfo.src_channel == 2 && this.m_chatInfo.items != null)
		{
			result = this.m_chatInfo.items.get_Item(0).type;
		}
		return result;
	}

	private void OnClickHeadIcon(GameObject sender)
	{
		if (sender != null)
		{
			ChatManager.OnClickRole(this.SenderUID, this.SenderName, sender.get_transform(), base.GuildId);
		}
	}

	private void OnClickBackground()
	{
		if (ChatManager.IsVoice(this.m_chatInfo))
		{
			this.m_rootVoiceDOT.get_gameObject().SetActive(false);
			if (this.m_chatInfo == null || this.m_chatInfo.items.get_Count() <= 0)
			{
				return;
			}
			ChatManager.Instance.SendVoiceReq(this.m_chatInfo.items.get_Item(0).id, this);
		}
	}

	public void SpeechPlay(byte[] audio, long uuid)
	{
		this.SetVoiceIcon(true);
		VoiceSDKManager.Instance.SpeechPlay(audio, uuid, delegate
		{
			this.SetVoiceIcon(false);
		});
	}

	public bool CheckVoice(long uuid)
	{
		if (base.get_gameObject() == null)
		{
			return false;
		}
		if (ChatManager.IsVoice(this.m_chatInfo))
		{
			if (this.m_chatInfo == null || this.m_chatInfo.items.get_Count() <= 0)
			{
				return false;
			}
			if (this.m_chatInfo.items.get_Item(0).id == uuid)
			{
				return true;
			}
		}
		return false;
	}

	protected override void SetPreferredHeight()
	{
		if (this.CheckIsQuestionContent())
		{
			if (this.GetQuestionContentType() == DetailType.DT.GuildQuestion)
			{
				this.m_height = -this.m_questionContent.get_preferredHeight() - 75f - 50f;
			}
			else if (this.GetQuestionContentType() == DetailType.DT.GuildQuestionNotice)
			{
				this.m_height = -this.m_questionContent.get_preferredHeight() - 50f;
			}
			else
			{
				this.m_height = -this.m_questionContent.get_preferredHeight() - 50f;
			}
		}
		else
		{
			this.m_height = -this.m_lblContent.get_preferredHeight() - 82f;
		}
	}

	protected override void SetContentSize(string datetime)
	{
		if (ChatManager.IsVoice(this.m_chatInfo))
		{
			this.m_btnContentBg.set_enabled(true);
			int voiceTime = ChatManager.GetVoiceTime(this.m_chatInfo);
			this.m_lblRootVoiceTime.set_text(voiceTime + "''");
			int voiceBackgroundSize = ChatManager.GetVoiceBackgroundSize(Mathf.Max(2, voiceTime));
			if (string.IsNullOrEmpty(this.m_lblContent.get_text()))
			{
				this.m_lblContent.set_text(ChatManager.GetBlank((float)voiceBackgroundSize, this.FONT_SIZE));
			}
			else
			{
				this.m_lblContent.set_text("\n" + this.m_lblContent.get_text());
			}
			this.m_rootVoiceDOT.get_gameObject().SetActive(true);
			this.SetBackgroundSize();
			this.SetSender(this.IsOwn(), true, this.SenderName, datetime);
		}
		else
		{
			this.m_btnContentBg.set_enabled(false);
			if (this.CheckIsQuestionContent())
			{
				RectTransform rectTransform = this.m_questionContent.get_transform() as RectTransform;
				rectTransform.set_sizeDelta(new Vector2(325f, ChatInfoBase.GetContentHeight(this.m_questionContent)));
			}
			else
			{
				RectTransform rectTransform2 = this.m_lblContent.get_transform() as RectTransform;
				rectTransform2.set_sizeDelta(new Vector2(this.LINE_WIDTH, ChatInfoBase.GetContentHeight(this.m_lblContent)));
			}
			this.SetBackgroundSize();
			this.SetSender(this.IsOwn(), false, this.SenderName, datetime);
		}
		this.SetPreferredHeight();
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
		if (this.CheckIsQuestionContent())
		{
			text_chatContent = this.DealQuestionContent(text_chatContent);
			this.m_questionContent.set_text(text_chatContent);
		}
		base.SetContent(text_chatContent, datetime);
	}

	protected override string GetSenderName(int channel, string senderName)
	{
		if (string.IsNullOrEmpty(senderName))
		{
			return string.Empty;
		}
		string result = string.Empty;
		if (channel != 1)
		{
			if (channel != 8)
			{
				result = senderName;
			}
			else
			{
				result = TextColorMgr.GetColorByID(senderName, 1000007);
			}
		}
		else
		{
			result = senderName;
		}
		return result;
	}

	protected override string GetContentTextInColor(string content)
	{
		return content;
	}

	protected override string GetContent()
	{
		if (this.m_chatInfo.src_channel == 64)
		{
			return TextColorMgr.GetColorByID(GameDataUtils.GetChineseContent(502004, false), 210) + this.m_chatInfo.chat_content;
		}
		return this.m_chatInfo.chat_content;
	}

	private string DealQuestionContent(string content)
	{
		DetailType.DT questionContentType = this.GetQuestionContentType();
		string text = string.Empty;
		string[] array = content.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				if (i == 0 && questionContentType == DetailType.DT.GuildQuestion)
				{
					this.m_questionTitle.set_text(string.Format("第{0}题", array[i]));
				}
				else if (questionContentType == DetailType.DT.GuildRightAnswer)
				{
					text = array[i];
				}
				else if (questionContentType == DetailType.DT.GuildQuestionNotice)
				{
					GameObject gameObject = new GameObject();
					gameObject.set_name("diamond" + i);
					gameObject.AddComponent<Image>();
					Image component = gameObject.GetComponent<Image>();
					component.get_rectTransform().SetParent(this.m_questionRootContent);
					component.get_rectTransform().set_localPosition(new Vector3(0f, -12.5f - (float)(27 * i), 0f));
					component.get_rectTransform().set_localScale(new Vector3(1f, 1f, 1f));
					component.get_rectTransform().set_sizeDelta(new Vector2(25f, 25f));
					ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("j_diamond001"));
					text += array[i];
					if (i != array.Length - 1 && array.Length > 0)
					{
						text += "\n";
					}
				}
				else
				{
					text += array[i];
				}
			}
		}
		if (questionContentType == DetailType.DT.GuildQuestion)
		{
			text = "<color=#50321E><size=22>" + text + "</size></color>";
		}
		return text;
	}
}
