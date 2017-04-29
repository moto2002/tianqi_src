using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUI : UIBase
{
	public enum TalkDirType
	{
		Right,
		Left
	}

	public static TalkUI Instance;

	private Image rightBody;

	private Text rightName;

	private Image leftBody;

	private Text leftName;

	private Textwriter Content;

	private List<int> contentId = new List<int>();

	private int index;

	private int lastCurrentID = -1;

	private TalkUI.TalkDirType mCurrentType;

	private Animator leftAnim;

	private Animator rightAnim;

	private int mNpcId;

	private uint mDelayId;

	private bool IsAddCallBack;

	private Action TalkEndCallBack;

	private bool IsUnlockGuide;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isEndNav = false;
	}

	private void Awake()
	{
		TalkUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		GuideManager.Instance.out_system_lock = true;
	}

	protected override void InitUI()
	{
		this.Content = base.FindTransform("Content").GetComponent<Textwriter>();
		this.leftAnim = base.FindTransform("Left").GetComponent<Animator>();
		this.leftBody = base.FindTransform("LeftBody").GetComponent<Image>();
		this.leftName = base.FindTransform("LeftName").GetComponent<Text>();
		this.rightAnim = base.FindTransform("Right").GetComponent<Animator>();
		this.rightBody = base.FindTransform("RightBody").GetComponent<Image>();
		this.rightName = base.FindTransform("RightName").GetComponent<Text>();
		ButtonCustom expr_AA = base.FindTransform("Next").GetComponent<ButtonCustom>();
		expr_AA.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_AA.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickNext));
	}

	private void AddMask()
	{
		base.SetMask(0f, true, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsFirstSibling();
		this.IsUnlockGuide = false;
		GuideManager.Instance.out_system_lock = true;
		SoundManager.SetBGMFade(false);
		if (TownUI.Instance != null)
		{
			TownUI.Instance.SwitchRightBottom(false, true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (!this.IsUnlockGuide)
		{
			GuideManager.Instance.out_system_lock = false;
		}
		UIStateSystem.LockOfClickInterval(350u);
		this.ResetAll();
		SoundManager.SetBGMFade(true);
		this.DeleteTimer();
	}

	protected void DeleteTimer()
	{
		if (this.mDelayId > 0u)
		{
			TimerHeap.DelTimer(this.mDelayId);
			this.mDelayId = 0u;
		}
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (!calledDestroy)
		{
			EventDispatcher.Broadcast("GuideManager.TalkUIClose");
			MainTaskManager.Instance.ShowTalkUINpc = 0;
			UIStackManager.Instance.PopTownUI();
		}
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			TalkUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickNext(GameObject go = null)
	{
		if (!this.IsAddCallBack)
		{
			return;
		}
		if (this.Content.IsWritering)
		{
			this.Content.StopText();
			return;
		}
		this.UpdateUI();
	}

	public void UpdateUI()
	{
		if (this.index >= this.contentId.get_Count())
		{
			this.OnClose();
			this.mNpcId = 0;
			this.index = 0;
			if (this.TalkEndCallBack != null)
			{
				this.TalkEndCallBack.Invoke();
			}
			return;
		}
		int talkContentId;
		int body;
		if (this.mNpcId > 0)
		{
			talkContentId = this.contentId.get_Item(this.index);
			body = this.mNpcId;
			this.mCurrentType = TalkUI.TalkDirType.Right;
		}
		else
		{
			DuiHuaPeiZhi duiHuaPeiZhi = DataReader<DuiHuaPeiZhi>.Get(this.contentId.get_Item(this.index));
			if (duiHuaPeiZhi == null)
			{
				Debug.Log("<color=red>Error:</color>此句话在对话表里面没有被找到：" + this.contentId.get_Item(this.index));
				this.OnClose();
				if (this.TalkEndCallBack != null)
				{
					this.TalkEndCallBack.Invoke();
				}
				return;
			}
			talkContentId = duiHuaPeiZhi.word;
			body = duiHuaPeiZhi.body;
			this.mCurrentType = ((duiHuaPeiZhi.picSide != 1) ? TalkUI.TalkDirType.Right : TalkUI.TalkDirType.Left);
		}
		this.TalkContent(talkContentId, body);
		if (this.lastCurrentID != body)
		{
			this.SetUI(body, this.mCurrentType);
		}
		this.lastCurrentID = body;
		this.index++;
	}

	private void TalkContent(int talkContentId, int npcId)
	{
		string text = GameDataUtils.GetChineseContent(talkContentId, false);
		if (text.Contains("{p0}"))
		{
			text = text.Replace("{p0}", EntityWorld.Instance.EntSelf.Name);
		}
		this.Content.SetText(text);
	}

	private void SetUI(int npcId, TalkUI.TalkDirType MeSpeak)
	{
		string text = string.Empty;
		int id = 0;
		if (npcId > 0)
		{
			NPC nPC = DataReader<NPC>.Get(npcId);
			if (nPC != null)
			{
				text = GameDataUtils.GetChineseContent(nPC.name, false);
				id = nPC.pic;
			}
		}
		else
		{
			text = EntityWorld.Instance.EntSelf.Name;
			id = DataReader<ZhuanZhiJiChuPeiZhi>.Get(EntityWorld.Instance.EntSelf.TypeID).jobPic;
		}
		if (MeSpeak != TalkUI.TalkDirType.Right)
		{
			if (MeSpeak == TalkUI.TalkDirType.Left)
			{
				this.rightAnim.Play("mainTalkRight");
				this.leftAnim.Play("mainTalkLeftBack");
				this.leftBody.get_gameObject().SetActive(true);
				this.leftName.set_text(text);
				ResourceManager.SetSprite(this.leftBody, GameDataUtils.GetIcon(id));
			}
		}
		else
		{
			this.leftAnim.Play("mainTalkLeft");
			this.rightAnim.Play("mainTalkRightBack");
			this.rightBody.get_gameObject().SetActive(true);
			this.rightName.set_text(text);
			ResourceManager.SetSprite(this.rightBody, GameDataUtils.GetIcon(id));
		}
	}

	internal void AddCallBack(List<int> talkIds, bool isMask, Action CallBack, int npcId = 0)
	{
		this.ResetAll();
		this.mNpcId = npcId;
		this.contentId = talkIds;
		this.TalkEndCallBack = CallBack;
		if (isMask)
		{
			this.AddMask();
		}
		this.IsAddCallBack = true;
		this.UpdateUI();
		this.DeleteTimer();
		if (MainTaskManager.Instance.AutoTaskId > 0)
		{
			this.mDelayId = TimerHeap.AddTimer(600u, 600, delegate
			{
				this.OnClickNext(null);
			});
		}
	}

	private void ResetAll()
	{
		this.IsAddCallBack = false;
		this.lastCurrentID = -1;
		this.index = 0;
		if (this.leftBody != null)
		{
			this.leftBody.get_gameObject().SetActive(false);
		}
		if (this.rightBody != null)
		{
			this.rightBody.get_gameObject().SetActive(false);
		}
	}

	private void OnClose()
	{
		this.Show(false);
	}

	public void CloseNoUnlockGuide()
	{
		this.IsUnlockGuide = true;
		this.Show(false);
	}
}
