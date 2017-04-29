using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BroadcastUI : UIBase
{
	public static BroadcastUI Instance;

	protected Text content;

	protected float duration;

	private float dst;

	protected Vector3 init_pos = new Vector3((float)(Screen.get_width() / 4), 0f, 0f);

	private int TimePerQuaterScreen = (int)float.Parse(DataReader<GlobalParams>.Get("timePerQuaterScreen").value);

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isClick = true;
		this.isInterruptStick = false;
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		BroadcastUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.content = base.FindTransform("Content").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	public void ShowBroadcast(string _content)
	{
		this.content.set_text(_content);
		this.content.get_rectTransform().set_localPosition(this.init_pos);
		this.content.get_rectTransform().set_sizeDelta(new Vector2(this.content.get_preferredWidth() + 30f, this.content.get_rectTransform().get_sizeDelta().y));
		if (UIManagerControl.Instance.IsOpen("TownUI"))
		{
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
			base.get_transform().SetSiblingIndex(uIIfExist.get_transform().GetSiblingIndex() + 1);
			this.Show(true);
		}
		this.dst = (float)(Screen.get_width() * 4 / 5) + this.content.get_preferredWidth();
		this.duration = this.dst / (float)(Screen.get_width() / 4) * (float)this.TimePerQuaterScreen;
		this.Move(this.dst, this.duration);
	}

	private void Move(float dst, float duration)
	{
		float dst_per_frame = 0f;
		uint t = 0u;
		float already_move_x = 0f;
		Vector3 now_pos = this.content.get_rectTransform().get_localPosition();
		dst_per_frame = dst / duration * 20f;
		if (already_move_x < dst)
		{
			t = TimerHeap.AddTimer(0u, 20, delegate
			{
				if (already_move_x < dst)
				{
					now_pos.x -= dst_per_frame;
					this.content.get_rectTransform().set_localPosition(now_pos);
					already_move_x += dst_per_frame;
				}
				else
				{
					TimerHeap.DelTimer(t);
					BroadcastManager.Instance.MoveOver();
				}
			});
		}
	}
}
