using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatTalkTipUIView : UIBase
{
	public const int MAX_SECOND = 60;

	private const int TIP_SECOND = 10;

	public static UIBase Instance;

	private GameObject m_goStatusRecord;

	private GameObject m_goStatusCancel;

	private List<Image> Volumns = new List<Image>();

	private readonly int VolumnNum = 6;

	private Text m_lblTimeTip;

	private bool m_statusIsRecord;

	private int m_fingerId;

	private Vector2 m_srcTouchPos = Vector2.get_zero();

	private static float m_srcTime;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		ChatTalkTipUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
		this.SetSrcPos();
		this.SetSrcTime();
		this.SetStatus(true);
		VoiceSDKManager.Instance.SpeechRecordStart();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_goStatusRecord = base.FindTransform("StatusRecord").get_gameObject();
		this.m_goStatusCancel = base.FindTransform("StatusCancel").get_gameObject();
		this.LoadVolumns();
		this.m_lblTimeTip = base.FindTransform("TimeTip").GetComponent<Text>();
	}

	public void SetVolumnNum()
	{
		int volumn = VoiceSDKManager.Instance.GetVolumn();
		for (int i = 0; i < this.Volumns.get_Count(); i++)
		{
			this.Volumns.get_Item(i).set_enabled(i < volumn);
		}
	}

	private void SetSrcPos()
	{
		this.m_fingerId = InputManager.GetCurrentFingerID();
		this.m_srcTouchPos = InputManager.GetTouchPositionBaseBottomL(this.m_fingerId);
	}

	private void Update()
	{
		if (this.m_statusIsRecord)
		{
			this.SetTime();
			this.SetVolumnNum();
			this.CheckCancel();
		}
	}

	private void CheckCancel()
	{
		if (this.IsCancel())
		{
			VoiceSDKManager.Instance.SpeechRecordCancel();
			this.SetStatus(false);
			if (ChatUIViewModel.Instance != null)
			{
				ChatUIViewModel.Instance.IsSayCancel = true;
			}
		}
	}

	private bool IsCancel()
	{
		Vector2 touchPositionBaseBottomL = InputManager.GetTouchPositionBaseBottomL(this.m_fingerId);
		return (new Vector2(touchPositionBaseBottomL.x, touchPositionBaseBottomL.y) - new Vector2(this.m_srcTouchPos.x, this.m_srcTouchPos.y)).get_normalized().y > 0f && Mathf.Abs(touchPositionBaseBottomL.y - this.m_srcTouchPos.y) > 60f;
	}

	private void LoadVolumns()
	{
		this.Volumns.Clear();
		for (int i = 1; i <= this.VolumnNum; i++)
		{
			this.Volumns.Add(base.FindTransform("Volumns" + i).GetComponent<Image>());
		}
	}

	private void SetStatus(bool statusIsRecord)
	{
		this.m_statusIsRecord = statusIsRecord;
		this.m_goStatusRecord.SetActive(statusIsRecord);
		this.m_goStatusCancel.SetActive(!statusIsRecord);
	}

	private void SetSrcTime()
	{
		ChatTalkTipUIView.m_srcTime = Time.get_realtimeSinceStartup();
		this.SetTime();
	}

	private void SetTime()
	{
		int second = ChatTalkTipUIView.GetSecond();
		int num = 60 - second;
		if (num > 10)
		{
			this.m_lblTimeTip.set_text(string.Empty);
		}
		else
		{
			this.m_lblTimeTip.set_text(TextColorMgr.GetColorByID(num + "s", 1000007));
		}
		if (num <= 0 && ChatUIViewModel.Instance != null)
		{
			ChatUIViewModel.Instance.SendMessageOfVoice();
		}
	}

	public static int GetSecond()
	{
		return Mathf.Max(0, (int)(Time.get_realtimeSinceStartup() - ChatTalkTipUIView.m_srcTime));
	}
}
