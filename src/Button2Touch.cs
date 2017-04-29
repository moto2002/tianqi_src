using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button2Touch : MonoBehaviour
{
	public enum InterfaceEnum
	{
		HandleTeamWorldInvite = 1,
		HandleWorldInvite
	}

	private const string under_line_string = "_";

	public RectTransform m_myRectTransform;

	private Button m_button2Collider;

	private Action m_action2Click;

	private DetailInfo m_detailInfo;

	private Text m_underline;

	private Shadow m_underline_shadow;

	protected ChatManager.ChatInfo m_chatInfo;

	private void Awake()
	{
		this.m_underline = base.get_gameObject().GetComponent<Text>();
		this.m_underline_shadow = base.get_gameObject().GetComponent<Shadow>();
		this.m_myRectTransform = base.get_gameObject().GetComponent<RectTransform>();
	}

	private void OnButtonClick()
	{
		if (this.m_action2Click != null)
		{
			this.m_action2Click.Invoke();
		}
		else
		{
			this.OnClickLink(this.m_detailInfo);
		}
	}

	private void OnClickLink(DetailInfo detailInfo)
	{
		if (detailInfo == null)
		{
			return;
		}
		if (detailInfo.type == DetailType.DT.Equipment)
		{
			ItemTipUIViewModel.ShowItem(this.m_detailInfo.cfgId, null);
		}
		else if (detailInfo.type == DetailType.DT.Role)
		{
			ChatManager.OnClickRole(this.m_detailInfo.id, this.m_detailInfo.label, base.get_transform(), 0L);
		}
		else if (detailInfo.type == DetailType.DT.UI)
		{
			if (LinkNavigationManager.BroadcastLink(detailInfo.cfgId))
			{
				UIManagerControl.Instance.HideUI("ChatUI");
			}
		}
		else if (detailInfo.type == DetailType.DT.Interface)
		{
			GuangBoLianJie guangBoLianJie = DataReader<GuangBoLianJie>.Get(detailInfo.cfgId);
			if (guangBoLianJie == null)
			{
				return;
			}
			if (guangBoLianJie.@interface == 2)
			{
				this.WorldInviteInterface(detailInfo);
			}
		}
	}

	private void WorldInviteInterface(DetailInfo detailInfo)
	{
		if (!SystemOpenManager.IsSystemClickOpen(59, 0, true))
		{
			return;
		}
		if (this.m_chatInfo == null)
		{
			return;
		}
		DetailInfo detailInfo2 = null;
		for (int i = 0; i < this.m_chatInfo.items.get_Count(); i++)
		{
			if (this.m_chatInfo.items.get_Item(i).type == DetailType.DT.Role)
			{
				detailInfo2 = this.m_chatInfo.items.get_Item(i);
				break;
			}
		}
		if (detailInfo2 != null)
		{
			TeamBasicManager.Instance.HandleWorldInvite(detailInfo2.id, detailInfo2.label, (uint)detailInfo.num, detailInfo.label);
		}
		else
		{
			TeamBasicManager.Instance.HandleWorldInvite(detailInfo.id, string.Empty, (uint)detailInfo.num, detailInfo.label);
		}
	}

	public void SetButton2Touch(ChatManager.ChatInfo chatInfo, Transform parent, DetailInfo detailInfo, Action callback = null)
	{
		this.m_chatInfo = chatInfo;
		UGUITools.ResetTransform(this.m_myRectTransform, parent);
		this.m_myRectTransform.set_anchorMin(new Vector2(0f, 0.5f));
		this.m_myRectTransform.set_anchorMax(new Vector2(0f, 0.5f));
		this.m_myRectTransform.set_pivot(new Vector2(0f, 1f));
		if (this.m_button2Collider == null)
		{
			this.m_button2Collider = this.m_myRectTransform.get_gameObject().AddComponent<Button>();
		}
		this.m_button2Collider.set_transition(0);
		this.m_button2Collider.get_onClick().AddListener(new UnityAction(this.OnButtonClick));
		this.m_action2Click = callback;
		this.m_detailInfo = detailInfo;
	}

	public void Underline(float button_width, int button_height, bool showItemColor = true)
	{
		if (this.m_myRectTransform == null)
		{
			return;
		}
		this.m_myRectTransform.set_sizeDelta(new Vector2(button_width, (float)button_height));
		string text = string.Empty;
		float num = this.m_myRectTransform.get_sizeDelta().x;
		int fontSize = button_height - 4;
		float preferredWidth = ChatManager.Instance.GetPreferredWidth("_", fontSize, false);
		if (preferredWidth <= 0f)
		{
			return;
		}
		while (num > 0f)
		{
			text += "_";
			num -= preferredWidth;
		}
		string color = "301414";
		if (this.m_detailInfo != null)
		{
			if (this.m_detailInfo.type == DetailType.DT.Interface)
			{
				color = "28c800";
			}
			else if (this.m_detailInfo.type == DetailType.DT.Equipment && showItemColor)
			{
				Items items = DataReader<Items>.Get(this.m_detailInfo.cfgId);
				if (items != null)
				{
					color = TextColorMgr.RGB.GetRGB(items.color);
				}
			}
		}
		this.m_underline_shadow.set_effectColor(XUtility.ReturnColorFromString(color, 128));
		this.m_underline.set_text(TextColorMgr.GetColor(text, color, string.Empty));
		this.m_underline.set_fontSize(fontSize);
	}
}
