using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountBackUI : UIBase
{
	public Transform quan;

	public Text Desc;

	public int time;

	private Text Num;

	public float speed = 1.5f;

	private float seconds;

	private int fxUid;

	private int soundId;

	private uint timeriD;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.Num = base.FindTransform("Num").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.seconds = (float)this.time;
	}

	private void UpdateCountDown()
	{
		if (this.seconds <= 0f)
		{
			this.seconds = 0f;
			if (this.timeriD != 0u)
			{
				TimerHeap.DelTimer(this.timeriD);
			}
			this.Show(false);
			UIManagerControl.Instance.UnLoadUIPrefab("TimeCountBackUI");
		}
		else
		{
			int fxId = 0;
			if (this.seconds > 3f)
			{
				fxId = 207;
				this.soundId = 10054;
			}
			else if (this.seconds > 2f)
			{
				fxId = 208;
				this.soundId = 10055;
			}
			else if (this.seconds > 1f)
			{
				fxId = 209;
				this.soundId = 10056;
			}
			else
			{
				fxId = 210;
				Animator component = base.GetComponent<Animator>();
				if (component)
				{
					component.Play("countdown_end");
				}
				this.soundId = 10057;
			}
			SoundManager.PlayUI(this.soundId, false);
			FXSpineManager.Instance.DeleteSpine(this.fxUid, true);
			this.fxUid = FXSpineManager.Instance.ReplaySpine(0, fxId, this.Num.get_transform(), "LuckDrawResult", 14001, delegate
			{
				if (fxId == 210)
				{
					UIManagerControl.Instance.UnLoadUIPrefab("TimeCountBackUI");
				}
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.seconds -= 1f;
		}
		if (this.quan != null)
		{
			this.quan.set_eulerAngles(new Vector3(this.quan.get_eulerAngles().x, this.quan.get_eulerAngles().y, this.quan.get_eulerAngles().z + this.speed));
		}
	}

	public void StartTimeCountBack(int countBackSeconds, Action finishAction = null)
	{
		FXSpineManager.Instance.DeleteSpine(this.fxUid, true);
		this.seconds = (float)countBackSeconds;
		if (this.timeriD != 0u)
		{
			TimerHeap.DelTimer(this.timeriD);
		}
		this.timeriD = TimerHeap.AddTimer(0u, 1000, new Action(this.UpdateCountDown));
		string chineseContent = GameDataUtils.GetChineseContent(InstanceManager.CurrentInstance.InstanceData.countdownInfo, false);
		this.Desc.set_text(chineseContent);
		this.Desc.get_transform().get_parent().get_gameObject().SetActive(chineseContent != null && chineseContent.get_Length() > 0);
		base.GetComponent<Animator>().Play("countdown_begin");
	}
}
